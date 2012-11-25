// created on 4/25/04 at 8:52 a

using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Security;

using Alnitak.Exceptions;

namespace Alnitak {

	public abstract class UserUtility {
		
		#region Static Members
	
		public static UserUtility bd;
		static UserUtility()
		{
			string value = OrionGlobals.getConfigurationValue("utilities",getKey());
			
			bd = (UserUtility)Activator.CreateInstance( Type.GetType( value , true) );
		}
		
		public static string getKey()
		{
			return OrionGlobals.resolveDataAccessName("userUtility");
		}
		
		#endregion
		
		#region Utilities
		
		/// <summary>Retorna a hash de uma pass para guardar na BD</summary>
		public string hashPassword( string text  )
		{
			return FormsAuthentication.HashPasswordForStoringInConfigFile(text, "sha1");
		}
		
		/// <summary>Retorna um array de inteiros com base num dataset</summary>
		/// <remarks>Vai bustar todos os inteiros na primeira coluna de cada linha</remarks>
		public int[] fetchUsersIds( DataSet source )
		{
			ArrayList list = new ArrayList();
			foreach( DataRow row in source.Tables[0].Rows ) {
				if( !(row[0] is System.DBNull) ) {
					list.Add((int)row[0]);
				}
			}
			return (int[]) list.ToArray(typeof(int));
		}

        #endregion

		#region Protected 

		protected User[] PopulateUserRankings(int count, DataSet ds, string idstr) {
			User[] users = new User[count];
			int i = -1;
	
			foreach( DataRow row in ds.Tables[0].Rows ) {
				User user = new User();
				
				user.Nick = (string) row["user_nick"];
				user.Mail = (string) row["user_mail"];
				user.UserId = (int) row[idstr];
				user.RulerId = (int) row["user_ruler_id"];
				user.EloRanking = (int) row["user_rank"];
				user.Wins = (int)row["user_wins"];
				user.Losses = (int)row["user_losses"];
				
				users[++i] = user;
			}
			return users;
		}

		protected ArrayList PopulateUsers(DataSet ds, string idstr) {
			ArrayList list = new ArrayList();
	
			foreach( DataRow row in ds.Tables[0].Rows ) {
				User user = new User();
				
				user.Nick = (string) row["user_nick"];
				user.Mail = (string) row["user_mail"];
				user.UserId = (int) row[idstr];
				user.RulerId = (int) row["user_ruler_id"];
				user.EloRanking = (int) row["user_rank"];
				
				user.AllianceId = (int) row["user_alliance_id"];
				user.AllianceRank = AllianceInfo.ToAllianceRank((string) row["user_alliance_rank"]);

				user.Wins = (int)row["user_wins"];
				user.Losses = (int)row["user_losses"];
				
				list.Add(user);
			}
			
			return list;
		}

		#endregion
		
		#region Abstract Members
		
		/// <summary>Verfica se existe um user com o mail e pass passados</summary>
		public abstract bool checkUser( string mail, string pass );

		/// <summary>Verfica se existe um user com o mail passado</summary>
		public abstract bool checkUser( string mail );

		/// <summary>Verfica se existe um user com o id passado</summary>
		public abstract string checkUser( int id );
				
		/// <summary>Retorna as roles de um User</summary>
		public abstract string[] getRoles( string userMail );
		
		/// <summary>Regista um User</summary>
		public abstract void registerUser( string nick, string mail, string pass );
		
		/// <summary>Preenche um User com os seus dados</summary>
		public abstract void fillUser( User user );
		
		/// <summary>Obtém um User dado o seu id</summary>
		public abstract User getUser( int id );
		
		/// <summary>Guarda um User com os seus dados na Base de Dados</summary>
		public abstract void saveUser( User user, string password );
		
		/// <summary>Coloca o campo RulerId = -1 para todos os Users</summary>
		public abstract void resetUserRulerId();
		
		/// <summary>Obtm todos os utilizadores com a role passada</summary>
		public abstract int[] getUsers( int role );
		
		/// <summary>Obtm todos ordenados por Ranking</summary>
		public abstract User[] getUsersRanking();
		
		/// <summary>Obtm a quantidade de utilizadores registados</summary>
		public abstract int getUserCount();

		/// <summary>Actualiza a data do ltimo login</summary>
		public abstract void updateLastLogin( string userMail );

		/// <summary>Reset da password</summary>
		public abstract bool resetPassword( string userMail, string newPassword );

		public abstract string getAvatar( int rulerId );

		public abstract IList getInactiveUsers();
		
		public abstract void setAllianceMembers( AllianceInfo info );
		
		#endregion

	};
}
