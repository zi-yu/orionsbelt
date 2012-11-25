CREATE or replace FUNCTION OrionsBelt_ChronosCountMessages (INTEGER,VARCHAR)
RETURNS BIGINT AS '
SELECT COUNT(message_read)
FROM orionsbelt_messages
WHERE message_id = $1 AND message_identifier = $2 AND message_read = false
'LANGUAGE SQL;

CREATE OR REPLACE FUNCTION orionsbelt_chronosremovebattle(int4)
RETURNS INTEGER AS'
DELETE FROM OrionsBelt_battles WHERE battle_id=$1;
select 1;
'LANGUAGE 'sql';

CREATE OR REPLACE FUNCTION orionsbelt_chronosloadmessage("varchar", "varchar", "varchar", "varchar")
RETURNS SETOF orionsbelt_messages AS
$$
DECLARE
	SQL VARCHAR(1000);
BEGIN
	SQL := 'SELECT message_data
	FROM OrionsBelt_Messages WHERE message_id = ' || $1 || ' AND message_identifier = ' || $2 || ' ' || $4 || ' ORDER BY message_uniqueId DESC LIMIT ' || $3;
	EXECUTE SQL;
	
END;
$$
LANGUAGE 'plpgsql';

CREATE or replace FUNCTION OrionsBelt_ChronosLoadBattle (INTEGER)
RETURNS BYTEA AS '
SELECT battle_data FROM OrionsBelt_Battles WHERE battle_id = $1
' LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_ChronosLoadUniverse()
RETURNS SETOF BYTEA AS '
SELECT data FROM OrionsBelt_Universe ORDER BY date DESC LIMIT 1;
'  LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_ChronosMarkAllAsRead(INTEGER,VARCHAR)
RETURNS INTEGER AS '
UPDATE OrionsBelt_Messages
SET message_read = true 
WHERE message_read = false AND message_id = $1 AND message_identifier = $2;
SELECT 1;
' LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_ChronosSaveMessage (INTEGER,VARCHAR,VARCHAR,BYTEA)
RETURNS INTEGER AS '
INSERT INTO OrionsBelt_Messages (message_id,message_identifier,message_type,message_data) VALUES ($1,$2,$3,$4);
SELECT 1;
' LANGUAGE SQL;

CREATE OR REPLACE FUNCTION OrionsBelt_ChronosSaveBattle(INTEGER,BYTEA)
RETURNS void AS
$BODY$
DECLARE
	myrec RECORD;
BEGIN
	SELECT INTO myrec * FROM OrionsBelt_Battles WHERE battle_id=$1;
	IF NOT FOUND THEN
		INSERT INTO OrionsBelt_Battles(battle_id,battle_data) VALUES ($1,$2);
	ELSE
		UPDATE OrionsBelt_Battles SET battle_data=$2 WHERE battle_id=$1;
	END IF;
	RETURN;
END;
$BODY$
LANGUAGE 'plpgsql';

CREATE OR REPLACE FUNCTION orionsbelt_chronossaveuniverse(bytea)
RETURNS void AS
$$
DECLARE 
	c INTEGER;
	id_ INTEGER;
BEGIN
	SELECT COUNT(*) INTO c FROM OrionsBelt_Universe;
	IF( c = 5 ) THEN
	BEGIN
		SELECT id INTO id_ FROM OrionsBelt_Universe ORDER BY date ASC;
		UPDATE OrionsBelt_Universe SET data = $1,date = LOCALTIMESTAMP WHERE id = id_;
	END;
	ELSE
		INSERT INTO OrionsBelt_Universe (data) VALUES ($1);
	END IF;
	RETURN;
END;
$$
LANGUAGE 'plpgsql';

CREATE or replace FUNCTION OrionsBelt_ExceptionLogLoad()
RETURNS SETOF OrionsBelt_ExceptionLog AS '
SELECT exceptionLog_id,exceptionLog_name,exceptionLog_message, exceptionLog_stackTrace, exceptionLog_date 
FROM OrionsBelt_ExceptionLog
ORDER BY exceptionLog_date DESC;
' LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_ExceptionLogRemoveAllExceptions()
RETURNS INTEGER AS'
DELETE FROM OrionsBelt_ExceptionLog;
SELECT 1;
' LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_ExceptionLogRemoveException(INTEGER)
RETURNS INTEGER AS'
DELETE FROM OrionsBelt_ExceptionLog WHERE exceptionLog_id = $1;
SELECT 1;
' LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_ExceptionLogSave (VARCHAR,VARCHAR,VARCHAR,TIMESTAMP)
RETURNS INTEGER AS'
--name,message,stackTrace,date
INSERT INTO OrionsBelt_ExceptionLog(exceptionlog_name,exceptionlog_message,exceptionlog_stacktrace,exceptionlog_date) VALUES($1,$2,$3,$4);
SELECT 1;
' LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_GetNews()
RETURNS SETOF OrionsBelt_News AS'
SELECT * FROM OrionsBelt_News
ORDER BY news_date DESC
LIMIT 10;
' LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_GetScansById(INTEGER)
RETURNS SETOF OrionsBelt_Scans AS'
SELECT * FROM OrionsBelt_Scans WHERE scans_id = $1 LIMIT 10;
' LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_GetScansByPlanetId(INTEGER)
RETURNS SETOF OrionsBelt_Scans AS'
SELECT * FROM OrionsBelt_Scans
WHERE scans_sourcePlanetId = $1
ORDER BY scans_id DESC
LIMIT 10;
' LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_InsertScans(INTEGER,BYTEA)
RETURNS INTEGER AS'
INSERT INTO OrionsBelt_Scans(scans_sourceplanetid,scans_data) VALUES($1,$2);
SELECT 1;
' LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_MasterSkinsGetAllMasterSkins()
RETURNS SETOF OrionsBelt_MasterSkins AS'
SELECT * FROM OrionsBelt_MasterSkins
' LANGUAGE SQL;


CREATE or replace FUNCTION OrionsBelt_NamedPagesGetAllNamedPages()
RETURNS SETOF OrionsBelt_NamedPage AS'
SELECT * FROM OrionsBelt_NamedPage;
' LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_SectionsGetAllSections()
RETURNS SETOF OrionsBelt_Sections AS'
SELECT * FROM OrionsBelt_Sections
' LANGUAGE SQL;


CREATE or replace FUNCTION OrionsBelt_SectionsGetAllSectionsRoles (INTEGER)
RETURNS SETOF VARCHAR AS'
SELECT roles_roleName
FROM OrionsBelt_SectionRoles inner join OrionsBelt_Roles on sectionRoles_role_id = roles_id
WHERE sectionroles_section_id = $1
' LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_SectionsGetMotherSections()
RETURNS INTEGER AS'
SELECT section_id FROM OrionsBelt_Sections WHERE section_parentId < 2;
' LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_UsersCheckUser (VARCHAR,CHAR)
RETURNS INTEGER AS'
SELECT user_id
FROM OrionsBelt_Users
WHERE user_mail = $1 AND user_pass = $2;
' LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_UsersCheckUserMail(VARCHAR)
RETURNS INTEGER AS'
SELECT user_id
FROM OrionsBelt_Users
WHERE user_mail = $1;
' LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_UsersGetAvatar (INTEGER)
RETURNS VARCHAR AS'
SELECT user_avatar
FROM OrionsBelt_Users
WHERE user_ruler_id = $1;
' LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_UsersGetCount()
RETURNS BIGINT AS'
SELECT COUNT(*) FROM OrionsBelt_Users;
' LANGUAGE SQL;


CREATE or replace FUNCTION OrionsBelt_UsersGetMailFromId(INTEGER)
RETURNS VARCHAR AS'
SELECT user_mail FROM OrionsBelt_Users WHERE user_id = $1;
' LANGUAGE SQL;


CREATE or replace FUNCTION OrionsBelt_UsersGetUser (VARCHAR)
RETURNS SETOF OrionsBelt_Users AS'
SELECT *
FROM OrionsBelt_Users
WHERE user_mail = $1;
' LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_UsersGetUserById(INTEGER)
RETURNS SETOF OrionsBelt_Users AS'
SELECT * FROM Orionsbelt_Users WHERE user_id = $1;
' LANGUAGE SQL;

CREATE or replace FUNCTION orionsbelt_usersgetuserroles (VARCHAR)
RETURNS VARCHAR AS'
SELECT roles_roleName
FROM Orionsbelt_Roles as roles INNER JOIN OrionsBelt_UserRoles as userRoles
ON roles.roles_id = userRoles.roles_id
WHERE user_id = (SELECT user_id FROM Orionsbelt_Users WHERE user_mail = $1);
' LANGUAGE SQL;

CREATE OR REPLACE FUNCTION orionsbelt_usersgetuserroles("varchar")
 RETURNS "varchar" AS
$BODY$
SELECT roles_roleName
FROM Orionsbelt_Roles as roles INNER JOIN OrionsBelt_UserRoles as userRoles
ON roles.roles_id = userRoles.roles_id
WHERE user_id = (SELECT user_id FROM Orionsbelt_Users WHERE user_mail = $1);
$BODY$
 LANGUAGE 'sql' VOLATILE;

CREATE or replace FUNCTION OrionsBelt_UsersGetUsersIdByRole(INTEGER)
RETURNS INTEGER AS'
SELECT user_id
FROM OrionsBelt_UserRoles
WHERE roles_id = $1;
' LANGUAGE SQL;

CREATE OR REPLACE FUNCTION orionsbelt_usersregisteruser("varchar", "varchar", "varchar", "varchar")
RETURNS void AS
$BODY$
DECLARE
	flags INTEGER;
	rank_id INTEGER;
	id INTEGER;
BEGIN
	INSERT INTO OrionsBelt_Users(user_mail,user_pass,user_nick,user_lang,user_rankid,user_flags) VALUES ($1,$2,$3,$4,3,2);
	select user_id INTO id from OrionsBelt_Users where $1 = user_mail;
	insert into yaf_UserGroup(UserID,GroupID) select id,GroupID from yaf_Group where BoardID = 1 and (2 & 4) <> 0;
	RETURN;
END
$BODY$
LANGUAGE 'plpgsql';

CREATE or replace FUNCTION OrionsBelt_UsersResetPassword(varchar,varchar)
RETURNS INTEGER AS'
update orionsbelt_users set user_pass = $2
where user_mail = $1;
SELECT 1;
' LANGUAGE SQL;


CREATE or replace FUNCTION OrionsBelt_UsersResetUserRulerId()
RETURNS INTEGER AS'
INSERT INTO OrionsBelt_Users(user_ruler_Id) VALUES ( -1 );
SELECT 1;
' LANGUAGE SQL;


CREATE or replace FUNCTION OrionsBelt_UsersUpdateLastLogin (VARCHAR)
RETURNS INTEGER AS'
UPDATE OrionsBelt_Users
SET user_lastLogin = LOCALTIMESTAMP
WHERE user_mail = $1;
SELECT 1;
' LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_UsersResetUserRulerId()
RETURNS SET AS'

' LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_UsersGetUserRanks()
RETURNS SETOF orionsbelt_users AS'
SELECT * FROM orionsbelt_users
ORDER BY user_rank DESC
LIMIT 50;
' LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_UsersGetInative(timestamp)
RETURNS SETOF orionsbelt_users AS'
SELECT * FROM orionsbelt_users
where user_ruler_id != -1 AND user_lastlogin < $1
' LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_AllianceGetAllianceMembers(int)
RETURNS SETOF orionsbelt_users AS'
SELECT * FROM orionsbelt_users where abs(user_alliance_id) = $1
' LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_AllianceGetAlliance()
RETURNS SETOF orionsbelt_alliance AS'
SELECT * FROM orionsbelt_alliance
' LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_AllianceGetAllianceById(int)
RETURNS SETOF orionsbelt_alliance AS'
select * from orionsbelt_alliance where alliance_id = $1;
' LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_AllianceSaveAlliance(varchar,varchar,varchar,int,int,int )
RETURNS void AS'
UPDATE orionsbelt_alliance
SET alliance_name = $1,
alliance_tag = $2,
alliance_motto = $3,
alliance_rank = $4,
alliance_rankBattles = $5
where alliance_id = $6
' LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_AllianceRegisterAlliance(varchar,varchar,varchar,int,int )
RETURNS BIGINT AS'
insert into orionsbelt_alliance(alliance_name, alliance_tag, alliance_motto, alliance_rank, alliance_rankBattles) values ($1,$2,$3,$4,$5);
select last_value from orionsbelt_alliance_seq;
' LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_InsertNews(VARCHAR,VARCHAR,VARCHAR)
RETURNS INTEGER AS'
INSERT INTO OrionsBelt_News(news_title, news_content, news_lang) VALUES($1,$2,$3);
SELECT 1;
' LANGUAGE SQL;


CREATE or replace FUNCTION OrionsBelt_GetNewsByLang(VARCHAR)
RETURNS SETOF OrionsBelt_News AS'
SELECT * FROM OrionsBelt_News
WHERE news_lang = $1
ORDER BY news_date DESC
LIMIT 10;
' LANGUAGE SQL;

CREATE or replace FUNCTION OrionsBelt_ChronosLoadUniverse()
RETURNS SETOF BYTEA AS '
SELECT data FROM OrionsBelt_Universe ORDER BY date DESC LIMIT 1;
'  LANGUAGE SQL;

CREATE OR REPLACE FUNCTION orionsbelt_chronossavebattle(int4, bytea, int4)
  RETURNS void AS
$BODY$
DECLARE
	myrec RECORD;
BEGIN
	SELECT INTO myrec * FROM OrionsBelt_Battles WHERE battle_id=$1;
	IF NOT FOUND THEN
		INSERT INTO OrionsBelt_Battles(battle_id,battle_data,battle_rulerid) VALUES ($1,$2,$3);
	ELSE
		UPDATE OrionsBelt_Battles SET battle_data=$2, battle_rulerid = $3 WHERE battle_id=$1;
	END IF;
	RETURN;
END;
$BODY$
LANGUAGE 'plpgsql' VOLATILE;

CREATE or replace FUNCTION OrionsBelt_ChronosLoadRulerId (INTEGER)
RETURNS INTEGER AS '
SELECT battle_rulerid FROM OrionsBelt_Battles WHERE battle_id = $1
' LANGUAGE SQL;


CREATE or replace FUNCTION OrionsBelt_ChronosSaveBattleTurn (INTEGER,INTEGER)
RETURNS void AS '
UPDATE OrionsBelt_Battles SET battle_rulerid = $2 WHERE battle_id=$1;
' LANGUAGE SQL;


CREATE OR REPLACE FUNCTION orionsbelt_usersupdateuser(int4, "varchar", int4, "varchar", int4, "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", int4, int4, "varchar", int4, int4)
RETURNS INTEGER AS $$
BEGIN
	UPDATE OrionsBelt_Users SET
		user_ruler_id = $3,
		user_nick  = $4,
		user_skin = $5,
		user_lang = $6,
		user_website  = $7,
		user_avatar  = $8,
		user_msn  = $9,
		user_icq  = $10,
		user_jabber  = $11,
		user_aim  = $12,
		user_yahoo  = $13,
		user_signature = $14,
		user_imagesDir = $15,
		user_rank = $16,
		user_alliance_id = $17,
		user_alliance_rank = $18,
		user_wins = $19,
		user_losses = $20
	WHERE user_id = $1;
	IF( $2 = '' )THEN
		RETURN 1;
	END IF;
	UPDATE OrionsBelt_Users SET user_pass=$2 WHERE user_id = $1;
	RETURN 1;
END; $$
LANGUAGE plpgsql;