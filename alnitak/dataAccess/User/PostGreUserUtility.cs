using System;
using System.Collections;
using System.Data;
using Alnitak.Exceptions;
using Alnitak.PostGre;
using NpgsqlTypes;
using Npgsql;
using Chronos.Utils;

namespace Alnitak {

	/// <summary>
	/// classe de acesso ao user no SqlServer
	/// </summary>
	public class PostGreUserUtility : UserUtility {

		private NpgsqlDataReader reader = null;

		#region Private

		private object getField( string columnName ) {
			try {
				return reader[columnName];
			} catch {
				throw new AlnitakException( string.Format( "Campo {0} no existe na tabela dos Users!", columnName ) );
			}
		}

		private IList PopulateInativeUsers(DataSet ds) {
			ArrayList users = new ArrayList();
			foreach( DataRow row in ds.Tables[0].Rows ) {
				User user = new User();
				
				user.UserId = (int) row[0];
				user.RulerId = (int) row[1];
				user.Nick = (string) row[6];
				
				users.Add( user );
			}
			return users;
		}

		#endregion

		#region Public

		/// <summary>
		/// Verfica se existe um user com o mail e pass passados
		/// </summary>
		public override bool checkUser( string mail, string pass ) {
		
			pass = hashPassword(pass);
			
			Log.log("checkUser({0}, {1})", mail, pass);
			string query = string.Format("select orionsbelt_userscheckuser('{0}','{1}')", mail, pass);
			DataSet ds = PostGreServerUtility.getFromDBWithQuery(query);
			return !(ds.Tables[0].Rows[0][0] is DBNull);
		}

		public override string checkUser(int id) {
			PostGre.PostGreParam [] param = new PostGreParam[1];
			param[0] = new PostGreParam(id,NpgsqlDbType.Integer);
			
			DataSet ds = PostGreServerUtility.getFromDB("OrionsBelt_UsersGetMailFromId",param);
			
			if( ds.Tables.Count == 0 )
				throw new AlnitakException("O Dataset deveria ter pelo menos 1 resultado @ SqlServerUserUtility::checkUser(int id)");

			return ds.Tables[0].Rows[0]["user_mail"].ToString();
		}

		
		/// <summary>
		/// Verifica se existe um user com o mail passado
		/// </summary>
		public override bool checkUser( string mail ) {
			Hashtable param = new Hashtable();
			
			param.Add("@user_mail",mail);
			
			return PostGreServerUtility.checkResults("OrionsBelt_UsersCheckUserMail",param);
		}
		
		
		/// <summary>
		/// Regista um User
		/// </summary>
		public override void registerUser( string nick, string mail, string pass ) {
			PostGreParam[] param = new PostGreParam[4];

			param[0] = new PostGreParam(mail,NpgsqlDbType.Varchar,30);
			param[1] = new PostGreParam(hashPassword(pass),NpgsqlDbType.Varchar,40);
			param[2] = new PostGreParam(nick,NpgsqlDbType.Varchar,30);
			param[3] = new PostGreParam(CultureModule.RequestLanguage,NpgsqlDbType.Varchar,5);
			
			PostGreServerUtility.executeNonQuery2("OrionsBelt_UsersRegisterUser", param);
		}
		
		/// <summary>Obtém um User dado o seu id</summary>
		public override User getUser( int id ) {
			NpgsqlConnection conn = new NpgsqlConnection(OrionGlobals.getConnectionString("connectionStringPG"));
			NpgsqlCommand cmd = new NpgsqlCommand("OrionsBelt_UsersGetUserById", conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;
			
			cmd.Parameters.Add("@id",id);
			
			try {
				conn.Open();
				
				reader = cmd.ExecuteReader();
				if( reader.Read() ) {
					User user = new User();

					user.Nick = (string) reader["user_nick"];
					user.UserId = (int) reader["user_id"];
					user.RulerId = (int) reader["user_ruler_id"];
					user.EloRanking = (int) reader["user_rank"];
				
					user.RegistDate = (DateTime) getField("user_registDate");
					user.LastLogin = (DateTime) getField("user_lastLogin");
					
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

					user.AllianceId = (int) reader["user_alliance_id"];
					user.AllianceRank = AllianceInfo.ToAllianceRank( reader["user_alliance_rank"].ToString() );

					user.Wins = (int) reader["user_wins"];
					user.Losses = (int) reader["user_losses"];
					
					return user;

				}else
					return null;
				
			} catch( Exception e ) {
				throw new AlnitakException("Erro ao executar o procedimento 'OrionsBelt_UsersGetUserById' @ NpgsqlServerUserutility::getUser : " + e.Message,e);
			} finally {
				conn.Close();
			}
		}
		
		/// <summary>
		/// Preenche um User com os seus dados
		/// </summary>
		public override void fillUser( User user ) {
			NpgsqlConnection conn = new NpgsqlConnection(OrionGlobals.getConnectionString("connectionStringPG"));
			NpgsqlCommand cmd = new NpgsqlCommand("OrionsBelt_UsersGetUser", conn);
			cmd.CommandType=CommandType.StoredProcedure;
			cmd.CommandTimeout = 0;
			
			cmd.Parameters.Add("@user_mail",user.Mail);
			
			try {
				conn.Open();
				
				reader = cmd.ExecuteReader();
				
				if( reader.HasRows ) {
					reader.Read();
					
					user.Nick = (string) getField("user_nick");
					user.UserId = (int) getField("user_id");
					
					if ( getField("user_ruler_id") != DBNull.Value )
						user.RulerId = (int) getField("user_ruler_id");

					user.EloRanking = (int) getField("user_rank");

					user.RegistDate = (DateTime) getField("user_registDate");
					user.LastLogin = (DateTime) getField("user_lastLogin");
					
					user.Skin = (int) getField("user_skin");
					user.Lang = (string) getField("user_lang");
					user.ImagesDir = (string) getField("user_imagesDir");
			
					user.Website = (string) getField("user_website");
					user.Avatar = (string) getField("user_avatar");

					user.Msn = (string) getField("user_msn");
					user.Icq = (string) getField("user_icq");
					user.Jabber = (string) getField("user_jabber");
					user.Aim = (string) getField("user_aim");
					user.Yahoo = (string) getField("user_yahoo");

					user.AllianceId = (int) reader["user_alliance_id"];
					user.AllianceRank = AllianceInfo.ToAllianceRank( reader["user_alliance_rank"].ToString() );

					user.Wins = (int) reader["user_wins"];
					user.Losses = (int) reader["user_losses"];

				}else {
					ExceptionLog.log("User não encontrado", "Mail: " + user.Mail==null?"null":user.Mail );

				}
				reader = null;
			} catch( NpgsqlException e ) {
				throw new AlnitakException("Erro ao executar o procedimento 'OrionsBelt_UsersGetUser' @ NpgsqlServerUserutility::fillUser ",e);
			} finally {
				conn.Close();
			}
		}
		
		/// <summary>
		/// Guarda um User com os seus dados na Base de Dados
		/// </summary>
		public override void saveUser( User user, string password ) {
			PostGreParam[] param = new PostGreParam[20];
			
			param[0] = new PostGreParam(user.UserId,NpgsqlDbType.Integer);

			if( password != string.Empty ) {
				param[1] = new PostGreParam(hashPassword(password),NpgsqlDbType.Varchar,40);
			}else {
				param[1] = new PostGreParam("",NpgsqlDbType.Varchar,40);
			}

			param[2] = new PostGreParam(user.RulerId,NpgsqlDbType.Integer);
			param[3] = new PostGreParam(user.Nick,NpgsqlDbType.Varchar,30);
			param[4] = new PostGreParam(user.Skin,NpgsqlDbType.Integer);
			param[5] = new PostGreParam(user.Lang,NpgsqlDbType.Varchar,5);
			param[6] = new PostGreParam(user.Website,NpgsqlDbType.Varchar,30);
			param[7] = new PostGreParam(user.Avatar,NpgsqlDbType.Varchar,250);
			param[8] = new PostGreParam(user.Msn,NpgsqlDbType.Varchar,30);
			param[9] = new PostGreParam(user.Icq,NpgsqlDbType.Varchar,30);
			param[10] = new PostGreParam(user.Jabber,NpgsqlDbType.Varchar,30);
			param[11] = new PostGreParam(user.Aim,NpgsqlDbType.Varchar,30);
			param[12] = new PostGreParam(user.Yahoo,NpgsqlDbType.Varchar,30);
			param[13] = new PostGreParam(user.Signature,NpgsqlDbType.Varchar,255);
			param[14] = new PostGreParam(user.ImagesDir,NpgsqlDbType.Varchar,100);
			param[15] = new PostGreParam(user.EloRanking,NpgsqlDbType.Integer);
			param[16] = new PostGreParam(user.AllianceId,NpgsqlDbType.Integer);
			param[17] = new PostGreParam(user.AllianceRank,NpgsqlDbType.Varchar,15);
			param[18] = new PostGreParam(user.Wins,NpgsqlDbType.Integer);
			param[19] = new PostGreParam(user.Losses,NpgsqlDbType.Integer);

			PostGreServerUtility.executeNonQuery2("OrionsBelt_UsersUpdateUser",param);
		}
		
		/// <summary>Retorna as roles de um User</summary>
		public override string[] getRoles( string userMail ) {
			PostGre.PostGreParam [] param = new PostGreParam[1];
			param[0] = new PostGreParam(userMail,NpgsqlDbType.Varchar,40);
			
			DataSet dataset = PostGreServerUtility.getFromDB("OrionsBelt_UsersGetUserRoles",param);
			string[] roles = new string[dataset.Tables[0].Rows.Count];
			
			int i = 0;
			bool insertMade = false;
			foreach ( DataRow role in dataset.Tables[0].Rows ) {
				if( !(role[0] is System.DBNull) ) {
					roles[i++] = (string)role[0];
					insertMade = true;
				}
			}

			if(insertMade) {
				return roles;
			}

			return new string[0];
		}

		/// <summary>
		/// faz reset ao id do user
		/// </summary>
		public override void resetUserRulerId() {
			PostGreServerUtility.executeNonQuery("OrionsBelt_UsersResetUserRulerId",null);
		}
		
		/// <summary>Obtm todos os utilizadores com a role passada</summary>
		public override int[] getUsers( int role ) {
			//(PostGre.PostGreParam [] p = new PostGreParam[1];
			//p[0] = new PostGreParam(role,NpgsqlDbType.Integer);
			
			//DataSet ds = PostGreServerUtility.getFromDB("OrionsBelt_UsersGetUsersIdByRole",p);
			DataSet ds = PostGreServerUtility.getFromDBWithQuery(string.Format("SELECT user_id FROM OrionsBelt_UserRoles WHERE roles_id ={0}",role));

			return fetchUsersIds( ds );
		}

		public override User[] getUsersRanking() {
			DataSet ds = PostGreServerUtility.getAllFromDB("OrionsBelt_UsersGetUserRanks");
			return PopulateUserRankings(50, ds, "user_id");
		}

		/// <summary>Obtm a quantidade de utilizadores registados</summary>
		public override int getUserCount()
		{
			DataSet ds = PostGreServerUtility.getAllFromDB("OrionsBelt_UsersGetCount");
			object o = ds.Tables[0].Rows[0][0];
			return (int)(Int64)o;
		}

		/// <summary>Actualiza a data do ltimo login</summary>
		public override void updateLastLogin( string userMail ) {
			Hashtable param = new Hashtable();
			param.Add("@user_mail",userMail);

			PostGreServerUtility.executeNonQuery("OrionsBelt_UsersUpdateLastLogin",param);
		}

		public override bool resetPassword(string userMail, string newPassword) {
			Hashtable param = new Hashtable();
			param.Add("@user_mail",userMail);
			
			bool result = PostGreServerUtility.checkResults("OrionsBelt_UsersCheckUserMail",param);
			
			if( result ) {
				param.Add("@user_password",newPassword);
				PostGreServerUtility.executeNonQuery("OrionsBelt_UsersResetPassword",param);
			}

			return result;
		}

		public override string getAvatar( int rulerId ) {
			PostGre.PostGreParam [] param = new PostGreParam[1];
			param[0] = new PostGreParam(rulerId,NpgsqlDbType.Integer);
			
			DataSet ds = PostGreServerUtility.getFromDB("OrionsBelt_UsersGetAvatar",param);

			object o = ds.Tables[0].Rows[0][0];

			if( ds.Tables[0].Rows.Count != 1 || o is System.DBNull ) {
				return User.DefaultAvatar;
			}
			string s = o.ToString();

			return s == string.Empty?User.DefaultAvatar:s;
		}

		public override IList getInactiveUsers() {
			PostGre.PostGreParam [] param = new PostGreParam[1];
			param[0] = new PostGreParam(DateTime.Now.AddMonths(-1),NpgsqlDbType.Date);
			
			DataSet ds = PostGreServerUtility.getFromDB("OrionsBelt_UsersGetInative",param);

			return PopulateInativeUsers(ds);
		}

		#endregion
		
		
		public override void setAllianceMembers( AllianceInfo info ) {
			PostGre.PostGreParam [] param = new PostGreParam[1];
			param[0] = new PostGreParam(info.Id,NpgsqlDbType.Integer);
			
			DataSet ds = PostGreServerUtility.getFromDB("OrionsBelt_AllianceGetAllianceMembers",param);
			
			ArrayList all = PopulateUsers(ds, "user_id");
			
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
	};
}
