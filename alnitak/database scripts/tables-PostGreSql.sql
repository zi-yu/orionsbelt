---------------------------------------------------------------------------------------------------------
-- ORION'S BELT -----------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------

CREATE SEQUENCE OrionsBelt_Alliance_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE orionsbelt_alliance
(
  alliance_id int4 NOT NULL DEFAULT nextval('OrionsBelt_Alliance_seq'::text),
  alliance_name varchar(150) NOT NULL DEFAULT ''::character varying,
  alliance_tag varchar(150) NOT NULL DEFAULT ''::character varying,
  alliance_motto varchar(450) NOT NULL DEFAULT ''::character varying,
  alliance_rank int4 NOT NULL DEFAULT 1000,
  alliance_rankbattles int4 NOT NULL DEFAULT 0,
  alliance_regist timestamp DEFAULT ('now'::text)::timestamp(6) without time zone
) 

--OrionsBelt_Users
CREATE SEQUENCE OrionsBelt_Users_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE orionsbelt_users
(
  user_id int4 NOT NULL DEFAULT nextval('OrionsBelt_Users_seq'::text),
  user_ruler_id int4 DEFAULT -1,
  user_registdate timestamp DEFAULT ('now'::text)::timestamp(6) without time zone,
  user_lastlogin timestamp DEFAULT ('now'::text)::timestamp(6) without time zone,
  user_mail varchar(30) NOT NULL DEFAULT ''::character varying,
  user_pass char(40) NOT NULL,
  user_nick varchar(30) NOT NULL,
  user_website varchar(500) NOT NULL DEFAULT ''::character varying,
  user_avatar varchar(250) NOT NULL DEFAULT ''::character varying,
  user_skin int4 NOT NULL DEFAULT 1,
  user_lang varchar(5) NOT NULL,
  user_imagesdir varchar(100) NOT NULL DEFAULT ''::character varying,
  user_msn varchar(30) NOT NULL DEFAULT ''::character varying,
  user_icq varchar(30) NOT NULL DEFAULT ''::character varying,
  user_jabber varchar(30) NOT NULL DEFAULT ''::character varying,
  user_aim varchar(30) NOT NULL DEFAULT ''::character varying,
  user_yahoo varchar(30) NOT NULL DEFAULT ''::character varying,
  user_signature varchar(255) NOT NULL DEFAULT ''::character varying,
  user_boardid int4 NOT NULL DEFAULT 1,
  user_ip varchar(100),
  user_numposts int4 NOT NULL DEFAULT 0,
  user_location varchar(100),
  user_timezone int4 NOT NULL DEFAULT 0,
  user_rankid int4 NOT NULL DEFAULT 3,
  user_gender int2 NOT NULL DEFAULT 0,
  user_flags int4 NOT NULL DEFAULT 2,
  user_rank int4 DEFAULT 1000,
  user_alliance_id int4 NOT NULL DEFAULT 0,
  user_alliance_rank varchar(15) DEFAULT 'Private'::character varying,
  CONSTRAINT orionsbelt_users_pkey PRIMARY KEY (user_id)
) 

-- Primary key
ALTER TABLE OrionsBelt_Users ADD  PRIMARY KEY(user_id);


--OrionsBelt_ExceptionLog
CREATE SEQUENCE OrionsBelt_ExceptionLog_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE OrionsBelt_ExceptionLog (
   exceptionLog_id INTEGER DEFAULT NEXTVAL('OrionsBelt_ExceptionLog_seq') NOT NULL,
   exceptionLog_name VARCHAR(100)  NOT NULL,
   exceptionLog_message VARCHAR(5000)  NOT NULL,
   exceptionLog_stackTrace VARCHAR(5000)  NOT NULL,
   exceptionLog_date TIMESTAMP NOT NULL
);
-- Primary key
ALTER TABLE OrionsBelt_ExceptionLog ADD  PRIMARY KEY(exceptionLog_id);

--OrionsBelt_MasterSkins

CREATE SEQUENCE OrionsBelt_MasterSkins_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE OrionsBelt_MasterSkins(
   masterSkin_id INTEGER DEFAULT NEXTVAL('OrionsBelt_MasterSkins_seq')   NOT NULL,
   masterSkin_name VARCHAR(30)  NOT NULL,
   masterSkin_style VARCHAR(30)  NOT NULL,
   masterSkin_script VARCHAR(30)  NOT NULL,
   masterSkin_description VARCHAR(50),
   masterSkin_count INTEGER DEFAULT 1
);
-- Primary key
ALTER TABLE OrionsBelt_MasterSkins ADD PRIMARY KEY(masterSkin_id);

--OrionsBelt_Messages
CREATE SEQUENCE OrionsBelt_Messages_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE OrionsBelt_Messages(
   message_uniqueId INTEGER DEFAULT NEXTVAL('OrionsBelt_Messages_seq') NOT NULL,
   message_id INTEGER  NOT NULL,
   message_identifier VARCHAR(100)  NOT NULL,
   message_read BOOLEAN DEFAULT false NOT NULL,
   message_type VARCHAR(100) DEFAULT ' ' NOT NULL,
   message_data BYTEA NOT NULL
);
-- Primary key
ALTER TABLE OrionsBelt_Messages ADD PRIMARY KEY(message_uniqueId);

--OrionsBelt_NamedPage
CREATE SEQUENCE OrionsBelt_NamedPage_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE OrionsBelt_NamedPage(
   namedPage_id INTEGER DEFAULT NEXTVAL('OrionsBelt_NamedPage_seq') NOT NULL,
   namedPage_path VARCHAR(30)  NOT NULL,
   namedPage_name VARCHAR(30)  NOT NULL,
   namedPage_title VARCHAR(30)  NOT NULL,
   namedPage_content VARCHAR(30)  NOT NULL,
   namedPage_description VARCHAR(30)
);
-- Primary key
ALTER TABLE OrionsBelt_NamedPage ADD  PRIMARY KEY(namedPage_id);

--OrionsBelt_News
CREATE SEQUENCE OrionsBelt_News_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE OrionsBelt_News(
   news_id INTEGER DEFAULT NEXTVAL('OrionsBelt_News_seq') NOT NULL,
   news_title VARCHAR(400)  NOT NULL,
   news_content VARCHAR(4000)  NOT NULL,
   news_date TIMESTAMP DEFAULT LOCALTIMESTAMP NOT NULL,
   news_lang VARCHAR(50) DEFAULT 'pt' NOT NULL
);
-- Primary key
ALTER TABLE OrionsBelt_News ADD  PRIMARY KEY(news_id);

--OrionsBelt_Roles
CREATE SEQUENCE OrionsBelt_Roles_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE OrionsBelt_Roles(
   roles_id INTEGER DEFAULT NEXTVAL('OrionsBelt_Roles_seq')   NOT NULL,
   roles_roleName VARCHAR(20)  NOT NULL,
   roles_roleDescription VARCHAR(50)
);
-- Primary key
ALTER TABLE OrionsBelt_Roles ADD  PRIMARY KEY(roles_id);

--OrionsBelt_Scans
CREATE SEQUENCE OrionsBelt_Scans_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE OrionsBelt_Scans(
   scans_id INTEGER DEFAULT NEXTVAL('OrionsBelt_Scans_seq') NOT NULL,
   scans_sourcePlanetId INTEGER NOT NULL,
   scans_data BYTEA NOT NULL
);
-- Primary key
ALTER TABLE OrionsBelt_Scans ADD PRIMARY KEY(scans_id);

--OrionsBelt_SectionRoles
CREATE TABLE OrionsBelt_SectionRoles(
   sectionroles_section_id INTEGER NOT NULL,
   sectionroles_role_id INTEGER NOT NULL
);
-- Primary key
ALTER TABLE OrionsBelt_SectionRoles ADD  PRIMARY KEY(sectionroles_section_id,sectionroles_role_id);

--OrionsBelt_Sections
CREATE SEQUENCE OrionsBelt_Sections_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE OrionsBelt_Sections(
   section_id INTEGER DEFAULT NEXTVAL('OrionsBelt_Sections_seq')   NOT NULL,
   section_parentId INTEGER  NOT NULL,
   section_name VARCHAR(30)  NOT NULL,
   section_title VARCHAR(30)  NOT NULL,
   section_skin INTEGER,
   section_content VARCHAR(30)  NOT NULL,
   section_description VARCHAR(30),
   section_iconId INTEGER,
   section_order INTEGER  NOT NULL,
   section_isVisible INTEGER DEFAULT 1 NOT NULL
);

-- Primary key
ALTER TABLE OrionsBelt_Sections ADD PRIMARY KEY(section_id);
ALTER TABLE OrionsBelt_Sections ADD FOREIGN KEY(section_skin)
REFERENCES OrionsBelt_MasterSkins(masterSkin_id) ON DELETE NO ACTION ON UPDATE NO ACTION;

--Orionsbelt_universe
CREATE SEQUENCE orionsbelt_universe_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE orionsbelt_universe(
	id INTEGER DEFAULT NEXTVAL('orionsbelt_universe_seq') NOT NULL,
	data BYTEA NOT NULL,
	date TIMESTAMP DEFAULT LOCALTIMESTAMP NOT NULL
);
ALTER TABLE orionsbelt_universe ADD PRIMARY KEY(id);

--OrionsBelt_UserRoles
CREATE TABLE OrionsBelt_UserRoles(
   user_id INTEGER  NOT NULL,
   roles_id INTEGER  NOT NULL
);

ALTER TABLE OrionsBelt_UserRoles ADD FOREIGN KEY(roles_id)
REFERENCES OrionsBelt_Roles(roles_id) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE OrionsBelt_UserRoles ADD FOREIGN KEY(user_id)
REFERENCES OrionsBelt_Users(user_id) ON DELETE NO ACTION ON UPDATE NO ACTION;


---------------------------------------------------------------------------------------------------------
-- FORUM ------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------
--yaf_Board
CREATE SEQUENCE yaf_Board_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE yaf_Board
(
   BoardID INTEGER DEFAULT NEXTVAL('yaf_Board_seq') NOT NULL,
   Name VARCHAR(50)  NOT NULL,
   AllowThreaded BOOLEAN  NOT NULL
);
-- Primary key
ALTER TABLE yaf_Board ADD  PRIMARY KEY(BoardID);

--yaf_AccessMask
CREATE SEQUENCE yaf_AccessMask_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE yaf_AccessMask(
   AccessMaskID INTEGER DEFAULT NEXTVAL('yaf_AccessMask_seq')   NOT NULL,
   BoardID INTEGER  NOT NULL,
   Name VARCHAR(50)  NOT NULL,
   Flags INTEGER DEFAULT 0  NOT NULL
);

-- Primary key
ALTER TABLE yaf_AccessMask ADD  PRIMARY KEY(AccessMaskID);

ALTER TABLE yaf_AccessMask ADD FOREIGN KEY(BoardID)
REFERENCES yaf_Board(BoardID) ON DELETE NO ACTION  ON UPDATE NO ACTION;

--yaf_Active
CREATE TABLE yaf_Active(
   SessionID VARCHAR(24)  NOT NULL,
   BoardID INTEGER  NOT NULL,
   UserID INTEGER  NOT NULL,
   IP VARCHAR(15)  NOT NULL,
   Login TIMESTAMP  NOT NULL,
   LastActive TIMESTAMP  NOT NULL,
   Location VARCHAR(50)  NOT NULL,
   ForumID INTEGER,
   TopicID INTEGER,
   Browser VARCHAR(50),
   Platform VARCHAR(50)
);
-- Primary key
ALTER TABLE yaf_Active ADD  PRIMARY KEY(SessionID,BoardID);

ALTER TABLE yaf_Active ADD  FOREIGN KEY(UserID)
REFERENCES OrionsBelt_Users(user_id) ON DELETE NO ACTION  ON UPDATE NO ACTION;

ALTER TABLE yaf_Active ADD  FOREIGN KEY(UserID)
REFERENCES OrionsBelt_Users(user_id) ON DELETE NO ACTION  ON UPDATE NO ACTION;

ALTER TABLE yaf_Active ADD  FOREIGN KEY(BoardID)
REFERENCES yaf_Board(BoardID)   ON DELETE NO ACTION  ON UPDATE NO ACTION;

--yaf_Attachment
CREATE SEQUENCE yaf_Attachment_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE yaf_Attachment(
   AttachmentID INTEGER DEFAULT NEXTVAL('yaf_Attachment_seq')   NOT NULL,
   MessageID INTEGER  NOT NULL,
   FileName VARCHAR(250)  NOT NULL,
   Bytes INTEGER  NOT NULL,
   FileID INTEGER,
   ContentType VARCHAR(50),
   Downloads INTEGER  NOT NULL,
   FileData BYTEA
);

-- Primary key
ALTER TABLE yaf_Attachment ADD  PRIMARY KEY(AttachmentID);

--yaf_BannedIP
CREATE SEQUENCE yaf_BannedIP_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE yaf_BannedIP(
   ID INTEGER DEFAULT NEXTVAL('yaf_BannedIP_seq')   NOT NULL,
   BoardID INTEGER  NOT NULL,
   Mask VARCHAR(15)  NOT NULL,
   Since TIMESTAMP  NOT NULL
);

-- Primary key
ALTER TABLE yaf_BannedIP ADD  PRIMARY KEY(ID);

ALTER TABLE yaf_BannedIP ADD  FOREIGN KEY(BoardID)
REFERENCES yaf_Board(BoardID) ON DELETE NO ACTION  ON UPDATE NO ACTION;

ALTER TABLE yaf_BannedIP ADD  UNIQUE(BoardID,Mask);

--

CREATE SEQUENCE yaf_Category_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE yaf_Category(
   CategoryID INTEGER DEFAULT NEXTVAL('yaf_Category_seq')   NOT NULL,
   BoardID INTEGER  NOT NULL,
   Name VARCHAR(50)  NOT NULL,
   SortOrder SMALLINT  NOT NULL
);

-- Primary key

ALTER TABLE yaf_Category ADD  PRIMARY KEY(CategoryID);

ALTER TABLE yaf_Category ADD  FOREIGN KEY(BoardID)
REFERENCES yaf_Board(BoardID)   ON DELETE NO ACTION  ON UPDATE NO ACTION;

ALTER TABLE yaf_Category ADD  UNIQUE(BoardID,Name);

--yaf_CheckEmail
CREATE SEQUENCE yaf_CheckEmail_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE yaf_CheckEmail(
   CheckEmailID INTEGER DEFAULT NEXTVAL('yaf_CheckEmail_seq')   NOT NULL,
   UserID INTEGER  NOT NULL,
   Email VARCHAR(50)  NOT NULL,
   Created TIMESTAMP  NOT NULL,
   Hash VARCHAR(32)  NOT NULL
);
-- Primary key
ALTER TABLE yaf_CheckEmail ADD  PRIMARY KEY(CheckEmailID);
ALTER TABLE yaf_CheckEmail ADD  UNIQUE(Hash);

--yaf_Poll
CREATE SEQUENCE yaf_Poll_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE yaf_Poll(
   PollID INTEGER DEFAULT NEXTVAL('yaf_Poll_seq') NOT NULL,
   Question VARCHAR(50) NOT NULL
);

-- Primary key
ALTER TABLE yaf_Poll ADD  PRIMARY KEY(PollID);

--yaf_Choice
CREATE SEQUENCE yaf_Choice_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE yaf_Choice (
   ChoiceID INTEGER DEFAULT NEXTVAL('yaf_Choice_seq')   NOT NULL,
   PollID INTEGER  NOT NULL,
   Choice VARCHAR(50)  NOT NULL,
   Votes INTEGER  NOT NULL
);

-- Primary key

ALTER TABLE yaf_Choice ADD  PRIMARY KEY(ChoiceID);

ALTER TABLE yaf_Choice ADD  FOREIGN KEY(PollID)
REFERENCES yaf_Poll(PollID)   ON DELETE NO ACTION  ON UPDATE NO ACTION;

--yaf_Forum
CREATE SEQUENCE yaf_Forum_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE yaf_Forum(
   ForumID INTEGER DEFAULT NEXTVAL('yaf_Forum_seq') NOT NULL,
   CategoryID INTEGER  NOT NULL,
   ParentID INTEGER,
   Name VARCHAR(50)  NOT NULL,
   Description VARCHAR(255)  NOT NULL,
   SortOrder SMALLINT  NOT NULL,
   LastPosted TIMESTAMP,
   LastTopicID INTEGER,
   LastMessageID INTEGER,
   LastUserID INTEGER,
   LastUserName VARCHAR(50),
   NumTopics INTEGER  NOT NULL,
   NumPosts INTEGER  NOT NULL,
   RemoteURL VARCHAR(100),
   Flags INTEGER DEFAULT 0  NOT NULL
);
-- Primary key
ALTER TABLE yaf_Forum ADD PRIMARY KEY(ForumID);

ALTER TABLE yaf_Forum ADD FOREIGN KEY(LastUserID)
REFERENCES OrionsBelt_Users(user_id) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE yaf_Forum ADD FOREIGN KEY(CategoryID)
REFERENCES yaf_Category(CategoryID) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE yaf_Forum ADD FOREIGN KEY(ParentID)
REFERENCES yaf_Forum(ForumID) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE yaf_Forum ADD UNIQUE(CategoryID,Name);

--yaf_Group
CREATE SEQUENCE yaf_Group_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE yaf_Group(
   GroupID INTEGER DEFAULT NEXTVAL('yaf_Group_seq')   NOT NULL,
   BoardID INTEGER  NOT NULL,
   Name VARCHAR(50)  NOT NULL,
   Flags INTEGER DEFAULT 0  NOT NULL
);

-- Primary key
ALTER TABLE yaf_Group ADD  PRIMARY KEY(GroupID);

ALTER TABLE yaf_Group ADD  FOREIGN KEY(BoardID)
REFERENCES yaf_Board(BoardID)   ON DELETE NO ACTION  ON UPDATE NO ACTION;

ALTER TABLE yaf_Group ADD  UNIQUE(BoardID,Name);


--yaf_ForumAccess
CREATE TABLE yaf_ForumAccess(
   GroupID INTEGER  NOT NULL,
   ForumID INTEGER  NOT NULL,
   AccessMaskID INTEGER  NOT NULL
);
-- Primary key
ALTER TABLE yaf_ForumAccess ADD  PRIMARY KEY(GroupID,ForumID);

ALTER TABLE yaf_ForumAccess ADD  FOREIGN KEY(AccessMaskID)
REFERENCES yaf_AccessMask(AccessMaskID)   ON DELETE NO ACTION  ON UPDATE NO ACTION;

ALTER TABLE yaf_ForumAccess ADD  FOREIGN KEY(ForumID)
REFERENCES yaf_Forum(ForumID)   ON DELETE NO ACTION  ON UPDATE NO ACTION;

ALTER TABLE yaf_ForumAccess ADD  FOREIGN KEY(GroupID)
REFERENCES yaf_Group(GroupID)   ON DELETE NO ACTION  ON UPDATE NO ACTION;


--yaf_Mail
CREATE SEQUENCE yaf_Mail_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE yaf_Mail(
   MailID INTEGER DEFAULT NEXTVAL('yaf_Mail_seq') NOT NULL,
   FromUser VARCHAR(50)  NOT NULL,
   ToUser VARCHAR(50)  NOT NULL,
   Created TIMESTAMP  NOT NULL,
   Subject VARCHAR(100)  NOT NULL,
   Body TEXT  NOT NULL
);

-- Primary key
ALTER TABLE yaf_Mail ADD  PRIMARY KEY(MailID);

--yaf_Message
CREATE SEQUENCE yaf_Message_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE yaf_Message(
   MessageID INTEGER DEFAULT NEXTVAL('yaf_Message_seq') NOT NULL,
   TopicID INTEGER  NOT NULL,
   ReplyTo INTEGER,
   Position INTEGER  NOT NULL,
   Indent INTEGER  NOT NULL,
   UserID INTEGER  NOT NULL,
   UserName VARCHAR(50),
   Posted TIMESTAMP  NOT NULL,
   Message TEXT  NOT NULL,
   IP VARCHAR(15)  NOT NULL,
   Edited TIMESTAMP,
   Flags INTEGER DEFAULT 23  NOT NULL
);

-- Primary key
ALTER TABLE yaf_Message ADD  PRIMARY KEY(MessageID);

ALTER TABLE yaf_Message ADD  FOREIGN KEY(UserID)
REFERENCES OrionsBelt_Users(user_id)   ON DELETE NO ACTION  ON UPDATE NO ACTION;

ALTER TABLE yaf_Message ADD  FOREIGN KEY(ReplyTo)
REFERENCES yaf_Message(MessageID) ON DELETE NO ACTION  ON UPDATE NO ACTION;

--yaf_NntpServer
CREATE SEQUENCE yaf_NntpServer_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE yaf_NntpServer(
   NntpServerID INTEGER DEFAULT NEXTVAL('yaf_NntpServer_seq') NOT NULL,
   BoardID INTEGER  NOT NULL,
   Name VARCHAR(50)  NOT NULL,
   Address VARCHAR(100)  NOT NULL,
   Port INTEGER,
   UserName VARCHAR(50),
   UserPass VARCHAR(50)
);
-- Primary key
ALTER TABLE yaf_NntpServer ADD  PRIMARY KEY(NntpServerID);

ALTER TABLE yaf_NntpServer ADD  FOREIGN KEY(BoardID)
REFERENCES yaf_Board(BoardID) ON DELETE NO ACTION ON UPDATE NO ACTION;

--yaf_NntpForum
CREATE SEQUENCE yaf_NntpForum_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE yaf_NntpForum(
   NntpForumID INTEGER DEFAULT NEXTVAL('yaf_NntpForum_seq')   NOT NULL,
   NntpServerID INTEGER  NOT NULL,
   GroupName VARCHAR(100)  NOT NULL,
   ForumID INTEGER  NOT NULL,
   LastMessageNo INTEGER  NOT NULL,
   LastUpdate TIMESTAMP  NOT NULL,
   Active BOOLEAN  NOT NULL
);

-- Primary key
ALTER TABLE yaf_NntpForum ADD  PRIMARY KEY(NntpForumID);

ALTER TABLE yaf_NntpForum ADD  FOREIGN KEY(ForumID)
REFERENCES yaf_Forum(ForumID)   ON DELETE NO ACTION  ON UPDATE NO ACTION;

ALTER TABLE yaf_NntpForum ADD  FOREIGN KEY(NntpServerID)
REFERENCES yaf_NntpServer(NntpServerID) ON DELETE NO ACTION  ON UPDATE NO ACTION;


--yaf_Topic
CREATE SEQUENCE yaf_Topic_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE yaf_Topic(
   TopicID INTEGER DEFAULT NEXTVAL('yaf_Topic_seq')   NOT NULL,
   ForumID INTEGER  NOT NULL,
   UserID INTEGER  NOT NULL,
   UserName VARCHAR(50),
   Posted TIMESTAMP  NOT NULL,
   Topic VARCHAR(100)  NOT NULL,
   Views INTEGER  NOT NULL,
   Priority SMALLINT  NOT NULL,
   PollID INTEGER,
   TopicMovedID INTEGER,
   LastPosted TIMESTAMP,
   LastMessageID INTEGER,
   LastUserID INTEGER,
   LastUserName VARCHAR(50),
   NumPosts INTEGER  NOT NULL,
   Flags INTEGER DEFAULT 0  NOT NULL
);

-- Primary key

ALTER TABLE yaf_Topic ADD PRIMARY KEY(TopicID);

ALTER TABLE yaf_Topic ADD FOREIGN KEY(UserID)
REFERENCES OrionsBelt_Users(user_id) ON DELETE NO ACTION  ON UPDATE NO ACTION;

ALTER TABLE yaf_Topic ADD  FOREIGN KEY(LastUserID)
REFERENCES OrionsBelt_Users(user_id) ON DELETE NO ACTION  ON UPDATE NO ACTION;

ALTER TABLE yaf_Topic ADD  FOREIGN KEY(ForumID)
REFERENCES yaf_Forum(ForumID) ON DELETE CASCADE  ON UPDATE NO ACTION;

ALTER TABLE yaf_Topic ADD  FOREIGN KEY(LastMessageID)
REFERENCES yaf_Message(MessageID) ON DELETE NO ACTION  ON UPDATE NO ACTION;

ALTER TABLE yaf_Topic ADD  FOREIGN KEY(PollID)
REFERENCES yaf_Poll(PollID) ON DELETE NO ACTION  ON UPDATE NO ACTION;

ALTER TABLE yaf_Topic ADD  FOREIGN KEY(TopicMovedID)
REFERENCES yaf_Topic(TopicID) ON DELETE NO ACTION  ON UPDATE NO ACTION;


--yaf_NntpTopic
CREATE SEQUENCE yaf_NntpTopic_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE yaf_NntpTopic(
   NntpTopicID INTEGER DEFAULT NEXTVAL('yaf_NntpTopic_seq')   NOT NULL,
   NntpForumID INTEGER  NOT NULL,
   Thread CHAR(32)  NOT NULL,
   TopicID INTEGER  NOT NULL
);
-- Primary key
ALTER TABLE yaf_NntpTopic ADD  PRIMARY KEY(NntpTopicID);

ALTER TABLE yaf_NntpTopic ADD  FOREIGN KEY(NntpForumID)
REFERENCES yaf_NntpForum(NntpForumID)   ON DELETE NO ACTION  ON UPDATE NO ACTION;

ALTER TABLE yaf_NntpTopic ADD  FOREIGN KEY(TopicID)
REFERENCES yaf_Topic(TopicID)   ON DELETE NO ACTION  ON UPDATE NO ACTION;

--yaf_PMessage
CREATE SEQUENCE yaf_PMessage_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE yaf_PMessage(
   PMessageID INTEGER DEFAULT NEXTVAL('yaf_PMessage_seq')   NOT NULL,
   FromUserID INTEGER  NOT NULL,
   Created TIMESTAMP  NOT NULL,
   Subject VARCHAR(100)  NOT NULL,
   Body TEXT  NOT NULL,
   Flags INTEGER  NOT NULL
);
-- Primary key
ALTER TABLE yaf_PMessage ADD  PRIMARY KEY(PMessageID);

ALTER TABLE yaf_PMessage ADD  FOREIGN KEY(FromUserID)
REFERENCES OrionsBelt_Users(user_id)   ON DELETE NO ACTION  ON UPDATE NO ACTION;

--yaf_Rank
CREATE SEQUENCE yaf_Rank_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE yaf_Rank(
   RankID INTEGER DEFAULT NEXTVAL('yaf_Rank_seq')   NOT NULL,
   BoardID INTEGER  NOT NULL,
   Name VARCHAR(50)  NOT NULL,
   MinPosts INTEGER,
   RankImage VARCHAR(50),
   Flags INTEGER DEFAULT 0  NOT NULL
);
-- Primary key
ALTER TABLE yaf_Rank ADD  PRIMARY KEY(RankID);

ALTER TABLE yaf_Rank ADD  FOREIGN KEY(BoardID)
REFERENCES yaf_Board(BoardID)   ON DELETE NO ACTION  ON UPDATE NO ACTION;

ALTER TABLE yaf_Rank ADD  UNIQUE(BoardID,Name);

--yaf_Registry
CREATE SEQUENCE yaf_Registry_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE yaf_Registry(
   RegistryID INTEGER DEFAULT NEXTVAL('yaf_Registry_seq')   NOT NULL,
   Name VARCHAR(50)  NOT NULL,
   Value VARCHAR(400),
   BoardID INTEGER
);
-- Primary key
ALTER TABLE yaf_Registry ADD  PRIMARY KEY(RegistryID);

ALTER TABLE yaf_Registry ADD  FOREIGN KEY(BoardID)
REFERENCES yaf_Board(BoardID)   ON DELETE CASCADE  ON UPDATE NO ACTION;

--yaf_Replace_Words
CREATE SEQUENCE yaf_Replace_Words_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE yaf_Replace_Words(
   id INTEGER DEFAULT NEXTVAL('yaf_Replace_Words_seq')   NOT NULL,
   badword VARCHAR(255),
   goodword VARCHAR(255)
);
-- Primary key
ALTER TABLE yaf_Replace_Words ADD  PRIMARY KEY(id);

--yaf_Smiley
CREATE SEQUENCE yaf_Smiley_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE yaf_Smiley(
   SmileyID INTEGER DEFAULT NEXTVAL('yaf_Smiley_seq')   NOT NULL,
   BoardID INTEGER  NOT NULL,
   Code VARCHAR(10)  NOT NULL,
   Icon VARCHAR(50)  NOT NULL,
   Emoticon VARCHAR(50)
);
-- Primary key
ALTER TABLE yaf_Smiley ADD  PRIMARY KEY(SmileyID);

ALTER TABLE yaf_Smiley ADD  FOREIGN KEY(BoardID)
REFERENCES yaf_Board(BoardID)   ON DELETE NO ACTION  ON UPDATE NO ACTION;

ALTER TABLE yaf_Smiley ADD  UNIQUE(BoardID,Code);

--
CREATE TABLE yaf_UserForum(
   UserID INTEGER  NOT NULL,
   ForumID INTEGER  NOT NULL,
   AccessMaskID INTEGER  NOT NULL,
   Invited TIMESTAMP  NOT NULL,
   Accepted BOOLEAN  NOT NULL
);
-- Primary key
ALTER TABLE yaf_UserForum ADD  PRIMARY KEY(UserID,ForumID);

ALTER TABLE yaf_UserForum ADD  FOREIGN KEY(UserID)
REFERENCES OrionsBelt_Users(user_id)   ON DELETE NO ACTION  ON UPDATE NO ACTION;

ALTER TABLE yaf_UserForum ADD  FOREIGN KEY(AccessMaskID)
REFERENCES yaf_AccessMask(AccessMaskID)   ON DELETE NO ACTION  ON UPDATE NO ACTION;

ALTER TABLE yaf_UserForum ADD  FOREIGN KEY(ForumID)
REFERENCES yaf_Forum(ForumID)   ON DELETE NO ACTION  ON UPDATE NO ACTION;

--yaf_UserGroup
CREATE TABLE yaf_UserGroup(
   UserID INTEGER  NOT NULL,
   GroupID INTEGER  NOT NULL
);
-- Primary key
ALTER TABLE yaf_UserGroup ADD  PRIMARY KEY(UserID,GroupID);

ALTER TABLE yaf_UserGroup ADD  FOREIGN KEY(UserID)
REFERENCES OrionsBelt_Users(user_id)   ON DELETE NO ACTION  ON UPDATE NO ACTION;

ALTER TABLE yaf_UserGroup ADD  FOREIGN KEY(GroupID)
REFERENCES yaf_Group(GroupID)   ON DELETE NO ACTION  ON UPDATE NO ACTION;

--yaf_UserPMessage
CREATE SEQUENCE yaf_UserPMessage_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE yaf_UserPMessage(
   UserPMessageID INTEGER DEFAULT NEXTVAL('yaf_UserPMessage_seq')   NOT NULL,
   UserID INTEGER  NOT NULL,
   PMessageID INTEGER  NOT NULL,
   IsRead BOOLEAN  NOT NULL
);
-- Primary key
ALTER TABLE yaf_UserPMessage ADD  PRIMARY KEY(UserPMessageID);

ALTER TABLE yaf_UserPMessage ADD  FOREIGN KEY(UserID)
REFERENCES OrionsBelt_Users(user_id)   ON DELETE NO ACTION  ON UPDATE NO ACTION;

ALTER TABLE yaf_UserPMessage ADD  FOREIGN KEY(PMessageID)
REFERENCES yaf_PMessage(PMessageID)   ON DELETE NO ACTION  ON UPDATE NO ACTION;

--yaf_WatchForum
CREATE SEQUENCE yaf_WatchForum_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE yaf_WatchForum(
   WatchForumID INTEGER DEFAULT NEXTVAL('yaf_WatchForum_seq')   NOT NULL,
   ForumID INTEGER  NOT NULL,
   UserID INTEGER  NOT NULL,
   Created TIMESTAMP  NOT NULL,
   LastMail TIMESTAMP
);
-- Primary key
ALTER TABLE yaf_WatchForum ADD  PRIMARY KEY(WatchForumID);

ALTER TABLE yaf_WatchForum ADD  FOREIGN KEY(UserID)
REFERENCES OrionsBelt_Users(user_id)   ON DELETE NO ACTION  ON UPDATE NO ACTION;

ALTER TABLE yaf_WatchForum ADD  FOREIGN KEY(ForumID)
REFERENCES yaf_Forum(ForumID)   ON DELETE NO ACTION  ON UPDATE NO ACTION;

ALTER TABLE yaf_WatchForum ADD  UNIQUE(ForumID,UserID);

--yaf_WatchTopic
CREATE SEQUENCE yaf_WatchTopic_seq INCREMENT BY 1 START WITH 1;
CREATE TABLE yaf_WatchTopic(
   WatchTopicID INTEGER DEFAULT NEXTVAL('yaf_WatchTopic_seq')   NOT NULL,
   TopicID INTEGER  NOT NULL,
   UserID INTEGER  NOT NULL,
   Created TIMESTAMP  NOT NULL,
   LastMail TIMESTAMP
);
-- Primary key
ALTER TABLE yaf_WatchTopic ADD  PRIMARY KEY(WatchTopicID);

ALTER TABLE yaf_WatchTopic ADD  FOREIGN KEY(UserID)
REFERENCES OrionsBelt_Users(user_id)   ON DELETE NO ACTION  ON UPDATE NO ACTION;

ALTER TABLE yaf_WatchTopic ADD  FOREIGN KEY(TopicID)
REFERENCES yaf_Topic(TopicID)   ON DELETE NO ACTION  ON UPDATE NO ACTION;

ALTER TABLE yaf_WatchTopic ADD  UNIQUE(TopicID,UserID);


CREATE TABLE OrionsBelt_Battles (
   battle_id INTEGER NOT NULL,
   battle_data BYTEA NOT NULL
);

