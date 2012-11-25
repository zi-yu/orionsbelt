using System;
using Chronos.Core;
using Chronos.Info.Results;
using Chronos.Messaging;

namespace Chronos.Battle {
	
	public class RotationInterpreter : InterpreterBase {
		
		#region Constructor

		public RotationInterpreter( string info, BattleInfo battleInfo ) : base( info, battleInfo )
			{}

		#endregion

		public override ResultItem CheckMove( ) {
			string[] items = Move.Split( '-' );

			Ruler ruler = Universe.instance.getRuler( BattleInfo.CurrentRulerId );
			RulerBattleInfo info = BattleInfo.GetRulerBattleInfo( ruler );

			if( !GridCoordValid( items[0] ) ) {
				return new InvalidCoordinate( items[0]);
			}

			if( !info.SectorHasElements( items[0] ) ) {
				return new InvalidShip( items[0] );
			}
			
			string pos = items[2].ToLower(  );
			if( pos != "n" && pos != "s" && pos != "w" && pos != "e" ) {
				return new InvalidRotation( pos );
			}

			return null;
		}

		public override void Interpretate( ) {
			string[] items = Move.Split( '-' );

			Ruler ruler = Universe.instance.getRuler( BattleInfo.CurrentRulerId );
			RulerBattleInfo info = BattleInfo.GetRulerBattleInfo( ruler );

			Element e = info.SectorGetElement( items[0] );
			e.Position = (Element.PositionType) Element.PositionType.Parse( typeof(Element.PositionType), items[2].ToUpper() );

			Messenger.Send( BattleInfo.GetRulerBattleInfo( ruler ), "BattleRotation",items[0],items[2].ToUpper());
		}
	}
}