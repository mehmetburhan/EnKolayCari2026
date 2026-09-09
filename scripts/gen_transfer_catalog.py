"""
gen_transfer_catalog.py
-----------------------
Domain entity'lerden transfer kataloğu üretir.
Aktarım sırası FK bağımlılık grafiğinden topolojik sıralama ile otomatik hesaplanır.
Manuel order_priority sadece eşit seviyedeki tablolar arasında sıralama için kullanılır.
"""
import re
import json
from collections import defaultdict, deque
from pathlib import Path

root = Path(r"C:\Dev\GitHub\EnKolayCari\EnKolayCari2026\EnKolayCari2026.Domain\Model")

prop_line = re.compile(
    r"public\s+(?!virtual)([\w\?<>,\s]+?)\s+(\w+)\s*\{\s*get;\s*set;\s*\}.*\|\s*Eski alan:\s*(\w+)\.(\w+)"
)

# Legacy FK kolonu → legacy parent tablo adı
FK_LEGACY_TO_TABLE = {
    "CariTanimId":      "CariTanim",
    "UrunTanimId":      "UrunTanim",
    "FaturaId":         "Fatura",
    "FaturaHareketId":  "FaturaHareket",
    "DepoTanimId":      "DepoTanim",
    "DepoTanim1Id":     "DepoTanim",
    "DepoId":           "DepoTanim",
    "KasaTanimId":      "KasaTanim",
    "KasaId":           "KasaTanim",
    "MarkaTanimId":     "MarkaTanim",
    "BirimId":          "BirimTanim",
    "BirimTanimId":     "BirimTanim",
    "PersonelTanimId":  "PersonelTanim",
    "PersonelId":       "PersonelTanim",
    "CekSenetTanimId":  "CekSenetTanim",
    "DovizTanimId":     "DovizTanim",
    "SayimTanimId":     "SayimTanim",
    "SayacId":          "Sayac",
    "RenkId":           "UrunRenkPaleti",
    "LisansTipiId":     "LisansTipi",
    "BagliFaturaId":    "Fatura",
}

# Katalogda YER ALMAYACAK tablolar (şirket transferi dışı)
SKIP_LEGACY = {"MerkezDovizKur"}

# PersonelTanim kataloğa eklenmez (özel kod); ama FK grafiğinde referans olarak bilinmeli
VIRTUAL_PARENTS = {"PersonelTanim", "SirketTanim"}

# -------------------------------------------------------------------
# 1. Domain model dosyalarından tablo / kolon bilgisi topla
# -------------------------------------------------------------------
catalog = []
known_tables = set()

for cs in sorted(root.rglob("*.cs")):
    if "HangFire" in str(cs):
        continue
    text = cs.read_text(encoding="utf-8")
    m = re.search(r"public class (\w+)\s+//(.+)", text)
    if not m:
        continue
    comment = m.group(2)
    tm = re.search(
        r"Eski tablo:\s*(\w+)\s*\(([^)]+)\)\.\s*Yeni şema:\s*([\w.]+)", comment
    )
    if not tm:
        continue
    legacy_table = tm.group(1)
    if legacy_table in SKIP_LEGACY:
        continue

    source_db = tm.group(2).strip()
    new_full = tm.group(3).strip()
    schema, new_table = new_full.split(".", 1) if "." in new_full else ("dbo", new_full)

    columns = []
    fks = []
    for line in text.splitlines():
        if "virtual" in line:
            continue
        pm = prop_line.search(line)
        if not pm:
            continue
        prop, lcol = pm.group(2), pm.group(4)
        if prop == "Id":
            columns.append({"new": prop, "legacy": lcol, "role": "legacyId"})
            continue
        if prop == "GId":
            columns.append({"new": prop, "legacy": lcol, "role": "gid"})
            continue
        if lcol == "SirketTanimId" or prop == "CompanyId":
            columns.append({"new": "CompanyId", "legacy": lcol, "role": "company"})
            continue
        role = "data"
        fk_src = None
        if lcol in FK_LEGACY_TO_TABLE:
            role = "fk"
            fk_src = FK_LEGACY_TO_TABLE[lcol]
            fks.append({"newCol": prop, "legacyCol": lcol, "legacyTable": fk_src})
        columns.append({"new": prop, "legacy": lcol, "role": role, "fkTable": fk_src})

    has_company = any(c["role"] == "company" for c in columns)
    source = "MASTER" if "MASTER" in source_db.upper() else "SLAVE"

    catalog.append({
        "legacyTable": legacy_table,
        "sourceDb": source,
        "newSchema": schema,
        "newTable": new_table,
        "entity": m.group(1),
        "companyFilter": has_company,
        "columns": columns,
        "fks": fks,
        "order": 0,  # topolojik sıralamadan gelecek
    })
    known_tables.add(legacy_table)

# -------------------------------------------------------------------
# 2. FK bağımlılık grafiğinden topolojik sıralama (Kahn algoritması)
# -------------------------------------------------------------------
#  - Self-referans (tablo kendi kendine FK) → döngü değil, ignore
#  - Katalogda olmayan parent → VIRTUAL_PARENTS veya dış tablo → ignore (idMap boş kalır)

catalog_set = {t["legacyTable"] for t in catalog} | VIRTUAL_PARENTS

# Her tablo için gerçek (katalogda olan) bağımlıları bul
deps: dict[str, set[str]] = defaultdict(set)
for t in catalog:
    for fk in t["fks"]:
        parent = fk["legacyTable"]
        child  = t["legacyTable"]
        if parent == child:
            continue          # self-reference → ignore
        if parent not in catalog_set:
            continue          # bilinmeyen parent → ignore
        if parent in VIRTUAL_PARENTS:
            continue          # PersonelTanim vs → özel kod, kataloğun dışında
        deps[child].add(parent)

# Kahn BFS topolojik sıralama → seviye (level) ata
in_degree: dict[str, int] = defaultdict(int)
children_of: dict[str, list[str]] = defaultdict(list)

all_nodes = {t["legacyTable"] for t in catalog}

for node in all_nodes:
    in_degree.setdefault(node, 0)

for child, parents in deps.items():
    for parent in parents:
        in_degree[child] += 1
        children_of[parent].append(child)

level: dict[str, int] = {}
queue: deque[str] = deque()

for node in all_nodes:
    if in_degree[node] == 0:
        queue.append(node)
        level[node] = 1

while queue:
    node = queue.popleft()
    for child in children_of[node]:
        in_degree[child] -= 1
        level[child] = max(level.get(child, 0), level[node] + 1)
        if in_degree[child] == 0:
            queue.append(child)

# Döngü kontrolü
remaining = [n for n in all_nodes if n not in level]
if remaining:
    print(f"UYARI: FK döngüsü veya çözümsüz bağımlılık tespit edildi: {remaining}")
    for n in remaining:
        level[n] = 999

# -------------------------------------------------------------------
# 3. Topolojik seviye × 10 = order (ince ayar için boşluk bırakır)
# -------------------------------------------------------------------
for t in catalog:
    topo = level.get(t["legacyTable"], 1)
    t["order"] = topo * 10

catalog.sort(key=lambda x: (x["order"], x["legacyTable"]))

# -------------------------------------------------------------------
# 4. Katalog dosyasına yaz
# -------------------------------------------------------------------
out = Path(
    r"C:\Dev\GitHub\EnKolayCari\EnKolayCari2026\DesktopApp\EnKolayCari2026.DataBridge\Catalog\transfer-catalog.json"
)
out.parent.mkdir(parents=True, exist_ok=True)
out.write_text(json.dumps({"tables": catalog}, ensure_ascii=False, indent=2), encoding="utf-8")

print(f"\ntables: {len(catalog)}")
print(f"\n{'ORDER':>6}  {'DB':6}  {'LEGACY TABLE':<26}  {'NEW TABLE':<36}  DEPENDS ON")
print("-" * 120)
for t in catalog:
    parent_names = sorted({fk["legacyTable"] for fk in t["fks"] if fk["legacyTable"] != t["legacyTable"]})
    print(f"{t['order']:>6}  {t['sourceDb']:6}  {t['legacyTable']:<26}  {t['newSchema']+'.'+t['newTable']:<36}  {', '.join(parent_names) or '-'}")

# Bağımlılık ihlali kontrolü: herhangi bir tablo parent'ından önce mi geliyor?
print("\n=== BAĞIMLILIK KONTROL ===")
order_map = {t["legacyTable"]: t["order"] for t in catalog}
ok = True
for t in catalog:
    for fk in t["fks"]:
        parent = fk["legacyTable"]
        if parent == t["legacyTable"]:
            continue
        if parent not in order_map:
            continue
        if order_map[parent] >= t["order"]:
            print(f"  SORUN: {t['legacyTable']} (order={t['order']}) -> {parent} (order={order_map[parent]}) [parent sonra geliyor!]")
            ok = False
if ok:
    print("  Tüm FK bağımlılıkları doğru sırada. ✓")
