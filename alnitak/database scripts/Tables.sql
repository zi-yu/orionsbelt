if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ForumAccess_AccessMask]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[yaf_ForumAccess] DROP CONSTRAINT FK_ForumAccess_AccessMask
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_UserForum_AccessMask]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[yaf_UserForum] DROP CONSTRAINT FK_UserForum_AccessMask
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_OrionsBelt_Users_yaf_Board]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[OrionsBelt_Users] DROP CONSTRAINT FK_OrionsBelt_Users_yaf_Board
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

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_OrionsBelt_Users_yaf_Rank]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[OrionsBelt_Users] DROP CONSTRAINT FK_OrionsBelt_Users_yaf_Rank
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

