// created on 3/29/04 at 6:21 a

using System;
using System.Web.UI;

namespace Alnitak {

	public class RoleVisible : Control {
	
		private string _showTo = null;
		private string _dontshowTo = null;
		
		/// <summary>Construtor</summary>
		public RoleVisible()
		{
			_showTo = null;
		}
		
		/// <summary>Retorna o Role a que este controlo se vai mostrar</summary>
		public string showTo {
			get { return _showTo; }
			set { _showTo = value; }
		}

		/// <summary>Retorna o Role a que este controlo se vai mostrar</summary>
		public string DontshowTo {
			get { return _dontshowTo; }
			set { _dontshowTo = value; }
		}
		
		/// <summary>Verifica se está num Role a que se vai mostrar</summary>
		protected override void OnLoad( EventArgs args )
		{
			base.OnLoad(args);
			if( showTo != null && showTo.Length != 0 )
			{
				if( ! Context.User.IsInRole(showTo) ) {
					Visible = false;
				}else{
					Visible = true;
				}
			}

			if( DontshowTo != null && DontshowTo.Length != 0 ) {
				if( Context.User.IsInRole(DontshowTo) ) {
					Visible = false;
				}else{
					Visible = true;
				}
			}
		}
	
	};

}