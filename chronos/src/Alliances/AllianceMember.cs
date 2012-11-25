// created on 4/10/04 at 9:33 a

using System;
using Chronos.Core;

namespace Chronos.Alliances {

	/// <summary>
	/// Representa um membro de uma aliana
	/// </summary>
	[Serializable]
	public class AllianceMember : IComparable {
	
		public enum Role {
			Private = 1,
			Corporal = 100,
			ViceAdmiral = 200,
			Admiral = 300
		};

		private Ruler ruler;
		private Role role;
		
		/// <summary>Construtor</summary>
		public AllianceMember( Ruler _ruler, Role _role )
		{
			ruler = _ruler;
			role = _role;
		}
		
		/// <summary>Compara 2 AllianceMembers, segundo o Role e o nome</summary>
		public int CompareTo( object obj )
		{
			if( obj == null ) {
				return 0;
			}
			
			AllianceMember member = (AllianceMember) obj;
			if( RulerRole != member.RulerRole ) {
				return RulerRole.CompareTo(member.RulerRole);
			}
			
			return Ruler.Name.CompareTo(member.Ruler.Name);
		}
		
		/// <summary>Indica o ruler associado</summary>
		public Ruler Ruler {
			get { return ruler; }
			set { ruler = value; }
		}
		
		
		/// <summary>Indica o ruler associado</summary>
		public Role RulerRole {
			get { return role; }
			set { role = value; }
		}
	
	};

}
