using System.Collections;
using Chronos.Core;
using Chronos.Info.Results;
using Chronos.Messaging;

namespace Chronos.Battle {
	
	public class MoveInterpreter : InterpreterBase {

		private Hashtable positionCheck = new Hashtable();
		private delegate bool CheckPosition( string src, string dst, string position );
		
		#region Constructor

		public MoveInterpreter( string info, BattleInfo battleInfo ) : base( info, battleInfo ) {
			positionCheck.Add( "all", new CheckPosition(CheckAll) );
			positionCheck.Add( "front", new CheckPosition(CheckFront) );
			positionCheck.Add( "diagonal", new CheckPosition(CheckDiagonal) );
			positionCheck.Add( "normal", new CheckPosition(CheckNormal) );
			positionCheck.Add( "position", new CheckPosition(CheckPositioning) );
			
			positionCheck.Add( "n", new CheckPosition(CheckN) );
			positionCheck.Add( "s", new CheckPosition(CheckS) );
			positionCheck.Add( "e", new CheckPosition(CheckE) );
			positionCheck.Add( "w", new CheckPosition(CheckW) );
		}

		#endregion

		#region Private

		private ResultItem CheckMove( Element e, string src, string dst, string quant ) {
			if( e == null ) {
				return new InvalidShip( src );
			}

			if( !GridCoordValid(dst) ) {
				return new InvalidCoordinate( dst );
			}

			if( !Utils.MathUtils.isInt(quant) ) {
				return new InvalidQuantity();
			}

			int q = int.Parse( quant );
			if( e.Quantity < q || q < 1 ) {
				return new InvalidQuantity();
			}

			int min = (int)(e.Quantity*0.2);
			
			if( q < min ){
				return new MinimumMove(q.ToString(),e.Type,min.ToString());
			}

			int quantRest = (e.Quantity - q);
			if( quantRest < min && quantRest != 0 ){
				return new MinimumRest(quantRest.ToString(),e.Type,min.ToString());
			}

			Ruler ruler = Universe.instance.getRuler( BattleInfo.CurrentRulerId );
			RulerBattleInfo enemyInfo = BattleInfo.GetEnemyBattleInfo( ruler );

			if( enemyInfo.SectorGetElement( RulerBattleInfo.InvertSector(dst) ) != null ) {
				return new InvalidMove();
			}

			return null;
		}

		#endregion
		
		#region Check

		private ResultItem PositioningCheck( RulerBattleInfo info, string[] items ) {
			Element elem = null;
			
			foreach( Element e in info.InitialContainer ) {
				if( e.Type != items[0] )
					continue;
					
				ResultItem r = CheckMove( e, items[0], items[1], items[2] );
				if( r != null )
					return r;

				elem = e;
				break;
			}

			if( elem == null ) {
				return new InvalidShip( items[0] );
			}
			
			if( !((CheckPosition)positionCheck["position"])(items[0],items[1],elem.Position.ToString() ) ) {
				return new InvalidCoordinate( items[0]);
			}

            return null;
		}

		private ResultItem TurnCheck( RulerBattleInfo info, string[] items ) {
			ResultItem r = CheckMove( info.SectorGetElement( items[0] ), items[0], items[1], items[2] );
			if( r != null )
				return r;

			if( !GridCoordValid(items[0]) ) {
				return new InvalidCoordinate( items[0]);
			}

			Element e = info.SectorGetElement( items[0] );
			
			if( !((CheckPosition)positionCheck[e.Unit.MovementTypeDescription])( items[0],items[1],e.Position.ToString() ) ) {
				return new InvalidCoordinate( items[1] );
			}

			return null;
		}

		#endregion

		#region Move

		private void MoveGridToGrid( string[] items ) {
			Ruler ruler = Universe.instance.getRuler( BattleInfo.CurrentRulerId );
			RulerBattleInfo info = BattleInfo.GetRulerBattleInfo( ruler );
			RulerBattleInfo enemyInfo = BattleInfo.GetEnemyBattleInfo( ruler );
			Element src = info.SectorGetElement( items[0] );

			info.SectorMove(items[0],items[1],int.Parse( items[2] ) );
					
			Messenger.Send( info, "BattleMove", items[2], src.Type, items[0], items[1] );
			Messenger.Send( enemyInfo, "BattleMove", items[2], src.Type, RulerBattleInfo.InvertSector( items[0] ), RulerBattleInfo.InvertSector( items[1] ) );
		}

		private void MoveSrcToGrid( string[] items ) {
			Ruler ruler = Universe.instance.getRuler( BattleInfo.CurrentRulerId );
			RulerBattleInfo info = BattleInfo.GetRulerBattleInfo( ruler );

			Element elem = null;
			foreach( Element e in info.InitialContainer ) {
				if( e.Type != items[0] )
					continue;
				elem = e;
				break;
			}
			if( elem == null )
				return;

			int quant = int.Parse(items[2]);

			info.SectorSrcMove( elem, items[1], quant );
		}

		#endregion

		#region Public

		public override ResultItem CheckMove( ) {
			Ruler ruler = Universe.instance.getRuler( BattleInfo.CurrentRulerId );
			RulerBattleInfo info = BattleInfo.GetRulerBattleInfo( ruler );
			string[] items = Move.Split( '-' );

			if( ruler.GetBattle( info.BattleId, info.BattleType ).IsPositionTime ) {
				return PositioningCheck( info, items );
			}else {
				return TurnCheck( info, items );			
			}
		}

		public override int MoveCost() {
			string[] items = Move.Split( '-' );

			Ruler ruler = Universe.instance.getRuler( BattleInfo.CurrentRulerId );
			RulerBattleInfo info = BattleInfo.GetRulerBattleInfo( ruler );

            Element e = info.SectorGetElement( items[0] );
			
			if( e != null ) {
				int quant = int.Parse( items[2] );
				if( quant == e.Quantity ) {
					return e.Unit.MovementCost;
				}
				return e.Unit.MovementCost*2;
			}
			return base.MoveCost( );
		}

		public override void Interpretate( ) {
			string[] items = Move.Split( '-' );

            if( items[0].IndexOf( '_' ) != -1 ) {
            	MoveGridToGrid( items );
            }else {
            	MoveSrcToGrid( items );
            }
		}

		#endregion

		#region Positions Check

		private bool CheckAll( string src, string dst, string position ) {
			int s_x = int.Parse(src[0].ToString());
			int s_y = int.Parse(src[2].ToString());
			int d_x = int.Parse(dst[0].ToString());
			int d_y = int.Parse(dst[2].ToString());

			if( d_x <= s_x + 1 && d_x >= s_x - 1 ) {
				if( d_y <= s_y + 1 && d_y >= s_y - 1 ) {
					return true;
				}
			}
			return false;
		}

		private bool CheckFront( string src, string dst, string position ) {
			return ((CheckPosition)positionCheck[position.ToLower()])( src,dst,position );
		}

		private bool CheckNormal( string src, string dst, string position ) {
			int s_x = int.Parse(src[0].ToString());
			int s_y = int.Parse(src[2].ToString());
			int d_x = int.Parse(dst[0].ToString());
			int d_y = int.Parse(dst[2].ToString());

			if( d_x <= s_x + 1 && d_x >= s_x - 1 && s_y == d_y ) {
				return true;
			}
			
			if( d_y <= s_y + 1 && d_y >= s_y - 1 && s_x == d_x ) {
				return true;
			}

			return false;
		}

		private bool CheckDiagonal( string src, string dst, string position ) {
			int s_x = int.Parse(src[0].ToString());
			int s_y = int.Parse(src[2].ToString());
			int d_x = int.Parse(dst[0].ToString());
			int d_y = int.Parse(dst[2].ToString());

			if( d_x == s_x+1 && d_y == s_y+1 ||
				d_x == s_x-1 && d_y == s_y-1 ||
				d_x == s_x+1 && d_y == s_y-1 ||
				d_x == s_x-1 && d_y == s_y+1 ) {
				return true;
			}
		
			return false;
		}

		private bool CheckPositioning( string src, string dst, string position ) {
			int coord = int.Parse( dst[0].ToString() );
			if( coord != 8 && coord != 7 ) {
				return false;
			}
			return true;
		}

		#endregion

		#region Directions Check

		private bool CheckN( string src, string dst, string position ) {
			int s_x = int.Parse(src[0].ToString());
			int s_y = int.Parse(src[2].ToString());
			int d_x = int.Parse(dst[0].ToString());
			int d_y = int.Parse(dst[2].ToString());

			if( d_x == s_x-1 && s_y == d_y )
				return true;
			return false;
		}
		private bool CheckS( string src, string dst, string position ) {
			int s_x = int.Parse(src[0].ToString());
			int s_y = int.Parse(src[2].ToString());
			int d_x = int.Parse(dst[0].ToString());
			int d_y = int.Parse(dst[2].ToString());

			if( d_x == s_x+1  && s_y == d_y )
				return true;
			return false;
		}
		private bool CheckW( string src, string dst, string position ) {
			int s_x = int.Parse(src[0].ToString());
			int s_y = int.Parse(src[2].ToString());
			int d_x = int.Parse(dst[0].ToString());
			int d_y = int.Parse(dst[2].ToString());

			if( d_y == s_y-1 && s_x == d_x )
				return true;
			return false;
		}
		private bool CheckE( string src, string dst, string position ) {
			int s_x = int.Parse(src[0].ToString());
			int s_y = int.Parse(src[2].ToString());
			int d_x = int.Parse(dst[0].ToString());
			int d_y = int.Parse(dst[2].ToString());

			if( d_y == s_y+1 && s_x == d_x )
				return true;
			return false;
		}

		#endregion
	}
}