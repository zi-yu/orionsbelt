-- MySQL dump 9.11
--
-- Host: localhost    Database:
-- ------------------------------------------------------
-- Server version	4.0.20-log

--
-- Current Database: orionsbelt
--

CREATE DATABASE /*!32312 IF NOT EXISTS*/ orionsbelt;

USE orionsbelt;

--
-- Table structure for table `Exceptions`
--

CREATE TABLE Exceptions (
  Id int(11) NOT NULL auto_increment,
  Name varchar(100) NOT NULL default '',
  Message mediumtext NOT NULL,
  StackTrace longtext NOT NULL,
  Date datetime NOT NULL default '0000-00-00 00:00:00',
  PRIMARY KEY  (Id)
) TYPE=MyISAM;

--
-- Dumping data for table `Exceptions`
--

--
-- Table structure for table `News`
--

CREATE TABLE News (
  Id int NOT NULL auto_increment,
  Title varchar(100) NOT NULL,
  Content mediumtext NOT NULL,
  Date datetime NOT NULL default '0000-00-00 00:00:00',
  PRIMARY KEY  (Id)
) TYPE=MyISAM;

--
-- Dumping data for table `News`
--

--
-- Table structure for table `Scans`
--

CREATE TABLE Scans (
  Id int NOT NULL auto_increment,
  SourcePlanetId int NOT NULL,
  Target varchar(20) NOT NULL,
  RareResources varchar(100) NOT NULL,
  ScanLevel int NOT NULL,
  Intercepted  int NOT NULL,
  Succeded int NOT NULL,
  Turn  int NOT NULL,
  
  Culture  int NOT NULL,
  HasCommsSatellite  int NOT NULL,
  HasGate  int NOT NULL,
  HasStarGate  int NOT NULL,
  HasStarPort  int NOT NULL,
  InBattle  int NOT NULL,
  
  FleetCount int NOT NULL,
  TargetPlanetOwner int NOT NULL,
  TotalShips  int NOT NULL,
  TotalSoldiers  int NOT NULL,
  TotalBarracks  int NOT NULL,
  
  PRIMARY KEY  (Id)
) TYPE=MyISAM;

--
-- Dumping data for table `Scans`
--

--
-- Table structure for table `MasterSkins`
--

CREATE TABLE MasterSkins (
  masterSkin_id int(11) NOT NULL auto_increment,
  masterSkin_name varchar(30) NOT NULL default '',
  masterSkin_style varchar(30) NOT NULL default '',
  masterSkin_script varchar(30) NOT NULL default '',
  masterSkin_description varchar(50) default NULL,
  masterSkin_count int(11) default '1',
  PRIMARY KEY  (masterSkin_id)
) TYPE=MyISAM;

--
-- Dumping data for table `MasterSkins`
--

INSERT INTO MasterSkins VALUES (1,'skins/galaxy','styles/default.css','scripts/default.js','Galaxy',3);
INSERT INTO MasterSkins VALUES (2,'skins/light','styles/default.css','scripts/default.js','Light',1);
INSERT INTO MasterSkins VALUES (3,'skins/rocks','styles/default.css','scripts/default.js','Rocks',1);
INSERT INTO MasterSkins VALUES (4,'skins/planetaria','styles/default.css','scripts/default.js','Planetaria',1);

--
-- Table structure for table `NamedPage`
--

CREATE TABLE NamedPage (
  namedPage_id int(11) NOT NULL auto_increment,
  namedPage_path varchar(30) NOT NULL default '',
  namedPage_name varchar(30) NOT NULL default '',
  namedPage_title varchar(30) NOT NULL default '',
  namedPage_Content varchar(30) NOT NULL default '',
  namedPage_description varchar(30) default NULL,
  PRIMARY KEY  (namedPage_id)
) TYPE=MyISAM;

--
-- Dumping data for table `NamedPage`
--

INSERT INTO NamedPage VALUES (1,'/index.aspx','Index','Index','Alnitak.BasePageModule','Index');
INSERT INTO NamedPage VALUES (2,'/faq.aspx','Faq','Faq','Alnitak.BasePageModule','Frequently asked questions');
INSERT INTO NamedPage VALUES (3,'/login.aspx','Login','Login','Alnitak.BasePageModule','Login');
INSERT INTO NamedPage VALUES (4,'/logout.aspx','Logout','Logout','Alnitak.Logout','Logout');
INSERT INTO NamedPage VALUES (5,'/regist.aspx','Regist','Regist','Alnitak.BasePageModule','Regist');
INSERT INTO NamedPage VALUES (6,'/profile.aspx','Profile','Profile','Alnitak.BasePageModule','Profile');
INSERT INTO NamedPage VALUES (7,'/addruler.aspx','AddRuler','AddRuler','Alnitak.AddRuler','AddRuler');
INSERT INTO NamedPage VALUES (8,'/battle.aspx','Battle','Battle','Alnitak.BattlePage','Batalha');
INSERT INTO NamedPage VALUES (9,'/planet.aspx','Planet','Planet','Alnitak.BasePageModule','Planet Info');
INSERT INTO NamedPage VALUES (10,'/buildings.aspx','buildings','buildings','Alnitak.Buildings','Buildings');
INSERT INTO NamedPage VALUES (11,'/toprulers.aspx','TopRulers','TopRulers','Alnitak.BasePageModule','Top Rulers');
INSERT INTO NamedPage VALUES (12,'/userinfo.aspx','UserInfo','UserInfo','Alnitak.UserInfo','UserInfo');
INSERT INTO NamedPage VALUES (13,'/prizes.aspx','Prizes','Prizes','Alnitak.Prizes','Prizes');
INSERT INTO NamedPage VALUES (14,'/scanreport.aspx','scanReport','scanReport','Alnitak.ScanReport','scanReport');
INSERT INTO NamedPage VALUES (15,'/resetpassword.aspx','ResetPassword','ResetPassword','Alnitak.BasePageModule','ResetPassword');
INSERT INTO NamedPage VALUES (16,'/contact.aspx','Contact','Contact','Alnitak.BasePageModule','Contact');
INSERT INTO NamedPage VALUES (17,'/stats.aspx','Stats','Stats','Alnitak.BasePageModule','Stats');
INSERT INTO NamedPage VALUES (18,'/about.aspx','About','About','Alnitak.BasePageModule','About');
INSERT INTO NamedPage VALUES (19,'/media.aspx','Media','Media','Alnitak.BasePageModule','Media');
INSERT INTO NamedPage VALUES (20,'/artwork.aspx','ArtWork','ArtWork','Alnitak.BasePageModule','ArtWork');
INSERT INTO NamedPage VALUES (21,'/supports.aspx','Supports','Supports','Alnitak.BasePageModule','Supports');
INSERT INTO NamedPage VALUES (22,'/orionsbelterror.aspx','OrionsBeltError','OrionsBeltError','Alnitak.BasePageModule','OrionsBeltError');
INSERT INTO NamedPage VALUES (23,'/topranks.aspx','TopRanks','TopRanks','Alnitak.BasePageModule','Top Ranks');
INSERT INTO NamedPage VALUES (24,'/rankcalculator.aspx','RankCalculator','RankCalculator','Alnitak.BasePageModule','RankCalculator');
INSERT INTO NamedPage VALUES (25,'/topplanets.aspx','TopPlanets','TopPlanets','Alnitak.BasePageModule','TopPlanets');

INSERT INTO NamedPage VALUES (26,'/allianceinfo.aspx','AllianceInfo','AllianceInfo','Alnitak.BasePageModule','AllianceInfo');
INSERT INTO NamedPage VALUES (27,'/topalliances.aspx','TopAlliances','TopAlliances','Alnitak.BasePageModule','Top Alliances');

--
-- Table structure for table `Roles`
--

CREATE TABLE Roles (
  IDRoles int(11) NOT NULL auto_increment,
  roles_roleName varchar(20) NOT NULL default '',
  roles_roleDescription varchar(50) default NULL,
  PRIMARY KEY  (IDRoles)
) TYPE=MyISAM;

--
-- Dumping data for table `Roles`
--

INSERT INTO Roles VALUES (1,'guest','role de guest');
INSERT INTO Roles VALUES (2,'user','role de um utilizador normal');
INSERT INTO Roles VALUES (3,'ruler','role de jogador');
INSERT INTO Roles VALUES (4,'admin','role de administrador');
INSERT INTO Roles VALUES (5,'betaTester','role de beta tester');
INSERT INTO Roles VALUES (6,'artist','role de artistas');

--
-- Table structure for table `SectionRoles`
--

CREATE TABLE SectionRoles (
  sectionRoles_section_id int(11) NOT NULL default '0',
  sectionRoles_role_id int(11) NOT NULL default '0',
  PRIMARY KEY  (sectionRoles_role_id,sectionRoles_section_id)
) TYPE=MyISAM;

--
-- Dumping data for table `SectionRoles`
--

INSERT INTO SectionRoles VALUES (1,1);
INSERT INTO SectionRoles VALUES (14,1);
INSERT INTO SectionRoles VALUES (1,2);
INSERT INTO SectionRoles VALUES (14,2);
INSERT INTO SectionRoles VALUES (1,3);
INSERT INTO SectionRoles VALUES (2,3);
INSERT INTO SectionRoles VALUES (3,3);
INSERT INTO SectionRoles VALUES (5,3);
INSERT INTO SectionRoles VALUES (6,3);
INSERT INTO SectionRoles VALUES (7,3);
INSERT INTO SectionRoles VALUES (8,3);
INSERT INTO SectionRoles VALUES (9,3);
INSERT INTO SectionRoles VALUES (11,3);
INSERT INTO SectionRoles VALUES (12,3);
INSERT INTO SectionRoles VALUES (14,3);
INSERT INTO SectionRoles VALUES (4,4);

--
-- Table structure for table `Sections`
--

CREATE TABLE Sections (
  section_id int(11) NOT NULL auto_increment,
  section_parentId int(11) NOT NULL default '0',
  section_name varchar(30) NOT NULL default '',
  section_title varchar(30) NOT NULL default '',
  section_skin int(11) default NULL,
  section_Content varchar(30) NOT NULL default '',
  section_description varchar(30) default NULL,
  section_iconId int(11) default NULL,
  section_order int(11) NOT NULL default '0',
  section_isVisible int(11) NOT NULL default '0',
  PRIMARY KEY  (section_id)
) TYPE=MyISAM;

--
-- Dumping data for table `Sections`
--

INSERT INTO Sections VALUES (1,-1,'Index','index',3,'Alnitak.Index','Initial Page',0,0,0);
INSERT INTO Sections VALUES (2,1,'Ruler','home',3,'Alnitak.Home','Rulers Home',0,1,1);
INSERT INTO Sections VALUES (3,2,'Planets','planets',3,'Alnitak.BasePageModule','Planet section',0,2,1);
INSERT INTO Sections VALUES (4,1,'Admin','admin',3,'Alnitak.BasePageModule','Administraction',0,20,1);
INSERT INTO Sections VALUES (5,3,'Planet','planet',3,'Alnitak.BasePageModule','Planet',0,3,1);
INSERT INTO Sections VALUES (6,5,'Buildings','buildings',3,'Alnitak.BasePageModule','Buildings',0,4,1);
INSERT INTO Sections VALUES (7,2,'Military','military',3,'Alnitak.BasePageModule','Military',0,5,1);
INSERT INTO Sections VALUES (8,5,'Army','army',3,'Alnitak.BasePageModule','Army',0,6,1);
INSERT INTO Sections VALUES (9,5,'Fleet','fleet',3,'Alnitak.BasePageModule','Fleet',0,7,1);
INSERT INTO Sections VALUES (10,5,'Mechs','mechs',3,'Alnitak.BasePageModule','Mechs',0,7,1);
INSERT INTO Sections VALUES (11,2,'Research','research',3,'Alnitak.BasePageModule','Research',0,6,1);
INSERT INTO Sections VALUES (12,2,'Battle','battle',3,'Alnitak.Battle.Battle','Battle',0,6,1);
INSERT INTO Sections VALUES (13,4,'ExceptionLog','exceptionLog',3,'Alnitak.BasePageModule','ExceptionLog',0,1,1);
INSERT INTO Sections VALUES (14,1,'Docs','Docs',3,'Alnitak.Docs','Docs',0,2,0);
INSERT INTO Sections VALUES (15,5,'Scan','Scan',3,'Alnitak.BasePageModule','Scan',0,10,1);
INSERT INTO Sections VALUES (16,4,'AddNews','AddNews',3,'Alnitak.BasePageModule','AddNews',0,1,1);
INSERT INTO Sections VALUES (17,5,'Tele','Tele',3,'Alnitak.BasePageModule','Tele',0,11,1);
INSERT INTO Sections VALUES (18,1,'Wiki','Wiki',3,'Alnitak.Wiki','Wiki',0,3,0);
INSERT INTO Sections VALUES (19,3,'Resources','Resources',3,'Alnitak.ResourcesOverview','Resources',0,1,1);
INSERT INTO Sections VALUES (20,3,'Ships','Ships',3,'Alnitak.ShipsOverview','Ships',0,2,1);
INSERT INTO Sections VALUES (21,5,'Market','Market',3,'Alnitak.BasePageModule','Market',0,12,1);
INSERT INTO Sections VALUES (22,3,'ScanOverview','ScanOverview',3,'Alnitak.BasePageModule','ScanOverview',0,3,1);
INSERT INTO Sections VALUES (23,5,'Barracks','Barracks',3,'Alnitak.BasePageModule','Barracks',0,13,1);
INSERT INTO Sections VALUES (24,1,'Tournament','tournament',5,'Alnitak.BasePageModule','Tournaments',0,1,0);
INSERT INTO Sections VALUES (25,4,'TAdmin','tadmin',5,'Alnitak.BasePageModule','Tournaments Administration',0,0,1);
INSERT INTO Sections VALUES (25,2,'Shop','shop',5,'Alnitak.BasePageModule','OB Shop',0,0,0);
INSERT INTO Sections VALUES (26,2,'Alliance','alliance',5,'Alnitak.BasePageModule','Alliance Administration',0,0,1);


--
-- Table structure for table `UserRoles`
--

CREATE TABLE UserRoles (
  IDUserRoles int(11) NOT NULL auto_increment,
  IDUser int(11) NOT NULL default '0',
  IDRoles int(11) NOT NULL default '0',
  PRIMARY KEY  (IDUserRoles)
) TYPE=MyISAM;

--
-- Dumping data for table `UserRoles`
--

INSERT INTO UserRoles VALUES (1,1,4);

--
-- Table structure for table `Users`
--

CREATE TABLE Users (
  IDUsers int(11) NOT NULL auto_increment,
  user_ruler_id int(11) default NULL,
  user_alliance_id int(11) default NULL,
  user_alliance_rank varchar(100) default NULL,
  user_regist datetime default NULL,
  user_lastLogin datetime default NULL,
  user_mail varchar(30) NOT NULL default '',
  user_pass varchar(40) NOT NULL default '',
  user_nick varchar(30) NOT NULL default '',
  user_website varchar(60) NOT NULL default '',
  user_avatar varchar(30) NOT NULL default '',
  user_skin int(11) NOT NULL default '0',
  user_lang varchar(5) NOT NULL default '',
  user_imagesDir varchar(30) NOT NULL default '',
  user_msn varchar(30) NOT NULL default '',
  user_icq varchar(30) NOT NULL default '',
  user_jabber varchar(30) NOT NULL default '',
  user_aim varchar(30) NOT NULL default '',
  user_yahoo varchar(30) NOT NULL default '',
  user_signature varchar(255) NOT NULL default '',
  user_rank int(11) NOT NULL default '1000',
  PRIMARY KEY  (IDUsers)
) TYPE=MyISAM;

--
-- Dumping data for table `Users`
--

INSERT INTO Users VALUES (1,-1,'2004-11-21 21:40:17','pre@psantos.net','5b4632ac6cc0704b5785e0c02bcac90ba4e0d46c','Kunami','','',3,'pt','','','','','','');
INSERT INTO Users VALUES (2,-1,'2004-12-01 19:22:39','buu@buu.pt','5b4632ac6cc0704b5785e0c02bcac90ba4e0d46c','Buu','','',2,'pt','','','','','','');
INSERT INTO Users VALUES (3,-1,'0000-00-00 00:00:00','buu-siege@buu.pt','5b4632ac6cc0704b5785e0c02bcac90ba4e0d46c','Buu','','',1,'en-US','','','','','','');
INSERT INTO Users VALUES (4,-1,'0000-00-00 00:00:00','siege@orionsbelt.pt','371c5e970653b90ee344a3de0c07c4f30b1f6a5c','Siege','','',2,'en','','','','','','');
INSERT INTO Users VALUES (5,-1,'2004-12-04 11:07:55','teste2@teste2.pt','5b4632ac6cc0704b5785e0c02bcac90ba4e0d46c','buu','','',2,'en','','','','','','');
INSERT INTO Users VALUES (6,-1,'0000-00-00 00:00:00','pre@oninetspeed.pt','5b4632ac6cc0704b5785e0c02bcac90ba4e0d46c','Dominador','','',2,'en','','','','','','');
INSERT INTO Users VALUES (7,-1,'0000-00-00 00:00:00','teste4@teste.pt','5b4632ac6cc0704b5785e0c02bcac90ba4e0d46c','Teste','','',2,'en','','','','','','');
INSERT INTO Users VALUES (8,-1,'2004-12-04 19:54:18','teste5@teste.pt','5b4632ac6cc0704b5785e0c02bcac90ba4e0d46c','Teste','','',2,'en','','','','','','');
INSERT INTO Users VALUES (9,-1,'2004-12-04 20:23:39','teste6@teste.pt','5b4632ac6cc0704b5785e0c02bcac90ba4e0d46c','Teste','','',2,'en','','','','','','');
INSERT INTO Users VALUES (10,-1,'0000-00-00 00:00:00','teste10@teste.pt','5b4632ac6cc0704b5785e0c02bcac90ba4e0d46c','Teste','','',2,'en','','','','','','');
INSERT INTO Users VALUES (11,-1,'2004-12-06 18:39:37','teste11@teste.pt','5b4632ac6cc0704b5785e0c02bcac90ba4e0d46c','PRE','','',2,'en','','','','','','');
INSERT INTO Users VALUES (12,-1,'0000-00-00 00:00:00','teste12@teste.pt','5b4632ac6cc0704b5785e0c02bcac90ba4e0d46c','PRE','','',2,'en','','','','','','');
INSERT INTO Users VALUES (13,-1,'0000-00-00 00:00:00','teste13@teste.pt','5b4632ac6cc0704b5785e0c02bcac90ba4e0d46c','PRE','','',2,'en','','','','','','');
INSERT INTO Users VALUES (14,-1,'0000-00-00 00:00:00','axanta@oninetspeed.pt','bd554690fefc45888160cd2e6032be8259b1c1ab','aXanta','','',1,'pt','','','','','','');
INSERT INTO Users VALUES (15,1,'0000-00-00 00:00:00','firefox@orion.orion','5b4632ac6cc0704b5785e0c02bcac90ba4e0d46c','Firefox','http://psantos.net','',2,'en','','','','','','');
INSERT INTO Users VALUES (16,2,'2004-12-23 13:22:16','mozilla@mozilla.org','5b4632ac6cc0704b5785e0c02bcac90ba4e0d46c','Mozilla','','',1,'pt','','','','','','');
INSERT INTO Users VALUES (17,-1,'0000-00-00 00:00:00','psantos@oninetspeed.pt','5b4632ac6cc0704b5785e0c02bcac90ba4e0d46c','Pedro Santos','','',2,'en','','','','','','');
INSERT INTO Users VALUES (18,-1,'0000-00-00 00:00:00','pyro@oninet.pt','9266a1dc680d7c19228e02bc5fe5e30a37e51f08','Pyro','','',1,'en-US','','','','','','');
INSERT INTO Users VALUES (19,-1,'2004-12-31 11:47:20','ola@ola.pt','5b4632ac6cc0704b5785e0c02bcac90ba4e0d46c','Ol','','',2,'en','','','','','','');

--
-- Table structure for table `Alliance`
--

CREATE TABLE Alliance (
  alliance_id int(11) NOT NULL auto_increment,
  alliance_name varchar(150) NOT NULL default '',
  alliance_tag varchar(150) NOT NULL default '{0}',
  alliance_motto varchar(450) NOT NULL default '',
  alliance_rank int(11) NOT NULL default '1000',
  alliance_rankBattles int(11) NOT NULL default '0',
  alliance_regist datetime default NULL,
  PRIMARY KEY  (alliance_id)
) TYPE=MyISAM;

--
-- Dumping data for table `Alliance`
--

--
-- Current Database: test
--

CREATE DATABASE /*!32312 IF NOT EXISTS*/ test;

USE test;

