namespace Alnitak {

	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Collections;

	using Alnitak.Exceptions;

	/// <summary>
	/// Classe que representa uma seco
	/// </summary>
	public class SectionInfo {

		#region private members
		
		private int _sectionId;
		private int _sectionParentId;
		private string _sectionName;
		private string _sectionTitle;
		
		//Skin
		private int _sectionSkinId;
		
		private string _sectionPath;
		
		private string _sectionContent;
		private string _sectionDescription;
		private int _sectionIconId;
		private int _sectionOrder;
		private string[] _sectionRoles;

		private bool _sectionIsVisible;

		private DataRow _section = null;

		#endregion

		#region private

		private object getField( string columnName ) {
			try {
				return _section[columnName];
			} catch {
				throw new AlnitakException( string.Format( "Campo {0} não existe na tabela das Secções!", columnName ) );
			}
		}

		#endregion

		#region properties
		
		/// <summary>
		/// devolve o id da seco
		/// </summary>
		public int sectionId {
			get{ return _sectionId; }
		}

		/// <summary>
		/// devolve o id do pai da seco
		/// </summary>
		public int sectionParentId {
			get{ return _sectionParentId;}
		}

		/// <summary>
		/// devolve nome da seco
		/// </summary>
		public string sectionName {
			get{ return _sectionName; }
		}

		/// <summary>
		/// devolve o ttulo da seco
		/// </summary>
		public string sectionTitle {
			get{ return _sectionTitle; }
		}

		/// <summary>
		/// devolve o numero de skins disponiveis
		/// </summary>
		public int sectionSkinId {
			get{ return _sectionSkinId; }
		}

		/// <summary>
		/// devolve o path da seco
		/// </summary>
		public string sectionPath {
			get{ return _sectionPath;}
		}

		/// <summary>
		/// devolve a classe associada  seco
		/// </summary>
		public string sectionContent {
			get{ return _sectionContent;}
		}
		
		/// <summary>
		/// devolve uma pequena descrio da seccao
		/// </summary>
		public string sectionDescription {
			get{ return _sectionDescription;}
		}

		/// <summary>
		/// devolve o ficheiro que representa o icon da seco
		/// </summary>
		public int sectionIconId {
			get{ return _sectionIconId;}
		}

		/// <summary>
		/// devolve a ordem da seco
		/// </summary>
		public int sectionOrder {
			get{ return _sectionOrder;}
		}

		/// <summary>
		/// devolve as roles da seccao
		/// </summary>
		public string[] sectionRoles {
			get{ return _sectionRoles;}
		}

		/// <summary>
		/// verifica se a secção está visivel ou não
		/// </summary>
		/// <remarks>
		/// Este método verifica se está visivel ou não
		/// para efeitos do SubSectionMenu, visto que existem seces que no
		/// fazem sentido aparecer.
		/// </remarks>
		public bool isVisible {
			get{ return _sectionIsVisible;}
		}
		


		#endregion

		#region public methods
		/// <summary>
		/// construtor
		/// </summary>
		/// <param name="sectionId">id da seccao</param>
		/// <param name="sectionParentId">id do pai da da seco</param>
		/// <param name="sectionName">nome da seco</param>
		/// <param name="sectionTitle">ttulo da seco</param>
		/// <param name="sectionStyle">estilo da seco</param>
		/// <param name="sectionPath">path da da seco</param>
		/// <param name="sectionContent">classe asoociada  seco</param>
		/// <param name="sectionDescription">pequena descrio da seco</param>
		/// <param name="sectionIconId">icon da seco</param>
		/// <param name="sectionOrder">numero de ordem da seco</param>
		/// <param name="sectionRoles">roles desta section</param>
		public SectionInfo(
			int sectionId,
			int sectionParentId,
			string sectionName,
			string sectionTitle,
			int sectionSkinId,
			string sectionPath,
			string sectionContent,
			string sectionDescription,
			int sectionIconId,
			int sectionOrder,
			string[] sectionRoles,
			bool sectionIsVisible

		) {

			_sectionId = sectionId;
			_sectionParentId = sectionParentId;
			_sectionName = sectionName;
			_sectionTitle = sectionTitle;

			_sectionSkinId = sectionSkinId;
			
			_sectionPath = sectionPath;
			
			_sectionContent = sectionContent;
			_sectionDescription = sectionDescription;
			_sectionIconId = sectionIconId;
			_sectionOrder = sectionOrder;

			_sectionRoles = sectionRoles;

			_sectionIsVisible = sectionIsVisible;

		}


		/// <summary>
		/// Construtor que povoa os atributos directamente da base de dados
		/// </summary>
		/// <param name="dataRow">datarow com todas as informaes</param>
		public SectionInfo( DataRow section, string[] sectionRoles){
			// referencia auxiliar para n tar passar parametros ao getField
			_section = section;	

			_sectionId = (int)getField("section_id");
			_sectionParentId = (int)getField("section_parentId");
			_sectionName = (string)getField("section_name");
			_sectionTitle = (string)getField("section_title");

			_sectionSkinId = Int32.Parse( (string)getField("section_skinId") );

			_sectionPath = (string)getField("section_path");
			_sectionContent = (string)getField("section_content");
			_sectionDescription = (string)getField("section_description");
			_sectionIconId = (int)getField("section_iconId");
			_sectionOrder = (int)getField("section_order");
			
			_sectionIsVisible = ( (int)getField("section_isVisible")==0 )?false:true;
			_sectionRoles = sectionRoles;

			// libertar a referencia nesta classe
			_section = null;
			
		}

		#endregion

	}
}
