-- Incremental: common.LegacyTransferMap (EKCN2026)
-- Safe to run on existing DB (no DROP DATABASE)
-- Encoding: UTF-8

USE [EKCN2026]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID(N'common.LegacyTransferMap', N'U') IS NULL
BEGIN
  CREATE TABLE common.LegacyTransferMap (
    Id bigint IDENTITY(1, 1) NOT NULL,
    GId uniqueidentifier DEFAULT newid() NOT NULL,
    CompanyId bigint NOT NULL,
    LegacyTableName nvarchar(128) COLLATE Turkish_CI_AI NOT NULL,
    LegacyGId uniqueidentifier NOT NULL,
    LegacyLastChangeDate datetime NULL,
    NewTableName nvarchar(128) COLLATE Turkish_CI_AI NOT NULL,
    NewGId uniqueidentifier NOT NULL,
    InsertDateTime datetime DEFAULT getdate() NOT NULL,
    CONSTRAINT LegacyTransferMap_pk PRIMARY KEY CLUSTERED (Id)
      WITH (
        PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
        ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
  )
  ON [PRIMARY];
END
GO

IF NOT EXISTS (
  SELECT 1 FROM sys.indexes
  WHERE name = N'UQ_LegacyTransferMap_LegacyTable_LegacyGId'
    AND object_id = OBJECT_ID(N'common.LegacyTransferMap')
)
BEGIN
  CREATE UNIQUE NONCLUSTERED INDEX UQ_LegacyTransferMap_LegacyTable_LegacyGId
    ON common.LegacyTransferMap (LegacyTableName, LegacyGId);
END
GO

IF NOT EXISTS (
  SELECT 1 FROM sys.indexes
  WHERE name = N'IX_LegacyTransferMap_CompanyId_NewTable_NewGId'
    AND object_id = OBJECT_ID(N'common.LegacyTransferMap')
)
BEGIN
  CREATE NONCLUSTERED INDEX IX_LegacyTransferMap_CompanyId_NewTable_NewGId
    ON common.LegacyTransferMap (CompanyId, NewTableName, NewGId);
END
GO

IF NOT EXISTS (
  SELECT 1 FROM sys.indexes
  WHERE name = N'IX_LegacyTransferMap_LegacyLastChangeDate'
    AND object_id = OBJECT_ID(N'common.LegacyTransferMap')
)
BEGIN
  CREATE NONCLUSTERED INDEX IX_LegacyTransferMap_LegacyLastChangeDate
    ON common.LegacyTransferMap (LegacyLastChangeDate);
END
GO

CREATE OR ALTER PROCEDURE dbo._SetMsDescriptionPatch
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

EXEC dbo._SetMsDescriptionPatch N'common', N'LegacyTransferMap', NULL,
  N'Transfer esleme tablosu. Eski tablo/GId ile yeni tablo/GId/CompanyId arasini tutar. Veri aktarimi sirasinda kullanilir.';
GO
EXEC dbo._SetMsDescriptionPatch N'common', N'LegacyTransferMap', N'Id',
  N'Birincil anahtar (Identity)';
GO
EXEC dbo._SetMsDescriptionPatch N'common', N'LegacyTransferMap', N'GId',
  N'Kuresel benzersiz kimlik';
GO
EXEC dbo._SetMsDescriptionPatch N'common', N'LegacyTransferMap', N'CompanyId',
  N'Yeni kaydin CompanyId degeri (tenant)';
GO
EXEC dbo._SetMsDescriptionPatch N'common', N'LegacyTransferMap', N'LegacyTableName',
  N'Eski tablo adi (ornek: CariTanim, UrunTanim)';
GO
EXEC dbo._SetMsDescriptionPatch N'common', N'LegacyTransferMap', N'LegacyGId',
  N'Eski kayit GId';
GO
EXEC dbo._SetMsDescriptionPatch N'common', N'LegacyTransferMap', N'LegacyLastChangeDate',
  N'Eski kayittaki Insert/Update/Delete tarihlerinden en guncel olan';
GO
EXEC dbo._SetMsDescriptionPatch N'common', N'LegacyTransferMap', N'NewTableName',
  N'Yeni tablo adi (ornek: finance.Account veya Account)';
GO
EXEC dbo._SetMsDescriptionPatch N'common', N'LegacyTransferMap', N'NewGId',
  N'Yeni kayit GId';
GO
EXEC dbo._SetMsDescriptionPatch N'common', N'LegacyTransferMap', N'InsertDateTime',
  N'Esleme kaydinin olusturulma zamani';
GO

DROP PROCEDURE IF EXISTS dbo._SetMsDescriptionPatch;
GO

PRINT N'common.LegacyTransferMap ready (with column MS_Description).';
GO
