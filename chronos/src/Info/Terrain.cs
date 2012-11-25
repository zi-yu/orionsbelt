// created on 5/24/04 at 9:00 a

using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Chronos.Utils;
using System.Runtime.Serialization;

namespace Chronos.Core {

	[Serializable]
	public class Terrain {
			
		#region Campos Privados
		
		private int id;
		private string description;
		private int mineral;
		private int food;
		private int gold;
		private int energy;
		private int groundSpace;
		private int waterSpace;
		private int orbitSpace;
		
		#endregion
		
		#region Membros Estáticos
		
		private static Terrain[] all = null;
		
		/// <summary>Retorna um array com todos os Terrain Disponíveis</summary>
		public static Terrain[] All {
			get { return all; }
		}
		
		/// <summary>Construtor Estático</summary>
		static Terrain()
		{
			LoadTerrainInfo(Platform.GeneralConfigDir);
		}
		
		/// <summary>Carrega toda a informação dos terrenos do ficheiro de configuração</summary>
		public static Exception LoadTerrainInfo( string dir )
		{
			try {
				XmlSerializer ser = new XmlSerializer( typeof(Terrain[]) );	
				all = (Terrain[]) ser.Deserialize( new XmlTextReader(dir + "terrain.xml") );
				return null;
			} catch( Exception ex ) {
				return ex;
			}
		}
		
		#endregion
		
		#region Construtores
		
		/// <summary>Construtor</summary>
		internal Terrain( int _id, string _desc, int _food, int _gold, int _gSpace, int _oSpace )
		{
			id = _id;
			description = _desc;
			food = _food;
			gold = _gold;
			groundSpace = _gSpace;
			orbitSpace = _oSpace;
		}
		
		/// <summary>Construtor por defeito</summary>
		public Terrain()
		{
			id = -1;
			description = string.Empty;
			food = -1;
			gold = -1;
			groundSpace = -1;
			orbitSpace = -1;
		}
		
		#endregion
		
		#region Propriedades
		
		/// <summary>Retorna id deste Terrain</summary>
		[XmlAttribute("id")]
		public int Id {
			get { return id; }
			set { id = value; }
		}
		
		/// <summary>Retorna a descrição deste Terrain</summary>
		[XmlElement("description")]
		public string Description {
			get { return description; }
			set { description = value; } 
		}
		
		/// <summary>Indica o nível de comida neste Terrain</summary>
		[XmlElement("mineralRicheness")]
		public int MineralRicheness {
			get { return mineral; }
			set { mineral = value; }
		}
		
		/// <summary>Indica o nível de comida neste Terrain</summary>
		[XmlElement("food")]
		public int Food {
			get { return food; }
			set { food = value; }
		}
		
		/// <summary>Indica o nível de ouro neste Terrain</summary>
		[XmlElement("gold")]
		public int Gold {
			get { return gold; }
			set { gold = value; }
		}
		
		/// <summary>Indica o nível de ouro neste Terrain</summary>
		[XmlElement("energy")]
		public int Energy {
			get { return energy; }
			set { energy = value; }
		}
		
		/// <summary>Indica o nível de espaço em terra deste Terrain</summary>
		[XmlElement("groundSpace")]
		public int GroundSpace {
			get { return groundSpace; }
			set { groundSpace = value; }
		}
		
		/// <summary>Indica o nível de espaço em terra deste Terrain</summary>
		[XmlElement("waterSpace")]
		public int WaterSpace {
			get { return waterSpace; }
			set { waterSpace = value; }
		}
		
		/// <summary>Indica o nível de espaço em orbita deste Terrain</summary>
		[XmlElement("orbitSpace")]
		public int OrbitSpace {
			get { return orbitSpace; }
			set { orbitSpace = value; }
		}

		public static Terrain Random {
			get {
				return Terrain.All[ MathUtils.random(0, All.Length-1) ];
			}
		}
		
		#endregion

	};

}
