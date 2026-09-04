from __future__ import annotations

"""
Build EnKolayCari2026_FullSchema.sql — clean install (DROP + CREATE).

Fixes prior run failures:
- UTF-8 BOM for SSMS
- DROP existing DB so objects are not duplicated
- AI Moduls (not legacy Modul)
- No sysdiagrams
- Product DDL regenerated with sanitized identifiers
"""

import re
from pathlib import Path

AI_SOURCE = Path(r"C:\Dev\GitHub\AIProjeMimari\docs\examples\sql\00-AILE-BOS-VERITABANI-BASLANGIC.sql")
PRODUCT_SOURCE = Path(r"C:\Dev\GitHub\EnKolayCari\EnkolayCari2026\database\EnKolayCari2026_CreateDatabase.sql")
OUTPUT = Path(r"C:\Dev\GitHub\EnKolayCari\EnkolayCari2026\database\EnKolayCari2026_FullSchema.sql")
DB_NAME = "EKCN2026"

AI_TABLES = {
    "common.Branch", "common.Country", "common.CountryHolidays", "common.CodeDef",
    "common.Languages", "common.TranslationDef", "common.LabelDef", "common.LabelPool",
    "common.GroupDef", "common.GroupDefCountry", "common.ProcessType", "common.ProcessFlow",
    "common.ProcessTypePersonal", "common.AppSettings", "common.AppExceptionLog", "common.MailLog",
    "common.PersonalBranch", "common.PersonalCompany", "common.PersonalGroup",
    "common.PersonalGroupDef", "common.PersonalLoginActivity", "common.PersonalWidget",
    "dbo.AspNetRoleClaims", "dbo.AspNetUserClaims", "dbo.AspNetUserLogins", "dbo.AspNetUserTokens",
}

REQUIRED_TABLES = {
    "common.Branch", "common.Country", "common.CountryHolidays", "common.CodeDef",
    "common.Languages", "common.TranslationDef", "common.LabelDef", "common.LabelPool",
    "common.GroupDef", "common.GroupDefCountry", "common.ProcessType", "common.ProcessFlow",
    "common.ProcessTypePersonal", "common.AppSettings", "common.AppExceptionLog", "common.MailLog",
    "common.PersonalBranch", "common.PersonalCompany", "common.PersonalGroup",
    "common.PersonalGroupDef", "common.PersonalLoginActivity", "common.PersonalWidget",
    "common.Company", "common.Personal", "common.City", "common.FileHeader", "common.FileBlob",
    "common.License", "common.LicenseType", "common.Token", "common.PageDef", "common.LegacyRole",
    "common.CentralCurrency", "common.CentralCurrencyRate", "common.Counter",
    "common.CounterReference", "common.EmailTemplate", "common.Moduls",
    "common.LegacyTransferMap",
    "dbo.AspNetUsers", "dbo.AspNetRoles", "dbo.AspNetUserRoles", "dbo.AspNetUserClaims",
    "dbo.AspNetRoleClaims", "dbo.AspNetUserLogins", "dbo.AspNetUserTokens", "dbo.MenuRole",
    "finance.Account", "inventory.Product", "inventory.Store",
    "trade.TradeDocument", "report.ReportDef",
}

NAME = r"(?:\[([^\]]+)\]|([A-Za-z_]\w*))"


def qname(match: re.Match[str], offset: int = 1) -> str:
    return f"{match.group(offset) or match.group(offset + 1)}.{match.group(offset + 2) or match.group(offset + 3)}"


def split_batches(sql: str) -> list[str]:
    return [part.strip() for part in re.split(r"(?im)^\s*GO\s*$", sql) if part.strip()]


def table_after(pattern: str, text: str) -> str | None:
    match = re.search(pattern + r"\s*" + NAME + r"\s*\.\s*" + NAME, text, re.I)
    return qname(match) if match else None


def extract_create_tables(sql: str) -> dict[str, list[str]]:
    result: dict[str, list[str]] = {}
    create_re = re.compile(r"CREATE\s+TABLE\s*" + NAME + r"\s*\.\s*" + NAME + r"\s*\(", re.I)
    for match in create_re.finditer(sql):
        table = qname(match)
        start = match.end()
        depth, i = 1, start
        in_string = False
        while i < len(sql) and depth:
            char = sql[i]
            if char == "'":
                if in_string and i + 1 < len(sql) and sql[i + 1] == "'":
                    i += 2
                    continue
                in_string = not in_string
            elif not in_string:
                if char == "(":
                    depth += 1
                elif char == ")":
                    depth -= 1
            i += 1
        body = sql[start : i - 1]
        columns: list[str] = []
        for line in body.splitlines():
            cm = re.match(r"\s*(?:\[([^\]]+)\]|([A-Za-z_]\w*))\s+", line)
            if not cm:
                continue
            col = cm.group(1) or cm.group(2)
            if col.upper() in {"CONSTRAINT", "PRIMARY", "UNIQUE", "FOREIGN", "CHECK"}:
                continue
            columns.append(col)
        result[table] = columns
    return result


def escape(value: str) -> str:
    return value.replace("'", "''")


def sanitize_sql_text(sql: str) -> str:
    """Remove replacement chars and fix known broken identifiers."""
    sql = sql.replace("\ufffd", "")
    sql = sql.replace("Hakkmizda", "Hakkimizda")
    sql = sql.replace("KaytYeri", "KayitYeri")
    # Never COLLATE varbinary/image leftovers
    sql = re.sub(
        r"(varbinary\s*(?:\([^)]+\))?)\s+COLLATE\s+\w+",
        r"\1",
        sql,
        flags=re.I,
    )
    sql = re.sub(r"(image)\s+COLLATE\s+\w+", r"\1", sql, flags=re.I)
    return sql


def select_ai_ddl(ai_sql: str, final_tables: set[str]) -> list[str]:
    selected: list[str] = []
    for batch in split_batches(ai_sql):
        create_table = table_after(r"CREATE\s+TABLE", batch)
        index_table = table_after(
            r"CREATE\s+(?:UNIQUE\s+)?(?:NONCLUSTERED\s+|CLUSTERED\s+)?INDEX\s+.*?\s+ON", batch
        )
        alter_table = table_after(r"ALTER\s+TABLE", batch)
        if create_table in AI_TABLES or index_table in AI_TABLES:
            selected.append(batch)
            continue
        if alter_table in AI_TABLES:
            ref = table_after(r"REFERENCES", batch)
            if ref is None or ref in final_tables:
                selected.append(batch)
    return selected


def select_ai_descriptions(ai_sql: str) -> list[str]:
    calls: list[str] = []
    call_re = re.compile(
        r"EXEC\s+dbo\._SetMsDescription\s+N?'([^']+)'\s*,\s*N?'([^']+)'\s*,\s*(NULL|N?'([^']+)')\s*,\s*N'((?:''|[^'])*)'\s*;",
        re.I,
    )
    for m in call_re.finditer(ai_sql):
        schema, table = m.group(1), m.group(2)
        full_name = f"{schema}.{table}"
        if full_name not in AI_TABLES:
            continue
        column = None if m.group(3).upper() == "NULL" else m.group(4)
        value = m.group(5).replace("''", "'")
        if column is None:
            value = "AI platform tablosu. " + value
            if full_name == "common.LabelDef":
                value += " Eski tablo: EtiketTanim."
            elif full_name == "common.LabelPool":
                value += " Eski tablo: EtiketHavuzu."
        column_sql = "NULL" if column is None else f"N'{escape(column)}'"
        calls.append(
            f"EXEC dbo._SetFullSchemaDescription N'{schema}', N'{table}', {column_sql}, N'{escape(value)}';"
        )
    return calls


def convert_product_descriptions(product: str) -> str:
    """Replace sp_addextendedproperty MS_Description with upsert helper calls."""
    prop_re = re.compile(
        r"EXEC\s+sp_addextendedproperty\s+'MS_Description',\s*N'((?:''|[^'])*)',\s*"
        r"N'schema',\s*N'([^']+)',\s*N'table',\s*N'([^']+)'"
        r"(?:\s*,\s*N'column',\s*N'([^']+)')?",
        re.I,
    )

    def repl(m: re.Match[str]) -> str:
        value, schema, table, column = m.group(1), m.group(2), m.group(3), m.group(4)
        column_sql = "NULL" if not column else f"N'{escape(column)}'"
        return (
            f"EXEC dbo._SetFullSchemaDescription N'{schema}', N'{table}', {column_sql}, N'{value}'"
        )

    return prop_re.sub(repl, product)


def strip_product_header(product: str) -> str:
    """Remove CREATE DATABASE / USE / SET from product; keep from first schema/table."""
    m = re.search(r"(?im)^--\s*\n--\s*Definition for schema", product)
    if m:
        return product[m.start() :]
    m = re.search(r"(?im)^CREATE\s+SCHEMA\b", product)
    if m:
        return product[m.start() :]
    return product


HEADER = f"""-- SQL Manager Lite style export
-- ---------------------------------------
-- Project   : EnKolayCari2026 (Unified DB) — CLEAN INSTALL
-- Database  : {DB_NAME}
-- Source    : TICARI_MASTER + TICARI_SLAVE1 + AIProjeMimari platform
-- Generated : scripts/build_full_ddl.py
-- Encoding  : UTF-8 with BOM (open in SSMS as UTF-8)
-- WARNING   : Drops and recreates database {DB_NAME}
-- ---------------------------------------

USE [master]
GO

IF DB_ID(N'{DB_NAME}') IS NOT NULL
BEGIN
  ALTER DATABASE [{DB_NAME}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
  DROP DATABASE [{DB_NAME}];
END
GO

CREATE DATABASE [{DB_NAME}]
  COLLATE Turkish_CI_AI
GO

USE [{DB_NAME}]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE dbo._SetFullSchemaDescription
    @Schema sysname,
    @Table sysname,
    @Column sysname = NULL,
    @Value nvarchar(4000)
AS
BEGIN
    SET NOCOUNT ON;
    IF @Column IS NULL
    BEGIN
        IF EXISTS (
            SELECT 1 FROM sys.extended_properties ep
            JOIN sys.tables t ON t.object_id = ep.major_id
            JOIN sys.schemas s ON s.schema_id = t.schema_id
            WHERE ep.name = N'MS_Description' AND ep.minor_id = 0
              AND s.name = @Schema AND t.name = @Table)
            EXEC sys.sp_updateextendedproperty N'MS_Description', @Value,
                 N'SCHEMA', @Schema, N'TABLE', @Table;
        ELSE
            EXEC sys.sp_addextendedproperty N'MS_Description', @Value,
                 N'SCHEMA', @Schema, N'TABLE', @Table;
    END
    ELSE
    BEGIN
        IF EXISTS (
            SELECT 1 FROM sys.extended_properties ep
            JOIN sys.tables t ON t.object_id = ep.major_id
            JOIN sys.schemas s ON s.schema_id = t.schema_id
            JOIN sys.columns c ON c.object_id = t.object_id AND c.column_id = ep.minor_id
            WHERE ep.name = N'MS_Description'
              AND s.name = @Schema AND t.name = @Table AND c.name = @Column)
            EXEC sys.sp_updateextendedproperty N'MS_Description', @Value,
                 N'SCHEMA', @Schema, N'TABLE', @Table, N'COLUMN', @Column;
        ELSE
            EXEC sys.sp_addextendedproperty N'MS_Description', @Value,
                 N'SCHEMA', @Schema, N'TABLE', @Table, N'COLUMN', @Column;
    END
END
GO
"""


def verify_foreign_keys(sql: str, tables: dict[str, list[str]]) -> None:
    for batch in split_batches(sql):
        source = table_after(r"ALTER\s+TABLE", batch)
        target = table_after(r"REFERENCES", batch)
        if source and target and (source not in tables or target not in tables):
            raise RuntimeError(f"Broken FK batch: {source} -> {target}")


def build() -> str:
    ai = AI_SOURCE.read_text(encoding="utf-8-sig").replace("BrandDb", DB_NAME)
    product = PRODUCT_SOURCE.read_text(encoding="utf-8-sig")
    product = sanitize_sql_text(product)
    product = convert_product_descriptions(product)
    product = strip_product_header(product)

    product_tables = extract_create_tables(product)
    overlap = AI_TABLES.intersection(product_tables)
    if overlap:
        raise RuntimeError(f"AI-only selection overlaps product tables: {sorted(overlap)}")

    final_table_names = set(product_tables) | AI_TABLES
    ai_batches = select_ai_ddl(ai, final_table_names)
    ai_ddl = "\nGO\n\n".join(ai_batches)
    ai_desc_calls = select_ai_descriptions(ai)

    body = product.rstrip() + "\n\nGO\n\n" + "\n".join([
        "-- ============================================================================",
        "-- AI platform tables (missing from product/legacy DDL)",
        "-- ============================================================================",
        "",
        ai_ddl,
        "GO",
        "",
        "-- AI platform MS_Description",
        "\n".join(ai_desc_calls),
        "GO",
        "",
        "DROP PROCEDURE IF EXISTS dbo._SetFullSchemaDescription;",
        "GO",
        "",
        f"PRINT N'{DB_NAME} full schema complete (clean install).';",
        "GO",
    ])

    result = HEADER + "\n" + body
    result = sanitize_sql_text(result)

    tables = extract_create_tables(result)
    missing = REQUIRED_TABLES - set(tables)
    if missing:
        raise RuntimeError(f"Missing required tables: {sorted(missing)}")

    create_names = [
        re.sub(r"[\[\]\s]", "", name)
        for name in re.findall(
            r"CREATE\s+TABLE\s+((?:\[[^\]]+\]|\w+)\s*\.\s*(?:\[[^\]]+\]|\w+))", result, re.I
        )
    ]
    duplicates = sorted({n for n in create_names if create_names.count(n) > 1})
    if duplicates:
        raise RuntimeError(f"Duplicate CREATE TABLE: {duplicates}")

    verify_foreign_keys(result, tables)
    if "\ufffd" in result:
        raise RuntimeError("Replacement character U+FFFD still present in output")
    return result


def main() -> None:
    sql = build()
    OUTPUT.parent.mkdir(parents=True, exist_ok=True)
    OUTPUT.write_text(sql, encoding="utf-8-sig", newline="\n")
    create_names = [
        re.sub(r"[\[\]\s]", "", name)
        for name in re.findall(
            r"CREATE\s+TABLE\s+((?:\[[^\]]+\]|\w+)\s*\.\s*(?:\[[^\]]+\]|\w+))", sql, re.I
        )
    ]
    print(f"Output: {OUTPUT}")
    print(f"Size: {OUTPUT.stat().st_size} bytes, lines: {len(sql.splitlines())}")
    print(f"Tables: {len(create_names)}")
    for name in create_names:
        print(f"  {name}")


if __name__ == "__main__":
    main()
