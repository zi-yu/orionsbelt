// created on 4/26/04 at 10:58 a

using System;
using System.Collections;
using System.Web;
using System.Text;
using System.IO;
using System.Data;
using ByteFX.Data;
using ByteFX.Data.MySqlClient;
using Chronos.Utils;

namespace Alnitak {

	public class MySqlUserUtility : UserUtility {
	
		private MySqlConnection conn;
	
		/// <summary>Construtor</summary>
		public MySqlUserUtility()
		{
			conn = new MySqlConnection( OrionGlobals.getConnectionString("connectiostring-mysql") );
		}

		/// <summary>Verfica se existe um user com o mail e pass passados</summary>
		public override bool checkUser( string mail, string pass )
		{
			string query = string.Format("SELECT IDUsers FROM Users WHERE `user_mail` = '{0}' and `user_pass` = '{1}'",mail, hashPassword(pass));
			HttpContext.Current.Trace.Write("MySQL","Query String: " + query);
			
			try {
				conn.Open();
				MySqlCommand cmd = conn.CreateCommand();
				cmd.CommandText = query;
				
				MySqlDataReader reader = cmd.ExecuteReader();
				reader.Read();
				
				HttpContext.Current.Trace.Write("MySQL","Has Rows: " + reader.HasRows.ToString());
				return reader.HasRows;
				
			} catch( Exception e ) {
				HttpContext.Current.Trace.Warn("MySQL",e.Message);
			} finally {
				conn.Close();
			}
			return false;
		}

		/// <summary>Verfica se existe um user com o mail passado</summary>
		public override string checkUser( int id )
		{
			string query = string.Format("SELECT IDUsers FROM Users WHERE `IDUsers` = '{0}'",id);
			HttpContext.Current.Trace.Write("MySQL","Query String: " + query);
			
			try {
				conn.Open();
				MySqlCommand cmd = conn.CreateCommand();
				cmd.CommandText = query;
				
				MySqlDataReader reader = cmd.ExecuteReader();
				reader.Read();
				HttpContext.Current.Trace.Write("MySQL","Has Rows: " + reader.HasRows.ToString());
				return reader["user_nick"].ToString();
				
			} catch( Exception e ) {
				HttpContext.Current.Trace.Warn("MySQL",e.Message);
			} finally {
				conn.Close();
			}
			return null;
		}
		
		/// <summary>Verfica se existe um user com o mail passado</summary>
		public override bool checkUser( string mail )
		{
			string query = string.Format("SELECT IDUsers FROM Users WHERE `user_mail` = '{0}'",mail);
			HttpContext.Current.Trace.Write("MySQL","Query String: " + query);
			
			try {
				conn.Open();
				MySqlCommand cmd = conn.CreateCommand();
				cmd.CommandText = query;
				
				MySqlDataReader reader = cmd.ExecuteReader();
				reader.Read();
				
				HttpContext.Current.Trace.Write("MySQL","Has Rows: " + reader.HasRows.ToString());
				return reader.HasRows;
				
			} catch( Exception e ) {
				HttpContext.Current.Trace.Warn("MySQL",e.Message);
			} finally {
				conn.Close();
			}
			return false;
		}
		
		/// <summary>Regista um User</summary>
		public override void registerUser( string nick, string mail, string pass )
		{
			/*
				INSERT INTO Users(user_id, user_ruler_id, user_regist, user_mail, user_pass, user_nick, user_website, user_avatar, user_skin, user_lang, user_msn, user_icq, user_jabber, user_aim, user_yahoo, user_signature)
    			{0} -> date
    			{1} -> mail
    			{2} -> pass
    			{3} -> nick
    			'YYYY-MM-DD HH:MM:SS'
    		*/

			StringWriter writer = new StringWriter();
			DateTime now = DateTime.Now;
			
			string date = now.Year + "-" + now.Month + "-" + now.Day + " " + now.Hour + "-" + now.Minute + "-" + now.Millisecond;

			writer.Write("INSERT INTO Users(IDUsers, user_ruler_id, user_regist, user_mail, user_pass, user_nick, user_website, user_avatar, user_skin, user_lang, user_msn, user_icq, user_jabber, user_aim, user_yahoo, user_signature) ");
			writer.Write("VALUES('', -1, NOW(), '{1}', '{2}', '{3}', '', '', 3, '{4}', '', '', '', '', '', '')",
					date,
					mail,
					hashPassword(pass),
					nick,
					System.Threading.Thread.CurrentThread.CurrentUICulture.ToString()
				);
			string query = writer.ToString();

			HttpContext.Current.Trace.Write("MySQL","Query String: " + query);
			
			try {
				conn.Open();
				MySqlCommand cmd = conn.CreateCommand();
				cmd.CommandText = query;
				
				int reader = cmd.ExecuteNonQuery();
				
				HttpContext.Current.Trace.Write("MySQL","Result: " + reader.ToString());
				
			} catch( Exception e ) {
				HttpContext.Current.Trace.Warn("MySQL",e.Message);
			} finally {
				conn.Close();
			}
		}
		
		/// <summary>Obtém um User dado o seu id</summary>
		public override User getUser( int id )
		{
			User user = new User();
			string query = string.Format("SELECT * FROM Users WHERE `IDUsers` = {0}", id);
			if( !fillUserInformation(user, query) ) {
				return null;
			}
			return user;
		}
		
		/// <summary>Preenche um User com os seus dados</summary>
		public override void fillUser( User user )
		{
			string query = string.Format("SELECT * FROM Users WHERE `user_mail` LIKE '{0}'",user.Mail);
			fillUserInformation(user, query);
		}
		
		/// <summary>Preenche um User com os seus dados</summary>
		private bool fillUserInformation( User user, string query )
		{
			HttpContext.Current.Trace.Write("MySQL","Query String: " + query);
			
			try {
				conn.Open();
				MySqlCommand cmd = conn.CreateCommand();
				cmd.CommandText = query;
				
				MySqlDataReader reader = cmd.ExecuteReader();
				reader.Read();
				
				HttpContext.Current.Trace.Write("MySQL","Has Rows: " + reader.HasRows.ToString());

				user.Nick = (string) reader["user_nick"];
				user.Mail = (string) reader["user_mail"];
				user.UserId = (int) reader["IDUsers"];
				user.RulerId = (int) reader["user_ruler_id"];
				user.EloRanking = (int) reader["user_rank"];
				user.AllianceId = (int) reader["user_alliance_id"];
				user.AllianceRank = AllianceInfo.ToAllianceRank((string) reader["user_alliance_rank"]);
			
				if( reader["user_regist"] != DBNull.Value ) {
					user.RegistDate = (DateTime) reader["user_regist"];
				} else {
					user.RegistDate = DateTime.Now;
				}

				if( reader["user_lastLogin"] != DBNull.Value ) {
					user.LastLogin = (DateTime) reader["user_lastLogin"];
				} else {
					user.LastLogin = DateTime.Now;
				}

				user.Skin = (int) reader["user_skin"];
				user.Lang = (string) reader["user_lang"];
				user.ImagesDir = (string) reader["user_imagesDir"];

				user.Website = (string) reader["user_website"];
				user.Avatar = (string) reader["user_avatar"];

				user.Msn = (string) reader["user_msn"];
				user.Icq = (string) reader["user_icq"];
				user.Jabber = (string) reader["user_jabber"];
				user.Aim = (string) reader["user_aim"];
				user.Yahoo = (string) reader["user_yahoo"];
				
				user.SetRoles( getRoles(user.UserId) );
				
				return true;
				
			} catch( Exception e ) {
				HttpContext.Current.Trace.Warn("MySQL",e.Message);
				Console.WriteLine("EXCEPTION: " + e);
				return false;
			} finally {
				conn.Close();
			}
		}
		
		/// <summary>Guarda um User com os seus dados na Base de Dados</summary>
		public override void saveUser( User user, string password )
		{
			/*
				UPDATE Users
    			user_mail='', user_pass='',
				user_msn='', user_icq='', user_jabber='', user_aim='', user_yahoo='', user_signature='', user_imagesDir=''
    			WHERE user_id=
			*/

			//
			
			StringWriter writer = new StringWriter();
			writer.Write("UPDATE Users SET ");
			writer.Write("user_ruler_id={0}, ", user.RulerId);
			
			writer.Write("user_nick='{0}', ", user.Nick);
			writer.Write("user_skin={0}, ", user.Skin);
			writer.Write("user_lang='{0}', ", user.Lang);
			
			writer.Write("user_website='{0}', ", user.Website);
			writer.Write("user_avatar='{0}', ", user.Avatar);
			writer.Write("user_rank='{0}', ", user.EloRanking);
			writer.Write("user_alliance_id='{0}', ", user.AllianceId);
			writer.Write("user_alliance_rank='{0}', ", AllianceInfo.FromAllianceRole(user.AllianceRank));
			
			writer.Write("user_msn='{0}', ", user.Msn);
			writer.Write("user_icq='{0}', ", user.Icq);
			writer.Write("user_jabber='{0}', ", user.Jabber);
			writer.Write("user_aim='{0}', ", user.Aim);
			writer.Write("user_yahoo='{0}', ", user.Yahoo);
			writer.Write("user_signature='{0}', ", user.Signature);
			writer.Write("user_imagesDir='{0}' ", user.ImagesDir);

			writer.Write("WHERE IDUsers={0}", user.UserId);

			string query = writer.ToString();
			HttpContext.Current.Trace.Write("MySQL","Query String: " + query);
			
			try {
				conn.Open();
				MySqlCommand cmd = conn.CreateCommand();
				cmd.CommandText = query;
				
				cmd.ExecuteNonQuery();
				
			} catch( Exception e ) {
				HttpContext.Current.Trace.Warn("MySQL",e.Message);
				Console.WriteLine("Update exception: " + e.Message);
			} finally {
				conn.Close();
			}
		}
		
		/// <summary>Retorna as roles de um User</summary>
		public override string[] getRoles( string userMail )
		{
			return new string[0];
		}
		
		/// <summary>Retorna as roles de um User dado o seu ID</summary>
		public string[] getRoles( int id )
		{
			string query = string.Format("SELECT roles_roleName FROM `UserRoles` inner join `Roles` on UserRoles.IDRoles=Roles.IDRoles WHERE UserRoles.IDUser={0}", id);
			DataSet ds = MySqlUtility.getQuery(query);
			
			string[] roles = new string[ds.Tables[0].Rows.Count];
			int i = 0;
			
			foreach( DataRow row in ds.Tables[0].Rows ) {
				roles[i++] = row[0].ToString();
			}
			
			return roles;
		}
		
		/// <summary>Coloca o campo RulerId = -1 para todos os Users</summary>
		public override void resetUserRulerId()
		{
			string query = "UPDATE Users SET user_ruler_id=-1";
			try {
				conn.Open();
				MySqlCommand cmd = conn.CreateCommand();
				cmd.CommandText = query;
				
				cmd.ExecuteNonQuery();
				
			} catch( Exception e ) {
				Console.WriteLine("Update exception: " + e.Message);
			} finally {
				conn.Close();
			}
		}
		
		/// <summary>Obtm todos os utilizadores com a role passada</summary>
		public override int[] getUsers( int role )
		{
			string query = string.Format("select IDUsers from Users inner join UserRoles on Users.IDUsers = UserRoles.IDUser where UserRoles.IDRoles = {0}", role);
			HttpContext.Current.Trace.Write("MySQL","Query String: " + query);
			
			return fetchUsersIds( MySqlUtility.getQuery(query) );
		}
		
		/// <summary>Obtm todos ordenados por Ranking</summary>
		public override User[] getUsersRanking()
		{
			int count = 50;
		
			string query = string.Format("select * from Users order by user_rank desc limit 0, {0}", count);
			DataSet ds = MySqlUtility.getQuery(query);

			return PopulateUserRankings(count, ds, "IDUsers");
		}

		/// <summary>Obtm a quantidade de utilizadores registados</summary>
		public override int getUserCount()
		{
			return MySqlUtility.executeNonQuery("select count(*) from Users");
		}

		/// <summary>Actualiza a data do ltimo login</summary>
		public override void updateLastLogin( string userMail )
		{
			MySqlUtility.executeNonQuery(
				string.Format("update Users set user_lastLogin = NOW() where user_mail = '{0}'",
							  	userMail
							  )
			);
		}

		/// <summary>Modifica a password de um utilizador</summary>
		public override bool resetPassword( string userMail, string newPassword )
		{
			if( !checkUser(userMail) ) {
				return false;
			}
			
			MySqlUtility.executeNonQuery(
				string.Format("update Users set user_pass = '{0}' where user_mail = '{1}'",
							  newPassword, userMail
						)
			);
			
			return true;
		}

		public override string getAvatar( int rulerId ) 
		{
			return string.Empty;
		}
		
		/// <summary>Obtm todos ordenados por Ranking</summary>
		public override IList getInactiveUsers()
		{
			DateTime now = DateTime.Now;
			
			int m = now.Month - 1;
			if( m <= 0 ) {
				m = 12;
			}
		
			string query = string.Format("select * from Users where user_ruler_id!=-1 and user_lastlogin <  '{0}-{1}-{2}'", now.Year, GetNumber(m), GetNumber(now.Day));
			Log.log(query);
			DataSet ds = MySqlUtility.getQuery(query);

			return PopulateUserRankings(ds.Tables[0].Rows.Count, ds, "IDUsers");
		}
		
		public override void setAllianceMembers( AllianceInfo info ){
			string query = string.Format("SELECT * FROM `Users` where abs(user_alliance_id) = {0}", info.Id);
			DataSet ds = MySqlUtility.getQuery(query);
			ArrayList all = PopulateUsers(ds, "IDUsers");
			
			ArrayList members = new ArrayList();
			ArrayList wannabe = new ArrayList();
			
			foreach( User user in all ) {
				if( user.AllianceId < 0 ) {
					wannabe.Add(user);
				} else {
					members.Add(user);
				}
			}
			
			info.Members = members;
			info.Wannabe = wannabe;
		}
		
		private string GetNumber( int n )
		{
			if( n < 10 ) {
				return string.Format("0{0}", n);
			}
			return n.ToString();
		}
		
	};

}
