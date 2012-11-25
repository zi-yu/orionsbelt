namespace Chronos.Alliances {

	using System;
	using System.Collections;
	using System.Runtime.Serialization;
	using Chronos.Utils;

	/// <summary>
	/// Summary description for AllianceCollection.
	/// </summary>
	[Serializable]
	public sealed class AllianceCollection : Hashtable, IDeserializationCallback {

		#region Instance Fields

		// Tem de ser campo porque não sei serializar uma class
		// que deriva de Hashtable!
	//	private Hashtable hashtable;
		
		#endregion

		#region overrided

		/// <summary>
		/// override do mtodo Add para no permitir repeties
		/// </summary>
		/// <param name="key">key</param>
		/// <param name="value">value</param>
		public override void Add(object key, object value) {
			if( ContainsKey(key) )
				return;

			base.Add(key,value);
		}

		/// <summary>
		/// override do indexer para no permitir repeties
		/// </summary>
		public override object this[object key] {
            set {
                // no adicionar duplicados
                if ( Contains(key) )
                    return;
                base[key] = value;
            }
        }

		#endregion

		#region Ctor

		/// <summary>Ctor</summary>
		public AllianceCollection()
		{
		}

		/// <summary>Usado pelo IFormatter</summary>
		private AllianceCollection( SerializationInfo info, StreamingContext context )
			: base(info, context)
		{
		}
		
		/// <summary>Repõe o estado</summary>
		public override void GetObjectData(SerializationInfo si, StreamingContext context)
		{
			try {
				base.GetObjectData(si,context);
			} catch( Exception e ) {
				Log.log("ERROR " + e.Message);
			}
		}

		/// <summary>Repõe o estado</summary>
		public override void OnDeserialization( object sender )
		{
			try {
				base.OnDeserialization(sender);
			} catch( Exception e ) {
				Log.log("ERROR " + e.Message);
			}

		}
	
		#endregion

	};
}
