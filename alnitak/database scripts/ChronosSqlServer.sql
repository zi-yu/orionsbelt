IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'Chronos')
	DROP DATABASE [Chronos]
GO

CREATE DATABASE [Chronos]  ON (NAME = N'Chronos_Data', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL\data\Chronos_Data.MDF' , SIZE = 21, FILEGROWTH = 10%) LOG ON (NAME = N'Chronos_Log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL\data\Chronos_Log.LDF' , SIZE = 31, FILEGROWTH = 10%)
 COLLATE SQL_Latin1_General_CP1_CI_AS
GO

exec sp_dboption N'Chronos', N'autoclose', N'false'
GO

exec sp_dboption N'Chronos', N'bulkcopy', N'false'
GO

exec sp_dboption N'Chronos', N'trunc. log', N'false'
GO

exec sp_dboption N'Chronos', N'torn page detection', N'true'
GO

exec sp_dboption N'Chronos', N'read only', N'false'
GO

exec sp_dboption N'Chronos', N'dbo use', N'false'
GO

exec sp_dboption N'Chronos', N'single', N'false'
GO

exec sp_dboption N'Chronos', N'autoshrink', N'false'
GO

exec sp_dboption N'Chronos', N'ANSI null default', N'false'
GO

exec sp_dboption N'Chronos', N'recursive triggers', N'false'
GO

exec sp_dboption N'Chronos', N'ANSI nulls', N'false'
GO

exec sp_dboption N'Chronos', N'concat null yields null', N'false'
GO

exec sp_dboption N'Chronos', N'cursor close on commit', N'false'
GO

exec sp_dboption N'Chronos', N'default to local cursor', N'false'
GO

exec sp_dboption N'Chronos', N'quoted identifier', N'false'
GO

exec sp_dboption N'Chronos', N'ANSI warnings', N'false'
GO

exec sp_dboption N'Chronos', N'auto create statistics', N'true'
GO

exec sp_dboption N'Chronos', N'auto update statistics', N'true'
GO

use [Chronos]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DeleteAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[DeleteAll]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Exist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Exist]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ExistIdentifier]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ExistIdentifier]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ExistIdentifierCategory]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ExistIdentifierCategory]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GetData]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetResourceInfoInfoData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GetResourceInfoInfoData]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetResourceManagerInfoData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GetResourceManagerInfoData]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[SaveAllianceInfoData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[SaveAllianceInfoData]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[SavePlanetInfoData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[SavePlanetInfoData]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[SaveResourceInfoInfoData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[SaveResourceInfoInfoData]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[SaveResourceManagerInfoData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[SaveResourceManagerInfoData]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[SaveRulerInfoData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[SaveRulerInfoData]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[SaveUniverseInfoData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[SaveUniverseInfoData]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[UpdateAllianceInfoData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[UpdateAllianceInfoData]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[UpdatePlanetInfoData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[UpdatePlanetInfoData]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[UpdateResourceInfoInfoData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[UpdateResourceInfoInfoData]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[UpdateResourceManagerInfoData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[UpdateResourceManagerInfoData]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[UpdateRulerInfoData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[UpdateRulerInfoData]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[UpdateUniverseInfoData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[UpdateUniverseInfoData]
GO

if not exists (select * from master.dbo.syslogins where loginname = N'GERAL')
BEGIN
	declare @logindb nvarchar(132), @loginlang nvarchar(132) select @logindb = N'MantainerTest', @loginlang = N'us_english'
	if @logindb is null or not exists (select * from master.dbo.sysdatabases where name = @logindb)
		select @logindb = N'master'
	if @loginlang is null or (not exists (select * from master.dbo.syslanguages where name = @loginlang) and @loginlang <> N'us_english')
		select @loginlang = @@language
	exec sp_addlogin N'GERAL', null, @logindb, @loginlang
END
GO

if not exists (select * from master.dbo.syslogins where loginname = N'mintaka')
BEGIN
	declare @logindb nvarchar(132), @loginlang nvarchar(132) select @logindb = N'master', @loginlang = N'us_english'
	if @logindb is null or not exists (select * from master.dbo.sysdatabases where name = @logindb)
		select @logindb = N'master'
	if @loginlang is null or (not exists (select * from master.dbo.syslanguages where name = @loginlang) and @loginlang <> N'us_english')
		select @loginlang = @@language
	exec sp_addlogin N'mintaka', null, @logindb, @loginlang
END
GO

if not exists (select * from master.dbo.syslogins where loginname = N'PC07\ASPNET')
	exec sp_grantlogin N'PC07\ASPNET'
	exec sp_defaultdb N'PC07\ASPNET', N'master'
	exec sp_defaultlanguage N'PC07\ASPNET', N'us_english'
GO

if not exists (select * from master.dbo.syslogins where loginname = N'tst')
BEGIN
	declare @logindb nvarchar(132), @loginlang nvarchar(132) select @logindb = N'MantainerTest', @loginlang = N'us_english'
	if @logindb is null or not exists (select * from master.dbo.sysdatabases where name = @logindb)
		select @logindb = N'master'
	if @loginlang is null or (not exists (select * from master.dbo.syslanguages where name = @loginlang) and @loginlang <> N'us_english')
		select @loginlang = @@language
	exec sp_addlogin N'tst', null, @logindb, @loginlang
END
GO

if not exists (select * from master.dbo.syslogins where loginname = N'UIP')
BEGIN
	declare @logindb nvarchar(132), @loginlang nvarchar(132) select @logindb = N'master', @loginlang = N'us_english'
	if @logindb is null or not exists (select * from master.dbo.sysdatabases where name = @logindb)
		select @logindb = N'master'
	if @loginlang is null or (not exists (select * from master.dbo.syslanguages where name = @loginlang) and @loginlang <> N'us_english')
		select @loginlang = @@language
	exec sp_addlogin N'UIP', null, @logindb, @loginlang
END
GO

exec sp_addsrvrolemember N'GERAL', sysadmin
GO

exec sp_addsrvrolemember N'tst', sysadmin
GO

exec sp_addsrvrolemember N'GERAL', securityadmin
GO

exec sp_addsrvrolemember N'GERAL', serveradmin
GO

exec sp_addsrvrolemember N'GERAL', setupadmin
GO

exec sp_addsrvrolemember N'GERAL', processadmin
GO

exec sp_addsrvrolemember N'GERAL', diskadmin
GO

exec sp_addsrvrolemember N'GERAL', dbcreator
GO

exec sp_addsrvrolemember N'GERAL', bulkadmin
GO

if not exists (select * from dbo.sysusers where name = N'mintaka' and uid < 16382)
	EXEC sp_grantdbaccess N'mintaka', N'mintaka'
GO

exec sp_addrolemember N'db_accessadmin', N'mintaka'
GO

exec sp_addrolemember N'db_backupoperator', N'mintaka'
GO

exec sp_addrolemember N'db_datareader', N'mintaka'
GO

exec sp_addrolemember N'db_datawriter', N'mintaka'
GO

exec sp_addrolemember N'db_ddladmin', N'mintaka'
GO

exec sp_addrolemember N'db_denydatareader', N'mintaka'
GO

exec sp_addrolemember N'db_denydatawriter', N'mintaka'
GO

exec sp_addrolemember N'db_owner', N'mintaka'
GO

exec sp_addrolemember N'db_securityadmin', N'mintaka'
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

create procedure DeleteAll
as
	delete from ResourceInfoInfo
	delete from ResourceManagerInfo
	delete from PlanetInfo
	delete from RulerInfo
	delete from AllianceInfo
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE procedure Exist
	@Id int,
	@Table varchar(50)
as
	DECLARE @SQL nvarchar(600)
	SET @SQL =  'select COUNT(*) from ' + @Table + ' where id= ' + CONVERT(varchar(5), @Id)
	EXEC sp_executesql @SQL
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE procedure ExistIdentifier
	@Id int,
	@Table varchar(50),
	@Identifier varchar(50)
as
	DECLARE @SQL nvarchar(600)

	SET @SQL =  'select COUNT(*) from ' + @Table + ' where id= ' + CONVERT(varchar(5), @Id)  + ' and Identifier = "' + @Identifier + '"'
	EXEC sp_executesql @SQL
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE procedure ExistIdentifierCategory
	@Id int,
	@Table varchar(50),
	@Identifier varchar(50),
	@Category varchar(50)
as
	select COUNT(*) from ResourceInfoInfo where id= @Id  and Identifier = @Identifier and Category = @Category
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE GetData
	@Table varchar(50)
AS

	DECLARE @SQL nvarchar(600)
	SET @SQL =  'SELECT * FROM ' + @Table
	EXEC sp_executesql @SQL
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE GetResourceInfoInfoData
	@Identifier varchar(50)
AS
	select * from ResourceInfoInfo
	where Identifier = @Identifier
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE GetResourceManagerInfoData
	@Identifier varchar(50)
AS
	select * from ResourceManagerInfo
	where Identifier = @Identifier
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE SaveAllianceInfoData
	@Id int,
	@Data varbinary(8000)
AS

	insert into AllianceInfo(Id, Data) values( @Id, @Data )
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE SavePlanetInfoData
	@Id int,
	@Data varbinary(8000)
AS

	insert into PlanetInfo(Id, Data) values( @Id, @Data )
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE SaveResourceInfoInfoData
	@Id int,
	@Data varbinary(8000),
	@Identifier varchar(50),
	@Category varchar(50)
AS

	insert into ResourceInfoInfo(Id, Identifier, Category, Data) values( @Id, @Identifier, @Category, @Data )
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE SaveResourceManagerInfoData
	@Id int,
	@Data varbinary(8000),
	@Identifier varchar(50)
AS

	insert into ResourceManagerInfo(Id, Identifier, Data) values( @Id, @Identifier, @Data )
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE SaveRulerInfoData
	@Id int,
	@Data varbinary(8000)
AS

	insert into RulerInfo(Id, Data) values( @Id, @Data )
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE SaveUniverseInfoData
	@Id int,
	@Data varbinary(8000)
AS

	insert into UniverseInfo(Id, Data) values( @Id, @Data )
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE UpdateAllianceInfoData
	@Id int,
	@Data varbinary(8000)
AS
	update AllianceInfo
	set Data =@Data
	Where id = @Id
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE UpdatePlanetInfoData
	@Id int,
	@Data varbinary(8000)
AS
	update PlanetInfo
	set Data =@Data
	Where id = @Id
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE UpdateResourceInfoInfoData
	@Id int,
	@Data varbinary(8000),
	@identifier varchar(50),
	@Category varchar(50)
AS
	update ResourceInfoInfo
	set Data =@Data
	Where id = @Id and identifier = @identifier and Category = @Category
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE UpdateResourceManagerInfoData
	@Id int,
	@Data varbinary(8000),
	@Identifier varchar(50)
AS
	update ResourceManagerInfo
	set Data =@Data
	Where id = @Id and Identifier = @Identifier
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE UpdateRulerInfoData
	@Id int,
	@Data varbinary(8000)
AS
	update RulerInfo
	set Data =@Data
	Where id = @Id
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE UpdateUniverseInfoData
	@Id int,
	@Data varbinary(8000)
AS
	update UniverseInfo
	set Data =@Data
	Where id = @Id
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

