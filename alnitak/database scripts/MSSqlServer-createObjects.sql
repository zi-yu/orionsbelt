IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'orionsbelt')

	DROP DATABASE [orionsbelt]

GO



CREATE DATABASE [orionsbelt]  ON (NAME = N'orionsbelt', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL\data\orionsbelt.mdf' , SIZE = 3, FILEGROWTH = 10%) LOG ON (NAME = N'orionsbelt_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL\data\orionsbelt_log.LDF' , SIZE = 1, FILEGROWTH = 10%)

 COLLATE Latin1_General_CI_AS

GO



exec sp_dboption N'orionsbelt', N'autoclose', N'true'

GO



exec sp_dboption N'orionsbelt', N'bulkcopy', N'false'

GO



exec sp_dboption N'orionsbelt', N'trunc. log', N'true'

GO



exec sp_dboption N'orionsbelt', N'torn page detection', N'true'

GO



exec sp_dboption N'orionsbelt', N'read only', N'false'

GO



exec sp_dboption N'orionsbelt', N'dbo use', N'false'

GO



exec sp_dboption N'orionsbelt', N'single', N'false'

GO



exec sp_dboption N'orionsbelt', N'autoshrink', N'true'

GO



exec sp_dboption N'orionsbelt', N'ANSI null default', N'false'

GO



exec sp_dboption N'orionsbelt', N'recursive triggers', N'false'

GO



exec sp_dboption N'orionsbelt', N'ANSI nulls', N'false'

GO



exec sp_dboption N'orionsbelt', N'concat null yields null', N'false'

GO



exec sp_dboption N'orionsbelt', N'cursor close on commit', N'false'

GO



exec sp_dboption N'orionsbelt', N'default to local cursor', N'false'

GO



exec sp_dboption N'orionsbelt', N'quoted identifier', N'false'

GO



exec sp_dboption N'orionsbelt', N'ANSI warnings', N'false'

GO



exec sp_dboption N'orionsbelt', N'auto create statistics', N'true'

GO



exec sp_dboption N'orionsbelt', N'auto update statistics', N'true'

GO



use [orionsbelt]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[fk_skin]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[OrionsBelt_Sections] DROP CONSTRAINT fk_skin

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_OrionsBelt_UserRoles_OrionsBelt_Roles]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[OrionsBelt_UserRoles] DROP CONSTRAINT FK_OrionsBelt_UserRoles_OrionsBelt_Roles

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_OrionsBelt_UserRoles_OrionsBelt_Users]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[OrionsBelt_UserRoles] DROP CONSTRAINT FK_OrionsBelt_UserRoles_OrionsBelt_Users

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Active_Users]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_Active] DROP CONSTRAINT FK_Active_Users

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_yaf_Active_OrionsBelt_Users]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_Active] DROP CONSTRAINT FK_yaf_Active_OrionsBelt_Users

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_yaf_Forum_OrionsBelt_Users]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_Forum] DROP CONSTRAINT FK_yaf_Forum_OrionsBelt_Users

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_yaf_Message_OrionsBelt_Users]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_Message] DROP CONSTRAINT FK_yaf_Message_OrionsBelt_Users

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_yaf_PMessage_OrionsBelt_Users]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_PMessage] DROP CONSTRAINT FK_yaf_PMessage_OrionsBelt_Users

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_yaf_Topic_OrionsBelt_Users]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_Topic] DROP CONSTRAINT FK_yaf_Topic_OrionsBelt_Users

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_yaf_Topic_OrionsBelt_Users1]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_Topic] DROP CONSTRAINT FK_yaf_Topic_OrionsBelt_Users1

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_yaf_UserForum_OrionsBelt_Users]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_UserForum] DROP CONSTRAINT FK_yaf_UserForum_OrionsBelt_Users

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_yaf_UserGroup_OrionsBelt_Users]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_UserGroup] DROP CONSTRAINT FK_yaf_UserGroup_OrionsBelt_Users

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_yaf_UserPMessage_OrionsBelt_Users]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_UserPMessage] DROP CONSTRAINT FK_yaf_UserPMessage_OrionsBelt_Users

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_yaf_WatchForum_OrionsBelt_Users]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_WatchForum] DROP CONSTRAINT FK_yaf_WatchForum_OrionsBelt_Users

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_yaf_WatchTopic_OrionsBelt_Users]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_WatchTopic] DROP CONSTRAINT FK_yaf_WatchTopic_OrionsBelt_Users

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ForumAccess_AccessMask]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_ForumAccess] DROP CONSTRAINT FK_ForumAccess_AccessMask

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_UserForum_AccessMask]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_UserForum] DROP CONSTRAINT FK_UserForum_AccessMask

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_AccessMask_Board]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_AccessMask] DROP CONSTRAINT FK_AccessMask_Board

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Active_Board]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_Active] DROP CONSTRAINT FK_Active_Board

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_BannedIP_Board]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_BannedIP] DROP CONSTRAINT FK_BannedIP_Board

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Category_Board]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_Category] DROP CONSTRAINT FK_Category_Board

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Group_Board]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_Group] DROP CONSTRAINT FK_Group_Board

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_NntpServer_Board]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_NntpServer] DROP CONSTRAINT FK_NntpServer_Board

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Rank_Board]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_Rank] DROP CONSTRAINT FK_Rank_Board

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Registry_Board]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_Registry] DROP CONSTRAINT FK_Registry_Board

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Smiley_Board]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_Smiley] DROP CONSTRAINT FK_Smiley_Board

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Forum_Category]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_Forum] DROP CONSTRAINT FK_Forum_Category

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Active_Forum]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_Active] DROP CONSTRAINT FK_Active_Forum

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Forum_Forum]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_Forum] DROP CONSTRAINT FK_Forum_Forum

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ForumAccess_Forum]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_ForumAccess] DROP CONSTRAINT FK_ForumAccess_Forum

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_NntpForum_Forum]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_NntpForum] DROP CONSTRAINT FK_NntpForum_Forum

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Topic_Forum]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_Topic] DROP CONSTRAINT FK_Topic_Forum

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_UserForum_Forum]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_UserForum] DROP CONSTRAINT FK_UserForum_Forum

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_WatchForum_Forum]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_WatchForum] DROP CONSTRAINT FK_WatchForum_Forum

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ForumAccess_Group]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_ForumAccess] DROP CONSTRAINT FK_ForumAccess_Group

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_UserGroup_Group]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_UserGroup] DROP CONSTRAINT FK_UserGroup_Group

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Attachment_Message]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_Attachment] DROP CONSTRAINT FK_Attachment_Message

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Forum_Message]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_Forum] DROP CONSTRAINT FK_Forum_Message

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Message_Message]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_Message] DROP CONSTRAINT FK_Message_Message

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Topic_Message]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_Topic] DROP CONSTRAINT FK_Topic_Message

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_NntpTopic_NntpForum]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_NntpTopic] DROP CONSTRAINT FK_NntpTopic_NntpForum

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_NntpForum_NntpServer]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_NntpForum] DROP CONSTRAINT FK_NntpForum_NntpServer

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_UserPMessage_PMessage]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_UserPMessage] DROP CONSTRAINT FK_UserPMessage_PMessage

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Choice_Poll]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_Choice] DROP CONSTRAINT FK_Choice_Poll

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Topic_Poll]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_Topic] DROP CONSTRAINT FK_Topic_Poll

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Active_Topic]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_Active] DROP CONSTRAINT FK_Active_Topic

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Forum_Topic]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_Forum] DROP CONSTRAINT FK_Forum_Topic

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Message_Topic]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_Message] DROP CONSTRAINT FK_Message_Topic

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_NntpTopic_Topic]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_NntpTopic] DROP CONSTRAINT FK_NntpTopic_Topic

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Topic_Topic]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_Topic] DROP CONSTRAINT FK_Topic_Topic

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_WatchTopic_Topic]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)

ALTER TABLE [dbo].[yaf_WatchTopic] DROP CONSTRAINT FK_WatchTopic_Topic

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_Active_insert]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)

drop trigger [dbo].[yaf_Active_insert]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_Forum_update]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)

drop trigger [dbo].[yaf_Forum_update]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_bitset]') and xtype in (N'FN', N'IF', N'TF'))

drop function [dbo].[yaf_bitset]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_forum_posts]') and xtype in (N'FN', N'IF', N'TF'))

drop function [dbo].[yaf_forum_posts]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_forum_topics]') and xtype in (N'FN', N'IF', N'TF'))

drop function [dbo].[yaf_forum_topics]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_ChronosCountMessages]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_ChronosCountMessages]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_ChronosLoadBattle]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_ChronosLoadBattle]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_ChronosLoadMessage]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_ChronosLoadMessage]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_ChronosLoadUniverse]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_ChronosLoadUniverse]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_ChronosMarkAllAsRead]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_ChronosMarkAllAsRead]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_ChronosRemoveBattle]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_ChronosRemoveBattle]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_ChronosSaveBattle]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_ChronosSaveBattle]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_ChronosSaveMessage]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_ChronosSaveMessage]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_ChronosSaveUniverse]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_ChronosSaveUniverse]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_ExceptionLogLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_ExceptionLogLoad]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_ExceptionLogRemoveAllExceptions]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_ExceptionLogRemoveAllExceptions]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_ExceptionLogRemoveException]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_ExceptionLogRemoveException]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_ExceptionLogSave]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_ExceptionLogSave]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_GetNews]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_GetNews]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_GetScans]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_GetScans]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_GetScansById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_GetScansById]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_GetScansByPlanetId]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_GetScansByPlanetId]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_InsertNews]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_InsertNews]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_InsertScans]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_InsertScans]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_MasterSkinsGetAllMasterSkins]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_MasterSkinsGetAllMasterSkins]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_NamedPagesGetAllNamedPages]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_NamedPagesGetAllNamedPages]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_SectionsGetAllSections]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_SectionsGetAllSections]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_SectionsGetAllSectionsRoles]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_SectionsGetAllSectionsRoles]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_SectionsGetMotherSections]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_SectionsGetMotherSections]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_UsersCheckUser]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_UsersCheckUser]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_UsersCheckUserMail]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_UsersCheckUserMail]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_UsersGetAvatar]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_UsersGetAvatar]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_UsersGetCount]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_UsersGetCount]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_UsersGetMailFromId]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_UsersGetMailFromId]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_UsersGetUser]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_UsersGetUser]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_UsersGetUserById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_UsersGetUserById]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_UsersGetUserRoles]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_UsersGetUserRoles]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_UsersGetUsersIdByRole]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_UsersGetUsersIdByRole]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_UsersRegisterUser]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_UsersRegisterUser]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_UsersResetPassword]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_UsersResetPassword]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_UsersResetUserRulerId]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_UsersResetUserRulerId]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_UsersUpdateLastLogin]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_UsersUpdateLastLogin]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_UsersUpdateUser]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[OrionsBelt_UsersUpdateUser]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_accessmask_delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_accessmask_delete]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_accessmask_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_accessmask_list]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_accessmask_save]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_accessmask_save]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_active_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_active_list]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_active_listforum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_active_listforum]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_active_listtopic]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_active_listtopic]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_active_stats]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_active_stats]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_attachment_delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_attachment_delete]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_attachment_download]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_attachment_download]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_attachment_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_attachment_list]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_attachment_save]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_attachment_save]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_bannedip_delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_bannedip_delete]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_bannedip_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_bannedip_list]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_bannedip_save]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_bannedip_save]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_board_create]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_board_create]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_board_delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_board_delete]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_board_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_board_list]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_board_poststats]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_board_poststats]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_board_save]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_board_save]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_board_stats]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_board_stats]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_category_delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_category_delete]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_category_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_category_list]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_category_listread]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_category_listread]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_category_save]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_category_save]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_checkemail_save]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_checkemail_save]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_checkemail_update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_checkemail_update]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_choice_vote]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_choice_vote]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_forum_delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_forum_delete]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_forum_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_forum_list]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_forum_listSubForums]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_forum_listSubForums]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_forum_listall]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_forum_listall]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_forum_listallmymoderated]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_forum_listallmymoderated]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_forum_listpath]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_forum_listpath]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_forum_listread]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_forum_listread]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_forum_listtopics]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_forum_listtopics]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_forum_moderatelist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_forum_moderatelist]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_forum_moderators]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_forum_moderators]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_forum_save]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_forum_save]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_forum_updatelastpost]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_forum_updatelastpost]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_forum_updatestats]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_forum_updatestats]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_forumaccess_group]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_forumaccess_group]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_forumaccess_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_forumaccess_list]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_forumaccess_save]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_forumaccess_save]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_group_delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_group_delete]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_group_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_group_list]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_group_member]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_group_member]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_group_save]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_group_save]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_mail_createwatch]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_mail_createwatch]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_mail_delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_mail_delete]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_mail_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_mail_list]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_message_approve]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_message_approve]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_message_delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_message_delete]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_message_findunread]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_message_findunread]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_message_getReplies]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_message_getReplies]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_message_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_message_list]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_message_save]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_message_save]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_message_unapproved]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_message_unapproved]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_message_update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_message_update]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_nntpforum_delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_nntpforum_delete]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_nntpforum_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_nntpforum_list]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_nntpforum_save]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_nntpforum_save]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_nntpforum_update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_nntpforum_update]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_nntpserver_delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_nntpserver_delete]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_nntpserver_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_nntpserver_list]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_nntpserver_save]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_nntpserver_save]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_nntptopic_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_nntptopic_list]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_nntptopic_savemessage]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_nntptopic_savemessage]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_pageload]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_pageload]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_pmessage_delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_pmessage_delete]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_pmessage_info]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_pmessage_info]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_pmessage_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_pmessage_list]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_pmessage_markread]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_pmessage_markread]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_pmessage_prune]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_pmessage_prune]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_pmessage_save]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_pmessage_save]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_poll_save]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_poll_save]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_poll_stats]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_poll_stats]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_post_last10user]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_post_last10user]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_post_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_post_list]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_post_list_reverse10]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_post_list_reverse10]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_rank_delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_rank_delete]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_rank_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_rank_list]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_rank_save]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_rank_save]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_registry_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_registry_list]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_registry_save]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_registry_save]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_replace_words_delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_replace_words_delete]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_replace_words_edit]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_replace_words_edit]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_replace_words_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_replace_words_list]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_replace_words_save]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_replace_words_save]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_smiley_delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_smiley_delete]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_smiley_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_smiley_list]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_smiley_listunique]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_smiley_listunique]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_smiley_save]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_smiley_save]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_system_initialize]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_system_initialize]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_system_updateversion]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_system_updateversion]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_topic_active]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_topic_active]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_topic_delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_topic_delete]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_topic_findnext]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_topic_findnext]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_topic_findprev]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_topic_findprev]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_topic_info]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_topic_info]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_topic_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_topic_list]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_topic_listmessages]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_topic_listmessages]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_topic_lock]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_topic_lock]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_topic_move]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_topic_move]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_topic_prune]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_topic_prune]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_topic_save]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_topic_save]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_topic_updatelastpost]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_topic_updatelastpost]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_user_accessmasks]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_user_accessmasks]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_user_activity_rank]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_user_activity_rank]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_user_adminsave]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_user_adminsave]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_user_approve]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_user_approve]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_user_avatarimage]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_user_avatarimage]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_user_changepassword]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_user_changepassword]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_user_delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_user_delete]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_user_deleteavatar]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_user_deleteavatar]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_user_deleteold]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_user_deleteold]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_user_emails]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_user_emails]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_user_find]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_user_find]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_user_getsignature]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_user_getsignature]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_user_guest]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_user_guest]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_user_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_user_list]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_user_login]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_user_login]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_user_nntp]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_user_nntp]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_user_recoverpassword]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_user_recoverpassword]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_user_save]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_user_save]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_user_saveavatar]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_user_saveavatar]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_user_savesignature]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_user_savesignature]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_user_suspend]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_user_suspend]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_user_upgrade]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_user_upgrade]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_userforum_delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_userforum_delete]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_userforum_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_userforum_list]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_userforum_save]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_userforum_save]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_usergroup_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_usergroup_list]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_usergroup_save]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_usergroup_save]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_userpmessage_delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_userpmessage_delete]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_userpmessage_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_userpmessage_list]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_watchforum_add]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_watchforum_add]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_watchforum_check]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_watchforum_check]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_watchforum_delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_watchforum_delete]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_watchforum_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_watchforum_list]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_watchtopic_add]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_watchtopic_add]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_watchtopic_check]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_watchtopic_check]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_watchtopic_delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_watchtopic_delete]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_watchtopic_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)

drop procedure [dbo].[yaf_watchtopic_list]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_vaccess]') and OBJECTPROPERTY(id, N'IsView') = 1)

drop view [dbo].[yaf_vaccess]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_Battles]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[OrionsBelt_Battles]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_ExceptionLog]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[OrionsBelt_ExceptionLog]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_MasterSkins]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[OrionsBelt_MasterSkins]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_Messages]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[OrionsBelt_Messages]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_NamedPage]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[OrionsBelt_NamedPage]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_News]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[OrionsBelt_News]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_Roles]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[OrionsBelt_Roles]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_Scans]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[OrionsBelt_Scans]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_SectionRoles]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[OrionsBelt_SectionRoles]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_Sections]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[OrionsBelt_Sections]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_Universe]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[OrionsBelt_Universe]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_UserRoles]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[OrionsBelt_UserRoles]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OrionsBelt_Users]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[OrionsBelt_Users]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_AccessMask]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_AccessMask]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_Active]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_Active]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_Attachment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_Attachment]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_BannedIP]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_BannedIP]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_Board]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_Board]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_Category]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_Category]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_CheckEmail]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_CheckEmail]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_Choice]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_Choice]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_Forum]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_Forum]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_ForumAccess]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_ForumAccess]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_Group]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_Group]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_Mail]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_Mail]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_Message]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_Message]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_NntpForum]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_NntpForum]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_NntpServer]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_NntpServer]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_NntpTopic]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_NntpTopic]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_PMessage]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_PMessage]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_Poll]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_Poll]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_Rank]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_Rank]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_Registry]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_Registry]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_Replace_Words]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_Replace_Words]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_Smiley]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_Smiley]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_Topic]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_Topic]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_UserForum]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_UserForum]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_UserGroup]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_UserGroup]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_UserPMessage]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_UserPMessage]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_WatchForum]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_WatchForum]

GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[yaf_WatchTopic]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

drop table [dbo].[yaf_WatchTopic]

GO



if not exists (select * from master.dbo.syslogins where loginname = N'csk')

BEGIN

	declare @logindb nvarchar(132), @loginlang nvarchar(132) select @logindb = N'master', @loginlang = N'us_english'

	if @logindb is null or not exists (select * from master.dbo.sysdatabases where name = @logindb)

		select @logindb = N'master'

	if @loginlang is null or (not exists (select * from master.dbo.syslanguages where name = @loginlang) and @loginlang <> N'us_english')

		select @loginlang = @@language

	exec sp_addlogin N'csk', null, @logindb, @loginlang

END

GO



if not exists (select * from master.dbo.syslogins where loginname = N'demo')

BEGIN

	declare @logindb nvarchar(132), @loginlang nvarchar(132) select @logindb = N'master', @loginlang = N'us_english'

	if @logindb is null or not exists (select * from master.dbo.sysdatabases where name = @logindb)

		select @logindb = N'master'

	if @loginlang is null or (not exists (select * from master.dbo.syslanguages where name = @loginlang) and @loginlang <> N'us_english')

		select @loginlang = @@language

	exec sp_addlogin N'demo', null, @logindb, @loginlang

END

GO



if not exists (select * from master.dbo.syslogins where loginname = N'orionsbelt')

BEGIN

	declare @logindb nvarchar(132), @loginlang nvarchar(132) select @logindb = N'orionsbelt', @loginlang = N'us_english'

	if @logindb is null or not exists (select * from master.dbo.sysdatabases where name = @logindb)

		select @logindb = N'master'

	if @loginlang is null or (not exists (select * from master.dbo.syslanguages where name = @loginlang) and @loginlang <> N'us_english')

		select @loginlang = @@language

	exec sp_addlogin N'orionsbelt', null, @logindb, @loginlang

END

GO



if not exists (select * from master.dbo.syslogins where loginname = N'tst')

BEGIN

	declare @logindb nvarchar(132), @loginlang nvarchar(132) select @logindb = N'master', @loginlang = N'us_english'

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



exec sp_addsrvrolemember N'tst', sysadmin

GO



if not exists (select * from dbo.sysusers where name = N'orionsbelt' and uid < 16382)

	EXEC sp_grantdbaccess N'orionsbelt', N'orionsbelt'

GO



exec sp_addrolemember N'db_owner', N'orionsbelt'

GO



CREATE TABLE [dbo].[OrionsBelt_Battles] (

	[battle_id] [int] NOT NULL ,

	[battle_data] [image] NOT NULL 

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO



CREATE TABLE [dbo].[OrionsBelt_ExceptionLog] (

	[exceptionLog_id] [int] IDENTITY (1, 1) NOT NULL ,

	[exceptionLog_name] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,

	[exceptionLog_message] [nvarchar] (500) COLLATE Latin1_General_CI_AS NOT NULL ,

	[exceptionLog_stackTrace] [nvarchar] (3000) COLLATE Latin1_General_CI_AS NOT NULL ,

	[exceptionLog_date] [datetime] NOT NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[OrionsBelt_MasterSkins] (

	[masterSkin_id] [int] IDENTITY (1, 1) NOT NULL ,

	[masterSkin_name] [varchar] (30) COLLATE Latin1_General_CI_AS NOT NULL ,

	[masterSkin_style] [varchar] (30) COLLATE Latin1_General_CI_AS NOT NULL ,

	[masterSkin_script] [varchar] (30) COLLATE Latin1_General_CI_AS NOT NULL ,

	[masterSkin_description] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,

	[masterSkin_count] [int] NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[OrionsBelt_Messages] (

	[message_uniqueId] [int] IDENTITY (1, 1) NOT FOR REPLICATION  NOT NULL ,

	[message_id] [int] NOT NULL ,

	[message_identifier] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,

	[message_read] [bit] NOT NULL ,

	[message_type] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,

	[message_data] [image] NOT NULL 

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO



CREATE TABLE [dbo].[OrionsBelt_NamedPage] (

	[namedPage_id] [int] IDENTITY (1, 1) NOT NULL ,

	[namedPage_path] [varchar] (30) COLLATE Latin1_General_CI_AS NOT NULL ,

	[namedPage_name] [varchar] (30) COLLATE Latin1_General_CI_AS NOT NULL ,

	[namedPage_title] [varchar] (30) COLLATE Latin1_General_CI_AS NOT NULL ,

	[namedPage_content] [varchar] (30) COLLATE Latin1_General_CI_AS NOT NULL ,

	[namedPage_description] [varchar] (30) COLLATE Latin1_General_CI_AS NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[OrionsBelt_News] (

	[news_title] [nvarchar] (400) COLLATE Latin1_General_CI_AS NOT NULL ,

	[news_content] [nvarchar] (4000) COLLATE Latin1_General_CI_AS NOT NULL ,

	[news_date] [datetime] NOT NULL ,

	[news_id] [int] IDENTITY (1, 1) NOT FOR REPLICATION  NOT NULL ,

	[news_lang] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[OrionsBelt_Roles] (

	[roles_id] [int] IDENTITY (1, 1) NOT NULL ,

	[roles_roleName] [varchar] (20) COLLATE Latin1_General_CI_AS NOT NULL ,

	[roles_roleDescription] [varchar] (50) COLLATE Latin1_General_CI_AS NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[OrionsBelt_Scans] (

	[scans_id] [int] IDENTITY (1, 1) NOT FOR REPLICATION  NOT NULL ,

	[scans_sourcePlanetId] [int] NOT NULL ,

	[scans_data] [image] NOT NULL 

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO



CREATE TABLE [dbo].[OrionsBelt_SectionRoles] (

	[sectionroles_section_id] [int] NOT NULL ,

	[sectionroles_role_id] [int] NOT NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[OrionsBelt_Sections] (

	[section_id] [int] IDENTITY (1, 1) NOT NULL ,

	[section_parentId] [int] NOT NULL ,

	[section_name] [varchar] (30) COLLATE Latin1_General_CI_AS NOT NULL ,

	[section_title] [varchar] (30) COLLATE Latin1_General_CI_AS NOT NULL ,

	[section_skin] [int] NULL ,

	[section_content] [varchar] (30) COLLATE Latin1_General_CI_AS NOT NULL ,

	[section_description] [varchar] (30) COLLATE Latin1_General_CI_AS NULL ,

	[section_iconId] [int] NULL ,

	[section_order] [int] NOT NULL ,

	[section_isVisible] [int] NOT NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[OrionsBelt_Universe] (

	[id] [int] IDENTITY (1, 1) NOT FOR REPLICATION  NOT NULL ,

	[data] [image] NOT NULL ,

	[date] [datetime] NOT NULL 

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO



CREATE TABLE [dbo].[OrionsBelt_UserRoles] (

	[user_id] [int] NOT NULL ,

	[roles_id] [int] NOT NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[OrionsBelt_Users] (

	[user_id] [int] IDENTITY (1, 1) NOT FOR REPLICATION  NOT NULL ,

	[user_ruler_id] [int] NULL ,

	[user_registDate] [datetime] NULL ,

	[user_lastLogin] [datetime] NULL ,

	[user_mail] [varchar] (30) COLLATE Latin1_General_CI_AS NOT NULL ,

	[user_pass] [char] (40) COLLATE Latin1_General_CI_AS NOT NULL ,

	[user_nick] [varchar] (30) COLLATE Latin1_General_CI_AS NOT NULL ,

	[user_website] [varchar] (500) COLLATE Latin1_General_CI_AS NOT NULL ,

	[user_avatar] [varchar] (250) COLLATE Latin1_General_CI_AS NOT NULL ,

	[user_skin] [int] NOT NULL ,

	[user_lang] [varchar] (5) COLLATE Latin1_General_CI_AS NOT NULL ,

	[user_imagesDir] [varchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,

	[user_msn] [varchar] (30) COLLATE Latin1_General_CI_AS NOT NULL ,

	[user_icq] [varchar] (30) COLLATE Latin1_General_CI_AS NOT NULL ,

	[user_jabber] [varchar] (30) COLLATE Latin1_General_CI_AS NOT NULL ,

	[user_aim] [varchar] (30) COLLATE Latin1_General_CI_AS NOT NULL ,

	[user_yahoo] [varchar] (30) COLLATE Latin1_General_CI_AS NOT NULL ,

	[user_signature] [varchar] (255) COLLATE Latin1_General_CI_AS NOT NULL ,

	[user_boardId] [int] NOT NULL ,

	[user_IP] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,

	[user_numPosts] [int] NOT NULL ,

	[user_location] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,

	[user_timezone] [int] NOT NULL ,

	[user_rankId] [int] NOT NULL ,

	[user_gender] [tinyint] NOT NULL ,

	[user_flags] [int] NOT NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_AccessMask] (

	[AccessMaskID] [int] IDENTITY (1, 1) NOT NULL ,

	[BoardID] [int] NOT NULL ,

	[Name] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,

	[Flags] [int] NOT NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_Active] (

	[SessionID] [nvarchar] (24) COLLATE Latin1_General_CI_AS NOT NULL ,

	[BoardID] [int] NOT NULL ,

	[UserID] [int] NOT NULL ,

	[IP] [nvarchar] (15) COLLATE Latin1_General_CI_AS NOT NULL ,

	[Login] [datetime] NOT NULL ,

	[LastActive] [datetime] NOT NULL ,

	[Location] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,

	[ForumID] [int] NULL ,

	[TopicID] [int] NULL ,

	[Browser] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,

	[Platform] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_Attachment] (

	[AttachmentID] [int] IDENTITY (1, 1) NOT NULL ,

	[MessageID] [int] NOT NULL ,

	[FileName] [nvarchar] (250) COLLATE Latin1_General_CI_AS NOT NULL ,

	[Bytes] [int] NOT NULL ,

	[FileID] [int] NULL ,

	[ContentType] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,

	[Downloads] [int] NOT NULL ,

	[FileData] [image] NULL 

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_BannedIP] (

	[ID] [int] IDENTITY (1, 1) NOT NULL ,

	[BoardID] [int] NOT NULL ,

	[Mask] [nvarchar] (15) COLLATE Latin1_General_CI_AS NOT NULL ,

	[Since] [datetime] NOT NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_Board] (

	[BoardID] [int] IDENTITY (1, 1) NOT NULL ,

	[Name] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,

	[AllowThreaded] [bit] NOT NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_Category] (

	[CategoryID] [int] IDENTITY (1, 1) NOT NULL ,

	[BoardID] [int] NOT NULL ,

	[Name] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,

	[SortOrder] [smallint] NOT NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_CheckEmail] (

	[CheckEmailID] [int] IDENTITY (1, 1) NOT NULL ,

	[UserID] [int] NOT NULL ,

	[Email] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,

	[Created] [datetime] NOT NULL ,

	[Hash] [nvarchar] (32) COLLATE Latin1_General_CI_AS NOT NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_Choice] (

	[ChoiceID] [int] IDENTITY (1, 1) NOT NULL ,

	[PollID] [int] NOT NULL ,

	[Choice] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,

	[Votes] [int] NOT NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_Forum] (

	[ForumID] [int] IDENTITY (1, 1) NOT NULL ,

	[CategoryID] [int] NOT NULL ,

	[ParentID] [int] NULL ,

	[Name] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,

	[Description] [nvarchar] (255) COLLATE Latin1_General_CI_AS NOT NULL ,

	[SortOrder] [smallint] NOT NULL ,

	[LastPosted] [datetime] NULL ,

	[LastTopicID] [int] NULL ,

	[LastMessageID] [int] NULL ,

	[LastUserID] [int] NULL ,

	[LastUserName] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,

	[NumTopics] [int] NOT NULL ,

	[NumPosts] [int] NOT NULL ,

	[RemoteURL] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,

	[Flags] [int] NOT NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_ForumAccess] (

	[GroupID] [int] NOT NULL ,

	[ForumID] [int] NOT NULL ,

	[AccessMaskID] [int] NOT NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_Group] (

	[GroupID] [int] IDENTITY (1, 1) NOT NULL ,

	[BoardID] [int] NOT NULL ,

	[Name] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,

	[Flags] [int] NOT NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_Mail] (

	[MailID] [int] IDENTITY (1, 1) NOT NULL ,

	[FromUser] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,

	[ToUser] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,

	[Created] [datetime] NOT NULL ,

	[Subject] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,

	[Body] [ntext] COLLATE Latin1_General_CI_AS NOT NULL 

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_Message] (

	[MessageID] [int] IDENTITY (1, 1) NOT NULL ,

	[TopicID] [int] NOT NULL ,

	[ReplyTo] [int] NULL ,

	[Position] [int] NOT NULL ,

	[Indent] [int] NOT NULL ,

	[UserID] [int] NOT NULL ,

	[UserName] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,

	[Posted] [datetime] NOT NULL ,

	[Message] [ntext] COLLATE Latin1_General_CI_AS NOT NULL ,

	[IP] [nvarchar] (15) COLLATE Latin1_General_CI_AS NOT NULL ,

	[Edited] [datetime] NULL ,

	[Flags] [int] NOT NULL 

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_NntpForum] (

	[NntpForumID] [int] IDENTITY (1, 1) NOT NULL ,

	[NntpServerID] [int] NOT NULL ,

	[GroupName] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,

	[ForumID] [int] NOT NULL ,

	[LastMessageNo] [int] NOT NULL ,

	[LastUpdate] [datetime] NOT NULL ,

	[Active] [bit] NOT NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_NntpServer] (

	[NntpServerID] [int] IDENTITY (1, 1) NOT NULL ,

	[BoardID] [int] NOT NULL ,

	[Name] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,

	[Address] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,

	[Port] [int] NULL ,

	[UserName] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,

	[UserPass] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_NntpTopic] (

	[NntpTopicID] [int] IDENTITY (1, 1) NOT NULL ,

	[NntpForumID] [int] NOT NULL ,

	[Thread] [char] (32) COLLATE Latin1_General_CI_AS NOT NULL ,

	[TopicID] [int] NOT NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_PMessage] (

	[PMessageID] [int] IDENTITY (1, 1) NOT NULL ,

	[FromUserID] [int] NOT NULL ,

	[Created] [datetime] NOT NULL ,

	[Subject] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,

	[Body] [ntext] COLLATE Latin1_General_CI_AS NOT NULL ,

	[Flags] [int] NOT NULL 

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_Poll] (

	[PollID] [int] IDENTITY (1, 1) NOT NULL ,

	[Question] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_Rank] (

	[RankID] [int] IDENTITY (1, 1) NOT NULL ,

	[BoardID] [int] NOT NULL ,

	[Name] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,

	[MinPosts] [int] NULL ,

	[RankImage] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,

	[Flags] [int] NOT NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_Registry] (

	[RegistryID] [int] IDENTITY (1, 1) NOT NULL ,

	[Name] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,

	[Value] [nvarchar] (400) COLLATE Latin1_General_CI_AS NULL ,

	[BoardID] [int] NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_Replace_Words] (

	[id] [int] IDENTITY (1, 1) NOT NULL ,

	[badword] [nvarchar] (255) COLLATE Latin1_General_CI_AS NULL ,

	[goodword] [nvarchar] (255) COLLATE Latin1_General_CI_AS NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_Smiley] (

	[SmileyID] [int] IDENTITY (1, 1) NOT NULL ,

	[BoardID] [int] NOT NULL ,

	[Code] [nvarchar] (10) COLLATE Latin1_General_CI_AS NOT NULL ,

	[Icon] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,

	[Emoticon] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_Topic] (

	[TopicID] [int] IDENTITY (1, 1) NOT NULL ,

	[ForumID] [int] NOT NULL ,

	[UserID] [int] NOT NULL ,

	[UserName] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,

	[Posted] [datetime] NOT NULL ,

	[Topic] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,

	[Views] [int] NOT NULL ,

	[Priority] [smallint] NOT NULL ,

	[PollID] [int] NULL ,

	[TopicMovedID] [int] NULL ,

	[LastPosted] [datetime] NULL ,

	[LastMessageID] [int] NULL ,

	[LastUserID] [int] NULL ,

	[LastUserName] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,

	[NumPosts] [int] NOT NULL ,

	[Flags] [int] NOT NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_UserForum] (

	[UserID] [int] NOT NULL ,

	[ForumID] [int] NOT NULL ,

	[AccessMaskID] [int] NOT NULL ,

	[Invited] [datetime] NOT NULL ,

	[Accepted] [bit] NOT NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_UserGroup] (

	[UserID] [int] NOT NULL ,

	[GroupID] [int] NOT NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_UserPMessage] (

	[UserPMessageID] [int] IDENTITY (1, 1) NOT NULL ,

	[UserID] [int] NOT NULL ,

	[PMessageID] [int] NOT NULL ,

	[IsRead] [bit] NOT NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_WatchForum] (

	[WatchForumID] [int] IDENTITY (1, 1) NOT NULL ,

	[ForumID] [int] NOT NULL ,

	[UserID] [int] NOT NULL ,

	[Created] [datetime] NOT NULL ,

	[LastMail] [datetime] NULL 

) ON [PRIMARY]

GO



CREATE TABLE [dbo].[yaf_WatchTopic] (

	[WatchTopicID] [int] IDENTITY (1, 1) NOT NULL ,

	[TopicID] [int] NOT NULL ,

	[UserID] [int] NOT NULL ,

	[Created] [datetime] NOT NULL ,

	[LastMail] [datetime] NULL 

) ON [PRIMARY]

GO



ALTER TABLE [dbo].[OrionsBelt_ExceptionLog] WITH NOCHECK ADD 

	CONSTRAINT [PK_OrionsBelt_ExceptionLog] PRIMARY KEY  CLUSTERED 

	(

		[exceptionLog_id]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[OrionsBelt_MasterSkins] WITH NOCHECK ADD 

	CONSTRAINT [masterSkin_id] PRIMARY KEY  CLUSTERED 

	(

		[masterSkin_id]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[OrionsBelt_NamedPage] WITH NOCHECK ADD 

	CONSTRAINT [pk_namedPageId] PRIMARY KEY  CLUSTERED 

	(

		[namedPage_id]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[OrionsBelt_News] WITH NOCHECK ADD 

	CONSTRAINT [PK_OrionsBelt_News] PRIMARY KEY  CLUSTERED 

	(

		[news_id]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[OrionsBelt_Roles] WITH NOCHECK ADD 

	CONSTRAINT [PK_OrionsBelt_Roles] PRIMARY KEY  CLUSTERED 

	(

		[roles_id]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[OrionsBelt_Scans] WITH NOCHECK ADD 

	CONSTRAINT [PK_OrionsBelt_Scans] PRIMARY KEY  CLUSTERED 

	(

		[scans_id]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[OrionsBelt_SectionRoles] WITH NOCHECK ADD 

	CONSTRAINT [pk_sectionroles_section_roles] PRIMARY KEY  CLUSTERED 

	(

		[sectionroles_role_id],

		[sectionroles_section_id]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[OrionsBelt_Sections] WITH NOCHECK ADD 

	CONSTRAINT [pk_sectionId] PRIMARY KEY  CLUSTERED 

	(

		[section_id]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[OrionsBelt_Users] WITH NOCHECK ADD 

	CONSTRAINT [pk_user_id] PRIMARY KEY  CLUSTERED 

	(

		[user_id]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_AccessMask] WITH NOCHECK ADD 

	CONSTRAINT [PK_AccessMask] PRIMARY KEY  CLUSTERED 

	(

		[AccessMaskID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_Active] WITH NOCHECK ADD 

	CONSTRAINT [PK_Active] PRIMARY KEY  CLUSTERED 

	(

		[SessionID],

		[BoardID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_Attachment] WITH NOCHECK ADD 

	CONSTRAINT [PK_Attachment] PRIMARY KEY  CLUSTERED 

	(

		[AttachmentID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_BannedIP] WITH NOCHECK ADD 

	CONSTRAINT [PK_BannedIP] PRIMARY KEY  CLUSTERED 

	(

		[ID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_Board] WITH NOCHECK ADD 

	CONSTRAINT [PK_Board] PRIMARY KEY  CLUSTERED 

	(

		[BoardID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_Category] WITH NOCHECK ADD 

	CONSTRAINT [PK_Category] PRIMARY KEY  CLUSTERED 

	(

		[CategoryID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_CheckEmail] WITH NOCHECK ADD 

	CONSTRAINT [PK_CheckEmail] PRIMARY KEY  CLUSTERED 

	(

		[CheckEmailID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_Choice] WITH NOCHECK ADD 

	CONSTRAINT [PK_Choice] PRIMARY KEY  CLUSTERED 

	(

		[ChoiceID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_Forum] WITH NOCHECK ADD 

	CONSTRAINT [PK_Forum] PRIMARY KEY  CLUSTERED 

	(

		[ForumID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_ForumAccess] WITH NOCHECK ADD 

	CONSTRAINT [PK_ForumAccess] PRIMARY KEY  CLUSTERED 

	(

		[GroupID],

		[ForumID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_Group] WITH NOCHECK ADD 

	CONSTRAINT [PK_Group] PRIMARY KEY  CLUSTERED 

	(

		[GroupID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_Mail] WITH NOCHECK ADD 

	CONSTRAINT [PK_Mail] PRIMARY KEY  CLUSTERED 

	(

		[MailID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_Message] WITH NOCHECK ADD 

	CONSTRAINT [PK_Message] PRIMARY KEY  CLUSTERED 

	(

		[MessageID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_NntpForum] WITH NOCHECK ADD 

	CONSTRAINT [PK_NntpForum] PRIMARY KEY  CLUSTERED 

	(

		[NntpForumID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_NntpServer] WITH NOCHECK ADD 

	CONSTRAINT [PK_NntpServer] PRIMARY KEY  CLUSTERED 

	(

		[NntpServerID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_NntpTopic] WITH NOCHECK ADD 

	CONSTRAINT [PK_NntpTopic] PRIMARY KEY  CLUSTERED 

	(

		[NntpTopicID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_PMessage] WITH NOCHECK ADD 

	CONSTRAINT [PK_PMessage] PRIMARY KEY  CLUSTERED 

	(

		[PMessageID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_Poll] WITH NOCHECK ADD 

	CONSTRAINT [PK_Poll] PRIMARY KEY  CLUSTERED 

	(

		[PollID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_Rank] WITH NOCHECK ADD 

	CONSTRAINT [PK_Rank] PRIMARY KEY  CLUSTERED 

	(

		[RankID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_Registry] WITH NOCHECK ADD 

	CONSTRAINT [PK_Registry] PRIMARY KEY  CLUSTERED 

	(

		[RegistryID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_Replace_Words] WITH NOCHECK ADD 

	CONSTRAINT [PK_Replace_Words] PRIMARY KEY  CLUSTERED 

	(

		[id]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_Smiley] WITH NOCHECK ADD 

	CONSTRAINT [PK_Smiley] PRIMARY KEY  CLUSTERED 

	(

		[SmileyID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_Topic] WITH NOCHECK ADD 

	CONSTRAINT [PK_Topic] PRIMARY KEY  CLUSTERED 

	(

		[TopicID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_UserForum] WITH NOCHECK ADD 

	CONSTRAINT [PK_UserForum] PRIMARY KEY  CLUSTERED 

	(

		[UserID],

		[ForumID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_UserGroup] WITH NOCHECK ADD 

	CONSTRAINT [PK_UserGroup] PRIMARY KEY  CLUSTERED 

	(

		[UserID],

		[GroupID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_UserPMessage] WITH NOCHECK ADD 

	CONSTRAINT [PK_UserPMessage] PRIMARY KEY  CLUSTERED 

	(

		[UserPMessageID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_WatchForum] WITH NOCHECK ADD 

	CONSTRAINT [PK_WatchForum] PRIMARY KEY  CLUSTERED 

	(

		[WatchForumID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_WatchTopic] WITH NOCHECK ADD 

	CONSTRAINT [PK_WatchTopic] PRIMARY KEY  CLUSTERED 

	(

		[WatchTopicID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[OrionsBelt_MasterSkins] WITH NOCHECK ADD 

	CONSTRAINT [DF__MasterSki__maste__19DFD96B] DEFAULT (1) FOR [masterSkin_count]

GO



ALTER TABLE [dbo].[OrionsBelt_Messages] WITH NOCHECK ADD 

	CONSTRAINT [DF_OrionsBelt_Messages_message_read] DEFAULT (0) FOR [message_read],

	CONSTRAINT [DF_OrionsBelt_Messages_message_type] DEFAULT ('') FOR [message_type]

GO



ALTER TABLE [dbo].[OrionsBelt_News] WITH NOCHECK ADD 

	CONSTRAINT [DF_OrionsBelt_News_news_date] DEFAULT (getdate()) FOR [news_date],

	CONSTRAINT [DF_OrionsBelt_News_news_lang] DEFAULT ('pt') FOR [news_lang]

GO



ALTER TABLE [dbo].[OrionsBelt_Sections] WITH NOCHECK ADD 

	CONSTRAINT [DF_OrionsBelt_Sections_section_isVisible] DEFAULT (1) FOR [section_isVisible]

GO



ALTER TABLE [dbo].[OrionsBelt_Universe] WITH NOCHECK ADD 

	CONSTRAINT [DF_OrionsBelt_Universe_date] DEFAULT (getdate()) FOR [date]

GO



ALTER TABLE [dbo].[OrionsBelt_Users] WITH NOCHECK ADD 

	CONSTRAINT [DF_OrionsBelt_Users_user_ruler_id] DEFAULT ((-1)) FOR [user_ruler_id],

	CONSTRAINT [DF__Users__user_regi__3E1D39E1] DEFAULT (getdate()) FOR [user_registDate],

	CONSTRAINT [DF_OrionsBelt_Users_user_lastLogin] DEFAULT (getdate()) FOR [user_lastLogin],

	CONSTRAINT [DF_OrionsBelt_Users_user_mail] DEFAULT ('') FOR [user_mail],

	CONSTRAINT [DF_Users_user_website] DEFAULT ('') FOR [user_website],

	CONSTRAINT [DF_Users_user_avatar] DEFAULT ('') FOR [user_avatar],

	CONSTRAINT [DF_Users_user_skin] DEFAULT (4) FOR [user_skin],

	CONSTRAINT [DF_OrionsBelt_Users_user_imagesDir] DEFAULT ('') FOR [user_imagesDir],

	CONSTRAINT [DF_Users_user_msn] DEFAULT ('') FOR [user_msn],

	CONSTRAINT [DF_Users_user_icq] DEFAULT ('') FOR [user_icq],

	CONSTRAINT [DF_Users_user_jabber] DEFAULT ('') FOR [user_jabber],

	CONSTRAINT [DF_Users_user_aim] DEFAULT ('') FOR [user_aim],

	CONSTRAINT [DF_Users_user_yahoo] DEFAULT ('') FOR [user_yahoo],

	CONSTRAINT [DF_Users_user_signature] DEFAULT ('') FOR [user_signature],

	CONSTRAINT [DF_OrionsBelt_Users_user_boardId] DEFAULT (1) FOR [user_boardId],

	CONSTRAINT [DF_OrionsBelt_Users_user_numPosts] DEFAULT (0) FOR [user_numPosts],

	CONSTRAINT [DF_OrionsBelt_Users_user_timezone] DEFAULT (0) FOR [user_timezone],

	CONSTRAINT [DF_OrionsBelt_Users_user_rankId] DEFAULT (3) FOR [user_rankId],

	CONSTRAINT [DF_OrionsBelt_Users_user_gender] DEFAULT (0) FOR [user_gender],

	CONSTRAINT [DF_OrionsBelt_Users_user_flags] DEFAULT (2) FOR [user_flags]

GO



ALTER TABLE [dbo].[yaf_AccessMask] WITH NOCHECK ADD 

	CONSTRAINT [DF_yaf_AccessMask_Flags] DEFAULT (0) FOR [Flags]

GO



ALTER TABLE [dbo].[yaf_BannedIP] WITH NOCHECK ADD 

	CONSTRAINT [IX_BannedIP] UNIQUE  NONCLUSTERED 

	(

		[BoardID],

		[Mask]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_Category] WITH NOCHECK ADD 

	CONSTRAINT [IX_Category] UNIQUE  NONCLUSTERED 

	(

		[BoardID],

		[Name]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_CheckEmail] WITH NOCHECK ADD 

	CONSTRAINT [IX_CheckEmail] UNIQUE  NONCLUSTERED 

	(

		[Hash]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_Forum] WITH NOCHECK ADD 

	CONSTRAINT [DF_yaf_Forum_Flags] DEFAULT (0) FOR [Flags],

	CONSTRAINT [IX_Forum] UNIQUE  NONCLUSTERED 

	(

		[CategoryID],

		[Name]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_Group] WITH NOCHECK ADD 

	CONSTRAINT [DF_yaf_Group_Flags] DEFAULT (0) FOR [Flags],

	CONSTRAINT [IX_Group] UNIQUE  NONCLUSTERED 

	(

		[BoardID],

		[Name]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_Message] WITH NOCHECK ADD 

	CONSTRAINT [DF_yaf_Message_Flags] DEFAULT (23) FOR [Flags]

GO



ALTER TABLE [dbo].[yaf_Rank] WITH NOCHECK ADD 

	CONSTRAINT [DF_yaf_Rank_Flags] DEFAULT (0) FOR [Flags],

	CONSTRAINT [IX_Rank] UNIQUE  NONCLUSTERED 

	(

		[BoardID],

		[Name]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_Smiley] WITH NOCHECK ADD 

	CONSTRAINT [IX_Smiley] UNIQUE  NONCLUSTERED 

	(

		[BoardID],

		[Code]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_Topic] WITH NOCHECK ADD 

	CONSTRAINT [DF_yaf_Topic_Flags] DEFAULT (0) FOR [Flags]

GO



ALTER TABLE [dbo].[yaf_WatchForum] WITH NOCHECK ADD 

	CONSTRAINT [IX_WatchForum] UNIQUE  NONCLUSTERED 

	(

		[ForumID],

		[UserID]

	)  ON [PRIMARY] 

GO



ALTER TABLE [dbo].[yaf_WatchTopic] WITH NOCHECK ADD 

	CONSTRAINT [IX_WatchTopic] UNIQUE  NONCLUSTERED 

	(

		[TopicID],

		[UserID]

	)  ON [PRIMARY] 

GO



 CREATE  UNIQUE  INDEX [IX_Name] ON [dbo].[yaf_Registry]([BoardID], [Name]) ON [PRIMARY]

GO



ALTER TABLE [dbo].[OrionsBelt_Sections] ADD 

	CONSTRAINT [fk_skin] FOREIGN KEY 

	(

		[section_skin]

	) REFERENCES [dbo].[OrionsBelt_MasterSkins] (

		[masterSkin_id]

	)

GO



ALTER TABLE [dbo].[OrionsBelt_UserRoles] ADD 

	CONSTRAINT [FK_OrionsBelt_UserRoles_OrionsBelt_Roles] FOREIGN KEY 

	(

		[roles_id]

	) REFERENCES [dbo].[OrionsBelt_Roles] (

		[roles_id]

	),

	CONSTRAINT [FK_OrionsBelt_UserRoles_OrionsBelt_Users] FOREIGN KEY 

	(

		[user_id]

	) REFERENCES [dbo].[OrionsBelt_Users] (

		[user_id]

	)

GO



ALTER TABLE [dbo].[yaf_AccessMask] ADD 

	CONSTRAINT [FK_AccessMask_Board] FOREIGN KEY 

	(

		[BoardID]

	) REFERENCES [dbo].[yaf_Board] (

		[BoardID]

	)

GO



ALTER TABLE [dbo].[yaf_Active] ADD 

	CONSTRAINT [FK_Active_Board] FOREIGN KEY 

	(

		[BoardID]

	) REFERENCES [dbo].[yaf_Board] (

		[BoardID]

	),

	CONSTRAINT [FK_Active_Forum] FOREIGN KEY 

	(

		[ForumID]

	) REFERENCES [dbo].[yaf_Forum] (

		[ForumID]

	),

	CONSTRAINT [FK_Active_Topic] FOREIGN KEY 

	(

		[TopicID]

	) REFERENCES [dbo].[yaf_Topic] (

		[TopicID]

	),

	CONSTRAINT [FK_Active_Users] FOREIGN KEY 

	(

		[UserID]

	) REFERENCES [dbo].[OrionsBelt_Users] (

		[user_id]

	),

	CONSTRAINT [FK_yaf_Active_OrionsBelt_Users] FOREIGN KEY 

	(

		[UserID]

	) REFERENCES [dbo].[OrionsBelt_Users] (

		[user_id]

	)

GO



ALTER TABLE [dbo].[yaf_Attachment] ADD 

	CONSTRAINT [FK_Attachment_Message] FOREIGN KEY 

	(

		[MessageID]

	) REFERENCES [dbo].[yaf_Message] (

		[MessageID]

	)

GO



ALTER TABLE [dbo].[yaf_BannedIP] ADD 

	CONSTRAINT [FK_BannedIP_Board] FOREIGN KEY 

	(

		[BoardID]

	) REFERENCES [dbo].[yaf_Board] (

		[BoardID]

	)

GO



ALTER TABLE [dbo].[yaf_Category] ADD 

	CONSTRAINT [FK_Category_Board] FOREIGN KEY 

	(

		[BoardID]

	) REFERENCES [dbo].[yaf_Board] (

		[BoardID]

	)

GO



ALTER TABLE [dbo].[yaf_Choice] ADD 

	CONSTRAINT [FK_Choice_Poll] FOREIGN KEY 

	(

		[PollID]

	) REFERENCES [dbo].[yaf_Poll] (

		[PollID]

	)

GO



ALTER TABLE [dbo].[yaf_Forum] ADD 

	CONSTRAINT [FK_Forum_Category] FOREIGN KEY 

	(

		[CategoryID]

	) REFERENCES [dbo].[yaf_Category] (

		[CategoryID]

	),

	CONSTRAINT [FK_Forum_Forum] FOREIGN KEY 

	(

		[ParentID]

	) REFERENCES [dbo].[yaf_Forum] (

		[ForumID]

	),

	CONSTRAINT [FK_Forum_Message] FOREIGN KEY 

	(

		[LastMessageID]

	) REFERENCES [dbo].[yaf_Message] (

		[MessageID]

	),

	CONSTRAINT [FK_Forum_Topic] FOREIGN KEY 

	(

		[LastTopicID]

	) REFERENCES [dbo].[yaf_Topic] (

		[TopicID]

	),

	CONSTRAINT [FK_yaf_Forum_OrionsBelt_Users] FOREIGN KEY 

	(

		[LastUserID]

	) REFERENCES [dbo].[OrionsBelt_Users] (

		[user_id]

	)

GO



ALTER TABLE [dbo].[yaf_ForumAccess] ADD 

	CONSTRAINT [FK_ForumAccess_AccessMask] FOREIGN KEY 

	(

		[AccessMaskID]

	) REFERENCES [dbo].[yaf_AccessMask] (

		[AccessMaskID]

	),

	CONSTRAINT [FK_ForumAccess_Forum] FOREIGN KEY 

	(

		[ForumID]

	) REFERENCES [dbo].[yaf_Forum] (

		[ForumID]

	),

	CONSTRAINT [FK_ForumAccess_Group] FOREIGN KEY 

	(

		[GroupID]

	) REFERENCES [dbo].[yaf_Group] (

		[GroupID]

	)

GO



ALTER TABLE [dbo].[yaf_Group] ADD 

	CONSTRAINT [FK_Group_Board] FOREIGN KEY 

	(

		[BoardID]

	) REFERENCES [dbo].[yaf_Board] (

		[BoardID]

	)

GO



ALTER TABLE [dbo].[yaf_Message] ADD 

	CONSTRAINT [FK_Message_Message] FOREIGN KEY 

	(

		[ReplyTo]

	) REFERENCES [dbo].[yaf_Message] (

		[MessageID]

	),

	CONSTRAINT [FK_Message_Topic] FOREIGN KEY 

	(

		[TopicID]

	) REFERENCES [dbo].[yaf_Topic] (

		[TopicID]

	),

	CONSTRAINT [FK_yaf_Message_OrionsBelt_Users] FOREIGN KEY 

	(

		[UserID]

	) REFERENCES [dbo].[OrionsBelt_Users] (

		[user_id]

	)

GO



ALTER TABLE [dbo].[yaf_NntpForum] ADD 

	CONSTRAINT [FK_NntpForum_Forum] FOREIGN KEY 

	(

		[ForumID]

	) REFERENCES [dbo].[yaf_Forum] (

		[ForumID]

	),

	CONSTRAINT [FK_NntpForum_NntpServer] FOREIGN KEY 

	(

		[NntpServerID]

	) REFERENCES [dbo].[yaf_NntpServer] (

		[NntpServerID]

	)

GO



ALTER TABLE [dbo].[yaf_NntpServer] ADD 

	CONSTRAINT [FK_NntpServer_Board] FOREIGN KEY 

	(

		[BoardID]

	) REFERENCES [dbo].[yaf_Board] (

		[BoardID]

	)

GO



ALTER TABLE [dbo].[yaf_NntpTopic] ADD 

	CONSTRAINT [FK_NntpTopic_NntpForum] FOREIGN KEY 

	(

		[NntpForumID]

	) REFERENCES [dbo].[yaf_NntpForum] (

		[NntpForumID]

	),

	CONSTRAINT [FK_NntpTopic_Topic] FOREIGN KEY 

	(

		[TopicID]

	) REFERENCES [dbo].[yaf_Topic] (

		[TopicID]

	)

GO



ALTER TABLE [dbo].[yaf_PMessage] ADD 

	CONSTRAINT [FK_yaf_PMessage_OrionsBelt_Users] FOREIGN KEY 

	(

		[FromUserID]

	) REFERENCES [dbo].[OrionsBelt_Users] (

		[user_id]

	)

GO



ALTER TABLE [dbo].[yaf_Rank] ADD 

	CONSTRAINT [FK_Rank_Board] FOREIGN KEY 

	(

		[BoardID]

	) REFERENCES [dbo].[yaf_Board] (

		[BoardID]

	)

GO



ALTER TABLE [dbo].[yaf_Registry] ADD 

	CONSTRAINT [FK_Registry_Board] FOREIGN KEY 

	(

		[BoardID]

	) REFERENCES [dbo].[yaf_Board] (

		[BoardID]

	) ON DELETE CASCADE 

GO



ALTER TABLE [dbo].[yaf_Smiley] ADD 

	CONSTRAINT [FK_Smiley_Board] FOREIGN KEY 

	(

		[BoardID]

	) REFERENCES [dbo].[yaf_Board] (

		[BoardID]

	)

GO



ALTER TABLE [dbo].[yaf_Topic] ADD 

	CONSTRAINT [FK_Topic_Forum] FOREIGN KEY 

	(

		[ForumID]

	) REFERENCES [dbo].[yaf_Forum] (

		[ForumID]

	) ON DELETE CASCADE ,

	CONSTRAINT [FK_Topic_Message] FOREIGN KEY 

	(

		[LastMessageID]

	) REFERENCES [dbo].[yaf_Message] (

		[MessageID]

	),

	CONSTRAINT [FK_Topic_Poll] FOREIGN KEY 

	(

		[PollID]

	) REFERENCES [dbo].[yaf_Poll] (

		[PollID]

	),

	CONSTRAINT [FK_Topic_Topic] FOREIGN KEY 

	(

		[TopicMovedID]

	) REFERENCES [dbo].[yaf_Topic] (

		[TopicID]

	),

	CONSTRAINT [FK_yaf_Topic_OrionsBelt_Users] FOREIGN KEY 

	(

		[UserID]

	) REFERENCES [dbo].[OrionsBelt_Users] (

		[user_id]

	),

	CONSTRAINT [FK_yaf_Topic_OrionsBelt_Users1] FOREIGN KEY 

	(

		[LastUserID]

	) REFERENCES [dbo].[OrionsBelt_Users] (

		[user_id]

	)

GO



ALTER TABLE [dbo].[yaf_UserForum] ADD 

	CONSTRAINT [FK_UserForum_AccessMask] FOREIGN KEY 

	(

		[AccessMaskID]

	) REFERENCES [dbo].[yaf_AccessMask] (

		[AccessMaskID]

	),

	CONSTRAINT [FK_UserForum_Forum] FOREIGN KEY 

	(

		[ForumID]

	) REFERENCES [dbo].[yaf_Forum] (

		[ForumID]

	),

	CONSTRAINT [FK_yaf_UserForum_OrionsBelt_Users] FOREIGN KEY 

	(

		[UserID]

	) REFERENCES [dbo].[OrionsBelt_Users] (

		[user_id]

	)

GO



ALTER TABLE [dbo].[yaf_UserGroup] ADD 

	CONSTRAINT [FK_UserGroup_Group] FOREIGN KEY 

	(

		[GroupID]

	) REFERENCES [dbo].[yaf_Group] (

		[GroupID]

	),

	CONSTRAINT [FK_yaf_UserGroup_OrionsBelt_Users] FOREIGN KEY 

	(

		[UserID]

	) REFERENCES [dbo].[OrionsBelt_Users] (

		[user_id]

	)

GO



ALTER TABLE [dbo].[yaf_UserPMessage] ADD 

	CONSTRAINT [FK_UserPMessage_PMessage] FOREIGN KEY 

	(

		[PMessageID]

	) REFERENCES [dbo].[yaf_PMessage] (

		[PMessageID]

	),

	CONSTRAINT [FK_yaf_UserPMessage_OrionsBelt_Users] FOREIGN KEY 

	(

		[UserID]

	) REFERENCES [dbo].[OrionsBelt_Users] (

		[user_id]

	)

GO



ALTER TABLE [dbo].[yaf_WatchForum] ADD 

	CONSTRAINT [FK_WatchForum_Forum] FOREIGN KEY 

	(

		[ForumID]

	) REFERENCES [dbo].[yaf_Forum] (

		[ForumID]

	),

	CONSTRAINT [FK_yaf_WatchForum_OrionsBelt_Users] FOREIGN KEY 

	(

		[UserID]

	) REFERENCES [dbo].[OrionsBelt_Users] (

		[user_id]

	)

GO



ALTER TABLE [dbo].[yaf_WatchTopic] ADD 

	CONSTRAINT [FK_WatchTopic_Topic] FOREIGN KEY 

	(

		[TopicID]

	) REFERENCES [dbo].[yaf_Topic] (

		[TopicID]

	),

	CONSTRAINT [FK_yaf_WatchTopic_OrionsBelt_Users] FOREIGN KEY 

	(

		[UserID]

	) REFERENCES [dbo].[OrionsBelt_Users] (

		[user_id]

	)

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create view dbo.yaf_vaccess as

	select

		UserID				= a.UserID,

		ForumID				= x.ForumID,

		IsAdmin				= max(convert(int,b.Flags & 1)),

		IsGuest				= max(convert(int,b.Flags & 2)),

		IsForumModerator		= max(convert(int,b.Flags & 8)),

		IsModerator			= (select count(1) from dbo.yaf_UserGroup v,dbo.yaf_Group w,dbo.yaf_ForumAccess x,dbo.yaf_AccessMask y where v.UserID=a.UserID and w.GroupID=v.GroupID and x.GroupID=w.GroupID and y.AccessMaskID=x.AccessMaskID and (y.Flags & 64)<>0),

		ReadAccess			= max(x.ReadAccess),

		PostAccess			= max(x.PostAccess),

		ReplyAccess			= max(x.ReplyAccess),

		PriorityAccess			= max(x.PriorityAccess),

		PollAccess			= max(x.PollAccess),

		VoteAccess			= max(x.VoteAccess),

		ModeratorAccess			= max(x.ModeratorAccess),

		EditAccess			= max(x.EditAccess),

		DeleteAccess			= max(x.DeleteAccess),

		UploadAccess			= max(x.UploadAccess)

	from

		(select

			b.UserID,

			b.ForumID,

			ReadAccess		= convert(int,c.Flags & 1),

			PostAccess		= convert(int,c.Flags & 2),

			ReplyAccess		= convert(int,c.Flags & 4),

			PriorityAccess		= convert(int,c.Flags & 8),

			PollAccess		= convert(int,c.Flags & 16),

			VoteAccess		= convert(int,c.Flags & 32),

			ModeratorAccess		= convert(int,c.Flags & 64),

			EditAccess		= convert(int,c.Flags & 128),

			DeleteAccess		= convert(int,c.Flags & 256),

			UploadAccess		= convert(int,c.Flags & 512)

		from

			dbo.yaf_UserForum b

			join dbo.yaf_AccessMask c on c.AccessMaskID=b.AccessMaskID

		

		union

		

		select

			b.UserID,

			c.ForumID,

			ReadAccess		= convert(int,d.Flags & 1),

			PostAccess		= convert(int,d.Flags & 2),

			ReplyAccess		= convert(int,d.Flags & 4),

			PriorityAccess		= convert(int,d.Flags & 8),

			PollAccess		= convert(int,d.Flags & 16),

			VoteAccess		= convert(int,d.Flags & 32),

			ModeratorAccess		= convert(int,d.Flags & 64),

			EditAccess		= convert(int,d.Flags & 128),

			DeleteAccess		= convert(int,d.Flags & 256),

			UploadAccess		= convert(int,d.Flags & 512)

		from

			dbo.yaf_UserGroup b

			join dbo.yaf_ForumAccess c on c.GroupID=b.GroupID

			join dbo.yaf_AccessMask d on d.AccessMaskID=c.AccessMaskID



		union



		select

			a.user_id,

			ForumID			= convert(int,0),

			ReadAccess		= convert(int,0),

			PostAccess		= convert(int,0),

			ReplyAccess		= convert(int,0),

			PriorityAccess	= convert(int,0),

			PollAccess		= convert(int,0),

			VoteAccess		= convert(int,0),

			ModeratorAccess	= convert(int,0),

			EditAccess		= convert(int,0),

			DeleteAccess	= convert(int,0),

			UploadAccess	= convert(int,0)

		from

			dbo.orionsbelt_users a

		) as x

		join dbo.yaf_UserGroup a on a.UserID=x.UserID

		join dbo.yaf_Group b on b.GroupID=a.GroupID

	group by a.UserID,x.ForumID





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO





CREATE PROC OrionsBelt_ChronosCountMessages (

	@id AS INT,

	@identifier NVARCHAR(50)

)AS



SELECT COUNT(message_read) as readCount

FROM OrionsBelt_Messages

WHERE message_id = @id AND message_identifier = @identifier AND message_read = 0



GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO



create procedure OrionsBelt_ChronosLoadBattle(

	@id INT

)AS



SELECT * FROM OrionsBelt_Battles WHERE battle_id = @id



GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO





CREATE PROC OrionsBelt_ChronosLoadMessage (

	@id AS NVARCHAR(10),

	@identifier NVARCHAR(50),

	@count NVARCHAR(5),

	@types NVARCHAR(200)

)AS



Declare @SQL VarChar(1000)



SELECT @SQL = 'SELECT TOP '+ @count +' message_id,message_identifier,message_read,message_data,message_uniqueId

FROM OrionsBelt_Messages WHERE message_id = '+ @id +' AND message_identifier = ' + @identifier +' '+ @types + ' ORDER BY message_uniqueId DESC'



EXEC (@SQL)



GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO





CREATE PROCEDURE OrionsBelt_ChronosLoadUniverse AS



SELECT TOP 1 data FROM OrionsBelt_Universe ORDER BY date DESC



GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO





CREATE PROC OrionsBelt_ChronosMarkAllAsRead(

	@id AS INT,

	@identifier NVARCHAR(50)

) AS



UPDATE OrionsBelt_Messages

SET message_read = 1 

WHERE message_read = 0 AND message_id = @id AND message_identifier = @identifier



GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO



Create PROCEDURE OrionsBelt_ChronosRemoveBattle(

	@id INT

)AS

DELETE FROM OrionsBelt_battles WHERE battle_id=@id



GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO



CREATE procedure OrionsBelt_ChronosSaveBattle (

	@id INT,

	@data IMAGE

)AS



IF EXISTS(SELECT * FROM OrionsBelt_Battles WHERE  battle_id=@id)

	UPDATE OrionsBelt_Battles SET battle_data=@data WHERE battle_id=@id

ELSE

	INSERT INTO OrionsBelt_Battles(battle_id,battle_data) VALUES (@id,@data)

GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO





CREATE PROC OrionsBelt_ChronosSaveMessage (

	@id AS INT,

	@identifier NVARCHAR(50),

	@type NVARCHAR(50),

	@data IMAGE

)AS



INSERT INTO OrionsBelt_Messages (message_id,message_identifier,message_type,message_data)

VALUES (@id,@identifier,@type,@data)



GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO





CREATE PROCEDURE OrionsBelt_ChronosSaveUniverse(

	@data as IMAGE

) AS





DECLARE @count AS INT

SELECT @count=COUNT(*) FROM OrionsBelt_Universe



IF( @count = 5 )

BEGIN

	DECLARE @id AS INT

	SELECT TOP 1 @id=id FROM OrionsBelt_Universe ORDER BY date ASC



	UPDATE OrionsBelt_Universe SET data = @data,date = getdate()  WHERE id = @id

END	

ELSE

	INSERT INTO OrionsBelt_Universe (data) VALUES (@data)



GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO





CREATE PROCEDURE OrionsBelt_ExceptionLogLoad

AS



SELECT exceptionLog_id,exceptionLog_name,exceptionLog_message, exceptionLog_stackTrace, exceptionLog_date 

FROM OrionsBelt_ExceptionLog 

ORDER BY exceptionLog_date DESC



GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO





CREATE PROC OrionsBelt_ExceptionLogRemoveAllExceptions AS



DELETE FROM OrionsBelt_ExceptionLog





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO





CREATE PROCEDURE OrionsBelt_ExceptionLogRemoveException(

	@id INT

)

AS



DELETE FROM OrionsBelt_ExceptionLog WHERE exceptionLog_id = @id









GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO





CREATE PROCEDURE OrionsBelt_ExceptionLogSave (

	@name NVARCHAR(100),

	@message NVARCHAR(3000),

	@stackTrace NVARCHAR(3000),

	@date DATETIME

) AS



INSERT INTO OrionsBelt_ExceptionLog VALUES(@name,@message,@stackTrace,@date)

GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO





CREATE PROCEDURE OrionsBelt_GetNews AS



SELECT TOP 10 news_id, news_title,news_content,news_date FROM OrionsBelt_News

ORDER BY news_date DESC



GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO



CREATE PROC OrionsBelt_GetScans (

	@count INT,

	@types VARCHAR(500)

)AS



Declare @SQL VarChar(1000)



SELECT @SQL = 'SELECT TOP '+ @count +' * FROM OrionsBelt_Scans WHERE ' +@types 



EXEC (@SQL)





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO





CREATE PROCEDURE OrionsBelt_GetScansById(

	@id INT

)AS



SELECT TOP 10 scans_id, scans_sourcePlanetId, scans_data

FROM OrionsBelt_Scans

WHERE scans_id = @id











GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO





CREATE PROCEDURE OrionsBelt_GetScansByPlanetId(

	@targetPlanetOwner INT

)AS



SELECT TOP 10 scans_id, scans_sourcePlanetId, scans_data

FROM OrionsBelt_Scans

WHERE scans_sourcePlanetId = @targetPlanetOwner

ORDER BY scans_id DESC









GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO





CREATE PROCEDURE OrionsBelt_InsertNews(

	@title NVARCHAR(200),

	@content NVARCHAR(1000)

)AS



INSERT INTO OrionsBelt_News(news_title, news_content) VALUES(@title,@content)







GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO





CREATE PROCEDURE OrionsBelt_InsertScans(

	@sourcePlanetId INT,

	@data IMAGE

)AS



INSERT INTO OrionsBelt_Scans VALUES(@sourcePlanetId,@data)



GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO





CREATE PROCEDURE OrionsBelt_MasterSkinsGetAllMasterSkins

AS

SELECT * FROM OrionsBelt_MasterSkins



GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO





CREATE PROCEDURE OrionsBelt_NamedPagesGetAllNamedPages

AS

SELECT * FROM OrionsBelt_NamedPage;



GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO





--procedimentos para acesso aos dados

CREATE PROCEDURE OrionsBelt_SectionsGetAllSections

AS

SELECT * FROM OrionsBelt_Sections



GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO





CREATE PROCEDURE OrionsBelt_SectionsGetAllSectionsRoles (

	@section_id INT

)AS

select roles_roleName

from OrionsBelt_SectionRoles inner join OrionsBelt_Roles on sectionRoles_role_id = roles_id

where sectionroles_section_id = @section_id



GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







CREATE PROCEDURE OrionsBelt_SectionsGetMotherSections

AS

SELECT section_id FROM OrionsBelt_Sections WHERE section_parentId < 2



GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO





CREATE PROCEDURE OrionsBelt_UsersCheckUser

(

	@user_mail VARCHAR(30),

	@user_pass CHAR(40)

)

AS

SELECT user_id

FROM OrionsBelt_Users

WHERE user_mail = @user_mail AND user_pass = @user_pass



GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO







CREATE PROCEDURE OrionsBelt_UsersCheckUserMail

(

	@user_mail VARCHAR(30)

)

AS

SELECT user_id

FROM OrionsBelt_Users

WHERE user_mail = @user_mail

GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO





CREATE PROCEDURE OrionsBelt_UsersGetAvatar (

	@ruler_id int

)AS



SELECT user_avatar

FROM OrionsBelt_Users

WHERE user_ruler_id = @ruler_id





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO





CREATE PROCEDURE OrionsBelt_UsersGetCount AS



SELECT COUNT(*) as count FROM OrionsBelt_Users





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO





CREATE PROCEDURE OrionsBelt_UsersGetMailFromId(

	@user_id INT

)



AS



SELECT user_mail FROM OrionsBelt_Users WHERE user_id = @user_id







GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO





CREATE PROCEDURE OrionsBelt_UsersGetUser (

	@user_mail VARCHAR(30)

)AS



SELECT *

FROM OrionsBelt_Users

WHERE user_mail = @user_mail



GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS OFF 

GO





CREATE PROCEDURE OrionsBelt_UsersGetUserById(

	@id as INT

) AS



SELECT * FROM Orionsbelt_Users WHERE user_id = @id



GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO





CREATE PROC OrionsBelt_UsersGetUserRoles(

	@user_mail VARCHAR(40)

)AS



BEGIN TRANSACTION trans1



DECLARE @id INT



SELECT @id = user_id

FROM Orionsbelt_Users

WHERE user_mail = @user_mail



SELECT roles_roleName

FROM Orionsbelt_Roles as roles INNER JOIN OrionsBelt_UserRoles as userRoles

ON roles.roles_id = userRoles.roles_id

WHERE user_id = @id



COMMIT TRANSACTION trans1





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO





CREATE PROCEDURE OrionsBelt_UsersGetUsersIdByRole(

@role VARCHAR(20)

) AS



SELECT user_id

FROM OrionsBelt_UserRoles

WHERE roles_id = @role

GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO





CREATE PROCEDURE OrionsBelt_UsersRegisterUser (

	@user_mail VARCHAR(30),

	@user_pass CHAR(40),

	@user_nick VARCHAR(30),

	@user_lang VARCHAR(5)

)

AS







declare @RankID int

declare @Flags int

	

set @Flags = 0

set @Flags = @Flags | 2



select @RankID = RankID from yaf_Rank where (Flags & 1)<>0 and BoardID=1



INSERT INTO OrionsBelt_Users(user_mail,user_pass,user_nick,user_lang,user_rankid,user_flags)VALUES(@user_mail,@user_pass,@user_nick,@user_lang,@RankID,@Flags)



Declare @UserId int



select @UserId = user_id from OrionsBelt_Users where user_mail = @user_mail



insert into yaf_UserGroup(UserID,GroupID) select @UserID,GroupID from yaf_Group where BoardID=1 and (Flags & 4)<>0









GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO





create proc OrionsBelt_UsersResetPassword(

	@user_mail as nvarchar(100),

	@user_password as nvarchar(100)

)as



update orionsbelt_users set user_pass = @user_password

where user_mail = @user_mail





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO





CREATE PROCEDURE OrionsBelt_UsersResetUserRulerId 

AS



INSERT INTO OrionsBelt_Users(user_ruler_Id) VALUES ( -1 )



GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS OFF 

GO





CREATE PROCEDURE OrionsBelt_UsersUpdateLastLogin (

	@user_mail AS VARCHAR(30)

) AS



UPDATE OrionsBelt_Users

SET user_lastLogin = getdate()

WHERE user_mail = @user_mail



GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO





CREATE PROCEDURE OrionsBelt_UsersUpdateUser (

	@user_id INT,

	@user_pass as VARCHAR(40) = '',

	@user_ruler_id INT,

	@user_nick VARCHAR(30),

	@user_skin INT,

	@user_lang VARCHAR(5),

	@user_website VARCHAR(30),

	@user_avatar VARCHAR(250),

	@user_msn VARCHAR(30),

	@user_icq VARCHAR(30),

	@user_jabber VARCHAR(30),

	@user_aim VARCHAR(30),

	@user_yahoo VARCHAR(30),

	@user_signature VARCHAR(255),

	@user_imagesDir VARCHAR(100)

)AS

UPDATE OrionsBelt_Users SET

	user_ruler_id = @user_ruler_id,

	user_nick  = @user_nick,

	user_skin = @user_skin,

	user_lang = @user_lang,

	user_website  = @user_website,

	user_avatar  = @user_avatar,

	user_msn  = @user_msn,

	user_icq  = @user_icq,

	user_jabber  = @user_jabber,

	user_aim  = @user_aim,

	user_yahoo  = @user_yahoo,

	user_signature = @user_signature,

	user_imagesDir = @user_imagesDir

WHERE user_id = @user_id



if( @user_pass <> '' )

BEGIN

	UPDATE OrionsBelt_Users SET user_pass=@user_pass WHERE user_id = @user_id

END







GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_accessmask_delete(@AccessMaskID int) as

begin

	declare @flag int

	

	set @flag=1

	if exists(select 1 from yaf_ForumAccess where AccessMaskID=@AccessMaskID) or exists(select 1 from yaf_UserForum where AccessMaskID=@AccessMaskID)

		set @flag=0

	else

		delete from yaf_AccessMask where AccessMaskID=@AccessMaskID

	

	select @flag

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_accessmask_list(@BoardID int,@AccessMaskID int=null) as

begin

	if @AccessMaskID is null

		select 

			a.* 

		from 

			yaf_AccessMask a 

		where

			a.BoardID = @BoardID

		order by 

			a.Name

	else

		select 

			a.* 

		from 

			yaf_AccessMask a 

		where

			a.BoardID = @BoardID and

			a.AccessMaskID = @AccessMaskID

		order by 

			a.Name

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_accessmask_save(

	@AccessMaskID		int=null,

	@BoardID			int,

	@Name				nvarchar(50),

	@ReadAccess			bit,

	@PostAccess			bit,

	@ReplyAccess		bit,

	@PriorityAccess		bit,

	@PollAccess			bit,

	@VoteAccess			bit,

	@ModeratorAccess	bit,

	@EditAccess			bit,

	@DeleteAccess		bit,

	@UploadAccess		bit

) as

begin

	declare @Flags	int

	

	set @Flags = 0

	if @ReadAccess<>0 set @Flags = @Flags | 1

	if @PostAccess<>0 set @Flags = @Flags | 2

	if @ReplyAccess<>0 set @Flags = @Flags | 4

	if @PriorityAccess<>0 set @Flags = @Flags | 8

	if @PollAccess<>0 set @Flags = @Flags | 16

	if @VoteAccess<>0 set @Flags = @Flags | 32

	if @ModeratorAccess<>0 set @Flags = @Flags | 64

	if @EditAccess<>0 set @Flags = @Flags | 128

	if @DeleteAccess<>0 set @Flags = @Flags | 256

	if @UploadAccess<>0 set @Flags = @Flags | 512



	if @AccessMaskID is null

		insert into yaf_AccessMask(Name,BoardID,Flags)

		values(@Name,@BoardID,@Flags)

	else

		update yaf_AccessMask set

			Name			= @Name,

			Flags			= @Flags

		where AccessMaskID=@AccessMaskID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_active_list(@BoardID int,@Guests bit=0) as

begin

	-- delete non-active

	delete from yaf_Active where DATEDIFF(minute,LastActive,getdate())>5

	-- select active

	if @Guests<>0

		select

			a.user_id,

			a.user_nick,

			a.user_ip,

			c.SessionID,

			c.ForumID,

			c.TopicID,

			ForumName = (select Name from yaf_Forum x where x.ForumID=c.ForumID),

			TopicName = (select Topic from yaf_Topic x where x.TopicID=c.TopicID),

			IsGuest = (select 1 from yaf_UserGroup x,yaf_Group y where x.UserID=a.User_ID and y.GroupID=x.GroupID and (y.Flags & 2)<>0),

			c.Login,

			c.LastActive,

			c.Location,

			Active = DATEDIFF(minute,c.Login,c.LastActive),

			c.Browser,

			c.Platform

		from

			orionsbelt_users a,

			yaf_Active c

		where

			c.UserID = a.User_ID and

			c.BoardID = @BoardID

		order by

			c.LastActive desc

	else

		select

			a.user_id,

			a.user_nick,

			a.user_ip,

			c.SessionID,

			c.ForumID,

			c.TopicID,

			ForumName = (select Name from yaf_Forum x where x.ForumID=c.ForumID),

			TopicName = (select Topic from yaf_Topic x where x.TopicID=c.TopicID),

			IsGuest = (select 1 from yaf_UserGroup x,yaf_Group y where x.UserID=a.User_ID and y.GroupID=x.GroupID and (y.Flags & 2)<>0),

			c.Login,

			c.LastActive,

			c.Location,

			Active = DATEDIFF(minute,c.Login,c.LastActive),

			c.Browser,

			c.Platform

		from

			orionsbelt_users a,

			yaf_Active c

		where

			c.UserID = a.User_ID and

			c.BoardID = @BoardID and

			not exists(select 1 from yaf_UserGroup x,yaf_Group y where x.UserID=a.User_ID and y.GroupID=x.GroupID and (y.Flags & 2)<>0)

		order by

			c.LastActive desc

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_active_listforum(@ForumID int) as

begin

	select

		UserID		= a.UserID,

		UserName	= b.user_nick

	from

		yaf_Active a join orionsbelt_users b on b.user_id=a.UserID

	where

		a.ForumID = @ForumID

	group by

		a.UserID,

		b.user_nick

	order by

		b.user_nick

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_active_listtopic(@TopicID int) as

begin

	select

		UserID		= a.UserID,

		UserName	= b.user_nick

	from

		yaf_Active a with(nolock)

		join orionsbelt_users b on b.user_id=a.UserID

	where

		a.TopicID = @TopicID

	group by

		a.UserID,

		b.user_nick

	order by

		b.user_nick

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_active_stats(@BoardID int) as

begin

	select

		ActiveUsers = (select count(1) from yaf_Active where BoardID=@BoardID),

		ActiveMembers = (select count(1) from yaf_Active x where BoardID=@BoardID and exists(select 1 from yaf_UserGroup y,yaf_Group z where y.UserID=x.UserID and y.GroupID=z.GroupID and (z.Flags & 2)=0)),

		ActiveGuests = (select count(1) from yaf_Active x where BoardID=@BoardID and exists(select 1 from yaf_UserGroup y,yaf_Group z where y.UserID=x.UserID and y.GroupID=z.GroupID and (z.Flags & 2)<>0))

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_attachment_delete(@AttachmentID int) as begin

	delete from yaf_Attachment where AttachmentID=@AttachmentID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_attachment_download(@AttachmentID int) as

begin

	update yaf_Attachment set Downloads=Downloads+1 where AttachmentID=@AttachmentID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_attachment_list(@MessageID int=null,@AttachmentID int=null,@BoardID int=null) as begin

	if @MessageID is not null

		select * from yaf_Attachment where MessageID=@MessageID

	else if @AttachmentID is not null

		select * from yaf_Attachment where AttachmentID=@AttachmentID

	else

		select 

			a.*,

			Posted		= b.Posted,

			ForumID		= d.ForumID,

			ForumName	= d.Name,

			TopicID		= c.TopicID,

			TopicName	= c.Topic

		from 

			yaf_Attachment a,

			yaf_Message b,

			yaf_Topic c,

			yaf_Forum d,

			yaf_Category e

		where

			b.MessageID = a.MessageID and

			c.TopicID = b.TopicID and

			d.ForumID = c.ForumID and

			e.CategoryID = d.CategoryID and

			e.BoardID = @BoardID

		order by

			d.Name,

			c.Topic,

			b.Posted

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_attachment_save(@MessageID int,@FileName nvarchar(50),@Bytes int,@ContentType nvarchar(50)=null,@FileData image=null) as begin

	insert into yaf_Attachment(MessageID,FileName,Bytes,ContentType,Downloads,FileData) values(@MessageID,@FileName,@Bytes,@ContentType,0,@FileData)

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_bannedip_delete(@ID int) as

begin

	delete from yaf_BannedIP where ID = @ID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_bannedip_list(@BoardID int,@ID int=null) as

begin

	if @ID is null

		select * from yaf_BannedIP where BoardID=@BoardID

	else

		select * from yaf_BannedIP where BoardID=@BoardID and ID=@ID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_bannedip_save(@ID int=null,@BoardID int,@Mask nvarchar(15)) as

begin

	if @ID is null or @ID = 0 begin

		insert into yaf_BannedIP(BoardID,Mask,Since) values(@BoardID,@Mask,getdate())

	end

	else begin

		update yaf_BannedIP set Mask = @Mask where ID = @ID

	end

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_board_create(

	@BoardName 		nvarchar(50),

	@AllowThreaded	bit,

	@UserName		nvarchar(50),

	@UserEmail		nvarchar(50),

	@UserPass		nvarchar(32),

	@IsHostAdmin	bit

) as 

begin

	declare @BoardID				int

	declare @TimeZone				int

	declare @ForumEmail				nvarchar(50)

	declare	@GroupIDAdmin			int

	declare	@GroupIDGuest			int

	declare @GroupIDMember			int

	declare	@AccessMaskIDAdmin		int

	declare @AccessMaskIDModerator	int

	declare @AccessMaskIDMember		int

	declare	@AccessMaskIDReadOnly	int

	declare @UserIDAdmin			int

	declare @UserIDGuest			int

	declare @RankIDAdmin			int

	declare @RankIDGuest			int

	declare @RankIDNewbie			int

	declare @RankIDMember			int

	declare @RankIDAdvanced			int

	declare	@CategoryID				int

	declare	@ForumID				int

	declare @UserFlags				int



	SET @TimeZone = (SELECT CAST(Value as int) FROM yaf_Registry WHERE LOWER(Name) = LOWER('TimeZone'))

	SET @ForumEmail = (SELECT Value FROM yaf_Registry WHERE LOWER(Name) = LOWER('ForumEmail'))



	-- yaf_Board

	insert into yaf_Board(Name,AllowThreaded) values(@BoardName,@AllowThreaded)

	set @BoardID = @@IDENTITY



	-- yaf_Rank

	insert into yaf_Rank(BoardID,Name,Flags,MinPosts) values(@BoardID,'Administration',0,null)

	set @RankIDAdmin = @@IDENTITY

	insert into yaf_Rank(BoardID,Name,Flags,MinPosts) values(@BoardID,'Guest',0,null)

	set @RankIDGuest = @@IDENTITY

	insert into yaf_Rank(BoardID,Name,Flags,MinPosts) values(@BoardID,'Newbie',3,0)

	set @RankIDNewbie = @@IDENTITY

	insert into yaf_Rank(BoardID,Name,Flags,MinPosts) values(@BoardID,'Member',2,10)

	set @RankIDMember = @@IDENTITY

	insert into yaf_Rank(BoardID,Name,Flags,MinPosts) values(@BoardID,'Advanced Member',2,30)

	set @RankIDAdvanced = @@IDENTITY



	-- yaf_AccessMask

	insert into yaf_AccessMask(BoardID,Name,Flags)

	values(@BoardID,'Admin Access Mask',1023)

	set @AccessMaskIDAdmin = @@IDENTITY

	insert into yaf_AccessMask(BoardID,Name,Flags)

	values(@BoardID,'Moderator Access Mask',487)

	set @AccessMaskIDModerator = @@IDENTITY

	insert into yaf_AccessMask(BoardID,Name,Flags)

	values(@BoardID,'Member Access Mask',423)

	set @AccessMaskIDMember = @@IDENTITY

	insert into yaf_AccessMask(BoardID,Name,Flags)

	values(@BoardID,'Read Only Access Mask',1)

	set @AccessMaskIDReadOnly = @@IDENTITY



	-- yaf_Group

	insert into yaf_Group(BoardID,Name,Flags) values(@BoardID,'Administration',1)

	set @GroupIDAdmin = @@IDENTITY

	insert into yaf_Group(BoardID,Name,Flags) values(@BoardID,'Guest',2)

	set @GroupIDGuest = @@IDENTITY

	insert into yaf_Group(BoardID,Name,Flags) values(@BoardID,'Member',4)

	set @GroupIDMember = @@IDENTITY	

	

	SET @UserFlags = 2



	-- orionsbelt_users

	insert into orionsbelt_users(user_boardid,user_RankID,user_nick,user_Pass,user_registdate,user_lastlogin,user_NumPosts,user_TimeZone,user_mail,user_gender,user_Flags)

	values(@BoardID,@RankIDGuest,'Guest','na',getdate(),getdate(),0,@TimeZone,@ForumEmail,0,@UserFlags)

	set @UserIDGuest = @@IDENTITY	

	

	if @IsHostAdmin<>0 SET @UserFlags = 3

	

	insert into orionsbelt_users(user_boardid,user_RankID,user_nick,user_Pass,user_registdate,user_lastlogin,user_NumPosts,user_TimeZone,user_mail,user_gender,user_Flags)

	values(@BoardID,@RankIDAdmin,@UserName,@UserPass,getdate(),getdate(),0,@TimeZone,@UserEmail,0,@UserFlags)

	set @UserIDAdmin = @@IDENTITY



	-- yaf_UserGroup

	insert into yaf_UserGroup(UserID,GroupID) values(@UserIDAdmin,@GroupIDAdmin)

	insert into yaf_UserGroup(UserID,GroupID) values(@UserIDGuest,@GroupIDGuest)



	-- yaf_Category

	insert into yaf_Category(BoardID,Name,SortOrder) values(@BoardID,'Test Category',1)

	set @CategoryID = @@IDENTITY

	

	-- yaf_Forum

	insert into yaf_Forum(CategoryID,Name,Description,SortOrder,NumTopics,NumPosts,Flags)

	values(@CategoryID,'Test Forum','A test forum',1,0,0,4)

	set @ForumID = @@IDENTITY



	-- yaf_ForumAccess

	insert into yaf_ForumAccess(GroupID,ForumID,AccessMaskID) values(@GroupIDAdmin,@ForumID,@AccessMaskIDAdmin)

	insert into yaf_ForumAccess(GroupID,ForumID,AccessMaskID) values(@GroupIDGuest,@ForumID,@AccessMaskIDReadOnly)

	insert into yaf_ForumAccess(GroupID,ForumID,AccessMaskID) values(@GroupIDMember,@ForumID,@AccessMaskIDMember)

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_board_delete(@BoardID int) as

begin

	declare @tmpForumID int;

	declare forum_cursor cursor for

		select ForumID 

		from yaf_Forum a join yaf_Category b on a.CategoryID=b.CategoryID

		where b.BoardID=@BoardID

		order by ForumID desc

	

	open forum_cursor

	fetch next from forum_cursor into @tmpForumID

	while @@FETCH_STATUS = 0

	begin

		exec yaf_forum_delete @tmpForumID;

		fetch next from forum_cursor into @tmpForumID

	end

	close forum_cursor

	deallocate forum_cursor



	delete from yaf_ForumAccess where exists(select 1 from yaf_Group x where x.GroupID=yaf_ForumAccess.GroupID and x.BoardID=@BoardID)

	delete from yaf_Forum where exists(select 1 from yaf_Category x where x.CategoryID=yaf_Forum.CategoryID and x.BoardID=@BoardID)

	delete from yaf_UserGroup where exists(select 1 from orionsbelt_users x where x.user_id=yaf_UserGroup.UserID and x.user_boardid=@BoardID)

	delete from yaf_Category where BoardID=@BoardID

	delete from orionsbelt_users where user_BoardID=@BoardID

	delete from yaf_Rank where BoardID=@BoardID

	delete from yaf_Group where BoardID=@BoardID

	delete from yaf_AccessMask where BoardID=@BoardID

	delete from yaf_Active where BoardID=@BoardID

	delete from yaf_Board where BoardID=@BoardID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_board_list(@BoardID int=null) as

begin

	select

		a.*,

		SQLVersion = @@VERSION

	from 

		yaf_Board a

	where

		(@BoardID is null or a.BoardID = @BoardID)

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_board_poststats(@BoardID int) as

begin

	select

		Posts = (select count(1) from yaf_Message a join yaf_Topic b on b.TopicID=a.TopicID join yaf_Forum c on c.ForumID=b.ForumID join yaf_Category d on d.CategoryID=c.CategoryID where d.BoardID=@BoardID),

		Topics = (select count(1) from yaf_Topic a join yaf_Forum b on b.ForumID=a.ForumID join yaf_Category c on c.CategoryID=b.CategoryID where c.BoardID=@BoardID),

		Forums = (select count(1) from yaf_Forum a join yaf_Category b on b.CategoryID=a.CategoryID where b.BoardID=@BoardID),

		Members = (select count(1) from orionsbelt_users a where a.user_BoardID=@BoardID),

		LastPostInfo.*,

		LastMemberInfo.*

	from

		(

			select top 1 

				LastMemberInfoID= 1,

				LastMemberID	= user_id,

				LastMember	= user_nick

			from 

				orionsbelt_users 

			where 

				Customer2163.yaf_bitset(user_Flags,2)=1 and 

				user_BoardID=@BoardID 

			order by 

				user_registdate desc

		) as LastMemberInfo

		left join (

			select top 1 

				LastPostInfoID	= 1,

				LastPost	= a.Posted,

				LastUserID	= a.UserID,

				LastUser	= e.user_nick

			from 

				yaf_Message a 

				join yaf_Topic b on b.TopicID=a.TopicID 

				join yaf_Forum c on c.ForumID=b.ForumID 

				join yaf_Category d on d.CategoryID=c.CategoryID 

				join orionsbelt_users e on e.user_id=a.UserID

			where 

				d.BoardID=@BoardID

			order by

				a.Posted desc

		) as LastPostInfo

		on LastMemberInfoID=LastPostInfoID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_board_save(@BoardID int,@Name nvarchar(50),@AllowThreaded bit) as

begin

	update yaf_Board set

		Name = @Name,

		AllowThreaded = @AllowThreaded

	where BoardID=@BoardID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_board_stats as begin

	select

		NumPosts	= (select count(1) from yaf_Message where (Flags & 24)=16),

		NumTopics	= (select count(1) from yaf_Topic),

		NumUsers	= (select count(1) from orionsbelt_users where Customer2163.yaf_bitset(user_Flags,2)<>0),

		BoardStart	= (select min(user_registDate) from orionsbelt_users)

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_category_delete(@CategoryID int) as

begin

	declare @flag int

 

	if exists(select 1 from yaf_Forum where CategoryID =  @CategoryID)

	begin

		set @flag = 0

	end else

	begin

		delete from yaf_Category where CategoryID = @CategoryID

		set @flag = 1

	end



	select @flag

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_category_list(@BoardID int,@CategoryID int=null) as

begin

	if @CategoryID is null

		select * from yaf_Category where BoardID = @BoardID order by SortOrder

	else

		select * from yaf_Category where BoardID = @BoardID and CategoryID = @CategoryID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_category_listread(@BoardID int,@UserID int,@CategoryID int=null) as

begin

	select 

		a.CategoryID,

		a.Name

	from 

		yaf_Category a

		join yaf_Forum b on b.CategoryID=a.CategoryID

		join yaf_vaccess v on v.ForumID=b.ForumID

	where

		a.BoardID=@BoardID and

		v.UserID=@UserID and

		(v.ReadAccess<>0 or (b.Flags & 2)=0) and

		(@CategoryID is null or a.CategoryID=@CategoryID) and

		b.ParentID is null

	group by

		a.CategoryID,

		a.Name,

		a.SortOrder

	order by 

		a.SortOrder

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_category_save(@BoardID int,@CategoryID int,@Name nvarchar(50),@SortOrder smallint) as

begin

	if @CategoryID>0 begin

		update yaf_Category set Name=@Name,SortOrder=@SortOrder where CategoryID=@CategoryID

		select CategoryID = @CategoryID

	end

	else begin

		insert into yaf_Category(BoardID,Name,SortOrder) values(@BoardID,@Name,@SortOrder)

		select CategoryID = @@IDENTITY

	end

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_checkemail_save(@UserID int,@Hash nvarchar(32),@Email nvarchar(50)) as

begin

	insert into yaf_CheckEmail(UserID,Email,Created,Hash)

	values(@UserID,@Email,getdate(),@Hash)	

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_checkemail_update(@Hash nvarchar(32)) as

begin

	declare @UserID int

	declare @CheckEmailID int

	declare @Email nvarchar(50)

	set @UserID = null

	select 

		@CheckEmailID = CheckEmailID,

		@UserID = UserID,

		@Email = Email

	from

		yaf_CheckEmail

	where

		Hash = @Hash

	if @UserID is null begin

		select convert(bit,0)

		return

	end

	-- Update new user email

	update orionsbelt_users set user_mail = @Email, user_Flags = user_Flags | 2 where user_id = @UserID

	delete yaf_CheckEmail where CheckEmailID = @CheckEmailID

	select convert(bit,1)

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_choice_vote(@ChoiceID int) as

begin

	update yaf_Choice set Votes = Votes + 1 where ChoiceID = @ChoiceID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_forum_delete(@ForumID int) as

begin

	-- Maybe an idea to use cascading foreign keys instead? Too bad they don't work on MS SQL 7.0...

	update yaf_Forum set LastMessageID=null,LastTopicID=null where ForumID=@ForumID

	update yaf_Topic set LastMessageID=null where ForumID=@ForumID

	delete from yaf_WatchTopic from yaf_Topic where yaf_Topic.ForumID = @ForumID and yaf_WatchTopic.TopicID = yaf_Topic.TopicID

	delete from yaf_Active from yaf_Topic where yaf_Topic.ForumID = @ForumID and yaf_Active.TopicID = yaf_Topic.TopicID

	delete from yaf_NntpTopic from yaf_NntpForum where yaf_NntpForum.ForumID = @ForumID and yaf_NntpTopic.NntpForumID = yaf_NntpForum.NntpForumID

	delete from yaf_NntpForum where ForumID=@ForumID	

	delete from yaf_WatchForum where ForumID = @ForumID



	-- BAI CHANGED 02.02.2004

	-- Delete topics, messages and attachments



	declare @tmpTopicID int;

	declare topic_cursor cursor for

		select TopicID from yaf_topic

		where ForumId = @ForumID

		order by TopicID desc

	

	open topic_cursor

	

	fetch next from topic_cursor

	into @tmpTopicID

	

	-- Check @@FETCH_STATUS to see if there are any more rows to fetch.

	while @@FETCH_STATUS = 0

	begin

		exec yaf_topic_delete @tmpTopicID;

	

	   -- This is executed as long as the previous fetch succeeds.

		fetch next from topic_cursor

		into @tmpTopicID

	end

	

	close topic_cursor

	deallocate topic_cursor



	--delete from yaf_Message from yaf_Topic where yaf_Topic.ForumID = @ForumID and yaf_Message.TopicID = yaf_Topic.TopicID

	--delete from yaf_Topic where ForumID = @ForumID



	-- TopicDelete finished

	-- END BAI CHANGED 02.02.2004



	delete from yaf_ForumAccess where ForumID = @ForumID

	--ABOT CHANGED

	--Delete UserForums Too 

	delete from yaf_UserForum where ForumID = @ForumID

	--END ABOT CHANGED 09.04.2004

	delete from yaf_Forum where ForumID = @ForumID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_forum_list(@BoardID int,@ForumID int=null) as

begin

	if @ForumID = 0 set @ForumID = null

	if @ForumID is null

		select a.* from yaf_Forum a join yaf_Category b on b.CategoryID=a.CategoryID where b.BoardID=@BoardID order by a.SortOrder

	else

		select a.* from yaf_Forum a join yaf_Category b on b.CategoryID=a.CategoryID where b.BoardID=@BoardID and a.ForumID = @ForumID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_forum_listSubForums(@ForumID int) as

begin

	select Sum(1) from yaf_Forum where ParentID = @ForumID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_forum_listall(@BoardID int,@UserID int) as

begin

	select

		b.CategoryID,

		Category = b.Name,

		a.ForumID,

		Forum = a.Name,

		x.Indent

	from

		(select

			b.ForumID,

			Indent = 0

		from

			yaf_Category a

			join yaf_Forum b on b.CategoryID=a.CategoryID

		where

			a.BoardID=@BoardID and

			b.ParentID is null

	

		union

	

		select

			c.ForumID,

			Indent = 1

		from

			yaf_Category a

			join yaf_Forum b on b.CategoryID=a.CategoryID

			join yaf_Forum c on c.ParentID=b.ForumID

		where

			a.BoardID=@BoardID and

			b.ParentID is null

	

		union

	

		select

			d.ForumID,

			Indent = 2

		from

			yaf_Category a

			join yaf_Forum b on b.CategoryID=a.CategoryID

			join yaf_Forum c on c.ParentID=b.ForumID

			join yaf_Forum d on d.ParentID=c.ForumID

		where

			a.BoardID=@BoardID and

			b.ParentID is null

		) as x

		join yaf_Forum a on a.ForumID=x.ForumID

		join yaf_Category b on b.CategoryID=a.CategoryID

		join yaf_vaccess c on c.ForumID=a.ForumID

	where

		c.UserID=@UserID and

		b.BoardID=@BoardID and

		c.ReadAccess>0

	order by

		b.SortOrder,

		a.SortOrder

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_forum_listallmymoderated(@BoardID int,@UserID int) as

begin

	select

		b.CategoryID,

		Category = b.Name,

		a.ForumID,

		Forum = a.Name,

		x.Indent

	from

		(select

			b.ForumID,

			Indent = 0

		from

			yaf_Category a

			join yaf_Forum b on b.CategoryID=a.CategoryID

		where

			a.BoardID=@BoardID and

			b.ParentID is null

	

		union

	

		select

			c.ForumID,

			Indent = 1

		from

			yaf_Category a

			join yaf_Forum b on b.CategoryID=a.CategoryID

			join yaf_Forum c on c.ParentID=b.ForumID

		where

			a.BoardID=@BoardID and

			b.ParentID is null

	

		union

	

		select

			d.ForumID,

			Indent = 2

		from

			yaf_Category a

			join yaf_Forum b on b.CategoryID=a.CategoryID

			join yaf_Forum c on c.ParentID=b.ForumID

			join yaf_Forum d on d.ParentID=c.ForumID

		where

			a.BoardID=@BoardID and

			b.ParentID is null

		) as x

		join yaf_Forum a on a.ForumID=x.ForumID

		join yaf_Category b on b.CategoryID=a.CategoryID

		join yaf_vaccess c on c.ForumID=a.ForumID

	where

		c.UserID=@UserID and

		b.BoardID=@BoardID and

		c.ModeratorAccess>0

	order by

		b.SortOrder,

		a.SortOrder

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_forum_listpath(@ForumID int) as

begin

	select

		a.ForumID,

		a.Name

	from

		(select

			a.ForumID,

			Indent = 0

		from

			yaf_Forum a

		where

			a.ForumID=@ForumID



		union



		select

			b.ForumID,

			Indent = 1

		from

			yaf_Forum a

			join yaf_Forum b on b.ForumID=a.ParentID

		where

			a.ForumID=@ForumID



		union



		select

			c.ForumID,

			Indent = 2

		from

			yaf_Forum a

			join yaf_Forum b on b.ForumID=a.ParentID

			join yaf_Forum c on c.ForumID=b.ParentID

		where

			a.ForumID=@ForumID

		) as x	

		join yaf_Forum a on a.ForumID=x.ForumID

	order by

		x.Indent desc

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO



CREATE procedure yaf_forum_listread(@BoardID int,@UserID int,@CategoryID int=null,@ParentID int=null) as

begin

select

a.CategoryID,

Category  = a.Name,

ForumID   = b.ForumID,

Forum   = b.Name,

Description,

Topics   = dbo.yaf_forum_topics(b.ForumID),

Posts   = dbo.yaf_forum_posts(b.ForumID),

LastPosted  = b.LastPosted,

LastMessageID = b.LastMessageID,

LastUserID  = b.LastUserID,

LastUser  = IsNull(b.LastUserName,(select user_nick from orionsbelt_users x where x.user_id=b.LastUserID)),

LastTopicID  = b.LastTopicID,

LastTopicName = (select x.Topic from yaf_Topic x where x.TopicID=b.LastTopicID),

b.Flags,

Viewing   = (select count(1) from yaf_Active x where x.ForumID=b.ForumID),

b.RemoteURL,

x.ReadAccess

from

yaf_Category a

join yaf_Forum b on b.CategoryID=a.CategoryID

join yaf_vaccess x on x.ForumID=b.ForumID

where

a.BoardID = @BoardID and

((b.Flags & 2)=0 or x.ReadAccess<>0) and

(@CategoryID is null or a.CategoryID=@CategoryID) and

((@ParentID is null and b.ParentID is null) or b.ParentID=@ParentID) and

x.UserID = @UserID

order by

a.SortOrder,

b.SortOrder

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_forum_listtopics(@ForumID int) as

begin

select * from yaf_Topic

Where ForumID = @ForumID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_forum_moderatelist as begin

	select

		CategoryID	= a.CategoryID,

		CategoryName	= a.Name,

		ForumID		= b.ForumID,

		ForumName	= b.Name,

		MessageCount	= count(d.MessageID)

	from

		yaf_Category a,

		yaf_Forum b,

		yaf_Topic c,

		yaf_Message d

	where

		b.CategoryID = a.CategoryID and

		c.ForumID = b.ForumID and

		d.TopicID = c.TopicID and

		(d.Flags & 16)=0

	group by

		a.CategoryID,

		a.Name,

		a.SortOrder,

		b.ForumID,

		b.Name,

		b.SortOrder

	order by

		a.SortOrder,

		b.SortOrder

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_forum_moderators as

begin

	select

		a.ForumID, 

		a.GroupID, 

		GroupName = b.Name

	from

		yaf_ForumAccess a,

		yaf_Group b,

		yaf_AccessMask c

	where

		(c.Flags & 64)<>0 and

		b.GroupID = a.GroupID and

		c.AccessMaskID = a.AccessMaskID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_forum_save(

	@ForumID 		int,

	@CategoryID		int,

	@ParentID		int=null,

	@Name			nvarchar(50),

	@Description	nvarchar(255),

	@SortOrder		smallint,

	@Locked			bit,

	@Hidden			bit,

	@IsTest			bit,

	@Moderated		bit,

	@RemoteURL		nvarchar(100)=null,

	@AccessMaskID	int = null

) as

begin

	declare @BoardID	int

	declare @Flags		int

	

	set @Flags = 0

	if @Locked<>0 set @Flags = @Flags | 1

	if @Hidden<>0 set @Flags = @Flags | 2

	if @IsTest<>0 set @Flags = @Flags | 4

	if @Moderated<>0 set @Flags = @Flags | 8



	if @ForumID>0 begin

		update yaf_Forum set 

			ParentID=@ParentID,

			Name=@Name,

			Description=@Description,

			SortOrder=@SortOrder,

			CategoryID=@CategoryID,

			RemoteURL = @RemoteURL,

			Flags = @Flags

		where ForumID=@ForumID

	end

	else begin

		select @BoardID=BoardID from yaf_Category where CategoryID=@CategoryID

	

		insert into yaf_Forum(ParentID,Name,Description,SortOrder,CategoryID,NumTopics,NumPosts,RemoteURL,Flags)

		values(@ParentID,@Name,@Description,@SortOrder,@CategoryID,0,0,@RemoteURL,@Flags)

		select @ForumID = @@IDENTITY



		insert into yaf_ForumAccess(GroupID,ForumID,AccessMaskID) 

		select GroupID,@ForumID,@AccessMaskID

		from yaf_Group 

		where BoardID=@BoardID

	end

	select ForumID = @ForumID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_forum_updatelastpost(@ForumID int) as

begin

	update yaf_Forum set

		LastPosted = (select top 1 y.Posted from yaf_Topic x,yaf_Message y where x.ForumID=yaf_Forum.ForumID and y.TopicID=x.TopicID and (y.Flags & 24)=16 order by y.Posted desc),

		LastTopicID = (select top 1 y.TopicID from yaf_Topic x,yaf_Message y where x.ForumID=yaf_Forum.ForumID and y.TopicID=x.TopicID and (y.Flags & 24)=16 order by y.Posted desc),

		LastMessageID = (select top 1 y.MessageID from yaf_Topic x,yaf_Message y where x.ForumID=yaf_Forum.ForumID and y.TopicID=x.TopicID and (y.Flags & 24)=16 order by y.Posted desc),

		LastUserID = (select top 1 y.UserID from yaf_Topic x,yaf_Message y where x.ForumID=yaf_Forum.ForumID and y.TopicID=x.TopicID and (y.Flags & 24)=16 order by y.Posted desc),

		LastUserName = (select top 1 y.UserName from yaf_Topic x,yaf_Message y where x.ForumID=yaf_Forum.ForumID and y.TopicID=x.TopicID and (y.Flags & 24)=16 order by y.Posted desc)

	where ForumID = @ForumID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_forum_updatestats(@ForumID int) as

begin

	update yaf_Forum set 

		NumPosts = (select count(1) from yaf_Message x,yaf_Topic y where y.TopicID=x.TopicID and y.ForumID = yaf_Forum.ForumID and (x.Flags & 24)=16),

		NumTopics = (select count(distinct x.TopicID) from yaf_Topic x,yaf_Message y where x.ForumID=yaf_Forum.ForumID and y.TopicID=x.TopicID and (y.Flags & 24)=16)

	where ForumID=@ForumID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_forumaccess_group(@GroupID int) as

begin

	select 

		a.*,

		ForumName = b.Name,

		CategoryName = c.Name 

	from 

		yaf_ForumAccess a, 

		yaf_Forum b, 

		yaf_Category c 

	where 

		a.GroupID = @GroupID and 

		b.ForumID=a.ForumID and 

		c.CategoryID=b.CategoryID 

	order by 

		c.SortOrder,

		b.SortOrder

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_forumaccess_list(@ForumID int) as

begin

	select 

		a.*,

		GroupName=b.Name 

	from 

		yaf_ForumAccess a, 

		yaf_Group b 

	where 

		a.ForumID = @ForumID and 

		b.GroupID = a.GroupID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_forumaccess_save(

	@ForumID			int,

	@GroupID			int,

	@AccessMaskID		int

) as

begin

	update yaf_ForumAccess set 

		AccessMaskID=@AccessMaskID

	where 

		ForumID = @ForumID and 

		GroupID = @GroupID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_group_delete(@GroupID int) as

begin

	delete from yaf_ForumAccess where GroupID = @GroupID

	delete from yaf_UserGroup where GroupID = @GroupID

	delete from yaf_Group where GroupID = @GroupID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_group_list(@BoardID int,@GroupID int=null) as

begin

	if @GroupID is null

		select * from yaf_Group where BoardID=@BoardID

	else

		select * from yaf_Group where BoardID=@BoardID and GroupID=@GroupID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_group_member(@BoardID int,@UserID int) as

begin

	select 

		a.GroupID,

		a.Name,

		Member = (select count(1) from yaf_UserGroup x where x.UserID=@UserID and x.GroupID=a.GroupID)

	from

		yaf_Group a

	where

		a.BoardID=@BoardID

	order by

		a.Name

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_group_save(

	@GroupID		int,

	@BoardID		int,

	@Name			nvarchar(50),

	@IsAdmin		bit,

	@IsGuest		bit,

	@IsStart		bit,

	@IsModerator	bit,

	@AccessMaskID	int=null

) as

begin

	declare @Flags	int

	

	set @Flags = 0

	if @IsAdmin<>0 set @Flags = @Flags | 1

	if @IsGuest<>0 set @Flags = @Flags | 2

	if @IsStart<>0 set @Flags = @Flags | 4

	if @IsModerator<>0 set @Flags = @Flags | 8



	if @GroupID>0 begin

		update yaf_Group set

			Name = @Name,

			Flags = @Flags

		where GroupID = @GroupID

	end

	else begin

		insert into yaf_Group(Name,BoardID,Flags)

		values(@Name,@BoardID,@Flags);

		set @GroupID = @@IDENTITY

		insert into yaf_ForumAccess(GroupID,ForumID,AccessMaskID)

		select @GroupID,a.ForumID,@AccessMaskID from yaf_Forum a join yaf_Category b on b.CategoryID=a.CategoryID where b.BoardID=@BoardID

	end

	select GroupID = @GroupID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_mail_createwatch(@TopicID int,@From nvarchar(50),@Subject nvarchar(100),@Body ntext,@UserID int) as begin

	insert into yaf_Mail(FromUser,ToUser,Created,Subject,Body)

	select

		@From,

		b.user_mail,

		getdate(),

		@Subject,

		@Body

	from

		yaf_WatchTopic a,

		orionsbelt_users b

	where

		b.user_id <> @UserID and

		b.user_id = a.UserID and

		a.TopicID = @TopicID and

		(a.LastMail is null or a.LastMail < b.user_lastlogin)

	

	insert into yaf_Mail(FromUser,ToUser,Created,Subject,Body)

	select

		@From,

		b.user_mail,

		getdate(),

		@Subject,

		@Body

	from

		yaf_WatchForum a,

		orionsbelt_users b,

		yaf_Topic c

	where

		b.user_id <> @UserID and

		b.user_id = a.UserID and

		c.TopicID = @TopicID and

		c.ForumID = a.ForumID and

		(a.LastMail is null or a.LastMail < b.user_lastlogin) and

		not exists(select 1 from yaf_WatchTopic x where x.UserID=b.user_id and x.TopicID=c.TopicID)



	update yaf_WatchTopic set LastMail = getdate() 

	where TopicID = @TopicID

	and UserID <> @UserID

	

	update yaf_WatchForum set LastMail = getdate() 

	where ForumID = (select ForumID from yaf_Topic where TopicID = @TopicID)

	and UserID <> @UserID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_mail_delete(@MailID int) as

begin

	delete from yaf_Mail where MailID = @MailID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_mail_list as

begin

	select top 10 * from yaf_Mail order by Created

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_message_approve(@MessageID int) as begin

	declare	@UserID		int

	declare	@ForumID	int

	declare	@TopicID	int

	declare @Posted		datetime

	declare	@UserName	nvarchar(50)



	select 

		@UserID = a.UserID,

		@TopicID = a.TopicID,

		@ForumID = b.ForumID,

		@Posted = a.Posted,

		@UserName = a.UserName

	from

		yaf_Message a,

		yaf_Topic b

	where

		a.MessageID = @MessageID and

		b.TopicID = a.TopicID



	-- update yaf_Message

	update yaf_Message set Flags = Flags | 16 where MessageID = @MessageID



	-- update orionsbelt_users

	if exists(select 1 from yaf_Forum where ForumID=@ForumID and (Flags & 4)=0)

	begin

		update orionsbelt_users set user_NumPosts = user_NumPosts + 1 where user_id = @UserID

		exec yaf_user_upgrade @UserID

	end



	-- update yaf_Forum

	update yaf_Forum set

		LastPosted = @Posted,

		LastTopicID = @TopicID,

		LastMessageID = @MessageID,

		LastUserID = @UserID,

		LastUserName = @UserName

	where ForumID = @ForumID



	-- update yaf_Topic

	update yaf_Topic set

		LastPosted = @Posted,

		LastMessageID = @MessageID,

		LastUserID = @UserID,

		LastUserName = @UserName,

		NumPosts = (select count(1) from yaf_Message x where x.TopicID=yaf_Topic.TopicID and (x.Flags & 24)=16)

	where TopicID = @TopicID

	

	-- update forum stats

	exec yaf_forum_updatestats @ForumID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_message_delete(@MessageID int) as

begin

	declare @TopicID		int

	declare @ForumID		int

	declare @MessageCount	int

	declare @LastMessageID	int



	-- Find TopicID and ForumID

	select @TopicID=b.TopicID,@ForumID=b.ForumID from yaf_Message a,yaf_Topic b where a.MessageID=@MessageID and b.TopicID=a.TopicID



	-- Update LastMessageID in Topic and Forum

	update yaf_Topic set 

		LastPosted = null,

		LastMessageID = null,

		LastUserID = null,

		LastUserName = null

	where LastMessageID = @MessageID



	update yaf_Forum set 

		LastPosted = null,

		LastTopicID = null,

		LastMessageID = null,

		LastUserID = null,

		LastUserName = null

	where LastMessageID = @MessageID



	-- "Delete" message

	update yaf_Message set Flags = Flags | 8 where MessageID = @MessageID

	

	-- Delete topic if there are no more messages

	select @MessageCount = count(1) from yaf_Message where TopicID = @TopicID and (Flags & 8)=0

	if @MessageCount=0 exec yaf_topic_delete @TopicID

	-- update lastpost

	exec yaf_topic_updatelastpost @ForumID,@TopicID

	exec yaf_forum_updatestats @ForumID

	-- update topic numposts

	update yaf_Topic set

		NumPosts = (select count(1) from yaf_Message x where x.TopicID=yaf_Topic.TopicID and (x.Flags & 24)=16)

	where TopicID = @TopicID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_message_findunread(@TopicID int,@LastRead datetime) as

begin

	select top 1 MessageID from yaf_Message

	where TopicID=@TopicID and Posted>@LastRead

	order by Posted

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_message_getReplies(@MessageID int) as

begin

	select MessageID from yaf_Message where ReplyTo = @MessageID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_message_list(@MessageID int) as

begin

	select

		a.MessageID,

		a.UserID,

		UserName = b.user_nick,

		a.Message,

		c.TopicID,

		c.ForumID,

		c.Topic,

		c.Priority,

		a.Flags,

		c.UserID as TopicOwnerID

	from

		yaf_Message a,

		orionsbelt_users b,

		yaf_Topic c

	where

		a.MessageID = @MessageID and

		b.user_id = a.UserID and

		c.TopicID = a.TopicID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







CREATE procedure dbo.yaf_message_save(

	@TopicID	int,

	@UserID	int,

	@Message	ntext,

	@UserName	nvarchar(50)=null,

	@IP		nvarchar(15),

	@Posted	datetime=null,

	@ReplyTo	int,

	@Flags		int,

	@MessageID	int output

) as

begin

	declare @ForumID	int

	declare	@ForumFlags	int

	declare @Position	int

	declare	@Indent	int



	if @Posted is null set @Posted = getdate()



	select @ForumID = x.ForumID, @ForumFlags = y.Flags from yaf_Topic x,yaf_Forum y where x.TopicID = @TopicID and y.ForumID=x.ForumID



	if @ReplyTo is null

	begin

		-- New thread

		set @Position = 0

		set @Indent = 0

	end else if @ReplyTo<0

	begin

		-- Find post to reply to and indent of this post

		select top 1 @ReplyTo=MessageID,@Indent=Indent+1

		from yaf_Message 

		where TopicID=@TopicID and ReplyTo is null

		order by Posted

	end else

	begin

		-- Got reply, find indent of this post

		select @Indent=Indent+1

		from yaf_Message 

		where MessageID=@ReplyTo

	end



	-- Find position

	if @ReplyTo is not null

	begin

		declare @temp int

		

		select @temp=ReplyTo,@Position=Position from yaf_Message where MessageID=@ReplyTo

		if @temp is null

			-- We are replying to first post

			select @Position=max(Position)+1 from yaf_Message where TopicID=@TopicID

		else

			-- Last position of replies to parent post

			select @Position=min(Position) from yaf_Message where ReplyTo=@temp and Position>@Position

		-- No replies, then use parent post's position+1

		if @Position is null select @Position=Position+1 from yaf_Message where MessageID=@ReplyTo

		-- Increase position of posts after this

		update yaf_Message set Position=Position+1 where TopicID=@TopicID and Position>=@Position

	end



	insert into yaf_Message(UserID,Message,TopicID,Posted,UserName,IP,ReplyTo,Position,Indent,Flags)

	values(@UserID,@Message,@TopicID,@Posted,@UserName,@IP,@ReplyTo,@Position,@Indent,@Flags & ~16)

	set @MessageID = @@IDENTITY

	

	if (@ForumFlags & 8)=0

		exec yaf_message_approve @MessageID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_message_unapproved(@ForumID int) as begin

	select

		MessageID	= b.MessageID,

		UserName	= IsNull(b.UserName,c.user_nick),

		Posted		= b.Posted,

		Topic		= a.Topic,

		Message		= b.Message

	from

		yaf_Topic a,

		yaf_Message b,

		orionsbelt_users c

	where

		a.ForumID = @ForumID and

		b.TopicID = a.TopicID and

		(b.Flags & 16)=0 and

		c.user_id = b.UserID

	order by

		a.Posted

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







CREATE procedure dbo.yaf_message_update(@MessageID int,@Priority int,@Subject nvarchar(100),@Flags int, @Message ntext) as

begin

	declare @TopicID	int

	declare	@ForumFlags	int



	set @Flags = @Flags & ~16	

	

	select 

		@TopicID	= a.TopicID,

		@ForumFlags	= c.Flags

	from 

		yaf_Message a,

		yaf_Topic b,

		yaf_Forum c

	where 

		a.MessageID = @MessageID and

		b.TopicID = a.TopicID and

		c.ForumID = b.ForumID



	if (@ForumFlags & 8)=0 set @Flags = @Flags | 16



	update yaf_Message set

		Message = @Message,

		Edited = getdate(),

		Flags = @Flags

	where

		MessageID = @MessageID



	if @Priority is not null begin

		update yaf_Topic set

			Priority = @Priority

		where

			TopicID = @TopicID

	end



	if not @Subject = '' and @Subject is not null begin

		update yaf_Topic set

			Topic = @Subject

		where

			TopicID = @TopicID

	end 

	

	-- If forum is moderated, make sure last post pointers are correct

	if (@ForumFlags & 8)<>0 exec yaf_topic_updatelastpost

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_nntpforum_delete(@NntpForumID int) as

begin

	delete from yaf_NntpTopic where NntpForumID = @NntpForumID

	delete from yaf_NntpForum where NntpForumID = @NntpForumID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_nntpforum_list(@BoardID int,@Minutes int=null,@NntpForumID int=null,@Active bit=null) as

begin

	select

		a.Name,

		a.Address,

		Port = IsNull(a.Port,119),

		a.NntpServerID,

		b.NntpForumID,

		b.GroupName,

		b.ForumID,

		b.LastMessageNo,

		b.LastUpdate,

		b.Active,

		ForumName = c.Name

	from

		yaf_NntpServer a

		join yaf_NntpForum b on b.NntpServerID = a.NntpServerID

		join yaf_Forum c on c.ForumID = b.ForumID

	where

		(@Minutes is null or datediff(n,b.LastUpdate,getdate())>@Minutes) and

		(@NntpForumID is null or b.NntpForumID=@NntpForumID) and

		a.BoardID=@BoardID and

		(@Active is null or b.Active=@Active)

	order by

		a.Name,

		b.GroupName

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_nntpforum_save(@NntpForumID int=null,@NntpServerID int,@GroupName nvarchar(100),@ForumID int,@Active bit) as

begin

	if @NntpForumID is null

		insert into yaf_NntpForum(NntpServerID,GroupName,ForumID,LastMessageNo,LastUpdate,Active)

		values(@NntpServerID,@GroupName,@ForumID,0,getdate(),@Active)

	else

		update yaf_NntpForum set

			NntpServerID = @NntpServerID,

			GroupName = @GroupName,

			ForumID = @ForumID,

			Active = @Active

		where NntpForumID = @NntpForumID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_nntpforum_update(@NntpForumID int,@LastMessageNo int,@UserID int) as

begin

	declare	@ForumID	int

	

	select @ForumID=ForumID from yaf_NntpForum where NntpForumID=@NntpForumID



	update yaf_NntpForum set

		LastMessageNo = @LastMessageNo,

		LastUpdate = getdate()

	where NntpForumID = @NntpForumID



	update yaf_Topic set 

		NumPosts = (select count(1) from yaf_message x where x.TopicID=yaf_Topic.TopicID and (x.Flags & 24)=16)

	where ForumID=@ForumID



	--exec yaf_user_upgrade @UserID

	exec yaf_forum_updatestats @ForumID

	-- exec yaf_topic_updatelastpost @ForumID,null

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_nntpserver_delete(@NntpServerID int) as

begin

	delete from yaf_NntpTopic where NntpForumID in (select NntpForumID from yaf_NntpForum where NntpServerID = @NntpServerID)

	delete from yaf_NntpForum where NntpServerID = @NntpServerID

	delete from yaf_NntpServer where NntpServerID = @NntpServerID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_nntpserver_list(@BoardID int=null,@NntpServerID int=null) as

begin

	if @NntpServerID is null

		select * from yaf_NntpServer where BoardID=@BoardID order by Name

	else

		select * from yaf_NntpServer where NntpServerID=@NntpServerID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_nntpserver_save(

	@NntpServerID 	int=null,

	@BoardID	int,

	@Name		nvarchar(50),

	@Address	nvarchar(100),

	@Port		int,

	@UserName	nvarchar(50)=null,

	@UserPass	nvarchar(50)=null

) as begin

	if @NntpServerID is null

		insert into yaf_NntpServer(Name,BoardID,Address,Port,UserName,UserPass)

		values(@Name,@BoardID,@Address,@Port,@UserName,@UserPass)

	else

		update yaf_NntpServer set

			Name = @Name,

			Address = @Address,

			Port = @Port,

			UserName = @UserName,

			UserPass = @UserPass

		where NntpServerID = @NntpServerID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_nntptopic_list(@Thread char(32)) as

begin

	select

		a.*

	from

		yaf_NntpTopic a

	where

		a.Thread = @Thread

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_nntptopic_savemessage(

	@NntpForumID	int,

	@Topic 			nvarchar(100),

	@Body 			ntext,

	@UserID 		int,

	@UserName		nvarchar(50),

	@IP				nvarchar(15),

	@Posted			datetime,

	@Thread			char(32)

) as 

begin

	declare	@ForumID	int

	declare @TopicID	int

	declare	@MessageID	int



	select @ForumID=ForumID from yaf_NntpForum where NntpForumID=@NntpForumID



	if exists(select 1 from yaf_NntpTopic where Thread=@Thread)

	begin

		-- thread exists

		select @TopicID=TopicID from yaf_NntpTopic where Thread=@Thread

	end else

	begin

		-- thread doesn't exists

		insert into yaf_Topic(ForumID,UserID,UserName,Posted,Topic,Views,Priority,NumPosts)

		values(@ForumID,@UserID,@UserName,@Posted,@Topic,0,0,0)

		set @TopicID=@@IDENTITY



		insert into yaf_NntpTopic(NntpForumID,Thread,TopicID)

		values(@NntpForumID,@Thread,@TopicID)

	end



	-- save message

	insert into yaf_Message(TopicID,UserID,UserName,Posted,Message,IP,Position,Indent)

	values(@TopicID,@UserID,@UserName,@Posted,@Body,@IP,0,0)

	set @MessageID=@@IDENTITY



	-- update user

	if exists(select 1 from yaf_Forum where ForumID=@ForumID and (Flags & 4)=0)

	begin

		update orionsbelt_users set user_NumPosts=user_NumPosts+1 where user_id=@UserID

	end

	

	-- update topic

	update yaf_Topic set 

		LastPosted		= @Posted,

		LastMessageID	= @MessageID,

		LastUserID		= @UserID,

		LastUserName	= @UserName

	where TopicID=@TopicID	

	-- update forum

	update yaf_Forum set

		LastPosted		= @Posted,

		LastTopicID	= @TopicID,

		LastMessageID	= @MessageID,

		LastUserID		= @UserID,

		LastUserName	= @UserName

	where ForumID=@ForumID and (LastPosted is null or LastPosted<@Posted)

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_pageload(

	@SessionID	nvarchar(24),

	@BoardID	int,

	@User		nvarchar(50),

	@IP			nvarchar(15),

	@Location	nvarchar(50),

	@Browser	nvarchar(50),

	@Platform	nvarchar(50),

	@CategoryID	int = null,

	@ForumID	int = null,

	@TopicID	int = null,

	@MessageID	int = null

) as

begin

	declare @UserID			int

	declare @UserBoardID	int

	declare @IsGuest		tinyint

	declare @rowcount		int

	

	set implicit_transactions off



	if @User is null or @User='' 

	begin

		select @UserID = a.user_id from orionsbelt_users a,yaf_UserGroup b,yaf_Group c where a.user_id=b.UserID and a.user_BoardID=@BoardID and b.GroupID=c.GroupID and (c.Flags & 2)<>0

		set @rowcount=@@rowcount

		if @rowcount<>1

		begin

			raiserror('Found %d possible guest users. Only 1 guest user should be a member of the group marked as the guest group.',16,1,@rowcount)

		end

		set @IsGuest = 1

		set @UserBoardID = @BoardID

	end else

	begin

		select @UserID = user_id, @UserBoardID = user_BoardID from orionsbelt_users where user_BoardID=@BoardID and user_mail=@User

		set @IsGuest = 0

	end

	-- Check valid ForumID

	if @ForumID is not null and not exists(select 1 from yaf_Forum where ForumID=@ForumID) begin

		set @ForumID = null

	end

	-- Check valid CategoryID

	if @CategoryID is not null and not exists(select 1 from yaf_Category where CategoryID=@CategoryID) begin

		set @CategoryID = null

	end

	-- Check valid MessageID

	if @MessageID is not null and not exists(select 1 from yaf_Message where MessageID=@MessageID) begin

		set @MessageID = null

	end

	-- Check valid TopicID

	if @TopicID is not null and not exists(select 1 from yaf_Topic where TopicID=@TopicID) begin

		set @TopicID = null

	end

	

	-- update last visit

	update orionsbelt_users set 

		user_lastlogin = getdate(),

		user_IP = @IP

	where user_id = @UserID



	-- find missing ForumID/TopicID

	if @MessageID is not null begin

		select

			@CategoryID = c.CategoryID,

			@ForumID = b.ForumID,

			@TopicID = b.TopicID

		from

			yaf_Message a,

			yaf_Topic b,

			yaf_Forum c,

			yaf_Category d

		where

			a.MessageID = @MessageID and

			b.TopicID = a.TopicID and

			c.ForumID = b.ForumID and

			d.CategoryID = c.CategoryID and

			d.BoardID = @BoardID

	end

	else if @TopicID is not null begin

		select 

			@CategoryID = b.CategoryID,

			@ForumID = a.ForumID 

		from 

			yaf_Topic a,

			yaf_Forum b,

			yaf_Category c

		where 

			a.TopicID = @TopicID and

			b.ForumID = a.ForumID and

			c.CategoryID = b.CategoryID and

			c.BoardID = @BoardID

	end

	else if @ForumID is not null begin

		select

			@CategoryID = a.CategoryID

		from

			yaf_Forum a,

			yaf_Category b

		where

			a.ForumID = @ForumID and

			b.CategoryID = a.CategoryID and

			b.BoardID = @BoardID

	end

	-- update active

	if @UserID is not null and @UserBoardID=@BoardID begin

		if exists(select 1 from yaf_Active where SessionID=@SessionID and BoardID=@BoardID)

		begin

			update yaf_Active set

				UserID = @UserID,

				IP = @IP,

				LastActive = getdate(),

				Location = @Location,

				ForumID = @ForumID,

				TopicID = @TopicID,

				Browser = @Browser,

				Platform = @Platform

			where SessionID = @SessionID

		end

		else begin

			insert into yaf_Active(SessionID,BoardID,UserID,IP,Login,LastActive,Location,ForumID,TopicID,Browser,Platform)

			values(@SessionID,@BoardID,@UserID,@IP,getdate(),getdate(),@Location,@ForumID,@TopicID,@Browser,@Platform)

		end

		-- remove duplicate users

		if @IsGuest=0

			delete from yaf_Active where UserID=@UserID and BoardID=@BoardID and SessionID<>@SessionID

	end

	-- return information

	select

		a.user_id,

		UserFlags			= a.user_Flags,

		UserName			= a.user_nick,

		LanguageFile		= a.user_lang,

		TimeZoneUser		= a.user_TimeZone,

		x.*,

		CategoryID			= @CategoryID,

		CategoryName		= (select Name from yaf_Category where CategoryID = @CategoryID),

		ForumID				= @ForumID,

		ForumName			= (select Name from yaf_Forum where ForumID = @ForumID),

		TopicID				= @TopicID,

		TopicName			= (select Topic from yaf_Topic where TopicID = @TopicID),

		MailsPending		= (select count(1) from yaf_Mail),

		Incoming			= (select count(1) from yaf_UserPMessage where UserID=a.user_id and IsRead=0)

	from

		orionsbelt_users a,

		yaf_vaccess x

	where

		a.user_id = @UserID and

		x.UserID = a.user_id and

		x.ForumID = IsNull(@ForumID,0)

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_pmessage_delete(@PMessageID int) as

begin

	delete from yaf_PMessage where PMessageID=@PMessageID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_pmessage_info as

begin

	select

		NumRead	= (select count(1) from yaf_UserPMessage where IsRead<>0),

		NumUnread = (select count(1) from yaf_UserPMessage where IsRead=0),

		NumTotal = (select count(1) from yaf_UserPMessage)

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_pmessage_list(@FromUserID int=null,@ToUserID int=null,@PMessageID int=null) as

begin

	if @PMessageID is null begin

		select

			a.*,

			FromUser = b.user_nick,

			ToUserID = c.user_id,

			ToUser = c.user_nick,

			d.IsRead,

			d.UserPMessageID

		from

			yaf_PMessage a,

			orionsbelt_users b,

			orionsbelt_users c,

			yaf_UserPMessage d

		where

			b.user_id = a.FromUserID and

			c.user_id = d.UserID and

			d.PMessageID = a.PMessageID and

			((@ToUserID is not null and d.UserID = @ToUserID) or (@FromUserID is not null and a.FromUserID = @FromUserID))

		order by

			Created desc

	end

	else begin

		select

			a.*,

			FromUser = b.user_nick,

			ToUserID = c.user_id,

			ToUser = c.user_nick,

			d.IsRead,

			d.UserPMessageID

		from

			yaf_PMessage a,

			orionsbelt_users b,

			orionsbelt_users c,

			yaf_UserPMessage d

		where

			b.user_id = a.FromUserID and

			c.user_id = d.UserID and

			d.PMessageID = a.PMessageID and

			a.PMessageID = @PMessageID and

			c.user_id = @FromUserID

		order by

			Created desc

	end

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_pmessage_markread(@UserPMessageID int=null) as begin

	update yaf_UserPMessage set IsRead=1 where UserPMessageID=@UserPMessageID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_pmessage_prune(@DaysRead int,@DaysUnread int) as

begin

	delete from yaf_UserPMessage

	where IsRead<>0

	and datediff(dd,(select Created from yaf_PMessage x where x.PMessageID=yaf_UserPMessage.PMessageID),getdate())>@DaysRead



	delete from yaf_UserPMessage

	where IsRead=0

	and datediff(dd,(select Created from yaf_PMessage x where x.PMessageID=yaf_UserPMessage.PMessageID),getdate())>@DaysUnread



	delete from yaf_PMessage

	where not exists(select 1 from yaf_UserPMessage x where x.PMessageID=yaf_PMessage.PMessageID)

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_pmessage_save(

	@FromUserID	int,

	@ToUserID	int,

	@Subject	nvarchar(100),

	@Body		ntext,

	@Flags		int

) as

begin

	declare @PMessageID int

	declare @UserID int



	insert into yaf_PMessage(FromUserID,Created,Subject,Body,Flags)

	values(@FromUserID,getdate(),@Subject,@Body,@Flags)



	set @PMessageID = @@IDENTITY

	if (@ToUserID = 0)

	begin

		insert into yaf_UserPMessage(UserID,PMessageID,IsRead)

		select

				a.user_id,@PMessageID,0

		from

				orionsbelt_users a

				join yaf_UserGroup b on b.UserID=a.user_id

				join yaf_Group c on c.GroupID=b.GroupID where

				(c.Flags & 2)=0 and

				c.BoardID=(select BoardID from orionsbelt_users x where x.User_ID=@FromUserID) and a.user_id<>@FromUserID

		group by

				a.user_id

	end

	else

	begin

		insert into yaf_UserPMessage(UserID,PMessageID,IsRead) values(@ToUserID,@PMessageID,0)

	end

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_poll_save(

	@Question	nvarchar(50),

	@Choice1	nvarchar(50),

	@Choice2	nvarchar(50),

	@Choice3	nvarchar(50) = null,

	@Choice4	nvarchar(50) = null,

	@Choice5	nvarchar(50) = null,

	@Choice6	nvarchar(50) = null,

	@Choice7	nvarchar(50) = null,

	@Choice8	nvarchar(50) = null,

	@Choice9	nvarchar(50) = null

) as

begin

	declare @PollID	int

	insert into yaf_Poll(Question) values(@Question)

	set @PollID = @@IDENTITY

	if @Choice1<>'' and @Choice1 is not null

		insert into yaf_Choice(PollID,Choice,Votes)

		values(@PollID,@Choice1,0)

	if @Choice2<>'' and @Choice2 is not null

		insert into yaf_Choice(PollID,Choice,Votes)

		values(@PollID,@Choice2,0)

	if @Choice3<>'' and @Choice3 is not null

		insert into yaf_Choice(PollID,Choice,Votes)

		values(@PollID,@Choice3,0)

	if @Choice4<>'' and @Choice4 is not null

		insert into yaf_Choice(PollID,Choice,Votes)

		values(@PollID,@Choice4,0)

	if @Choice5<>'' and @Choice5 is not null

		insert into yaf_Choice(PollID,Choice,Votes)

		values(@PollID,@Choice5,0)

	if @Choice6<>'' and @Choice6 is not null

		insert into yaf_Choice(PollID,Choice,Votes)

		values(@PollID,@Choice6,0)

	if @Choice7<>'' and @Choice7 is not null

		insert into yaf_Choice(PollID,Choice,Votes)

		values(@PollID,@Choice7,0)

	if @Choice8<>'' and @Choice8 is not null

		insert into yaf_Choice(PollID,Choice,Votes)

		values(@PollID,@Choice8,0)

	if @Choice9<>'' and @Choice9 is not null

		insert into yaf_Choice(PollID,Choice,Votes)

		values(@PollID,@Choice9,0)

	select PollID = @PollID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_poll_stats(@PollID int) as

begin

	select

		a.PollID,

		b.Question,

		a.ChoiceID,

		a.Choice,

		a.Votes,

		Stats = (select 100 * a.Votes / case sum(x.Votes) when 0 then 1 else sum(x.Votes) end from yaf_Choice x where x.PollID=a.PollID)

	from

		yaf_Choice a,

		yaf_Poll b

	where

		b.PollID = a.PollID and

		b.PollID = @PollID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_post_last10user(@BoardID int,@UserID int,@PageUserID int) as

begin

	set nocount on



	select top 10

		a.Posted,

		Subject = c.Topic,

		a.Message,

		a.UserID,

		a.Flags,

		UserName = IsNull(a.UserName,b.user_nick),

		b.user_signature,

		c.TopicID

	from

		yaf_Message a

		join orionsbelt_users b on b.user_id=a.UserID

		join yaf_Topic c on c.TopicID=a.TopicID

		join yaf_Forum d on d.ForumID=c.ForumID

		join yaf_Category e on e.CategoryID=d.CategoryID

		join yaf_vaccess x on x.ForumID=d.ForumID

	where

		a.UserID = @UserID and

		x.UserID = @PageUserID and

		x.ReadAccess <> 0 and

		e.BoardID = @BoardID and

		(a.Flags & 24)=16 and

		(c.Flags & 8)=0

	order by

		a.Posted desc

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO







CREATE procedure dbo.yaf_post_list(@TopicID int,@UpdateViewCount smallint=1) as

begin

	set nocount on



	if @UpdateViewCount>0

		update yaf_Topic set Views = Views + 1 where TopicID = @TopicID



	select

		d.TopicID,

		TopicFlags	= d.Flags,

		ForumFlags	= g.Flags,

		a.MessageID,

		a.Posted,

		Subject = d.Topic,

		a.Message,

		a.UserID,

		a.Position,

		a.Indent,

		a.IP,

		a.Flags,

		UserName	= IsNull(a.UserName,b.user_nick),

		b.user_registdate,

		b.user_avatar,

		b.user_location as Location,

		b.user_Signature as Signature,

		b.user_website as Homepage,

		b.user_MSN as MSN,

		b.user_jabber as Jabber,

		b.user_aim as AIM,

		b.user_icq as ICQ,

		Posts		= b.user_NumPosts,

		d.Views,

		d.ForumID,

		RankName = c.Name,

		c.RankImage,

		Edited = IsNull(a.Edited,a.Posted),

		HasAttachments	= (select count(1) from yaf_Attachment x where x.MessageID=a.MessageID),

		HasAvatarImage = (select count(1) from orionsbelt_users x where x.user_id=b.User_ID and user_Avatar<>'' )

	from

		yaf_Message a

		join orionsbelt_users b on b.user_id=a.UserID

		join yaf_Topic d on d.TopicID=a.TopicID

		join yaf_Forum g on g.ForumID=d.ForumID

		join yaf_Category h on h.CategoryID=g.CategoryID

		join yaf_Rank c on c.RankID=b.user_RankID

	where

		a.TopicID = @TopicID and

		(a.Flags & 24)=16

	order by

		a.Posted asc

end



GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_post_list_reverse10(@TopicID int) as

begin

	set nocount on



	select top 10

		a.Posted,

		Subject = d.Topic,

		a.Message,

		a.UserID,

		a.Flags,

		UserName = IsNull(a.UserName,b.user_nick),

		b.user_Signature

	from

		yaf_Message a, 

		orionsbelt_users b,

		yaf_Topic d

	where

		(a.Flags & 24)=16 and

		a.TopicID = @TopicID and

		b.user_id = a.UserID and

		d.TopicID = a.TopicID

	order by

		a.Posted desc

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_rank_delete(@RankID int) as begin

	delete from yaf_Rank where RankID = @RankID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_rank_list(@BoardID int,@RankID int=null) as begin

	if @RankID is null

		select

			a.*

		from

			yaf_Rank a

		where

			a.BoardID=@BoardID

		order by

			a.Name

	else

		select

			a.*

		from

			yaf_Rank a

		where

			a.RankID = @RankID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_rank_save(

	@RankID		int,

	@BoardID	int,

	@Name		nvarchar(50),

	@IsStart	bit,

	@IsLadder	bit,

	@MinPosts	int,

	@RankImage	nvarchar(50)=null

) as

begin

	declare @Flags int



	if @IsLadder=0 set @MinPosts = null

	if @IsLadder=1 and @MinPosts is null set @MinPosts = 0

	

	set @Flags = 0

	if @IsStart<>0 set @Flags = @Flags | 1

	if @IsLadder<>0 set @Flags = @Flags | 2

	

	if @RankID>0 begin

		update yaf_Rank set

			Name = @Name,

			Flags = @Flags,

			MinPosts = @MinPosts,

			RankImage = @RankImage

		where RankID = @RankID

	end

	else begin

		insert into yaf_Rank(BoardID,Name,Flags,MinPosts,RankImage)

		values(@BoardID,@Name,@Flags,@MinPosts,@RankImage);

	end

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_registry_list(@Name nvarchar(50) = null,@BoardID int = null) as

BEGIN

	if @BoardID is null

	begin

		IF @Name IS NULL OR @Name = ''

		BEGIN

			SELECT * FROM yaf_Registry where BoardID is null

		END ELSE

		BEGIN

			SELECT * FROM yaf_Registry WHERE LOWER(Name) = LOWER(@Name) and BoardID is null

		END

	end else 

	begin

		IF @Name IS NULL OR @Name = ''

		BEGIN

			SELECT * FROM yaf_Registry where BoardID=@BoardID

		END ELSE

		BEGIN

			SELECT * FROM yaf_Registry WHERE LOWER(Name) = LOWER(@Name) and BoardID=@BoardID

		END

	end

END





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_registry_save(

	@Name nvarchar(50),

	@Value nvarchar(400) = NULL,

	@BoardID int = null

) AS

BEGIN

	if @BoardID is null

	begin

		if exists(select 1 from yaf_Registry where lower(Name)=lower(@Name))

			update yaf_Registry set Value = @Value where lower(Name)=lower(@Name) and BoardID is null

		else

		begin

			insert into yaf_Registry(Name,Value) values(lower(@Name),@Value)

		end

	end else

	begin

		if exists(select 1 from yaf_Registry where lower(Name)=lower(@Name) and BoardID=@BoardID)

			update yaf_Registry set Value = @Value where lower(Name)=lower(@Name) and BoardID=@BoardID

		else

		begin

			insert into yaf_Registry(Name,Value,BoardID) values(lower(@Name),@Value,@BoardID)

		end

	end

END





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_replace_words_delete(@ID int) as

begin

	delete from dbo.yaf_replace_words where ID = @ID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_replace_words_edit(@ID int=null) as

begin

	select * from yaf_replace_words where ID=@ID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_replace_words_list as begin

	select * from yaf_Replace_Words

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_replace_words_save(@ID int=null,@badword nvarchar(255),@goodword nvarchar(255)) as

begin

	if @ID is null or @ID = 0 begin

		insert into yaf_replace_words(badword,goodword) values(@badword,@goodword)

	end

	else begin

		update yaf_replace_words set badword = @badword,goodword = @goodword where ID = @ID

	end

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_smiley_delete(@SmileyID int=null) as begin

	if @SmileyID is not null

		delete from yaf_Smiley where SmileyID=@SmileyID

	else

		delete from yaf_Smiley

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_smiley_list(@BoardID int,@SmileyID int=null) as

begin

	if @SmileyID is null

		select * from yaf_Smiley where BoardID=@BoardID order by LEN(Code) desc

	else

		select * from yaf_Smiley where SmileyID=@SmileyID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_smiley_listunique(@BoardID int) as

begin

	select 

		Icon, 

		Emoticon,

		Code = (select top 1 Code from yaf_Smiley x where x.Icon=yaf_Smiley.Icon)

	from 

		yaf_Smiley

	where

		BoardID=@BoardID

	group by

		Icon,

		Emoticon

	order by

		Code

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_smiley_save(@SmileyID int=null,@BoardID int,@Code nvarchar(10),@Icon nvarchar(50),@Emoticon nvarchar(50),@Replace smallint=0) as begin

	if @SmileyID is not null begin

		update yaf_Smiley set Code = @Code, Icon = @Icon, Emoticon = @Emoticon where SmileyID = @SmileyID

	end

	else begin

		if @Replace>0

			delete from yaf_Smiley where Code=@Code



		if not exists(select 1 from yaf_Smiley where BoardID=@BoardID and Code=@Code)

			insert into yaf_Smiley(BoardID,Code,Icon,Emoticon) values(@BoardID,@Code,@Icon,@Emoticon)

	end

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_system_initialize(

	@Name		nvarchar(50),

	@TimeZone	int,

	@ForumEmail	nvarchar(50),

	@SmtpServer	nvarchar(50),

	@User		nvarchar(50),

	@UserEmail	nvarchar(50),

	@Password	nvarchar(32)

) as 

begin

	DECLARE @tmpValue AS nvarchar(100)



	-- initalize required 'registry' settings

	EXEC yaf_registry_save 'Version','1'

	EXEC yaf_registry_save 'VersionName','1.0.0'

	SET @tmpValue = CAST(@TimeZone AS nvarchar(100))

	EXEC yaf_registry_save 'TimeZone', @tmpValue

	EXEC yaf_registry_save 'SmtpServer', @SmtpServer

	EXEC yaf_registry_save 'ForumEmail', @ForumEmail



	-- initalize new board

	EXEC yaf_board_create @Name,0,@User,@UserEmail,@Password,1

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_system_updateversion(

	@Version		int,

	@VersionName	nvarchar(50)

) as

begin

	EXEC yaf_registry_save 'Version', @Version

	EXEC yaf_registry_save 'VersionName',@VersionName

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_topic_active(@BoardID int,@UserID int,@Since datetime,@CategoryID int=null) as

begin

	select

		c.ForumID,

		c.TopicID,

		c.Posted,

		LinkTopicID = IsNull(c.TopicMovedID,c.TopicID),

		Subject = c.Topic,

		c.UserID,

		Starter = IsNull(c.UserName,b.user_nick),

		Replies = (select count(1) from yaf_Message x where x.TopicID=c.TopicID and (x.Flags & 8)=0) - 1,

		Views = c.Views,

		LastPosted = c.LastPosted,

		LastUserID = c.LastUserID,

		LastUserName = IsNull(c.LastUserName,(select user_nick from orionsbelt_users x where x.user_id=c.LastUserID)),

		LastMessageID = c.LastMessageID,

		LastTopicID = c.TopicID,

		TopicFlags = c.Flags,

		c.Priority,

		c.PollID,

		ForumName = d.Name,

		c.TopicMovedID,

		ForumFlags = d.Flags

	from

		yaf_Topic c

		join orionsbelt_users b on b.user_id=c.UserID

		join yaf_Forum d on d.ForumID=c.ForumID

		join yaf_vaccess x on x.ForumID=d.ForumID

		join yaf_Category e on e.CategoryID=d.CategoryID

	where

		@Since < c.LastPosted and

		x.UserID = @UserID and

		x.ReadAccess <> 0 and

		e.BoardID = @BoardID and

		(@CategoryID is null or e.CategoryID=@CategoryID) and

		(c.Flags & 8)=0

	order by

		d.Name asc,

		Priority desc,

		LastPosted desc

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_topic_delete (@TopicID int,@UpdateLastPost bit=1) 

as

begin

	SET NOCOUNT ON



	declare @ForumID int

	declare @pollID int

	

	select @ForumID=ForumID from yaf_Topic where TopicID=@TopicID



	update yaf_Topic set LastMessageID = null where TopicID = @TopicID

	update yaf_Forum set 

		LastTopicID = null,

		LastMessageID = null,

		LastUserID = null,

		LastUserName = null,

		LastPosted = null

	where LastMessageID in (select MessageID from yaf_Message where TopicID = @TopicID)

	update yaf_Active set TopicID = null where TopicID = @TopicID

	

	--remove polls	

	select @pollID = pollID from yaf_topic where TopicID = @TopicID

	if (@pollID is not null)

	begin

		delete from yaf_choice where PollID = @PollID

		update yaf_topic set PollID = null where TopicID = @TopicID

		delete from yaf_poll where PollID = @PollID	

	end	

	

	--delete messages and topics

	delete from yaf_message where TopicID = @TopicID

	delete from yaf_topic where TopicMovedID = @TopicID

	delete from yaf_topic where TopicID = @TopicID

	

	--commit

	if @UpdateLastPost<>0

		exec yaf_forum_updatelastpost @ForumID

	

	if @ForumID is not null

		exec yaf_forum_updatestats @ForumID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_topic_findnext(@TopicID int) as

begin

	declare @LastPosted datetime

	declare @ForumID int

	select @LastPosted = LastPosted, @ForumID = ForumID from yaf_Topic where TopicID = @TopicID

	select top 1 TopicID from yaf_Topic where LastPosted>@LastPosted and ForumID = @ForumID order by LastPosted asc

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_topic_findprev(@TopicID int) as 

begin

	declare @LastPosted datetime

	declare @ForumID int

	select @LastPosted = LastPosted, @ForumID = ForumID from yaf_Topic where TopicID = @TopicID

	select top 1 TopicID from yaf_Topic where LastPosted<@LastPosted and ForumID = @ForumID order by LastPosted desc

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_topic_info(@TopicID int=null) as

begin

	if @TopicID = 0 set @TopicID = null

	if @TopicID is null

		select * from yaf_Topic

	else

		select * from yaf_Topic where TopicID = @TopicID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO





CREATE procedure dbo.yaf_topic_list(@ForumID int,@Announcement smallint,@Date datetime=null,@Offset int,@Count int) as

begin

	create table #data(

		RowNo	int identity primary key not null,

		TopicID	int not null

	)



	insert into #data(TopicID)

	select

		c.TopicID

	from

		yaf_Topic c join orionsbelt_users b on b.user_id=c.UserID join yaf_Forum d on d.ForumID=c.ForumID

	where

		c.ForumID = @ForumID and

		(@Date is null or c.Posted>=@Date or c.LastPosted>=@Date or Priority>0) and

		((@Announcement=1 and c.Priority=2) or (@Announcement=0 and c.Priority<>2) or (@Announcement<0)) and

		(c.TopicMovedID is not null or c.NumPosts>0) and

		(c.Flags & 8)=0

	order by

		Priority desc,

		c.LastPosted desc



	declare	@RowCount int

	set @RowCount = (select count(1) from #data)



	select

		[RowCount] = @RowCount,

		c.ForumID,

		c.TopicID,

		c.Posted,

		LinkTopicID = IsNull(c.TopicMovedID,c.TopicID),

		c.TopicMovedID,

		Subject = c.Topic,

		c.UserID,

		Starter = IsNull(c.UserName,b.user_nick),

		Replies = c.NumPosts - 1,

		Views = c.Views,

		LastPosted = c.LastPosted,

		LastUserID = c.LastUserID,

		LastUserName = IsNull(c.LastUserName,(select user_nick from orionsbelt_users x where x.user_id=c.LastUserID)),

		LastMessageID = c.LastMessageID,

		LastTopicID = c.TopicID,

		TopicFlags = c.Flags,

		c.Priority,

		c.PollID,

		ForumFlags = d.Flags

	from

		yaf_Topic c 

		join orionsbelt_users b on b.user_id=c.UserID 

		join yaf_Forum d on d.ForumID=c.ForumID 

		join #data e on e.TopicID=c.TopicID

	where

		e.RowNo between @Offset+1 and @Offset + @Count

	order by

		e.RowNo

end







GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_topic_listmessages(@TopicID int) as

begin

	select * from yaf_Message

	where TopicID = @TopicID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_topic_lock(@TopicID int,@Locked bit) as

begin

	if @Locked<>0

		update yaf_Topic set Flags = Flags | 1 where TopicID = @TopicID

	else

		update yaf_Topic set Flags = Flags & ~1 where TopicID = @TopicID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_topic_move(@TopicID int,@ForumID int,@ShowMoved bit) as

begin

	declare @OldForumID int



	select @OldForumID = ForumID from yaf_Topic where TopicID = @TopicID



	if @ShowMoved<>0 begin

		-- create a moved message

		insert into yaf_Topic(ForumID,UserID,UserName,Posted,Topic,Views,Flags,Priority,PollID,TopicMovedID,LastPosted,NumPosts)

		select ForumID,UserID,UserName,Posted,Topic,0,Flags,Priority,PollID,@TopicID,LastPosted,0

		from yaf_Topic where TopicID = @TopicID

	end



	-- move the topic

	update yaf_Topic set ForumID = @ForumID where TopicID = @TopicID



	-- update last posts

	exec yaf_topic_updatelastpost @OldForumID

	exec yaf_topic_updatelastpost @ForumID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_topic_prune(@ForumID int=null,@Days int) as

begin

	declare @c cursor

	declare @TopicID int

	declare @Count int

	set @Count = 0

	if @ForumID = 0 set @ForumID = null

	if @ForumID is not null begin

		set @c = cursor for

		select 

			TopicID

		from 

			yaf_Topic

		where 

			ForumID = @ForumID and

			Priority = 0 and

			datediff(dd,LastPosted,getdate())>@Days

	end

	else begin

		set @c = cursor for

		select 

			TopicID

		from 

			yaf_Topic

		where 

			Priority = 0 and

			datediff(dd,LastPosted,getdate())>@Days

	end

	open @c

	fetch @c into @TopicID

	while @@FETCH_STATUS=0 begin

		exec yaf_topic_delete @TopicID,0

		set @Count = @Count + 1

		fetch @c into @TopicID

	end

	close @c

	deallocate @c



	-- This takes forever with many posts...

	--exec yaf_topic_updatelastpost



	select Count = @Count

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_topic_save(

	@ForumID	int,

	@Subject	nvarchar(100),

	@UserID		int,

	@Message	ntext,

	@Priority	smallint,

	@UserName	nvarchar(50)=null,

	@IP			nvarchar(15),

	@PollID		int=null,

	@Posted		datetime=null,

	@Flags		int

) as

begin

	declare @TopicID int

	declare @MessageID int



	if @Posted is null set @Posted = getdate()



	insert into yaf_Topic(ForumID,Topic,UserID,Posted,Views,Priority,PollID,UserName,NumPosts)

	values(@ForumID,@Subject,@UserID,@Posted,0,@Priority,@PollID,@UserName,0)

	set @TopicID = @@IDENTITY

	exec yaf_message_save @TopicID,@UserID,@Message,@UserName,@IP,@Posted,null,@Flags,@MessageID output



	select TopicID = @TopicID, MessageID = @MessageID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_topic_updatelastpost(@ForumID int=null,@TopicID int=null) as

begin

	-- this really needs some work...

	if @TopicID is not null

		update yaf_Topic set

			LastPosted = (select top 1 x.Posted from yaf_Message x where x.TopicID=yaf_Topic.TopicID and (x.Flags & 24)=16 order by Posted desc),

			LastMessageID = (select top 1 x.MessageID from yaf_Message x where x.TopicID=yaf_Topic.TopicID and (x.Flags & 24)=16 order by Posted desc),

			LastUserID = (select top 1 x.UserID from yaf_Message x where x.TopicID=yaf_Topic.TopicID and (x.Flags & 24)=16 order by Posted desc),

			LastUserName = (select top 1 x.UserName from yaf_Message x where x.TopicID=yaf_Topic.TopicID and (x.Flags & 24)=16 order by Posted desc)

		where TopicID = @TopicID

	else

		update yaf_Topic set

			LastPosted = (select top 1 x.Posted from yaf_Message x where x.TopicID=yaf_Topic.TopicID and (x.Flags & 24)=16 order by Posted desc),

			LastMessageID = (select top 1 x.MessageID from yaf_Message x where x.TopicID=yaf_Topic.TopicID and (x.Flags & 24)=16 order by Posted desc),

			LastUserID = (select top 1 x.UserID from yaf_Message x where x.TopicID=yaf_Topic.TopicID and (x.Flags & 24)=16 order by Posted desc),

			LastUserName = (select top 1 x.UserName from yaf_Message x where x.TopicID=yaf_Topic.TopicID and (x.Flags & 24)=16 order by Posted desc)

		where TopicMovedID is null

		and (@ForumID is null or ForumID=@ForumID)



	if @ForumID is not null

		update yaf_Forum set

			LastPosted = (select top 1 y.Posted from yaf_Topic x,yaf_Message y where x.ForumID=yaf_Forum.ForumID and y.TopicID=x.TopicID and (y.Flags & 24)=16 order by y.Posted desc),

			LastTopicID = (select top 1 y.TopicID from yaf_Topic x,yaf_Message y where x.ForumID=yaf_Forum.ForumID and y.TopicID=x.TopicID and (y.Flags & 24)=16 order by y.Posted desc),

			LastMessageID = (select top 1 y.MessageID from yaf_Topic x,yaf_Message y where x.ForumID=yaf_Forum.ForumID and y.TopicID=x.TopicID and (y.Flags & 24)=16 order by y.Posted desc),

			LastUserID = (select top 1 y.UserID from yaf_Topic x,yaf_Message y where x.ForumID=yaf_Forum.ForumID and y.TopicID=x.TopicID and (y.Flags & 24)=16 order by y.Posted desc),

			LastUserName = (select top 1 y.UserName from yaf_Topic x,yaf_Message y where x.ForumID=yaf_Forum.ForumID and y.TopicID=x.TopicID and (y.Flags & 24)=16 order by y.Posted desc)

		where ForumID = @ForumID

	else 

		update yaf_Forum set

			LastPosted = (select top 1 y.Posted from yaf_Topic x,yaf_Message y where x.ForumID=yaf_Forum.ForumID and y.TopicID=x.TopicID and (y.Flags & 24)=16 order by y.Posted desc),

			LastTopicID = (select top 1 y.TopicID from yaf_Topic x,yaf_Message y where x.ForumID=yaf_Forum.ForumID and y.TopicID=x.TopicID and (y.Flags & 24)=16 order by y.Posted desc),

			LastMessageID = (select top 1 y.MessageID from yaf_Topic x,yaf_Message y where x.ForumID=yaf_Forum.ForumID and y.TopicID=x.TopicID and (y.Flags & 24)=16 order by y.Posted desc),

			LastUserID = (select top 1 y.UserID from yaf_Topic x,yaf_Message y where x.ForumID=yaf_Forum.ForumID and y.TopicID=x.TopicID and (y.Flags & 24)=16 order by y.Posted desc),

			LastUserName = (select top 1 y.UserName from yaf_Topic x,yaf_Message y where x.ForumID=yaf_Forum.ForumID and y.TopicID=x.TopicID and (y.Flags & 24)=16 order by y.Posted desc)

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_user_accessmasks(@BoardID int,@UserID int) as

begin

	select * from(

		select

			AccessMaskID	= e.AccessMaskID,

			AccessMaskName	= e.Name,

			ForumID			= f.ForumID,

			ForumName		= f.Name

		from

			orionsbelt_users a 

			join yaf_UserGroup b on b.UserID=a.user_id

			join yaf_Group c on c.GroupID=b.GroupID

			join yaf_ForumAccess d on d.GroupID=c.GroupID

			join yaf_AccessMask e on e.AccessMaskID=d.AccessMaskID

			join yaf_Forum f on f.ForumID=d.ForumID

		where

			a.user_id=@UserID and

			c.BoardID=@BoardID

		group by

			e.AccessMaskID,

			e.Name,

			f.ForumID,

			f.Name

		

		union

			

		select

			AccessMaskID	= c.AccessMaskID,

			AccessMaskName	= c.Name,

			ForumID			= d.ForumID,

			ForumName		= d.Name

		from

			orionsbelt_users a 

			join yaf_UserForum b on b.UserID=a.user_id

			join yaf_AccessMask c on c.AccessMaskID=b.AccessMaskID

			join yaf_Forum d on d.ForumID=b.ForumID

		where

			a.user_id=@UserID and

			c.BoardID=@BoardID

		group by

			c.AccessMaskID,

			c.Name,

			d.ForumID,

			d.Name

	) as x

	order by

		ForumName, AccessMaskName

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_user_activity_rank(@StartDate as datetime) AS

begin

	select top 3  ID, user_nick, NumOfPosts from orionsbelt_users u inner join

	(

		select m.UserID as ID, Count(m.UserID) as NumOfPosts from yaf_Message m

		where m.Posted >= @StartDate

		group by m.UserID

	) as counter

	on u.user_id = counter.ID

	order by NumOfPosts desc

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_user_adminsave(@BoardID int,@UserID int,@Name nvarchar(50),@Email nvarchar(50),@IsHostAdmin bit,@RankID int) as

begin

	if @IsHostAdmin<>0

		update orionsbelt_users set user_Flags = user_Flags | 1 where user_id = @UserID

	else

		update orionsbelt_users set user_Flags = user_Flags & ~1 where user_id = @UserID



	update orionsbelt_users set

		user_nick = @Name,

		user_mail = @Email,

		user_RankID = @RankID

	where user_id = @UserID

	select user_id = @UserID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_user_approve(@UserID int) as

begin

	declare @CheckEmailID int

	declare @Email nvarchar(50)



	select 

		@CheckEmailID = CheckEmailID,

		@Email = Email

	from

		yaf_CheckEmail

	where

		UserID = @UserID



	-- Update new user email

	update orionsbelt_users set user_mail = @Email, user_Flags = user_Flags | 2 where user_id = @UserID

	delete yaf_CheckEmail where CheckEmailID = @CheckEmailID

	select convert(bit,1)

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_user_avatarimage(@UserID int) as begin

	select user_id,user_Avatar from orionsbelt_users where user_id=@UserID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_user_changepassword(@UserID int,@OldPassword nvarchar(32),@NewPassword nvarchar(32)) as

begin

	declare @CurrentOld nvarchar(32)

	select @CurrentOld = user_pass from orionsbelt_users where user_id = @UserID

	if @CurrentOld<>@OldPassword begin

		select Success = convert(bit,0)

		return

	end

	update orionsbelt_users set user_pass = @NewPassword where user_id = @UserID

	select Success = convert(bit,1)

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_user_delete(@UserID int) as

begin

	declare @GuestUserID	int

	declare @UserName		nvarchar(50)

	declare @GuestCount		int



	select @UserName = user_nick from orionsbelt_users where user_id=@UserID



	select top 1

		@GuestUserID = a.user_id

	from

		orionsbelt_users a,

		yaf_UserGroup b,

		yaf_Group c

	where

		b.UserID = a.user_id and

		b.GroupID = c.GroupID and

		(c.Flags & 2)<>0



	select 

		@GuestCount = count(1) 

	from 

		yaf_UserGroup a

		join yaf_Group b on b.GroupID=a.GroupID

	where

		(b.Flags & 2)<>0



	if @GuestUserID=@UserID and @GuestCount=1 begin

		return

	end



	update yaf_Message set UserName=@UserName,UserID=@GuestUserID where UserID=@UserID

	update yaf_Topic set UserName=@UserName,UserID=@GuestUserID where UserID=@UserID

	update yaf_Topic set LastUserName=@UserName,LastUserID=@GuestUserID where LastUserID=@UserID

	update yaf_Forum set LastUserName=@UserName,LastUserID=@GuestUserID where LastUserID=@UserID



	delete from yaf_PMessage where FromUserID=@UserID

	delete from yaf_UserPMessage where UserID=@UserID

	delete from yaf_CheckEmail where UserID = @UserID

	delete from yaf_WatchTopic where UserID = @UserID

	delete from yaf_WatchForum where UserID = @UserID

	delete from yaf_UserGroup where UserID = @UserID

	--ABOT CHANGED

	--Delete UserForums entries Too 

	delete from yaf_UserForum where UserID = @UserID

	--END ABOT CHANGED 09.04.2004

	delete from orionsbelt_users where user_id = @UserID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_user_deleteavatar(@UserID int) as begin

	update orionsbelt_users set user_Avatar = '' where user_id = @UserID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_user_deleteold(@BoardID int) as

begin

	declare @Since datetime

	

	set @Since = getdate()



	delete from yaf_CheckEmail where UserID in(select user_id from orionsbelt_users where user_BoardID=@BoardID and Customer2163.yaf_bitset(user_Flags,2)=0 and datediff(day,user_registdate,@Since)>2)

	delete from yaf_UserGroup where UserID in(select user_id from orionsbelt_users where user_BoardID=@BoardID and Customer2163.yaf_bitset(user_Flags,2)=0 and datediff(day,user_registdate,@Since)>2)

	delete from orionsbelt_users where user_BoardID=@BoardID and Customer2163.yaf_bitset(user_Flags,2)=0 and datediff(day,user_registdate,@Since)>2

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_user_emails(@BoardID int,@GroupID int=null) as

begin

	if @GroupID = 0 set @GroupID = null

	if @GroupID is null

		select 

			a.user_mail 

		from 

			orionsbelt_users a

		where 

			a.user_mail is not null and 

			a.user_BoardID = @BoardID and

			a.user_mail is not null and 

			a.user_mail<>''

	else

		select 

			a.user_mail 

		from 

			orionsbelt_users a join yaf_UserGroup b on b.UserID=a.user_id

		where 

			b.GroupID = @GroupID and 

			a.user_mail is not null and 

			a.user_mail<>''

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_user_find(@BoardID int,@Filter bit,@UserName nvarchar(50)=null,@Email nvarchar(50)=null) as

begin

	if @Filter<>0

	begin

		if @UserName is not null

			set @UserName = '%' + @UserName + '%'



		select 

			a.*,

			IsGuest = (select count(1) from yaf_UserGroup x,yaf_Group y where x.UserID=a.user_id and x.GroupID=y.GroupID and (y.Flags & 2)<>0)

		from 

			orionsbelt_users a

		where 

			a.user_boardid=@BoardID and

			(@UserName is not null and a.user_nick like @UserName) or (@Email is not null and user_mail like @Email)

		order by

			a.user_nick

	end else

	begin

		select 

			a.user_id,

			IsGuest = (select count(1) from yaf_UserGroup x,yaf_Group y where x.UserID=a.user_id and x.GroupID=y.GroupID and (y.Flags & 2)<>0)

		from 

			orionsbelt_users a

		where 

			a.user_BoardID=@BoardID and

			((@UserName is not null and a.user_nick=@UserName) or (@Email is not null and user_mail=@Email))

	end

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_user_getsignature(@UserID int) as

begin

	select user_Signature from orionsbelt_users where User_ID = @UserID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_user_guest as

begin

	select top 1

		a.User_ID

	from

		orionsbelt_users a,

		yaf_UserGroup b,

		yaf_Group c

	where

		b.UserID = a.User_ID and

		b.GroupID = c.GroupID and

		(c.Flags & 2)<>0

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







CREATE procedure dbo.yaf_user_list(@BoardID int,@UserID int=null,@Approved bit=null,@GroupID int=null,@RankID int=null) as

begin

	if @UserID is not null

		select 

			a.*,

			a.user_NumPosts,

			b.RankID,

			RankName = b.Name,

			NumDays = datediff(d,a.user_registdate,getdate())+1,

			NumPostsForum = (select count(1) from yaf_Message x where (x.Flags & 24)=16),

			HasAvatarImage = (select count(1) from orionsbelt_users x where x.user_id=a.User_ID and user_Avatar<>''),

			IsAdmin	= IsNull(c.IsAdmin,0),

			IsGuest	= IsNull(c.IsGuest,0),

			IsForumModerator	= IsNull(c.IsForumModerator,0),

			IsModerator		= IsNull(c.IsModerator,0)

		from 

			orionsbelt_users a

			join yaf_Rank b on b.RankID=a.user_RankID

			left join yaf_vaccess c on c.UserID=a.User_ID

		where 

			a.User_ID = @UserID and

			a.user_BoardID = @BoardID and

			IsNull(c.ForumID,0) = 0			

		order by 

			a.user_nick

	else if @GroupID is null and @RankID is null

		select 

			a.*,

			a.user_NumPosts,

			IsAdmin = (select count(1) from yaf_UserGroup x,yaf_Group y where x.UserID=a.User_ID and y.GroupID=x.GroupID and (y.Flags & 1)<>0),

			b.RankID,

			RankName = b.Name

		from 

			orionsbelt_users a

			join yaf_Rank b on b.RankID=a.user_RankID

		where 

			a.user_BoardID = @BoardID and

			(@Approved is null or (@Approved=0 and (a.user_Flags & 2)=0) or (@Approved=1 and (a.user_Flags & 2)=2))

		order by 

			a.user_nick

	else

		select 

			a.*,

			a.user_NumPosts,

			IsAdmin = (select count(1) from yaf_UserGroup x,yaf_Group y where x.UserID=a.User_ID and y.GroupID=x.GroupID and (y.Flags & 1)<>0),

			b.RankID,

			RankName = b.Name

		from 

			orionsbelt_users a

			join yaf_Rank b on b.RankID=a.user_RankID

		where 

			a.user_BoardID = @BoardID and

			(@Approved is null or (@Approved=0 and (a.user_Flags & 2)=0) or (@Approved=1 and (a.user_Flags & 2)=2)) and

			(@GroupID is null or exists(select 1 from yaf_UserGroup x where x.UserID=a.User_ID and x.GroupID=@GroupID)) and

			(@RankID is null or a.user_RankID=@RankID)

		order by 

			a.user_nick

end



GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_user_login(@BoardID int,@Name nvarchar(50),@Password nvarchar(32)) as

begin

	declare @UserID int



	if not exists(select User_ID from orionsbelt_users where user_nick=@Name and user_Pass=@Password and (user_BoardID=@BoardID or (user_Flags & 3)=3))

		set @UserID=null

	else

		select 

			@UserID=User_ID 

		from 

			orionsbelt_users 

		where 

			user_nick=@Name and 

			user_pass=@Password and 

			(user_BoardID=@BoardID or (user_Flags & 1)=1) and

			(user_Flags & 2)=2



	select @UserID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_user_nntp(@BoardID int,@UserName nvarchar(50),@Email nvarchar(50)) as

begin

	declare @UserID int



	set @UserName = @UserName + ' (NNTP)'



	select

		@UserID=User_ID

	from

		orionsbelt_users

	where

		user_BoardID=@BoardID and

		user_nick=@UserName



	if @@ROWCOUNT<1

	begin

		exec yaf_user_save 0,@BoardID,@UserName,'-',@Email,null,'Usenet',null,0,null,null,null,1,null,null,null,null,null,null,null,0,null

		-- The next one is not safe, but this procedure is only used for testing

		select @UserID=max(User_ID) from orionsbelt_users

	end



	select UserID=@UserID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_user_recoverpassword(@BoardID int,@UserName nvarchar(50),@Email nvarchar(50),@Password nvarchar(32)) as

begin

	declare @UserID int

	select @UserID = User_ID from orionsbelt_users where user_BoardID = @BoardID and user_nick = @UserName and user_mail = @Email

	if @UserID is null begin

		select Success = convert(bit,0)

		return

	end

	update orionsbelt_users set user_pass = @Password where user_id = @UserID

	if @@rowcount<>1

	begin

		select Success = convert(bit,0)

		return

	end

	select Success = convert(bit,1)

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_user_save(

	@UserID			int,

	@BoardID		int,

	@UserName		nvarchar(50) = null,

	@Password		nvarchar(32) = null,

	@Email			nvarchar(50) = null,

	@Hash			nvarchar(32) = null,

	@Location		nvarchar(50) = null,

	@HomePage		nvarchar(50) = null,

	@TimeZone		int,

	@Avatar			nvarchar(255) = null,

	@LanguageFile	nvarchar(50) = null,

	@ThemeFile		nvarchar(50) = null,

	@Approved		bit = null,

	@MSN			nvarchar(50) = null,

	@YIM			nvarchar(30) = null,

	@AIM			nvarchar(30) = null,

	@ICQ			int = null,

	@RealName		nvarchar(50) = null,

	@Occupation		nvarchar(50) = null,

	@Interests		nvarchar(100) = null,

	@Gender			tinyint = 0,

	@Weblog			nvarchar(100) = null

) as

begin

	declare @RankID int

	declare @Flags int

	

	set @Flags = 0

	if @Approved<>0 set @Flags = @Flags | 2

	

	if @Location is not null and @Location = '' set @Location = null

	if @HomePage is not null and @HomePage = '' set @HomePage = null

	if @Avatar is not null and @Avatar = '' set @Avatar = null

	if @MSN is not null and @MSN = '' set @MSN = null

	if @YIM is not null and @YIM = '' set @YIM = null

	if @AIM is not null and @AIM = '' set @AIM = null

	if @ICQ is not null and @ICQ = 0 set @ICQ = null

	if @RealName is not null and @RealName = '' set @RealName = null

	if @Occupation is not null and @Occupation = '' set @Occupation = null

	if @Interests is not null and @Interests = '' set @Interests = null

	if @Weblog is not null and @Weblog = '' set @Weblog = null



	if @UserID is null or @UserID<1 begin

		if @Email = '' set @Email = null

		

		select @RankID = RankID from yaf_Rank where (Flags & 1)<>0 and BoardID=@BoardID

		

		insert into orionsbelt_users(user_BoardID,user_RankID,user_nick,user_pass,user_mail,user_registdate,user_lastlogin,user_NumPosts,user_Location,user_website,user_TimeZone,user_Avatar,user_Gender,user_Flags) 

		values(@BoardID,@RankID,@UserName,@Password,@Email,getdate(),getdate(),0,@Location,@HomePage,@TimeZone,@Avatar,@Gender,@Flags)

	

		set @UserID = @@IDENTITY



		insert into yaf_UserGroup(UserID,GroupID) select @UserID,GroupID from yaf_Group where BoardID=@BoardID and (Flags & 4)<>0

		

		if @Hash is not null and @Hash <> '' and @Approved=0 begin

			insert into yaf_CheckEmail(UserID,Email,Created,Hash)

			values(@UserID,@Email,getdate(),@Hash)	

		end

	end

	else begin

		update orionsbelt_users set

			user_Location = @Location,

			user_website = @HomePage,

			user_TimeZone = @TimeZone,

			user_Avatar = @Avatar,

			user_MSN = @MSN,

			user_jabber = @YIM,

			user_AIM = @AIM,

			user_ICQ = @ICQ,

			user_gender = @Gender

		where User_ID = @UserID

		

		if @Email is not null

			update orionsbelt_users set user_mail = @Email where user_id = @UserID

	end

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_user_saveavatar(@UserID int,@Avatar nvarchar) as

begin

	update orionsbelt_users set user_Avatar=@Avatar where User_ID = @UserID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_user_savesignature(@UserID int,@Signature nvarchar) as

begin

	update orionsbelt_users set user_Signature = @Signature where User_ID = @UserID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_user_suspend(@UserID int,@Suspend datetime=null) as

begin

	select * from orionsbelt_users

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_user_upgrade(@UserID int) as

begin

	declare @RankID		int

	declare @Flags		int

	declare @MinPosts	int

	declare @NumPosts	int

	-- Get user and rank information

	select

		@RankID = b.RankID,

		@Flags = b.Flags,

		@MinPosts = b.MinPosts,

		@NumPosts = a.user_NumPosts

	from

		orionsbelt_users a,

		yaf_Rank b

	where

		a.User_ID = @UserID and

		b.RankID = a.user_RankID

	

	-- If user isn't member of a ladder rank, exit

	if (@Flags & 2) = 0 return

	

	-- See if user got enough posts for next ladder group

	select top 1

		@RankID = RankID

	from

		yaf_Rank

	where

		(Flags & 2) = 2 and

		MinPosts <= @NumPosts and

		MinPosts > @MinPosts

	order by

		MinPosts

	if @@ROWCOUNT=1

		update orionsbelt_users set user_RankID = @RankID where User_ID = @UserID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_userforum_delete(@UserID int,@ForumID int) as

begin

	delete from yaf_UserForum where UserID=@UserID and ForumID=@ForumID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_userforum_list(@UserID int=null,@ForumID int=null) as 

begin

	select 

		a.*,

		b.AccessMaskID,

		b.Accepted,

		Access = c.Name

	from

		orionsbelt_users a join yaf_UserForum b on b.UserID=a.User_ID

		join yaf_AccessMask c on c.AccessMaskID=b.AccessMaskID

	where

		(@UserID is null or a.User_ID=@UserID) and

		(@ForumID is null or b.ForumID=@ForumID)

	order by

		a.user_nick	

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_userforum_save(@UserID int,@ForumID int,@AccessMaskID int) as

begin

	if exists(select 1 from yaf_UserForum where UserID=@UserID and ForumID=@ForumID)

		update yaf_UserForum set AccessMaskID=@AccessMaskID where UserID=@UserID and ForumID=@ForumID

	else

		insert into yaf_UserForum(UserID,ForumID,AccessMaskID,Invited,Accepted) values(@UserID,@ForumID,@AccessMaskID,getdate(),1)

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_usergroup_list(@UserID int) as begin

	select 

		b.GroupID,

		b.Name

	from

		yaf_UserGroup a

		join yaf_Group b on b.GroupID=a.GroupID

	where

		a.UserID = @UserID

	order by

		b.Name

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_usergroup_save(@UserID int,@GroupID int,@Member bit) as

begin

	if @Member=0

		delete from yaf_UserGroup where UserID=@UserID and GroupID=@GroupID

	else

		insert into yaf_UserGroup(UserID,GroupID)

		select @UserID,@GroupID

		where not exists(select 1 from yaf_UserGroup where UserID=@UserID and GroupID=@GroupID)

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_userpmessage_delete(@UserPMessageID int) as

begin

	delete from yaf_UserPMessage where UserPMessageID=@UserPMessageID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_userpmessage_list(@UserPMessageID int) as

begin

	select

		a.*,

		FromUser = b.user_nick,

		ToUserID = c.User_ID,

		ToUser = c.user_nick,

		d.IsRead,

		d.UserPMessageID

	from

		yaf_PMessage a,

		orionsbelt_users b,

		orionsbelt_users c,

		yaf_UserPMessage d

	where

		b.User_ID = a.FromUserID and

		c.User_ID = d.UserID and

		d.PMessageID = a.PMessageID and

		d.UserPMessageID = @UserPMessageID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_watchforum_add(@UserID int,@ForumID int) as

begin

	insert into yaf_WatchForum(ForumID,UserID,Created)

	select @ForumID, @UserID, getdate()

	where not exists(select 1 from yaf_WatchForum where ForumID=@ForumID and UserID=@UserID)

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_watchforum_check(@UserID int,@ForumID int) as

begin

	SELECT WatchForumID FROM yaf_WatchForum WHERE UserID = @UserID AND ForumID = @ForumID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_watchforum_delete(@WatchForumID int) as

begin

	delete from yaf_WatchForum where WatchForumID = @WatchForumID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_watchforum_list(@UserID int) as

begin

	select

		a.*,

		ForumName = b.Name,

		Messages = (select count(1) from yaf_Topic x, yaf_Message y where x.ForumID=a.ForumID and y.TopicID=x.TopicID),

		Topics = (select count(1) from yaf_Topic x where x.ForumID=a.ForumID and x.TopicMovedID is null),

		b.LastPosted,

		b.LastMessageID,

		LastTopicID = (select TopicID from yaf_Message x where x.MessageID=b.LastMessageID),

		b.LastUserID,

		LastUserName = IsNull(b.LastUserName,(select user_nick from orionsbelt_users x where x.User_ID=b.LastUserID))

	from

		yaf_WatchForum a,

		yaf_Forum b

	where

		a.UserID = @UserID and

		b.ForumID = a.ForumID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_watchtopic_add(@UserID int,@TopicID int) as

begin

	insert into yaf_WatchTopic(TopicID,UserID,Created)

	select @TopicID, @UserID, getdate()

	where not exists(select 1 from yaf_WatchTopic where TopicID=@TopicID and UserID=@UserID)

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_watchtopic_check(@UserID int,@TopicID int) as

begin

	SELECT WatchTopicID FROM yaf_WatchTopic WHERE UserID = @UserID AND TopicID = @TopicID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_watchtopic_delete(@WatchTopicID int) as

begin

	delete from yaf_WatchTopic where WatchTopicID = @WatchTopicID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create procedure dbo.yaf_watchtopic_list(@UserID int) as

begin

	select

		a.*,

		TopicName = b.Topic,

		Replies = (select count(1) from yaf_Message x where x.TopicID=b.TopicID),

		b.Views,

		b.LastPosted,

		b.LastMessageID,

		b.LastUserID,

		LastUserName = IsNull(b.LastUserName,(select user_nick from orionsbelt_users x where x.User_ID=b.LastUserID))

	from

		yaf_WatchTopic a,

		yaf_Topic b

	where

		a.UserID = @UserID and

		b.TopicID = a.TopicID

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create function yaf_bitset(@Flags int,@Mask int) returns bit as

begin

	declare @bool bit



	if (@Flags & @Mask) = @Mask

		set @bool = 1

	else

		set @bool = 0

		

	return @bool

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create function yaf_forum_posts(@ForumID int) returns int as

begin

	declare @NumPosts int

	declare @tmp int



	select @NumPosts=NumPosts from Customer2163.yaf_Forum where ForumID=@ForumID



	if exists(select 1 from Customer2163.yaf_Forum where ParentID=@ForumID)

	begin

		declare c cursor for

		select ForumID from Customer2163.yaf_Forum

		where ParentID = @ForumID

		

		open c

		

		fetch next from c into @tmp

		while @@FETCH_STATUS = 0

		begin

			set @NumPosts=@NumPosts+Customer2163.yaf_forum_posts(@tmp)

			fetch next from c into @tmp

		end

		close c

		deallocate c

	end



	return @NumPosts

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO







create function yaf_forum_topics(@ForumID int) returns int as

begin

	declare @NumTopics int

	declare @tmp int



	select @NumTopics=NumTopics from dbo.yaf_Forum where ForumID=@ForumID



	if exists(select 1 from dbo.yaf_Forum where ParentID=@ForumID)

	begin

		declare c cursor for

		select ForumID from Customer2163.yaf_Forum

		where ParentID = @ForumID

		

		open c

		

		fetch next from c into @tmp

		while @@FETCH_STATUS = 0

		begin

			set @NumTopics=@NumTopics+Customer2163.yaf_forum_topics(@tmp)

			fetch next from c into @tmp

		end

		close c

		deallocate c

	end



	return @NumTopics

end





GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO





create trigger yaf_Active_insert on dbo.yaf_Active for insert as

begin

	declare @BoardID int, @count int, @max int



	-- Assumes only one row was inserted - shouldn't be a problem?

	select @BoardID = BoardID from inserted

	

	select @count = count(distinct IP) from yaf_Active with(nolock) where BoardID=@BoardID

	select @max = cast(Value as int) from yaf_Registry where BoardID=@BoardID and Name=N'maxusers'

	if @@rowcount=0

	begin

		insert into yaf_Registry(BoardID,Name,Value) values(@BoardID,N'maxusers',cast(@count as nvarchar))

		insert into yaf_Registry(BoardID,Name,Value) values(@BoardID,N'maxuserswhen',convert(nvarchar,getdate(),126))

	end else if @count>@max

	begin

		update yaf_Registry set Value=cast(@count as nvarchar) where BoardID=@BoardID and Name=N'maxusers'

		update yaf_Registry set Value=convert(nvarchar,getdate(),126) where BoardID=@BoardID and Name=N'maxuserswhen'

	end

end



GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



SET QUOTED_IDENTIFIER ON 

GO

SET ANSI_NULLS ON 

GO





create trigger yaf_Forum_update on dbo.yaf_Forum for update as

begin

	if update(LastTopicID) or update(LastMessageID)

	begin

		update a set

			a.LastPosted=b.LastPosted,

			a.LastTopicID=b.LastTopicID,

			a.LastMessageID=b.LastMessageID,

			a.LastUserID=b.LastUserID,

			a.LastUserName=b.LastUserName

		from

			yaf_Forum a join inserted b on a.ForumID=b.ParentID

		where

			a.LastPosted < b.LastPosted

	end

end



GO

SET QUOTED_IDENTIFIER OFF 

GO

SET ANSI_NULLS ON 

GO



