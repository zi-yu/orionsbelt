
using Chronos.Core;
using Chronos.Resources;

namespace Chronos.Messaging {
	
	/// <summary>
	/// Identifica que sabe traduzir um nome de um recurso para text localizado
	/// </summary>
	public interface ITranslator {
		
		/// <summary>Traduz uma palavra numa frase localizada</summary>
		string translate( string origin );
		
		/// <summary>Traduz um Planeta para texto localizado</summary>
		string translate( Planet planet );
		
		/// <summary>Traduz um Ruler para texto localizado</summary>
		string translate( Ruler ruler );
		
		/// <summary>Traduz uma ResourceFactory para texto localizado</summary>
		string translate( ResourceFactory factory );

		/// <summary>Decora uma string</summary>
		string decorate( string origin );

		/// <summary>Traduz e decora uma palavra</summary>
		string operate( string origin );
			
	};
}
