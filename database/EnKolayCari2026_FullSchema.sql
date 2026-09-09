-- SQL Manager Lite style export
-- ---------------------------------------
-- Project   : EnKolayCari2026 (Unified DB) — CLEAN INSTALL
-- Database  : EKCN2026
-- Source    : TICARI_MASTER + TICARI_SLAVE1 + AIProjeMimari platform
-- Generated : scripts/build_full_ddl.py
-- Encoding  : UTF-8 with BOM (open in SSMS as UTF-8)
-- WARNING   : Drops and recreates database EKCN2026
-- ---------------------------------------

USE [master]
GO

IF DB_ID(N'EKCN2026') IS NOT NULL
BEGIN
  ALTER DATABASE [EKCN2026] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
  DROP DATABASE [EKCN2026];
END
GO

CREATE DATABASE [EKCN2026]
  COLLATE Turkish_CI_AI
GO

USE [EKCN2026]
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

--
-- Definition for schema common :
--

CREATE SCHEMA common
  AUTHORIZATION [dbo]
GO

--
-- Definition for schema HangFire :
--

CREATE SCHEMA [HangFire]
  AUTHORIZATION [dbo]
GO

--
-- Definition for schema finance :
--

CREATE SCHEMA finance
  AUTHORIZATION [dbo]
GO

--
-- Definition for schema inventory :
--

CREATE SCHEMA inventory
  AUTHORIZATION [dbo]
GO

--
-- Definition for schema trade :
--

CREATE SCHEMA trade
  AUTHORIZATION [dbo]
GO

--
-- Definition for schema report :
--

CREATE SCHEMA report
  AUTHORIZATION [dbo]
GO


--
-- Definition for table AspNetRoles (Identity)
--

CREATE TABLE dbo.AspNetRoles (
  Id nvarchar(450) COLLATE Turkish_CI_AI DEFAULT newid() NOT NULL,
  Name nvarchar(256) COLLATE Turkish_CI_AI NULL,
  NormalizedName nvarchar(256) COLLATE Turkish_CI_AI NULL,
  ConcurrencyStamp nvarchar(max) COLLATE Turkish_CI_AI NULL,
  Discriminator nvarchar(128) COLLATE Turkish_CI_AI DEFAULT 'AspNetRole' NOT NULL,
  CONSTRAINT PK_AspNetRoles PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetRoles', NULL, N'ASP.NET Identity roller. Eski: MASTER.dbo.Roles + SLAVE.dbo.UserRoles hedefi.'
GO

CREATE TABLE dbo.AspNetUsers (
  Id nvarchar(450) COLLATE Turkish_CI_AI NOT NULL,
  FullName nvarchar(60) COLLATE Turkish_CI_AI NOT NULL,
  UserName nvarchar(256) COLLATE Turkish_CI_AI NULL,
  NormalizedUserName nvarchar(256) COLLATE Turkish_CI_AI NULL,
  Email nvarchar(256) COLLATE Turkish_CI_AI NULL,
  NormalizedEmail nvarchar(256) COLLATE Turkish_CI_AI NULL,
  EmailConfirmed bit NOT NULL,
  PasswordHash nvarchar(max) COLLATE Turkish_CI_AI NULL,
  SecurityStamp nvarchar(max) COLLATE Turkish_CI_AI NULL,
  ConcurrencyStamp nvarchar(max) COLLATE Turkish_CI_AI NULL,
  PhoneNumber nvarchar(max) COLLATE Turkish_CI_AI NULL,
  PhoneNumberConfirmed bit NOT NULL,
  TwoFactorEnabled bit NOT NULL,
  LockoutEnd datetimeoffset(0) NULL,
  LockoutEnabled bit NOT NULL,
  AccessFailedCount int NOT NULL,
  Discriminator nvarchar(128) COLLATE Turkish_CI_AI DEFAULT 'AspNetUsers' NOT NULL,
  CompanyId bigint DEFAULT 0 NOT NULL,
  CONSTRAINT PK_AspNetUsers PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetUsers', NULL, N'ASP.NET Identity kullanicilar. Eski: PersonelTanim.UserId eslemesi.'
GO

EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetUsers', N'CompanyId', N'Sirket kapsamli tenant. Eski: SirketTanimId -> CompanyId | Eski alan: PersonelTanim.SirketTanimId'
GO

CREATE TABLE dbo.AspNetUserRoles (
  UserId nvarchar(450) COLLATE Turkish_CI_AI NOT NULL,
  RoleId nvarchar(450) COLLATE Turkish_CI_AI NOT NULL,
  Discriminator nvarchar(128) COLLATE Turkish_CI_AI DEFAULT 'AspNetUserRoles' NOT NULL,
  CompanyId bigint NOT NULL,
  CONSTRAINT PK_AspNetUserRoles PRIMARY KEY CLUSTERED (UserId, RoleId, CompanyId)
)
ON [PRIMARY]
GO

CREATE TABLE dbo.MenuRole (
  Id bigint IDENTITY(1, 1) NOT NULL,
  CompanyId bigint NOT NULL,
  MenuKey nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  RoleId nvarchar(450) COLLATE Turkish_CI_AI NOT NULL,
  CreatedDate datetime DEFAULT getdate() NOT NULL,
  CreatedUser bigint DEFAULT 0 NOT NULL,
  CONSTRAINT PK_MenuRole PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'dbo', N'MenuRole', NULL, N'Sirket kapsamli menu-rol eslemesi. Eski: SayfaTanim + Roles.'
GO



--
-- Definition for table Moduls :
-- Source: AIProjeMimari common.Moduls (Id is NOT IDENTITY; replaces legacy Modul)
--

CREATE TABLE common.Moduls (
  Id bigint NOT NULL,
  ModulName nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  CONSTRAINT Moduls_pk PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Moduls', NULL, N'Modul katalogu (AI mimari). Eski tablo: Modul (TICARI_MASTER). Id IDENTITY degildir.'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Moduls', N'Id', N'Modul Id (manuel seed). Eski alan: Modul.Id'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Moduls', N'ModulName', N'Modul adi. Eski alan: Modul (ad/aciklama alanlari)'
GO

--
-- Definition for table City :
-- Source: AIProjeMimari / Auditness common.City (replaces TICARI_MASTER.dbo.Iller + Ilceler)
-- Level: 1=Province (Il), 2=District (Ilce)
--

CREATE TABLE common.City (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier NOT NULL,
  Stat bit DEFAULT 1 NOT NULL,
  FullCityCode nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  CountryCode nvarchar(10) COLLATE Turkish_CI_AI NOT NULL,
  CityCode nvarchar(30) COLLATE Turkish_CI_AI NULL,
  ParentCityCode nvarchar(30) COLLATE Turkish_CI_AI NULL,
  CityName nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  Level int DEFAULT 1 NOT NULL,
  CONSTRAINT City_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'common', N'City', NULL, N'Sehir/ilce hiyerarsisi (AI mimari). Level 1=Il, Level 2=Ilce. Eski: Iller + Ilceler.'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'City', N'Id', N'Birincil anahtar (Identity)'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'City', N'GId', N'Kuresel benzersiz kimlik (Guid)'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'City', N'Stat', N'Aktif/pasif (1=aktif)'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'City', N'FullCityCode', N'Bilesik sehir kodu (ulke + il/ilce). Unique anahtar.'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'City', N'CountryCode', N'Ulke kodu (ornek: TR)'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'City', N'CityCode', N'Il veya ilce kodu'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'City', N'ParentCityCode', N'Ust il CityCode (Level=2 iken). Level=1 icin null.'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'City', N'CityName', N'Il veya ilce adi. Eski: Iller.IlAdi / Ilceler.IlceAdi'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'City', N'Level', N'Seviye: 1=Il, 2=Ilce. Eski Iller+Ilceler hiyerarsisi.'
GO

CREATE UNIQUE NONCLUSTERED INDEX City_uq ON common.City
  (FullCityCode)
WITH (
  PAD_INDEX = OFF,
  IGNORE_DUP_KEY = OFF,
  DROP_EXISTING = OFF,
  STATISTICS_NORECOMPUTE = OFF,
  SORT_IN_TEMPDB = OFF,
  ONLINE = OFF,
  ALLOW_ROW_LOCKS = ON,
  ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX IX_City_FullCityCode_Level ON common.City
  (FullCityCode, Level)
INCLUDE (CityName, CountryCode, ParentCityCode)
WITH (
  PAD_INDEX = OFF,
  FILLFACTOR = 90,
  DROP_EXISTING = OFF,
  STATISTICS_NORECOMPUTE = OFF,
  SORT_IN_TEMPDB = OFF,
  ONLINE = OFF,
  ALLOW_ROW_LOCKS = ON,
  ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY]
GO

--
-- Definition for table FileHeader :
-- Source: AIProjeMimari / Auditness common.FileHeader (replaces TICARI_SLAVE1.dbo.Dosyalar)
-- Polymorphic attachment: TableName + TableId
--

CREATE TABLE common.FileHeader (
  Id bigint IDENTITY(1, 1) NOT NULL,
  TableName nvarchar(50) COLLATE Turkish_CI_AI NULL,
  TableId bigint NULL,
  FileName nvarchar(500) COLLATE Turkish_CI_AI NULL,
  FileOrder int NOT NULL,
  FileType nvarchar(max) COLLATE Turkish_CI_AI NULL,
  CONSTRAINT FileHeader_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'common', N'FileHeader', NULL, N'Dosya ust bilgisi (AI mimari). TableName + TableId ile baglanir. Eski: Dosyalar.'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'FileHeader', N'Id', N'Birincil anahtar (Identity)'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'FileHeader', N'TableName', N'Sahip tablo adi (ornek: Product, Account, TradeDocument). Eski: Dosyalar.TabloAdi'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'FileHeader', N'TableId', N'Sahip kayit Id. Eski: Dosyalar.TabloId'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'FileHeader', N'FileName', N'Dosya adi. Eski: Dosyalar.DosyaAdi'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'FileHeader', N'FileOrder', N'Goruntuleme sirasi. Eski: Dosyalar.Sira'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'FileHeader', N'FileType', N'Dosya tipi (png / jpg / pdf vb.). Eski: Dosyalar.DosyaUzantisi'
GO

CREATE NONCLUSTERED INDEX IX_FileHeader_TableName_TableId ON common.FileHeader
  (TableName, TableId)
INCLUDE (FileName, FileOrder, FileType)
WITH (
  PAD_INDEX = OFF,
  FILLFACTOR = 90,
  DROP_EXISTING = OFF,
  STATISTICS_NORECOMPUTE = OFF,
  SORT_IN_TEMPDB = OFF,
  ONLINE = OFF,
  ALLOW_ROW_LOCKS = ON,
  ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY]
GO

--
-- Definition for table FileBlob :
-- Source: AIProjeMimari / Auditness common.FileBlob (replaces TICARI_SLAVE1.dbo.DosyalarBlob)
--

CREATE TABLE common.FileBlob (
  Id bigint IDENTITY(1, 1) NOT NULL,
  FileHeaderId bigint NOT NULL,
  Blob image NOT NULL,
  CONSTRAINT FileBlob_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
TEXTIMAGE_ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'common', N'FileBlob', NULL, N'Dosya icerigi (binary). FK -> FileHeader. Eski: DosyalarBlob.'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'FileBlob', N'Id', N'Birincil anahtar (Identity)'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'FileBlob', N'FileHeaderId', N'FK: common.FileHeader.Id'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'FileBlob', N'Blob', N'Binary dosya icerigi. Eski: DosyalarBlob.Blob'
GO

ALTER TABLE common.FileBlob
ADD CONSTRAINT FileBlob_fk FOREIGN KEY (FileHeaderId)
  REFERENCES common.FileHeader (Id)
  ON UPDATE NO ACTION
  ON DELETE CASCADE
GO

--
-- Definition for table Personal :
-- Source: AIProjeMimari / Auditness common.Personal (replaces TICARI_MASTER.dbo.PersonelTanim)
-- Audit Family A (Created*/Modifed*)
--

CREATE TABLE common.Personal (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyMasterCode nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  Stat bit DEFAULT 1 NOT NULL,
  Name nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  SurName nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  UseEMailForLogin bit DEFAULT 0 NOT NULL,
  Email nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  DomainUserId nvarchar(100) COLLATE Turkish_CI_AI NULL,
  IsWebEnabled bit DEFAULT 0 NOT NULL,
  IsBranchManager bit DEFAULT 0 NOT NULL,
  ManagerAccountCode nvarchar(50) COLLATE Turkish_CI_AI NULL,
  CostCenter nvarchar(100) COLLATE Turkish_CI_AI NULL,
  PhoneNumber nvarchar(100) COLLATE Turkish_CI_AI NULL,
  BranchId bigint NULL,
  ParentPersonalId bigint NULL,
  IdentityNumber nvarchar(30) COLLATE Turkish_CI_AI NULL,
  ManagerIdentityNumber nvarchar(30) COLLATE Turkish_CI_AI NULL,
  StartWorkDate datetime NULL,
  EndWorkDate datetime NULL,
  AppCode nvarchar(30) COLLATE Turkish_CI_AI NULL,
  AppCodeId bigint NULL,
  CountryCode nvarchar(30) COLLATE Turkish_CI_AI NULL,
  UserId nvarchar(450) COLLATE Turkish_CI_AI NULL,
  UserIdForDomain nvarchar(450) COLLATE Turkish_CI_AI NULL,
  DeviceId nvarchar(500) COLLATE Turkish_CI_AI NULL,
  CustomClientOs nvarchar(100) COLLATE Turkish_CI_AI NULL,
  CustomClientAppVersion nvarchar(100) COLLATE Turkish_CI_AI NULL,
  Color nvarchar(200) COLLATE Turkish_CI_AI NULL,
  ResetPassCode nvarchar(30) COLLATE Turkish_CI_AI NULL,
  ResetPassExpireDate datetime NULL,
  EmplId nvarchar(20) COLLATE Turkish_CI_AI NULL,
  Division nvarchar(10) COLLATE Turkish_CI_AI NULL,
  PositionDesc nvarchar(255) COLLATE Turkish_CI_AI NULL,
  CreatedDate datetime DEFAULT getdate() NOT NULL,
  CreatedUser bigint DEFAULT 0 NOT NULL,
  ModifedDate datetime NULL,
  ModifedUser bigint NULL,
  DeletedDate datetime NULL,
  DeletedUser bigint NULL,
  IsDelete bigint DEFAULT 0 NOT NULL,
  CONSTRAINT Personal_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Personal', NULL, N'Personel/kullanici karti (AI mimari). Eski: PersonelTanim. Tenant: CompanyMasterCode.'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Personal', N'Id', N'Birincil anahtar (Identity). Eski: PersonelTanim.Id'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Personal', N'GId', N'Kuresel benzersiz kimlik. Eski: PersonelTanim.GId'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Personal', N'CompanyMasterCode', N'Sirket master kodu (tenant). Eski: PersonelTanim.SirketTanimId -> Company.CompanyMasterCode'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Personal', N'Stat', N'Aktif/pasif. Eski: PersonelTanim.Aktif'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Personal', N'Name', N'Ad. Eski: PersonelTanim.AdSoyad (bolunmus)'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Personal', N'SurName', N'Soyad. Eski: PersonelTanim.AdSoyad (bolunmus)'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Personal', N'UseEMailForLogin', N'Domain kullanicisi olmayanlar icin e-posta ile giris. | Eski alan: PersonelTanim e-posta giris modu'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Personal', N'Email', N'Giris e-postasi. Eski: PersonelTanim.Email'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Personal', N'DomainUserId', N'Domain kullanici Id. Eski: PersonelTanim (dogrudan alan yok)'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Personal', N'PhoneNumber', N'Telefon. Eski: PersonelTanim.Telefon'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Personal', N'UserId', N'AspNetUsers.Id (e-posta girisi). Eski: PersonelTanim Identity baglantisi'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Personal', N'UserIdForDomain', N'AspNetUsers.Id (domain girisi).'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Personal', N'Color', N'Takvim/UI rengi. Eski: PersonelTanim.Renk'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Personal', N'ResetPassExpireDate', N'Sifre sifirlama kodu son kullanma. Eski: PersonelTanim.PasswordResetCodeExpirationMinutes'
GO

CREATE NONCLUSTERED INDEX IX_Personal_CompanyMasterCode_IsDelete ON common.Personal
  (CompanyMasterCode, IsDelete)
INCLUDE (BranchId, DomainUserId, Email, Id, Name, SurName)
WITH (
  PAD_INDEX = OFF,
  FILLFACTOR = 90,
  DROP_EXISTING = OFF,
  STATISTICS_NORECOMPUTE = OFF,
  SORT_IN_TEMPDB = OFF,
  ONLINE = OFF,
  ALLOW_ROW_LOCKS = ON,
  ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX IX_Personal_DomainUserId_IsDelete ON common.Personal
  (DomainUserId, IsDelete)
WHERE ([DomainUserId] IS NOT NULL AND [DomainUserId]<>'')
WITH (
  PAD_INDEX = OFF,
  FILLFACTOR = 90,
  DROP_EXISTING = OFF,
  STATISTICS_NORECOMPUTE = OFF,
  SORT_IN_TEMPDB = OFF,
  ONLINE = OFF,
  ALLOW_ROW_LOCKS = ON,
  ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX IX_Missing_Personal_UserId ON common.Personal
  (UserId)
INCLUDE (DomainUserId, UserIdForDomain)
WITH (
  PAD_INDEX = OFF,
  DROP_EXISTING = OFF,
  STATISTICS_NORECOMPUTE = OFF,
  SORT_IN_TEMPDB = OFF,
  ONLINE = OFF,
  ALLOW_ROW_LOCKS = ON,
  ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX Personal_idx ON common.Personal
  (ManagerAccountCode)
WITH (
  PAD_INDEX = OFF,
  DROP_EXISTING = OFF,
  STATISTICS_NORECOMPUTE = OFF,
  SORT_IN_TEMPDB = OFF,
  ONLINE = OFF,
  ALLOW_ROW_LOCKS = ON,
  ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY]
GO



--
-- Definition for table LegacyTransferMap :
-- Transfer esleme: eski tablo/GId -> yeni tablo/GId/CompanyId
--

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
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LegacyTransferMap', NULL, N'Transfer esleme tablosu. Eski tablo/GId ile yeni tablo/GId/CompanyId arasini tutar. Veri aktarimi sirasinda kullanilir.'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LegacyTransferMap', N'Id', N'Birincil anahtar (Identity)'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LegacyTransferMap', N'GId', N'Kuresel benzersiz kimlik'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LegacyTransferMap', N'CompanyId', N'Yeni kaydin CompanyId degeri (tenant)'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LegacyTransferMap', N'LegacyTableName', N'Eski tablo adi (ornek: CariTanim, UrunTanim)'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LegacyTransferMap', N'LegacyGId', N'Eski kayit GId'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LegacyTransferMap', N'LegacyLastChangeDate', N'Eski kayittaki Insert/Update/Delete tarihlerinden en guncel olan'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LegacyTransferMap', N'NewTableName', N'Yeni tablo adi (ornek: finance.Account veya Account)'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LegacyTransferMap', N'NewGId', N'Yeni kayit GId'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LegacyTransferMap', N'InsertDateTime', N'Esleme kaydinin olusturulma zamani'
GO

CREATE UNIQUE NONCLUSTERED INDEX UQ_LegacyTransferMap_LegacyTable_LegacyGId
  ON common.LegacyTransferMap (LegacyTableName, LegacyGId)
WITH (
  PAD_INDEX = OFF,
  IGNORE_DUP_KEY = OFF,
  DROP_EXISTING = OFF,
  STATISTICS_NORECOMPUTE = OFF,
  SORT_IN_TEMPDB = OFF,
  ONLINE = OFF,
  ALLOW_ROW_LOCKS = ON,
  ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX IX_LegacyTransferMap_CompanyId_NewTable_NewGId
  ON common.LegacyTransferMap (CompanyId, NewTableName, NewGId)
WITH (
  PAD_INDEX = OFF,
  DROP_EXISTING = OFF,
  STATISTICS_NORECOMPUTE = OFF,
  SORT_IN_TEMPDB = OFF,
  ONLINE = OFF,
  ALLOW_ROW_LOCKS = ON,
  ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX IX_LegacyTransferMap_LegacyLastChangeDate
  ON common.LegacyTransferMap (LegacyLastChangeDate)
WITH (
  PAD_INDEX = OFF,
  DROP_EXISTING = OFF,
  STATISTICS_NORECOMPUTE = OFF,
  SORT_IN_TEMPDB = OFF,
  ONLINE = OFF,
  ALLOW_ROW_LOCKS = ON,
  ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY]
GO


--
-- Definition for table CentralCurrency :
-- Legacy: TICARI_MASTER.dbo.MerkezDoviz
--

CREATE TABLE common.CentralCurrency (
  SortOrder int NOT NULL,
  Stat bit DEFAULT 1 NOT NULL,
  Code nvarchar(10) COLLATE Turkish_CI_AI NOT NULL,
  Info nvarchar(50) COLLATE Turkish_CI_AI NULL
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'common', N'CentralCurrency', NULL, N'Eski tablo: MerkezDoviz (TICARI_MASTER). Yeni şema: common.CentralCurrency'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'CentralCurrency', N'SortOrder', N'Listeleme sirasi. | Eski alan: MerkezDoviz.Sira'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'CentralCurrency', N'Stat', N'Aktif mi? | Eski alan: MerkezDoviz.Aktif'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'CentralCurrency', N'Code', N'Doviz kodu (PK). | Eski alan: MerkezDoviz.Kod'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'CentralCurrency', N'Info', N'Doviz aciklamasi. | Eski alan: MerkezDoviz.Info'
GO

--
-- Definition for table CentralCurrencyRate :
-- Legacy: TICARI_MASTER.dbo.MerkezDovizKur
--

CREATE TABLE common.CentralCurrencyRate (
  Id bigint IDENTITY(1, 1) NOT NULL,
  TransactionDate datetime NOT NULL,
  CurrencyCode nvarchar(10) COLLATE Turkish_CI_AI NOT NULL,
  Value decimal(18,4) NOT NULL,
  DefaultCurrencyCode nvarchar(10) COLLATE Turkish_CI_AI NOT NULL,
  InsertDateTime datetime DEFAULT getdate() NULL,
  UpdateDateTime datetime NULL
,
  CONSTRAINT CentralCurrencyRate_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'common', N'CentralCurrencyRate', NULL, N'Eski tablo: MerkezDovizKur (TICARI_MASTER). Yeni şema: common.CentralCurrencyRate'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'CentralCurrencyRate', N'Id', N'Birincil anahtar. | Eski alan: MerkezDovizKur.Id'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'CentralCurrencyRate', N'TransactionDate', N'Kur tarihi. | Eski alan: MerkezDovizKur.Tarih'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'CentralCurrencyRate', N'CurrencyCode', N'Doviz kodu (FK -> MerkezDoviz.Kod). | Eski alan: MerkezDovizKur.DovizKodu'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'CentralCurrencyRate', N'Value', N'Kur degeri. | Eski alan: MerkezDovizKur.Deger'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'CentralCurrencyRate', N'DefaultCurrencyCode', N'Karsilastirma para birimi (genelde TRY). | Eski alan: MerkezDovizKur.StandartDovizKodu'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'CentralCurrencyRate', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: MerkezDovizKur.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'CentralCurrencyRate', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: MerkezDovizKur.UpdateDateTime'
GO

--
-- Definition for table Company :
-- Legacy: TICARI_MASTER.dbo.SirketTanim
--

CREATE TABLE common.Company (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  Stat bit DEFAULT 1 NOT NULL,
  Code nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  City nvarchar(50) COLLATE Turkish_CI_AI NULL,
  IsActivationCompleted bit NOT NULL,
  ActivationDate datetime NULL,
  Title nvarchar(250) COLLATE Turkish_CI_AI NOT NULL,
  Email nvarchar(100) COLLATE Turkish_CI_AI NULL,
  WebsiteUrl nvarchar(200) COLLATE Turkish_CI_AI NULL,
  Address nvarchar(1000) COLLATE Turkish_CI_AI NULL,
  District nvarchar(50) COLLATE Turkish_CI_AI NULL,
  MapUrl nvarchar(400) COLLATE Turkish_CI_AI NULL,
  TaxOffice nvarchar(50) COLLATE Turkish_CI_AI NULL,
  TaxNumber nvarchar(50) COLLATE Turkish_CI_AI NULL,
  Phone1 nvarchar(30) COLLATE Turkish_CI_AI NULL,
  Phone2 nvarchar(30) COLLATE Turkish_CI_AI NULL,
  Fax nvarchar(30) COLLATE Turkish_CI_AI NULL,
  AuthorizedPerson nvarchar(30) COLLATE Turkish_CI_AI NULL,
  DefaultCurrencyCode nvarchar(10) COLLATE Turkish_CI_AI NULL,
  CurrencyCodes nvarchar(500) COLLATE Turkish_CI_AI NULL,
  Parameters nvarchar(30) COLLATE Turkish_CI_AI NULL,
  LegacyServerId bigint NULL,
  WeightBarcodeLength int NOT NULL,
  WeightBarcodeDecimalLength int NOT NULL,
  Integrator int NULL,
  IntegratorUserName nvarchar(100) COLLATE Turkish_CI_AI NULL,
  IntegratorPassword nvarchar(100) COLLATE Turkish_CI_AI NULL,
  UserCount int NULL,
  AboutUs nvarchar(max) COLLATE Turkish_CI_AI NULL,
  ColorSizeLabel nvarchar(30) COLLATE Turkish_CI_AI NULL,
  EcommerceStyle nvarchar(100) COLLATE Turkish_CI_AI NULL,
  EcommerceStylePrefix nvarchar(30) COLLATE Turkish_CI_AI NULL,
  FavIconPrefix nvarchar(30) COLLATE Turkish_CI_AI NULL,
  EcommerceStyle1 nvarchar(100) COLLATE Turkish_CI_AI NULL,
  EcommerceStylePrefix1 nvarchar(30) COLLATE Turkish_CI_AI NULL,
  FavIconPrefix1 nvarchar(30) COLLATE Turkish_CI_AI NULL,
  FreeShippingLimit decimal(18,4) NOT NULL,
  WeightServiceFee decimal(18,4) NULL,
  DesiWeightServiceFee decimal(18,4) NULL,
  ShippingFee decimal(18,4) NOT NULL,
  ShippingLabel nvarchar(30) COLLATE Turkish_CI_AI NULL,
  FacebookUrl nvarchar(200) COLLATE Turkish_CI_AI NULL,
  InstagramUrl nvarchar(200) COLLATE Turkish_CI_AI NULL,
  TwitterUrl nvarchar(200) COLLATE Turkish_CI_AI NULL,
  PinterestUrl nvarchar(200) COLLATE Turkish_CI_AI NULL,
  YoutubeUrl nvarchar(200) COLLATE Turkish_CI_AI NULL,
  IsBankTransferActive bit NULL,
  IsCashOnDeliveryActive bit NULL,
  EcommerceSiteName nvarchar(30) COLLATE Turkish_CI_AI NULL,
  ShowcaseName nvarchar(50) COLLATE Turkish_CI_AI NULL,
  ShowcaseLabel nvarchar(30) COLLATE Turkish_CI_AI NULL,
  GoogleAnalyticsId nvarchar(30) COLLATE Turkish_CI_AI NULL,
  FacebookPixelId nvarchar(30) COLLATE Turkish_CI_AI NULL,
  WhatsAppPhone nvarchar(30) COLLATE Turkish_CI_AI NULL,
  IsSslActive bit NOT NULL,
  IsWideTopMenuActive bit NOT NULL,
  AllProductsMainMenuActive bit NOT NULL,
  AllProductsSubMenuActive bit NOT NULL,
  ProductImageWidth int NOT NULL,
  ProductImageHeight int NOT NULL,
  SeoKeywords nvarchar(200) COLLATE Turkish_CI_AI NULL,
  GenerateSitemap bit NOT NULL,
  HideCategoryText bit NOT NULL,
  ShowAllStores bit NOT NULL,
  ShowAllCashRegisters bit NOT NULL,
  TrendyolSellerId nvarchar(30) COLLATE Turkish_CI_AI NULL,
  TrendyolApiKey varchar(30) COLLATE Turkish_CI_AI NULL,
  TrendyolApiSecret varchar(30) COLLATE Turkish_CI_AI NULL,
  ShowColorSizeOnProductScreen bit NOT NULL,
  FirstPurchaseDiscountPercent decimal(18,4) NOT NULL,
  UseOwnMailSettings bit NULL,
  MailAddress nvarchar(100) COLLATE Turkish_CI_AI NULL,
  MailDisplayName nvarchar(100) COLLATE Turkish_CI_AI NULL,
  MailUserName nvarchar(100) COLLATE Turkish_CI_AI NULL,
  MailPassword nvarchar(100) COLLATE Turkish_CI_AI NULL,
  MailHost nvarchar(100) COLLATE Turkish_CI_AI NULL,
  MailPort int NULL,
  IsDynamicExchangeRate bit NULL,
  LogoHeight int NOT NULL,
  MersisNumber nvarchar(50) COLLATE Turkish_CI_AI NULL,
  TradeRegistryNumber nvarchar(50) COLLATE Turkish_CI_AI NULL,
  ImportExternalData bit NOT NULL,
  ExternalSystemName nvarchar(30) COLLATE Turkish_CI_AI NULL,
  ExternalSystemIp nvarchar(100) COLLATE Turkish_CI_AI NULL,
  ExternalSystemDb nvarchar(100) COLLATE Turkish_CI_AI NULL,
  ExternalSystemUser nvarchar(100) COLLATE Turkish_CI_AI NULL,
  ExternalSystemPassword nvarchar(100) COLLATE Turkish_CI_AI NULL,
  IntegratorEInvoiceTemplate nvarchar(MAX) COLLATE Turkish_CI_AI NULL,
  IntegratorEArchiveTemplate nvarchar(MAX) COLLATE Turkish_CI_AI NULL,
  SmsPhoneNumber nvarchar(20) COLLATE Turkish_CI_AI NULL,
  SmsTitle nvarchar(30) COLLATE Turkish_CI_AI NULL,
  SmsUserName nvarchar(30) COLLATE Turkish_CI_AI NULL,
  SmsPassword nvarchar(30) COLLATE Turkish_CI_AI NULL,
  AppointmentFirstSms nvarchar(200) COLLATE Turkish_CI_AI NULL,
  AppointmentChangeSms nvarchar(200) COLLATE Turkish_CI_AI NULL,
  AppointmentCancelForceSms nvarchar(200) COLLATE Turkish_CI_AI NULL,
  AppointmentCancelPatientSms nvarchar(200) COLLATE Turkish_CI_AI NULL,
  AppCode nvarchar(30) COLLATE Turkish_CI_AI NULL,
  InsertDateTime datetime DEFAULT getdate() NULL,
  UpdateUser bigint NULL,
  UpdateDateTime datetime NULL,
  DeleteUser bigint NULL,
  DeleteDateTime datetime NULL
,
  CONSTRAINT Company_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', NULL, N'Eski tablo: SirketTanim (TICARI_MASTER). Yeni şema: common.Company'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'Id', N'Birincil anahtar. | Eski alan: SirketTanim.Id'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: SirketTanim.GId'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'Stat', N'Sirket kaydi aktif mi? | Eski alan: SirketTanim.Aktif'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'Code', N'Sirket kisa kodu. | Eski alan: SirketTanim.Kod'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'City', N'Il adi. | Eski alan: SirketTanim.Il'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'IsActivationCompleted', N'E-posta aktivasyonu tamamlandi mi? | Eski alan: SirketTanim.AktivasyonYapildi'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'ActivationDate', N'Aktivasyon tamamlanma tarihi. | Eski alan: SirketTanim.AktivasyonTarihi'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'Title', N'Sirket unvani. | Eski alan: SirketTanim.Unvan'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'Email', N'Sirket e-posta adresi. | Eski alan: SirketTanim.EMail'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'WebsiteUrl', N'Web sitesi URL. | Eski alan: SirketTanim.WebSayfasi'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'Address', N'Acik adres. | Eski alan: SirketTanim.Adres'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'District', N'Ilce adi. | Eski alan: SirketTanim.Ilce'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'MapUrl', N'Harita/konum URL. | Eski alan: SirketTanim.MapUrl'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'TaxOffice', N'Vergi dairesi. | Eski alan: SirketTanim.VergiDairesi'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'TaxNumber', N'Vergi kimlik numarasi (VKN). | Eski alan: SirketTanim.VergiNumarasi'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'Phone1', N'Birincil telefon. | Eski alan: SirketTanim.Telefon1'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'Phone2', N'Ikincil telefon. | Eski alan: SirketTanim.Telefon2'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'Fax', N'Faks numarasi. | Eski alan: SirketTanim.Faks'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'AuthorizedPerson', N'Yetkili kisi. | Eski alan: SirketTanim.Yetkili'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'DefaultCurrencyCode', N'Ana para birimi kodu (TRY, USD vb.). | Eski alan: SirketTanim.StandartDovizKodu'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'CurrencyCodes', N'Merkez Bankasindan alinacak kur kodlari listesi. Isletme hangi kurlarla calisacagini belirler. | Eski alan: SirketTanim.Kurlar'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'Parameters', N'Genel isletme parametreleri. | Eski alan: SirketTanim.Parametreler'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'LegacyServerId', N'Slave sunucu (FK -> ServerTanim.Id). | Eski alan: SirketTanim.ServerId'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'WeightBarcodeLength', N'Weight barkodu toplam uzunlugu. | Eski alan: SirketTanim.KgBarkodUzunlugu'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'WeightBarcodeDecimalLength', N'Weight barkodu ondalik hane sayisi. | Eski alan: SirketTanim.KgBarkodOndalikUzunluk'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'Integrator', N'e-Belge entegratoru: 0=MukellefDegilim, 50=Logo, 100=NesBilgi. | Eski alan: SirketTanim.Entegrator'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'IntegratorUserName', N'Entegrator kullanici adi (sifreli). | Eski alan: SirketTanim.EntegratorUserName'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'IntegratorPassword', N'Entegrator sifresi (sifreli). | Eski alan: SirketTanim.EntegratorPassword'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'UserCount', N'Izin verilen maksimum kullanici sayisi. | Eski alan: SirketTanim.KullaniciSayisi'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'AboutUs', N'E-ticaret hakkimizda metni. | Eski alan: SirketTanim.Hakkimizda'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'ColorSizeLabel', N'Varyant etiketi (varsayilan Beden). | Eski alan: SirketTanim.RenkBedenStr'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'EcommerceStyle', N'Birincil e-ticaret tema CSS dosyasi. | Eski alan: SirketTanim.ETicaretStyle'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'EcommerceStylePrefix', N'Birincil tema dosya oneki. | Eski alan: SirketTanim.ETicaretStylePrefix'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'FavIconPrefix', N'Birincil favicon oneki. | Eski alan: SirketTanim.FavIconPrefix'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'EcommerceStyle1', N'Ikincil e-ticaret tema CSS dosyasi. | Eski alan: SirketTanim.ETicaretStyle1'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'EcommerceStylePrefix1', N'Ikincil tema dosya oneki. | Eski alan: SirketTanim.ETicaretStylePrefix1'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'FavIconPrefix1', N'Ikincil favicon oneki. | Eski alan: SirketTanim.FavIconPrefix1'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'FreeShippingLimit', N'Ucretsiz kargo limit tutari. | Eski alan: SirketTanim.KargoBedavaLimit'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'WeightServiceFee', N'Kg bazli kargo hizmet bedeli. | Eski alan: SirketTanim.KgHizmetBedeli'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'DesiWeightServiceFee', N'DesiWeight bazli kargo hizmet bedeli. | Eski alan: SirketTanim.DesiWeightHizmetBedeli'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'ShippingFee', N'Sabit kargo ucreti. | Eski alan: SirketTanim.KargoBedeli'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'ShippingLabel', N'Kargo etiket sablonu. | Eski alan: SirketTanim.KargoEtiket'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'FacebookUrl', N'Facebook URL. | Eski alan: SirketTanim.FaceBookUrl'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'InstagramUrl', N'Instagram URL. | Eski alan: SirketTanim.InstagramUrl'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'TwitterUrl', N'Twitter/X URL. | Eski alan: SirketTanim.TwitterUrl'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'PinterestUrl', N'Pinterest URL. | Eski alan: SirketTanim.PinterestUrl'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'YoutubeUrl', N'YouTube URL. | Eski alan: SirketTanim.YoutubeUrl'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'IsBankTransferActive', N'Banka havalesi odeme aktif mi? | Eski alan: SirketTanim.BankaHavalesiAktif'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'IsCashOnDeliveryActive', N'Kapida odeme aktif mi? | Eski alan: SirketTanim.KapidaOdemeAktif'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'EcommerceSiteName', N'E-ticaret site basligi. | Eski alan: SirketTanim.ETicaretSiteAdi'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'ShowcaseName', N'Ana vitrin basligi. | Eski alan: SirketTanim.VitrinAdi'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'ShowcaseLabel', N'Vitrin etiket metni. | Eski alan: SirketTanim.VitrinEtiket'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'GoogleAnalyticsId', N'Google Analytics ID. | Eski alan: SirketTanim.GoogleAnalisticId'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'FacebookPixelId', N'Facebook Pixel ID. | Eski alan: SirketTanim.FacebookPixelId'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'WhatsAppPhone', N'WhatsApp iletisim numarasi. | Eski alan: SirketTanim.WhatsAppTelefon'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'IsSslActive', N'HTTPS zorunlulugu aktif mi? | Eski alan: SirketTanim.SSLAktif'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'IsWideTopMenuActive', N'Genis ust menu aktif mi? | Eski alan: SirketTanim.GenisUstMenuAktif'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'AllProductsMainMenuActive', N'Tum urunler ana menude mi? | Eski alan: SirketTanim.TumUrunlerAnaMenuAktif'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'AllProductsSubMenuActive', N'Tum urunler alt menude mi? | Eski alan: SirketTanim.TumUrunlerAltMenuAktif'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'ProductImageWidth', N'Urun resmi genisligi (px). | Eski alan: SirketTanim.UrunResimGenislik'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'ProductImageHeight', N'Urun resmi yuksekligi (px). | Eski alan: SirketTanim.UrunResimYukseklik'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'SeoKeywords', N'Meta keywords (SEO). | Eski alan: SirketTanim.SEOKelimeler'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'GenerateSitemap', N'Otomatik sitemap olusturulsun mu? | Eski alan: SirketTanim.SiteHaritasiOlustur'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'HideCategoryText', N'Kategori yazilari gizlensin mi? | Eski alan: SirketTanim.KategoriYaziGizle'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'ShowAllStores', N'Tum depolar gosterilsin mi? | Eski alan: SirketTanim.TumDepolariGoster'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'ShowAllCashRegisters', N'Tum kasalar gosterilsin mi? | Eski alan: SirketTanim.TumKasalariGoster'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'TrendyolSellerId', N'Trendyol satici ID. | Eski alan: SirketTanim.TrendyolSaticiId'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'TrendyolApiKey', N'Trendyol API key. | Eski alan: SirketTanim.TrendyolApiKey'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'TrendyolApiSecret', N'Trendyol API secret. | Eski alan: SirketTanim.TrendyolAPISecret'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'ShowColorSizeOnProductScreen', N'Urun detayda renk/beden secimi gosterilsin mi? | Eski alan: SirketTanim.UrunEkraniRenkBeden'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'FirstPurchaseDiscountPercent', N'Ilk alisveris iskonto yuzdesi. | Eski alan: SirketTanim.IlkAlisverisdeIskontoYuzdesi'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'UseOwnMailSettings', N'Isletme kendi SMTP sunucusunu kullansin mi? | Eski alan: SirketTanim.KendiMailimiKullan'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'MailAddress', N'Gonderen e-posta adresi. | Eski alan: SirketTanim.MailAddress'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'MailDisplayName', N'Gonderen gorunen adi. | Eski alan: SirketTanim.MailDisplayName'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'MailUserName', N'SMTP kullanici adi. | Eski alan: SirketTanim.MailUserName'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'MailPassword', N'SMTP sifresi. | Eski alan: SirketTanim.MailPassword'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'MailHost', N'SMTP sunucu adresi. | Eski alan: SirketTanim.MailHost'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'MailPort', N'SMTP port. | Eski alan: SirketTanim.MailPort'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'IsDynamicExchangeRate', N'Merkez kurlari otomatik guncellensin mi? | Eski alan: SirketTanim.DinamikDovizKuru'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'LogoHeight', N'Site logo yuksekligi (px). | Eski alan: SirketTanim.LogoYukseklik'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'MersisNumber', N'MERSIS numarasi. | Eski alan: SirketTanim.MersisNo'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'TradeRegistryNumber', N'Ticaret sicil numarasi. | Eski alan: SirketTanim.TicaretSicilNo'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'ImportExternalData', N'Harici sistemden veri cekme aktif mi? | Eski alan: SirketTanim.DisaridanVeriAl'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'ExternalSystemName', N'Harici sistem adi. | Eski alan: SirketTanim.DisSistemAdi'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'ExternalSystemIp', N'Harici sistem IP/adres. | Eski alan: SirketTanim.DisSistemIP'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'ExternalSystemDb', N'Harici sistem veritabani. | Eski alan: SirketTanim.DisSistemDB'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'ExternalSystemUser', N'Harici sistem DB kullanicisi. | Eski alan: SirketTanim.DisSistemUser'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'ExternalSystemPassword', N'Harici sistem DB sifresi. | Eski alan: SirketTanim.DisSistemParola'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'IntegratorEInvoiceTemplate', N'e-Fatura tasarim/XSLT sablonu. | Eski alan: SirketTanim.EntegratorEfaturaTasarim'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'IntegratorEArchiveTemplate', N'e-Arsiv tasarim/XSLT sablonu. | Eski alan: SirketTanim.EntegratorEarsivTasarim'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'SmsPhoneNumber', N'SMS gonderim telefonu. | Eski alan: SirketTanim.SMSTelNo'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'SmsTitle', N'SMS baslik (originator). | Eski alan: SirketTanim.SmsBaslik'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'SmsUserName', N'SMS servis kullanici adi. | Eski alan: SirketTanim.SmsUserName'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'SmsPassword', N'SMS servis sifresi. | Eski alan: SirketTanim.SmsPassword'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'AppointmentFirstSms', N'Randevu olusturma SMS sablonu. | Eski alan: SirketTanim.RandevuIlkSMS'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'AppointmentChangeSms', N'Randevu degisiklik SMS sablonu. | Eski alan: SirketTanim.RandevuDegisiklikSMS'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'AppointmentCancelForceSms', N'Mucbir sebeple iptal SMS sablonu. | Eski alan: SirketTanim.RandevuIptalMucbirSMS'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'AppointmentCancelPatientSms', N'Hasta istegiyle iptal SMS sablonu. | Eski alan: SirketTanim.RandevuIptaHastaninIstegiSMS'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'AppCode', N'Uygulama kodu (web.config AppCode). | Eski alan: SirketTanim.AppCode'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'InsertDateTime', N'Kayit olusturma tarihi. | Eski alan: SirketTanim.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'UpdateUser', N'Son guncelleyen kullanici. | Eski alan: SirketTanim.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'UpdateDateTime', N'Son guncelleme tarihi. | Eski alan: SirketTanim.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'DeleteUser', N'Silen kullanici (soft delete). | Eski alan: SirketTanim.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Company', N'DeleteDateTime', N'Silme tarihi (soft delete). | Eski alan: SirketTanim.DeleteDateTime'
GO

--
-- Definition for table Counter :
-- Legacy: TICARI_SLAVE1.dbo.Sayac
--

CREATE TABLE common.Counter (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  Type nvarchar(15) COLLATE Turkish_CI_AI NULL,
  SerialCode nvarchar(10) COLLATE Turkish_CI_AI NULL,
  StartNumber int NOT NULL,
  EndNumber int NOT NULL,
  NextNumber int NOT NULL,
  InsertUser bigint DEFAULT 0 NOT NULL,
  InsertDateTime datetime DEFAULT getdate() NOT NULL,
  UpdateUser bigint NULL,
  UpdateDateTime datetime NULL,
  DeleteUser bigint NULL,
  DeleteDateTime datetime NULL,
  RecordDateTime datetime DEFAULT getdate() NOT NULL
,
  CONSTRAINT Counter_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Counter', NULL, N'Eski tablo: Sayac (TICARI_SLAVE1). Yeni şema: common.Counter'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Counter', N'Id', N'Birincil anahtar. | Eski alan: Sayac.Id'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Counter', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: Sayac.GId'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Counter', N'CompanyId', N'Bagli sirket. | Eski alan: Sayac.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Counter', N'Type', N'Sayac tipi (Fatura, Irsaliye vb.). | Eski alan: Sayac.Tip'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Counter', N'SerialCode', N'Belge seri kodu. | Eski alan: Sayac.SerialCode'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Counter', N'StartNumber', N'Baslangic numarasi. | Eski alan: Sayac.StartNumber'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Counter', N'EndNumber', N'Bitis numarasi. | Eski alan: Sayac.EndNumber'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Counter', N'NextNumber', N'Siradaki numara. | Eski alan: Sayac.SiradakiNo'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Counter', N'InsertUser', N'Olusturan kullanici. | Eski alan: Sayac.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Counter', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: Sayac.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Counter', N'UpdateUser', N'Guncelleyen kullanici. | Eski alan: Sayac.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Counter', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: Sayac.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Counter', N'DeleteUser', N'Silen kullanici. | Eski alan: Sayac.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Counter', N'DeleteDateTime', N'Silme tarihi. | Eski alan: Sayac.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Counter', N'RecordDateTime', N'Kayit zaman damgasi. | Eski alan: Sayac.RecDateTime'
GO

--
-- Definition for table CounterReference :
-- Legacy: TICARI_SLAVE1.dbo.SayacReferans
--

CREATE TABLE common.CounterReference (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  Type nvarchar(15) COLLATE Turkish_CI_AI NOT NULL,
  CounterDefId bigint NOT NULL,
  StoreId bigint NOT NULL,
  InsertUser bigint DEFAULT 0 NOT NULL,
  InsertDateTime datetime DEFAULT getdate() NOT NULL,
  UpdateUser bigint NULL,
  UpdateDateTime datetime NULL,
  DeleteUser bigint NULL,
  DeleteDateTime datetime NULL,
  RecordDateTime datetime DEFAULT getdate() NOT NULL
,
  CONSTRAINT CounterReference_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'common', N'CounterReference', NULL, N'Eski tablo: SayacReferans (TICARI_SLAVE1). Yeni şema: common.CounterReference'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'CounterReference', N'Id', N'Birincil anahtar. | Eski alan: SayacReferans.Id'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'CounterReference', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: SayacReferans.GId'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'CounterReference', N'CompanyId', N'Bagli sirket. | Eski alan: SayacReferans.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'CounterReference', N'Type', N'Sayac tipi. | Eski alan: SayacReferans.Tip'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'CounterReference', N'CounterDefId', N'Sayac (FK -> Sayac.Id). | Eski alan: SayacReferans.SayacTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'CounterReference', N'StoreId', N'Depo (FK -> DepoTanim.Id). | Eski alan: SayacReferans.DepoTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'CounterReference', N'InsertUser', N'Olusturan kullanici. | Eski alan: SayacReferans.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'CounterReference', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: SayacReferans.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'CounterReference', N'UpdateUser', N'Guncelleyen kullanici. | Eski alan: SayacReferans.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'CounterReference', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: SayacReferans.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'CounterReference', N'DeleteUser', N'Silen kullanici. | Eski alan: SayacReferans.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'CounterReference', N'DeleteDateTime', N'Silme tarihi. | Eski alan: SayacReferans.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'CounterReference', N'RecordDateTime', N'Kayit zaman damgasi. | Eski alan: SayacReferans.RecDateTime'
GO

--
-- Definition for table EmailTemplate :
-- Legacy: TICARI_SLAVE1.dbo.EMailTanim
--

CREATE TABLE common.EmailTemplate (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  Stat bit DEFAULT 1 NOT NULL,
  Konu nvarchar(200) COLLATE Turkish_CI_AI NOT NULL,
  Body nvarchar(max) COLLATE Turkish_CI_AI NULL,
  InsertDateTime datetime DEFAULT getdate() NOT NULL,
  InsertUser bigint DEFAULT 0 NOT NULL,
  UpdateDateTime datetime NULL,
  UpdateUser bigint NULL,
  DeleteDateTime datetime NULL,
  DeleteUser bigint NULL,
  RecordDateTime datetime DEFAULT getdate() NOT NULL
,
  CONSTRAINT EmailTemplate_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'common', N'EmailTemplate', NULL, N'Eski tablo: EMailTanim (TICARI_SLAVE1). Yeni şema: common.EmailTemplate'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'EmailTemplate', N'Id', N'Birincil anahtar. | Eski alan: EMailTanim.Id'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'EmailTemplate', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: EMailTanim.GId'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'EmailTemplate', N'CompanyId', N'Bagli sirket. | Eski alan: EMailTanim.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'EmailTemplate', N'Stat', N'Sablon aktif mi? | Eski alan: EMailTanim.Aktif'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'EmailTemplate', N'Konu', N'E-posta konusu. | Eski alan: EMailTanim.Konu'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'EmailTemplate', N'Body', N'E-posta govde metni (HTML/text). | Eski alan: EMailTanim.Body'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'EmailTemplate', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: EMailTanim.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'EmailTemplate', N'InsertUser', N'Olusturan kullanici. | Eski alan: EMailTanim.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'EmailTemplate', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: EMailTanim.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'EmailTemplate', N'UpdateUser', N'Guncelleyen kullanici. | Eski alan: EMailTanim.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'EmailTemplate', N'DeleteDateTime', N'Silme tarihi. | Eski alan: EMailTanim.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'EmailTemplate', N'DeleteUser', N'Silen kullanici. | Eski alan: EMailTanim.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'EmailTemplate', N'RecordDateTime', N'Kayit zaman damgasi. | Eski alan: EMailTanim.RecDateTime'
GO

--
-- Definition for table LegacyRole :
-- Legacy: TICARI_MASTER.dbo.Roles
--

CREATE TABLE common.LegacyRole (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  AppCode nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  RoleCode nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  RoleName nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  GroupCode nvarchar(100) COLLATE Turkish_CI_AI NULL,
  SubGroupCode nvarchar(100) COLLATE Turkish_CI_AI NULL,
  GroupOrdered int NOT NULL,
  SubGroupOrdered int NOT NULL
,
  CONSTRAINT LegacyRole_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LegacyRole', NULL, N'Eski tablo: Roles (TICARI_MASTER). Yeni şema: common.LegacyRole'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LegacyRole', N'Id', N'Birincil anahtar. | Eski alan: Roles.Id'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LegacyRole', N'GId', N'Rol GUID. | Eski alan: Roles.GId'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LegacyRole', N'AppCode', N'Uygulama kodu. | Eski alan: Roles.AppCode'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LegacyRole', N'RoleCode', N'Rol kodu. | Eski alan: Roles.RoleCode'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LegacyRole', N'RoleName', N'Rol adi. | Eski alan: Roles.RoleName'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LegacyRole', N'GroupCode', N'Menu/grup kodu. | Eski alan: Roles.GroupCode'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LegacyRole', N'SubGroupCode', N'Alt grup kodu. | Eski alan: Roles.SubGroupCode'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LegacyRole', N'GroupOrdered', N'Grup siralama. | Eski alan: Roles.GroupOrdered'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LegacyRole', N'SubGroupOrdered', N'Alt grup siralama. | Eski alan: Roles.SubGroupOrdered'
GO

--
-- Definition for table License :
-- Legacy: TICARI_MASTER.dbo.Lisans
--

CREATE TABLE common.License (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NULL,
  CompanyId bigint NOT NULL,
  IsGift bit NULL,
  LicenseType nvarchar(10) COLLATE Turkish_CI_AI NOT NULL,
  StartDate datetime NOT NULL,
  EndDate datetime NOT NULL,
  CollectionMethod int NULL,
  CollectionAmount decimal(18,2) NOT NULL,
  InsertDateTime datetime DEFAULT getdate() NOT NULL,
  InsertUser bigint DEFAULT 0 NOT NULL,
  UpdateDateTime datetime NULL,
  UpdateUser bigint NULL,
  DeleteDateTime date NULL,
  DeleteUser bigint NULL
,
  CONSTRAINT License_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'common', N'License', NULL, N'Eski tablo: Lisans (TICARI_MASTER). Yeni şema: common.License'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'License', N'Id', N'Birincil anahtar. | Eski alan: Lisans.Id'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'License', N'GId', N'Global benzersiz kimlik. | Eski alan: Lisans.GId'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'License', N'CompanyId', N'Bagli sirket (FK). | Eski alan: Lisans.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'License', N'IsGift', N'IsGift/demo lisans mi? | Eski alan: Lisans.IsGift'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'License', N'LicenseType', N'Lisans paket kodu (FK -> LisansTipi.Kod). | Eski alan: Lisans.LisansTipi'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'License', N'StartDate', N'Lisans baslangic tarihi. | Eski alan: Lisans.BaslangicTarihi'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'License', N'EndDate', N'Lisans bitis tarihi. | Eski alan: Lisans.BitisTarihi'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'License', N'CollectionMethod', N'Tahsilat/odeme sekli kodu. | Eski alan: Lisans.CollectionMethod'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'License', N'CollectionAmount', N'Lisans ucreti. | Eski alan: Lisans.CollectionAmount'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'License', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: Lisans.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'License', N'InsertUser', N'Olusturan kullanici. | Eski alan: Lisans.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'License', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: Lisans.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'License', N'UpdateUser', N'Guncelleyen kullanici. | Eski alan: Lisans.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'License', N'DeleteDateTime', N'Silme tarihi. | Eski alan: Lisans.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'License', N'DeleteUser', N'Silen kullanici. | Eski alan: Lisans.DeleteUser'
GO

--
-- Definition for table LicenseType :
-- Legacy: TICARI_MASTER.dbo.LisansTipi
--

CREATE TABLE common.LicenseType (
  Code nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  Value int NOT NULL,
  ProductTracking bit NULL,
  AccountTracking bit NULL,
  CheckNoteTracking bit NULL,
  TradeDocumentTracking bit NULL,
  PaymentTracking bit NULL,
  BankTracking bit NULL,
  ShippingNoteTracking bit NULL,
  QuoteOrderTracking bit NULL,
  EArchiveEInvoice bit NULL,
  MedicalService bit NULL,
  IsEcommerce bit NULL
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LicenseType', NULL, N'Eski tablo: LisansTipi (TICARI_MASTER). Yeni şema: common.LicenseType'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LicenseType', N'Code', N'Paket kodu (PK). | Eski alan: LisansTipi.Kod'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LicenseType', N'Value', N'Paket oncelik degeri (bitmask). | Eski alan: LisansTipi.Deger'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LicenseType', N'ProductTracking', N'Urun/stok modulu dahil mi? | Eski alan: LisansTipi.UrunTakip'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LicenseType', N'AccountTracking', N'Cari modulu dahil mi? | Eski alan: LisansTipi.CariTakip'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LicenseType', N'CheckNoteTracking', N'Cek/senet modulu dahil mi? | Eski alan: LisansTipi.CekSenetTakip'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LicenseType', N'TradeDocumentTracking', N'Fatura modulu dahil mi? | Eski alan: LisansTipi.FaturaTakip'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LicenseType', N'PaymentTracking', N'Odeme modulu dahil mi? | Eski alan: LisansTipi.OdemeTakip'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LicenseType', N'BankTracking', N'Banka modulu dahil mi? | Eski alan: LisansTipi.BankaTakip'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LicenseType', N'ShippingNoteTracking', N'Irsaliye modulu dahil mi? | Eski alan: LisansTipi.ShippingNoteTracking'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LicenseType', N'QuoteOrderTracking', N'Teklif/siparis modulu dahil mi? | Eski alan: LisansTipi.QuoteOrderTracking'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LicenseType', N'EArchiveEInvoice', N'e-Arsiv/e-Fatura modulu dahil mi? | Eski alan: LisansTipi.EArsivEFatura'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LicenseType', N'MedicalService', N'Servis modulu dahil mi? | Eski alan: LisansTipi.Servis'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'LicenseType', N'IsEcommerce', N'E-ticaret modulu dahil mi? | Eski alan: LisansTipi.ETicaret'
GO

--
-- Definition for table PageDef :
-- Legacy: TICARI_MASTER.dbo.SayfaTanim
--

CREATE TABLE common.PageDef (
  Id bigint IDENTITY(1, 1) NOT NULL,
  Description nvarchar(200) COLLATE Turkish_CI_AI NULL,
  Url nvarchar(200) COLLATE Turkish_CI_AI NULL,
  LicenseType nvarchar(30) COLLATE Turkish_CI_AI NULL
,
  CONSTRAINT PageDef_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'common', N'PageDef', NULL, N'Eski tablo: SayfaTanim (TICARI_MASTER). Yeni şema: common.PageDef'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'PageDef', N'Id', N'Sayfa ID. | Eski alan: SayfaTanim.Id'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'PageDef', N'Description', N'Sayfa aciklamasi. | Eski alan: SayfaTanim.Aciklama'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'PageDef', N'Url', N'Sayfa URL yolu. | Eski alan: SayfaTanim.Url'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'PageDef', N'LicenseType', N'Gerekli minimum lisans tipi. | Eski alan: SayfaTanim.LisansTipi'
GO

--
-- Definition for table Token :
-- Legacy: TICARI_MASTER.dbo.Token
--

CREATE TABLE common.Token (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  PersonalId bigint NOT NULL,
  ExpreDate datetime NOT NULL,
  Application nvarchar(30) COLLATE Turkish_CI_AI NULL,
  UpdateDateTime datetime NOT NULL
,
  CONSTRAINT Token_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Token', NULL, N'Eski tablo: Token (TICARI_MASTER). Yeni şema: common.Token'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Token', N'Id', N'Birincil anahtar. | Eski alan: Token.Id'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Token', N'GId', N'Token GUID. | Eski alan: Token.GId'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Token', N'CompanyId', N'Token sahibi sirket. | Eski alan: Token.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Token', N'PersonalId', N'Token sahibi kullanici. | Eski alan: Token.PersonelTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Token', N'ExpreDate', N'Token gecerlilik bitis tarihi. | Eski alan: Token.ExpreDate'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Token', N'Application', N'Token olusturan uygulama (Web, API, Mobil). | Eski alan: Token.Application'
GO

EXEC dbo._SetFullSchemaDescription N'common', N'Token', N'UpdateDateTime', N'Son aktivite zamani. | Eski alan: Token.UpdateDateTime'
GO

--
-- Definition for table Account :
-- Legacy: TICARI_SLAVE1.dbo.CariTanim
--

CREATE TABLE finance.Account (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  AccountType int NOT NULL,
  Stat bit DEFAULT 1 NULL,
  AccountCode nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  FirstName nvarchar(250) COLLATE Turkish_CI_AI NULL,
  LastName nvarchar(250) COLLATE Turkish_CI_AI NULL,
  FullNameOrTitle nvarchar(250) COLLATE Turkish_CI_AI NULL,
  BankAccountHolder nvarchar(100) COLLATE Turkish_CI_AI NULL,
  Email nvarchar(100) COLLATE Turkish_CI_AI NULL,
  Password nvarchar(100) COLLATE Turkish_CI_AI NULL,
  CorrespondenceEmail nvarchar(100) COLLATE Turkish_CI_AI NULL,
  IsActivationCompleted bit NOT NULL,
  WebsiteUrl nvarchar(100) COLLATE Turkish_CI_AI NULL,
  Address nvarchar(MAX) COLLATE Turkish_CI_AI NULL,
  City nvarchar(50) COLLATE Turkish_CI_AI NULL,
  District nvarchar(50) COLLATE Turkish_CI_AI NULL,
  PostalCode nvarchar(30) COLLATE Turkish_CI_AI NULL,
  TaxOffice nvarchar(50) COLLATE Turkish_CI_AI NULL,
  TaxNumber nvarchar(50) COLLATE Turkish_CI_AI NULL,
  Phone1 nvarchar(30) COLLATE Turkish_CI_AI NULL,
  Phone2 nvarchar(30) COLLATE Turkish_CI_AI NULL,
  Fax nvarchar(30) COLLATE Turkish_CI_AI NULL,
  BankBranchCode nvarchar(30) COLLATE Turkish_CI_AI NULL,
  AccountNumber nvarchar(30) COLLATE Turkish_CI_AI NULL,
  IBAN nvarchar(50) COLLATE Turkish_CI_AI NULL,
  IsETaxpayerActive bit NULL,
  DiscountRate decimal(18,4) NOT NULL,
  PriceGroupId bigint NULL,
  IntegrationName nvarchar(30) COLLATE Turkish_CI_AI NULL,
  IntegrationId nvarchar(50) COLLATE Turkish_CI_AI NULL,
  Label nvarchar(500) COLLATE Turkish_CI_AI NULL,
  ShowIbanOnEcommerce bit NULL,
  NewsletterOptIn bit NOT NULL,
  SmsOptIn bit NOT NULL,
  MembershipAgreementAccepted bit NOT NULL,
  KvkkAccepted bit NOT NULL,
  RecordSource int NOT NULL,
  NationalId nvarchar(30) COLLATE Turkish_CI_AI NULL,
  Gender nvarchar(10) COLLATE Turkish_CI_AI NULL,
  BirthDate datetime NULL,
  FreeShipping bit NOT NULL,
  TrendyolId bigint NULL,
  TrendyolDescription nvarchar(100) COLLATE Turkish_CI_AI NULL,
  PaymentTermDays int NOT NULL,
  SocialSecurity varchar(30) COLLATE Turkish_CI_AI NULL,
  AdditionalInfo nvarchar(500) COLLATE Turkish_CI_AI NULL,
  KvkkApprovalCode nvarchar(10) COLLATE Turkish_CI_AI NULL,
  InsertUser bigint DEFAULT 0 NULL,
  RecordDateTime datetime DEFAULT getdate() NULL,
  InsertDateTime datetime DEFAULT getdate() NULL,
  UpdateDateTime datetime NULL,
  UpdateUser bigint NULL,
  DeleteDateTime datetime NULL,
  DeleteUser bigint NULL
,
  CONSTRAINT Account_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', NULL, N'Eski tablo: CariTanim (TICARI_SLAVE1). Yeni şema: finance.Account'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'Id', N'Birincil anahtar. | Eski alan: CariTanim.Id'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: CariTanim.GId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'CompanyId', N'Bagli sirket. | Eski alan: CariTanim.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'AccountType', N'0=Musteri, 1=Toptanci, 2=Banka | Eski alan: CariTanim.CariTipi'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'Stat', N'Cari kaydi aktif mi? | Eski alan: CariTanim.Aktif'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'AccountCode', N'Cari hesap kodu. | Eski alan: CariTanim.CariKodu'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'FirstName', N'Ad (bireysel musteri). | Eski alan: CariTanim.Ad'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'LastName', N'Soyad (bireysel musteri). | Eski alan: CariTanim.Soyad'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'FullNameOrTitle', N'Ad soyad veya firma unvani. | Eski alan: CariTanim.AdSoyadUnvan'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'BankAccountHolder', N'Banka hesap sahibi adi. | Eski alan: CariTanim.BankaHesapSahibi'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'Email', N'E-posta adresi / e-ticaret giris. | Eski alan: CariTanim.EMail'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'Password', N'E-ticaret sifresi. | Eski alan: CariTanim.Parola'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'CorrespondenceEmail', N'Yazisma/bildirim e-posta adresi. | Eski alan: CariTanim.YazismaEMail'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'IsActivationCompleted', N'E-posta aktivasyonu tamamlandi mi? | Eski alan: CariTanim.AktivasyonYapildi'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'WebsiteUrl', N'Web sitesi URL. | Eski alan: CariTanim.WebSayfasi'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'Address', N'Acik adres. | Eski alan: CariTanim.Adres'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'City', N'Il adi. | Eski alan: CariTanim.Il'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'District', N'Ilce adi. | Eski alan: CariTanim.Ilce'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'PostalCode', N'Posta kodu. | Eski alan: CariTanim.PostaKodu'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'TaxOffice', N'Vergi dairesi. | Eski alan: CariTanim.VergiDairesi'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'TaxNumber', N'Vergi kimlik numarasi (VKN). | Eski alan: CariTanim.VergiNumarasi'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'Phone1', N'Birincil telefon. | Eski alan: CariTanim.Telefon1'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'Phone2', N'Ikincil telefon. | Eski alan: CariTanim.Telefon2'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'Fax', N'Faks numarasi. | Eski alan: CariTanim.Faks'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'BankBranchCode', N'Banka sube kodu. | Eski alan: CariTanim.BankaSubeKodu'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'AccountNumber', N'Banka hesap numarasi. | Eski alan: CariTanim.HesapNo'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'IBAN', N'IBAN numarasi. | Eski alan: CariTanim.IBAN'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'IsETaxpayerActive', N'e-Mukellef (e-Fatura/e-Arsiv) aktif mi? | Eski alan: CariTanim.EMukellefAktif'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'DiscountRate', N'Varsayilan iskonto orani (%). | Eski alan: CariTanim.IskontoOrani'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'PriceGroupId', N'Bagli fiyat grubu (FK -> FiyatGrupTanim.Id). | Eski alan: CariTanim.FiyatGrupTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'IntegrationName', N'Harici entegrasyon adi (Trendyol vb.). | Eski alan: CariTanim.EntegrasyonAdi'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'IntegrationId', N'Harici entegrasyon cari ID. | Eski alan: CariTanim.EntegrasyonId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'Label', N'Etiket listesi (virgul/noktali virgul ile). | Eski alan: CariTanim.Etiket'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'ShowIbanOnEcommerce', N'E-ticarette IBAN gosterilsin mi? | Eski alan: CariTanim.ETicaretIBANGoster'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'NewsletterOptIn', N'E-bulten almak istiyor mu? | Eski alan: CariTanim.EBulten'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'SmsOptIn', N'SMS almak istiyor mu? | Eski alan: CariTanim.SmsAlmakIstiyorum'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'MembershipAgreementAccepted', N'Uyelik sozlesmesi onayi. | Eski alan: CariTanim.UyelikSozlesmesiniOkudum'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'KvkkAccepted', N'KVKK metni onayi. | Eski alan: CariTanim.KvkkOkudum'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'RecordSource', N'0=Normal, 1=ETicaret | Eski alan: CariTanim.KayitYeri'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'NationalId', N'TC kimlik numarasi. | Eski alan: CariTanim.TCKimlikNo'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'Gender', N'K=Kadin, E=Erkek, C=Cocuk | Eski alan: CariTanim.Cinsiyet'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'BirthDate', N'Dogum tarihi. | Eski alan: CariTanim.DogumTarihi'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'FreeShipping', N'Bu cariye ucretsiz kargo uygulansin mi? | Eski alan: CariTanim.KargoBedava'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'TrendyolId', N'Trendyol musteri ID. | Eski alan: CariTanim.TrendyolId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'TrendyolDescription', N'Trendyol aciklama/not. | Eski alan: CariTanim.TrendyolAciklama'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'PaymentTermDays', N'Varsayilan vade gunu. | Eski alan: CariTanim.VadeGun'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'SocialSecurity', N'Sosyal guvence bilgisi. | Eski alan: CariTanim.SosyalGuvence'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'AdditionalInfo', N'Ek bilgi/not alani. | Eski alan: CariTanim.EkBilgi'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'KvkkApprovalCode', N'KVKK SMS/e-posta onay kodu. | Eski alan: CariTanim.KVKKOnayKodu'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'InsertUser', N'Olusturan kullanici. | Eski alan: CariTanim.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'RecordDateTime', N'Kayit zaman damgasi. | Eski alan: CariTanim.RecDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: CariTanim.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: CariTanim.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'UpdateUser', N'Guncelleyen kullanici. | Eski alan: CariTanim.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'DeleteDateTime', N'Silme tarihi. | Eski alan: CariTanim.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Account', N'DeleteUser', N'Silen kullanici. | Eski alan: CariTanim.DeleteUser'
GO

--
-- Definition for table AccountAddress :
-- Legacy: TICARI_SLAVE1.dbo.CariTanimAdres
--

CREATE TABLE finance.AccountAddress (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  AccountId bigint NOT NULL,
  AddressTitle nvarchar(50) COLLATE Turkish_CI_AI NOT NULL,
  AdSoyad nvarchar(250) COLLATE Turkish_CI_AI NULL,
  Address nvarchar(MAX) COLLATE Turkish_CI_AI NOT NULL,
  City nvarchar(50) COLLATE Turkish_CI_AI NOT NULL,
  District nvarchar(50) COLLATE Turkish_CI_AI NOT NULL,
  PostalCode nvarchar(30) COLLATE Turkish_CI_AI NULL,
  Email nvarchar(100) COLLATE Turkish_CI_AI NULL,
  Phone nvarchar(30) COLLATE Turkish_CI_AI NULL,
  InsertUser bigint DEFAULT 0 NULL,
  InsertDateTime datetime DEFAULT getdate() NULL,
  UpdateDateTime datetime NULL,
  UpdateUser bigint NULL,
  DeleteDateTime datetime NULL,
  DeleteUser bigint NULL,
  RecordDateTime datetime DEFAULT getdate() NOT NULL
,
  CONSTRAINT AccountAddress_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountAddress', NULL, N'Eski tablo: CariTanimAdres (TICARI_SLAVE1). Yeni şema: finance.AccountAddress'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountAddress', N'Id', N'Birincil anahtar. | Eski alan: CariTanimAdres.Id'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountAddress', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: CariTanimAdres.GId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountAddress', N'CompanyId', N'Bagli sirket. | Eski alan: CariTanimAdres.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountAddress', N'AccountId', N'Bagli cari (FK -> CariTanim.Id). | Eski alan: CariTanimAdres.CariTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountAddress', N'AddressTitle', N'Adres basligi (Ev, Is vb.). | Eski alan: CariTanimAdres.AdresBaslik'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountAddress', N'AdSoyad', N'Teslim alacak kisi ad soyad. | Eski alan: CariTanimAdres.AdSoyad'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountAddress', N'Address', N'Acik adres. | Eski alan: CariTanimAdres.Adres'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountAddress', N'City', N'Il. | Eski alan: CariTanimAdres.Il'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountAddress', N'District', N'Ilce. | Eski alan: CariTanimAdres.Ilce'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountAddress', N'PostalCode', N'Posta kodu. | Eski alan: CariTanimAdres.PostaKodu'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountAddress', N'Email', N'E-posta. | Eski alan: CariTanimAdres.EMail'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountAddress', N'Phone', N'Telefon. | Eski alan: CariTanimAdres.Telefon'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountAddress', N'InsertUser', N'Olusturan kullanici. | Eski alan: CariTanimAdres.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountAddress', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: CariTanimAdres.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountAddress', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: CariTanimAdres.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountAddress', N'UpdateUser', N'Guncelleyen kullanici. | Eski alan: CariTanimAdres.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountAddress', N'DeleteDateTime', N'Silme tarihi. | Eski alan: CariTanimAdres.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountAddress', N'DeleteUser', N'Silen kullanici. | Eski alan: CariTanimAdres.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountAddress', N'RecordDateTime', N'Kayit zaman damgasi. | Eski alan: CariTanimAdres.RecDateTime'
GO

--
-- Definition for table AccountDocument :
-- Legacy: TICARI_SLAVE1.dbo.CariTanimBelge
--

CREATE TABLE finance.AccountDocument (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  DocumentDefId bigint NOT NULL,
  DocumentTypeDescription nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  AccountId bigint NOT NULL,
  DocumentContent nvarchar(max) COLLATE Turkish_CI_AI NOT NULL,
  ApprovalType nvarchar(10) COLLATE Turkish_CI_AI NULL,
  InsertUser bigint DEFAULT 0 NOT NULL,
  InsertDateTime datetime DEFAULT getdate() NOT NULL,
  UpdateUser bigint NULL,
  UpdateDateTime datetime NULL,
  DeleteUser bigint NULL,
  DeleteDateTime datetime NULL,
  RecordDateTime datetime DEFAULT getdate() NOT NULL
,
  CONSTRAINT AccountDocument_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountDocument', NULL, N'Eski tablo: CariTanimBelge (TICARI_SLAVE1). Yeni şema: finance.AccountDocument'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountDocument', N'Id', N'Birincil anahtar. | Eski alan: CariTanimBelge.Id'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountDocument', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: CariTanimBelge.GId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountDocument', N'CompanyId', N'Bagli sirket. | Eski alan: CariTanimBelge.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountDocument', N'DocumentDefId', N'Belge sablonu (FK -> BelgeTanim.Id). | Eski alan: CariTanimBelge.BelgeTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountDocument', N'DocumentTypeDescription', N'Belge aciklamasi (anlik kopya). | Eski alan: CariTanimBelge.BelgeTanimAciklama'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountDocument', N'AccountId', N'Bagli cari (FK -> CariTanim.Id). | Eski alan: CariTanimBelge.CariTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountDocument', N'DocumentContent', N'Imzalanan belge icerigi. | Eski alan: CariTanimBelge.BelgeIcerik'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountDocument', N'ApprovalType', N'Onay tipi kodu. | Eski alan: CariTanimBelge.OnayTipi'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountDocument', N'InsertUser', N'Olusturan kullanici. | Eski alan: CariTanimBelge.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountDocument', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: CariTanimBelge.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountDocument', N'UpdateUser', N'Guncelleyen kullanici. | Eski alan: CariTanimBelge.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountDocument', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: CariTanimBelge.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountDocument', N'DeleteUser', N'Silen kullanici. | Eski alan: CariTanimBelge.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountDocument', N'DeleteDateTime', N'Silme tarihi. | Eski alan: CariTanimBelge.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountDocument', N'RecordDateTime', N'Kayit zaman damgasi. | Eski alan: CariTanimBelge.RecDateTime'
GO

--
-- Definition for table AccountTransaction :
-- Legacy: TICARI_SLAVE1.dbo.CariHareket
--

CREATE TABLE finance.AccountTransaction (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  AccountId bigint NOT NULL,
  TransactionDate datetime NOT NULL,
  DueDate datetime NULL,
  DocumentNo nvarchar(50) COLLATE Turkish_CI_AI NULL,
  TransactionType int NOT NULL,
  TradeDocumentId bigint NOT NULL,
  Description nvarchar(1000) COLLATE Turkish_CI_AI NULL,
  DebitAmount decimal(18,4) NOT NULL,
  CreditAmount decimal(18,4) NOT NULL,
  CurrencyCode nvarchar(10) COLLATE Turkish_CI_AI NOT NULL,
  RelatedAccountTransactionId bigint NULL,
  CheckNoteDefId bigint NULL,
  CashRegisterId bigint NULL,
  Label nvarchar(500) COLLATE Turkish_CI_AI NULL,
  InsertDateTime datetime DEFAULT getdate() NOT NULL,
  InsertUser bigint DEFAULT 0 NULL,
  UpdateDateTime datetime NULL,
  UpdateUser bigint NULL,
  DeleteUser bigint NULL,
  DeleteDateTime datetime NULL,
  RecordDateTime datetime DEFAULT getdate() NULL
,
  CONSTRAINT AccountTransaction_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountTransaction', NULL, N'Eski tablo: CariHareket (TICARI_SLAVE1). Yeni şema: finance.AccountTransaction'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountTransaction', N'Id', N'Birincil anahtar. | Eski alan: CariHareket.Id'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountTransaction', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: CariHareket.GId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountTransaction', N'CompanyId', N'Bagli sirket. | Eski alan: CariHareket.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountTransaction', N'AccountId', N'Bagli cari (FK -> CariTanim.Id). | Eski alan: CariHareket.CariTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountTransaction', N'TransactionDate', N'Hareket tarihi. | Eski alan: CariHareket.Tarih'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountTransaction', N'DueDate', N'Vade tarihi. | Eski alan: CariHareket.VadeTarihi'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountTransaction', N'DocumentNo', N'Belge/evrak numarasi. | Eski alan: CariHareket.BelgeNo'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountTransaction', N'TransactionType', N'SatisFaturasi=100, AlisFaturasi=200, NakitTahsilat=1, NakitOdeme=2, AlinanCek=102, AlinanSenet=103, BankaDekontTahsilat=11, BankaDekontOdeme=21, VerilenFirmaCeki=202, VerilenMusteriCeki=203, VerilenFirmaSenet=204, VerilenMusteriSenet=205 | Eski alan: CariHareket.HareketTipi'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountTransaction', N'TradeDocumentId', N'Iliskili fatura (FK -> Fatura.Id). | Eski alan: CariHareket.FaturaId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountTransaction', N'Description', N'Hareket aciklamasi. | Eski alan: CariHareket.Aciklama'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountTransaction', N'DebitAmount', N'Borc tutari. | Eski alan: CariHareket.Borc'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountTransaction', N'CreditAmount', N'Alacak tutari. | Eski alan: CariHareket.Alacak'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountTransaction', N'CurrencyCode', N'Para birimi kodu. | Eski alan: CariHareket.DovizKodu'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountTransaction', N'RelatedAccountTransactionId', N'Bagli/karsilik cari hareket ID. | Eski alan: CariHareket.BagliCariHareketId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountTransaction', N'CheckNoteDefId', N'Iliskili cek/senet (FK). | Eski alan: CariHareket.CekSenetTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountTransaction', N'CashRegisterId', N'Iliskili kasa (FK -> KasaTanim.Id). | Eski alan: CariHareket.KasaTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountTransaction', N'Label', N'Etiket. | Eski alan: CariHareket.Etiket'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountTransaction', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: CariHareket.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountTransaction', N'InsertUser', N'Olusturan kullanici. | Eski alan: CariHareket.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountTransaction', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: CariHareket.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountTransaction', N'UpdateUser', N'Guncelleyen kullanici. | Eski alan: CariHareket.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountTransaction', N'DeleteUser', N'Silen kullanici. | Eski alan: CariHareket.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountTransaction', N'DeleteDateTime', N'Silme tarihi. | Eski alan: CariHareket.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'AccountTransaction', N'RecordDateTime', N'Kayit zaman damgasi. | Eski alan: CariHareket.RecDateTime'
GO

--
-- Definition for table CashRegister :
-- Legacy: TICARI_SLAVE1.dbo.KasaTanim
--

CREATE TABLE finance.CashRegister (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  Stat bit DEFAULT 1 NOT NULL,
  CashRegisterCode nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  Description nvarchar(100) COLLATE Turkish_CI_AI NULL,
  StoreId bigint NOT NULL,
  InsertDateTime datetime DEFAULT getdate() NULL,
  InsertUser bigint DEFAULT 0 NULL,
  UpdateDateTime datetime NULL,
  UpdateUser bigint NULL,
  DeleteDateTime datetime NULL,
  DeleteUser bigint NULL,
  RecordDateTime datetime DEFAULT getdate() NULL
,
  CONSTRAINT CashRegister_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegister', NULL, N'Eski tablo: KasaTanim (TICARI_SLAVE1). Yeni şema: finance.CashRegister'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegister', N'Id', N'Birincil anahtar. | Eski alan: KasaTanim.Id'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegister', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: KasaTanim.GId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegister', N'CompanyId', N'Bagli sirket. | Eski alan: KasaTanim.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegister', N'Stat', N'Kasa aktif mi? | Eski alan: KasaTanim.Aktif'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegister', N'CashRegisterCode', N'Kasa kodu. | Eski alan: KasaTanim.KasaKodu'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegister', N'Description', N'Kasa aciklamasi. | Eski alan: KasaTanim.Aciklama'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegister', N'StoreId', N'Bagli depo (FK -> DepoTanim.Id). | Eski alan: KasaTanim.DepoId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegister', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: KasaTanim.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegister', N'InsertUser', N'Olusturan kullanici. | Eski alan: KasaTanim.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegister', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: KasaTanim.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegister', N'UpdateUser', N'Guncelleyen kullanici. | Eski alan: KasaTanim.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegister', N'DeleteDateTime', N'Silme tarihi. | Eski alan: KasaTanim.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegister', N'DeleteUser', N'Silen kullanici. | Eski alan: KasaTanim.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegister', N'RecordDateTime', N'Kayit zaman damgasi. | Eski alan: KasaTanim.RecDateTime'
GO

--
-- Definition for table CashRegisterPersonal :
-- Legacy: TICARI_SLAVE1.dbo.KasaPersonel
--

CREATE TABLE finance.CashRegisterPersonal (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  InsertUser bigint DEFAULT 0 NOT NULL,
  InsertDateTime datetime DEFAULT getdate() NOT NULL,
  UpdateUser bigint NULL,
  UpdateDateTime datetime NULL,
  DeleteUser bigint NULL,
  DeleteDateTime datetime NULL,
  RecordDateTime datetime DEFAULT getdate() NOT NULL,
  CashRegisterId bigint NOT NULL,
  PersonalId bigint NOT NULL
,
  CONSTRAINT CashRegisterPersonal_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegisterPersonal', NULL, N'Eski tablo: KasaPersonel (TICARI_SLAVE1). Yeni şema: finance.CashRegisterPersonal'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegisterPersonal', N'Id', N'Birincil anahtar. | Eski alan: KasaPersonel.Id'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegisterPersonal', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: KasaPersonel.GId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegisterPersonal', N'CompanyId', N'Bagli sirket. | Eski alan: KasaPersonel.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegisterPersonal', N'InsertUser', N'Olusturan kullanici. | Eski alan: KasaPersonel.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegisterPersonal', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: KasaPersonel.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegisterPersonal', N'UpdateUser', N'Guncelleyen kullanici. | Eski alan: KasaPersonel.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegisterPersonal', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: KasaPersonel.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegisterPersonal', N'DeleteUser', N'Silen kullanici. | Eski alan: KasaPersonel.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegisterPersonal', N'DeleteDateTime', N'Silme tarihi. | Eski alan: KasaPersonel.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegisterPersonal', N'RecordDateTime', N'Kayit zaman damgasi. | Eski alan: KasaPersonel.RecDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegisterPersonal', N'CashRegisterId', N'Kasa (FK -> KasaTanim.Id). | Eski alan: KasaPersonel.KasaId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashRegisterPersonal', N'PersonalId', N'Personel (FK -> PersonelTanim.Id). | Eski alan: KasaPersonel.PersonelId'
GO

--
-- Definition for table CashTransaction :
-- Legacy: TICARI_SLAVE1.dbo.KasaHareket
--

CREATE TABLE finance.CashTransaction (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  CashRegisterId bigint NOT NULL,
  AccountTransactionId bigint NOT NULL,
  TransactionDate datetime NOT NULL,
  CashRegisterTransactionType int NOT NULL,
  Description nvarchar(1000) COLLATE Turkish_CI_AI NULL,
  DebitAmount decimal(18,4) NOT NULL,
  CreditAmount decimal(18,4) NOT NULL,
  CurrencyCode nvarchar(10) COLLATE Turkish_CI_AI NOT NULL,
  Label nvarchar(500) COLLATE Turkish_CI_AI NULL,
  InsertDateTime datetime DEFAULT getdate() NOT NULL,
  InsertUser bigint DEFAULT 0 NOT NULL,
  UpdateDateTime datetime NULL,
  UpdateUser bigint NULL,
  DeleteDateTime datetime NULL,
  DeleteUser bigint NULL,
  RecordDateTime datetime DEFAULT getdate() NULL
,
  CONSTRAINT CashTransaction_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashTransaction', NULL, N'Eski tablo: KasaHareket (TICARI_SLAVE1). Yeni şema: finance.CashTransaction'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashTransaction', N'Id', N'Birincil anahtar. | Eski alan: KasaHareket.Id'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashTransaction', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: KasaHareket.GId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashTransaction', N'CompanyId', N'Bagli sirket. | Eski alan: KasaHareket.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashTransaction', N'CashRegisterId', N'Kasa (FK -> KasaTanim.Id). | Eski alan: KasaHareket.KasaTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashTransaction', N'AccountTransactionId', N'Bagli cari hareket (FK -> CariHareket.Id). | Eski alan: KasaHareket.CariHareketId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashTransaction', N'TransactionDate', N'Hareket tarihi. | Eski alan: KasaHareket.Tarih'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashTransaction', N'CashRegisterTransactionType', N'Kasa hareket tipi. | Eski alan: KasaHareket.KasaHareketTipi'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashTransaction', N'Description', N'Hareket aciklamasi. | Eski alan: KasaHareket.Aciklama'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashTransaction', N'DebitAmount', N'Borc tutari. | Eski alan: KasaHareket.Borc'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashTransaction', N'CreditAmount', N'Alacak tutari. | Eski alan: KasaHareket.Alacak'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashTransaction', N'CurrencyCode', N'Para birimi kodu. | Eski alan: KasaHareket.DovizKodu'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashTransaction', N'Label', N'Etiket. | Eski alan: KasaHareket.Etiket'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashTransaction', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: KasaHareket.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashTransaction', N'InsertUser', N'Olusturan kullanici. | Eski alan: KasaHareket.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashTransaction', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: KasaHareket.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashTransaction', N'UpdateUser', N'Guncelleyen kullanici. | Eski alan: KasaHareket.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashTransaction', N'DeleteDateTime', N'Silme tarihi. | Eski alan: KasaHareket.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashTransaction', N'DeleteUser', N'Silen kullanici. | Eski alan: KasaHareket.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CashTransaction', N'RecordDateTime', N'Kayit zaman damgasi. | Eski alan: KasaHareket.RecDateTime'
GO

--
-- Definition for table CheckNote :
-- Legacy: TICARI_SLAVE1.dbo.CekSenetTanim
--

CREATE TABLE finance.CheckNote (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  AccountId bigint NOT NULL,
  KimdeAccountDefId bigint NOT NULL,
  DocumentNo nvarchar(50) COLLATE Turkish_CI_AI NOT NULL,
  DocumentStatus int NOT NULL,
  TransactionDate datetime NOT NULL,
  DueDate datetime NOT NULL,
  NoteType int NOT NULL,
  Amount decimal(18,4) NOT NULL,
  CurrencyCode nvarchar(10) COLLATE Turkish_CI_AI NOT NULL,
  DocumentOriginalOwnerTitle nvarchar(150) COLLATE Turkish_CI_AI NULL,
  TCKN nvarchar(30) COLLATE Turkish_CI_AI NULL,
  OwningBank nvarchar(100) COLLATE Turkish_CI_AI NULL,
  BankBranch nvarchar(100) COLLATE Turkish_CI_AI NULL,
  KesideYeri nvarchar(100) COLLATE Turkish_CI_AI NULL,
  IBAN nvarchar(50) COLLATE Turkish_CI_AI NULL,
  MersisNumber nvarchar(50) COLLATE Turkish_CI_AI NULL,
  FindeksNo nvarchar(50) COLLATE Turkish_CI_AI NULL,
  Label nvarchar(500) COLLATE Turkish_CI_AI NULL,
  InsertDatetime datetime NOT NULL,
  InsertUser bigint DEFAULT 0 NOT NULL,
  UpdateDateTime datetime NULL,
  UpdateUser bigint NULL,
  DeleteDateTime datetime NULL,
  DeleteUser bigint NULL,
  RecordDateTime datetime DEFAULT getdate() NOT NULL
,
  CONSTRAINT CheckNote_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', NULL, N'Eski tablo: CekSenetTanim (TICARI_SLAVE1). Yeni şema: finance.CheckNote'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'Id', N'Birincil anahtar. | Eski alan: CekSenetTanim.Id'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: CekSenetTanim.GId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'CompanyId', N'Bagli sirket. | Eski alan: CekSenetTanim.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'AccountId', N'Cari sahibi (FK -> CariTanim.Id). | Eski alan: CekSenetTanim.CariTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'KimdeAccountDefId', N'Belgenin su an kimde oldugu cari. | Eski alan: CekSenetTanim.KimdeCariTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'DocumentNo', N'Cek/senet numarasi. | Eski alan: CekSenetTanim.BelgeNo'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'DocumentStatus', N'Portfoyde=100, BankadaTahsilde=200, BankadaTeminatta=300, MusteriyeVerildi=400, Cirolandi=500, Karsiliksiz=900, TahsilEdildi=1000 | Eski alan: CekSenetTanim.BelgeDurum'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'TransactionDate', N'Duzenleme tarihi. | Eski alan: CekSenetTanim.Tarih'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'DueDate', N'Vade tarihi. | Eski alan: CekSenetTanim.VadeTarihi'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'NoteType', N'VerilenFirmaCeki=202, VerilenMusteriCeki=203, VerilenFirmaSenet=204, VerilenMusteriSenet=205 | Eski alan: CekSenetTanim.HareketTipi'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'Amount', N'Tutar. | Eski alan: CekSenetTanim.Tutar'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'CurrencyCode', N'Para birimi. | Eski alan: CekSenetTanim.DovizKodu'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'DocumentOriginalOwnerTitle', N'Belgenin ilk sahibi unvani. | Eski alan: CekSenetTanim.BelgeIlkSahibiUnvan'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'TCKN', N'TC/VKN. | Eski alan: CekSenetTanim.TCKN'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'OwningBank', N'Ait oldugu banka. | Eski alan: CekSenetTanim.AitOlduguBanka'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'BankBranch', N'Banka subesi. | Eski alan: CekSenetTanim.BankaSubesi'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'KesideYeri', N'Keside yeri. | Eski alan: CekSenetTanim.KesideYeri'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'IBAN', N'IBAN. | Eski alan: CekSenetTanim.IBAN'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'MersisNumber', N'MERSIS no. | Eski alan: CekSenetTanim.MersisNo'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'FindeksNo', N'Findeks no. | Eski alan: CekSenetTanim.FindeksNo'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'Label', N'Etiket. | Eski alan: CekSenetTanim.Etiket'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'InsertDatetime', N'Olusturma tarihi. | Eski alan: CekSenetTanim.InsertDatetime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'InsertUser', N'Olusturan kullanici. | Eski alan: CekSenetTanim.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: CekSenetTanim.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'UpdateUser', N'Guncelleyen kullanici. | Eski alan: CekSenetTanim.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'DeleteDateTime', N'Silme tarihi. | Eski alan: CekSenetTanim.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'DeleteUser', N'Silen kullanici. | Eski alan: CekSenetTanim.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNote', N'RecordDateTime', N'Kayit zaman damgasi. | Eski alan: CekSenetTanim.RecDateTime'
GO

--
-- Definition for table CheckNoteTransaction :
-- Legacy: TICARI_SLAVE1.dbo.CekSenetHareket
--

CREATE TABLE finance.CheckNoteTransaction (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  TransactionDate datetime NOT NULL,
  CompanyId bigint NOT NULL,
  CheckNoteId bigint NOT NULL,
  DocumentStatus int NOT NULL,
  AccountId bigint NOT NULL,
  AccountTransactionId bigint NOT NULL,
  CashRegisterTransactionId bigint NOT NULL,
  Label nvarchar(500) COLLATE Turkish_CI_AI NULL,
  InsertDateTime datetime DEFAULT getdate() NOT NULL,
  InsertUser bigint DEFAULT 0 NOT NULL,
  UpdateDateTime datetime NULL,
  UpdateUser bigint NULL,
  DeleteDateTime datetime NULL,
  DeleteUser bigint NULL,
  RecordDateTime datetime DEFAULT getdate() NULL
,
  CONSTRAINT CheckNoteTransaction_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNoteTransaction', NULL, N'Eski tablo: CekSenetHareket (TICARI_SLAVE1). Yeni şema: finance.CheckNoteTransaction'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNoteTransaction', N'Id', N'Birincil anahtar. | Eski alan: CekSenetHareket.Id'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNoteTransaction', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: CekSenetHareket.GId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNoteTransaction', N'TransactionDate', N'Hareket tarihi. | Eski alan: CekSenetHareket.HareketTarihi'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNoteTransaction', N'CompanyId', N'Bagli sirket. | Eski alan: CekSenetHareket.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNoteTransaction', N'CheckNoteId', N'Bagli cek/senet (FK). | Eski alan: CekSenetHareket.CekSenetId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNoteTransaction', N'DocumentStatus', N'Yeni belge durumu. | Eski alan: CekSenetHareket.BelgeDurum'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNoteTransaction', N'AccountId', N'Iliskili cari. | Eski alan: CekSenetHareket.CariTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNoteTransaction', N'AccountTransactionId', N'Iliskili cari hareket. | Eski alan: CekSenetHareket.CariHareketId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNoteTransaction', N'CashRegisterTransactionId', N'Iliskili kasa hareket. | Eski alan: CekSenetHareket.KasaHareketId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNoteTransaction', N'Label', N'Etiket. | Eski alan: CekSenetHareket.Etiket'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNoteTransaction', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: CekSenetHareket.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNoteTransaction', N'InsertUser', N'Olusturan kullanici. | Eski alan: CekSenetHareket.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNoteTransaction', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: CekSenetHareket.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNoteTransaction', N'UpdateUser', N'Guncelleyen kullanici. | Eski alan: CekSenetHareket.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNoteTransaction', N'DeleteDateTime', N'Silme tarihi. | Eski alan: CekSenetHareket.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNoteTransaction', N'DeleteUser', N'Silen kullanici. | Eski alan: CekSenetHareket.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CheckNoteTransaction', N'RecordDateTime', N'Kayit zaman damgasi. | Eski alan: CekSenetHareket.RecDateTime'
GO

--
-- Definition for table Currency :
-- Legacy: TICARI_SLAVE1.dbo.DovizTanim
--

CREATE TABLE finance.Currency (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  SortOrder int NOT NULL,
  CurrencyCode nvarchar(10) COLLATE Turkish_CI_AI NOT NULL,
  DecimalPrecision int NOT NULL,
  InsertUser bigint DEFAULT 0 NOT NULL,
  InsertDateTime datetime DEFAULT getdate() NOT NULL,
  UpdateUser bigint NULL,
  UpdateDateTime datetime NULL,
  DeleteUser bigint NULL,
  DeleteDateTime datetime NULL,
  RecordDateTime datetime DEFAULT getdate() NOT NULL
,
  CONSTRAINT Currency_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Currency', NULL, N'Eski tablo: DovizTanim (TICARI_SLAVE1). Yeni şema: finance.Currency'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Currency', N'Id', N'Birincil anahtar. | Eski alan: DovizTanim.Id'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Currency', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: DovizTanim.GId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Currency', N'CompanyId', N'Bagli sirket. | Eski alan: DovizTanim.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Currency', N'SortOrder', N'Listeleme sirasi. | Eski alan: DovizTanim.Sira'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Currency', N'CurrencyCode', N'Doviz kodu (USD, EUR vb.). | Eski alan: DovizTanim.DovizKodu'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Currency', N'DecimalPrecision', N'Ondalik hassasiyet. | Eski alan: DovizTanim.DecimalPrecision'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Currency', N'InsertUser', N'Olusturan kullanici. | Eski alan: DovizTanim.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Currency', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: DovizTanim.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Currency', N'UpdateUser', N'Guncelleyen kullanici. | Eski alan: DovizTanim.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Currency', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: DovizTanim.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Currency', N'DeleteUser', N'Silen kullanici. | Eski alan: DovizTanim.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Currency', N'DeleteDateTime', N'Silme tarihi. | Eski alan: DovizTanim.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'Currency', N'RecordDateTime', N'Kayit zaman damgasi. | Eski alan: DovizTanim.RecDateTime'
GO

--
-- Definition for table CurrencyRate :
-- Legacy: TICARI_SLAVE1.dbo.DovizKur
--

CREATE TABLE finance.CurrencyRate (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  TransactionDate datetime NOT NULL,
  CurrencyCode nvarchar(10) COLLATE Turkish_CI_AI NOT NULL,
  Value decimal(18,4) NOT NULL,
  DefaultCurrencyCode nvarchar(10) COLLATE Turkish_CI_AI NOT NULL,
  InsertUser bigint DEFAULT 0 NOT NULL,
  InsertDateTime datetime DEFAULT getdate() NOT NULL,
  UpdateUser bigint NULL,
  UpdateDateTime datetime NULL,
  DeleteUser bigint NULL,
  DeleteDateTime datetime NULL,
  RecordDateTime datetime DEFAULT getdate() NOT NULL
,
  CONSTRAINT CurrencyRate_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CurrencyRate', NULL, N'Eski tablo: DovizKur (TICARI_SLAVE1). Yeni şema: finance.CurrencyRate'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CurrencyRate', N'Id', N'Birincil anahtar. | Eski alan: DovizKur.Id'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CurrencyRate', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: DovizKur.GId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CurrencyRate', N'CompanyId', N'Bagli sirket. | Eski alan: DovizKur.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CurrencyRate', N'TransactionDate', N'Kur tarihi. | Eski alan: DovizKur.Tarih'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CurrencyRate', N'CurrencyCode', N'Doviz kodu. | Eski alan: DovizKur.DovizKodu'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CurrencyRate', N'Value', N'Kur degeri. | Eski alan: DovizKur.Deger'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CurrencyRate', N'DefaultCurrencyCode', N'Karsilastirma para birimi (genelde TRY). | Eski alan: DovizKur.StandartDovizKodu'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CurrencyRate', N'InsertUser', N'Olusturan kullanici. | Eski alan: DovizKur.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CurrencyRate', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: DovizKur.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CurrencyRate', N'UpdateUser', N'Guncelleyen kullanici. | Eski alan: DovizKur.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CurrencyRate', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: DovizKur.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CurrencyRate', N'DeleteUser', N'Silen kullanici. | Eski alan: DovizKur.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CurrencyRate', N'DeleteDateTime', N'Silme tarihi. | Eski alan: DovizKur.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'finance', N'CurrencyRate', N'RecordDateTime', N'Kayit zaman damgasi. | Eski alan: DovizKur.RecDateTime'
GO

--
-- Definition for table Brand :
-- Legacy: TICARI_SLAVE1.dbo.MarkaTanim
--

CREATE TABLE inventory.Brand (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  Stat bit DEFAULT 1 NOT NULL,
  BrandCode nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  Description nvarchar(100) COLLATE Turkish_CI_AI NULL,
  TrendyolId bigint NULL,
  TrendyolDescription nvarchar(100) COLLATE Turkish_CI_AI NULL,
  InsertUser bigint DEFAULT 0 NOT NULL,
  InsertDateTime datetime DEFAULT getdate() NOT NULL,
  UpdateUser bigint NULL,
  UpdateDateTime datetime NULL,
  DeleteUser bigint NULL,
  DeleteDateTime datetime NULL,
  RecordDateTime datetime DEFAULT getdate() NOT NULL
,
  CONSTRAINT Brand_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Brand', NULL, N'Eski tablo: MarkaTanim (TICARI_SLAVE1). Yeni şema: inventory.Brand'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Brand', N'Id', N'Birincil anahtar. | Eski alan: MarkaTanim.Id'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Brand', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: MarkaTanim.GId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Brand', N'CompanyId', N'Bagli sirket. | Eski alan: MarkaTanim.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Brand', N'Stat', N'Aktif mi? | Eski alan: MarkaTanim.Aktif'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Brand', N'BrandCode', N'Marka kodu. | Eski alan: MarkaTanim.MarkaKodu'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Brand', N'Description', N'Marka aciklamasi. | Eski alan: MarkaTanim.Aciklama'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Brand', N'TrendyolId', N'Trendyol marka ID. | Eski alan: MarkaTanim.TrendyolId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Brand', N'TrendyolDescription', N'Trendyol marka aciklamasi. | Eski alan: MarkaTanim.TrendyolAciklama'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Brand', N'InsertUser', N'Olusturan kullanici. | Eski alan: MarkaTanim.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Brand', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: MarkaTanim.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Brand', N'UpdateUser', N'Guncelleyen kullanici. | Eski alan: MarkaTanim.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Brand', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: MarkaTanim.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Brand', N'DeleteUser', N'Silen kullanici. | Eski alan: MarkaTanim.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Brand', N'DeleteDateTime', N'Silme tarihi. | Eski alan: MarkaTanim.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Brand', N'RecordDateTime', N'Kayit zaman damgasi. | Eski alan: MarkaTanim.RecDateTime'
GO

--
-- Definition for table Product :
-- Legacy: TICARI_SLAVE1.dbo.UrunTanim
--

CREATE TABLE inventory.Product (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  Stat bit DEFAULT 1 NOT NULL,
  ProductCode nvarchar(50) COLLATE Turkish_CI_AI NOT NULL,
  OzelKod nvarchar(50) COLLATE Turkish_CI_AI NULL,
  BrandId bigint NOT NULL,
  ProductDescription nvarchar(300) COLLATE Turkish_CI_AI NOT NULL,
  ProductKisaBilgi nvarchar(300) COLLATE Turkish_CI_AI NULL,
  Barcode nvarchar(50) COLLATE Turkish_CI_AI NULL,
  ColorId bigint NULL,
  IsWeightBarcode bit NOT NULL,
  UnitId bigint NOT NULL,
  CategoryId bigint NULL,
  CriticalStockQuantity decimal(18,4) NULL,
  InternetCriticalStockQuantity decimal(18,4) NULL,
  SerialCodeNoTakip bit NOT NULL,
  PurchaseVatRate int NOT NULL,
  VatRate int NOT NULL,
  PurchasePrice decimal(18,4) NOT NULL,
  PurchaseCurrencyCode nvarchar(10) COLLATE Turkish_CI_AI NOT NULL,
  PurchaseVatIncluded nvarchar(1) COLLATE Turkish_CI_AI NOT NULL,
  PreviousSalePrice decimal(18,4) NOT NULL,
  SalePrice decimal(18,4) NOT NULL,
  InstallmentSalePrice decimal(18,4) NOT NULL,
  SaleCurrencyCode nvarchar(10) COLLATE Turkish_CI_AI NOT NULL,
  SaleVatIncluded nvarchar(1) COLLATE Turkish_CI_AI NOT NULL,
  ProductDetay nvarchar(max) COLLATE Turkish_CI_AI NULL,
  VideoUrl nvarchar(500) COLLATE Turkish_CI_AI NULL,
  IntegrationName nvarchar(30) COLLATE Turkish_CI_AI NULL,
  IntegrationId bigint NULL,
  IsInternetSaleActive bit NOT NULL,
  IsTrendyolActive bit NOT NULL,
  IsHepsiBuradaActive bit NOT NULL,
  Width int NOT NULL,
  Height int NOT NULL,
  Depth int NOT NULL,
  DesiWeight decimal(18,4) NOT NULL,
  Weight decimal(18,4) NOT NULL,
  Color nvarchar(30) COLLATE Turkish_CI_AI NULL,
  IsAppointmentActive bit NULL,
  IsAppointmentOpen bit NULL,
  TrendyolSyncType nvarchar(1) COLLATE Turkish_CI_AI NULL,
  HepsiBuradaSyncType nvarchar(1) COLLATE Turkish_CI_AI NULL,
  InsertDateTime datetime DEFAULT getdate() NULL,
  InsertUser bigint DEFAULT 0 NULL,
  UpdateDateTime datetime NULL,
  UpdateUser bigint NULL,
  DeleteDateTime datetime NULL,
  DeleteUser bigint NULL,
  RecordDateTime datetime DEFAULT getdate() NULL,
  HepsiBuradaProductId nvarchar(50) COLLATE Turkish_CI_AI NULL,
  MinSaleQuantity int NOT NULL
,
  CONSTRAINT Product_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', NULL, N'Eski tablo: UrunTanim (TICARI_SLAVE1). Yeni şema: inventory.Product'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'Id', N'Birincil anahtar. | Eski alan: UrunTanim.Id'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: UrunTanim.GId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'CompanyId', N'Bagli sirket. | Eski alan: UrunTanim.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'Stat', N'Urun aktif mi? | Eski alan: UrunTanim.Aktif'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'ProductCode', N'Urun kodu. | Eski alan: UrunTanim.UrunKodu'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'OzelKod', N'Ozel kod. | Eski alan: UrunTanim.OzelKod'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'BrandId', N'Marka (FK -> MarkaTanim.Id). | Eski alan: UrunTanim.MarkaTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'ProductDescription', N'Urun aciklamasi. | Eski alan: UrunTanim.UrunAciklama'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'ProductKisaBilgi', N'Kisa bilgi/ozet. | Eski alan: UrunTanim.UrunKisaBilgi'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'Barcode', N'Ana barkod. | Eski alan: UrunTanim.Barkod'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'ColorId', N'Renk (FK -> UrunRenkPaleti.Id). | Eski alan: UrunTanim.RenkId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'IsWeightBarcode', N'Weight barkodu mu? | Eski alan: UrunTanim.KgBarkod'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'UnitId', N'Ana birim (FK -> BirimTanim.Id). | Eski alan: UrunTanim.BirimId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'CategoryId', N'Kategori/beden (FK -> KategoriTanim.Id). | Eski alan: UrunTanim.KategoriTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'CriticalStockQuantity', N'Kritik stok esik miktari. | Eski alan: UrunTanim.KritikStokMiktari'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'InternetCriticalStockQuantity', N'E-ticaret kritik stok esigi. | Eski alan: UrunTanim.InternetKritikStokMiktari'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'SerialCodeNoTakip', N'SerialCode numarasi takibi yapilsin mi? | Eski alan: UrunTanim.SerialCodeNoTakip'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'PurchaseVatRate', N'Alis KDV orani (%). | Eski alan: UrunTanim.AlisKdvOrani'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'VatRate', N'Satis KDV orani (%). | Eski alan: UrunTanim.KdvOrani'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'PurchasePrice', N'Alis fiyati. | Eski alan: UrunTanim.AlisFiyati'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'PurchaseCurrencyCode', N'Alis doviz kodu. | Eski alan: UrunTanim.AlisDovizKodu'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'PurchaseVatIncluded', N'Alis KDV durumu: D=Dahil, H=Haric. | Eski alan: UrunTanim.AlisKdvDH'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'PreviousSalePrice', N'Onceki satis fiyati (indirim hesabi). | Eski alan: UrunTanim.OncekiSatisFiyati'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'SalePrice', N'Satis fiyati. | Eski alan: UrunTanim.SatisFiyati'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'InstallmentSalePrice', N'Taksitli satis fiyati. | Eski alan: UrunTanim.TaksitliSatisFiyati'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'SaleCurrencyCode', N'Satis doviz kodu. | Eski alan: UrunTanim.SatisDovizKodu'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'SaleVatIncluded', N'Satis KDV durumu: D=Dahil, H=Haric. | Eski alan: UrunTanim.SatisKdvDH'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'ProductDetay', N'Urun detay HTML/text. | Eski alan: UrunTanim.UrunDetay'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'VideoUrl', N'Urun video URL. | Eski alan: UrunTanim.VideoUrl'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'IntegrationName', N'Harici entegrasyon adi. | Eski alan: UrunTanim.EntegrasyonAdi'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'IntegrationId', N'Harici entegrasyon urun ID. | Eski alan: UrunTanim.EntegrasyonId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'IsInternetSaleActive', N'E-ticarette satisa acik mi? | Eski alan: UrunTanim.InternetSatisAktif'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'IsTrendyolActive', N'Trendyol entegrasyonu aktif mi? | Eski alan: UrunTanim.TrendyolAktif'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'IsHepsiBuradaActive', N'Hepsiburada entegrasyonu aktif mi? | Eski alan: UrunTanim.HepsiburadaAktif'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'Width', N'Urun genisligi (cm). | Eski alan: UrunTanim.Genislik'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'Height', N'Urun yuksekligi (cm). | Eski alan: UrunTanim.Yukseklik'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'Depth', N'Urun derinligi (cm). | Eski alan: UrunTanim.Depth'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'DesiWeight', N'DesiWeight degeri (kargo). | Eski alan: UrunTanim.DesiWeight'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'Weight', N'Weight (kg). | Eski alan: UrunTanim.Weight'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'Color', N'Randevu takvim renk kodu. | Eski alan: UrunTanim.Renk'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'IsAppointmentActive', N'Randevu modulu aktif mi? | Eski alan: UrunTanim.RandevuAktif'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'IsAppointmentOpen', N'Randevu almaya acik mi? | Eski alan: UrunTanim.RandevuAcik'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'TrendyolSyncType', N'Trendyol senkron tipi (I/U/D). | Eski alan: UrunTanim.TransTypeTrendyol'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'HepsiBuradaSyncType', N'Hepsiburada senkron tipi. | Eski alan: UrunTanim.TransTypeHepsiburada'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: UrunTanim.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'InsertUser', N'Olusturan kullanici. | Eski alan: UrunTanim.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: UrunTanim.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'UpdateUser', N'Guncelleyen kullanici. | Eski alan: UrunTanim.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'DeleteDateTime', N'Silme tarihi. | Eski alan: UrunTanim.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'DeleteUser', N'Silen kullanici. | Eski alan: UrunTanim.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'RecordDateTime', N'Kayit zaman damgasi. | Eski alan: UrunTanim.RecDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'HepsiBuradaProductId', N'Hepsiburada urun ID. | Eski alan: UrunTanim.HepsiBuradaUrunId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Product', N'MinSaleQuantity', N'Minimum satis miktari. | Eski alan: UrunTanim.MinSatisMiktar'
GO

--
-- Definition for table ProductBarcode :
-- Legacy: TICARI_SLAVE1.dbo.UrunBarkod
--

CREATE TABLE inventory.ProductBarcode (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  ProductId bigint NOT NULL,
  SortOrder int NOT NULL,
  IsMainProduct bit NOT NULL,
  ColorSize varchar(50) COLLATE Turkish_CI_AI NULL,
  Size nvarchar(50) COLLATE Turkish_CI_AI NULL,
  Color nvarchar(50) COLLATE Turkish_CI_AI NULL,
  ProductColorPaletteId bigint NULL,
  Barcode nvarchar(50) COLLATE Turkish_CI_AI NULL,
  IsSalePriceActive bit NOT NULL,
  SalePrice numeric(18,4) NOT NULL,
  InstallmentSalePrice decimal(18,4) NOT NULL,
  PreviousSalePrice decimal(18,4) NULL,
  InsertUser bigint DEFAULT 0 NOT NULL,
  InsertDateTime datetime DEFAULT getdate() NOT NULL,
  UpdateUser bigint NULL,
  UpdateDateTime datetime NULL,
  DeleteUser bigint NULL,
  DeleteDateTime datetime NULL,
  RecordDateTime datetime DEFAULT getdate() NOT NULL,
  HepsiBuradaProductId nvarchar(50) COLLATE Turkish_CI_AI NULL
,
  CONSTRAINT ProductBarcode_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductBarcode', NULL, N'Eski tablo: UrunBarkod (TICARI_SLAVE1). Yeni şema: inventory.ProductBarcode'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductBarcode', N'Id', N'- | Eski alan: UrunBarkod.Id'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductBarcode', N'GId', N'- | Eski alan: UrunBarkod.GId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductBarcode', N'CompanyId', N'- | Eski alan: UrunBarkod.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductBarcode', N'ProductId', N'- | Eski alan: UrunBarkod.UrunTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductBarcode', N'SortOrder', N'- | Eski alan: UrunBarkod.Sira'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductBarcode', N'IsMainProduct', N'- | Eski alan: UrunBarkod.AnaUrun'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductBarcode', N'ColorSize', N'- | Eski alan: UrunBarkod.RenkBeden'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductBarcode', N'Size', N'- | Eski alan: UrunBarkod.Beden'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductBarcode', N'Color', N'- | Eski alan: UrunBarkod.Renk'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductBarcode', N'ProductColorPaletteId', N'- | Eski alan: UrunBarkod.UrunRenkPaletiId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductBarcode', N'Barcode', N'- | Eski alan: UrunBarkod.Barkod'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductBarcode', N'IsSalePriceActive', N'- | Eski alan: UrunBarkod.SatisFiyatAktif'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductBarcode', N'SalePrice', N'- | Eski alan: UrunBarkod.SatisFiyati'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductBarcode', N'InstallmentSalePrice', N'- | Eski alan: UrunBarkod.TaksitliSatisFiyati'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductBarcode', N'PreviousSalePrice', N'- | Eski alan: UrunBarkod.OncekiSatisFiyati'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductBarcode', N'InsertUser', N'- | Eski alan: UrunBarkod.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductBarcode', N'InsertDateTime', N'- | Eski alan: UrunBarkod.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductBarcode', N'UpdateUser', N'- | Eski alan: UrunBarkod.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductBarcode', N'UpdateDateTime', N'- | Eski alan: UrunBarkod.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductBarcode', N'DeleteUser', N'- | Eski alan: UrunBarkod.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductBarcode', N'DeleteDateTime', N'- | Eski alan: UrunBarkod.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductBarcode', N'RecordDateTime', N'- | Eski alan: UrunBarkod.RecDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductBarcode', N'HepsiBuradaProductId', N'- | Eski alan: UrunBarkod.HepsiBuradaUrunId'
GO

--
-- Definition for table ProductColorPalette :
-- Legacy: TICARI_SLAVE1.dbo.UrunRenkPaleti
--

CREATE TABLE inventory.ProductColorPalette (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  Stat bit DEFAULT 1 NOT NULL,
  SortOrder int NOT NULL,
  ColorCode varchar(30) COLLATE Turkish_CI_AI NOT NULL,
  ColorDescription nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  InsertUser bigint DEFAULT 0 NOT NULL,
  InsertDateTime datetime DEFAULT getdate() NOT NULL,
  UpdateUser bigint NULL,
  UpdateDateTime datetime NULL,
  DeleteUser bigint NULL,
  DeleteDateTime datetime NULL,
  RecordDateTime datetime DEFAULT getdate() NOT NULL
,
  CONSTRAINT ProductColorPalette_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductColorPalette', NULL, N'Eski tablo: UrunRenkPaleti (TICARI_SLAVE1). Yeni şema: inventory.ProductColorPalette'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductColorPalette', N'Id', N'Birincil anahtar. | Eski alan: UrunRenkPaleti.Id'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductColorPalette', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: UrunRenkPaleti.GId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductColorPalette', N'CompanyId', N'Bagli sirket. | Eski alan: UrunRenkPaleti.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductColorPalette', N'Stat', N'Aktif mi? | Eski alan: UrunRenkPaleti.Aktif'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductColorPalette', N'SortOrder', N'Listeleme sirasi. | Eski alan: UrunRenkPaleti.Sira'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductColorPalette', N'ColorCode', N'Renk kodu (hex veya kisa kod). | Eski alan: UrunRenkPaleti.RenkKodu'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductColorPalette', N'ColorDescription', N'Renk aciklamasi. | Eski alan: UrunRenkPaleti.RenkAciklama'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductColorPalette', N'InsertUser', N'Olusturan kullanici. | Eski alan: UrunRenkPaleti.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductColorPalette', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: UrunRenkPaleti.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductColorPalette', N'UpdateUser', N'Guncelleyen kullanici. | Eski alan: UrunRenkPaleti.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductColorPalette', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: UrunRenkPaleti.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductColorPalette', N'DeleteUser', N'Silen kullanici. | Eski alan: UrunRenkPaleti.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductColorPalette', N'DeleteDateTime', N'Silme tarihi. | Eski alan: UrunRenkPaleti.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductColorPalette', N'RecordDateTime', N'Kayit zaman damgasi. | Eski alan: UrunRenkPaleti.RecDateTime'
GO

--
-- Definition for table ProductUnit :
-- Legacy: TICARI_SLAVE1.dbo.UrunBirim
--

CREATE TABLE inventory.ProductUnit (
  Id bigint NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  Stat bit DEFAULT 1 NOT NULL,
  ProductId bigint NOT NULL,
  UnitId bigint NOT NULL,
  Barcode nvarchar(50) COLLATE Turkish_CI_AI NULL,
  Carpan float NOT NULL,
  DecimalPrecision int NOT NULL,
  Label nvarchar(500) COLLATE Turkish_CI_AI NULL,
  InsertDateTime datetime DEFAULT getdate() NULL,
  InsertUser bigint DEFAULT 0 NULL,
  UpdateDateTime datetime NULL,
  UpdateUser bigint NULL,
  DeleteDateTime datetime NULL,
  DeleteUser bigint NULL,
  RecordDateTime datetime DEFAULT getdate() NULL
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductUnit', NULL, N'Eski tablo: UrunBirim (TICARI_SLAVE1). Yeni şema: inventory.ProductUnit'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductUnit', N'Id', N'- | Eski alan: UrunBirim.Id'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductUnit', N'GId', N'- | Eski alan: UrunBirim.GId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductUnit', N'CompanyId', N'- | Eski alan: UrunBirim.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductUnit', N'Stat', N'- | Eski alan: UrunBirim.Aktif'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductUnit', N'ProductId', N'- | Eski alan: UrunBirim.UrunId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductUnit', N'UnitId', N'- | Eski alan: UrunBirim.BirimId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductUnit', N'Barcode', N'- | Eski alan: UrunBirim.Barkod'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductUnit', N'Carpan', N'- | Eski alan: UrunBirim.Carpan'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductUnit', N'DecimalPrecision', N'- | Eski alan: UrunBirim.DecimalPrecision'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductUnit', N'Label', N'- | Eski alan: UrunBirim.Etiket'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductUnit', N'InsertDateTime', N'- | Eski alan: UrunBirim.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductUnit', N'InsertUser', N'- | Eski alan: UrunBirim.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductUnit', N'UpdateDateTime', N'- | Eski alan: UrunBirim.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductUnit', N'UpdateUser', N'- | Eski alan: UrunBirim.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductUnit', N'DeleteDateTime', N'- | Eski alan: UrunBirim.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductUnit', N'DeleteUser', N'- | Eski alan: UrunBirim.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'ProductUnit', N'RecordDateTime', N'- | Eski alan: UrunBirim.RecDateTime'
GO

--
-- Definition for table StockCount :
-- Legacy: TICARI_SLAVE1.dbo.SayimTanim
--

CREATE TABLE inventory.StockCount (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  InsertUser bigint DEFAULT 0 NOT NULL,
  InsertDateTime datetime DEFAULT getdate() NOT NULL,
  UpdateUser bigint NULL,
  UpdateDateTime datetime NULL,
  DeleteUser bigint NULL,
  DeleteDateTime datetime NULL,
  RecordDateTime datetime DEFAULT getdate() NOT NULL,
  StockCountDate datetime NOT NULL,
  StockCountDescription nvarchar(30) COLLATE Turkish_CI_AI NULL,
  StockCountStatus int NOT NULL,
  StoreId bigint NOT NULL,
  InboundTradeDocumentId bigint NOT NULL,
  OutboundTradeDocumentId bigint NOT NULL
,
  CONSTRAINT StockCount_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCount', NULL, N'Eski tablo: SayimTanim (TICARI_SLAVE1). Yeni şema: inventory.StockCount'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCount', N'Id', N'Birincil anahtar. | Eski alan: SayimTanim.Id'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCount', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: SayimTanim.GId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCount', N'CompanyId', N'Bagli sirket. | Eski alan: SayimTanim.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCount', N'InsertUser', N'Olusturan kullanici. | Eski alan: SayimTanim.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCount', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: SayimTanim.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCount', N'UpdateUser', N'Guncelleyen kullanici. | Eski alan: SayimTanim.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCount', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: SayimTanim.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCount', N'DeleteUser', N'Silen kullanici. | Eski alan: SayimTanim.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCount', N'DeleteDateTime', N'Silme tarihi. | Eski alan: SayimTanim.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCount', N'RecordDateTime', N'Kayit zaman damgasi. | Eski alan: SayimTanim.RecDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCount', N'StockCountDate', N'Sayim tarihi. | Eski alan: SayimTanim.SayimTarihi'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCount', N'StockCountDescription', N'Sayim aciklamasi. | Eski alan: SayimTanim.SayimAciklama'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCount', N'StockCountStatus', N'- | Eski alan: SayimTanim.SayimDurumu'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCount', N'StoreId', N'Sayim yapilan depo (FK -> DepoTanim.Id). | Eski alan: SayimTanim.DepoTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCount', N'InboundTradeDocumentId', N'Sayim farki giris faturasi (FK -> Fatura.Id). | Eski alan: SayimTanim.GirisFaturaId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCount', N'OutboundTradeDocumentId', N'Sayim farki cikis faturasi (FK -> Fatura.Id). | Eski alan: SayimTanim.CikisFaturaId'
GO

--
-- Definition for table StockCountLine :
-- Legacy: TICARI_SLAVE1.dbo.SayimHareket
--

CREATE TABLE inventory.StockCountLine (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  InsertUser bigint DEFAULT 0 NOT NULL,
  InsertDateTime datetime DEFAULT getdate() NOT NULL,
  UpdateUser bigint NULL,
  UpdateDateTime datetime NULL,
  DeleteUser bigint NULL,
  DeleteDateTime datetime NULL,
  RecordDateTime datetime DEFAULT getdate() NOT NULL,
  StockCountId bigint NOT NULL,
  ProductId bigint NOT NULL,
  Color nvarchar(50) COLLATE Turkish_CI_AI NULL,
  Barcode nvarchar(50) COLLATE Turkish_CI_AI NULL,
  Quantity decimal(18,4) NOT NULL,
  TradeDocumentLineId bigint NOT NULL
,
  CONSTRAINT StockCountLine_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCountLine', NULL, N'Eski tablo: SayimHareket (TICARI_SLAVE1). Yeni şema: inventory.StockCountLine'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCountLine', N'Id', N'Birincil anahtar. | Eski alan: SayimHareket.Id'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCountLine', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: SayimHareket.GId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCountLine', N'CompanyId', N'Bagli sirket. | Eski alan: SayimHareket.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCountLine', N'InsertUser', N'Olusturan kullanici. | Eski alan: SayimHareket.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCountLine', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: SayimHareket.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCountLine', N'UpdateUser', N'Guncelleyen kullanici. | Eski alan: SayimHareket.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCountLine', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: SayimHareket.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCountLine', N'DeleteUser', N'Silen kullanici. | Eski alan: SayimHareket.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCountLine', N'DeleteDateTime', N'Silme tarihi. | Eski alan: SayimHareket.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCountLine', N'RecordDateTime', N'Kayit zaman damgasi. | Eski alan: SayimHareket.RecDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCountLine', N'StockCountId', N'Sayim (FK -> SayimTanim.Id). | Eski alan: SayimHareket.SayimId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCountLine', N'ProductId', N'Urun (FK -> UrunTanim.Id). | Eski alan: SayimHareket.UrunTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCountLine', N'Color', N'Renk bilgisi. | Eski alan: SayimHareket.Renk'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCountLine', N'Barcode', N'Barkod. | Eski alan: SayimHareket.Barkod'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCountLine', N'Quantity', N'Sayilan miktar. | Eski alan: SayimHareket.Miktar'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StockCountLine', N'TradeDocumentLineId', N'Iliskili fatura satiri (FK -> FaturaHareket.Id). | Eski alan: SayimHareket.FaturaHareketId'
GO

--
-- Definition for table Store :
-- Legacy: TICARI_SLAVE1.dbo.DepoTanim
--

CREATE TABLE inventory.Store (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  Stat bit DEFAULT 1 NOT NULL,
  IsEcommerceSaleActive bit NOT NULL,
  IsMainStore bit NOT NULL,
  StoreCode nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  Description nvarchar(100) COLLATE Turkish_CI_AI NULL,
  AuthorizedPerson nvarchar(100) COLLATE Turkish_CI_AI NULL,
  Label nvarchar(MAX) COLLATE Turkish_CI_AI NULL,
  Address nvarchar(1000) COLLATE Turkish_CI_AI NULL,
  City nvarchar(50) COLLATE Turkish_CI_AI NULL,
  District nvarchar(50) COLLATE Turkish_CI_AI NULL,
  Phone1 nvarchar(30) COLLATE Turkish_CI_AI NULL,
  Phone2 nvarchar(30) COLLATE Turkish_CI_AI NULL,
  Fax nvarchar(30) COLLATE Turkish_CI_AI NULL,
  InsertDateTime datetime DEFAULT getdate() NULL,
  InsertUser bigint DEFAULT 0 NULL,
  UpdateDateTime datetime NULL,
  UpdateUser bigint NULL,
  DeleteDateTime datetime NULL,
  DeleteUser bigint NULL,
  RecordDateTime datetime DEFAULT getdate() NULL
,
  CONSTRAINT Store_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Store', NULL, N'Eski tablo: DepoTanim (TICARI_SLAVE1). Yeni şema: inventory.Store'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Store', N'Id', N'Birincil anahtar. | Eski alan: DepoTanim.Id'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Store', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: DepoTanim.GId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Store', N'CompanyId', N'Bagli sirket. | Eski alan: DepoTanim.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Store', N'Stat', N'Depo aktif mi? | Eski alan: DepoTanim.Aktif'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Store', N'IsEcommerceSaleActive', N'E-ticaret satisinda stok gosterilsin mi? | Eski alan: DepoTanim.ETicaretSatisAktif'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Store', N'IsMainStore', N'Ana depo mu? | Eski alan: DepoTanim.AnaDepo'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Store', N'StoreCode', N'Depo kodu. | Eski alan: DepoTanim.DepoKodu'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Store', N'Description', N'Depo aciklamasi. | Eski alan: DepoTanim.Aciklama'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Store', N'AuthorizedPerson', N'Depo yetkilisi. | Eski alan: DepoTanim.Yetkili'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Store', N'Label', N'Etiket. | Eski alan: DepoTanim.Etiket'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Store', N'Address', N'Adres. | Eski alan: DepoTanim.Adres'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Store', N'City', N'Il. | Eski alan: DepoTanim.Il'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Store', N'District', N'Ilce. | Eski alan: DepoTanim.Ilce'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Store', N'Phone1', N'Birincil telefon. | Eski alan: DepoTanim.Telefon1'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Store', N'Phone2', N'Ikincil telefon. | Eski alan: DepoTanim.Telefon2'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Store', N'Fax', N'Faks. | Eski alan: DepoTanim.Faks'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Store', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: DepoTanim.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Store', N'InsertUser', N'Olusturan kullanici. | Eski alan: DepoTanim.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Store', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: DepoTanim.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Store', N'UpdateUser', N'Guncelleyen kullanici. | Eski alan: DepoTanim.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Store', N'DeleteDateTime', N'Silme tarihi. | Eski alan: DepoTanim.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Store', N'DeleteUser', N'Silen kullanici. | Eski alan: DepoTanim.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Store', N'RecordDateTime', N'Kayit zaman damgasi. | Eski alan: DepoTanim.RecDateTime'
GO

--
-- Definition for table StorePersonal :
-- Legacy: TICARI_SLAVE1.dbo.DepoPersonel
--

CREATE TABLE inventory.StorePersonal (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  InsertUser bigint DEFAULT 0 NOT NULL,
  InsertDateTime datetime DEFAULT getdate() NOT NULL,
  UpdateUser bigint NULL,
  UpdateDateTime datetime NULL,
  DeleteUser bigint NULL,
  DeleteDateTime datetime NULL,
  RecordDateTime datetime DEFAULT getdate() NOT NULL,
  StoreId bigint NOT NULL,
  PersonalId bigint NOT NULL
,
  CONSTRAINT StorePersonal_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StorePersonal', NULL, N'Eski tablo: DepoPersonel (TICARI_SLAVE1). Yeni şema: inventory.StorePersonal'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StorePersonal', N'Id', N'Birincil anahtar. | Eski alan: DepoPersonel.Id'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StorePersonal', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: DepoPersonel.GId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StorePersonal', N'CompanyId', N'Bagli sirket. | Eski alan: DepoPersonel.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StorePersonal', N'InsertUser', N'Olusturan kullanici. | Eski alan: DepoPersonel.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StorePersonal', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: DepoPersonel.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StorePersonal', N'UpdateUser', N'Guncelleyen kullanici. | Eski alan: DepoPersonel.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StorePersonal', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: DepoPersonel.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StorePersonal', N'DeleteUser', N'Silen kullanici. | Eski alan: DepoPersonel.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StorePersonal', N'DeleteDateTime', N'Silme tarihi. | Eski alan: DepoPersonel.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StorePersonal', N'RecordDateTime', N'Kayit zaman damgasi. | Eski alan: DepoPersonel.RecDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StorePersonal', N'StoreId', N'Depo (FK -> DepoTanim.Id). | Eski alan: DepoPersonel.DepoId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'StorePersonal', N'PersonalId', N'Personel (FK -> TICARI_MASTER.dbo.PersonelTanim.Id). | Eski alan: DepoPersonel.PersonelId'
GO

--
-- Definition for table Unit :
-- Legacy: TICARI_SLAVE1.dbo.BirimTanim
--

CREATE TABLE inventory.Unit (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  Code nvarchar(15) COLLATE Turkish_CI_AI NOT NULL,
  Description nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  DecimalPrecision int NOT NULL,
  Label nvarchar(500) COLLATE Turkish_CI_AI NULL,
  InsertDateTime datetime DEFAULT getdate() NULL,
  InsertUser bigint DEFAULT 0 NULL,
  UpdateDateTime datetime NULL,
  UpdateUser bigint NULL,
  DeleteDateTime datetime NULL,
  DeleteUser bigint NULL,
  RecordDateTime datetime DEFAULT getdate() NULL
,
  CONSTRAINT Unit_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Unit', NULL, N'Eski tablo: BirimTanim (TICARI_SLAVE1). Yeni şema: inventory.Unit'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Unit', N'Id', N'Birincil anahtar. | Eski alan: BirimTanim.Id'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Unit', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: BirimTanim.GId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Unit', N'CompanyId', N'Bagli sirket. | Eski alan: BirimTanim.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Unit', N'Code', N'Birim kodu. | Eski alan: BirimTanim.Kod'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Unit', N'Description', N'Birim aciklamasi. | Eski alan: BirimTanim.Aciklama'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Unit', N'DecimalPrecision', N'Ondalik hassasiyet (hane sayisi). | Eski alan: BirimTanim.DecimalPrecision'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Unit', N'Label', N'Etiket/barkod metni. | Eski alan: BirimTanim.Etiket'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Unit', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: BirimTanim.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Unit', N'InsertUser', N'Olusturan kullanici. | Eski alan: BirimTanim.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Unit', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: BirimTanim.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Unit', N'UpdateUser', N'Guncelleyen kullanici. | Eski alan: BirimTanim.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Unit', N'DeleteDateTime', N'Silme tarihi. | Eski alan: BirimTanim.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Unit', N'DeleteUser', N'Silen kullanici. | Eski alan: BirimTanim.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'inventory', N'Unit', N'RecordDateTime', N'Kayit zaman damgasi. | Eski alan: BirimTanim.RecDateTime'
GO

--
-- Definition for table TradeDocument :
-- Legacy: TICARI_SLAVE1.dbo.Fatura
--

CREATE TABLE trade.TradeDocument (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  StoreId bigint NOT NULL,
  SecondaryStoreId bigint NULL,
  AccountId bigint NOT NULL,
  DocumentType int NOT NULL,
  TradeDocumentNo nvarchar(50) COLLATE Turkish_CI_AI NOT NULL,
  TransactionDate datetime NOT NULL,
  Saat datetime NULL,
  DueDate datetime NULL,
  CurrencyCode nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  OrderStatus int NULL,
  OrderShippingSlipNumber nvarchar(50) COLLATE Turkish_CI_AI NULL,
  ShippingCompanyDefId bigint NULL,
  BillingFullNameOrTitle varchar(250) COLLATE Turkish_CI_AI NULL,
  BillingEmail nvarchar(100) COLLATE Turkish_CI_AI NULL,
  BillingPhone nvarchar(30) COLLATE Turkish_CI_AI NULL,
  Address nvarchar(500) COLLATE Turkish_CI_AI NULL,
  City nvarchar(50) COLLATE Turkish_CI_AI NULL,
  District nvarchar(50) COLLATE Turkish_CI_AI NULL,
  PostalCode nvarchar(30) COLLATE Turkish_CI_AI NULL,
  DeliverToDifferentAddress bit NOT NULL,
  DeliveryFullNameOrTitle nvarchar(250) COLLATE Turkish_CI_AI NULL,
  DeliveryEmail nvarchar(100) COLLATE Turkish_CI_AI NULL,
  DeliveryPhone nvarchar(30) COLLATE Turkish_CI_AI NULL,
  DeliveryAddress nvarchar(500) COLLATE Turkish_CI_AI NULL,
  DeliveryCity nvarchar(50) COLLATE Turkish_CI_AI NULL,
  DeliveryDistrict nvarchar(50) COLLATE Turkish_CI_AI NULL,
  DeliveryPostalCode nvarchar(30) COLLATE Turkish_CI_AI NULL,
  TaxOffice nvarchar(50) COLLATE Turkish_CI_AI NULL,
  TaxNumber nvarchar(50) COLLATE Turkish_CI_AI NULL,
  RelatedTradeDocumentId bigint NULL,
  MedicalServiceProductBrandModel nvarchar(250) COLLATE Turkish_CI_AI NULL,
  MedicalServiceCihazSerialCodeNo nvarchar(50) COLLATE Turkish_CI_AI NULL,
  MedicalServiceSellerCompany nvarchar(100) COLLATE Turkish_CI_AI NULL,
  ServiceAccessories nvarchar(750) COLLATE Turkish_CI_AI NULL,
  ServiceDeviceDescription nvarchar(750) COLLATE Turkish_CI_AI NULL,
  ServiceCustomerNote nvarchar(750) COLLATE Turkish_CI_AI NULL,
  ServicePersonalNote nvarchar(750) COLLATE Turkish_CI_AI NULL,
  ServiceWarrantyInfo bit NULL,
  MedicalServiceStatus int NULL,
  ServiceDeliveryDate datetime NULL,
  ServiceDeliveryRecipient nvarchar(100) COLLATE Turkish_CI_AI NULL,
  QuoteStatus int NULL,
  Label nvarchar(500) COLLATE Turkish_CI_AI NULL,
  IsDocumentClosed bit NOT NULL,
  ED_LastProcessDate datetime NULL,
  ED_Code int NULL,
  ED_Description nvarchar(200) COLLATE Turkish_CI_AI NULL,
  ED_DetailDescription nvarchar(500) COLLATE Turkish_CI_AI NULL,
  RecordSource int NULL,
  EcommercePaymentMethod int NULL,
  EcommercePaymentBank nvarchar(30) COLLATE Turkish_CI_AI NULL,
  EcommercePaymentId nvarchar(30) COLLATE Turkish_CI_AI NULL,
  IntegrationId nvarchar(50) COLLATE Turkish_CI_AI NULL,
  IntegrationName nvarchar(30) COLLATE Turkish_CI_AI NULL,
  LineCount int NOT NULL,
  TotalVatAmount decimal(18,4) NOT NULL,
  TotalAmount decimal(18,4) NOT NULL,
  InsertDateTime datetime DEFAULT getdate() NOT NULL,
  InsertUser bigint DEFAULT 0 NOT NULL,
  UpdateDateTime datetime NULL,
  UpdateUser bigint NULL,
  DeleteDateTime datetime NULL,
  DeleteUser bigint NULL,
  RecordDateTime datetime DEFAULT getdate() NULL,
  IsDocumentCancelled bit NOT NULL,
  ElectronicDocumentType int NULL,
  ElectronicDocumentNo nvarchar(50) COLLATE Turkish_CI_AI NULL,
  ElectronicDocumentSentDate datetime NULL,
  ElectronicDocumentErrors nvarchar(MAX) COLLATE Turkish_CI_AI NULL,
  ElectronicDocumentSendStatus int NULL
,
  CONSTRAINT TradeDocument_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', NULL, N'Eski tablo: Fatura (TICARI_SLAVE1). Yeni şema: trade.TradeDocument'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'Id', N'Birincil anahtar. | Eski alan: Fatura.Id'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: Fatura.GId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'CompanyId', N'Bagli sirket. | Eski alan: Fatura.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'StoreId', N'Ana depo (FK -> DepoTanim.Id). | Eski alan: Fatura.DepoTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'SecondaryStoreId', N'Ikincil/hedef depo. | Eski alan: Fatura.DepoTanim1Id'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'AccountId', N'Bagli cari (FK -> CariTanim.Id). | Eski alan: Fatura.CariTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'DocumentType', N'Tanimsiz=0, SatisFaturasi=100, SatisIrsaliye=101, AlinanSiparis=103, VerilenTeklif=104, VerilenServis=105, DepoCikis=106, AlisIadeFaturasi=107, SayimGiris=108, AlisFaturasi=200, AlisIrsaliye=201, VerilenSiparis=203, AlinanTeklif=204, DepoGiris=206, SatisIadeFaturasi=207, SayimCikis=208 | Eski alan: Fatura.HareketTipi'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'TradeDocumentNo', N'Belge/fatura numarasi. | Eski alan: Fatura.FaturaNo'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'TransactionDate', N'Belge tarihi. | Eski alan: Fatura.Tarih'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'Saat', N'Belge saati. | Eski alan: Fatura.Saat'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'DueDate', N'Vade tarihi. | Eski alan: Fatura.VadeTarihi'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'CurrencyCode', N'Para birimi. | Eski alan: Fatura.DovizKodu'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'OrderStatus', N'E-ticaret siparis durumu. | Eski alan: Fatura.SiparisDurumu'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'OrderShippingSlipNumber', N'Kargo takip/fis numarasi. | Eski alan: Fatura.SiparisKargoFisNo'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'ShippingCompanyDefId', N'Kargo firmasi ID. | Eski alan: Fatura.KargoFirmaTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'BillingFullNameOrTitle', N'Fatura teslim alici. | Eski alan: Fatura.FaturaTeslimAdSoyadUnvan'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'BillingEmail', N'Fatura teslim e-posta. | Eski alan: Fatura.FaturaTeslimEMail'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'BillingPhone', N'Fatura teslim telefon. | Eski alan: Fatura.FaturaTeslimTelefon'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'Address', N'Fatura adresi. | Eski alan: Fatura.Adres'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'City', N'Fatura ili. | Eski alan: Fatura.Il'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'District', N'Fatura ilcesi. | Eski alan: Fatura.Ilce'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'PostalCode', N'Fatura posta kodu. | Eski alan: Fatura.PostaKodu'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'DeliverToDifferentAddress', N'Farkli adrese teslim mi? | Eski alan: Fatura.FarkliAdreseTeslim'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'DeliveryFullNameOrTitle', N'Teslimat alici. | Eski alan: Fatura.TeslimatAdSoyadUnvan'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'DeliveryEmail', N'Teslimat e-posta. | Eski alan: Fatura.TeslimatEMail'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'DeliveryPhone', N'Teslimat telefon. | Eski alan: Fatura.TeslimatTelefon'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'DeliveryAddress', N'Teslimat adresi. | Eski alan: Fatura.TeslimatAdres'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'DeliveryCity', N'Teslimat ili. | Eski alan: Fatura.DeliveryCity'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'DeliveryDistrict', N'Teslimat ilcesi. | Eski alan: Fatura.DeliveryCityce'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'DeliveryPostalCode', N'Teslimat posta kodu. | Eski alan: Fatura.TeslimatPostaKodu'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'TaxOffice', N'Vergi dairesi. | Eski alan: Fatura.VergiDairesi'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'TaxNumber', N'Vergi numarasi. | Eski alan: Fatura.VergiNumarasi'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'RelatedTradeDocumentId', N'Teklif->Siparis->Irsaliye->Fatura zincirinde sonraki belge ID. | Eski alan: Fatura.BagliFaturaId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'MedicalServiceProductBrandModel', N'Servis: urun marka/model. | Eski alan: Fatura.ServisUrunMarkaModel'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'MedicalServiceCihazSerialCodeNo', N'Servis: cihaz seri no. | Eski alan: Fatura.ServisCihazSerialCodeNo'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'MedicalServiceSellerCompany', N'Servis: satici firma. | Eski alan: Fatura.ServisSaticiFirma'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'ServiceAccessories', N'Servis: aksesuarlar. | Eski alan: Fatura.ServisAksesuar'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'ServiceDeviceDescription', N'Servis: cihaz aciklamasi. | Eski alan: Fatura.ServisCihazAciklama'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'ServiceCustomerNote', N'Servis: musteri notu. | Eski alan: Fatura.ServisMusteriNotu'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'ServicePersonalNote', N'Servis: personel notu. | Eski alan: Fatura.ServisPersonelNotu'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'ServiceWarrantyInfo', N'Servis: garanti bilgisi. | Eski alan: Fatura.ServisGarantiBilgisi'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'MedicalServiceStatus', N'YeniSiparis=0, Hazirlaniyor=100, KargoyaVerildi=200, TeslimEdildi=300, Iade=400, Iptal=500 | Eski alan: Fatura.ServisDurumu'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'ServiceDeliveryDate', N'Servis teslim tarihi. | Eski alan: Fatura.ServisTeslimTarihi'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'ServiceDeliveryRecipient', N'Servisi teslim alan kisi. | Eski alan: Fatura.ServisTeslimAlanKisi'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'QuoteStatus', N'-100=TeklifIptal, 100=YeniTeklif, 200=TeklifKabulEdildi, 300=SozlesmeImzalandi, 400=KabulEdilmedi | Eski alan: Fatura.TeklifDurum'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'Label', N'Etiket. | Eski alan: Fatura.Etiket'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'IsDocumentClosed', N'Belge kapatildi mi (stok/cari kilit)? | Eski alan: Fatura.BelgeKapali'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'ED_LastProcessDate', N'e-Belge son islem tarihi. | Eski alan: Fatura.ED_SonIslemTarihi'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'ED_Code', N'e-Belge islem kodu. | Eski alan: Fatura.ED_Code'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'ED_Description', N'e-Belge islem aciklamasi. | Eski alan: Fatura.ED_Description'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'ED_DetailDescription', N'e-Belge detay aciklamasi. | Eski alan: Fatura.ED_DetailDescription'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'RecordSource', N'0=Normal, 1=ETicaret | Eski alan: Fatura.KayitYeri'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'EcommercePaymentMethod', N'1=KrediKarti, 2=BankayaHavale, 3=Kapida | Eski alan: Fatura.ETicaretOdemeSekli'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'EcommercePaymentBank', N'Odeme bankasi/kanal adi. | Eski alan: Fatura.ETicaretOdemeBanka'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'EcommercePaymentId', N'Odeme referans/islem ID. | Eski alan: Fatura.ETicaretOdemeId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'IntegrationId', N'Harici entegrasyon belge ID. | Eski alan: Fatura.EntegrasyonId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'IntegrationName', N'Harici entegrasyon adi. | Eski alan: Fatura.EntegrasyonAdi'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'LineCount', N'Toplam kalem sayisi. | Eski alan: Fatura.KalemSayisi'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'TotalVatAmount', N'Toplam KDV tutari. | Eski alan: Fatura.ToplamKdvTutar'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'TotalAmount', N'Toplam belge tutari. | Eski alan: Fatura.TotalAmount'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: Fatura.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'InsertUser', N'Olusturan kullanici. | Eski alan: Fatura.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: Fatura.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'UpdateUser', N'Guncelleyen kullanici. | Eski alan: Fatura.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'DeleteDateTime', N'Silme tarihi. | Eski alan: Fatura.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'DeleteUser', N'Silen kullanici. | Eski alan: Fatura.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'RecordDateTime', N'Kayit zaman damgasi. | Eski alan: Fatura.RecDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'IsDocumentCancelled', N'Belge iptal edildi mi? | Eski alan: Fatura.BelgeIptal'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'ElectronicDocumentType', N'1=EArsiv, 2=EFatura, 3=EIrsaliye | Eski alan: Fatura.ElektronikBelgeTipi'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'ElectronicDocumentNo', N'e-Belge numarasi. | Eski alan: Fatura.ElectronicDocumentNo'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'ElectronicDocumentSentDate', N'e-Belge gonderim tarihi. | Eski alan: Fatura.ElektronikBelgeGonderimTarihi'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'ElectronicDocumentErrors', N'e-Belge gonderim hata mesajlari. | Eski alan: Fatura.ElectronicDocumentErrors'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocument', N'ElectronicDocumentSendStatus', N'null=Gonderilmemis, -1=Hatali, 1=Basarili | Eski alan: Fatura.ElektronikBelgeGonderimDurumu'
GO

--
-- Definition for table TradeDocumentCurrency :
-- Legacy: TICARI_SLAVE1.dbo.FaturaDoviz
--

CREATE TABLE trade.TradeDocumentCurrency (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  TradeDocumentId bigint NULL,
  CurrencyCode nvarchar(10) COLLATE Turkish_CI_AI NULL,
  Value decimal(18,4) NULL,
  InsertDateTime datetime DEFAULT getdate() NULL,
  InsertUser bigint DEFAULT 0 NULL,
  UpdateDateTime datetime NULL,
  UpdateUser bigint NULL,
  DeleteDateTime datetime NULL,
  DeleteUser bigint NULL,
  RecordDateTime datetime DEFAULT getdate() NULL
,
  CONSTRAINT TradeDocumentCurrency_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentCurrency', NULL, N'Eski tablo: FaturaDoviz (TICARI_SLAVE1). Yeni şema: trade.TradeDocumentCurrency'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentCurrency', N'Id', N'Birincil anahtar. | Eski alan: FaturaDoviz.Id'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentCurrency', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: FaturaDoviz.GId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentCurrency', N'CompanyId', N'Bagli sirket. | Eski alan: FaturaDoviz.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentCurrency', N'TradeDocumentId', N'Bagli fatura (FK -> Fatura.Id). | Eski alan: FaturaDoviz.FaturaId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentCurrency', N'CurrencyCode', N'Doviz kodu. | Eski alan: FaturaDoviz.DovizKodu'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentCurrency', N'Value', N'Kur degeri. | Eski alan: FaturaDoviz.Deger'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentCurrency', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: FaturaDoviz.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentCurrency', N'InsertUser', N'Olusturan kullanici. | Eski alan: FaturaDoviz.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentCurrency', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: FaturaDoviz.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentCurrency', N'UpdateUser', N'Guncelleyen kullanici. | Eski alan: FaturaDoviz.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentCurrency', N'DeleteDateTime', N'Silme tarihi. | Eski alan: FaturaDoviz.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentCurrency', N'DeleteUser', N'Silen kullanici. | Eski alan: FaturaDoviz.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentCurrency', N'RecordDateTime', N'Kayit zaman damgasi. | Eski alan: FaturaDoviz.RecDateTime'
GO

--
-- Definition for table TradeDocumentLine :
-- Legacy: TICARI_SLAVE1.dbo.FaturaHareket
--

CREATE TABLE trade.TradeDocumentLine (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  TradeDocumentId bigint NOT NULL,
  StoreId bigint NOT NULL,
  DocumentType int NOT NULL,
  ProductId bigint NOT NULL,
  ProductDescription nvarchar(500) COLLATE Turkish_CI_AI NOT NULL,
  SerialCodeNo nvarchar(50) COLLATE Turkish_CI_AI NULL,
  ColorSize nvarchar(50) COLLATE Turkish_CI_AI NULL,
  Quantity decimal(18,4) NOT NULL,
  UnitId bigint NOT NULL,
  UnitMultiplier decimal(18,4) NULL,
  UnitPrice decimal(18,4) NOT NULL,
  CurrencyCode nvarchar(10) COLLATE Turkish_CI_AI NOT NULL,
  VatRate int NOT NULL,
  VatAmount decimal(18,4) NOT NULL,
  LineAmount decimal(18,4) NOT NULL,
  ExcludeFromStockCount bit NULL,
  Label nvarchar(500) COLLATE Turkish_CI_AI NULL,
  InsertDateTime datetime DEFAULT getdate() NULL,
  InsertUser bigint DEFAULT 0 NULL,
  UpdateDateTime datetime NULL,
  UpdateUser bigint NULL,
  DeleteDateTime datetime NULL,
  DeleteUser bigint NULL,
  RecordDateTime datetime DEFAULT getdate() NULL,
  DiscountPercent decimal(18,4) NULL,
  Discount1Percent decimal(18,4) NULL,
  DeferralPercent decimal(18,4) NULL
,
  CONSTRAINT TradeDocumentLine_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', NULL, N'Eski tablo: FaturaHareket (TICARI_SLAVE1). Yeni şema: trade.TradeDocumentLine'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'Id', N'Birincil anahtar. | Eski alan: FaturaHareket.Id'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: FaturaHareket.GId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'CompanyId', N'Bagli sirket. | Eski alan: FaturaHareket.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'TradeDocumentId', N'Bagli belge (FK -> Fatura.Id). | Eski alan: FaturaHareket.FaturaId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'StoreId', N'Islem yapilan depo (FK -> DepoTanim.Id). | Eski alan: FaturaHareket.DepoTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'DocumentType', N'Tanimsiz = 0, | Eski alan: FaturaHareket.HareketTipi'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'ProductId', N'Bagli urun (FK -> UrunTanim.Id). | Eski alan: FaturaHareket.UrunTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'ProductDescription', N'Satir urun aciklamasi. | Eski alan: FaturaHareket.UrunAciklama'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'SerialCodeNo', N'SerialCode numarasi. | Eski alan: FaturaHareket.SerialCodeNo'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'ColorSize', N'Renk/beden bilgisi. | Eski alan: FaturaHareket.RenkBeden'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'Quantity', N'Miktar. | Eski alan: FaturaHareket.Miktar'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'UnitId', N'Birim (FK -> BirimTanim.Id). | Eski alan: FaturaHareket.BirimId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'UnitMultiplier', N'Birim donusum katsayisi. | Eski alan: FaturaHareket.BirimKatsayi'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'UnitPrice', N'Birim fiyati. | Eski alan: FaturaHareket.BirimFiyati'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'CurrencyCode', N'Para birimi kodu. | Eski alan: FaturaHareket.DovizKodu'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'VatRate', N'KDV orani (%). | Eski alan: FaturaHareket.KdvOrani'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'VatAmount', N'KDV tutari. | Eski alan: FaturaHareket.KdvTutari'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'LineAmount', N'Satir tutari (KDV haric). | Eski alan: FaturaHareket.LineAmount'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'ExcludeFromStockCount', N'rsaliye faturaya dnnce irsaliye iinde kalan kalemler stok saymna dahil edilmesin diye bu yaplmtr. | Eski alan: FaturaHareket.ExcludeFromStockCount'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'Label', N'Etiket. | Eski alan: FaturaHareket.Etiket'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: FaturaHareket.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'InsertUser', N'Olusturan kullanici. | Eski alan: FaturaHareket.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: FaturaHareket.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'UpdateUser', N'Guncelleyen kullanici. | Eski alan: FaturaHareket.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'DeleteDateTime', N'Silme tarihi. | Eski alan: FaturaHareket.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'DeleteUser', N'Silen kullanici. | Eski alan: FaturaHareket.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'RecordDateTime', N'Kayit zaman damgasi. | Eski alan: FaturaHareket.RecDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'DiscountPercent', N'Iskonto yuzdesi. | Eski alan: FaturaHareket.IskontoYuzde'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'Discount1Percent', N'Ikinci iskonto yuzdesi. | Eski alan: FaturaHareket.Iskonto1Yuzde'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLine', N'DeferralPercent', N'Vade farki yuzdesi. | Eski alan: FaturaHareket.VadeFarkiYuzde'
GO

--
-- Definition for table TradeDocumentLineTemp :
-- Legacy: TICARI_SLAVE1.dbo.FaturaHareketTemp
--

CREATE TABLE trade.TradeDocumentLineTemp (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  PersonalGId uniqueidentifier NOT NULL,
  CartId bigint NULL,
  CompanyId bigint NOT NULL,
  DocumentType int NOT NULL,
  ProductGId uniqueidentifier NOT NULL,
  ProductDescription nvarchar(500) COLLATE Turkish_CI_AI NOT NULL,
  SerialCodeNo nvarchar(50) COLLATE Turkish_CI_AI NULL,
  ColorSize nvarchar(50) COLLATE Turkish_CI_AI NULL,
  Quantity decimal(18,4) NOT NULL,
  UnitId bigint NOT NULL,
  UnitMultiplier decimal(18,4) NULL,
  UnitPrice decimal(18,4) NOT NULL,
  UnitPriceVatIncluded decimal(18,4) NOT NULL,
  CurrencyCode nvarchar(10) COLLATE Turkish_CI_AI NOT NULL,
  VatRate int NOT NULL,
  VatDH nvarchar(1) COLLATE Turkish_CI_AI NULL,
  TotalAmountExVat decimal(18,4) NULL,
  TotalAmount decimal(18,4) NOT NULL,
  TradeDocumentGId uniqueidentifier NULL,
  TradeDocumentTypeGId uniqueidentifier NULL,
  InsertDateTime datetime DEFAULT getdate() NOT NULL,
  EcommercePaymentId nvarchar(50) COLLATE Turkish_CI_AI NULL,
  IsSoldOut bit NOT NULL,
  StoreGId uniqueidentifier NULL,
  DiscountPercent decimal(18,4) NULL,
  Discount1Percent decimal(18,4) NULL,
  DeferralPercent decimal(18,4) NULL
,
  CONSTRAINT TradeDocumentLineTemp_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', NULL, N'Eski tablo: FaturaHareketTemp (TICARI_SLAVE1). Yeni şema: trade.TradeDocumentLineTemp'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'Id', N'Birincil anahtar. | Eski alan: FaturaHareketTemp.Id'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: FaturaHareketTemp.GId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'PersonalGId', N'Personel/kullanici GUID (sepet sahibi). | Eski alan: FaturaHareketTemp.PersonelGId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'CartId', N'Sepet oturum ID. | Eski alan: FaturaHareketTemp.CartId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'CompanyId', N'Bagli sirket. | Eski alan: FaturaHareketTemp.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'DocumentType', N'Belge hareket tipi. | Eski alan: FaturaHareketTemp.HareketTipi'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'ProductGId', N'Urun GUID. | Eski alan: FaturaHareketTemp.UrunGId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'ProductDescription', N'Satir urun aciklamasi. | Eski alan: FaturaHareketTemp.UrunAciklama'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'SerialCodeNo', N'SerialCode numarasi. | Eski alan: FaturaHareketTemp.SerialCodeNo'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'ColorSize', N'Renk/beden bilgisi. | Eski alan: FaturaHareketTemp.RenkBeden'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'Quantity', N'Miktar. | Eski alan: FaturaHareketTemp.Miktar'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'UnitId', N'Birim (FK -> BirimTanim.Id). | Eski alan: FaturaHareketTemp.BirimId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'UnitMultiplier', N'Birim donusum katsayisi. | Eski alan: FaturaHareketTemp.BirimKatsayi'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'UnitPrice', N'Birim fiyati (KDV haric). | Eski alan: FaturaHareketTemp.BirimFiyati'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'UnitPriceVatIncluded', N'Birim fiyati (KDV dahil). | Eski alan: FaturaHareketTemp.BirimFiyatiKdvDahil'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'CurrencyCode', N'Para birimi kodu. | Eski alan: FaturaHareketTemp.DovizKodu'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'VatRate', N'KDV orani (%). | Eski alan: FaturaHareketTemp.KdvOrani'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'VatDH', N'KDV durumu: D=Dahil, H=Haric. | Eski alan: FaturaHareketTemp.KdvDH'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'TotalAmountExVat', N'Toplam tutar (KDV haric). | Eski alan: FaturaHareketTemp.TotalAmountKdvHaric'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'TotalAmount', N'Toplam tutar (KDV dahil). | Eski alan: FaturaHareketTemp.TotalAmount'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'TradeDocumentGId', N'Bagli fatura GUID. | Eski alan: FaturaHareketTemp.FaturaGId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'TradeDocumentTypeGId', N'Bagli fatura satir GUID. | Eski alan: FaturaHareketTemp.FaturaHareketGId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: FaturaHareketTemp.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'EcommercePaymentId', N'E-ticaret odeme referans ID. | Eski alan: FaturaHareketTemp.ETicaretOdemeId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'IsSoldOut', N'Stok tukendi mi? | Eski alan: FaturaHareketTemp.IsSoldOut'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'StoreGId', N'Depo GUID. | Eski alan: FaturaHareketTemp.DepoTanimGId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'DiscountPercent', N'Iskonto yuzdesi. | Eski alan: FaturaHareketTemp.IskontoYuzde'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'Discount1Percent', N'Ikinci iskonto yuzdesi. | Eski alan: FaturaHareketTemp.Iskonto1Yuzde'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentLineTemp', N'DeferralPercent', N'Vade farki yuzdesi. | Eski alan: FaturaHareketTemp.VadeFarkiYuzde'
GO

--
-- Definition for table TradeDocumentTemp :
-- Legacy: TICARI_SLAVE1.dbo.FaturaTemp
--

CREATE TABLE trade.TradeDocumentTemp (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  PersonalGId uniqueidentifier NOT NULL,
  DocumentType int NOT NULL,
  AccountId bigint NULL,
  TradeDocumentNo nvarchar(50) COLLATE Turkish_CI_AI NULL,
  TransactionDate datetime NULL,
  CurrencyCode nvarchar(30) COLLATE Turkish_CI_AI NULL,
  Address nvarchar(500) COLLATE Turkish_CI_AI NULL
,
  CONSTRAINT TradeDocumentTemp_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentTemp', NULL, N'Eski tablo: FaturaTemp (TICARI_SLAVE1). Yeni şema: trade.TradeDocumentTemp'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentTemp', N'Id', N'Birincil anahtar. | Eski alan: FaturaTemp.Id'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentTemp', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: FaturaTemp.GId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentTemp', N'CompanyId', N'Bagli sirket. | Eski alan: FaturaTemp.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentTemp', N'PersonalGId', N'Personel/kullanici GUID. | Eski alan: FaturaTemp.PersonelGId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentTemp', N'DocumentType', N'Belge hareket tipi. | Eski alan: FaturaTemp.HareketTipi'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentTemp', N'AccountId', N'Bagli cari (FK -> CariTanim.Id). | Eski alan: FaturaTemp.CariTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentTemp', N'TradeDocumentNo', N'Belge/fatura numarasi. | Eski alan: FaturaTemp.FaturaNo'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentTemp', N'TransactionDate', N'Belge tarihi. | Eski alan: FaturaTemp.Tarih'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentTemp', N'CurrencyCode', N'Para birimi kodu. | Eski alan: FaturaTemp.DovizKodu'
GO

EXEC dbo._SetFullSchemaDescription N'trade', N'TradeDocumentTemp', N'Address', N'Adres. | Eski alan: FaturaTemp.Adres'
GO

--
-- Definition for table ReportDef :
-- Legacy: TICARI_SLAVE1.dbo.RaporTanim
--

CREATE TABLE report.ReportDef (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  ReportType nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  Description nvarchar(100) COLLATE Turkish_CI_AI NULL,
  BinData varbinary(max) NOT NULL,
  InsertDateTime datetime DEFAULT getdate() NOT NULL,
  InsertUser bigint DEFAULT 0 NOT NULL,
  UpdateDateTime datetime NULL,
  UpdateUser bigint NULL,
  DeleteDateTime datetime NULL,
  DeleteUser bigint NULL,
  RecordDateTime datetime DEFAULT getdate() NULL
,
  CONSTRAINT ReportDef_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC dbo._SetFullSchemaDescription N'report', N'ReportDef', NULL, N'Eski tablo: RaporTanim (TICARI_SLAVE1). Yeni şema: report.ReportDef'
GO

EXEC dbo._SetFullSchemaDescription N'report', N'ReportDef', N'Id', N'Birincil anahtar. | Eski alan: RaporTanim.Id'
GO

EXEC dbo._SetFullSchemaDescription N'report', N'ReportDef', N'GId', N'Global benzersiz kimlik (GUID). | Eski alan: RaporTanim.GId'
GO

EXEC dbo._SetFullSchemaDescription N'report', N'ReportDef', N'CompanyId', N'Bagli sirket. | Eski alan: RaporTanim.SirketTanimId'
GO

EXEC dbo._SetFullSchemaDescription N'report', N'ReportDef', N'ReportType', N'Rapor tipi kodu. | Eski alan: RaporTanim.RaporTipi'
GO

EXEC dbo._SetFullSchemaDescription N'report', N'ReportDef', N'Description', N'Rapor aciklamasi. | Eski alan: RaporTanim.Aciklama'
GO

EXEC dbo._SetFullSchemaDescription N'report', N'ReportDef', N'BinData', N'Rapor sablon binary verisi. | Eski alan: RaporTanim.BinData'
GO

EXEC dbo._SetFullSchemaDescription N'report', N'ReportDef', N'InsertDateTime', N'Olusturma tarihi. | Eski alan: RaporTanim.InsertDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'report', N'ReportDef', N'InsertUser', N'Olusturan kullanici. | Eski alan: RaporTanim.InsertUser'
GO

EXEC dbo._SetFullSchemaDescription N'report', N'ReportDef', N'UpdateDateTime', N'Guncelleme tarihi. | Eski alan: RaporTanim.UpdateDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'report', N'ReportDef', N'UpdateUser', N'Guncelleyen kullanici. | Eski alan: RaporTanim.UpdateUser'
GO

EXEC dbo._SetFullSchemaDescription N'report', N'ReportDef', N'DeleteDateTime', N'Silme tarihi. | Eski alan: RaporTanim.DeleteDateTime'
GO

EXEC dbo._SetFullSchemaDescription N'report', N'ReportDef', N'DeleteUser', N'Silen kullanici. | Eski alan: RaporTanim.DeleteUser'
GO

EXEC dbo._SetFullSchemaDescription N'report', N'ReportDef', N'RecordDateTime', N'Kayit zaman damgasi. | Eski alan: RaporTanim.RecDateTime'
GO


-- HangFire schema tables (standard)

CREATE TABLE HangFire.AggregatedCounter (
  [Key] nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  Value bigint NOT NULL,
  ExpireAt datetime NULL,
  CONSTRAINT PK_HangFire_CounterAggregated PRIMARY KEY CLUSTERED ([Key])
)
ON [PRIMARY]
GO

CREATE TABLE HangFire.Counter (
  [Key] nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  Value int NOT NULL,
  ExpireAt datetime NULL,
  Id bigint IDENTITY(1, 1) NOT NULL,
  CONSTRAINT PK_HangFire_Counter PRIMARY KEY CLUSTERED ([Key], Id)
)
ON [PRIMARY]
GO

CREATE TABLE HangFire.Hash (
  [Key] nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  Field nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  Value nvarchar(max) COLLATE Turkish_CI_AI NULL,
  ExpireAt datetime2(7) NULL,
  CONSTRAINT PK_HangFire_Hash PRIMARY KEY CLUSTERED ([Key], Field)
)
ON [PRIMARY]
GO

CREATE TABLE HangFire.Job (
  Id bigint IDENTITY(1, 1) NOT NULL,
  StateId bigint NULL,
  StateName nvarchar(20) COLLATE Turkish_CI_AI NULL,
  InvocationData nvarchar(max) COLLATE Turkish_CI_AI NOT NULL,
  Arguments nvarchar(max) COLLATE Turkish_CI_AI NOT NULL,
  CreatedAt datetime NOT NULL,
  ExpireAt datetime NULL,
  CONSTRAINT PK_HangFire_Job PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

CREATE TABLE HangFire.JobParameter (
  JobId bigint NOT NULL,
  Name nvarchar(40) COLLATE Turkish_CI_AI NOT NULL,
  Value nvarchar(max) COLLATE Turkish_CI_AI NULL,
  CONSTRAINT PK_HangFire_JobParameter PRIMARY KEY CLUSTERED (JobId, Name)
)
ON [PRIMARY]
GO

CREATE TABLE HangFire.JobQueue (
  Id bigint IDENTITY(1, 1) NOT NULL,
  JobId bigint NOT NULL,
  Queue nvarchar(50) COLLATE Turkish_CI_AI NOT NULL,
  FetchedAt datetime NULL,
  CONSTRAINT PK_HangFire_JobQueue PRIMARY KEY CLUSTERED (Queue, Id)
)
ON [PRIMARY]
GO

CREATE TABLE HangFire.List (
  Id bigint IDENTITY(1, 1) NOT NULL,
  [Key] nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  Value nvarchar(max) COLLATE Turkish_CI_AI NULL,
  ExpireAt datetime NULL,
  CONSTRAINT PK_HangFire_List PRIMARY KEY CLUSTERED ([Key], Id)
)
ON [PRIMARY]
GO

CREATE TABLE HangFire.[Schema] (
  Version int NOT NULL,
  CONSTRAINT PK_HangFire_Schema PRIMARY KEY CLUSTERED (Version)
)
ON [PRIMARY]
GO

CREATE TABLE HangFire.Server (
  Id nvarchar(200) COLLATE Turkish_CI_AI NOT NULL,
  Data nvarchar(max) COLLATE Turkish_CI_AI NULL,
  LastHeartbeat datetime NOT NULL,
  CONSTRAINT PK_HangFire_Server PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

CREATE TABLE HangFire.[Set] (
  [Key] nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  Score float NOT NULL,
  Value nvarchar(256) COLLATE Turkish_CI_AI NOT NULL,
  ExpireAt datetime NULL,
  CONSTRAINT PK_HangFire_Set PRIMARY KEY CLUSTERED ([Key], Value)
)
ON [PRIMARY]
GO

CREATE TABLE HangFire.State (
  Id bigint IDENTITY(1, 1) NOT NULL,
  JobId bigint NOT NULL,
  Name nvarchar(20) COLLATE Turkish_CI_AI NOT NULL,
  Reason nvarchar(100) COLLATE Turkish_CI_AI NULL,
  CreatedAt datetime NOT NULL,
  Data nvarchar(max) COLLATE Turkish_CI_AI NULL,
  CONSTRAINT PK_HangFire_State PRIMARY KEY CLUSTERED (JobId, Id)
)
ON [PRIMARY]
GO



-- Seed: common.Moduls (AIProjeMimari standard)
-- Table mapping lives in docs/TABLE-MAPPING.md (platform schema deferred)

INSERT INTO common.Moduls (Id, ModulName) VALUES
  (1, N'AccountType'),
  (2, N'TradeDocumentStatus'),
  (3, N'PaymentStatus'),
  (4, N'ActionStatus'),
  (5, N'TaskStatus')
GO


--
-- Definition for foreign keys :
--

ALTER TABLE common.License
ADD CONSTRAINT FK_License_CompanyId FOREIGN KEY (CompanyId)
  REFERENCES common.Company (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE common.Token
ADD CONSTRAINT FK_Token_CompanyId FOREIGN KEY (CompanyId)
  REFERENCES common.Company (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE common.Token
ADD CONSTRAINT FK_Token_PersonalId FOREIGN KEY (PersonalId)
  REFERENCES common.Personal (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE inventory.Unit
ADD CONSTRAINT FK_Unit_CompanyId FOREIGN KEY (CompanyId)
  REFERENCES common.Company (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE finance.AccountTransaction
ADD CONSTRAINT FK_AccountTransaction_CompanyId FOREIGN KEY (CompanyId)
  REFERENCES common.Company (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE finance.AccountTransaction
ADD CONSTRAINT FK_AccountTransaction_AccountId FOREIGN KEY (AccountId)
  REFERENCES finance.Account (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE finance.AccountTransaction
ADD CONSTRAINT FK_AccountTransaction_TradeDocumentId FOREIGN KEY (TradeDocumentId)
  REFERENCES trade.TradeDocument (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE finance.AccountTransaction
ADD CONSTRAINT FK_AccountTransaction_CashRegisterId FOREIGN KEY (CashRegisterId)
  REFERENCES finance.CashRegister (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE finance.Account
ADD CONSTRAINT FK_Account_CompanyId FOREIGN KEY (CompanyId)
  REFERENCES common.Company (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE finance.AccountAddress
ADD CONSTRAINT FK_AccountAddress_CompanyId FOREIGN KEY (CompanyId)
  REFERENCES common.Company (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE finance.AccountAddress
ADD CONSTRAINT FK_AccountAddress_AccountId FOREIGN KEY (AccountId)
  REFERENCES finance.Account (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE finance.AccountDocument
ADD CONSTRAINT FK_AccountDocument_CompanyId FOREIGN KEY (CompanyId)
  REFERENCES common.Company (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE finance.AccountDocument
ADD CONSTRAINT FK_AccountDocument_AccountId FOREIGN KEY (AccountId)
  REFERENCES finance.Account (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE finance.CheckNoteTransaction
ADD CONSTRAINT FK_CheckNoteTransaction_CompanyId FOREIGN KEY (CompanyId)
  REFERENCES common.Company (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE finance.CheckNoteTransaction
ADD CONSTRAINT FK_CheckNoteTransaction_AccountId FOREIGN KEY (AccountId)
  REFERENCES finance.Account (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE finance.CheckNote
ADD CONSTRAINT FK_CheckNote_CompanyId FOREIGN KEY (CompanyId)
  REFERENCES common.Company (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE finance.CheckNote
ADD CONSTRAINT FK_CheckNote_AccountId FOREIGN KEY (AccountId)
  REFERENCES finance.Account (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE inventory.StorePersonal
ADD CONSTRAINT FK_StorePersonal_CompanyId FOREIGN KEY (CompanyId)
  REFERENCES common.Company (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE inventory.StorePersonal
ADD CONSTRAINT FK_StorePersonal_StoreId FOREIGN KEY (StoreId)
  REFERENCES inventory.Store (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE inventory.StorePersonal
ADD CONSTRAINT FK_StorePersonal_PersonalId FOREIGN KEY (PersonalId)
  REFERENCES common.Personal (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE inventory.Store
ADD CONSTRAINT FK_Store_CompanyId FOREIGN KEY (CompanyId)
  REFERENCES common.Company (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE finance.CurrencyRate
ADD CONSTRAINT FK_CurrencyRate_CompanyId FOREIGN KEY (CompanyId)
  REFERENCES common.Company (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE finance.Currency
ADD CONSTRAINT FK_Currency_CompanyId FOREIGN KEY (CompanyId)
  REFERENCES common.Company (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE common.EmailTemplate
ADD CONSTRAINT FK_EmailTemplate_CompanyId FOREIGN KEY (CompanyId)
  REFERENCES common.Company (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE trade.TradeDocument
ADD CONSTRAINT FK_TradeDocument_CompanyId FOREIGN KEY (CompanyId)
  REFERENCES common.Company (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE trade.TradeDocument
ADD CONSTRAINT FK_TradeDocument_StoreId FOREIGN KEY (StoreId)
  REFERENCES inventory.Store (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE trade.TradeDocument
ADD CONSTRAINT FK_TradeDocument_AccountId FOREIGN KEY (AccountId)
  REFERENCES finance.Account (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE trade.TradeDocumentCurrency
ADD CONSTRAINT FK_TradeDocumentCurrency_CompanyId FOREIGN KEY (CompanyId)
  REFERENCES common.Company (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE trade.TradeDocumentCurrency
ADD CONSTRAINT FK_TradeDocumentCurrency_TradeDocumentId FOREIGN KEY (TradeDocumentId)
  REFERENCES trade.TradeDocument (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE trade.TradeDocumentLine
ADD CONSTRAINT FK_TradeDocumentLine_CompanyId FOREIGN KEY (CompanyId)
  REFERENCES common.Company (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE trade.TradeDocumentLine
ADD CONSTRAINT FK_TradeDocumentLine_TradeDocumentId FOREIGN KEY (TradeDocumentId)
  REFERENCES trade.TradeDocument (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE trade.TradeDocumentLine
ADD CONSTRAINT FK_TradeDocumentLine_StoreId FOREIGN KEY (StoreId)
  REFERENCES inventory.Store (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE trade.TradeDocumentLine
ADD CONSTRAINT FK_TradeDocumentLine_ProductId FOREIGN KEY (ProductId)
  REFERENCES inventory.Product (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE trade.TradeDocumentLine
ADD CONSTRAINT FK_TradeDocumentLine_UnitId FOREIGN KEY (UnitId)
  REFERENCES inventory.Unit (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE trade.TradeDocumentLineTemp
ADD CONSTRAINT FK_TradeDocumentLineTemp_CompanyId FOREIGN KEY (CompanyId)
  REFERENCES common.Company (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE trade.TradeDocumentLineTemp
ADD CONSTRAINT FK_TradeDocumentLineTemp_UnitId FOREIGN KEY (UnitId)
  REFERENCES inventory.Unit (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE trade.TradeDocumentTemp
ADD CONSTRAINT FK_TradeDocumentTemp_CompanyId FOREIGN KEY (CompanyId)
  REFERENCES common.Company (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE trade.TradeDocumentTemp
ADD CONSTRAINT FK_TradeDocumentTemp_AccountId FOREIGN KEY (AccountId)
  REFERENCES finance.Account (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE finance.CashTransaction
ADD CONSTRAINT FK_CashTransaction_CompanyId FOREIGN KEY (CompanyId)
  REFERENCES common.Company (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE finance.CashTransaction
ADD CONSTRAINT FK_CashTransaction_CashRegisterId FOREIGN KEY (CashRegisterId)
  REFERENCES finance.CashRegister (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE finance.CashRegisterPersonal
ADD CONSTRAINT FK_CashRegisterPersonal_CompanyId FOREIGN KEY (CompanyId)
  REFERENCES common.Company (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE finance.CashRegisterPersonal
ADD CONSTRAINT FK_CashRegisterPersonal_CashRegisterId FOREIGN KEY (CashRegisterId)
  REFERENCES finance.CashRegister (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE finance.CashRegisterPersonal
ADD CONSTRAINT FK_CashRegisterPersonal_PersonalId FOREIGN KEY (PersonalId)
  REFERENCES common.Personal (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE finance.CashRegister
ADD CONSTRAINT FK_CashRegister_CompanyId FOREIGN KEY (CompanyId)
  REFERENCES common.Company (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE finance.CashRegister
ADD CONSTRAINT FK_CashRegister_StoreId FOREIGN KEY (StoreId)
  REFERENCES inventory.Store (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE inventory.Brand
ADD CONSTRAINT FK_Brand_CompanyId FOREIGN KEY (CompanyId)
  REFERENCES common.Company (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE report.ReportDef
ADD CONSTRAINT FK_ReportDef_CompanyId FOREIGN KEY (CompanyId)
  REFERENCES common.Company (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE common.Counter
ADD CONSTRAINT FK_Counter_CompanyId FOREIGN KEY (CompanyId)
  REFERENCES common.Company (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE common.CounterReference
ADD CONSTRAINT FK_CounterReference_CompanyId FOREIGN KEY (CompanyId)
  REFERENCES common.Company (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE common.CounterReference
ADD CONSTRAINT FK_CounterReference_StoreId FOREIGN KEY (StoreId)
  REFERENCES inventory.Store (Id)
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

GO

-- ============================================================================
-- AI platform tables (missing from product/legacy DDL)
-- ============================================================================

-- Definition for table Country :
CREATE TABLE [common].[Country] (
  [Id] bigint IDENTITY(1, 1) NOT NULL,
  [GId] uniqueidentifier DEFAULT (newid()) NOT NULL,
  [CompanyId] bigint NOT NULL,
  [CountryCode] nvarchar(10) COLLATE Turkish_CI_AI NOT NULL,
  [Stat] bit DEFAULT ((1)) NOT NULL,
  [Description] nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  [Ordered] int DEFAULT ((0)) NOT NULL,
  CONSTRAINT [Country_pk] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [Country_idx] ON [common].[Country] ([CountryCode] ASC)
ON [PRIMARY]
GO

-- Definition for table Branch :
CREATE TABLE [common].[Branch] (
  [Id] bigint IDENTITY(1, 1) NOT NULL,
  [GId] uniqueidentifier DEFAULT (newid()) NOT NULL,
  [Stat] bit DEFAULT ((1)) NOT NULL,
  [CompanyMasterCode] nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  [Code] nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  [Description] nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  [ManagerPersonalId] bigint NULL,
  [ManagerAccountCode] nvarchar(100) COLLATE Turkish_CI_AI NULL,
  [Country] nvarchar(30) COLLATE Turkish_CI_AI NULL,
  [City] nvarchar(30) COLLATE Turkish_CI_AI NULL,
  [District] varchar(30) COLLATE Turkish_CI_AI NULL,
  [CityCode] nvarchar(100) COLLATE Turkish_CI_AI NULL,
  [DistrictCode] nvarchar(100) COLLATE Turkish_CI_AI NULL,
  [Latitude] decimal(18,7) DEFAULT ((0.0)) NOT NULL,
  [Longitude] decimal(18,7) DEFAULT ((0.0)) NOT NULL,
  [StartIP] nvarchar(60) COLLATE Turkish_CI_AI NULL,
  [EndIP] nvarchar(60) COLLATE Turkish_CI_AI NULL,
  [Color] nvarchar(200) COLLATE Turkish_CI_AI NULL,
  [Label] nvarchar(max) COLLATE Turkish_CI_AI NULL,
  [CreatedDate] datetime DEFAULT (getdate()) NOT NULL,
  [CreatedUser] bigint DEFAULT ((0)) NOT NULL,
  [ModifedDate] datetime NULL,
  [ModifedUser] bigint NULL,
  [DeletedDate] datetime NULL,
  [DeletedUser] bigint NULL,
  [IsDelete] bigint DEFAULT ((0)) NOT NULL,
  CONSTRAINT [PK__Branch__3214EC072945F5AE] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Branch_idx] ON [common].[Branch] ([GId] ASC)
ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Branch_uq] ON [common].[Branch] ([CompanyMasterCode] ASC, [Code] ASC, [DeletedDate] ASC)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_Branch_CompanyMasterCode_IsDelete_Stat] ON [common].[Branch] ([CompanyMasterCode] ASC, [IsDelete] ASC, [Stat] ASC) INCLUDE ([Id], [Code], [Description], [City], [Country])
ON [PRIMARY]
GO

-- Definition for table PersonalCompany :
CREATE TABLE [common].[PersonalCompany] (
  [Id] bigint IDENTITY(1, 1) NOT NULL,
  [GId] uniqueidentifier DEFAULT (newid()) NOT NULL,
  [PersonalId] bigint NOT NULL,
  [CompanyId] bigint NOT NULL,
  [IsWebLogin] bit DEFAULT ((1)) NOT NULL,
  [IsMobileLogin] bit DEFAULT ((1)) NOT NULL,
  [ShowAllAuditSession] bit DEFAULT ((0)) NOT NULL,
  [ShowAllAuditAction] bit DEFAULT ((0)) NOT NULL,
  [BranchActionReport] bit DEFAULT ((0)) NOT NULL,
  [CreatedDate] datetime DEFAULT (getdate()) NOT NULL,
  [CreatedUser] bigint DEFAULT ((0)) NOT NULL,
  [ModifedDate] datetime NULL,
  [ModifedUser] bigint NULL,
  [DeletedDate] datetime NULL,
  [DeletedUser] bigint NULL,
  [IsDelete] bigint DEFAULT ((0)) NOT NULL,
  CONSTRAINT [PersonalCompany_pk] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_PersonalCompany_CompanyId_IsDelete] ON [common].[PersonalCompany] ([CompanyId] ASC, [IsDelete] ASC) INCLUDE ([IsMobileLogin], [IsWebLogin], [PersonalId])
ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [PersonalCompany_uq] ON [common].[PersonalCompany] ([PersonalId] ASC, [CompanyId] ASC, [DeletedDate] ASC)
ON [PRIMARY]
GO

-- Definition for table PersonalBranch :
CREATE TABLE [common].[PersonalBranch] (
  [Id] bigint IDENTITY(1, 1) NOT NULL,
  [GId] uniqueidentifier DEFAULT (newid()) NOT NULL,
  [Stat] bit DEFAULT ((1)) NOT NULL,
  [PersonalId] bigint NOT NULL,
  [BranchId] bigint NOT NULL,
  [IsPrimary] bit DEFAULT ((0)) NOT NULL,
  [CreatedDate] datetime DEFAULT (getdate()) NOT NULL,
  [CreatedUser] bigint DEFAULT ((0)) NOT NULL,
  [ModifedDate] datetime NULL,
  [ModifedUser] bigint NULL,
  [DeletedDate] datetime NULL,
  [DeletedUser] bigint NULL,
  [IsDelete] bigint DEFAULT ((0)) NOT NULL,
  CONSTRAINT [PersonalBranch_pk] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [PersonalBranch_BranchId_idx] ON [common].[PersonalBranch] ([BranchId] ASC, [DeletedDate] ASC) INCLUDE ([PersonalId])
ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [PersonalBranch_idx] ON [common].[PersonalBranch] ([GId] ASC)
ON [PRIMARY]
GO

-- Definition for table CodeDef :
CREATE TABLE [common].[CodeDef] (
  [Id] bigint IDENTITY(1, 1) NOT NULL,
  [GId] uniqueidentifier DEFAULT (newid()) NOT NULL,
  [Stat] bit DEFAULT ((1)) NOT NULL,
  [Prefix] nvarchar(30) COLLATE Turkish_CI_AI NULL,
  [Code] nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  [Description] nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  [CompanyId] bigint NOT NULL,
  [CreatedDate] datetime DEFAULT (getdate()) NOT NULL,
  [CreatedUser] bigint DEFAULT ((0)) NOT NULL,
  [ModifedDate] datetime NULL,
  [ModifedUser] bigint NULL,
  [DeletedDate] datetime NULL,
  [DeletedUser] bigint NULL,
  [IsDelete] bigint DEFAULT ((0)) NOT NULL,
  CONSTRAINT [CodeDef_pk] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_CodeDef_Prefix_CompanyId_IsDelete] ON [common].[CodeDef] ([Prefix] ASC, [CompanyId] ASC, [IsDelete] ASC) INCLUDE ([Code], [Description], [Stat])
ON [PRIMARY]
GO

-- Definition for table ProcessType :
CREATE TABLE [common].[ProcessType] (
  [Id] bigint IDENTITY(0, 1) NOT NULL,
  [ModulId] bigint NOT NULL,
  [Code] nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  [DescriptionTR] nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  [DescriptionEN] nvarchar(100) COLLATE Turkish_CI_AI NULL,
  [Ordered] int DEFAULT ((0)) NOT NULL,
  [Color] nvarchar(200) COLLATE Turkish_CI_AI NULL,
  CONSTRAINT [ProcessType_pk] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [ProcessType_uq] ON [common].[ProcessType] ([ModulId] ASC, [Code] ASC)
ON [PRIMARY]
GO

-- Definition for table ProcessFlow :
CREATE TABLE [common].[ProcessFlow] (
  [Id] bigint IDENTITY(1, 1) NOT NULL,
  [GId] uniqueidentifier DEFAULT (newid()) NOT NULL,
  [ModulId] bigint NOT NULL,
  [FromCode] nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  [ToCode] nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  CONSTRAINT [ProcessFlow_pk] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [ProcessFlow_GId_uq] ON [common].[ProcessFlow] ([GId] ASC)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ProcessFlow_ModulId_idx] ON [common].[ProcessFlow] ([ModulId] ASC)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ProcessFlow_FromCode_idx] ON [common].[ProcessFlow] ([FromCode] ASC)
ON [PRIMARY]
GO

-- Definition for table Languages :
CREATE TABLE [common].[Languages] (
  [Stat] bit DEFAULT ((1)) NOT NULL,
  [MasterLanguge] bit DEFAULT ((0)) NOT NULL,
  [LanguageCode] nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  [DisLanguageCode] nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  [Description] nvarchar(50) COLLATE Turkish_CI_AI NOT NULL,
  [Order] int DEFAULT ((99)) NOT NULL,
  [Flag] nvarchar(50) COLLATE Turkish_CI_AI NULL,
  CONSTRAINT [PK__Language__8B8C8A35606543D8] PRIMARY KEY CLUSTERED ([LanguageCode])
)
ON [PRIMARY]
GO

-- Definition for table TranslationDef :
CREATE TABLE [common].[TranslationDef] (
  [Id] bigint IDENTITY(1, 1) NOT NULL,
  [GId] uniqueidentifier DEFAULT (newid()) NOT NULL,
  [CompanyId] bigint DEFAULT ((1)) NOT NULL,
  [TableName] nvarchar(50) COLLATE Turkish_CI_AI NOT NULL,
  [TableId] bigint NOT NULL,
  [TableFieldName] nvarchar(50) COLLATE Turkish_CI_AI NULL,
  [LanguageCode] nvarchar(10) COLLATE Turkish_CI_AI NOT NULL,
  [Description] nvarchar(max) COLLATE Turkish_CI_AI NOT NULL,
  [CreatedDate] datetime DEFAULT (getdate()) NOT NULL,
  [CreatedUser] bigint DEFAULT ((0)) NOT NULL,
  [ModifedDate] datetime NULL,
  [ModifedUser] bigint NULL,
  [DeletedDate] datetime NULL,
  [DeletedUser] bigint NULL,
  [IsDelete] bigint DEFAULT ((0)) NOT NULL
)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_TranslationDef_TableName_TableId_Field_Lang] ON [common].[TranslationDef] ([TableName] ASC, [TableId] ASC, [TableFieldName] ASC, [LanguageCode] ASC) WHERE ([IsDelete]=(0))
ON [PRIMARY]
GO

-- Definition for table LabelDef :
CREATE TABLE [common].[LabelDef] (
  [Id] bigint IDENTITY(1, 1) NOT NULL,
  [GId] uniqueidentifier DEFAULT (newid()) NOT NULL,
  [CompanyId] bigint NOT NULL,
  [Stat] bit DEFAULT ((1)) NOT NULL,
  [Label] nvarchar(max) COLLATE Turkish_CI_AI NULL,
  [LabelDescription] nvarchar(100) COLLATE Turkish_CI_AI NULL,
  [SeoKey] nvarchar(max) COLLATE Turkish_CI_AI NULL,
  [IsMainCategory] bit NULL,
  [Order] int NULL,
  [CreatedDate] datetime DEFAULT (getdate()) NOT NULL,
  [CreatedUser] bigint DEFAULT ((0)) NOT NULL,
  [ModifedDate] datetime NULL,
  [ModifedUser] bigint NULL,
  [DeletedDate] datetime NULL,
  [DeletedUser] bigint NULL,
  [IsDelete] bit DEFAULT ((0)) NOT NULL,
  CONSTRAINT [LabelDef_pk] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IDX_LabelDef_CompanyId_IsDelete] ON [common].[LabelDef] ([CompanyId] ASC, [IsDelete] ASC) INCLUDE ([Order], [CreatedDate], [CreatedUser], [DeletedDate], [DeletedUser], [GId], [IsMainCategory], [Label], [LabelDescription], [ModifedDate], [ModifedUser], [SeoKey], [Stat])
ON [PRIMARY]
GO

-- Definition for table LabelPool :
CREATE TABLE [common].[LabelPool] (
  [Id] bigint IDENTITY(1, 1) NOT NULL,
  [GId] uniqueidentifier DEFAULT (newid()) NOT NULL,
  [CompanyId] bigint NOT NULL,
  [TableName] nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  [TableId] bigint NOT NULL,
  [Label] nvarchar(50) COLLATE Turkish_CI_AI NOT NULL,
  [LabelDefId] bigint NULL,
  [CreatedDate] datetime DEFAULT (getdate()) NOT NULL,
  [CreatedUser] bigint DEFAULT ((0)) NOT NULL,
  [ModifedDate] datetime NULL,
  [ModifedUser] bigint NULL,
  [DeletedDate] datetime NULL,
  [DeletedUser] bigint NULL,
  [IsDelete] bit DEFAULT ((0)) NOT NULL,
  CONSTRAINT [PK__LabelPoo__3214EC076B31C324] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IDX_LabelPool_CompanyId_IsDelete] ON [common].[LabelPool] ([CompanyId] ASC, [IsDelete] ASC) INCLUDE ([CreatedDate], [CreatedUser], [DeletedDate], [DeletedUser], [GId], [Label], [LabelDefId], [ModifedDate], [ModifedUser], [TableId], [TableName])
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IDX_LabelPool_LabelDefId_IsDelete] ON [common].[LabelPool] ([LabelDefId] ASC, [IsDelete] ASC) INCLUDE ([CompanyId], [GId], [Label], [TableId], [TableName])
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IDX_LabelPool_TableName_TableId_IsDelete] ON [common].[LabelPool] ([TableName] ASC, [TableId] ASC, [IsDelete] ASC) INCLUDE ([CompanyId], [GId], [Label], [LabelDefId])
ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [LabelPool_uq] ON [common].[LabelPool] ([TableName] ASC, [TableId] ASC, [Label] ASC, [DeletedDate] ASC)
ON [PRIMARY]
GO

-- Definition for table AppSettings :
CREATE TABLE [common].[AppSettings] (
  [Id] bigint IDENTITY(1, 1) NOT NULL,
  [GId] uniqueidentifier DEFAULT (newid()) NOT NULL,
  [AppCode] nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  [MaxStayInsideHour] int DEFAULT ((8)) NOT NULL,
  [IOSAppUrl] nvarchar(500) COLLATE Turkish_CI_AI NULL,
  [AndroidAppUrl] nvarchar(500) COLLATE Turkish_CI_AI NULL,
  [CreatedDate] datetime DEFAULT (getdate()) NOT NULL,
  [CreatedUser] bigint DEFAULT ((0)) NOT NULL,
  [ModifedDate] datetime NULL,
  [ModifedUser] bigint NULL,
  [DeletedDate] datetime NULL,
  [DeletedUser] bigint NULL,
  [IsDelete] bit DEFAULT ((0)) NOT NULL,
  CONSTRAINT [AppSettings_pk] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

-- Definition for table AppExceptionLog :
CREATE TABLE [common].[AppExceptionLog] (
  [Id] bigint IDENTITY(1, 1) NOT NULL,
  [GId] uniqueidentifier DEFAULT (newid()) NOT NULL,
  [CompanyId] bigint NULL,
  [PersonalId] bigint NULL,
  [DeviceId] nvarchar(500) COLLATE Turkish_CI_AI NOT NULL,
  [ClientOs] nvarchar(100) COLLATE Turkish_CI_AI NULL,
  [AppVersion] nvarchar(100) COLLATE Turkish_CI_AI NULL,
  [Language] nvarchar(10) COLLATE Turkish_CI_AI NULL,
  [IpAddress] nvarchar(60) COLLATE Turkish_CI_AI NULL,
  [ExceptionType] nvarchar(200) COLLATE Turkish_CI_AI NULL,
  [ExceptionMessage] nvarchar(max) COLLATE Turkish_CI_AI NULL,
  [StackTrace] nvarchar(max) COLLATE Turkish_CI_AI NULL,
  [Source] nvarchar(500) COLLATE Turkish_CI_AI NULL,
  [PageOrScreen] nvarchar(200) COLLATE Turkish_CI_AI NULL,
  [ActionOrMethod] nvarchar(200) COLLATE Turkish_CI_AI NULL,
  [RequestUrl] nvarchar(2000) COLLATE Turkish_CI_AI NULL,
  [RequestMethod] nvarchar(10) COLLATE Turkish_CI_AI NULL,
  [SeverityLevel] nvarchar(20) COLLATE Turkish_CI_AI DEFAULT (N'Error') NULL,
  [AdditionalData] nvarchar(max) COLLATE Turkish_CI_AI NULL,
  [OccurredAt] datetime DEFAULT (getdate()) NOT NULL,
  [Stat] bit DEFAULT ((1)) NOT NULL,
  [CreatedDate] datetime DEFAULT (getdate()) NOT NULL,
  [CreatedUser] bigint DEFAULT ((0)) NOT NULL,
  [ModifedDate] datetime NULL,
  [ModifedUser] bigint NULL,
  [DeletedDate] datetime NULL,
  [DeletedUser] bigint NULL,
  [IsDelete] bit DEFAULT ((0)) NOT NULL,
  CONSTRAINT [PK_AppExceptionLog] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_AppExceptionLog_OccurredAt] ON [common].[AppExceptionLog] ([OccurredAt] DESC) INCLUDE ([CompanyId], [PersonalId], [ClientOs], [ExceptionType], [SeverityLevel]) WHERE ([IsDelete]=(0))
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_AppExceptionLog_CompanyId_OccurredAt] ON [common].[AppExceptionLog] ([CompanyId] ASC, [OccurredAt] DESC) INCLUDE ([PersonalId], [ClientOs], [ExceptionType]) WHERE ([IsDelete]=(0))
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_AppExceptionLog_DeviceId_OccurredAt] ON [common].[AppExceptionLog] ([DeviceId] ASC, [OccurredAt] DESC) WHERE ([IsDelete]=(0))
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_AppExceptionLog_PersonalId_OccurredAt] ON [common].[AppExceptionLog] ([PersonalId] ASC, [OccurredAt] DESC) WHERE ([IsDelete]=(0) AND [PersonalId] IS NOT NULL)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_AppExceptionLog_ExceptionType] ON [common].[AppExceptionLog] ([ExceptionType] ASC) INCLUDE ([OccurredAt], [CompanyId], [ClientOs]) WHERE ([IsDelete]=(0))
ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_AppExceptionLog_GId] ON [common].[AppExceptionLog] ([GId] ASC)
ON [PRIMARY]
GO

-- Definition for table MailLog :
CREATE TABLE [common].[MailLog] (
  [Id] bigint IDENTITY(1, 1) NOT NULL,
  [GId] uniqueidentifier DEFAULT (newid()) NOT NULL,
  [CompanyId] bigint NOT NULL,
  [TableName] nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  [TableId] bigint NOT NULL,
  [MailTypeCode] nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  [MailTo] nvarchar(500) COLLATE Turkish_CI_AI NOT NULL,
  [MailSubject] nvarchar(500) COLLATE Turkish_CI_AI NULL,
  [MailBody] nvarchar(max) COLLATE Turkish_CI_AI NULL,
  [SentDate] datetime DEFAULT (getdate()) NOT NULL,
  [SentByPersonalId] bigint NULL,
  [Stat] bit DEFAULT ((1)) NOT NULL,
  [CreatedDate] datetime DEFAULT (getdate()) NOT NULL,
  [CreatedUser] bigint DEFAULT ((0)) NOT NULL,
  [ModifedDate] datetime NULL,
  [ModifedUser] bigint NULL,
  [DeletedDate] datetime NULL,
  [DeletedUser] bigint NULL,
  [IsDelete] bit DEFAULT ((0)) NOT NULL,
  CONSTRAINT [PK_MailLog] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_MailLog_TableName_TableId] ON [common].[MailLog] ([TableName] ASC, [TableId] ASC) INCLUDE ([MailTypeCode], [SentDate], [MailTo], [CompanyId]) WHERE ([IsDelete]=(0))
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_MailLog_CompanyId_SentDate] ON [common].[MailLog] ([CompanyId] ASC, [SentDate] DESC) INCLUDE ([TableName], [TableId], [MailTypeCode], [MailTo]) WHERE ([IsDelete]=(0))
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_MailLog_MailTypeCode] ON [common].[MailLog] ([MailTypeCode] ASC) INCLUDE ([CompanyId], [TableName], [TableId], [SentDate]) WHERE ([IsDelete]=(0))
ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_MailLog_GId] ON [common].[MailLog] ([GId] ASC)
ON [PRIMARY]
GO

-- Definition for table GroupDef :
CREATE TABLE [common].[GroupDef] (
  [Id] bigint IDENTITY(1, 1) NOT NULL,
  [GId] uniqueidentifier DEFAULT (newid()) NOT NULL,
  [Stat] bit DEFAULT ((1)) NOT NULL,
  [CompanyId] bigint NOT NULL,
  [Code] nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  [Description] nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  [Order] int DEFAULT ((0)) NOT NULL,
  [CreatedDate] datetime DEFAULT (getdate()) NOT NULL,
  [CreatedUser] bigint DEFAULT ((0)) NOT NULL,
  [ModifedDate] datetime NULL,
  [ModifedUser] bigint NULL,
  [DeletedDate] datetime NULL,
  [DeletedUser] bigint NULL,
  [IsDelete] bigint DEFAULT ((0)) NOT NULL,
  CONSTRAINT [PK_GroupDef] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_GroupDef_GId] ON [common].[GroupDef] ([GId] ASC)
ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_GroupDef_Company_Code_DeletedDate] ON [common].[GroupDef] ([CompanyId] ASC, [Code] ASC, [DeletedDate] ASC)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_GroupDef_CompanyId_IsDelete] ON [common].[GroupDef] ([CompanyId] ASC, [IsDelete] ASC) INCLUDE ([Code], [Description], [Stat])
ON [PRIMARY]
GO

-- Definition for table GroupDefCountry :
CREATE TABLE [common].[GroupDefCountry] (
  [Id] bigint IDENTITY(1, 1) NOT NULL,
  [GId] uniqueidentifier DEFAULT (newid()) NOT NULL,
  [Stat] bit DEFAULT ((1)) NOT NULL,
  [CompanyId] bigint NOT NULL,
  [GroupDefId] bigint NOT NULL,
  [CountryCode] nvarchar(10) COLLATE Turkish_CI_AI NOT NULL,
  [CreatedDate] datetime DEFAULT (getdate()) NOT NULL,
  [CreatedUser] bigint DEFAULT ((0)) NOT NULL,
  [ModifedDate] datetime NULL,
  [ModifedUser] bigint NULL,
  [DeletedDate] datetime NULL,
  [DeletedUser] bigint NULL,
  [IsDelete] bigint DEFAULT ((0)) NOT NULL,
  CONSTRAINT [PK_GroupDefCountry] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_GroupDefCountry_GId] ON [common].[GroupDefCountry] ([GId] ASC)
ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_GroupDefCountry_Group_CountryCode_DeletedDate] ON [common].[GroupDefCountry] ([GroupDefId] ASC, [CountryCode] ASC, [DeletedDate] ASC)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_GroupDefCountry_CompanyId_IsDelete] ON [common].[GroupDefCountry] ([CompanyId] ASC, [IsDelete] ASC) INCLUDE ([GroupDefId], [CountryCode], [Stat])
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_GroupDefCountry_CountryCode] ON [common].[GroupDefCountry] ([CountryCode] ASC) INCLUDE ([CompanyId], [GroupDefId], [Stat]) WHERE ([IsDelete]=(0))
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_GroupDefCountry_GroupDefId] ON [common].[GroupDefCountry] ([GroupDefId] ASC) INCLUDE ([CompanyId], [CountryCode], [Stat]) WHERE ([IsDelete]=(0))
ON [PRIMARY]
GO

-- Definition for table CountryHolidays :
CREATE TABLE [common].[CountryHolidays] (
  [Id] bigint IDENTITY(1, 1) NOT NULL,
  [GId] uniqueidentifier DEFAULT (newid()) NOT NULL,
  [Stat] bit DEFAULT ((1)) NOT NULL,
  [CountryCode] nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  [AppCode] nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  [CompanyId] bigint NOT NULL,
  [HolidayDate] date NOT NULL,
  [HolidayName] nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  [HolidayType] nvarchar(30) COLLATE Turkish_CI_AI DEFAULT ('NATIONAL') NOT NULL,
  [CreatedDate] datetime DEFAULT (getdate()) NOT NULL,
  [CreatedUser] bigint DEFAULT ((0)) NOT NULL,
  [ModifedDate] datetime NULL,
  [ModifedUser] bigint NULL,
  [DeletedDate] datetime NULL,
  [DeletedUser] bigint NULL,
  [IsDelete] bit DEFAULT ((0)) NOT NULL,
  CONSTRAINT [CountryHolidays_pk] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

-- Definition for table PersonalLoginActivity :
CREATE TABLE [common].[PersonalLoginActivity] (
  [Id] bigint IDENTITY(1, 1) NOT NULL,
  [GId] uniqueidentifier DEFAULT (newid()) NOT NULL,
  [PersonalId] bigint NOT NULL,
  [ActivityType] nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  [IsSuccess] bit DEFAULT ((1)) NOT NULL,
  [CustomHeaderJson] nvarchar(max) COLLATE Turkish_CI_AI NULL,
  [DeviceId] nvarchar(500) COLLATE Turkish_CI_AI NOT NULL,
  [ClientOs] nvarchar(100) COLLATE Turkish_CI_AI NULL,
  [AppVersion] nvarchar(100) COLLATE Turkish_CI_AI NULL,
  [Language] nvarchar(10) COLLATE Turkish_CI_AI DEFAULT ('tr-TR') NOT NULL,
  [IpAddress] nvarchar(60) COLLATE Turkish_CI_AI NULL,
  [CreatedDate] datetime DEFAULT (getdate()) NOT NULL,
  CONSTRAINT [PK_PersonalLoginActivity] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [PersonalLoginActivity_uq] ON [common].[PersonalLoginActivity] ([GId] ASC)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_PersonalLoginActivity_PersonalId_CreatedDate] ON [common].[PersonalLoginActivity] ([PersonalId] ASC, [CreatedDate] DESC) INCLUDE ([ActivityType], [IsSuccess], [DeviceId], [ClientOs])
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_PersonalLoginActivity_DeviceId_CreatedDate] ON [common].[PersonalLoginActivity] ([DeviceId] ASC, [CreatedDate] DESC) INCLUDE ([PersonalId], [ActivityType], [IsSuccess])
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_PersonalLoginActivity_ActivityType_Success] ON [common].[PersonalLoginActivity] ([ActivityType] ASC, [IsSuccess] ASC, [CreatedDate] DESC) INCLUDE ([PersonalId], [DeviceId])
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_PersonalLoginActivity_CreatedDate] ON [common].[PersonalLoginActivity] ([CreatedDate] DESC) INCLUDE ([PersonalId], [ActivityType], [IsSuccess], [DeviceId])
ON [PRIMARY]
GO

-- Definition for table PersonalWidget :
CREATE TABLE [common].[PersonalWidget] (
  [Id] bigint IDENTITY(1, 1) NOT NULL,
  [GId] uniqueidentifier DEFAULT (newid()) NOT NULL,
  [Stat] bit DEFAULT ((1)) NOT NULL,
  [PersonalId] bigint NOT NULL,
  [WidgetId] nvarchar(50) COLLATE Turkish_CI_AI NOT NULL,
  [With] varchar(30) COLLATE Turkish_CI_AI DEFAULT ((1)) NULL,
  [IsVisible] bit DEFAULT ((1)) NOT NULL,
  [Position] int DEFAULT ((0)) NOT NULL,
  [RowIndex] int DEFAULT ((0)) NOT NULL,
  [CreatedDate] datetime2(0) DEFAULT (getdate()) NOT NULL,
  [CreatedUser] bigint NOT NULL,
  [ModifedDate] datetime2(0) NULL,
  [ModifedUser] bigint NULL,
  [DeletedDate] datetime2(0) NULL,
  [DeletedUser] bigint NULL,
  [IsDelete] bigint DEFAULT ((0)) NOT NULL,
  CONSTRAINT [PK__Personal__3214EC075D5E303F] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_PersonalWidget] ON [common].[PersonalWidget] ([PersonalId] ASC, [WidgetId] ASC)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_PersonalWidget_GId] ON [common].[PersonalWidget] ([GId] ASC)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_PersonalWidget_PersonalId] ON [common].[PersonalWidget] ([PersonalId] ASC)
ON [PRIMARY]
GO

-- Definition for table ProcessTypePersonal :
CREATE TABLE [common].[ProcessTypePersonal] (
  [Id] bigint IDENTITY(1, 1) NOT NULL,
  [GId] uniqueidentifier DEFAULT (newid()) NOT NULL,
  [CompanyId] bigint NOT NULL,
  [ProcessTypeId] bigint NOT NULL,
  [PersonalId] bigint NOT NULL,
  [Stat] bit DEFAULT ((1)) NOT NULL,
  [CreatedDate] datetime DEFAULT (getdate()) NOT NULL,
  [CreatedUser] bigint DEFAULT ((0)) NOT NULL,
  [ModifedDate] datetime NULL,
  [ModifedUser] bigint NULL,
  [DeletedDate] datetime NULL,
  [DeletedUser] bigint NULL,
  [IsDelete] bit DEFAULT ((0)) NOT NULL,
  CONSTRAINT [ProcessTypePersonal_pk] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

-- Definition for table PersonalGroup :
CREATE TABLE [common].[PersonalGroup] (
  [Id] bigint IDENTITY(1, 1) NOT NULL,
  [GId] uniqueidentifier DEFAULT (newid()) NOT NULL,
  [CompanyId] bigint NULL,
  [PersonalId] bigint NOT NULL,
  [GroupCode] int NOT NULL,
  [CreatedDate] datetime DEFAULT (getdate()) NOT NULL,
  [CreatedUser] bigint DEFAULT ((0)) NOT NULL,
  [ModifedDate] datetime NULL,
  [ModifedUser] bigint NULL,
  [DeletedDate] datetime NULL,
  [DeletedUser] bigint NULL,
  [IsDelete] bigint DEFAULT ((0)) NOT NULL
)
ON [PRIMARY]
GO

-- Definition for table PersonalGroupDef :
CREATE TABLE [common].[PersonalGroupDef] (
  [Id] bigint IDENTITY(1, 1) NOT NULL,
  [GId] uniqueidentifier DEFAULT (newid()) NOT NULL,
  [Stat] bit DEFAULT ((1)) NOT NULL,
  [CompanyId] bigint NOT NULL,
  [PersonalId] bigint NOT NULL,
  [GroupDefId] bigint NOT NULL,
  [CreatedDate] datetime DEFAULT (getdate()) NOT NULL,
  [CreatedUser] bigint DEFAULT ((0)) NOT NULL,
  [ModifedDate] datetime NULL,
  [ModifedUser] bigint NULL,
  [DeletedDate] datetime NULL,
  [DeletedUser] bigint NULL,
  [IsDelete] bigint DEFAULT ((0)) NOT NULL,
  CONSTRAINT [PK_PersonalGroupDef] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_PersonalGroupDef_GId] ON [common].[PersonalGroupDef] ([GId] ASC)
ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_PersonalGroupDef_Personal_Group_DeletedDate] ON [common].[PersonalGroupDef] ([PersonalId] ASC, [GroupDefId] ASC, [DeletedDate] ASC)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_PersonalGroupDef_CompanyId_IsDelete] ON [common].[PersonalGroupDef] ([CompanyId] ASC, [IsDelete] ASC) INCLUDE ([PersonalId], [GroupDefId], [Stat])
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_PersonalGroupDef_PersonalId] ON [common].[PersonalGroupDef] ([PersonalId] ASC) INCLUDE ([CompanyId], [GroupDefId], [Stat]) WHERE ([IsDelete]=(0))
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_PersonalGroupDef_GroupDefId] ON [common].[PersonalGroupDef] ([GroupDefId] ASC) INCLUDE ([CompanyId], [PersonalId], [Stat]) WHERE ([IsDelete]=(0))
ON [PRIMARY]
GO

-- Definition for table AspNetRoleClaims :
CREATE TABLE [dbo].[AspNetRoleClaims] (
  [Id] int IDENTITY(1, 1) NOT NULL,
  [RoleId] nvarchar(450) COLLATE Turkish_CI_AI NOT NULL,
  [ClaimType] nvarchar(max) COLLATE Turkish_CI_AI NULL,
  [ClaimValue] nvarchar(max) COLLATE Turkish_CI_AI NULL,
  [Discriminator] nvarchar(128) COLLATE Turkish_CI_AI DEFAULT ('AspNetRoleClaims') NOT NULL,
  CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims] ([RoleId] ASC)
ON [PRIMARY]
GO

-- Definition for table AspNetUserClaims :
CREATE TABLE [dbo].[AspNetUserClaims] (
  [Id] int IDENTITY(1, 1) NOT NULL,
  [UserId] nvarchar(450) COLLATE Turkish_CI_AI NOT NULL,
  [ClaimType] nvarchar(max) COLLATE Turkish_CI_AI NULL,
  [ClaimValue] nvarchar(max) COLLATE Turkish_CI_AI NULL,
  [Discriminator] nvarchar(128) COLLATE Turkish_CI_AI DEFAULT ('AspNetUserClaims') NOT NULL,
  CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims] ([UserId] ASC)
ON [PRIMARY]
GO

-- Definition for table AspNetUserLogins :
CREATE TABLE [dbo].[AspNetUserLogins] (
  [LoginProvider] nvarchar(450) COLLATE Turkish_CI_AI NOT NULL,
  [ProviderKey] nvarchar(450) COLLATE Turkish_CI_AI NOT NULL,
  [ProviderDisplayName] nvarchar(max) COLLATE Turkish_CI_AI NULL,
  [UserId] nvarchar(450) COLLATE Turkish_CI_AI NOT NULL,
  [Discriminator] nvarchar(128) COLLATE Turkish_CI_AI DEFAULT ('AspNetUserLogins') NOT NULL,
  CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey])
)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins] ([UserId] ASC)
ON [PRIMARY]
GO

-- Definition for table AspNetUserTokens :
CREATE TABLE [dbo].[AspNetUserTokens] (
  [UserId] nvarchar(450) COLLATE Turkish_CI_AI NOT NULL,
  [LoginProvider] nvarchar(450) COLLATE Turkish_CI_AI NOT NULL,
  [Name] nvarchar(450) COLLATE Turkish_CI_AI NOT NULL,
  [Value] nvarchar(max) COLLATE Turkish_CI_AI NULL,
  [ExpireDate] datetime2(7) NULL,
  [Discriminator] nvarchar(128) COLLATE Turkish_CI_AI DEFAULT ('AspNetUserTokens') NOT NULL,
  CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED ([UserId], [LoginProvider], [Name])
)
ON [PRIMARY]
GO

ALTER TABLE [common].[AppExceptionLog] ADD CONSTRAINT [FK_AppExceptionLog_Company] FOREIGN KEY ([CompanyId]) REFERENCES [common].[Company] ([Id])
GO

ALTER TABLE [common].[AppExceptionLog] ADD CONSTRAINT [FK_AppExceptionLog_Personal] FOREIGN KEY ([PersonalId]) REFERENCES [common].[Personal] ([Id])
GO

ALTER TABLE [dbo].[AspNetRoleClaims] ADD CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserClaims] ADD CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserLogins] ADD CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserTokens] ADD CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [common].[GroupDef] ADD CONSTRAINT [FK_GroupDef_Company] FOREIGN KEY ([CompanyId]) REFERENCES [common].[Company] ([Id])
GO

ALTER TABLE [common].[GroupDefCountry] ADD CONSTRAINT [FK_GroupDefCountry_Company] FOREIGN KEY ([CompanyId]) REFERENCES [common].[Company] ([Id])
GO

ALTER TABLE [common].[GroupDefCountry] ADD CONSTRAINT [FK_GroupDefCountry_GroupDef] FOREIGN KEY ([GroupDefId]) REFERENCES [common].[GroupDef] ([Id])
GO

ALTER TABLE [common].[MailLog] ADD CONSTRAINT [FK_MailLog_Company] FOREIGN KEY ([CompanyId]) REFERENCES [common].[Company] ([Id])
GO

ALTER TABLE [common].[MailLog] ADD CONSTRAINT [FK_MailLog_SentByPersonal] FOREIGN KEY ([SentByPersonalId]) REFERENCES [common].[Personal] ([Id])
GO

ALTER TABLE [common].[PersonalGroupDef] ADD CONSTRAINT [FK_PersonalGroupDef_Company] FOREIGN KEY ([CompanyId]) REFERENCES [common].[Company] ([Id])
GO

ALTER TABLE [common].[PersonalGroupDef] ADD CONSTRAINT [FK_PersonalGroupDef_GroupDef] FOREIGN KEY ([GroupDefId]) REFERENCES [common].[GroupDef] ([Id])
GO

ALTER TABLE [common].[PersonalGroupDef] ADD CONSTRAINT [FK_PersonalGroupDef_Personal] FOREIGN KEY ([PersonalId]) REFERENCES [common].[Personal] ([Id])
GO

ALTER TABLE [common].[PersonalWidget] ADD CONSTRAINT [FK_PersonalWidget_Personal] FOREIGN KEY ([PersonalId]) REFERENCES [common].[Personal] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [common].[ProcessFlow] ADD CONSTRAINT [FK_ProcessFlow_Moduls] FOREIGN KEY ([ModulId]) REFERENCES [common].[Moduls] ([Id])
GO

ALTER TABLE [common].[LabelPool] ADD CONSTRAINT [LabelPool_fk_LabelDef] FOREIGN KEY ([LabelDefId]) REFERENCES [common].[LabelDef] ([Id])
GO

ALTER TABLE [common].[PersonalBranch] ADD CONSTRAINT [PersonalBranch_Branch_fk] FOREIGN KEY ([BranchId]) REFERENCES [common].[Branch] ([Id])
GO

ALTER TABLE [common].[PersonalBranch] ADD CONSTRAINT [PersonalBranch_Personal_fk] FOREIGN KEY ([PersonalId]) REFERENCES [common].[Personal] ([Id])
GO

ALTER TABLE [common].[PersonalCompany] ADD CONSTRAINT [PersonalCompany_fk] FOREIGN KEY ([PersonalId]) REFERENCES [common].[Personal] ([Id])
GO

ALTER TABLE [common].[PersonalCompany] ADD CONSTRAINT [PersonalCompany_fk2] FOREIGN KEY ([CompanyId]) REFERENCES [common].[Company] ([Id])
GO

ALTER TABLE [common].[PersonalGroup] ADD CONSTRAINT [PersonalGroup_fk] FOREIGN KEY ([CompanyId]) REFERENCES [common].[Company] ([Id])
GO

ALTER TABLE [common].[PersonalGroup] ADD CONSTRAINT [PersonalGroup_fk2] FOREIGN KEY ([PersonalId]) REFERENCES [common].[Personal] ([Id])
GO

ALTER TABLE [common].[PersonalLoginActivity] ADD CONSTRAINT [PersonalLoginActivity_fk] FOREIGN KEY ([PersonalId]) REFERENCES [common].[Personal] ([Id])
GO

ALTER TABLE [common].[ProcessTypePersonal] ADD CONSTRAINT [ProcessTypePersonal_fk] FOREIGN KEY ([CompanyId]) REFERENCES [common].[Company] ([Id])
GO

ALTER TABLE [common].[ProcessTypePersonal] ADD CONSTRAINT [ProcessTypePersonal_fk_Personal] FOREIGN KEY ([PersonalId]) REFERENCES [common].[Personal] ([Id])
GO

ALTER TABLE [common].[ProcessTypePersonal] ADD CONSTRAINT [ProcessTypePersonal_fk2] FOREIGN KEY ([ProcessTypeId]) REFERENCES [common].[ProcessType] ([Id])
GO

ALTER TABLE [common].[TranslationDef] ADD CONSTRAINT [TranslationDef_fk] FOREIGN KEY ([CompanyId]) REFERENCES [common].[Company] ([Id])
GO

-- AI platform MS_Description
EXEC dbo._SetFullSchemaDescription N'common', N'Branch', NULL, N'AI platform tablosu. Sube / magaza / lokasyon tanimi. CompanyMasterCode ile tenant baglidir.';
EXEC dbo._SetFullSchemaDescription N'common', N'Branch', N'Id', N'Primary key.';
EXEC dbo._SetFullSchemaDescription N'common', N'Branch', N'GId', N'Dis kimlik (Guid).';
EXEC dbo._SetFullSchemaDescription N'common', N'Branch', N'Stat', N'Aktif/pasif.';
EXEC dbo._SetFullSchemaDescription N'common', N'Branch', N'CompanyMasterCode', N'Sirket master kodu (Company.CompanyMasterCode).';
EXEC dbo._SetFullSchemaDescription N'common', N'Branch', N'Code', N'Sube kodu; (CompanyMasterCode, Code, DeletedDate) unique.';
EXEC dbo._SetFullSchemaDescription N'common', N'Branch', N'Description', N'Sube adi / aciklama.';
EXEC dbo._SetFullSchemaDescription N'common', N'Branch', N'ManagerPersonalId', N'Sube muduru Personal.Id (opsiyonel).';
EXEC dbo._SetFullSchemaDescription N'common', N'Branch', N'ManagerAccountCode', N'Sube muduru hesap/kod referansi (legacy/entegrasyon).';
EXEC dbo._SetFullSchemaDescription N'common', N'Branch', N'Country', N'Ulke adi veya kod metni (gosterim).';
EXEC dbo._SetFullSchemaDescription N'common', N'Branch', N'City', N'Sehir adi (gosterim).';
EXEC dbo._SetFullSchemaDescription N'common', N'Branch', N'District', N'Ilce / semt.';
EXEC dbo._SetFullSchemaDescription N'common', N'Branch', N'CityCode', N'Sehir kodu (City.FullCityCode / CityCode baglantisi).';
EXEC dbo._SetFullSchemaDescription N'common', N'Branch', N'DistrictCode', N'Ilce kodu.';
EXEC dbo._SetFullSchemaDescription N'common', N'Branch', N'Latitude', N'Enlem (GPS).';
EXEC dbo._SetFullSchemaDescription N'common', N'Branch', N'Longitude', N'Boylam (GPS).';
EXEC dbo._SetFullSchemaDescription N'common', N'Branch', N'StartIP', N'Izinli IP araligi baslangici (opsiyonel).';
EXEC dbo._SetFullSchemaDescription N'common', N'Branch', N'EndIP', N'Izinli IP araligi bitisi (opsiyonel).';
EXEC dbo._SetFullSchemaDescription N'common', N'Branch', N'Color', N'UI renk / badge JSON veya hex.';
EXEC dbo._SetFullSchemaDescription N'common', N'Branch', N'Label', N'Etiket / serbest metin veya JSON label listesi.';
EXEC dbo._SetFullSchemaDescription N'common', N'Branch', N'CreatedDate', N'Olusturma tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'Branch', N'CreatedUser', N'Olusturan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'Branch', N'ModifedDate', N'Guncelleme tarihi (Modifed typo korunur).';
EXEC dbo._SetFullSchemaDescription N'common', N'Branch', N'ModifedUser', N'Guncelleyen kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'Branch', N'DeletedDate', N'Soft-delete tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'Branch', N'DeletedUser', N'Soft-delete yapan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'Branch', N'IsDelete', N'Soft-delete bayragi (bigint).';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', NULL, N'AI platform tablosu. Uygulama / istemci hata loglari (WebAPI, WebUI, Mobil).';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'Id', N'Primary key.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'GId', N'Dis kimlik (Guid).';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'CompanyId', N'Sirket FK (opsiyonel).';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'PersonalId', N'Ilgili personel FK (opsiyonel).';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'DeviceId', N'Cihaz kimligi (CustomHeader).';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'ClientOs', N'Istemci isletim sistemi.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'AppVersion', N'Uygulama surumu.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'Language', N'Istemci dil kodu (or. tr-TR).';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'IpAddress', N'Istemci IP.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'ExceptionType', N'Exception tip adi.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'ExceptionMessage', N'Hata mesaji.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'StackTrace', N'Stack trace.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'Source', N'Kaynak (assembly / modul).';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'PageOrScreen', N'Sayfa veya ekran adi.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'ActionOrMethod', N'Action / method adi.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'RequestUrl', N'HTTP istek URL.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'RequestMethod', N'HTTP method (GET/POST/...).';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'SeverityLevel', N'Siddet (Error/Warning/Info...).';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'AdditionalData', N'Ek JSON / serbest veri.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'OccurredAt', N'Hatanin olustugu zaman.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'Stat', N'Aktif/pasif.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'CreatedDate', N'Kayit olusturma.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'CreatedUser', N'Olusturan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'ModifedDate', N'Guncelleme tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'ModifedUser', N'Guncelleyen kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'DeletedDate', N'Soft-delete tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'DeletedUser', N'Soft-delete yapan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppExceptionLog', N'IsDelete', N'Soft-delete bayragi (bit).';
EXEC dbo._SetFullSchemaDescription N'common', N'AppSettings', NULL, N'AI platform tablosu. Uygulama genel ayarlari (mobil magaza URL, sure sinirlari vb.).';
EXEC dbo._SetFullSchemaDescription N'common', N'AppSettings', N'Id', N'Primary key.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppSettings', N'GId', N'Dis kimlik (Guid).';
EXEC dbo._SetFullSchemaDescription N'common', N'AppSettings', N'AppCode', N'Uygulama kodu (WEB/MOBILE/urun).';
EXEC dbo._SetFullSchemaDescription N'common', N'AppSettings', N'MaxStayInsideHour', N'Maksimum icerde kalma suresi (saat).';
EXEC dbo._SetFullSchemaDescription N'common', N'AppSettings', N'IOSAppUrl', N'iOS magaza / indir URL.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppSettings', N'AndroidAppUrl', N'Android magaza / indir URL.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppSettings', N'CreatedDate', N'Olusturma tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppSettings', N'CreatedUser', N'Olusturan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppSettings', N'ModifedDate', N'Guncelleme tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppSettings', N'ModifedUser', N'Guncelleyen kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppSettings', N'DeletedDate', N'Soft-delete tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppSettings', N'DeletedUser', N'Soft-delete yapan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'AppSettings', N'IsDelete', N'Soft-delete bayragi (bit).';
EXEC dbo._SetFullSchemaDescription N'common', N'CodeDef', NULL, N'AI platform tablosu. Genel kod tablosu (Prefix + Code). Lookup / enum yerine DB tanimlari.';
EXEC dbo._SetFullSchemaDescription N'common', N'CodeDef', N'Id', N'Primary key.';
EXEC dbo._SetFullSchemaDescription N'common', N'CodeDef', N'GId', N'Dis kimlik (Guid).';
EXEC dbo._SetFullSchemaDescription N'common', N'CodeDef', N'Stat', N'Aktif/pasif.';
EXEC dbo._SetFullSchemaDescription N'common', N'CodeDef', N'Prefix', N'Kod grubu / kategori (or. GENDER, CURRENCY).';
EXEC dbo._SetFullSchemaDescription N'common', N'CodeDef', N'Code', N'Kod degeri.';
EXEC dbo._SetFullSchemaDescription N'common', N'CodeDef', N'Description', N'Kod aciklamasi (TR varsayilan).';
EXEC dbo._SetFullSchemaDescription N'common', N'CodeDef', N'CompanyId', N'Sirket Id (tenant).';
EXEC dbo._SetFullSchemaDescription N'common', N'CodeDef', N'CreatedDate', N'Olusturma tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'CodeDef', N'CreatedUser', N'Olusturan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'CodeDef', N'ModifedDate', N'Guncelleme tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'CodeDef', N'ModifedUser', N'Guncelleyen kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'CodeDef', N'DeletedDate', N'Soft-delete tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'CodeDef', N'DeletedUser', N'Soft-delete yapan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'CodeDef', N'IsDelete', N'Soft-delete bayragi (bigint).';
EXEC dbo._SetFullSchemaDescription N'common', N'Country', NULL, N'AI platform tablosu. Sirkete bagli ulke listesi.';
EXEC dbo._SetFullSchemaDescription N'common', N'Country', N'Id', N'Primary key.';
EXEC dbo._SetFullSchemaDescription N'common', N'Country', N'GId', N'Dis kimlik (Guid).';
EXEC dbo._SetFullSchemaDescription N'common', N'Country', N'CompanyId', N'Sirket Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'Country', N'CountryCode', N'Ulke kodu.';
EXEC dbo._SetFullSchemaDescription N'common', N'Country', N'Stat', N'Aktif/pasif.';
EXEC dbo._SetFullSchemaDescription N'common', N'Country', N'Description', N'Ulke adi.';
EXEC dbo._SetFullSchemaDescription N'common', N'Country', N'Ordered', N'Siralama.';
EXEC dbo._SetFullSchemaDescription N'common', N'CountryHolidays', NULL, N'AI platform tablosu. Ulke / uygulama tatil takvimi.';
EXEC dbo._SetFullSchemaDescription N'common', N'CountryHolidays', N'Id', N'Primary key.';
EXEC dbo._SetFullSchemaDescription N'common', N'CountryHolidays', N'GId', N'Dis kimlik (Guid).';
EXEC dbo._SetFullSchemaDescription N'common', N'CountryHolidays', N'Stat', N'Aktif/pasif.';
EXEC dbo._SetFullSchemaDescription N'common', N'CountryHolidays', N'CountryCode', N'Ulke kodu.';
EXEC dbo._SetFullSchemaDescription N'common', N'CountryHolidays', N'AppCode', N'Uygulama kodu.';
EXEC dbo._SetFullSchemaDescription N'common', N'CountryHolidays', N'CompanyId', N'Sirket Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'CountryHolidays', N'HolidayDate', N'Tatil tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'CountryHolidays', N'HolidayName', N'Tatil adi.';
EXEC dbo._SetFullSchemaDescription N'common', N'CountryHolidays', N'HolidayType', N'Tatil tipi (NATIONAL vb.).';
EXEC dbo._SetFullSchemaDescription N'common', N'CountryHolidays', N'CreatedDate', N'Olusturma tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'CountryHolidays', N'CreatedUser', N'Olusturan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'CountryHolidays', N'ModifedDate', N'Guncelleme tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'CountryHolidays', N'ModifedUser', N'Guncelleyen kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'CountryHolidays', N'DeletedDate', N'Soft-delete tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'CountryHolidays', N'DeletedUser', N'Soft-delete yapan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'CountryHolidays', N'IsDelete', N'Soft-delete bayragi (bit).';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDef', NULL, N'AI platform tablosu. Sirket ici grup / yetki grubu tanimi.';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDef', N'Id', N'Primary key.';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDef', N'GId', N'Dis kimlik (Guid).';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDef', N'Stat', N'Aktif/pasif.';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDef', N'CompanyId', N'Sirket Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDef', N'Code', N'Grup kodu.';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDef', N'Description', N'Grup adi.';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDef', N'Order', N'Siralama.';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDef', N'CreatedDate', N'Olusturma tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDef', N'CreatedUser', N'Olusturan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDef', N'ModifedDate', N'Guncelleme tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDef', N'ModifedUser', N'Guncelleyen kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDef', N'DeletedDate', N'Soft-delete tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDef', N'DeletedUser', N'Soft-delete yapan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDef', N'IsDelete', N'Soft-delete bayragi (bigint).';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDefCountry', NULL, N'AI platform tablosu. Grup–ulke eslemesi.';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDefCountry', N'Id', N'Primary key.';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDefCountry', N'GId', N'Dis kimlik (Guid).';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDefCountry', N'Stat', N'Aktif/pasif.';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDefCountry', N'CompanyId', N'Sirket Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDefCountry', N'GroupDefId', N'FK → GroupDef.Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDefCountry', N'CountryCode', N'Ulke kodu.';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDefCountry', N'CreatedDate', N'Olusturma tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDefCountry', N'CreatedUser', N'Olusturan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDefCountry', N'ModifedDate', N'Guncelleme tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDefCountry', N'ModifedUser', N'Guncelleyen kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDefCountry', N'DeletedDate', N'Soft-delete tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDefCountry', N'DeletedUser', N'Soft-delete yapan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'GroupDefCountry', N'IsDelete', N'Soft-delete bayragi (bigint).';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelDef', NULL, N'AI platform tablosu. Etiket (tag) tanimlari / kategorileri. Eski tablo: EtiketTanim.';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelDef', N'Id', N'Primary key.';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelDef', N'GId', N'Dis kimlik (Guid).';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelDef', N'CompanyId', N'Sirket Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelDef', N'Stat', N'Aktif/pasif.';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelDef', N'Label', N'Etiket metni.';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelDef', N'LabelDescription', N'Etiket aciklamasi.';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelDef', N'SeoKey', N'SEO / slug anahtari.';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelDef', N'IsMainCategory', N'Ana kategori mi.';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelDef', N'Order', N'Siralama.';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelDef', N'CreatedDate', N'Olusturma tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelDef', N'CreatedUser', N'Olusturan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelDef', N'ModifedDate', N'Guncelleme tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelDef', N'ModifedUser', N'Guncelleyen kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelDef', N'DeletedDate', N'Soft-delete tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelDef', N'DeletedUser', N'Soft-delete yapan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelDef', N'IsDelete', N'Soft-delete bayragi (bit).';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelPool', NULL, N'AI platform tablosu. Kayit–etiket eslemesi (TableName + TableId + Label). Eski tablo: EtiketHavuzu.';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelPool', N'Id', N'Primary key.';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelPool', N'GId', N'Dis kimlik (Guid).';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelPool', N'CompanyId', N'Sirket Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelPool', N'TableName', N'Kaynak tablo adi.';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelPool', N'TableId', N'Kaynak kayit Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelPool', N'Label', N'Etiket metni / kodu.';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelPool', N'LabelDefId', N'FK → LabelDef.Id (opsiyonel).';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelPool', N'CreatedDate', N'Olusturma tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelPool', N'CreatedUser', N'Olusturan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelPool', N'ModifedDate', N'Guncelleme tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelPool', N'ModifedUser', N'Guncelleyen kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelPool', N'DeletedDate', N'Soft-delete tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelPool', N'DeletedUser', N'Soft-delete yapan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'LabelPool', N'IsDelete', N'Soft-delete bayragi (bit).';
EXEC dbo._SetFullSchemaDescription N'common', N'Languages', NULL, N'AI platform tablosu. Desteklenen UI/servis dilleri. Min tr-TR + en-US (AI-02i). PK = LanguageCode.';
EXEC dbo._SetFullSchemaDescription N'common', N'Languages', N'Stat', N'Dil aktif mi (AcceptLanguages ile uyumlu).';
EXEC dbo._SetFullSchemaDescription N'common', N'Languages', N'MasterLanguge', N'Master dil mi (yazim: MasterLanguge — mevcut sozlesme).';
EXEC dbo._SetFullSchemaDescription N'common', N'Languages', N'LanguageCode', N'Kultur kodu PK (tr-TR, en-US...).';
EXEC dbo._SetFullSchemaDescription N'common', N'Languages', N'DisLanguageCode', N'Kisa gosterim kodu (TR, EN...).';
EXEC dbo._SetFullSchemaDescription N'common', N'Languages', N'Description', N'Dil adi (gorunen).';
EXEC dbo._SetFullSchemaDescription N'common', N'Languages', N'Order', N'Siralama.';
EXEC dbo._SetFullSchemaDescription N'common', N'Languages', N'Flag', N'Bayrak ikon yolu (UI).';
EXEC dbo._SetFullSchemaDescription N'common', N'MailLog', NULL, N'AI platform tablosu. Gonderilen e-posta logu (OTP, bildirim vb.).';
EXEC dbo._SetFullSchemaDescription N'common', N'MailLog', N'Id', N'Primary key.';
EXEC dbo._SetFullSchemaDescription N'common', N'MailLog', N'GId', N'Dis kimlik (Guid).';
EXEC dbo._SetFullSchemaDescription N'common', N'MailLog', N'CompanyId', N'Sirket Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'MailLog', N'TableName', N'Ilgili tablo adi.';
EXEC dbo._SetFullSchemaDescription N'common', N'MailLog', N'TableId', N'Ilgili kayit Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'MailLog', N'MailTypeCode', N'Mail tipi kodu (RESET_PASSWORD, INVITE...).';
EXEC dbo._SetFullSchemaDescription N'common', N'MailLog', N'MailTo', N'Alici e-posta.';
EXEC dbo._SetFullSchemaDescription N'common', N'MailLog', N'MailSubject', N'Konu.';
EXEC dbo._SetFullSchemaDescription N'common', N'MailLog', N'MailBody', N'Govde (HTML/text).';
EXEC dbo._SetFullSchemaDescription N'common', N'MailLog', N'SentDate', N'Gonderim zamani.';
EXEC dbo._SetFullSchemaDescription N'common', N'MailLog', N'SentByPersonalId', N'Gonderen personel Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'MailLog', N'Stat', N'Aktif/pasif.';
EXEC dbo._SetFullSchemaDescription N'common', N'MailLog', N'CreatedDate', N'Olusturma tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'MailLog', N'CreatedUser', N'Olusturan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'MailLog', N'ModifedDate', N'Guncelleme tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'MailLog', N'ModifedUser', N'Guncelleyen kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'MailLog', N'DeletedDate', N'Soft-delete tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'MailLog', N'DeletedUser', N'Soft-delete yapan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'MailLog', N'IsDelete', N'Soft-delete bayragi (bit).';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessType', NULL, N'AI platform tablosu. Durum kodlari (ModulId + Code). Yeni durum = satir seed; tablo yapisi degismez.';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessType', N'Id', N'Primary key (IDENTITY seed 0).';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessType', N'ModulId', N'FK mantigi → Moduls.Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessType', N'Code', N'Durum kodu (PLANNED, COMPLETED...).';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessType', N'DescriptionTR', N'Turkce aciklama.';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessType', N'DescriptionEN', N'Ingilizce aciklama.';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessType', N'Ordered', N'Siralama.';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessType', N'Color', N'UI renk JSON/hex.';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessFlow', NULL, N'AI platform tablosu. Durum gecisleri (FromCode → ToCode) per ModulId.';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessFlow', N'Id', N'Primary key.';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessFlow', N'GId', N'Dis kimlik (Guid).';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessFlow', N'ModulId', N'FK → Moduls.Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessFlow', N'FromCode', N'Kaynak durum kodu.';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessFlow', N'ToCode', N'Hedef durum kodu.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalBranch', NULL, N'AI platform tablosu. Personel–sube coklu esleme (primary bayrakli).';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalBranch', N'Id', N'Primary key.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalBranch', N'GId', N'Dis kimlik (Guid).';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalBranch', N'Stat', N'Aktif/pasif.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalBranch', N'PersonalId', N'FK → Personal.Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalBranch', N'BranchId', N'FK → Branch.Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalBranch', N'IsPrimary', N'Birincil sube mi.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalBranch', N'CreatedDate', N'Olusturma tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalBranch', N'CreatedUser', N'Olusturan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalBranch', N'ModifedDate', N'Guncelleme tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalBranch', N'ModifedUser', N'Guncelleyen kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalBranch', N'DeletedDate', N'Soft-delete tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalBranch', N'DeletedUser', N'Soft-delete yapan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalBranch', N'IsDelete', N'Soft-delete bayragi (bigint).';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalCompany', NULL, N'AI platform tablosu. Personel–sirket uyelik / login yetkileri (web/mobil).';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalCompany', N'Id', N'Primary key.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalCompany', N'GId', N'Dis kimlik (Guid).';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalCompany', N'PersonalId', N'FK → Personal.Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalCompany', N'CompanyId', N'FK → Company.Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalCompany', N'IsWebLogin', N'Web giris izni.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalCompany', N'IsMobileLogin', N'Mobil giris izni.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalCompany', N'ShowAllAuditSession', N'Tum denetim oturumlarini gorebilsin (urun bayragi).';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalCompany', N'ShowAllAuditAction', N'Tum denetim aksiyonlarini gorebilsin.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalCompany', N'BranchActionReport', N'Sube aksiyon raporu yetkisi.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalCompany', N'CreatedDate', N'Olusturma tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalCompany', N'CreatedUser', N'Olusturan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalCompany', N'ModifedDate', N'Guncelleme tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalCompany', N'ModifedUser', N'Guncelleyen kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalCompany', N'DeletedDate', N'Soft-delete tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalCompany', N'DeletedUser', N'Soft-delete yapan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalCompany', N'IsDelete', N'Soft-delete bayragi (bigint).';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalGroup', NULL, N'AI platform tablosu. Personel grup kodu eslemesi (GroupCode int).';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalGroup', N'Id', N'Satir Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalGroup', N'GId', N'Dis kimlik (Guid).';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalGroup', N'CompanyId', N'Sirket Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalGroup', N'PersonalId', N'FK → Personal.Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalGroup', N'GroupCode', N'Grup kodu (int).';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalGroup', N'CreatedDate', N'Olusturma tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalGroup', N'CreatedUser', N'Olusturan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalGroup', N'ModifedDate', N'Guncelleme tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalGroup', N'ModifedUser', N'Guncelleyen kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalGroup', N'DeletedDate', N'Soft-delete tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalGroup', N'DeletedUser', N'Soft-delete yapan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalGroup', N'IsDelete', N'Soft-delete bayragi (bigint).';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalGroupDef', NULL, N'AI platform tablosu. Personel–GroupDef uyelik.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalGroupDef', N'Id', N'Primary key.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalGroupDef', N'GId', N'Dis kimlik (Guid).';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalGroupDef', N'Stat', N'Aktif/pasif.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalGroupDef', N'CompanyId', N'Sirket Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalGroupDef', N'PersonalId', N'FK → Personal.Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalGroupDef', N'GroupDefId', N'FK → GroupDef.Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalGroupDef', N'CreatedDate', N'Olusturma tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalGroupDef', N'CreatedUser', N'Olusturan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalGroupDef', N'ModifedDate', N'Guncelleme tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalGroupDef', N'ModifedUser', N'Guncelleyen kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalGroupDef', N'DeletedDate', N'Soft-delete tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalGroupDef', N'DeletedUser', N'Soft-delete yapan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalGroupDef', N'IsDelete', N'Soft-delete bayragi (bigint).';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalLoginActivity', NULL, N'AI platform tablosu. Personel login / logout / basarisiz giris aktivite logu.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalLoginActivity', N'Id', N'Primary key.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalLoginActivity', N'GId', N'Dis kimlik (Guid).';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalLoginActivity', N'PersonalId', N'FK → Personal.Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalLoginActivity', N'ActivityType', N'Aktivite tipi (LOGIN, LOGOUT, FAIL...).';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalLoginActivity', N'IsSuccess', N'Basarili mi.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalLoginActivity', N'CustomHeaderJson', N'CustomHeader JSON snapshot.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalLoginActivity', N'DeviceId', N'Cihaz kimligi.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalLoginActivity', N'ClientOs', N'Istemci OS.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalLoginActivity', N'AppVersion', N'Uygulama surumu.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalLoginActivity', N'Language', N'Dil kodu.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalLoginActivity', N'IpAddress', N'IP adresi.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalLoginActivity', N'CreatedDate', N'Aktivite zamani.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalWidget', NULL, N'AI platform tablosu. Personel dashboard widget tercihleri.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalWidget', N'Id', N'Primary key.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalWidget', N'GId', N'Dis kimlik (Guid).';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalWidget', N'Stat', N'Aktif/pasif.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalWidget', N'PersonalId', N'FK → Personal.Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalWidget', N'WidgetId', N'Widget kodu.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalWidget', N'With', N'Genislik / layout parametresi (kolon adi: With).';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalWidget', N'IsVisible', N'Gorunur mu.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalWidget', N'Position', N'Sira pozisyonu.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalWidget', N'RowIndex', N'Satir indeksi.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalWidget', N'CreatedDate', N'Olusturma tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalWidget', N'CreatedUser', N'Olusturan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalWidget', N'ModifedDate', N'Guncelleme tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalWidget', N'ModifedUser', N'Guncelleyen kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalWidget', N'DeletedDate', N'Soft-delete tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalWidget', N'DeletedUser', N'Soft-delete yapan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'PersonalWidget', N'IsDelete', N'Soft-delete bayragi (bigint).';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessTypePersonal', NULL, N'AI platform tablosu. Personelin hangi ProcessType / durum kodlarina yetkili oldugu.';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessTypePersonal', N'Id', N'Primary key.';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessTypePersonal', N'GId', N'Dis kimlik (Guid).';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessTypePersonal', N'CompanyId', N'Sirket Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessTypePersonal', N'ProcessTypeId', N'FK → ProcessType.Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessTypePersonal', N'PersonalId', N'FK → Personal.Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessTypePersonal', N'Stat', N'Aktif/pasif.';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessTypePersonal', N'CreatedDate', N'Olusturma tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessTypePersonal', N'CreatedUser', N'Olusturan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessTypePersonal', N'ModifedDate', N'Guncelleme tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessTypePersonal', N'ModifedUser', N'Guncelleyen kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessTypePersonal', N'DeletedDate', N'Soft-delete tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessTypePersonal', N'DeletedUser', N'Soft-delete yapan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'ProcessTypePersonal', N'IsDelete', N'Soft-delete bayragi (bit).';
EXEC dbo._SetFullSchemaDescription N'common', N'TranslationDef', NULL, N'AI platform tablosu. Kayit alan cevirileri (TableName/TableId/Field + LanguageCode).';
EXEC dbo._SetFullSchemaDescription N'common', N'TranslationDef', N'Id', N'Satir Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'TranslationDef', N'GId', N'Dis kimlik (Guid).';
EXEC dbo._SetFullSchemaDescription N'common', N'TranslationDef', N'CompanyId', N'Sirket Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'TranslationDef', N'TableName', N'Kaynak tablo.';
EXEC dbo._SetFullSchemaDescription N'common', N'TranslationDef', N'TableId', N'Kaynak kayit Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'TranslationDef', N'TableFieldName', N'Alan adi.';
EXEC dbo._SetFullSchemaDescription N'common', N'TranslationDef', N'LanguageCode', N'Dil kodu.';
EXEC dbo._SetFullSchemaDescription N'common', N'TranslationDef', N'Description', N'Ceviri metni.';
EXEC dbo._SetFullSchemaDescription N'common', N'TranslationDef', N'CreatedDate', N'Olusturma tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'TranslationDef', N'CreatedUser', N'Olusturan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'TranslationDef', N'ModifedDate', N'Guncelleme tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'TranslationDef', N'ModifedUser', N'Guncelleyen kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'TranslationDef', N'DeletedDate', N'Soft-delete tarihi.';
EXEC dbo._SetFullSchemaDescription N'common', N'TranslationDef', N'DeletedUser', N'Soft-delete yapan kullanici Id.';
EXEC dbo._SetFullSchemaDescription N'common', N'TranslationDef', N'IsDelete', N'Soft-delete bayragi (bigint).';
EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetRoleClaims', NULL, N'AI platform tablosu. Role claim kayitlari.';
EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetRoleClaims', N'Id', N'Primary key.';
EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetRoleClaims', N'RoleId', N'FK → AspNetRoles.Id.';
EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetRoleClaims', N'ClaimType', N'Claim tipi.';
EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetRoleClaims', N'ClaimValue', N'Claim degeri.';
EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetRoleClaims', N'Discriminator', N'EF TPH discriminator.';
EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetUserClaims', NULL, N'AI platform tablosu. Kullanici claim kayitlari (or. UserId claim).';
EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetUserClaims', N'Id', N'Primary key.';
EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetUserClaims', N'UserId', N'FK → AspNetUsers.Id.';
EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetUserClaims', N'ClaimType', N'Claim tipi.';
EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetUserClaims', N'ClaimValue', N'Claim degeri.';
EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetUserClaims', N'Discriminator', N'EF TPH discriminator.';
EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetUserLogins', NULL, N'AI platform tablosu. Harici login saglayicilari (Google vb.).';
EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetUserLogins', N'LoginProvider', N'Saglayici adi (PK parca).';
EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetUserLogins', N'ProviderKey', N'Saglayici anahtari (PK parca).';
EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetUserLogins', N'ProviderDisplayName', N'Saglayici gorunen ad.';
EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetUserLogins', N'UserId', N'FK → AspNetUsers.Id.';
EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetUserLogins', N'Discriminator', N'EF TPH discriminator.';
EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetUserTokens', NULL, N'AI platform tablosu. Kullanici tokenlari (refresh / provider token). ExpireDate aile ozel.';
EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetUserTokens', N'UserId', N'FK → AspNetUsers.Id (PK).';
EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetUserTokens', N'LoginProvider', N'Saglayici (PK).';
EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetUserTokens', N'Name', N'Token adi (PK).';
EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetUserTokens', N'Value', N'Token degeri.';
EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetUserTokens', N'ExpireDate', N'Son gecerlilik (aile ozel).';
EXEC dbo._SetFullSchemaDescription N'dbo', N'AspNetUserTokens', N'Discriminator', N'EF TPH discriminator.';
GO

DROP PROCEDURE IF EXISTS dbo._SetFullSchemaDescription;
GO

PRINT N'EKCN2026 full schema complete (clean install).';
GO