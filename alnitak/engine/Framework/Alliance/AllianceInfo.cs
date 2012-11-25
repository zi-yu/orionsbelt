// created on 3/20/2006 at 9:28 AM

using System;
using System.Collections;
using Chronos.Alliances;
using Chronos.Core;
using Chronos.Utils;

namespace Alnitak {
	
	public class AllianceInfo {
	
		#region Fields

		private int id;		
		private string name;
		private string tag;
		private string motto;
		private int ranking = 1000;
		private int rankingBattles;
		private DateTime registDate = DateTime.Now;
		private ArrayList members = new ArrayList();
		private ArrayList wannabe = new ArrayList();
		
		#endregion
		
		#region Properties
		
		public int Id {
			get { return id; }
			set { id = value; }
		}
		
		public string Name {
			get { return name; }
			set { name = value; }
		}
		
		public string Tag {
			get { return tag; }
			set { tag = value; }
		}
		
		public string Motto {
			get { return motto; }
			set { motto = value; }
		}
		
		public DateTime RegistDate {
			get { return registDate; }
			set { registDate = value; }
		}
		
		public int Ranking {
			get { return ranking; }
			set { ranking = value; }
		}
		
		public int AverageRanking {
			get {
				if( Members.Count == 0 ) {
					return 0;
				}
				int sum = 0;
				foreach( User user in Members ) {
					sum += user.EloRanking;
				} 
				return sum / Members.Count;
			}
		}
		
		public int Score {
			get {
				if( Members == null ) {
					return 0;
				}
			
				int sum = 0;
				foreach( User user in Members ) {
					if( user.RulerId >= 0 ) {
						Ruler ruler = Universe.instance.getRuler(user.RulerId);
						if( ruler != null ) {
							sum += ruler.Score;
						}
					}
				}
				return sum;
			}
		}
		
		public int AverageScore {
			get {
				int active = ActiveMembers;
				if( active == 0 ) {
					return 0;
				}
				return Score / active;
			}
		}
		
		public int ActiveMembers {
			get {
				int sum = 0;
				foreach( User user in Members ) {
					if( user.RulerId >= 0 ) {
						++sum;
					}
				}
				return sum;
			}
		}
		
		public int RankingBattles {
			get { return rankingBattles; }
			set { rankingBattles = value; }
		}
		
		public ArrayList Members {
			get { return members; }
			set { members = value; }
		}
		
		public ArrayList Wannabe {
			get { return wannabe; }
			set { wannabe = value; }
		}

		public bool HasMembers {
			get { return Members != null && Members.Count > 0; }
		}

		public int TotalPlanets {
			get { 
				if( !HasMembers ) {
					return 0;
				}
				int sum = 0;
				foreach( User user in Members ) {
					if( user.RulerId >= 0 ) {
						Ruler ruler = Universe.instance.getRuler(user.RulerId);
						sum += ruler.Planets.Length;
					}
				}
				return sum;
			}
		}

		public int TotalSpace {
			get { 
				if( !HasMembers ) {
					return 0;
				}
				int sum = 0;
				foreach( User user in Members ) {
					if( user.RulerId >= 0 ) {
						Ruler ruler = Universe.instance.getRuler(user.RulerId);
						foreach( Planet planet in ruler.Planets ) {
							sum += planet.Info.GroundSpace + planet.Info.WaterSpace + planet.Info.OrbitSpace;
						}
					}
				}
				return sum;
			}
		}

		public int TotalRoundWins {
			get { 
				if( !HasMembers ) {
					return 0;
				}
				int sum = 0;
				foreach( User user in Members ) {
					if( user.RulerId >= 0 ) {
						Ruler ruler = Universe.instance.getRuler(user.RulerId);
						sum += ruler.Victories;
					}
				}
				return sum;
			}
		}

		public int TotalRoundDefeats {
			get { 
				if( !HasMembers ) {
					return 0;
				}
				int sum = 0;
				foreach( User user in Members ) {
					if( user.RulerId >= 0 ) {
						Ruler ruler = Universe.instance.getRuler(user.RulerId);
						sum += ruler.Defeats;
					}
				}
				return sum;
			}
		}
		
		#endregion
		
		#region Utilities
		
		public static AllianceMember.Role ToAllianceRank( string rank )
		{
			return (AllianceMember.Role) Enum.Parse(typeof(AllianceMember.Role), rank);
		}
		
		public static string FromAllianceRole( AllianceMember.Role role )
		{
			return role.ToString();
		}
		
		public bool HasMember( User asker ) 
		{
			if( asker == null ) {
				return false;
			}
		
			foreach( User user in Members ) {
				if( user.UserId == asker.UserId )  {
					return true;
				}
			}
			
			return false;
		}
		
		public bool HasWannabe( User asker ) 
		{
			if( asker == null ) {
				return false;
			}
		
			foreach( User user in Wannabe ) {
				if( user.UserId == asker.UserId )  {
					return true;
				}
			}
			
			return false;
		}
		
		public void RemoveWannabe( int id ) 
		{
			Log.log("Removing user {0}", id);
			User toRemove = null;
			foreach( User user in Wannabe ) {
				Log.log("Checking user {0}", user.UserId);
				if( user.UserId == id )  {
					Log.log("Found user {0}", user.UserId);
					toRemove = user;
				}
			}
			
			if( toRemove != null ) {
				Wannabe.Remove(toRemove);
				Log.log("Not Found user {0}", id);
			}
		}
		
		public void RemoveMember( int id ) 
		{
			Log.log("Removing user {0}", id);
			User toRemove = null;
			foreach( User user in Members ) {
				Log.log("Checking user {0}", user.UserId);
				if( user.UserId == id )  {
					Log.log("Found user {0}", user.UserId);
					toRemove = user;
				}
			}
			
			if( toRemove != null ) {
				Members.Remove(toRemove);
				Log.log("Not Found user {0}", id);
			}
		}
		
		#endregion
		
	};
	
}
