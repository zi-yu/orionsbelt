/* Version 1.0.0 */

/*
** Create missing tables
*/
if not exists (select 1 from sysobjects where id = object_id(N'yaf_Active') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	create table dbo.yaf_Active(
		SessionID		nvarchar (24) NOT NULL ,
		BoardID			int NOT NULL ,
		UserID			int NOT NULL ,
		IP				nvarchar (15) NOT NULL ,
		Login			datetime NOT NULL ,
		LastActive		datetime NOT NULL ,
		Location		nvarchar (50) NOT NULL ,
		ForumID			int NULL ,
		TopicID			int NULL ,
		Browser			nvarchar (50) NULL ,
		Platform		nvarchar (50) NULL 
	)
go

if not exists (select 1 from sysobjects where id = object_id(N'yaf_BannedIP') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	create table dbo.yaf_BannedIP(
		ID				int IDENTITY (1, 1) NOT NULL ,
		BoardID			int NOT NULL ,
		Mask			nvarchar (15) NOT NULL ,
		Since			datetime NOT NULL 
	)
go

if not exists (select 1 from sysobjects where id = object_id(N'yaf_Category') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	create table dbo.yaf_Category(
		CategoryID		int IDENTITY (1, 1) NOT NULL ,
		BoardID			int NOT NULL ,
		Name			nvarchar (50) NOT NULL ,
		SortOrder		smallint NOT NULL 
	)
GO

if not exists (select 1 from sysobjects where id = object_id(N'yaf_CheckEmail') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	create table dbo.yaf_CheckEmail(
		CheckEmailID	int IDENTITY (1, 1) NOT NULL ,
		UserID			int NOT NULL ,
		Email			nvarchar (50) NOT NULL ,
		Created			datetime NOT NULL ,
		Hash			nvarchar (32) NOT NULL 
	)
GO

if not exists (select 1 from sysobjects where id = object_id(N'yaf_Choice') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	create table dbo.yaf_Choice(
		ChoiceID		int IDENTITY (1, 1) NOT NULL ,
		PollID			int NOT NULL ,
		Choice			nvarchar (50) NOT NULL ,
		Votes			int NOT NULL 
	)
GO

if not exists (select 1 from sysobjects where id = object_id(N'yaf_Forum') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	create table dbo.yaf_Forum(
		ForumID			int IDENTITY (1, 1) NOT NULL ,
		CategoryID		int NOT NULL ,
		ParentID		int NULL ,
		Name			nvarchar (50) NOT NULL ,
		Description		nvarchar (255) NOT NULL ,
		SortOrder		smallint NOT NULL ,
		LastPosted		datetime NULL ,
		LastTopicID		int NULL ,
		LastMessageID	int NULL ,
		LastUserID		int NULL ,
		LastUserName	nvarchar (50) NULL ,
		NumTopics		int NOT NULL,
		NumPosts		int NOT NULL,
		RemoteURL		nvarchar(100) null,
		Flags			int not null constraint DF_yaf_Forum_Flags default (0)
	)
GO

if not exists (select 1 from sysobjects where id = object_id(N'yaf_ForumAccess') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	create table dbo.yaf_ForumAccess(
		GroupID			int NOT NULL ,
		ForumID			int NOT NULL ,
		AccessMaskID	int NOT NULL
	)
GO

if not exists (select * from sysobjects where id = object_id(N'yaf_Group') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	create table dbo.yaf_Group(
		GroupID			int IDENTITY (1, 1) NOT NULL ,
		BoardID			int NOT NULL ,
		Name			nvarchar (50) NOT NULL ,
		Flags			int not null constraint DF_yaf_Group_Flags default (0)
	)
GO

if not exists (select * from sysobjects where id = object_id(N'yaf_Mail') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	create table dbo.yaf_Mail(
		MailID			int IDENTITY (1, 1) NOT NULL ,
		FromUser		nvarchar (50) NOT NULL ,
		ToUser			nvarchar (50) NOT NULL ,
		Created			datetime NOT NULL ,
		Subject			nvarchar (100) NOT NULL ,
		Body			ntext NOT NULL 
	)
GO

if not exists (select * from sysobjects where id = object_id(N'yaf_Message') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	create table dbo.yaf_Message(
		MessageID		int IDENTITY (1, 1) NOT NULL ,
		TopicID			int NOT NULL ,
		ReplyTo			int NULL ,
		Position		int NOT NULL ,
		Indent			int NOT NULL ,
		UserID			int NOT NULL ,
		UserName		nvarchar (50) NULL ,
		Posted			datetime NOT NULL ,
		Message			ntext NOT NULL ,
		IP				nvarchar (15) NOT NULL ,
		Edited			datetime NULL ,
		Flags			int NOT NULL constraint DF_yaf_Message_Flags default (23)
	)
GO

if not exists (select * from sysobjects where id = object_id(N'yaf_PMessage') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	create table dbo.yaf_PMessage(
		PMessageID		int IDENTITY (1, 1) NOT NULL ,
		FromUserID		int NOT NULL ,
		Created			datetime NOT NULL ,
		Subject			nvarchar (100) NOT NULL ,
		Body			ntext NOT NULL,
		Flags			int NOT NULL 
	)
GO

if not exists (select * from sysobjects where id = object_id(N'yaf_Poll') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	create table dbo.yaf_Poll(
		PollID			int IDENTITY (1, 1) NOT NULL ,
		Question		nvarchar (50) NOT NULL 
	)
GO

if not exists (select * from sysobjects where id = object_id(N'yaf_Smiley') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	create table dbo.yaf_Smiley(
		SmileyID		int IDENTITY (1, 1) NOT NULL ,
		BoardID			int NOT NULL ,
		Code			nvarchar (10) NOT NULL ,
		Icon			nvarchar (50) NOT NULL ,
		Emoticon		nvarchar (50) NULL 
	)
GO

if not exists (select * from sysobjects where id = object_id(N'yaf_Topic') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	create table dbo.yaf_Topic(
		TopicID			int IDENTITY (1, 1) NOT NULL ,
		ForumID			int NOT NULL ,
		UserID			int NOT NULL ,
		UserName		nvarchar (50) NULL ,
		Posted			datetime NOT NULL ,
		Topic			nvarchar (100) NOT NULL ,
		Views			int NOT NULL ,
		Priority		smallint NOT NULL ,
		PollID			int NULL ,
		TopicMovedID	int NULL ,
		LastPosted		datetime NULL ,
		LastMessageID	int NULL ,
		LastUserID		int NULL ,
		LastUserName	nvarchar (50) NULL,
		NumPosts		int NOT NULL,
		Flags			int not null constraint DF_yaf_Topic_Flags default (0)
	)
GO

if not exists (select * from sysobjects where id = object_id(N'yaf_User') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	create table dbo.yaf_User(
		UserID			int IDENTITY (1, 1) NOT NULL ,
		BoardID			int NOT NULL,
		Name			nvarchar (50) NOT NULL ,
		Password		nvarchar (32) NOT NULL ,
		Email			nvarchar (50) NULL ,
		Joined			datetime NOT NULL ,
		LastVisit		datetime NOT NULL ,
		IP				nvarchar (15) NULL ,
		NumPosts		int NOT NULL ,
		Location		nvarchar (50) NULL ,
		HomePage		nvarchar (50) NULL ,
		TimeZone		int NOT NULL ,
		Avatar			nvarchar (255) NULL ,
		Signature		ntext NULL ,
		AvatarImage		image NULL,
		RankID			int NOT NULL,
		Suspended		datetime NULL,
		LanguageFile	nvarchar(50) NULL,
		ThemeFile		nvarchar(50) NULL,
		MSN				nvarchar (50) NULL ,
		YIM				nvarchar (30) NULL ,
		AIM				nvarchar (30) NULL ,
		ICQ				int NULL ,
		RealName		nvarchar (50) NULL ,
		Occupation		nvarchar (50) NULL ,
		Interests		nvarchar (100) NULL ,
		Gender			tinyint NOT NULL ,
		Weblog			nvarchar (100) NULL,
		Flags			int not null constraint DF_yaf_User_Flags default (0)
)
GO

if not exists (select * from sysobjects where id = object_id(N'yaf_WatchForum') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	create table dbo.yaf_WatchForum(
		WatchForumID	int IDENTITY (1, 1) NOT NULL ,
		ForumID			int NOT NULL ,
		UserID			int NOT NULL ,
		Created			datetime NOT NULL ,
		LastMail		datetime null
	)
GO

if not exists (select * from sysobjects where id = object_id(N'yaf_WatchTopic') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	create table dbo.yaf_WatchTopic(
		WatchTopicID	int IDENTITY (1, 1) NOT NULL ,
		TopicID			int NOT NULL ,
		UserID			int NOT NULL ,
		Created			datetime NOT NULL ,
		LastMail		datetime null
	)
GO

if not exists (select * from sysobjects where id = object_id(N'yaf_Attachment') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	create table dbo.yaf_Attachment(
		AttachmentID	int identity not null,
		MessageID		int not null,
		FileName		nvarchar(250) not null,
		Bytes			int not null,
		FileID			int null,
		ContentType		nvarchar(50) null,
		Downloads		int not null,
		FileData		image null
	)
GO

if not exists (select * from sysobjects where id = object_id(N'yaf_UserGroup') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	create table dbo.yaf_UserGroup(
		UserID			int NOT NULL,
		GroupID			int NOT NULL
	)
GO

if not exists (select * from sysobjects where id = object_id(N'yaf_Rank') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	create table dbo.yaf_Rank(
		RankID			int IDENTITY (1, 1) NOT NULL,
		BoardID			int NOT NULL ,
		Name			nvarchar (50) NOT NULL,
		MinPosts		int NULL,
		RankImage		nvarchar (50) NULL,
		Flags			int not null constraint DF_yaf_Rank_Flags default (0)
	)
GO

if not exists (select * from sysobjects where id = object_id(N'yaf_AccessMask') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	create table dbo.yaf_AccessMask(
		AccessMaskID	int IDENTITY NOT NULL ,
		BoardID			int NOT NULL ,
		Name			nvarchar(50) NOT NULL ,
		Flags			int not null constraint DF_yaf_AccessMask_Flags default (0)
	)
GO

if not exists (select * from sysobjects where id = object_id(N'yaf_UserForum') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	create table dbo.yaf_UserForum(
		UserID			int NOT NULL ,
		ForumID			int NOT NULL ,
		AccessMaskID	int NOT NULL ,
		Invited			datetime NOT NULL ,
		Accepted		bit NOT NULL
	)
GO

if not exists (select * from sysobjects where id = object_id(N'yaf_Board') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
begin
	create table dbo.yaf_Board(
		BoardID			int NOT NULL IDENTITY(1,1),
		Name			nvarchar(50) NOT NULL,
		AllowThreaded	bit NOT NULL,
	)
end
GO

if not exists (select * from sysobjects where id = object_id(N'yaf_NntpServer') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	create table dbo.yaf_NntpServer(
		NntpServerID	int identity not null,
		BoardID			int NOT NULL ,
		Name			nvarchar(50) not null,
		Address			nvarchar(100) not null,
		Port			int null,
		UserName		nvarchar(50) null,
		UserPass		nvarchar(50) null
	)
GO

if not exists (select * from sysobjects where id = object_id(N'yaf_NntpForum') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	create table dbo.yaf_NntpForum(
		NntpForumID		int identity not null,
		NntpServerID	int not null,
		GroupName		nvarchar(100) not null,
		ForumID			int not null,
		LastMessageNo	int not null,
		LastUpdate		datetime not null,
		Active			bit not null
	)
GO

if not exists (select * from sysobjects where id = object_id(N'yaf_NntpTopic') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	create table dbo.yaf_NntpTopic(
		NntpTopicID		int identity not null,
		NntpForumID		int not null,
		Thread			char(32) not null,
		TopicID			int not null
	)
GO

if not exists (select * from sysobjects where id = object_id(N'yaf_UserPMessage') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
begin
	create table dbo.yaf_UserPMessage(
		UserPMessageID	int identity not null,
		UserID			int not null,
		PMessageID		int not null,
		IsRead			bit not null
	)
end
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'yaf_Replace_Words') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	create table dbo.yaf_Replace_Words(
		id				int IDENTITY (1, 1) NOT NULL ,
		badword			nvarchar (255) NULL ,
		goodword		nvarchar (255) NULL ,
		constraint PK_Replace_Words primary key(id)
	)
GO

if not exists (select * from sysobjects where id = object_id(N'yaf_Registry') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
begin
	create table dbo.yaf_Registry(
		RegistryID		int IDENTITY(1, 1) NOT NULL,
		Name			nvarchar(50) NOT NULL,
		Value			nvarchar(400),
		BoardID			int,
		CONSTRAINT PK_Registry PRIMARY KEY (RegistryID)
	)
end
GO

/*
** Added columns
*/

if exists(select 1 from dbo.syscolumns where id = object_id(N'yaf_User') and name=N'Signature' and xtype<>99)
	alter table yaf_User alter column Signature ntext null
go

if not exists(select * from syscolumns where id=object_id('yaf_Forum') and name='RemoteURL')
	alter table yaf_Forum add RemoteURL nvarchar(100) null
GO

if not exists(select 1 from syscolumns where id=object_id('yaf_NntpForum') and name='Active')
begin
	alter table yaf_NntpForum add Active bit null
	exec('update yaf_NntpForum set Active=1 where Active is null')
	alter table yaf_NntpForum alter column Active bit not null
end
GO

if exists (select * from dbo.syscolumns where id = object_id(N'yaf_Replace_Words') and name='badword' and prec < 255)
 	alter table yaf_Replace_Words alter column badword nvarchar(255) NULL
GO

if exists (select * from dbo.syscolumns where id = object_id(N'yaf_Replace_Words') and name='goodword' and prec < 255)
	alter table yaf_Replace_Words alter column goodword nvarchar(255) NULL
GO	

if not exists(select 1 from syscolumns where id=object_id('yaf_Registry') and name='BoardID')
	alter table yaf_Registry add BoardID int
GO

if not exists(select 1 from syscolumns where id=object_id('yaf_PMessage') and name='Flags')
begin
	alter table dbo.yaf_PMessage add Flags int not null constraint DF_yaf_Message_Flags default (23)
end
GO

if not exists(select 1 from syscolumns where id=object_id('yaf_Topic') and name='Flags')
begin
	alter table dbo.yaf_Topic add Flags int not null constraint DF_yaf_Topic_Flags default (0)
	update yaf_Message set Flags = Flags & 7
end
GO

if exists(select 1 from syscolumns where id=object_id('yaf_Message') and name='Approved')
begin
	exec('update yaf_Message set Flags = Flags | 16 where Approved<>0')
	alter table dbo.yaf_Message drop column Approved
end
GO

if not exists(select 1 from syscolumns where id=object_id('yaf_Forum') and name='Flags')
begin
	alter table dbo.yaf_Forum add Flags int not null constraint DF_yaf_Forum_Flags default (0)
end
GO

if exists(select 1 from syscolumns where id=object_id('yaf_Forum') and name='Locked')
begin
	exec('update yaf_Forum set Flags = Flags | 1 where Locked<>0')
	alter table dbo.yaf_Forum drop column Locked
end
GO

if exists(select 1 from syscolumns where id=object_id('yaf_Forum') and name='Hidden')
begin
	exec('update yaf_Forum set Flags = Flags | 2 where Hidden<>0')
	alter table dbo.yaf_Forum drop column Hidden
end
GO

if exists(select 1 from syscolumns where id=object_id('yaf_Forum') and name='IsTest')
begin
	exec('update yaf_Forum set Flags = Flags | 4 where IsTest<>0')
	alter table dbo.yaf_Forum drop column IsTest
end
GO

if exists(select 1 from syscolumns where id=object_id('yaf_Forum') and name='Moderated')
begin
	exec('update yaf_Forum set Flags = Flags | 8 where Moderated<>0')
	alter table dbo.yaf_Forum drop column Moderated
end
GO

if not exists(select 1 from syscolumns where id=object_id('yaf_Group') and name='Flags')
begin
	alter table dbo.yaf_Group add Flags int not null constraint DF_yaf_Group_Flags default (0)
end
GO

if exists(select 1 from syscolumns where id=object_id('yaf_Group') and name='IsAdmin')
begin
	exec('update yaf_Group set Flags = Flags | 1 where IsAdmin<>0')
	alter table dbo.yaf_Group drop column IsAdmin
end
GO

if exists(select 1 from syscolumns where id=object_id('yaf_Group') and name='IsGuest')
begin
	exec('update yaf_Group set Flags = Flags | 2 where IsGuest<>0')
	alter table dbo.yaf_Group drop column IsGuest
end
GO

if exists(select 1 from syscolumns where id=object_id('yaf_Group') and name='IsStart')
begin
	exec('update yaf_Group set Flags = Flags | 4 where IsStart<>0')
	alter table dbo.yaf_Group drop column IsStart
end
GO

if exists(select 1 from syscolumns where id=object_id('yaf_Group') and name='IsModerator')
begin
	exec('update yaf_Group set Flags = Flags | 8 where IsModerator<>0')
	alter table dbo.yaf_Group drop column IsModerator
end
GO

if not exists(select 1 from syscolumns where id=object_id('yaf_AccessMask') and name='Flags')
begin
	alter table dbo.yaf_AccessMask add Flags int not null constraint DF_yaf_AccessMask_Flags default (0)
end
GO

if exists(select 1 from syscolumns where id=object_id('yaf_AccessMask') and name='ReadAccess')
begin
	exec('update yaf_AccessMask set Flags = Flags | 1 where ReadAccess<>0')
	alter table dbo.yaf_AccessMask drop column ReadAccess
end
GO

if exists(select 1 from syscolumns where id=object_id('yaf_AccessMask') and name='PostAccess')
begin
	exec('update yaf_AccessMask set Flags = Flags | 2 where PostAccess<>0')
	alter table dbo.yaf_AccessMask drop column PostAccess
end
GO

if exists(select 1 from syscolumns where id=object_id('yaf_AccessMask') and name='ReplyAccess')
begin
	exec('update yaf_AccessMask set Flags = Flags | 4 where ReplyAccess<>0')
	alter table dbo.yaf_AccessMask drop column ReplyAccess
end
GO

if exists(select 1 from syscolumns where id=object_id('yaf_AccessMask') and name='PriorityAccess')
begin
	exec('update yaf_AccessMask set Flags = Flags | 8 where PriorityAccess<>0')
	alter table dbo.yaf_AccessMask drop column PriorityAccess
end
GO

if exists(select 1 from syscolumns where id=object_id('yaf_AccessMask') and name='PollAccess')
begin
	exec('update yaf_AccessMask set Flags = Flags | 16 where PollAccess<>0')
	alter table dbo.yaf_AccessMask drop column PollAccess
end
GO

if exists(select 1 from syscolumns where id=object_id('yaf_AccessMask') and name='VoteAccess')
begin
	exec('update yaf_AccessMask set Flags = Flags | 32 where VoteAccess<>0')
	alter table dbo.yaf_AccessMask drop column VoteAccess
end
GO

if exists(select 1 from syscolumns where id=object_id('yaf_AccessMask') and name='ModeratorAccess')
begin
	exec('update yaf_AccessMask set Flags = Flags | 64 where ModeratorAccess<>0')
	alter table dbo.yaf_AccessMask drop column ModeratorAccess
end
GO

if exists(select 1 from syscolumns where id=object_id('yaf_AccessMask') and name='EditAccess')
begin
	exec('update yaf_AccessMask set Flags = Flags | 128 where EditAccess<>0')
	alter table dbo.yaf_AccessMask drop column EditAccess
end
GO

if exists(select 1 from syscolumns where id=object_id('yaf_AccessMask') and name='DeleteAccess')
begin
	exec('update yaf_AccessMask set Flags = Flags | 256 where DeleteAccess<>0')
	alter table dbo.yaf_AccessMask drop column DeleteAccess
end
GO

if exists(select 1 from syscolumns where id=object_id('yaf_AccessMask') and name='UploadAccess')
begin
	exec('update yaf_AccessMask set Flags = Flags | 512 where UploadAccess<>0')
	alter table dbo.yaf_AccessMask drop column UploadAccess
end
GO

if exists(select 1 from syscolumns where id=object_id('yaf_Topic') and name='IsLocked')
begin
	grant update on yaf_Topic to public
	exec('update yaf_Topic set Flags = Flags | 1 where IsLocked<>0')
	revoke update on yaf_Topic from public
	alter table dbo.yaf_Topic drop column IsLocked
end
GO

if not exists(select 1 from syscolumns where id=object_id('yaf_User') and name='Flags')
begin
	alter table dbo.yaf_User add Flags int not null constraint DF_yaf_User_Flags default (0)
end
GO

if exists(select 1 from syscolumns where id=object_id('yaf_User') and name='IsHostAdmin')
begin
	grant update on yaf_User to public
	exec('update yaf_User set Flags = Flags | 1 where IsHostAdmin<>0')
	revoke update on yaf_User from public
	alter table dbo.yaf_User drop column IsHostAdmin
end
GO

if exists(select 1 from syscolumns where id=object_id('yaf_User') and name='Approved')
begin
	grant update on yaf_User to public
	exec('update yaf_User set Flags = Flags | 2 where Approved<>0')
	revoke update on yaf_User from public
	alter table dbo.yaf_User drop column Approved
end
GO

if not exists(select 1 from syscolumns where id=object_id('yaf_Rank') and name='Flags')
begin
	alter table dbo.yaf_Rank add Flags int not null constraint DF_yaf_Rank_Flags default (0)
end
GO

if exists(select 1 from syscolumns where id=object_id('yaf_Rank') and name='IsStart')
begin
	grant update on yaf_Rank to public
	exec('update yaf_Rank set Flags = Flags | 1 where IsStart<>0')
	revoke update on yaf_Rank from public
	alter table dbo.yaf_Rank drop column IsStart
end
GO

if exists(select 1 from syscolumns where id=object_id('yaf_Rank') and name='IsLadder')
begin
	grant update on yaf_Rank to public
	exec('update yaf_Rank set Flags = Flags | 2 where IsLadder<>0')
	revoke update on yaf_Rank from public
	alter table dbo.yaf_Rank drop column IsLadder
end
GO

/*
** Defaults
*/

if exists(select 1 from sysobjects where name=N'DF_yaf_Message_Flags' and parent_obj=object_id(N'yaf_Message'))
	alter table dbo.yaf_Message drop constraint DF_yaf_Message_Flags
GO

if not exists(select 1 from sysobjects where name=N'DF_yaf_Message_Flags' and parent_obj=object_id(N'yaf_Message'))
	alter table dbo.yaf_Message add constraint DF_yaf_Message_Flags default (23) for Flags
GO

/*
** Triggers
*/

if exists(select 1 from sysobjects where id=object_id(N'yaf_Active_insert') and objectproperty(id, N'IsTrigger') = 1)
	drop trigger yaf_Active_insert
go

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
go

if exists(select 1 from sysobjects where id=object_id(N'yaf_Forum_update') and objectproperty(id, N'IsTrigger') = 1)
	drop trigger yaf_Forum_update
go

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
go

/*
** Views
*/

if exists (select * from sysobjects where id = object_id(N'yaf_vaccess') and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view yaf_vaccess
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
			a.UserID,
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
			dbo.yaf_User a
		) as x
		join dbo.yaf_UserGroup a on a.UserID=x.UserID
		join dbo.yaf_Group b on b.GroupID=a.GroupID
	group by a.UserID,x.ForumID
GO

/*
** Primary keys
*/

if not exists(select 1 from sysindexes where id=object_id('yaf_BannedIP') and name='PK_BannedIP')
	alter table dbo.yaf_BannedIP with nocheck add constraint PK_BannedIP primary key clustered(ID)
GO

if not exists(select 1 from sysindexes where id=object_id('yaf_Category') and name='PK_Category')
	alter table dbo.yaf_Category with nocheck add constraint PK_Category primary key clustered(CategoryID)   
GO

if not exists(select 1 from sysindexes where id=object_id('yaf_CheckEmail') and name='PK_CheckEmail')
	alter table dbo.yaf_CheckEmail with nocheck add constraint PK_CheckEmail primary key clustered(CheckEmailID)   
GO

if not exists(select 1 from sysindexes where id=object_id('yaf_Choice') and name='PK_Choice')
	alter table dbo.yaf_Choice with nocheck add constraint PK_Choice primary key clustered(ChoiceID)   
GO

if not exists(select 1 from sysindexes where id=object_id('yaf_Forum') and name='PK_Forum')
	alter table dbo.yaf_Forum with nocheck add constraint PK_Forum primary key clustered(ForumID)   
GO

if not exists(select 1 from sysindexes where id=object_id('yaf_ForumAccess') and name='PK_ForumAccess')
	alter table dbo.yaf_ForumAccess with nocheck add constraint PK_ForumAccess primary key clustered(GroupID,ForumID)   
GO

if not exists(select 1 from sysindexes where id=object_id('yaf_Group') and name='PK_Group')
	alter table dbo.yaf_Group with nocheck add constraint PK_Group primary key clustered(GroupID)   
GO

if not exists(select 1 from sysindexes where id=object_id('yaf_Mail') and name='PK_Mail')
	alter table dbo.yaf_Mail with nocheck add constraint PK_Mail primary key clustered(MailID)   
GO

if not exists(select 1 from sysindexes where id=object_id('yaf_Message') and name='PK_Message')
	alter table dbo.yaf_Message with nocheck add constraint PK_Message primary key clustered(MessageID)   
GO

if not exists(select 1 from sysindexes where id=object_id('yaf_PMessage') and name='PK_PMessage')
	alter table dbo.yaf_PMessage with nocheck add constraint PK_PMessage primary key clustered(PMessageID)   
GO

if not exists(select 1 from sysindexes where id=object_id('yaf_Poll') and name='PK_Poll')
	alter table dbo.yaf_Poll with nocheck add constraint PK_Poll primary key clustered(PollID)   
GO

if not exists(select 1 from sysindexes where id=object_id('yaf_Smiley') and name='PK_Smiley')
	alter table dbo.yaf_Smiley with nocheck add constraint PK_Smiley primary key clustered(SmileyID)   
GO

if not exists(select 1 from sysindexes where id=object_id('yaf_Topic') and name='PK_Topic')
	alter table dbo.yaf_Topic with nocheck add constraint PK_Topic primary key clustered(TopicID)   
GO

if not exists(select 1 from sysindexes where id=object_id('yaf_User') and name='PK_User')
	alter table dbo.yaf_User with nocheck add constraint PK_User primary key clustered(UserID)   
GO

if not exists(select 1 from sysindexes where id=object_id('yaf_WatchForum') and name='PK_WatchForum')
	alter table dbo.yaf_WatchForum with nocheck add constraint PK_WatchForum primary key clustered(WatchForumID)   
GO

if not exists(select 1 from sysindexes where id=object_id('yaf_WatchTopic') and name='PK_WatchTopic')
	alter table dbo.yaf_WatchTopic with nocheck add constraint PK_WatchTopic primary key clustered(WatchTopicID)   
GO

if not exists(select 1 from sysindexes where id=object_id('yaf_UserGroup') and name='PK_UserGroup')
	alter table dbo.yaf_UserGroup with nocheck add constraint PK_UserGroup primary key clustered(UserID,GroupID)
GO

if not exists(select 1 from sysindexes where id=object_id('yaf_Rank') and name='PK_Rank')
	alter table dbo.yaf_Rank with nocheck add constraint PK_Rank primary key clustered(RankID)
GO

if not exists(select 1 from sysindexes where id=object_id('yaf_NntpServer') and name='PK_NntpServer')
	alter table dbo.yaf_NntpServer with nocheck add constraint PK_NntpServer primary key clustered (NntpServerID) 
GO

if not exists(select 1 from sysindexes where id=object_id('yaf_NntpForum') and name='PK_NntpForum')
	alter table dbo.yaf_NntpForum with nocheck add constraint PK_NntpForum primary key clustered (NntpForumID) 
GO

if not exists(select 1 from sysindexes where id=object_id('yaf_NntpTopic') and name='PK_NntpTopic')
	alter table dbo.yaf_NntpTopic with nocheck add constraint PK_NntpTopic primary key clustered (NntpTopicID) 
GO

if not exists(select * from sysindexes where id=object_id('yaf_AccessMask') and name='PK_AccessMask')
	alter table dbo.yaf_AccessMask with nocheck add constraint PK_AccessMask primary key clustered (AccessMaskID) 
GO

if not exists(select * from sysindexes where id=object_id('yaf_UserForum') and name='PK_UserForum')
	alter table dbo.yaf_UserForum with nocheck add constraint PK_UserForum primary key clustered (UserID,ForumID) 
GO

if not exists(select * from sysindexes where id=object_id('yaf_Board') and name='PK_Board')
	alter table dbo.yaf_Board with nocheck add constraint PK_Board primary key clustered (BoardID)
GO

if not exists(select * from sysindexes where id=object_id('yaf_Active') and name='PK_Active')
	alter table dbo.yaf_Active with nocheck add constraint PK_Active primary key clustered(SessionID,BoardID)
GO

if not exists(select * from sysindexes where id=object_id('yaf_UserPMessage') and name='PK_UserPMessage')
	alter table dbo.yaf_UserPMessage with nocheck add constraint PK_UserPMessage primary key clustered (UserPMessageID) 
GO

if not exists(select * from sysindexes where id=object_id('yaf_Attachment') and name='PK_Attachment')
	alter table dbo.yaf_Attachment with nocheck add constraint PK_Attachment primary key clustered (AttachmentID) 
GO

if not exists(select * from sysindexes where id=object_id('yaf_Active') and name='PK_Active')
	alter table dbo.yaf_Active with nocheck add constraint PK_Active primary key clustered(SessionID,BoardID)
GO

/*
** Unique constraints
*/

if not exists(select 1 from sysindexes where id=object_id('yaf_CheckEmail') and name='IX_CheckEmail')
	alter table dbo.yaf_CheckEmail add constraint IX_CheckEmail unique nonclustered (Hash)   
GO

if not exists(select 1 from sysindexes where id=object_id('yaf_Forum') and name='IX_Forum')
	alter table dbo.yaf_Forum add constraint IX_Forum unique nonclustered (CategoryID,Name)   
GO

if not exists(select 1 from sysindexes where id=object_id('yaf_WatchForum') and name='IX_WatchForum')
	alter table dbo.yaf_WatchForum add constraint IX_WatchForum unique nonclustered (ForumID,UserID)   
GO

if not exists(select 1 from sysindexes where id=object_id('yaf_WatchTopic') and name='IX_WatchTopic')
	alter table dbo.yaf_WatchTopic add constraint IX_WatchTopic unique nonclustered (TopicID,UserID)   
GO

if not exists(select * from sysindexes where id=object_id('yaf_Category') and name='IX_Category')
	alter table dbo.yaf_Category add constraint IX_Category unique nonclustered(BoardID,Name)
GO

if not exists(select * from sysindexes where id=object_id('yaf_Rank') and name='IX_Rank')
	alter table dbo.yaf_Rank add constraint IX_Rank unique(BoardID,Name)
GO

if not exists(select * from sysindexes where id=object_id('yaf_User') and name='IX_User')
	alter table dbo.yaf_User add constraint IX_User unique nonclustered(BoardID,Name)
GO

if not exists(select * from sysindexes where id=object_id('yaf_Group') and name='IX_Group')
	alter table dbo.yaf_Group add constraint IX_Group unique nonclustered(BoardID,Name)
GO

if not exists(select * from sysindexes where id=object_id('yaf_BannedIP') and name='IX_BannedIP')
	alter table dbo.yaf_BannedIP add constraint IX_BannedIP unique nonclustered(BoardID,Mask)
GO

if not exists(select * from sysindexes where id=object_id('yaf_Smiley') and name='IX_Smiley')
	alter table dbo.yaf_Smiley add constraint IX_Smiley unique nonclustered(BoardID,Code)
GO

if not exists(select * from sysindexes where id=object_id('yaf_BannedIP') and name='IX_BannedIP')
	alter table dbo.yaf_BannedIP add constraint IX_BannedIP unique nonclustered(BoardID,Mask)
GO

if not exists(select * from sysindexes where id=object_id('yaf_Category') and name='IX_Category')
	alter table dbo.yaf_Category add constraint IX_Category unique nonclustered(BoardID,Name)
GO

if not exists(select * from sysindexes where id=object_id('yaf_CheckEmail') and name='IX_CheckEmail')
	alter table dbo.yaf_CheckEmail add constraint IX_CheckEmail unique nonclustered(Hash)
GO

if not exists(select * from sysindexes where id=object_id('yaf_Forum') and name='IX_Forum')
	alter table dbo.yaf_Forum add constraint IX_Forum unique nonclustered(CategoryID,Name)   
GO

if not exists(select * from sysindexes where id=object_id('yaf_Group') and name='IX_Group')
	alter table dbo.yaf_Group add constraint IX_Group unique nonclustered(BoardID,Name)
GO

if not exists(select * from sysindexes where id=object_id('yaf_Rank') and name='IX_Rank')
	alter table dbo.yaf_Rank add constraint IX_Rank unique nonclustered(BoardID,Name)
GO

if not exists(select * from sysindexes where id=object_id('yaf_Smiley') and name='IX_Smiley')
	alter table dbo.yaf_Smiley add constraint IX_Smiley unique nonclustered(BoardID,Code)
GO

if not exists(select * from sysindexes where id=object_id('yaf_User') and name='IX_User')
	alter table dbo.yaf_User add constraint IX_User unique nonclustered(BoardID,Name)
GO

/*
** Foreign keys
*/

if not exists(select * from sysobjects where name='FK_Active_Forum' and parent_obj=object_id('yaf_Active') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_Active add constraint FK_Active_Forum foreign key (ForumID) references dbo.yaf_Forum (ForumID)
GO

if not exists(select * from sysobjects where name='FK_Active_Topic' and parent_obj=object_id('yaf_Active') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_Active add constraint FK_Active_Topic foreign key (TopicID) references dbo.yaf_Topic (TopicID)
GO

if not exists(select * from sysobjects where name='FK_Active_User' and parent_obj=object_id('yaf_Active') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_Active add constraint FK_Active_User foreign key (UserID) references dbo.yaf_User (UserID)
GO

if not exists(select * from sysobjects where name='FK_CheckEmail_User' and parent_obj=object_id('yaf_CheckEmail') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_CheckEmail add constraint FK_CheckEmail_User foreign key (UserID) references dbo.yaf_User (UserID)
GO

if not exists(select * from sysobjects where name='FK_Choice_Poll' and parent_obj=object_id('yaf_Choice') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_Choice add constraint FK_Choice_Poll foreign key (PollID) references dbo.yaf_Poll (PollID)
GO

if not exists(select * from sysobjects where name='FK_Forum_Category' and parent_obj=object_id('yaf_Forum') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_Forum add constraint FK_Forum_Category foreign key (CategoryID) references dbo.yaf_Category (CategoryID)
GO

if not exists(select * from sysobjects where name='FK_Forum_Message' and parent_obj=object_id('yaf_Forum') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_Forum add constraint FK_Forum_Message foreign key (LastMessageID) references dbo.yaf_Message (MessageID)
GO

if not exists(select * from sysobjects where name='FK_Forum_Topic' and parent_obj=object_id('yaf_Forum') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_Forum add constraint FK_Forum_Topic foreign key (LastTopicID) references dbo.yaf_Topic (TopicID)
GO

if not exists(select * from sysobjects where name='FK_Forum_User' and parent_obj=object_id('yaf_Forum') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_Forum add constraint FK_Forum_User foreign key (LastUserID) references dbo.yaf_User (UserID)
GO

if not exists(select * from sysobjects where name='FK_ForumAccess_Forum' and parent_obj=object_id('yaf_ForumAccess') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_ForumAccess add constraint FK_ForumAccess_Forum foreign key (ForumID) references dbo.yaf_Forum (ForumID)
GO

if not exists(select * from sysobjects where name='FK_ForumAccess_Group' and parent_obj=object_id('yaf_ForumAccess') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_ForumAccess add constraint FK_ForumAccess_Group foreign key (GroupID) references dbo.yaf_Group (GroupID)
GO

if not exists(select * from sysobjects where name='FK_Message_Topic' and parent_obj=object_id('yaf_Message') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_Message add constraint FK_Message_Topic foreign key (TopicID) references dbo.yaf_Topic (TopicID)
GO

if not exists(select * from sysobjects where name='FK_Message_User' and parent_obj=object_id('yaf_Message') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_Message add constraint FK_Message_User foreign key (UserID) references dbo.yaf_User (UserID)
GO

if not exists(select * from sysobjects where name='FK_PMessage_User1' and parent_obj=object_id('yaf_PMessage') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_PMessage add constraint FK_PMessage_User1 foreign key (FromUserID) references dbo.yaf_User (UserID)
GO

if exists(select * from sysobjects where name='FK_Topic_Forum' and parent_obj=object_id('yaf_Topic') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_Topic drop constraint FK_Topic_Forum
GO

if not exists(select * from sysobjects where name='FK_Topic_Forum' and parent_obj=object_id('yaf_Topic') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_Topic add constraint FK_Topic_Forum foreign key (ForumID) references dbo.yaf_Forum (ForumID) ON DELETE CASCADE
GO

if not exists(select * from sysobjects where name='FK_Topic_Message' and parent_obj=object_id('yaf_Topic') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_Topic add constraint FK_Topic_Message foreign key (LastMessageID) references dbo.yaf_Message (MessageID)
GO

if not exists(select * from sysobjects where name='FK_Topic_Poll' and parent_obj=object_id('yaf_Topic') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_Topic add constraint FK_Topic_Poll foreign key (PollID) references dbo.yaf_Poll (PollID)
GO

if not exists(select * from sysobjects where name='FK_Topic_Topic' and parent_obj=object_id('yaf_Topic') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_Topic add constraint FK_Topic_Topic foreign key (TopicMovedID) references dbo.yaf_Topic (TopicID)
GO

if not exists(select * from sysobjects where name='FK_Topic_User' and parent_obj=object_id('yaf_Topic') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_Topic add constraint FK_Topic_User foreign key (UserID) references dbo.yaf_User (UserID)
GO

if not exists(select * from sysobjects where name='FK_Topic_User2' and parent_obj=object_id('yaf_Topic') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_Topic add constraint FK_Topic_User2 foreign key (LastUserID) references dbo.yaf_User (UserID)
GO

if not exists(select * from sysobjects where name='FK_WatchForum_Forum' and parent_obj=object_id('yaf_WatchForum') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_WatchForum add constraint FK_WatchForum_Forum foreign key (ForumID) references dbo.yaf_Forum(ForumID)
GO

if not exists(select * from sysobjects where name='FK_WatchForum_User' and parent_obj=object_id('yaf_WatchForum') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_WatchForum add constraint FK_WatchForum_User foreign key (UserID) references dbo.yaf_User(UserID)
GO

if not exists(select * from sysobjects where name='FK_WatchTopic_Topic' and parent_obj=object_id('yaf_WatchTopic') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_WatchTopic add constraint FK_WatchTopic_Topic foreign key (TopicID) references dbo.yaf_Topic(TopicID)
GO

if not exists(select * from sysobjects where name='FK_WatchTopic_User' and parent_obj=object_id('yaf_WatchTopic') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_WatchTopic add constraint FK_WatchTopic_User foreign key (UserID) references dbo.yaf_User(UserID)
GO

if not exists(select * from sysobjects where name='FK_Active_Forum' and parent_obj=object_id('yaf_Active') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_Attachment add constraint FK_Attachment_Message foreign key (MessageID) references yaf_Message (MessageID)
GO

if not exists(select * from sysobjects where name='FK_UserGroup_User' and parent_obj=object_id('yaf_UserGroup') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_UserGroup add constraint FK_UserGroup_User foreign key (UserID) references yaf_User(UserID)
GO

if not exists(select * from sysobjects where name='FK_UserGroup_Group' and parent_obj=object_id('yaf_UserGroup') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_UserGroup add constraint FK_UserGroup_Group foreign key(GroupID) references yaf_Group (GroupID)
GO

if not exists(select * from sysobjects where name='FK_Attachment_Message' and parent_obj=object_id('yaf_Attachment') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_Attachment add constraint FK_Attachment_Message foreign key (MessageID) references yaf_Message (MessageID)
GO

if not exists(select * from sysobjects where name='FK_NntpForum_NntpServer' and parent_obj=object_id('yaf_NntpForum') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_NntpForum add constraint FK_NntpForum_NntpServer foreign key (NntpServerID) references yaf_NntpServer(NntpServerID)
GO

if not exists(select * from sysobjects where name='FK_NntpForum_Forum' and parent_obj=object_id('yaf_NntpForum') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_NntpForum add constraint FK_NntpForum_Forum foreign key (ForumID) references yaf_Forum(ForumID)
GO

if not exists(select * from sysobjects where name='FK_NntpTopic_NntpForum' and parent_obj=object_id('yaf_NntpTopic') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_NntpTopic add constraint FK_NntpTopic_NntpForum foreign key (NntpForumID) references yaf_NntpForum(NntpForumID)
GO

if not exists(select * from sysobjects where name='FK_NntpTopic_Topic' and parent_obj=object_id('yaf_NntpTopic') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_NntpTopic add constraint FK_NntpTopic_Topic foreign key (TopicID) references yaf_Topic(TopicID)
GO

if not exists(select * from sysobjects where name='FK_ForumAccess_AccessMask' and parent_obj=object_id('yaf_ForumAccess') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_ForumAccess add constraint FK_ForumAccess_AccessMask foreign key (AccessMaskID) references yaf_AccessMask (AccessMaskID)
GO

if not exists(select * from sysobjects where name='FK_UserForum_User' and parent_obj=object_id('yaf_UserForum') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_UserForum add constraint FK_UserForum_User foreign key (UserID) references yaf_User (UserID)
GO

if not exists(select * from sysobjects where name='FK_UserForum_Forum' and parent_obj=object_id('yaf_UserForum') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_UserForum add constraint FK_UserForum_Forum foreign key (ForumID) references yaf_Forum (ForumID)
GO

if not exists(select * from sysobjects where name='FK_UserForum_AccessMask' and parent_obj=object_id('yaf_UserForum') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_UserForum add constraint FK_UserForum_AccessMask foreign key (AccessMaskID) references yaf_AccessMask (AccessMaskID)
GO

if not exists(select * from sysobjects where name='FK_Category_Board' and parent_obj=object_id('yaf_Category') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_Category add constraint FK_Category_Board foreign key(BoardID) references yaf_Board (BoardID)
GO

if not exists(select * from sysobjects where name='FK_AccessMask_Board' and parent_obj=object_id('yaf_AccessMask') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_AccessMask add constraint FK_AccessMask_Board foreign key(BoardID) references yaf_Board (BoardID)
GO

if not exists(select * from sysobjects where name='FK_Active_Board' and parent_obj=object_id('yaf_Active') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_Active add constraint FK_Active_Board foreign key(BoardID) references yaf_Board (BoardID)
GO

if not exists(select * from sysobjects where name='FK_BannedIP_Board' and parent_obj=object_id('yaf_BannedIP') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_BannedIP add constraint FK_BannedIP_Board foreign key(BoardID) references yaf_Board (BoardID)
GO

if not exists(select * from sysobjects where name='FK_Group_Board' and parent_obj=object_id('yaf_Group') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_Group add constraint FK_Group_Board foreign key(BoardID) references yaf_Board (BoardID)
GO

if not exists(select * from sysobjects where name='FK_NntpServer_Board' and parent_obj=object_id('yaf_NntpServer') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_NntpServer add constraint FK_NntpServer_Board foreign key(BoardID) references yaf_Board (BoardID)
GO

if not exists(select * from sysobjects where name='FK_Rank_Board' and parent_obj=object_id('yaf_Rank') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_Rank add constraint FK_Rank_Board foreign key(BoardID) references yaf_Board (BoardID)
GO

if not exists(select * from sysobjects where name='FK_Smiley_Board' and parent_obj=object_id('yaf_Smiley') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_Smiley add constraint FK_Smiley_Board foreign key(BoardID) references yaf_Board (BoardID)
GO

if not exists(select * from sysobjects where name='FK_User_Rank' and parent_obj=object_id('yaf_User') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_User add constraint FK_User_Rank foreign key(RankID) references yaf_Rank(RankID)
GO

if not exists(select * from sysobjects where name='FK_User_Board' and parent_obj=object_id('yaf_User') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_User add constraint FK_User_Board foreign key(BoardID) references yaf_Board(BoardID)
GO

if not exists(select * from sysobjects where name='FK_Forum_Forum' and parent_obj=object_id('yaf_Forum') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_Forum add constraint FK_Forum_Forum foreign key(ParentID) references yaf_Forum(ForumID)
GO

if not exists(select * from sysobjects where name='FK_Message_Message' and parent_obj=object_id('yaf_Message') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_Message add constraint FK_Message_Message foreign key(ReplyTo) references yaf_Message(MessageID)
GO

if not exists(select * from sysobjects where name='FK_UserPMessage_User' and parent_obj=object_id('yaf_UserPMessage') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_UserPMessage add constraint FK_UserPMessage_User foreign key (UserID) references yaf_User (UserID)
GO

if not exists(select * from sysobjects where name='FK_UserPMessage_PMessage' and parent_obj=object_id('yaf_UserPMessage') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_UserPMessage add constraint FK_UserPMessage_PMessage foreign key (PMessageID) references yaf_PMessage (PMessageID)
GO

if not exists(select * from sysobjects where name='FK_Registry_Board' and parent_obj=object_id('yaf_Registry') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table dbo.yaf_Registry add constraint FK_Registry_Board foreign key(BoardID) references yaf_Board(BoardID) on delete cascade
go

/*
** Indexes
*/

if exists(select 1 from dbo.sysindexes where name=N'IX_Name' and id=object_id(N'yaf_Registry'))
	drop index dbo.yaf_Registry.IX_Name
go

if not exists(select 1 from dbo.sysindexes where name=N'IX_Name' and id=object_id(N'yaf_Registry'))
	create unique index IX_Name on dbo.yaf_Registry(BoardID,Name)
go

/*
** Stored procedures
*/

-- yaf_pmessage_info
if exists (select * from sysobjects where id = object_id(N'yaf_pmessage_info') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_pmessage_info
GO

create procedure dbo.yaf_pmessage_info as
begin
	select
		NumRead	= (select count(1) from yaf_UserPMessage where IsRead<>0),
		NumUnread = (select count(1) from yaf_UserPMessage where IsRead=0),
		NumTotal = (select count(1) from yaf_UserPMessage)
end
GO

-- yaf_pmessage_prune

if exists (select * from sysobjects where id = object_id(N'yaf_pmessage_prune') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_pmessage_prune
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

-- yaf_message_getReplies
if exists (select * from sysobjects where id = object_id(N'yaf_message_getReplies') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_message_getReplies
GO

create procedure dbo.yaf_message_getReplies(@MessageID int) as
begin
	select MessageID from yaf_Message where ReplyTo = @MessageID
end
GO

-- yaf_pmessage_delete
if exists (select * from sysobjects where id = object_id(N'yaf_pmessage_delete') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_pmessage_delete
GO

create procedure dbo.yaf_pmessage_delete(@PMessageID int) as
begin
	delete from yaf_PMessage where PMessageID=@PMessageID
end
GO

-- yaf_userpmessage_delete
if exists (select * from sysobjects where id = object_id(N'yaf_userpmessage_delete') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_userpmessage_delete
GO

create procedure dbo.yaf_userpmessage_delete(@UserPMessageID int) as
begin
	delete from yaf_UserPMessage where UserPMessageID=@UserPMessageID
end
GO

-- yaf_pmessage_list
if exists (select * from sysobjects where id = object_id(N'yaf_pmessage_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_pmessage_list
GO

create procedure dbo.yaf_pmessage_list(@FromUserID int=null,@ToUserID int=null,@PMessageID int=null) as
begin
	if @PMessageID is null begin
		select
			a.*,
			FromUser = b.Name,
			ToUserID = c.UserID,
			ToUser = c.Name,
			d.IsRead,
			d.UserPMessageID
		from
			yaf_PMessage a,
			yaf_User b,
			yaf_User c,
			yaf_UserPMessage d
		where
			b.UserID = a.FromUserID and
			c.UserID = d.UserID and
			d.PMessageID = a.PMessageID and
			((@ToUserID is not null and d.UserID = @ToUserID) or (@FromUserID is not null and a.FromUserID = @FromUserID))
		order by
			Created desc
	end
	else begin
		select
			a.*,
			FromUser = b.Name,
			ToUserID = c.UserID,
			ToUser = c.Name,
			d.IsRead,
			d.UserPMessageID
		from
			yaf_PMessage a,
			yaf_User b,
			yaf_User c,
			yaf_UserPMessage d
		where
			b.UserID = a.FromUserID and
			c.UserID = d.UserID and
			d.PMessageID = a.PMessageID and
			a.PMessageID = @PMessageID and
			c.UserID = @FromUserID
		order by
			Created desc
	end
end
GO

-- yaf_userpmessage_list
if exists (select * from sysobjects where id = object_id(N'yaf_userpmessage_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_userpmessage_list
GO

create procedure dbo.yaf_userpmessage_list(@UserPMessageID int) as
begin
	select
		a.*,
		FromUser = b.Name,
		ToUserID = c.UserID,
		ToUser = c.Name,
		d.IsRead,
		d.UserPMessageID
	from
		yaf_PMessage a,
		yaf_User b,
		yaf_User c,
		yaf_UserPMessage d
	where
		b.UserID = a.FromUserID and
		c.UserID = d.UserID and
		d.PMessageID = a.PMessageID and
		d.UserPMessageID = @UserPMessageID
end
GO

-- yaf_forum_delete
if exists (select * from sysobjects where id = object_id(N'yaf_forum_delete') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_forum_delete
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

-- yaf_user_list
if exists (select * from sysobjects where id = object_id(N'yaf_user_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_user_list
GO

create procedure dbo.yaf_user_list(@BoardID int,@UserID int=null,@Approved bit=null,@GroupID int=null,@RankID int=null) as
begin
	if @UserID is not null
		select 
			a.*,
			a.NumPosts,
			b.RankID,
			RankName = b.Name,
			NumDays = datediff(d,a.Joined,getdate())+1,
			NumPostsForum = (select count(1) from yaf_Message x where (x.Flags & 24)=16),
			HasAvatarImage = (select count(1) from yaf_User x where x.UserID=a.UserID and AvatarImage is not null),
			IsAdmin	= IsNull(c.IsAdmin,0),
			IsGuest	= IsNull(c.IsGuest,0),
			IsForumModerator	= IsNull(c.IsForumModerator,0),
			IsModerator		= IsNull(c.IsModerator,0)
		from 
			yaf_User a
			join yaf_Rank b on b.RankID=a.RankID
			left join yaf_vaccess c on c.UserID=a.UserID
		where 
			a.UserID = @UserID and
			a.BoardID = @BoardID and
			IsNull(c.ForumID,0) = 0 and
			(@Approved is null or (@Approved=0 and (a.Flags & 2)=0) or (@Approved=1 and (a.Flags & 2)=2))
		order by 
			a.Name
	else if @GroupID is null and @RankID is null
		select 
			a.*,
			a.NumPosts,
			IsAdmin = (select count(1) from yaf_UserGroup x,yaf_Group y where x.UserID=a.UserID and y.GroupID=x.GroupID and (y.Flags & 1)<>0),
			b.RankID,
			RankName = b.Name
		from 
			yaf_User a
			join yaf_Rank b on b.RankID=a.RankID
		where 
			a.BoardID = @BoardID and
			(@Approved is null or (@Approved=0 and (a.Flags & 2)=0) or (@Approved=1 and (a.Flags & 2)=2))
		order by 
			a.Name
	else
		select 
			a.*,
			a.NumPosts,
			IsAdmin = (select count(1) from yaf_UserGroup x,yaf_Group y where x.UserID=a.UserID and y.GroupID=x.GroupID and (y.Flags & 1)<>0),
			b.RankID,
			RankName = b.Name
		from 
			yaf_User a
			join yaf_Rank b on b.RankID=a.RankID
		where 
			a.BoardID = @BoardID and
			(@Approved is null or (@Approved=0 and (a.Flags & 2)=0) or (@Approved=1 and (a.Flags & 2)=2)) and
			(@GroupID is null or exists(select 1 from yaf_UserGroup x where x.UserID=a.UserID and x.GroupID=@GroupID)) and
			(@RankID is null or a.RankID=@RankID)
		order by 
			a.Name
end
GO

-- yaf_forum_listallmymoderated ABOT NEW 16.04.04
if exists (select * from sysobjects where id = object_id(N'yaf_forum_listallmymoderated') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_forum_listallmymoderated
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
-- END ABOT NEW 16.04.04
-- yaf_forum_listall
if exists (select * from sysobjects where id = object_id(N'yaf_forum_listall') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_forum_listall
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

-- yaf_poll_save
if exists (select * from sysobjects where id = object_id(N'yaf_poll_save') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_poll_save
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

-- yaf_mail_createwatch
if exists (select * from sysobjects where id = object_id(N'yaf_mail_createwatch') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_mail_createwatch
GO

create procedure dbo.yaf_mail_createwatch(@TopicID int,@From nvarchar(50),@Subject nvarchar(100),@Body ntext,@UserID int) as begin
	insert into yaf_Mail(FromUser,ToUser,Created,Subject,Body)
	select
		@From,
		b.Email,
		getdate(),
		@Subject,
		@Body
	from
		yaf_WatchTopic a,
		yaf_User b
	where
		b.UserID <> @UserID and
		b.UserID = a.UserID and
		a.TopicID = @TopicID and
		(a.LastMail is null or a.LastMail < b.LastVisit)
	
	insert into yaf_Mail(FromUser,ToUser,Created,Subject,Body)
	select
		@From,
		b.Email,
		getdate(),
		@Subject,
		@Body
	from
		yaf_WatchForum a,
		yaf_User b,
		yaf_Topic c
	where
		b.UserID <> @UserID and
		b.UserID = a.UserID and
		c.TopicID = @TopicID and
		c.ForumID = a.ForumID and
		(a.LastMail is null or a.LastMail < b.LastVisit) and
		not exists(select 1 from yaf_WatchTopic x where x.UserID=b.UserID and x.TopicID=c.TopicID)

	update yaf_WatchTopic set LastMail = getdate() 
	where TopicID = @TopicID
	and UserID <> @UserID
	
	update yaf_WatchForum set LastMail = getdate() 
	where ForumID = (select ForumID from yaf_Topic where TopicID = @TopicID)
	and UserID <> @UserID
end
GO

-- yaf_checkemail_save
if exists (select * from sysobjects where id = object_id(N'yaf_checkemail_save') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_checkemail_save
GO

create procedure dbo.yaf_checkemail_save(@UserID int,@Hash nvarchar(32),@Email nvarchar(50)) as
begin
	insert into yaf_CheckEmail(UserID,Email,Created,Hash)
	values(@UserID,@Email,getdate(),@Hash)	
end
GO

-- yaf_checkemail_update
if exists (select * from sysobjects where id = object_id(N'yaf_checkemail_update') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_checkemail_update
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
	update yaf_User set Email = @Email, Flags = Flags | 2 where UserID = @UserID
	delete yaf_CheckEmail where CheckEmailID = @CheckEmailID
	select convert(bit,1)
end
GO

-- yaf_message_searchphrase
if exists (select * from sysobjects where id = object_id(N'yaf_message_searchphrase') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_message_searchphrase
GO

-- yaf_user_changepassword
if exists (select * from sysobjects where id = object_id(N'yaf_user_changepassword') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_user_changepassword
GO

create procedure dbo.yaf_user_changepassword(@UserID int,@OldPassword nvarchar(32),@NewPassword nvarchar(32)) as
begin
	declare @CurrentOld nvarchar(32)
	select @CurrentOld = Password from yaf_User where UserID = @UserID
	if @CurrentOld<>@OldPassword begin
		select Success = convert(bit,0)
		return
	end
	update yaf_User set Password = @NewPassword where UserID = @UserID
	select Success = convert(bit,1)
end
GO

-- yaf_user_recoverpassword
if exists (select * from sysobjects where id = object_id(N'yaf_user_recoverpassword') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_user_recoverpassword
GO

create procedure dbo.yaf_user_recoverpassword(@BoardID int,@UserName nvarchar(50),@Email nvarchar(50),@Password nvarchar(32)) as
begin
	declare @UserID int
	select @UserID = UserID from yaf_User where BoardID = @BoardID and Name = @UserName and Email = @Email
	if @UserID is null begin
		select Success = convert(bit,0)
		return
	end
	update yaf_User set Password = @Password where UserID = @UserID
	if @@rowcount<>1
	begin
		select Success = convert(bit,0)
		return
	end
	select Success = convert(bit,1)
end
GO

-- yaf_message_approve
if exists (select * from sysobjects where id = object_id(N'yaf_message_approve') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_message_approve
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

	-- update yaf_User
	if exists(select 1 from yaf_Forum where ForumID=@ForumID and (Flags & 4)=0)
	begin
		update yaf_User set NumPosts = NumPosts + 1 where UserID = @UserID
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

-- yaf_user_approve
if exists (select * from sysobjects where id = object_id(N'yaf_user_approve') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_user_approve
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
	update yaf_User set Email = @Email, Flags = Flags | 2 where UserID = @UserID
	delete yaf_CheckEmail where CheckEmailID = @CheckEmailID
	select convert(bit,1)
end
GO

-- yaf_attachment_save
if exists (select * from sysobjects where id = object_id(N'yaf_attachment_save') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_attachment_save
GO

create procedure dbo.yaf_attachment_save(@MessageID int,@FileName nvarchar(50),@Bytes int,@ContentType nvarchar(50)=null,@FileData image=null) as begin
	insert into yaf_Attachment(MessageID,FileName,Bytes,ContentType,Downloads,FileData) values(@MessageID,@FileName,@Bytes,@ContentType,0,@FileData)
end
GO

-- yaf_category_save
if exists (select * from sysobjects where id = object_id(N'yaf_category_save') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_category_save
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

-- yaf_accessmask_save
if exists (select * from sysobjects where id = object_id(N'yaf_accessmask_save') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_accessmask_save
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

-- yaf_group_save
if exists (select * from sysobjects where id = object_id(N'yaf_group_save') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_group_save
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

-- yaf_bannedip_save
if exists (select * from sysobjects where id = object_id(N'yaf_bannedip_save') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_bannedip_save
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

-- yaf_nntpserver_save
if exists (select * from sysobjects where id = object_id(N'yaf_nntpserver_save') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_nntpserver_save
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

-- yaf_smiley_save
if exists (select * from sysobjects where id = object_id(N'yaf_smiley_save') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_smiley_save
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

-- yaf_user_adminsave
if exists (select * from sysobjects where id = object_id(N'yaf_user_adminsave') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_user_adminsave
GO

create procedure dbo.yaf_user_adminsave(@BoardID int,@UserID int,@Name nvarchar(50),@Email nvarchar(50),@IsHostAdmin bit,@RankID int) as
begin
	if @IsHostAdmin<>0
		update yaf_User set Flags = Flags | 1 where UserID = @UserID
	else
		update yaf_User set Flags = Flags & ~1 where UserID = @UserID

	update yaf_User set
		Name = @Name,
		Email = @Email,
		RankID = @RankID
	where UserID = @UserID
	select UserID = @UserID
end
GO

-- yaf_board_save
if exists (select * from sysobjects where id = object_id(N'yaf_board_save') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_board_save
GO

create procedure dbo.yaf_board_save(@BoardID int,@Name nvarchar(50),@AllowThreaded bit) as
begin
	update yaf_Board set
		Name = @Name,
		AllowThreaded = @AllowThreaded
	where BoardID=@BoardID
end
GO

-- yaf_rank_save
if exists (select * from sysobjects where id = object_id(N'yaf_rank_save') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_rank_save
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

-- yaf_message_save
if exists (select * from sysobjects where id = object_id(N'yaf_message_save') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_message_save
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

-- yaf_topic_save
if exists (select * from sysobjects where id = object_id(N'yaf_topic_save') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_topic_save
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

-- yaf_user_login
if exists (select * from sysobjects where id = object_id(N'yaf_user_login') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_user_login
GO

create procedure dbo.yaf_user_login(@BoardID int,@Name nvarchar(50),@Password nvarchar(32)) as
begin
	declare @UserID int

	if not exists(select UserID from yaf_User where Name=@Name and Password=@Password and (BoardID=@BoardID or (Flags & 3)=3))
		set @UserID=null
	else
		select 
			@UserID=UserID 
		from 
			yaf_User 
		where 
			Name=@Name and 
			Password=@Password and 
			(BoardID=@BoardID or (Flags & 1)=1) and
			(Flags & 2)=2

	select @UserID
end
GO

-- yaf_user_save
if exists (select * from sysobjects where id = object_id(N'yaf_user_save') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_user_save
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
		
		insert into yaf_User(BoardID,RankID,Name,Password,Email,Joined,LastVisit,NumPosts,Location,HomePage,TimeZone,Avatar,Gender,Flags) 
		values(@BoardID,@RankID,@UserName,@Password,@Email,getdate(),getdate(),0,@Location,@HomePage,@TimeZone,@Avatar,@Gender,@Flags)
	
		set @UserID = @@IDENTITY

		insert into yaf_UserGroup(UserID,GroupID) select @UserID,GroupID from yaf_Group where BoardID=@BoardID and (Flags & 4)<>0
		
		if @Hash is not null and @Hash <> '' and @Approved=0 begin
			insert into yaf_CheckEmail(UserID,Email,Created,Hash)
			values(@UserID,@Email,getdate(),@Hash)	
		end
	end
	else begin
		update yaf_User set
			Location = @Location,
			HomePage = @HomePage,
			TimeZone = @TimeZone,
			Avatar = @Avatar,
			LanguageFile = @LanguageFile,
			ThemeFile = @ThemeFile,
			MSN = @MSN,
			YIM = @YIM,
			AIM = @AIM,
			ICQ = @ICQ,
			RealName = @RealName,
			Occupation = @Occupation,
			Interests = @Interests,
			Gender = @Gender,
			Weblog = @Weblog
		where UserID = @UserID
		
		if @Email is not null
			update yaf_User set Email = @Email where UserID = @UserID
	end
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_replace_words_delete') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_replace_words_delete
GO

create procedure dbo.yaf_replace_words_delete(@ID int) as
begin
	delete from dbo.yaf_replace_words where ID = @ID
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_replace_words_edit') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_replace_words_edit
GO

create procedure dbo.yaf_replace_words_edit(@ID int=null) as
begin
	select * from yaf_replace_words where ID=@ID
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_replace_words_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_replace_words_list
GO

create procedure dbo.yaf_replace_words_list as begin
	select * from yaf_Replace_Words
end
GO

if exists (select * from dbo.sysobjects where id = object_id(N'yaf_replace_words_save') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_replace_words_save
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

/* subject editing added by Jaben Cargman */

-- yaf_message_update
if exists (select * from dbo.sysobjects where id = object_id(N'yaf_message_update') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_message_update
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



-- yaf_message_list
if exists (select * from sysobjects where id = object_id(N'yaf_message_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_message_list
GO

create procedure dbo.yaf_message_list(@MessageID int) as
begin
	select
		a.MessageID,
		a.UserID,
		UserName = b.Name,
		a.Message,
		c.TopicID,
		c.ForumID,
		c.Topic,
		c.Priority,
		a.Flags,
		c.UserID as TopicOwnerID
	from
		yaf_Message a,
		yaf_User b,
		yaf_Topic c
	where
		a.MessageID = @MessageID and
		b.UserID = a.UserID and
		c.TopicID = a.TopicID
end
GO

-- registry implementation by Jaben Cargman

-- yaf_registry_list
if exists (select * from sysobjects where id = object_id(N'yaf_registry_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_registry_list
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

-- yaf_registry_save
if exists (select * from sysobjects where id = object_id(N'yaf_registry_save') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_registry_save
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

-- yaf_system_initialize
if exists (select * from sysobjects where id = object_id(N'yaf_system_initialize') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_system_initialize
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

-- yaf_system_updateversion
if exists (select * from sysobjects where id = object_id(N'yaf_system_updateversion') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_system_updateversion
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

if exists (select * from sysobjects where id = object_id(N'yaf_system_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_system_list
GO

if exists (select * from sysobjects where id = object_id(N'yaf_system_save') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_system_save
GO

-- yaf_board_list
if exists (select * from sysobjects where id = object_id(N'yaf_board_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_board_list
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

-- yaf_board_create
if exists (select * from sysobjects where id = object_id(N'yaf_board_create') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_board_create
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

	-- yaf_User
	insert into yaf_User(BoardID,RankID,Name,Password,Joined,LastVisit,NumPosts,TimeZone,Email,Gender,Flags)
	values(@BoardID,@RankIDGuest,'Guest','na',getdate(),getdate(),0,@TimeZone,@ForumEmail,0,@UserFlags)
	set @UserIDGuest = @@IDENTITY	
	
	if @IsHostAdmin<>0 SET @UserFlags = 3
	
	insert into yaf_User(BoardID,RankID,Name,Password,Joined,LastVisit,NumPosts,TimeZone,Email,Gender,Flags)
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

create procedure dbo.yaf_system_upgrade_to_registry as
begin
	DECLARE @TimeZone			int
	DECLARE @SmtpServer			nvarchar(50)
	DECLARE @SmtpUserName		nvarchar(50)
	DECLARE @SmtpUserPass		nvarchar(50)
	DECLARE @ForumEmail			nvarchar(50)
	DECLARE @EmailVerification		bit
	DECLARE @ShowMoved		bit
	DECLARE @BlankLinks			bit
	DECLARE @ShowGroups		bit
	DECLARE @AvatarWidth		int
	DECLARE @AvatarHeight		int
	DECLARE @AvatarUpload		bit
	DECLARE @AvatarRemote		bit
	DECLARE @AvatarSize			int
	DECLARE @AllowRichEdit		bit
	DECLARE @AllowUserTheme		bit
	DECLARE @AllowUserLanguage		bit
	DECLARE @UseFileTable		bit
	DECLARE @MaxFileSize		int
	DECLARE @tmp			nvarchar(100)	

	select 	@TimeZone = TimeZone,
		@SmtpServer = SmtpServer,
		@SmtpUserName = SmtpUserName,
		@SmtpUserPass = SmtpUserPass,
		@ForumEmail = ForumEmail,
		@EmailVerification = EmailVerification,
		@ShowMoved = ShowMoved,
		@BlankLinks = BlankLinks,
		@ShowGroups = ShowGroups,
		@AvatarWidth = AvatarWidth,
		@AvatarHeight = AvatarHeight,
		@AvatarUpload = AvatarUpload,
		@AvatarRemote = AvatarRemote,
		@AvatarSize = AvatarSize,
		@AllowRichEdit = AllowRichEdit,
		@AllowUserTheme = AllowUserTheme,
		@AllowUserLanguage = AllowUserLanguage,
		@UseFileTable = UseFileTable,
		@MaxFileSize = MaxFileSize
	FROM yaf_System WHERE SystemID = 1

	-- put old settings into new registry table
	EXEC yaf_registry_save 'SmtpServer',@SmtpServer
	EXEC yaf_registry_save 'SmtpUserName',@SmtpUserName
	EXEC yaf_registry_save 'SmtpUserPass',@SmtpUserPass
	EXEC yaf_registry_save 'ForumEmail',@ForumEmail

	SET @tmp = CAST(@TimeZone AS nvarchar(100))
	EXEC yaf_registry_save 'TimeZone',@tmp
	SET @tmp = CAST(@AvatarWidth AS nvarchar(100))
	EXEC yaf_registry_save 'AvatarWidth',@AvatarWidth
	SET @tmp = CAST(@AvatarHeight AS nvarchar(100))
	EXEC yaf_registry_save 'AvatarHeight',@AvatarHeight
	SET @tmp = CAST(@AvatarSize AS nvarchar(100))
	EXEC yaf_registry_save 'AvatarSize',@AvatarSize
	SET @tmp = CAST(@MaxFileSize AS nvarchar(100))
	EXEC yaf_registry_save 'MaxFileSize',@MaxFileSize

	SET @tmp = CAST(@EmailVerification AS nvarchar(100))
	EXEC yaf_registry_save 'EmailVerification',@EmailVerification
	SET @tmp = CAST(@ShowMoved AS nvarchar(100))
	EXEC yaf_registry_save 'ShowMoved',@ShowMoved
	SET @tmp = CAST(@BlankLinks AS nvarchar(100))
	EXEC yaf_registry_save 'BlankLinks',@BlankLinks
	SET @tmp = CAST(@ShowGroups AS nvarchar(100))
	EXEC yaf_registry_save 'ShowGroups',@ShowGroups
	SET @tmp = CAST(@AvatarUpload AS nvarchar(100))
	EXEC yaf_registry_save 'AvatarUpload',@AvatarUpload
	SET @tmp = CAST(@AvatarRemote AS nvarchar(100))
	EXEC yaf_registry_save 'AvatarRemote',@AvatarRemote
	SET @tmp = CAST(@AllowUserTheme AS nvarchar(100))
	EXEC yaf_registry_save 'AllowUserTheme',@AllowUserTheme
	SET @tmp = CAST(@AllowUserLanguage AS nvarchar(100))
	EXEC yaf_registry_save 'AllowUserLanguage',@AllowUserLanguage
	SET @tmp = CAST(@UseFileTable AS nvarchar(100))
	EXEC yaf_registry_save 'UseFileTable',@UseFileTable

end
GO

grant execute on dbo.yaf_system_upgrade_to_registry to public
go

-- no longer require system table
if exists (select * from dbo.sysobjects where id = object_id(N'yaf_System') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
begin
	-- upgrade then delete
	exec('EXEC yaf_system_upgrade_to_registry')
	drop table yaf_System
end
GO

-- and upgrade procedure
if exists (select * from sysobjects where id = object_id(N'yaf_system_upgrade_to_registry') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_system_upgrade_to_registry
GO

if exists (select * from sysobjects where id = object_id(N'yaf_watchtopic_check') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_watchtopic_check
GO

create procedure dbo.yaf_watchtopic_check(@UserID int,@TopicID int) as
begin
	SELECT WatchTopicID FROM yaf_WatchTopic WHERE UserID = @UserID AND TopicID = @TopicID
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_watchforum_check') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_watchforum_check
GO

create procedure dbo.yaf_watchforum_check(@UserID int,@ForumID int) as
begin
	SELECT WatchForumID FROM yaf_WatchForum WHERE UserID = @UserID AND ForumID = @ForumID
end
GO

-- yaf_post_list
if exists (select * from sysobjects where id = object_id(N'yaf_post_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_post_list
GO

create procedure dbo.yaf_post_list(@TopicID int,@UpdateViewCount smallint=1) as
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
		UserName	= IsNull(a.UserName,b.Name),
		b.Joined,
		b.Avatar,
		b.Location,
		b.Signature,
		b.HomePage,
		b.Weblog,
		b.MSN,
		b.YIM,
		b.AIM,
		b.ICQ,
		Posts		= b.NumPosts,
		d.Views,
		d.ForumID,
		RankName = c.Name,
		c.RankImage,
		Edited = IsNull(a.Edited,a.Posted),
		HasAttachments	= (select count(1) from yaf_Attachment x where x.MessageID=a.MessageID),
		HasAvatarImage = (select count(1) from yaf_User x where x.UserID=b.UserID and AvatarImage is not null)
	from
		yaf_Message a
		join yaf_User b on b.UserID=a.UserID
		join yaf_Topic d on d.TopicID=a.TopicID
		join yaf_Forum g on g.ForumID=d.ForumID
		join yaf_Category h on h.CategoryID=g.CategoryID
		join yaf_Rank c on c.RankID=b.RankID
	where
		a.TopicID = @TopicID and
		(a.Flags & 24)=16
	order by
		a.Posted asc
end
GO

-- yaf_user_savesignature
if exists (select * from sysobjects where id = object_id(N'yaf_user_savesignature') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_user_savesignature
GO

create procedure dbo.yaf_user_savesignature(@UserID int,@Signature ntext) as
begin
	update yaf_User set Signature = @Signature where UserID = @UserID
end
GO

-- yaf_nntpforum_update
if exists (select * from sysobjects where id = object_id(N'yaf_nntpforum_update') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_nntpforum_update
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

-- yaf_nntptopic_savemessage
if exists (select * from sysobjects where id = object_id(N'yaf_nntptopic_savemessage') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_nntptopic_savemessage
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
		update yaf_User set NumPosts=NumPosts+1 where UserID=@UserID
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

-- yaf_forum_save
if exists (select * from sysobjects where id = object_id(N'yaf_forum_save') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_forum_save
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

-- yaf_message_delete
if exists (select * from sysobjects where id = object_id(N'yaf_message_delete') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_message_delete
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

if exists (select * from sysobjects where id = object_id(N'yaf_topic_updatelastpost') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_topic_updatelastpost
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

if exists (select * from sysobjects where id = object_id(N'yaf_forum_updatelastpost') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_forum_updatelastpost
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

-- yaf_topic_delete
if exists (select * from sysobjects where id = object_id(N'yaf_topic_delete') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_topic_delete
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

-- yaf_topic_listmessages
--ABOT NEW 16.04.04
if exists (select * from sysobjects where id = object_id(N'yaf_topic_listmessages') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_topic_listmessages
GO

create procedure dbo.yaf_topic_listmessages(@TopicID int) as
begin
	select * from yaf_Message
	where TopicID = @TopicID
end
GO
--END ABOT NEW 16.04.04

-- yaf_nntpforum_delete
if exists (select * from sysobjects where id = object_id(N'yaf_nntpforum_delete') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_nntpforum_delete
GO

create procedure dbo.yaf_nntpforum_delete(@NntpForumID int) as
begin
	delete from yaf_NntpTopic where NntpForumID = @NntpForumID
	delete from yaf_NntpForum where NntpForumID = @NntpForumID
end
GO

--ABOT NEW 16.04.04
-- yaf_forum_listSubForums
if exists (select * from sysobjects where id = object_id(N'yaf_forum_listSubForums') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_forum_listSubForums
GO

create procedure dbo.yaf_forum_listSubForums(@ForumID int) as
begin
	select Sum(1) from yaf_Forum where ParentID = @ForumID
end
GO
--END ABOT NEW 16.04.04
--ABOT NEW 16.04.04

-- yaf_forum_listtopics
if exists (select * from sysobjects where id = object_id(N'yaf_forum_listtopics') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_forum_listtopics
GO

create procedure dbo.yaf_forum_listtopics(@ForumID int) as
begin
select * from yaf_Topic
Where ForumID = @ForumID
end
GO
--END ABOT NEW 16.04.04

if exists(select 1 from sysobjects where id = object_id(N'yaf_forum_posts') and OBJECTPROPERTY(id, N'IsScalarFunction')=1)
	drop function dbo.yaf_forum_posts
go

create function dbo.yaf_forum_posts(@ForumID int) returns int as
begin
	declare @NumPosts int
	declare @tmp int

	select @NumPosts=NumPosts from dbo.yaf_Forum where ForumID=@ForumID

	if exists(select 1 from dbo.yaf_Forum where ParentID=@ForumID)
	begin
		declare c cursor for
		select ForumID from dbo.yaf_Forum
		where ParentID = @ForumID
		
		open c
		
		fetch next from c into @tmp
		while @@FETCH_STATUS = 0
		begin
			set @NumPosts=@NumPosts+dbo.yaf_forum_posts(@tmp)
			fetch next from c into @tmp
		end
		close c
		deallocate c
	end

	return @NumPosts
end
go

if exists(select 1 from sysobjects where id = object_id(N'yaf_bitset') and OBJECTPROPERTY(id, N'IsScalarFunction')=1)
	drop function dbo.yaf_bitset
go

create function dbo.yaf_bitset(@Flags int,@Mask int) returns bit as
begin
	declare @bool bit

	if (@Flags & @Mask) = @Mask
		set @bool = 1
	else
		set @bool = 0
		
	return @bool
end
go

if exists(select 1 from sysobjects where id = object_id(N'yaf_forum_topics') and OBJECTPROPERTY(id, N'IsScalarFunction')=1)
	drop function dbo.yaf_forum_topics
go

create function dbo.yaf_forum_topics(@ForumID int) returns int as
begin
	declare @NumTopics int
	declare @tmp int

	select @NumTopics=NumTopics from dbo.yaf_Forum where ForumID=@ForumID

	if exists(select 1 from dbo.yaf_Forum where ParentID=@ForumID)
	begin
		declare c cursor for
		select ForumID from dbo.yaf_Forum
		where ParentID = @ForumID
		
		open c
		
		fetch next from c into @tmp
		while @@FETCH_STATUS = 0
		begin
			set @NumTopics=@NumTopics+dbo.yaf_forum_topics(@tmp)
			fetch next from c into @tmp
		end
		close c
		deallocate c
	end

	return @NumTopics
end
go

-- yaf_forum_listread
if exists (select * from sysobjects where id = object_id(N'yaf_forum_listread') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_forum_listread
GO

create procedure dbo.yaf_forum_listread(@BoardID int,@UserID int,@CategoryID int=null,@ParentID int=null) as
begin
	select 
		a.CategoryID, 
		Category		= a.Name, 
		ForumID			= b.ForumID,
		Forum			= b.Name, 
		Description,
		Topics			= dbo.yaf_forum_topics(b.ForumID),
		Posts			= dbo.yaf_forum_posts(b.ForumID),
		LastPosted		= b.LastPosted,
		LastMessageID	= b.LastMessageID,
		LastUserID		= b.LastUserID,
		LastUser		= IsNull(b.LastUserName,(select Name from yaf_User x where x.UserID=b.LastUserID)),
		LastTopicID		= b.LastTopicID,
		LastTopicName	= (select x.Topic from yaf_Topic x where x.TopicID=b.LastTopicID),
		b.Flags,
		Viewing			= (select count(1) from yaf_Active x where x.ForumID=b.ForumID),
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

-- yaf_user_access
if exists (select * from sysobjects where id = object_id(N'yaf_user_access') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_user_access
GO

-- yaf_nntpforum_list
if exists (select * from sysobjects where id = object_id(N'yaf_nntpforum_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_nntpforum_list
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

-- yaf_nntpforum_save
if exists (select * from sysobjects where id = object_id(N'yaf_nntpforum_save') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_nntpforum_save
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

if exists (select * from sysobjects where id = object_id(N'yaf_bannedip_delete') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_bannedip_delete
GO

create procedure dbo.yaf_bannedip_delete(@ID int) as
begin
	delete from yaf_BannedIP where ID = @ID
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_choice_vote') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_choice_vote
GO

create procedure dbo.yaf_choice_vote(@ChoiceID int) as
begin
	update yaf_Choice set Votes = Votes + 1 where ChoiceID = @ChoiceID
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_forumaccess_group') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_forumaccess_group
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

if exists (select * from sysobjects where id = object_id(N'yaf_forumaccess_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_forumaccess_list
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

if exists (select * from sysobjects where id = object_id(N'yaf_mail_delete') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_mail_delete
GO

create procedure dbo.yaf_mail_delete(@MailID int) as
begin
	delete from yaf_Mail where MailID = @MailID
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_mail_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_mail_list
GO

create procedure dbo.yaf_mail_list as
begin
	select top 10 * from yaf_Mail order by Created
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_topic_findnext') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_topic_findnext
GO

create procedure dbo.yaf_topic_findnext(@TopicID int) as
begin
	declare @LastPosted datetime
	declare @ForumID int
	select @LastPosted = LastPosted, @ForumID = ForumID from yaf_Topic where TopicID = @TopicID
	select top 1 TopicID from yaf_Topic where LastPosted>@LastPosted and ForumID = @ForumID order by LastPosted asc
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_topic_findprev') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_topic_findprev
GO

create procedure dbo.yaf_topic_findprev(@TopicID int) as 
begin
	declare @LastPosted datetime
	declare @ForumID int
	select @LastPosted = LastPosted, @ForumID = ForumID from yaf_Topic where TopicID = @TopicID
	select top 1 TopicID from yaf_Topic where LastPosted<@LastPosted and ForumID = @ForumID order by LastPosted desc
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_topic_info') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_topic_info
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

if exists (select * from sysobjects where id = object_id(N'yaf_topic_lock') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_topic_lock
GO

create procedure dbo.yaf_topic_lock(@TopicID int,@Locked bit) as
begin
	if @Locked<>0
		update yaf_Topic set Flags = Flags | 1 where TopicID = @TopicID
	else
		update yaf_Topic set Flags = Flags & ~1 where TopicID = @TopicID
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_user_getsignature') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_user_getsignature
GO

create procedure dbo.yaf_user_getsignature(@UserID int) as
begin
	select Signature from yaf_User where UserID = @UserID
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_watchforum_add') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_watchforum_add
GO

create procedure dbo.yaf_watchforum_add(@UserID int,@ForumID int) as
begin
	insert into yaf_WatchForum(ForumID,UserID,Created)
	select @ForumID, @UserID, getdate()
	where not exists(select 1 from yaf_WatchForum where ForumID=@ForumID and UserID=@UserID)
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_watchforum_delete') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_watchforum_delete
GO

create procedure dbo.yaf_watchforum_delete(@WatchForumID int) as
begin
	delete from yaf_WatchForum where WatchForumID = @WatchForumID
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_watchtopic_delete') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_watchtopic_delete
GO

create procedure dbo.yaf_watchtopic_delete(@WatchTopicID int) as
begin
	delete from yaf_WatchTopic where WatchTopicID = @WatchTopicID
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_watchtopic_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_watchtopic_list
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
		LastUserName = IsNull(b.LastUserName,(select Name from yaf_User x where x.UserID=b.LastUserID))
	from
		yaf_WatchTopic a,
		yaf_Topic b
	where
		a.UserID = @UserID and
		b.TopicID = a.TopicID
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_poll_stats') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_poll_stats
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

if exists (select * from sysobjects where id = object_id(N'yaf_smiley_delete') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_smiley_delete
GO

create procedure dbo.yaf_smiley_delete(@SmileyID int=null) as begin
	if @SmileyID is not null
		delete from yaf_Smiley where SmileyID=@SmileyID
	else
		delete from yaf_Smiley
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_user_avatarimage') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_user_avatarimage
GO

create procedure dbo.yaf_user_avatarimage(@UserID int) as begin
	select UserID,AvatarImage from yaf_User where UserID=@UserID
end
GO

if exists(select * from sysobjects where name='FK_User_Avatar' and parent_obj=object_id('yaf_User') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	alter table yaf_User drop constraint FK_User_Avatar
GO

if exists(select * from syscolumns where id=object_id('yaf_User') and name='AvatarID')
	alter table yaf_User drop column AvatarID
GO

if exists (select * from sysobjects where id = object_id(N'yaf_Avatar') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table yaf_Avatar
GO

if exists (select * from sysobjects where id = object_id(N'yaf_rank_delete') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_rank_delete
GO

create procedure dbo.yaf_rank_delete(@RankID int) as begin
	delete from yaf_Rank where RankID = @RankID
end
GO

if not exists(select * from syscolumns where id=object_id('yaf_User') and name='RankID')
	alter table yaf_User add RankID int not null default(1)
GO

if not exists(select * from sysobjects where name='FK_User_Rank' and parent_obj=object_id('yaf_User') and OBJECTPROPERTY(id,N'IsForeignKey')=1)
	update yaf_User set RankID = (select RankID from yaf_Rank where (Flags & 2)<>0)
GO

if exists (select * from sysobjects where id = object_id(N'yaf_user_upgrade') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_user_upgrade
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
		@NumPosts = a.NumPosts
	from
		yaf_User a,
		yaf_Rank b
	where
		a.UserID = @UserID and
		b.RankID = a.RankID
	
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
		update yaf_User set RankID = @RankID where UserID = @UserID
end
GO

if exists(select * from syscolumns where id=object_id('yaf_Group') and name='IsLadder')
	alter table yaf_Group drop column IsLadder
GO

if exists(select * from syscolumns where id=object_id('yaf_Group') and name='MinPosts')
	alter table yaf_Group drop column MinPosts
GO

if exists(select * from syscolumns where id=object_id('yaf_Group') and name='RankImage')
	alter table yaf_Group drop column RankImage
GO

if exists (select * from sysobjects where id = object_id(N'yaf_watchtopic_add') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_watchtopic_add
GO

create procedure dbo.yaf_watchtopic_add(@UserID int,@TopicID int) as
begin
	insert into yaf_WatchTopic(TopicID,UserID,Created)
	select @TopicID, @UserID, getdate()
	where not exists(select 1 from yaf_WatchTopic where TopicID=@TopicID and UserID=@UserID)
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_forum_moderatelist') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_forum_moderatelist
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

if exists (select * from sysobjects where id = object_id(N'yaf_user_deleteavatar') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_user_deleteavatar
GO

create procedure dbo.yaf_user_deleteavatar(@UserID int) as begin
	update yaf_User set AvatarImage = null where UserID = @UserID
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_user_activity_rank') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_user_activity_rank
GO

create procedure dbo.yaf_user_activity_rank(@StartDate as datetime) AS
begin
	select top 3  ID, Name, NumOfPosts from yaf_User u inner join
	(
		select m.UserID as ID, Count(m.UserID) as NumOfPosts from yaf_Message m
		where m.Posted >= @StartDate
		group by m.UserID
	) as counter
	on u.UserID = counter.ID
	order by NumOfPosts desc
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_post_list_reverse10') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_post_list_reverse10
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
		UserName = IsNull(a.UserName,b.Name),
		b.Signature
	from
		yaf_Message a, 
		yaf_User b,
		yaf_Topic d
	where
		(a.Flags & 24)=16 and
		a.TopicID = @TopicID and
		b.UserID = a.UserID and
		d.TopicID = a.TopicID
	order by
		a.Posted desc
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_message_unapproved') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_message_unapproved
GO

create procedure dbo.yaf_message_unapproved(@ForumID int) as begin
	select
		MessageID	= b.MessageID,
		UserName	= IsNull(b.UserName,c.Name),
		Posted		= b.Posted,
		Topic		= a.Topic,
		Message		= b.Message
	from
		yaf_Topic a,
		yaf_Message b,
		yaf_User c
	where
		a.ForumID = @ForumID and
		b.TopicID = a.TopicID and
		(b.Flags & 16)=0 and
		c.UserID = b.UserID
	order by
		a.Posted
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_group_delete') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_group_delete
GO

create procedure dbo.yaf_group_delete(@GroupID int) as
begin
	delete from yaf_ForumAccess where GroupID = @GroupID
	delete from yaf_UserGroup where GroupID = @GroupID
	delete from yaf_Group where GroupID = @GroupID
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_nntptopic_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_nntptopic_list
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

if exists (select * from sysobjects where id = object_id(N'yaf_nntpserver_delete') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_nntpserver_delete
GO

create procedure dbo.yaf_nntpserver_delete(@NntpServerID int) as
begin
	delete from yaf_NntpTopic where NntpForumID in (select NntpForumID from yaf_NntpForum where NntpServerID = @NntpServerID)
	delete from yaf_NntpForum where NntpServerID = @NntpServerID
	delete from yaf_NntpServer where NntpServerID = @NntpServerID
end
GO

-- NNTP END

if not exists(select * from syscolumns where id=object_id('yaf_User') and name='Suspended')
	alter table yaf_User add Suspended datetime null
GO

if exists (select * from sysobjects where id = object_id(N'yaf_user_suspend') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_user_suspend
GO

create procedure dbo.yaf_user_suspend(@UserID int,@Suspend datetime=null) as
begin
	update yaf_User set Suspended = @Suspend where UserID=@UserID
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_watchforum_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_watchforum_list
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
		LastUserName = IsNull(b.LastUserName,(select Name from yaf_User x where x.UserID=b.LastUserID))
	from
		yaf_WatchForum a,
		yaf_Forum b
	where
		a.UserID = @UserID and
		b.ForumID = a.ForumID
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_attachment_delete') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_attachment_delete
GO

create procedure dbo.yaf_attachment_delete(@AttachmentID int) as begin
	delete from yaf_Attachment where AttachmentID=@AttachmentID
end
go

if exists (select * from sysobjects where id = object_id(N'yaf_user_guest') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_user_guest
GO

create procedure dbo.yaf_user_guest as
begin
	select top 1
		a.UserID
	from
		yaf_User a,
		yaf_UserGroup b,
		yaf_Group c
	where
		b.UserID = a.UserID and
		b.GroupID = c.GroupID and
		(c.Flags & 2)<>0
end
go

if exists (select * from sysobjects where id = object_id(N'yaf_topic_prune') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_topic_prune
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

if exists (select * from sysobjects where id = object_id(N'yaf_forum_updatestats') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_forum_updatestats
GO

create procedure dbo.yaf_forum_updatestats(@ForumID int) as
begin
	update yaf_Forum set 
		NumPosts = (select count(1) from yaf_Message x,yaf_Topic y where y.TopicID=x.TopicID and y.ForumID = yaf_Forum.ForumID and (x.Flags & 24)=16),
		NumTopics = (select count(distinct x.TopicID) from yaf_Topic x,yaf_Message y where x.ForumID=yaf_Forum.ForumID and y.TopicID=x.TopicID and (y.Flags & 24)=16)
	where ForumID=@ForumID
end
go

if exists (select * from sysobjects where id = object_id(N'yaf_category_delete') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_category_delete
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

if exists (select * from sysobjects where id = object_id(N'yaf_active_listforum') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_active_listforum
GO

create procedure dbo.yaf_active_listforum(@ForumID int) as
begin
	select
		UserID		= a.UserID,
		UserName	= b.Name
	from
		yaf_Active a join yaf_User b on b.UserID=a.UserID
	where
		a.ForumID = @ForumID
	group by
		a.UserID,
		b.Name
	order by
		b.Name
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_forum_moderators') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_forum_moderators
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

if exists (select * from sysobjects where id = object_id(N'yaf_accessmask_delete') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_accessmask_delete
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

if exists (select * from sysobjects where id = object_id(N'yaf_forumaccess_save') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_forumaccess_save
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

if exists (select * from sysobjects where id = object_id(N'yaf_userforum_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_userforum_list
GO

create procedure dbo.yaf_userforum_list(@UserID int=null,@ForumID int=null) as 
begin
	select 
		a.*,
		b.AccessMaskID,
		b.Accepted,
		Access = c.Name
	from
		yaf_User a join yaf_UserForum b on b.UserID=a.UserID
		join yaf_AccessMask c on c.AccessMaskID=b.AccessMaskID
	where
		(@UserID is null or a.UserID=@UserID) and
		(@ForumID is null or b.ForumID=@ForumID)
	order by
		a.Name	
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_userforum_delete') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_userforum_delete
GO

create procedure dbo.yaf_userforum_delete(@UserID int,@ForumID int) as
begin
	delete from yaf_UserForum where UserID=@UserID and ForumID=@ForumID
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_userforum_save') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_userforum_save
GO

create procedure dbo.yaf_userforum_save(@UserID int,@ForumID int,@AccessMaskID int) as
begin
	if exists(select 1 from yaf_UserForum where UserID=@UserID and ForumID=@ForumID)
		update yaf_UserForum set AccessMaskID=@AccessMaskID where UserID=@UserID and ForumID=@ForumID
	else
		insert into yaf_UserForum(UserID,ForumID,AccessMaskID,Invited,Accepted) values(@UserID,@ForumID,@AccessMaskID,getdate(),1)
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_active_listtopic') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_active_listtopic
GO

create procedure dbo.yaf_active_listtopic(@TopicID int) as
begin
	select
		UserID		= a.UserID,
		UserName	= b.Name
	from
		yaf_Active a with(nolock)
		join yaf_User b on b.UserID=a.UserID
	where
		a.TopicID = @TopicID
	group by
		a.UserID,
		b.Name
	order by
		b.Name
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_topic_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_topic_list
GO

create procedure dbo.yaf_topic_list(@ForumID int,@Announcement smallint,@Date datetime=null,@Offset int,@Count int) as
begin
	create table #data(
		RowNo	int identity primary key not null,
		TopicID	int not null
	)

	insert into #data(TopicID)
	select
		c.TopicID
	from
		yaf_Topic c join yaf_User b on b.UserID=c.UserID join yaf_Forum d on d.ForumID=c.ForumID
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
		Starter = IsNull(c.UserName,b.Name),
		Replies = c.NumPosts - 1,
		Views = c.Views,
		LastPosted = c.LastPosted,
		LastUserID = c.LastUserID,
		LastUserName = IsNull(c.LastUserName,(select Name from yaf_User x where x.UserID=c.LastUserID)),
		LastMessageID = c.LastMessageID,
		LastTopicID = c.TopicID,
		TopicFlags = c.Flags,
		c.Priority,
		c.PollID,
		ForumFlags = d.Flags
	from
		yaf_Topic c 
		join yaf_User b on b.UserID=c.UserID 
		join yaf_Forum d on d.ForumID=c.ForumID 
		join #data e on e.TopicID=c.TopicID
	where
		e.RowNo between @Offset+1 and @Offset + @Count
	order by
		e.RowNo
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_topic_move') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_topic_move
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

if exists (select * from sysobjects where id = object_id(N'yaf_attachment_download') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_attachment_download
GO

create procedure dbo.yaf_attachment_download(@AttachmentID int) as
begin
	update yaf_Attachment set Downloads=Downloads+1 where AttachmentID=@AttachmentID
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_user_saveavatar') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_user_saveavatar
GO

create procedure dbo.yaf_user_saveavatar(@UserID int,@AvatarImage image) as
begin
	update yaf_User set AvatarImage=@AvatarImage where UserID = @UserID
end
GO

if exists (select * from sysobjects where id = object_id(N'yaf_message_findunread') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_message_findunread
GO

create procedure dbo.yaf_message_findunread(@TopicID int,@LastRead datetime) as
begin
	select top 1 MessageID from yaf_Message
	where TopicID=@TopicID and Posted>@LastRead
	order by Posted
end
go

-- yaf_category_list
if exists (select * from sysobjects where id = object_id(N'yaf_category_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_category_list
GO

create procedure dbo.yaf_category_list(@BoardID int,@CategoryID int=null) as
begin
	if @CategoryID is null
		select * from yaf_Category where BoardID = @BoardID order by SortOrder
	else
		select * from yaf_Category where BoardID = @BoardID and CategoryID = @CategoryID
end
GO

-- yaf_forum_list
if exists (select * from sysobjects where id = object_id(N'yaf_forum_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_forum_list
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

-- yaf_forum_stats
if exists (select * from sysobjects where id = object_id(N'yaf_forum_stats') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_forum_stats
GO

-- yaf_board_stats
-- yaf_accessmask_list
if exists (select * from sysobjects where id = object_id(N'yaf_accessmask_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_accessmask_list
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

-- yaf_active_list
if exists (select * from sysobjects where id = object_id(N'yaf_active_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_active_list
GO

create procedure dbo.yaf_active_list(@BoardID int,@Guests bit=0) as
begin
	-- delete non-active
	delete from yaf_Active where DATEDIFF(minute,LastActive,getdate())>5
	-- select active
	if @Guests<>0
		select
			a.UserID,
			a.Name,
			a.IP,
			c.SessionID,
			c.ForumID,
			c.TopicID,
			ForumName = (select Name from yaf_Forum x where x.ForumID=c.ForumID),
			TopicName = (select Topic from yaf_Topic x where x.TopicID=c.TopicID),
			IsGuest = (select 1 from yaf_UserGroup x,yaf_Group y where x.UserID=a.UserID and y.GroupID=x.GroupID and (y.Flags & 2)<>0),
			c.Login,
			c.LastActive,
			c.Location,
			Active = DATEDIFF(minute,c.Login,c.LastActive),
			c.Browser,
			c.Platform
		from
			yaf_User a,
			yaf_Active c
		where
			c.UserID = a.UserID and
			c.BoardID = @BoardID
		order by
			c.LastActive desc
	else
		select
			a.UserID,
			a.Name,
			a.IP,
			c.SessionID,
			c.ForumID,
			c.TopicID,
			ForumName = (select Name from yaf_Forum x where x.ForumID=c.ForumID),
			TopicName = (select Topic from yaf_Topic x where x.TopicID=c.TopicID),
			IsGuest = (select 1 from yaf_UserGroup x,yaf_Group y where x.UserID=a.UserID and y.GroupID=x.GroupID and (y.Flags & 2)<>0),
			c.Login,
			c.LastActive,
			c.Location,
			Active = DATEDIFF(minute,c.Login,c.LastActive),
			c.Browser,
			c.Platform
		from
			yaf_User a,
			yaf_Active c
		where
			c.UserID = a.UserID and
			c.BoardID = @BoardID and
			not exists(select 1 from yaf_UserGroup x,yaf_Group y where x.UserID=a.UserID and y.GroupID=x.GroupID and (y.Flags & 2)<>0)
		order by
			c.LastActive desc
end
GO

-- yaf_active_stats
if exists (select * from sysobjects where id = object_id(N'yaf_active_stats') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_active_stats
GO

create procedure dbo.yaf_active_stats(@BoardID int) as
begin
	select
		ActiveUsers = (select count(1) from yaf_Active where BoardID=@BoardID),
		ActiveMembers = (select count(1) from yaf_Active x where BoardID=@BoardID and exists(select 1 from yaf_UserGroup y,yaf_Group z where y.UserID=x.UserID and y.GroupID=z.GroupID and (z.Flags & 2)=0)),
		ActiveGuests = (select count(1) from yaf_Active x where BoardID=@BoardID and exists(select 1 from yaf_UserGroup y,yaf_Group z where y.UserID=x.UserID and y.GroupID=z.GroupID and (z.Flags & 2)<>0))
end
GO

-- yaf_group_list
if exists (select * from sysobjects where id = object_id(N'yaf_group_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_group_list
GO

create procedure dbo.yaf_group_list(@BoardID int,@GroupID int=null) as
begin
	if @GroupID is null
		select * from yaf_Group where BoardID=@BoardID
	else
		select * from yaf_Group where BoardID=@BoardID and GroupID=@GroupID
end
GO

-- yaf_group_member
if exists (select * from sysobjects where id = object_id(N'yaf_group_member') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_group_member
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

-- yaf_bannedip_list
if exists (select * from sysobjects where id = object_id(N'yaf_bannedip_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_bannedip_list
GO

create procedure dbo.yaf_bannedip_list(@BoardID int,@ID int=null) as
begin
	if @ID is null
		select * from yaf_BannedIP where BoardID=@BoardID
	else
		select * from yaf_BannedIP where BoardID=@BoardID and ID=@ID
end
GO

-- yaf_user_emails
if exists (select * from sysobjects where id = object_id(N'yaf_user_emails') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_user_emails
GO

create procedure dbo.yaf_user_emails(@BoardID int,@GroupID int=null) as
begin
	if @GroupID = 0 set @GroupID = null
	if @GroupID is null
		select 
			a.Email 
		from 
			yaf_User a
		where 
			a.Email is not null and 
			a.BoardID = @BoardID and
			a.Email is not null and 
			a.Email<>''
	else
		select 
			a.Email 
		from 
			yaf_User a join yaf_UserGroup b on b.UserID=a.UserID
		where 
			b.GroupID = @GroupID and 
			a.Email is not null and 
			a.Email<>''
end
GO

-- yaf_smiley_listunique
if exists (select * from sysobjects where id = object_id(N'yaf_smiley_listunique') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_smiley_listunique
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

-- yaf_smiley_list
if exists (select * from sysobjects where id = object_id(N'yaf_smiley_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_smiley_list
GO

create procedure dbo.yaf_smiley_list(@BoardID int,@SmileyID int=null) as
begin
	if @SmileyID is null
		select * from yaf_Smiley where BoardID=@BoardID order by LEN(Code) desc
	else
		select * from yaf_Smiley where SmileyID=@SmileyID
end
GO

-- yaf_post_last10user
if exists (select * from sysobjects where id = object_id(N'yaf_post_last10user') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_post_last10user
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
		UserName = IsNull(a.UserName,b.Name),
		b.Signature,
		c.TopicID
	from
		yaf_Message a
		join yaf_User b on b.UserID=a.UserID
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

-- yaf_topic_active
if exists (select * from sysobjects where id = object_id(N'yaf_topic_active') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_topic_active
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
		Starter = IsNull(c.UserName,b.Name),
		Replies = (select count(1) from yaf_Message x where x.TopicID=c.TopicID and (x.Flags & 8)=0) - 1,
		Views = c.Views,
		LastPosted = c.LastPosted,
		LastUserID = c.LastUserID,
		LastUserName = IsNull(c.LastUserName,(select Name from yaf_User x where x.UserID=c.LastUserID)),
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
		join yaf_User b on b.UserID=c.UserID
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

-- yaf_user_accessmasks
if exists (select * from sysobjects where id = object_id(N'yaf_user_accessmasks') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_user_accessmasks
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
			yaf_User a 
			join yaf_UserGroup b on b.UserID=a.UserID
			join yaf_Group c on c.GroupID=b.GroupID
			join yaf_ForumAccess d on d.GroupID=c.GroupID
			join yaf_AccessMask e on e.AccessMaskID=d.AccessMaskID
			join yaf_Forum f on f.ForumID=d.ForumID
		where
			a.UserID=@UserID and
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
			yaf_User a 
			join yaf_UserForum b on b.UserID=a.UserID
			join yaf_AccessMask c on c.AccessMaskID=b.AccessMaskID
			join yaf_Forum d on d.ForumID=b.ForumID
		where
			a.UserID=@UserID and
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

-- yaf_usergroup_list
if exists (select * from sysobjects where id = object_id(N'yaf_usergroup_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_usergroup_list
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

-- yaf_forum_listpath
if exists (select * from sysobjects where id = object_id(N'yaf_forum_listpath') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_forum_listpath
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

-- yaf_category_listread
if exists (select * from sysobjects where id = object_id(N'yaf_category_listread') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_category_listread
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

-- yaf_rank_list
if exists (select * from sysobjects where id = object_id(N'yaf_rank_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_rank_list
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

-- yaf_topic_info
if exists (select * from sysobjects where id = object_id(N'yaf_topic_info') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_topic_info
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

if exists (select * from sysobjects where id = object_id(N'yaf_usergroup_save') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_usergroup_save
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

-- yaf_pmessage_save
if exists (select * from sysobjects where id = object_id(N'yaf_pmessage_save') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_pmessage_save
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
				a.UserID,@PMessageID,0
		from
				yaf_User a
				join yaf_UserGroup b on b.UserID=a.UserID
				join yaf_Group c on c.GroupID=b.GroupID where
				(c.Flags & 2)=0 and
				c.BoardID=(select BoardID from yaf_User x where x.UserID=@FromUserID) and a.UserID<>@FromUserID
		group by
				a.UserID
	end
	else
	begin
		insert into yaf_UserPMessage(UserID,PMessageID,IsRead) values(@ToUserID,@PMessageID,0)
	end
end

GO

-- yaf_pageload
if exists (select * from sysobjects where id = object_id(N'yaf_pageload') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_pageload
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
		select @UserID = a.UserID from yaf_User a,yaf_UserGroup b,yaf_Group c where a.UserID=b.UserID and a.BoardID=@BoardID and b.GroupID=c.GroupID and (c.Flags & 2)<>0
		set @rowcount=@@rowcount
		if @rowcount<>1
		begin
			raiserror('Found %d possible guest users. Only 1 guest user should be a member of the group marked as the guest group.',16,1,@rowcount)
		end
		set @IsGuest = 1
		set @UserBoardID = @BoardID
	end else
	begin
		select @UserID = UserID, @UserBoardID = BoardID from yaf_User where BoardID=@BoardID and Name=@User
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
	update yaf_User set 
		LastVisit = getdate(),
		IP = @IP
	where UserID = @UserID

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
		a.UserID,
		UserFlags			= a.Flags,
		UserName			= a.Name,
		Suspended			= a.Suspended,
		ThemeFile			= a.ThemeFile,
		LanguageFile		= a.LanguageFile,
		TimeZoneUser		= a.TimeZone,
		x.*,
		CategoryID			= @CategoryID,
		CategoryName		= (select Name from yaf_Category where CategoryID = @CategoryID),
		ForumID				= @ForumID,
		ForumName			= (select Name from yaf_Forum where ForumID = @ForumID),
		TopicID				= @TopicID,
		TopicName			= (select Topic from yaf_Topic where TopicID = @TopicID),
		MailsPending		= (select count(1) from yaf_Mail),
		Incoming			= (select count(1) from yaf_UserPMessage where UserID=a.UserID and IsRead=0)
	from
		yaf_User a,
		yaf_vaccess x
	where
		a.UserID = @UserID and
		x.UserID = a.UserID and
		x.ForumID = IsNull(@ForumID,0)
end
GO

-- yaf_user_delete
if exists (select * from sysobjects where id = object_id(N'yaf_user_delete') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_user_delete
GO

create procedure dbo.yaf_user_delete(@UserID int) as
begin
	declare @GuestUserID	int
	declare @UserName		nvarchar(50)
	declare @GuestCount		int

	select @UserName = Name from yaf_User where UserID=@UserID

	select top 1
		@GuestUserID = a.UserID
	from
		yaf_User a,
		yaf_UserGroup b,
		yaf_Group c
	where
		b.UserID = a.UserID and
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
	delete from yaf_User where UserID = @UserID
end
GO

-- yaf_user_find
if exists (select * from sysobjects where id = object_id(N'yaf_user_find') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_user_find
GO

create procedure dbo.yaf_user_find(@BoardID int,@Filter bit,@UserName nvarchar(50)=null,@Email nvarchar(50)=null) as
begin
	if @Filter<>0
	begin
		if @UserName is not null
			set @UserName = '%' + @UserName + '%'

		select 
			a.*,
			IsGuest = (select count(1) from yaf_UserGroup x,yaf_Group y where x.UserID=a.UserID and x.GroupID=y.GroupID and (y.Flags & 2)<>0)
		from 
			yaf_User a
		where 
			a.BoardID=@BoardID and
			(@UserName is not null and a.Name like @UserName) or (@Email is not null and Email like @Email)
		order by
			a.Name
	end else
	begin
		select 
			a.UserID,
			IsGuest = (select count(1) from yaf_UserGroup x,yaf_Group y where x.UserID=a.UserID and x.GroupID=y.GroupID and (y.Flags & 2)<>0)
		from 
			yaf_User a
		where 
			a.BoardID=@BoardID and
			((@UserName is not null and a.Name=@UserName) or (@Email is not null and Email=@Email))
	end
end
GO

-- yaf_pmessage_markread
if exists (select * from sysobjects where id = object_id(N'yaf_pmessage_markread') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_pmessage_markread
GO

create procedure dbo.yaf_pmessage_markread(@UserPMessageID int=null) as begin
	update yaf_UserPMessage set IsRead=1 where UserPMessageID=@UserPMessageID
end
GO

-- yaf_attachment_list
if exists (select * from sysobjects where id = object_id(N'yaf_attachment_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_attachment_list
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

-- yaf_nntpserver_list
if exists (select * from sysobjects where id = object_id(N'yaf_nntpserver_list') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_nntpserver_list
GO

create procedure dbo.yaf_nntpserver_list(@BoardID int=null,@NntpServerID int=null) as
begin
	if @NntpServerID is null
		select * from yaf_NntpServer where BoardID=@BoardID order by Name
	else
		select * from yaf_NntpServer where NntpServerID=@NntpServerID
end
GO

-- yaf_board_delete
if exists (select * from sysobjects where id = object_id(N'yaf_board_delete') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_board_delete
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
	delete from yaf_UserGroup where exists(select 1 from yaf_User x where x.UserID=yaf_UserGroup.UserID and x.BoardID=@BoardID)
	delete from yaf_Category where BoardID=@BoardID
	delete from yaf_User where BoardID=@BoardID
	delete from yaf_Rank where BoardID=@BoardID
	delete from yaf_Group where BoardID=@BoardID
	delete from yaf_AccessMask where BoardID=@BoardID
	delete from yaf_Active where BoardID=@BoardID
	delete from yaf_Board where BoardID=@BoardID
end
GO

-- yaf_user_nntp
if exists (select * from sysobjects where id = object_id(N'yaf_user_nntp') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_user_nntp
go

create procedure dbo.yaf_user_nntp(@BoardID int,@UserName nvarchar(50),@Email nvarchar(50)) as
begin
	declare @UserID int

	set @UserName = @UserName + ' (NNTP)'

	select
		@UserID=UserID
	from
		yaf_User
	where
		BoardID=@BoardID and
		Name=@UserName

	if @@ROWCOUNT<1
	begin
		exec yaf_user_save 0,@BoardID,@UserName,'-',@Email,null,'Usenet',null,0,null,null,null,1,null,null,null,null,null,null,null,0,null
		-- The next one is not safe, but this procedure is only used for testing
		select @UserID=max(UserID) from yaf_User
	end

	select UserID=@UserID
end
go

-- yaf_user_deleteold
if exists (select * from sysobjects where id = object_id(N'yaf_user_deleteold') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_user_deleteold
GO

create procedure dbo.yaf_user_deleteold(@BoardID int) as
begin
	declare @Since datetime
	
	set @Since = getdate()

	delete from yaf_CheckEmail where UserID in(select UserID from yaf_User where BoardID=@BoardID and dbo.yaf_bitset(Flags,2)=0 and datediff(day,Joined,@Since)>2)
	delete from yaf_UserGroup where UserID in(select UserID from yaf_User where BoardID=@BoardID and dbo.yaf_bitset(Flags,2)=0 and datediff(day,Joined,@Since)>2)
	delete from yaf_User where BoardID=@BoardID and dbo.yaf_bitset(Flags,2)=0 and datediff(day,Joined,@Since)>2
end
GO

-- yaf_board_stats
if exists (select * from sysobjects where id = object_id(N'yaf_board_stats') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_board_stats
GO

create procedure dbo.yaf_board_stats as begin
	select
		NumPosts	= (select count(1) from yaf_Message where (Flags & 24)=16),
		NumTopics	= (select count(1) from yaf_Topic),
		NumUsers	= (select count(1) from yaf_User where dbo.yaf_bitset(Flags,2)<>0),
		BoardStart	= (select min(Joined) from yaf_User)
end
GO

-- yaf_board_poststats
if exists (select * from sysobjects where id = object_id(N'yaf_board_poststats') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure yaf_board_poststats
GO

create procedure dbo.yaf_board_poststats(@BoardID int) as
begin
	select
		Posts = (select count(1) from yaf_Message a join yaf_Topic b on b.TopicID=a.TopicID join yaf_Forum c on c.ForumID=b.ForumID join yaf_Category d on d.CategoryID=c.CategoryID where d.BoardID=@BoardID),
		Topics = (select count(1) from yaf_Topic a join yaf_Forum b on b.ForumID=a.ForumID join yaf_Category c on c.CategoryID=b.CategoryID where c.BoardID=@BoardID),
		Forums = (select count(1) from yaf_Forum a join yaf_Category b on b.CategoryID=a.CategoryID where b.BoardID=@BoardID),
		Members = (select count(1) from yaf_User a where a.BoardID=@BoardID),
		LastPostInfo.*,
		LastMemberInfo.*
	from
		(
			select top 1 
				LastMemberInfoID= 1,
				LastMemberID	= UserID,
				LastMember	= Name
			from 
				yaf_User 
			where 
				dbo.yaf_bitset(Flags,2)=1 and 
				BoardID=@BoardID 
			order by 
				Joined desc
		) as LastMemberInfo
		left join (
			select top 1 
				LastPostInfoID	= 1,
				LastPost	= a.Posted,
				LastUserID	= a.UserID,
				LastUser	= e.Name
			from 
				yaf_Message a 
				join yaf_Topic b on b.TopicID=a.TopicID 
				join yaf_Forum c on c.ForumID=b.ForumID 
				join yaf_Category d on d.CategoryID=c.CategoryID 
				join yaf_User e on e.UserID=a.UserID
			where 
				d.BoardID=@BoardID
			order by
				a.Posted desc
		) as LastPostInfo
		on LastMemberInfoID=LastPostInfoID
end
GO

