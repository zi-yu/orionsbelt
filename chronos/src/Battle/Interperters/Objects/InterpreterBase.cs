using Chronos.Info.Results;

namespace Chronos.Battle {
	
	public abstract class InterpreterBase {

		#region Fields 

		private string _move;
		private BattleInfo _battleInfo;
        
		#endregion

		#region Public

		public bool GridCoordValid( string coord ) {
			if( coord.Length != 3 )
				return false;

			int l = int.Parse( coord[0].ToString() );
			int r = int.Parse( coord[2].ToString() );
			char c = coord[1];

			if( l < 1 || l > 8 || r < 1 || r > 8 || c != '_' ) {
				return false;
			}

			return true;
		}

		public virtual int MoveCost() {
			return 1;
		}

		#endregion

		#region Properties

		public string Move {
			get { return _move; }
			set { _move = value; }
		}

		public BattleInfo BattleInfo {
			get { return _battleInfo; }
			set { _battleInfo = value; }
		}

		#endregion

		#region Constructor

		public InterpreterBase( string info, BattleInfo battleInfo ) {
			_move = info;
			_battleInfo = battleInfo;
		}

		#endregion

		public abstract ResultItem CheckMove( );
		
		public abstract void Interpretate();
	}
}
