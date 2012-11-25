--inserir dados na tabela OrionsBelt_MasterSkins;
INSERT INTO OrionsBelt_MasterSkins (masterSkin_name,masterSkin_style,masterSkin_script,masterSkin_description,masterSkin_count) VALUES ('skins/galaxy','styles/default.css','scripts/default.js','galaxy',3);
INSERT INTO OrionsBelt_MasterSkins (masterSkin_name,masterSkin_style,masterSkin_script,masterSkin_description,masterSkin_count) VALUES ('skins/light','	styles/default.css','scripts/default.js','light',1);
INSERT INTO OrionsBelt_MasterSkins (masterSkin_name,masterSkin_style,masterSkin_script,masterSkin_description,masterSkin_count) VALUES ('skins/rocks','	styles/default.css','scripts/default.js','rocks',1);
INSERT INTO OrionsBelt_MasterSkins (masterSkin_name,masterSkin_style,masterSkin_script,masterSkin_description,masterSkin_count) VALUES ('skins/planetaria','styles/default.css','scripts/default.js','planetaria',1);
INSERT INTO OrionsBelt_MasterSkins (masterSkin_name,masterSkin_style,masterSkin_script,masterSkin_description,masterSkin_count) VALUES ('skins/eva','styles/default.css','scripts/default.js','eva',1);

--inserir dados na tabela OrionsBelt_Sections;
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_skin,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (-1,'Index','index',1,'Alnitak.Index','Initial Page',0,0,1);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (1,'Ruler','home','Alnitak.Home','Rulers Home',0,1,0);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (2,'Planets','planets','Alnitak.BasePageModule','Planet section',0,2,1);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (1,'Admin','admin','Alnitak.BasePageModule','Administration',0,20,1);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (3,'Planet','planet','Alnitak.BasePageModule','Planet',0,4,1);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (5,'Buildings','buildings','Alnitak.BasePageModule','Buildings',0,5,1);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (2,'Military','military','Alnitak.BasePageModule','Military',0,6,1);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (5,'Army','army','Alnitak.BasePageModule','Army',0,1,1);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (5,'Fleet','fleet','Alnitak.BasePageModule','Fleet',0,2,1);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (5,'Mechs','mechs','Alnitak.BasePageModule','Mechs',0,3,1);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (2,'Research','research','Alnitak.BasePageModule','Research',0,7,1);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (4,'ExceptionLog','exceptionLog','Alnitak.BasePageModule','ExceptionLog',0,1,1);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (2,'Battle','battle','Alnitak.Battle.Battle','Battle',0,8,1);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (1,'Docs','docs','Alnitak.Docs','Docs',0,3,0);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (5,'Scan','scan','Alnitak.BasePageModule','Scan',0,4,1);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (4,'AddNews','addNews','Alnitak.BasePageModule','AddNews',0,1,2);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (1,'Wiki','wiki','Alnitak.Wiki','Wiki',0,9,0);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (5,'Tele','tele','Alnitak.BasePageModule','Teletransportation',0,7,1);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (1,'Forum','forum','Alnitak.BasePageModule','Forum',0,10,0);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (3,'Resources','Resources','Alnitak.ResourcesOverview','Resources',0,1,1);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (3,'Ships','Ships','Alnitak.ShipsOverview','Ships',0,2,1);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (4,'ForumAdmin','ForumAdmin','Alnitak.ForumAdmin','ForumAdmin',0,3,1);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (13,'FriendlyBattle','FriendlyBattle','Alnitak.BasePageModule','FriendlyBattle',0,1,1);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (5,'Market','Market','Alnitak.BasePageModule','Market',0,8,1);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (4,'BattleAdmin','BattleAdmin','Alnitak.BasePageModule','BattleAdmin',0,4,1);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (3,'ScanOverview','ScanOverview','Alnitak.BasePageModule','ScanOverview',0,3,1);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (5,'Barracks','Barracks','Alnitak.BasePageModule','Barracks',0,13,1);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (1,'Tournament','tournament','Alnitak.BasePageModule','Tournaments',0,1,0);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (4,'TAdmin','tadmin','Alnitak.BasePageModule','Tournaments Administration',0,5,1);
INSERT INTO OrionsBelt_Sections (section_parentId,section_name,section_title,section_content,section_description,section_iconId,section_order,section_isVisible) VALUES (2,'Alliance','alliance','Alnitak.BasePageModule','Alliance Administration',0,9,1);

--inserir dados na tabela OrionsBelt_NamedPage;
INSERT INTO OrionsBelt_NamedPage(namedPage_path,namedPage_name,namedPage_title,namedPage_content,namedPage_description) VALUES ('/faq.aspx','Faq','Faq','Alnitak.BasePageModule','Frequently asked questions');
INSERT INTO OrionsBelt_NamedPage(namedPage_path,namedPage_name,namedPage_title,namedPage_content,namedPage_description) VALUES ('/index.aspx','index','index','Alnitak.Index','Index');
INSERT INTO OrionsBelt_NamedPage(namedPage_path,namedPage_name,namedPage_title,namedPage_content,namedPage_description) VALUES ('/login.aspx','login','login','Alnitak.BasePageModule','Login');
INSERT INTO OrionsBelt_NamedPage(namedPage_path,namedPage_name,namedPage_title,namedPage_content,namedPage_description) VALUES ('/logout.aspx','logout','logout','Alnitak.Logout','Logout');
INSERT INTO OrionsBelt_NamedPage(namedPage_path,namedPage_name,namedPage_title,namedPage_content,namedPage_description) VALUES ('/regist.aspx','regist','regist','Alnitak.BasePageModule','Regist');
INSERT INTO OrionsBelt_NamedPage(namedPage_path,namedPage_name,namedPage_title,namedPage_content,namedPage_description) VALUES ('/addruler.aspx','AddRuler','AddRuler','Alnitak.AddRuler','Adiciona um Ruler');
INSERT INTO OrionsBelt_NamedPage(namedPage_path,namedPage_name,namedPage_title,namedPage_content,namedPage_description) VALUES ('/profile.aspx','profile','profile','Alnitak.BasePageModule','Profile');
INSERT INTO OrionsBelt_NamedPage(namedPage_path,namedPage_name,namedPage_title,namedPage_content,namedPage_description) VALUES ('/battle.aspx','battle','battle','Alnitak.BattlePage','Batalha');
INSERT INTO OrionsBelt_NamedPage(namedPage_path,namedPage_name,namedPage_title,namedPage_content,namedPage_description) VALUES ('/orionsbelterror.aspx','OrionsBeltError','OrionsBeltError','Alnitak.BasePageModule','OrionsBeltError');
INSERT INTO OrionsBelt_NamedPage(namedPage_path,namedPage_name,namedPage_title,namedPage_content,namedPage_description) VALUES ('/toprulers.aspx','TopRuler','TopRulers','Alnitak.TopRulers','Página do Top');
INSERT INTO OrionsBelt_NamedPage(namedPage_path,namedPage_name,namedPage_title,namedPage_content,namedPage_description) VALUES ('/userinfo.aspx','UserInfo','UserInfo','Alnitak.UserInfo','Informação do Utilizador');
INSERT INTO OrionsBelt_NamedPage(namedPage_path,namedPage_name,namedPage_title,namedPage_content,namedPage_description) VALUES ('/stats.aspx','Stats','Stats','Alnitak.BasePageModule','Stats');

INSERT INTO OrionsBelt_NamedPage(namedPage_path,namedPage_name,namedPage_title,namedPage_content,namedPage_description) VALUES ('/prizes.aspx','Prizes','prizes','Alnitak.Prizes','Página de Prémios');
INSERT INTO OrionsBelt_NamedPage(namedPage_path,namedPage_name,namedPage_title,namedPage_content,namedPage_description) VALUES ('/scanreport.aspx','ScanReport','scanReport','Alnitak.ScanReport','scanReport');
INSERT INTO OrionsBelt_NamedPage(namedPage_path,namedPage_name,namedPage_title,namedPage_content,namedPage_description) VALUES ('/resetpassword.aspx','ResetPassword','resetPassword','Alnitak.BasePageModule','Password Reset');

INSERT INTO OrionsBelt_NamedPage(namedPage_path,namedPage_name,namedPage_title,namedPage_content,namedPage_description) VALUES ('/contact.aspx','Contact','Contact','Alnitak.BasePageModule','Contact');

INSERT INTO OrionsBelt_NamedPage(namedPage_path,namedPage_name,namedPage_title,namedPage_content,namedPage_description) VALUES ('/media.aspx','Media','Media','Alnitak.BasePageModule','Media');
INSERT INTO OrionsBelt_NamedPage(namedPage_path,namedPage_name,namedPage_title,namedPage_content,namedPage_description) VALUES ('/artwork.aspx','ArtWork','ArtWork','Alnitak.BasePageModule','ArtWork');
INSERT INTO OrionsBelt_NamedPage(namedPage_path,namedPage_name,namedPage_title,namedPage_content,namedPage_description) VALUES ('/supports.aspx','Supports','Supports','Alnitak.BasePageModule','Supports');
INSERT INTO OrionsBelt_NamedPage(namedPage_path,namedPage_name,namedPage_title,namedPage_content,namedPage_description) VALUES ('/about.aspx','About','About','Alnitak.BasePageModule','About');

INSERT INTO OrionsBelt_NamedPage(namedPage_path,namedPage_name,namedPage_title,namedPage_content,namedPage_description) VALUES ('/topranks.aspx','TopRanks','TopRanks','Alnitak.BasePageModule','Top Ranks');
INSERT INTO OrionsBelt_NamedPage(namedPage_path,namedPage_name,namedPage_title,namedPage_content,namedPage_description) VALUES ('/rankcalculator.aspx','RankCalculator','RankCalculator','Alnitak.BasePageModule','RankCalculator');

INSERT INTO OrionsBelt_NamedPage(namedPage_path,namedPage_name,namedPage_title,namedPage_content,namedPage_description) VALUES ('/allianceinfo.aspx','AllianceInfo','AllianceInfo','Alnitak.BasePageModule','AllianceInfo');
INSERT INTO OrionsBelt_NamedPage(namedPage_path,namedPage_name,namedPage_title,namedPage_content,namedPage_description) VALUES ('/topalliances.aspx','TopAlliances','TopAlliances','Alnitak.BasePageModule','Top Alliances');


--inserir dados na tabela de OrionsBelt_Roles;
INSERT INTO OrionsBelt_Roles(roles_roleName,roles_roleDescription) values ('guest','role de guest');
INSERT INTO OrionsBelt_Roles(roles_roleName,roles_roleDescription) values ('user','role de um utilizador normal');
INSERT INTO OrionsBelt_Roles(roles_roleName,roles_roleDescription) values ('ruler','role de jogador');
INSERT INTO OrionsBelt_Roles(roles_roleName,roles_roleDescription) values ('admin','role de administrador');
INSERT INTO OrionsBelt_Roles(roles_roleName,roles_roleDescription) values ('betaTester','role de designer');
INSERT INTO OrionsBelt_Roles(roles_roleName,roles_roleDescription) values ('artist','role de artistas');


-- inserir utilizadores;
INSERT INTO OrionsBelt_Users(-1,'05-10-2004 10:09:52','admin@orionsbelt.net','D033E22AE348AEB5660FC2140AEC35850C4DA997','admin',1,'pt',0,0);


--inserir dados na tabela de OrionsBelt_UserRoles;
INSERT INTO OrionsBelt_UserRoles(user_id,roles_id) VALUES (1,4);

--inserir dados na tabela OrionsBelt_SectionRoles;
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (1,1);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (1,2);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (1,3);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (2,3);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (3,3);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (4,4);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (5,3);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (6,3);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (7,3);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (8,3);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (9,3);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (10,3);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (11,3);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (12,4);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (13,3);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (14,3);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (15,3);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (16,4);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (17,4);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (18,3);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (19,3);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (20,3);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (21,3);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (22,4);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (23,3);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (24,3);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (25,4);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (26,3);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (27,3);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (28,3);
INSERT INTO OrionsBelt_SectionRoles(sectionRoles_section_id,sectionRoles_role_id) values (29,4);



CREATE OR REPLACE FUNCTION orionsbelt_usersregisteruser("varchar", "varchar", "varchar", "varchar")
RETURNS INTEGER AS
$BODY$
DECLARE
	flags INTEGER;
	rank_id INTEGER;
	id INTEGER;
BEGIN
	INSERT INTO OrionsBelt_Users(user_mail,user_pass,user_nick,user_lang,user_rankid,user_flags) VALUES ($1,$2,$3,$4,3,2);
	select user_id INTO id from OrionsBelt_Users where $1 = user_mail;
	insert into yaf_UserGroup(UserID,GroupID) select id,GroupID from yaf_Group where BoardID = 1 and (2 & 4) <> 0;
	RETURN 1;
END
$BODY$
LANGUAGE 'plpgsql' VOLATILE;



