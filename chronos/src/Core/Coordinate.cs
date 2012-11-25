using System;
using Chronos.Exceptions;

namespace Chronos.Core {
	
	/// <summary>
	/// Representa a corrdenada de algo (fleet, planet,...)
	/// </summary>
	[Serializable]
	public class Coordinate {
		
		#region members

		private const int _maximumGalaxies = 3;
		private const int _maximumSystems = 20;
		private const int _maximumSectors = 20;
		private const int _maximumPlanets = 3;
		

		private int _galaxy = 1;
		private int _system = 1;
		private int _sector = 1;
		private int _planet = 1;

		#endregion
		
		#region Static Members
		
		//private static readonly Coordinate first = new Coordinate(1,1,1,1);
		
		/// <summary>Retorna uma coordenada em 1,1,1</summary>
		public static Coordinate First {
			get { return new Coordinate(1,1,1,1); }
		}

		/// <summary>
		/// obtem o numero maximo de Galaxias
		/// </summary>
		public static int MaximumGalaxies {
			get{ return _maximumGalaxies;}
		}

		/// <summary>
		/// obtem o numero maximo de Sectores
		/// </summary>
		public static int MaximumSystems {
			get{ return _maximumSystems;}
		}

		/// <summary>
		/// obtem o numero maximo de Sectores
		/// </summary>
		public static int MaximumSectors {
			get{ return _maximumSectors;}
		}

		/// <summary>
		/// obtem o numero maximo de planetas
		/// </summary>
		public static int MaximumPlanets {
			get{ return _maximumPlanets;}
		}

		/// <summary>
		/// traduz a coordenada
		/// </summary>
		public static Coordinate translateCoordinate( string coord ) {
			string[] points = coord.Split( new char[]{ ':' } );

			if( points.Length != 4 )
				throw new RuntimeException("Coordenada " + coord + " no tem o formato correcto. Tentar g:s:p ");

			int g,sy,s,p;
			g = int.Parse( points[0] );
			sy = int.Parse( points[1] );
			s = int.Parse( points[2] );
			p = int.Parse( points[3] );

			if( g < 0 || sy < 0 || s < 0 || p < 0 || g > MaximumGalaxies || sy > MaximumSystems || s > MaximumSectors || p > MaximumPlanets )
				throw new RuntimeException("Coordenada " + coord + " possui valores invlidos!" );
				
            return new Coordinate( g, sy, s, p );
		}
		
		/// <summary>
		/// verifica se uma coordenada  vlida
		/// </summary>
		public static bool isValid( Coordinate c ) {
			return c.Galaxy > 0 && c.System > 0 && c.Sector > 0 && c.Planet > 0 &&
				c.Galaxy <= MaximumGalaxies && c.System <= MaximumSystems &&
				c.Sector <= MaximumSectors && c.Planet <= MaximumPlanets;
		}
		
		/// <summary>Indica se determinada coordenada  acessvel</summary>
		public static bool IsAccessible( Planet asker, Coordinate target )
		{
			return IsAccessible((Ruler)asker.Owner, asker.Coordinate, target);
		}
		
		/// <summary>Indica se determinada coordenada  acessvel a um planeta com base nas tecnologias</summary>
		public static bool IsAccessible( Ruler ruler, Coordinate source, Coordinate target )
		{
			if( ruler == null ) {
				return false;
			}
			
			if( target.Planet == 1 ) {
				return target.CompareTo(ruler.HomePlanet.Coordinate) == 0;
			}
			
			if( source.Galaxy != target.Galaxy ) {
				return ruler.isResourceAvailable("Research", "GalaxyExploration", 1);
			}
			if( source.System != target.System ){
				return ruler.isResourceAvailable("Research", "SystemExploration", 1);
			}
			if( source.Sector != target.Sector ){
				return ruler.isResourceAvailable("Research", "SectorExploration", 1);
			}
			if( source.Planet != target.Planet ){
				return ruler.isResourceAvailable("Research", "PlanetExploration", 1);
			}
			
			return true;
		}
		
		#endregion

		#region properties

		/// <summary>
		/// retorna o numero da galaxia
		/// </summary>
		public int Galaxy {
			get{ return _galaxy; }
			set{ _galaxy = value; }
		}

		/// <summary>
		/// retorna o numero do sistema
		/// </summary>
		public int System {
			get{ return _system; }
			set{ _system = value; }
		}

		/// <summary>
		/// retorna o numero do sector
		/// </summary>
		public int Sector {
			get{ return _sector; }
			set{ _sector = value; }
		}

		/// <summary>
		/// retorna o planeta
		/// </summary>
		public int Planet {
			get{ return _planet; }
			set{ _planet = value; }
		}

		/// <summary>
		/// verifica se se pode atribuir mais coordenadas
		/// </summary>
		public bool HasMoreCoordinates {
			get{ return !(_galaxy == _maximumGalaxies && _system == _maximumSystems && _sector == _maximumSectors ); }
		}
		
		#endregion

		#region public methods

		/// <summary>
		/// incrementa a coordenada
		/// </summary>
		public bool incrementCoordinate() {
			if( _sector == _maximumSectors ) {
				_sector = 1;
				if( _system == _maximumSystems ) {
					_system = 1;
					if( _galaxy == _maximumGalaxies ) {
						return false;
					} else {
						++_galaxy;
					}
				} else {
					++_system;
				}
			} else {
				++_sector;
			}
			return true;
		}
		
		/// <summary>Retorna uma string correspondente  coordenada</summary>
		public override string ToString()
		{
			return _galaxy + ":" + _system + ":" + _sector + ":" + _planet;
		}


		#endregion

		#region Construtores
		
		/// <summary>
		/// representa uma coordenada da galxia
		/// </summary>
		/// <param name="galaxy">numero da galaxia</param>
		/// <param name="sector">numero do sector na galaxia</param>
		/// <param name="planet">numero do planeta no sector</param>
		public Coordinate( int galaxy, int system, int sector, int planet )
		{
			_galaxy = galaxy;
			_system = system;
			_sector = sector;
			_planet = planet;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="c"></param>
		public Coordinate( Coordinate c ):this( c.Galaxy , c.System, c.Sector , c.Planet )
		{
		}

		#endregion

		#region Object
	
		/// <summary>
		/// verifica se a coordenada  igual  passada como parmetro
		/// </summary>
		/// <param name="obj">objecto com o qual esta instncia vai ser comparada</param>
		/// <returns><code>true</code> se for igual, <code>false</code> caso contrrio</returns>
		public override bool Equals(object obj) {
			Coordinate coord = (Coordinate)obj;
			return _galaxy == coord._galaxy && _system == coord.System && _sector == coord.Sector && _planet == coord.Planet;
		}

		/// <summary>
		/// codigo hash diferente para cada coordenada
		/// </summary>
		/// <returns>cdigo hash</returns>
		public override int GetHashCode() {
			return _galaxy*10000 + _system*1000 + _sector*100 + _planet*10;
		}


		#endregion

		#region IComparable Members

		public int CompareTo(object obj) {
			Coordinate c2 = (Coordinate)obj;
			if( _galaxy > c2._galaxy )
				return 1;
			if( _galaxy < c2._galaxy )
				return -1;
			if( _system > c2._system )
				return 1;
			if( _system < c2._system )
				return -1;
			if( _sector > c2._sector )
				return 1;
			if( _sector < c2._sector )
				return -1;
			if( _planet > c2._planet )
				return 1;
			if( _planet < c2._planet )
				return -1;
			return 0;
		}

		#endregion
	}
}
