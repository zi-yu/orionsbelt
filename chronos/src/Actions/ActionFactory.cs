// created on 2/23/04 at 2:45 a

using System.Collections;
using System.Xml;
using Chronos.Exceptions;
using DesignPatterns;

namespace Chronos.Actions {
	
	/// <summary>Classe base das ActionFactory's</summary>
	public abstract class ActionFactory : IFactory {
		
		/// <summary>Prapara os argumentos e passa-os a createAction</summary>
		public object create( object args )
		{
			if( args == null ) {
				return createAction(null);
			}
			
			Hashtable table = new Hashtable();
			XmlNode node = (XmlNode) args;
			
			foreach( XmlAttribute att in node.Attributes ) {
				
				if( hasSpaces(att.Value) ) {
					throw new LoaderException("No spaces allowed: '" +att.Value+"'");
				}
				
				table.Add( att.Name, att.Value );
			}
			
			return createAction(table);
		}
		
		/// <summary>Retorna true se uma string tem algum espao</summary>
		private bool hasSpaces( string source )
		{	
			return source.IndexOf(' ') != -1;
		}
		
		/// <summary>Cria uma action</summary>
		protected abstract Action createAction( Hashtable args );
		
		public Action Create( Hashtable args )
		{
			return createAction(args);
		}
		
		/// <summary>Obtém um inteiro</summary>
		public int GetInt( Hashtable args, string key )
		{
			try {
				return int.Parse(args[key].ToString());
			} catch {
				throw new LoaderException("Error trying to parse '"+key+"' attribute to integet");
			}
		}
		
	};
	
}
