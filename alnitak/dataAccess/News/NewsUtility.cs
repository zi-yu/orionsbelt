
using System;
using System.Data;
using System.Web;
using Alnitak.News;
using Alnitak.Mail;

namespace Alnitak {
	
	public abstract class NewsUtility {
		
		#region Static Region
		
		/// <summary>Indica o driver que sabe persistir notcias</summary>
		public static NewsUtility Persistence {
			get {
				string value = OrionGlobals.getConfigurationValue("utilities",getKey());
			
				return (NewsUtility)Activator.CreateInstance( Type.GetType( value , true) );
			}
		}
		
		public static string getKey()
		{
			return OrionGlobals.resolveDataAccessName("newsUtility");
		}
	
		#endregion
		
		#region Public Members
		
		/// <summary>Regista uma nova notcia</summary>
		public void AddNews( Entry entry )
		{
			#if !DEBUG
			Mailer.SendToNewsML( entry.Title, entry.Content );
			#endif
			Register(entry);
		}
		
		/// <summary>Regista uma nova notcia</summary>
		public void AddNews( string title, string message, string lang )
		{
			AddNews( new Entry(DateTime.Now, message, title, lang) );
		}
		
		/// <summary>Obtm as notcias existentes</summary>
		public NewsList GetNews()
		{
			string culture = OrionGlobals.getCulture();
			object obj = HttpContext.Current.Cache["NewsList" + culture];
			if( obj == null ) {
				NewsList list = GetNewsFromDBByLang( culture );
				HttpContext.Current.Cache["NewsList" + culture] = list;
				return list;
			}
			
			return (NewsList) obj;
		}

		#endregion
		
		#region Abstract Methods
		
		/// <summary>Regista uma nova notcia</summary>
		protected abstract void Register( Entry entry );
		
		/// <summary>Obtm as notcias existentes</summary>
		protected abstract NewsList GetNewsFromDB();

		/// <summary>Obtm as notcias existentes</summary>
		protected abstract NewsList GetNewsFromDBByLang( string lang );
		
		#endregion
		
		#region Utilities
		
		/// <summary>Cria uma NewsList com base num DataSet</summary>
		/// <remarks>
		/// 	Este mtodo espera um dataset com as seguintes colunas, na
		/// 	ordem seguinte:
		/// 	id | Title | Content | Date
		///
		/// 	O mtodo no liga aos nomes, vai aos ndices buscar o contedo
		/// </remarks>
		public NewsList NewsFromDataSet( DataSet ds )
		{
			NewsList list = new NewsList();
			
			foreach( DataRow row in ds.Tables[0].Rows ) {
				DateTime date = (DateTime) row[3];
				string title = row[1].ToString();
				string message = row[2].ToString();
				string lang = row[3].ToString();
				Entry entry = new Entry(date, message, title,lang);
				entry.Id = (int) row[0];
				list.List.Add(entry);
			}
			
			return list;
		}
		
		#endregion
		
	};
	
}
