delete from orionsbelt_universe;
delete from orionsbelt_messages;
delete from orionsbelt_battles;
delete from orionsbelt_exceptionlog;
delete from orionsbelt_scans;
update orionsbelt_users set user_ruler_id = -1 where user_ruler_id != -1;
