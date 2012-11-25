// created on 5/24/04 at 9:48 a

using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace Chronos.Utils {

	public class XmlUtils {
	
		#region Construtores
		
		/// <summary>Construtor Estático</summary>
		static XmlUtils()
		{
			 validationHandler = new ValidationEventHandler(ValidationCallback);
		}
		
		#endregion
	
		#region Métodos Estáticos
		
		private static ValidationEventHandler validationHandler;
		
		/// <summary>Faz o load de um ficheiro XML para um XmlDocument</summary>
		public static XmlDocument loadNonValidatedXmlDoc( string file ) 
		{
			XmlValidatingReader reader = null;
			XmlDocument doc = null;

			try {

				XmlTextReader txtReader = new XmlTextReader(file);
				reader = new XmlValidatingReader(txtReader);
		
				// Set a handler to handle validation errors.
				reader.ValidationEventHandler += validationHandler;
		
				// Pass the validating reader to the XML document.
				// Validation fails due to an undefined attribute, but the 
				// data is still loaded into the document.
				doc = new XmlDocument();
				doc.Load(reader);
		    	
				return doc;
		            
			} finally {
				if ( reader != null ) 
					reader.Close();
			}
		}
		
		/// <summary>Faz o load de um ficheiro XML para um XmlDocument</summary>
		public static XmlDocument loadXmlDoc( string file )
		{
			XmlValidatingReader reader = null;
			XmlDocument doc = null;

		    try {

		       	XmlTextReader txtReader = new XmlTextReader(file);
		       	reader = new XmlValidatingReader(txtReader);
		       	reader.ValidationType = ValidationType.DTD;
		
		       	// Set a handler to handle validation errors.
		       	reader.ValidationEventHandler += validationHandler;
		
		       	// Pass the validating reader to the XML document.
		       	// Validation fails due to an undefined attribute, but the 
		       	// data is still loaded into the document.
		       	doc = new XmlDocument();
		    	doc.Load(reader);
		    	
		    	return doc;
		            
			} finally {
		       if ( reader != null ) 
		         reader.Close();
		     }
		}
		
		/// <summary>Lana excepaao devido a um erro da DTD</summary>
		private static void ValidationCallback( object sender, ValidationEventArgs args )
		{
			throw new Exception(args.Exception.Message);
		}
		
		#endregion
		
		#region Xml Attribute Utilities
		
		/// <summary>Retorna um atributo inteiro</summary>
		public static int getInt( XmlNode elem, string name ) 
		{
			XmlAttribute o = elem.Attributes[name];
			if( o == null ) {
				throw new Exception("No '" + name + "' atributo found in element '" + elem.Name + "'");
			}
			string num = o.Value;
			
			try { 
				return int.Parse(num);
				
			} catch(FormatException ex) {
				string msg = "Failed to convert '" + o.ToString() + "' to int in '" + name + "' attribute from element '" + elem.Name +"'";
				throw new Exceptions.RuntimeException(msg + "[" + ex.Message + "]");
			}
		}
		
		/// <summary>Retorna um atributo inteiro</summary>
		public static string getString( XmlNode elem, string name ) 
		{
			XmlAttribute o = elem.Attributes[name];
			if( o == null ) {
				throw new Exception("No '" + name + "' atribut found in element '" + elem.Name + "'");
			}
			return o.Value;
		}
		
		/// <summary>Retorna um atributo inteiro</summary>
		public static bool getBool( XmlNode elem, string name ) 
		{
			XmlAttribute o = elem.Attributes[name];
			if( o == null ) {
				throw new Exception("No '" + name + "' atributo found in element '" + elem.Name + "'");
			}
			
			string num = o.Value;
			
			try { 
				return bool.Parse(num);
				
			} catch(FormatException ex) {
				string msg = "Failed to convert '" + o.ToString() + "' to bool in '" + name + "' attribute from element '" + elem.Name +"'";
				throw new Exceptions.RuntimeException(msg + "[" + ex.Message + "]");
			}
		}
		
		#endregion
	
	};

}