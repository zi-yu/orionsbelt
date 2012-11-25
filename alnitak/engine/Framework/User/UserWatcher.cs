using System;
using System.Collections;
using Language;

using Alnitak.Exceptions;

namespace Alnitak {
	
	public class UserWatcher {
		
		#region Instance Fields
		
		private SortedList list = new SortedList();
		
		#endregion
		
		#region Instance Properties
		
		/// <summary>Indica os ltimos utilizadores online</summary>
		public SortedList List {
			get { return list; }
		}
		
		/// <summary>Indica se h utilizadores registados</summary>
		public bool HasRegisteredUsers {
			get { return list.Count != 0; }
		}
		
		public int Limit {
			get { return 10; }
		}
		
		#endregion
		
		#region Instance Methods
		
		/// <summary>Regista um utilizador</summary>
		public void Register( User user )
		{
			if( HasRegisteredUsers ) {
				foreach(User registered in list.Values) {
					if( registered.UserId == user.UserId ) {
						return;
					}
				}
			}
			
			lock(this) {
				DateTime date = DateTime.Now;
				
				while( list[date] != null ) {
					date = date.AddMilliseconds(5);
				}
				
				list.Add(date, user);
				if( list.Count == Limit ) {
					list.RemoveAt(0);
				}
			}
		}
		
		#endregion
		
	};
	
}
