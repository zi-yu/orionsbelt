namespace Alnitak {

	using System.Data;

	/// <summary>
	/// Classe que representa uma skin
	/// </summary>
	public class MasterSkinInfo {
		
		#region private members
		
		private int _masterSkinId;
		private string _masterSkinName;
		private string _masterSkinStyle;
		private string _masterSkinScript;
		private string _masterSkinDescription;
		private int _masterSkinCount;
		
		#endregion

		#region poperties

			public int masterSkinId {
				get { return _masterSkinId; }
			}

			public string masterSkinName {
				get { return _masterSkinName; }
			}

			public string masterSkinStyle {
				get { return _masterSkinStyle; }
			}

			public string masterSkinScript {
				get { return _masterSkinScript; }
			}

			public string masterSkinDescription {
				get { return _masterSkinDescription; }
			}

		public int masterSkinCount {
				get { return _masterSkinCount; }
			}

		#endregion

		#region constructors

        public MasterSkinInfo(
			int masterSkinId,
			string masterSkinName,
			string masterSkinStyle,
			string masterSkinScript,
			string masterSkinDescription,
			int masterSkinCount
		) {
			_masterSkinId = masterSkinId;
			_masterSkinName = masterSkinName;
			_masterSkinStyle = masterSkinStyle;
			_masterSkinScript = masterSkinScript;
			_masterSkinDescription = masterSkinDescription;
			_masterSkinCount = masterSkinCount;
		}

		public MasterSkinInfo(DataRow dataRow) {
			_masterSkinId = (int)dataRow["masterSkin_id"];
			_masterSkinName = (string)dataRow["masterSkin_name"];
			_masterSkinStyle = (string)dataRow["masterSkin_style"];
			_masterSkinScript = (string)dataRow["masterSkin_script"];
			_masterSkinDescription = (string)dataRow["masterSkin_description"];
			_masterSkinCount = (int)dataRow["masterSkin_count"];
		}

		#endregion

	}
}
