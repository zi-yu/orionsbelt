using System;
using System.Collections;
using Chronos.Core;
using Chronos.Resources;

namespace Chronos.Battle {
	
	/// <summary>
	/// Contem informaes essenciais de um tipo
	/// de elemento de batalha
	/// </summary>
	[Serializable]
	public class Element : ICloneable  {

		public enum PositionType {N,S,W,E};
        		
		#region private members

		private string _coordinate;
		
		private string _type;
		private int _quantity;
		private Hashtable _modifiers;
		private PositionType _currentPosition;

		private int _remainLife;
		private bool retrieveShip = true;
		private bool isBuilding = false;

		[NonSerialized]
		private Resource resource = null;

		#endregion

		#region properties

		public string Coordinate {
			get{ return _coordinate; }
			set{ _coordinate = value; }
		}

		public string Type {
			get{ return _type; }
			set {
				_type = value;
				_remainLife = Resource.Unit.HitPoints;
			}
		}
		
		public int Quantity {
			get{ return _quantity; }
			set{ _quantity = value; }
		}

		public int Attack {
			get{ return Resource.Unit.BaseAttack; }
		}

		public int Defense {
			get{ return Resource.Unit.BaseDefense; }
		}

		public Hashtable Modifiers {
			get{ return _modifiers; }
			set{ _modifiers = value; }
		}

		public PositionType Position {
			get{ return _currentPosition; }
			set{ _currentPosition = value; }
		}

		public PositionType InvertedPosition {
			get {
				switch(Position) {
					case PositionType.N:
						return PositionType.S;
					case PositionType.S:
						return PositionType.N;
					case PositionType.W:
						return PositionType.E;
					case PositionType.E:
						return PositionType.W;
				}
				return PositionType.N;
			}
		}

		public bool RetrieveShip {
			get { return retrieveShip; }
			set { retrieveShip = value; }
		}

		public Resource Resource {
			get {
				if( resource == null ) {
					if( IsBuilding ) {
						resource = Universe.getFactory("planet", "Building", _type).create();
					}else {
						try {
							resource = Universe.getFactory("planet", "Unit", _type).create();	
						} catch {
							resource = Universe.getFactory("planet", "Building", _type).create();
							isBuilding = true;
						}
					}
					
				}
				return resource;
			}
		}

		public int RemainLive {
			get { return _remainLife; }
			set {
				if( value == 0 ) {
					_remainLife = Resource.Unit.HitPoints;
				}else{
					_remainLife = value;
					if( _remainLife < 0 ) {
						_remainLife = 0;
					}
				}
			}
		}

		public UnitDescriptor Unit {
			get{ return Resource.Unit; }
		}

		public bool IsBuilding {
			get{ return isBuilding; }
			set{ isBuilding = value; }
		}

		#endregion

		#region IClonable 

		/// <summary>
		/// cria uma nova referência do objecto Element por
		/// cópia do corrente
		/// </summary>
		/// <returns>Referncia por cópia do objecto elemento corrente</returns>
		public object Clone() {
			Element e = new Element();
			e.resource = Resource;
			e.IsBuilding = IsBuilding;
			e.Type = Type;
			e.Modifiers = Modifiers;
			e.Quantity = Quantity;
			e.Position = Position;
			e.Coordinate = Coordinate;
			e.RemainLive = Resource.Unit.HitPoints;
			
			return e;
		}

		#endregion

		#region Public

		#endregion

		#region Constructor
		
		public Element( ) {
			_currentPosition = PositionType.N;
		}

		#endregion

	}
}
