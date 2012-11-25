using Chronos.Messaging;
using Chronos.Core;
using Chronos.Resources;
using Language;

namespace Alnitak {
	
	/// <summary>
	/// Sabe traduzir e decorar expressões
	/// </summary>
	public sealed class MessageDecorator : ITranslator {

		#region Instance Fields

		private ILanguageInfo info;

		#endregion

		#region Ctors

		/// <summary>Ctor</summary>
		public MessageDecorator( ILanguageInfo _info )
		{
			info = _info;
		}
		
		/// <summary>Ctor</summary>
		public MessageDecorator()
		{
			info = null;
		}

		#endregion
		
		#region ITranslator Implementation
		
		/// <summary>Traduz uma palavra numa frase localizada</summary>
		public string translate( string origin )
		{
			return info.getContent(origin);
		}

		/// <summary>Traduz um Planeta para texto localizado</summary>
		public string translate( Planet planet )
		{
			return string.Format("<a href='{0}' class='note'>{1}</a>",
					OrionGlobals.getSectionBaseUrl("planet") + "?id="+planet.Id,
					planet.Name
				);
		}
		
		/// <summary>Traduz um Ruler para texto localizado</summary>
		public string translate( Ruler ruler )
		{
			return OrionGlobals.getLink(ruler);
		}
		
		/// <summary>Traduz uma ResourceFactory para texto localizado</summary>
		public string translate( ResourceFactory factory )
		{
			return string.Format("<a href='{0}' class='docs'>{1}</a>",
					OrionGlobals.getSectionBaseUrl("docs") + "?category="+factory.Category+"#"+factory.Name,
					info.getContent(factory.Name)
				);
		}

		/// <summary>Decora uma string</summary>
		public string decorate( string origin )
		{
			return string.Format("<span class='note'>{0}</span>", origin);
		}

		/// <summary>Traduz e decora uma palavra</summary>
		public string operate( string origin )
		{
			return decorate( translate(origin) );
		}

		#endregion
	
	};
}
