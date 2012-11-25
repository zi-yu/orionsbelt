// created on 5/24/04 at 9:02 a

using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Chronos.Utils;
using Chronos.Core;
using System.Runtime.Serialization;
using System.Collections;

namespace Chronos.Info {

	[Serializable]
	public class PlanetInfo : ISerializable {
	
		#region Instance Fields
		
		private int id;
		private float diameter;
		private float mass;
		private float escapeVelocity;
		private string temperature;
		private int terrainId;
		
		#endregion
		
		#region Static Members
		
		private static PlanetInfo[] all;
		
		/// <summary>Retorna todos os planet infos</summary>
		public static PlanetInfo[] All {
			get { return all; }
		}
		
		/// <summary>Retorna um PlanetInfo ao calhas</summary>
		public static PlanetInfo Random {
			get {
				return All[ MathUtils.random(0, All.Length-1) ];
			}
		}

		public static PlanetInfo DefaultPlanetInfo {
			get{
				return All[0];
			}
		}
		
		/// <summary>Construtor estático</summary>
		static PlanetInfo() {
			try {
				loadPlanetInfo();
			} catch( Exception e ) {
				Log.log(e.Message);
				throw e;
			}
		}
		
		/// <summary>Carrega toda a informação dos terrenos do ficheiro de configuração</summary>
		private static void loadPlanetInfo() {
			string dir = Platform.GeneralConfigDir;
			XmlSerializer ser = new XmlSerializer( typeof(PlanetInfo[]) );
			
			all = (PlanetInfo[]) ser.Deserialize( new XmlTextReader(dir + "planets.xml") );
		}

		
		#endregion
		
		#region Construtores
		
		internal PlanetInfo( int _id, float _d, float _m, float _ev, string _temp, int _t ) {
			id = _id;
			diameter = _d;
			mass = _m;
			escapeVelocity = _ev;
			temperature = _temp;
			terrainId = _t;
		}
		
		public PlanetInfo() {
			id = -1;
			diameter = -1;
			mass = -1;
			escapeVelocity = -1;
			temperature = string.Empty;
			terrainId = -1;
		}
		
		#endregion
		
		#region Utility Methods
		
		/// <summary>Retorna um inteiro arredondado</summary>
		private int round( float num ) {
			return MathUtils.round(num);
		}
		
		#endregion
		
		#region Porportion Properties
		
		/// <summary>Indica a percentagem relativa ao diametro</summary>
		private int DiameterProportion {
			get {
				return round( 100 * Diameter / TopDiameter );
			}
		}
		
		/// <summary>Indica a percentagem relativa ao diametro</summary>
		private int MassProportion {
			get {
				return round( 100 * Mass / TopMass );
			}
		}
		
		/// <summary>Indica a percentagem relativa ao GroundSpace</summary>
		private int GroundSpaceProportion {
			get {
				return round( 100 * Terrain.GroundSpace / TopScale );
			}
		}
		
		/// <summary>Indica a pertentagem de comida</summary>
		private int FoodProportion {
			get {
				return round( Terrain.Food * 100 / TopScale );
			}
		}
		
		/// <summary>Indica a pertentagem de ouro</summary>
		private int GoldProportion {
			get {
				return round( Terrain.Gold * 100 / TopScale );
			}
		}
		
		/// <summary>Indica a pertentagem de energia</summary>
		private int EnergyProportion {
			get {
				return round( Terrain.Energy * 100 / TopScale );
			}
		}
		
		/// <summary>Indica a pertentagem de matérias primas</summary>
		private int MineralRichenessProportion {
			get {
				return round( Terrain.MineralRicheness * 100 / TopScale );
			}
		}
		
		#endregion
		
		#region Ratio Properties
		
		/// <summary>Indica a percentagem de comida</summary>
		public int FoodRatio {
			get {
				return round( FoodProportion / 2 + DiameterProportion / 2 );
				//return round( FoodProportion / 3 + MassProportion / 3 + DiameterProportion / 3 );
			}
		}
		
		/// <summary>Indica a percentagem de matérias primas</summary>
		public int MPRatio {
			get {
				return round( MineralRichenessProportion / 3 + MassProportion / 3 + DiameterProportion / 3 );
			}
		}
		
		/// <summary>Indica a percentagem de ouro</summary>
		public int GoldRatio {
			get {
				return round( 
					MineralRichenessProportion / 4 + 
					FoodProportion / 4 + 
					GoldProportion / 4 +
					EnergyProportion / 4 
					);
			}
		}
		
		/// <summary>Indica a percentagem de energia</summary>
		public int EnergyRatio {
			get {
				return round( EnergyProportion / 3 + MassProportion / 3 + DiameterProportion / 3 );
			}
		}
		
		/// <summary>Indica a o espaço em terra</summary>
		public int GroundSpace {
			get {
				return round( Terrain.GroundSpace * DiameterProportion / 10 ) + 10;
			}
		}
		
		/// <summary>Indica o espaço em água</summary>
		public int WaterSpace {
			get {
				return round( Terrain.WaterSpace * DiameterProportion / 10 ) + 10;
			}
		}
		
		/// <summary>Indica o espaço em orbita</summary>
		public int OrbitSpace {
			get {
				return round( Terrain.OrbitSpace * DiameterProportion / 20 );
			}
		}
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Indica o id desta Info</summary>
		[XmlAttribute("id")]
		public int Id {
			get { return id; }
			set { id = value; }
		}
		
		/// <summary>Indica o diâmetro desta Info</summary>
		[XmlAttribute("diameter")]
		public float Diameter {
			get { return diameter; }
			set { diameter = value; }
		}
		
		/// <summary>Indica a Massa desta Info</summary>
		[XmlAttribute("mass")]
		public float Mass {
			get { return mass; }
			set { mass = value; }
		}
		
		/// <summary>Indica a EscapeVelocity desta Info</summary>
		[XmlAttribute("escapevelocity")]
		public float EscapeVelocity {
			get { return escapeVelocity; }
			set { escapeVelocity = value; }
		}
		
		/// <summary>Indica a temperatura desta Info</summary>
		[XmlAttribute("temperature")]
		public string Temperature {
			get { return temperature; }
			set { temperature = value; }
		}
		
		
		/// <summary>Indica o Terrain desta Info</summary>
		[XmlAttribute("terrain")]
		public int TerrainId {
			get { return terrainId; }
			set { terrainId = value; }
		}
		
		/// <summary>Indica o Terrain desta Info</summary>
		public Terrain Terrain {
			get { return Terrain.All[TerrainId]; }
		}
		
		/// <summary>Indica o topo da escala</summary>
		public int TopScale {
			get { return 6; }
		}
		
		/// <summary>Indica o topo da escala</summary>
		public int TopDiameter {
			get { return 15; }
		}
		
		/// <summary>Indica o topo da escala</summary>
		public int TopMass {
			get { return 8; }
		}
		
		#endregion

		#region Serialization 

		/// <summary>Classe auxiliar</summary>
		[Serializable]
		private sealed class PlanetInfoSerializationHelper : IObjectReference {
			
			#region Instance Fields

			public int id = 0;
			
			#endregion
			
			/// <summary>Retorna a ResourceFactoryAssociada</summary>
			public object GetRealObject( StreamingContext context ) {
				return PlanetInfo.All[id];
			}
		};
		/// <summary>
		/// Serializa este objecto
		/// </summary>
		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.SetType( typeof(PlanetInfoSerializationHelper) );
			info.AddValue("id", Id );
		}

		#endregion
	};
}
