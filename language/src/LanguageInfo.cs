// created on 3/21/04 at 9:28 a

using System;
using System.Xml;
using System.Xml.Schema;
using System.Collections;
using System.IO;
using Language.Exceptions;

namespace Language {

	public class LanguageInfo : ILanguageInfo {
	
		private Hashtable root;
		private Hashtable targets;
		private string directory;

		private ValidationEventHandler validationHandler = null;
	
		/// <summary>Construtor</summary>
		public LanguageInfo( string dir )
		{
			root = null;
			targets = null;
			directory = dir;
		
			string[] files = Directory.GetFiles(dir,"*.xml");
			foreach( string file in files ) {
				loadFile(file);
			}
		}
		
		/// <summary>Carrega a informação de um ficheiro</summary>
		private void loadFile( string file )
		{
			XmlDocument doc = loadXmlDoc(file);
			XmlElement root = doc.DocumentElement;
			Hashtable hash = null;
			
			XmlAttribute target = root.Attributes["target"];
			if( target == null || target.Value.Length == 0 ) {
				hash = Root;
			} else {
				hash = (Hashtable) Targets[target.Value];
				if( hash == null ) {
					Targets.Add( target.Value, hash = new Hashtable() );
				}
			}
		
			// itera sobre todas as tag's <resource>
			foreach( XmlNode son in root.ChildNodes ) {
				
				if( son is XmlComment )
					continue;
				
				XmlAttribute reference = son.Attributes["ref"];
				
				if( reference == null || reference.Value.Length == 0 ) {
					throw new LanguageException("attribute 'ref' not set on file " +file);
				}

				hash.Add( reference.Value, son.InnerText.Trim() );
			}
			
		}
		
		/// <summary>Faz o load de um ficheiro XML para um XmlDocument</summary>
		private XmlDocument loadXmlDoc( string file )
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

		    } catch(XmlException e) {
		    	throw new LanguageException("Error loading xml: " + e.Message);
			}finally {
		       if ( reader != null )
		         reader.Close();
		     }
		}
		
		/// <summary>Retorna a hashtable de root</summary>
		public Hashtable Root {
			get {
				if( root == null ) {
					root = new Hashtable();
				}
				return root;
			}
		}
		
		/// <summary>Retorna a hashtable de root</summary>
		public Hashtable Targets {
			get {
				if( targets == null ) {
					targets = new Hashtable();
				}
				return targets;
			}
		}
		
		/// <summary>Retorna a string associada a uma referência</summary>
		public string getContent( string reference, bool force )
		{
			string buu = null;
			return getContent(buu,reference, force);
		}
		
		/// <summary>Retorna a string associada a uma referência</summary>
		public string getContent( string reference )
		{
			string buu = null;
			return getContent(buu,reference);
		}
		
		/// <summary>Retorna a string associada a uma referência</summary>
		public string getContent( string target, string reference )
		{
			return getContent(target, reference, true);
		}
	
		/// <summary>Retorna a string associada a uma referência</summary>
		public string getContent( string target, string reference, bool force )
		{
			string result = null;
		
			if( target == null || target.Length == 0 ) {
				result = getContent( Root, reference );
			} else {
				Hashtable hash = (Hashtable) Targets[target];
				if( hash == null ) {
					throw new LanguageException("Target "+target+" not found in '"+directory+"'");
				}
				result = getContent( hash, reference );
			}
			
			if( result == null ) {
				if( force ) {
					throw new LanguageException("Reference '" +reference +"' not found in '"+directory+"' target="+(target==null?"null":target));
				}
				return reference;
			}
			
			return result;
		}
		
		/// <summary>Retorna a string associada a uma referência</summary>
		private string getContent( Hashtable hash, string reference )
		{
			object o = hash[reference];
			return (string) o;
		}
		
		/// <summary>Retorna a hash de um target</summary>
		public Hashtable getTargetHash( string target )
		{
			object o = Targets[target];
			if( o == null ) {
				throw new LanguageException("Target '"+target+"' not found");
			}
			return (Hashtable) o;
		}
	};

}
