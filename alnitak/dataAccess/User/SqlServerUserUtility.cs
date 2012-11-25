using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Alnitak.Exceptions;
using Alnitak.MsSqlServer;

namespace Alnitak {

	/// <summary>
	/// classe de acesso ao user no SqlServer
	/// </summary>
	public class SqlServerUserUtility : UserUtility {

		private SqlDataReader reader = null;

		#region Private

		private object getField( string columnName ) {
			try {
				return reader[columnName];
			} catch {
				throw new AlnitakException( string.Format( "Campo {0} no existe na tabela dos Users!", columnName ) );
			}
		}

		#endregion

		#region Public

		/// <summary>
		/// Verfica se existe um user com o mail e pass passados
		/// </summary>
		public override bool checkUser( string mail, string pass ) {
			Hashtable param = new Hashtable();
			
			param.Add("@user_mail",mail);
			param.Add("@user_pass",hashPassword(pass));
			
			return SqlServerUtility.checkResults("OrionsBelt_UsersCheckUser",param);
			
		}

		public override string checkUser(int id) {
			Hashtable param = new Hashtable();
			
			param.Add("@user_id",id);
			
			DataSet ds = SqlServerUtility.getFromDB("OrionsBelt_UsersGetMailFromId",param);
			
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
			
			return SqlServerUtility.checkResults("OrionsBelt_UsersCheckUserMail",param);
		}
		
		
		/// <summary>
		/// Regista um User
		/// </summary>
		public override void registerUser( string nick, string mail, string pass ) {
			Hashtable param = new Hashtable();

			param.Add("@user_mail",mail);
			param.Add("@user_pass",hashPassword(pass));
			param.Add("@user_nick",nick);
			param.Add("@user_lang",CultureModule.RequestLanguage);
			SqlServerUtility.executeNonQuery("OrionsBelt_UsersRegisterUser",param);
		}
		
		/// <summary>Obtém um User dado o seu id</summary>
		public override User getUser( int id ) {
			SqlConnection conn = new SqlConnection(OrionGlobals.getConnectionString("connectionString"));
			SqlCommand cmd = new SqlCommand("OrionsBelt_UsersGetUserById", conn);
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
					
					return user;

				}else
					return null;
				
			} catch( Exception e ) {
				throw new AlnitakException("Erro ao executar o procedimento 'OrionsBelt_UsersGetUserById' @ SqlServerUserutility::getUser : " + e.Message,e);
			} finally {
				conn.Close();
			}
		}
		
		/// <summary>
		/// Preenche um User com os seus dados
		/// </summary>
		public override void fillUser( User user ) {
			SqlConnection conn = new SqlConnection(OrionGlobals.getConnectionString("connectionString"));
			SqlCommand cmd = new SqlCommand("OrionsBelt_UsersGetUser", conn);
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

					user.EloRanking = (int) reader["user_rank"];

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
				}else {
					ExceptionLog.log("User não encontrado", "Mail: " + user.Mail==null?"null":user.Mail );

				}
				reader = null;
			} catch( SqlException e ) {
				throw new AlnitakException("Erro ao executar o procedimento 'OrionsBelt_UsersGetUser' @ SqlServerUserutility::fillUser ",e);
			} finally {
				conn.Close();
			}
		}
		
		/// <summary>
		/// Guarda um User com os seus dados na Base de Dados
		/// </summary>
		public override void saveUser( User user, string password ) {
			Hashtable param = new Hashtable();

			param.Add("@user_id",user.UserId);
			if( password != string.Empty )
				param.Add("@user_pass",hashPassword(password) );

			param.Add("@user_ruler_id",user.RulerId);
			param.Add("@user_nick",user.Nick);
			param.Add("@user_skin",user.Skin);
			param.Add("@user_lang",user.Lang);
			param.Add("@user_website",user.Website);
			param.Add("@user_avatar",user.Avatar);
			param.Add("@user_msn",user.Msn);
			param.Add("@user_icq",user.Icq);
			param.Add("@user_jabber",user.Jabber);
			param.Add("@user_aim",user.Aim);
			param.Add("@user_yahoo",user.Yahoo);
			param.Add("@user_signature",user.Signature);
			param.Add("@user_imagesDir",user.ImagesDir);
			param.Add("@user_rank",user.EloRanking);
						
			SqlServerUtility.executeNonQuery("OrionsBelt_UsersUpdateUser",param);
		}
		
		/// <summary>Retorna as roles de um User</summary>
		public override string[] getRoles( string userMail ) {
			Hashtable param = new Hashtable();

			param.Add("@user_mail",userMail);
			
			DataSet dataset = SqlServerUtility.getFromDB("OrionsBelt_UsersGetUserRoles",param);
			string[] roles = new string[dataset.Tables[0].Rows.Count];
			
			int i = 0;
			foreach ( DataRow role in dataset.Tables[0].Rows ) {
				roles[i++] = (string)role["roles_roleName"];
			}

			return roles;
		}

		/// <summary>
		/// faz reset ao id do user
		/// </summary>
		public override void resetUserRulerId() {
			SqlServerUtility.executeNonQuery("OrionsBelt_UsersResetUserRulerId",null);
		}
		
		/// <summary>Obtm todos os utilizadores com a role passada</summary>
		public override int[] getUsers( int role ) {
			Hashtable param = new Hashtable();
			param.Add("@role",role);
			
			DataSet ds = SqlServerUtility.getFromDB("OrionsBelt_UsersGetUsersIdByRole",param);
						
			return fetchUsersIds( ds );
		}

		public override User[] getUsersRanking() {
			DataSet ds = SqlServerUtility.getAllFromDB("OrionsBelt_UsersGetUserRanks");
			return PopulateUserRankings(50, ds, "user_id");
		}

		/// <summary>Obtm a quantidade de utilizadores registados</summary>
		public override int getUserCount()
		{
			DataSet ds = SqlServerUtility.getAllFromDB("OrionsBelt_UsersGetCount");
			return (int)ds.Tables[0].Rows[0]["count"];
		}

		/// <summary>Actualiza a data do ltimo login</summary>
		public override void updateLastLogin( string userMail ) {
			Hashtable param = new Hashtable();
			param.Add("@user_mail",userMail);

			SqlServerUtility.executeNonQuery("OrionsBelt_UsersUpdateLastLogin",param);
		}

		public override bool resetPassword(string userMail, string newPassword) {
			Hashtable param = new Hashtable();
			param.Add("@user_mail",userMail);
			
			bool result = SqlServerUtility.checkResults("OrionsBelt_UsersCheckUserMail",param);
			
			if( result ) {
				param.Add("@user_password",newPassword);
				SqlServerUtility.executeNonQuery("OrionsBelt_UsersResetPassword",param);
			}

			return result;
		}

		public override string getAvatar( int rulerId ) {
			Hashtable param = new Hashtable();
			param.Add("@ruler_id",rulerId);
			
			DataSet ds = SqlServerUtility.getFromDB("OrionsBelt_UsersGetAvatar",param);

			if( ds.Tables[0].Rows.Count != 1) {
				return User.DefaultAvatar;
			}
			string s = (string)ds.Tables[0].Rows[0]["user_avatar"];

			return s == string.Empty?User.DefaultAvatar:s;
		}

		public override IList getInactiveUsers() {
			throw new NotImplementedException("SqlServeruserutility.getInactiveUsers is not implemented!!!");
		}

		public override void setAllianceMembers( AllianceInfo info ) {
			throw new NotImplementedException("SqlServeruserutility.setAllianceMembers is not implemented!!!");
		}

		#endregion
	};
}
