using System;
using System.Security.Principal;
using System.Web;
using Alnitak.Exceptions;
using Chronos.Alliances;
using Chronos.Core;

#if FORUM
	using yaf;
#endif


namespace Alnitak {

	[Serializable]
	public class User : Ranking, IPrincipal
#if FORUM
	, IForumUser
#endif
	{
		
		#region Instance Fields
	
		private IIdentity identity;
		private string[] roles;
	
		private string nick;
		private string mail;
		private int userId;
		private int rulerId;
		private int allianceId;
		private AllianceMember.Role allianceRank;
		private int wins;
		private int losses;
		
		private int skin;
		private string lang;
		
		private DateTime registDate;
		private DateTime lastLogin;
		
		private string website;
		private string avatar;
		private string imagesDir = string.Empty;
		
		private string msn;
		private string icq;
		private string jabber;
		private string aim;
		private string yahoo;
		
		private string signature;

		private string _skinName;
		private string _skinStyle;
		private string _skinScript;
		
		#endregion
		
		#region Ctor
		
		/// <summary>Construtor</summary>
		public User()
		{
			reset();
		}

		/// <summary>Construtor</summary>
		public User( IIdentity _user, string[] _roles )
		{
			reset();
			if(_user.Name == null) {
				mail = "";
			}else{
				mail = _user.Name;
			}

			roles = _roles;
			identity = _user;
			UserUtility.bd.fillUser(this);

			//carregar a skin
			MasterSkinInfo masterSkin = MasterSkinUtility.getMasterSkinInfoFromId(Skin);
			if( masterSkin == null ) {
				throw new AlnitakException( string.Format( "No foi possivel carregar a skin com o id {0}", skin ) );
			}
			SkinName = masterSkin.masterSkinName;
			SkinStyle = masterSkin.masterSkinStyle;
			SkinScript = masterSkin.masterSkinScript;
		}
		
		#endregion
		
		#region Utilities
		
		/// <summary>Coloca todos os campos com o seu valor por defeito</summary>
		public void reset() {
			nick = string.Empty;
			userId = -1;
			rulerId = -1;
			
			skin = 2;
			lang = "pt";
			
			website = string.Empty;
			avatar = string.Empty;
			
			msn = string.Empty;
			icq = string.Empty;
			jabber = string.Empty;
			aim = string.Empty;
			yahoo = string.Empty;
			
			signature = string.Empty;
		}
		
		/// <summary>Altera as roles de utilizador</summary>
		public void SetRoles( string[] roles )
		{
			this.roles = roles;
		}
		
		/// <summary>Retorna true se o user estiver na role passada</summary>
		public bool IsInRole( string _role )
		{
			switch(_role) {
				case "ruler": {
					return RulerId >= 0 ? true : false;
				}
				case "user": {
					return true;
				}
				case "user-no-ruler": {
					return RulerId < 0 ? true : false;
				}
				default: {
					if(roles == null) {
						return false;
					}
					foreach( string role in roles ) {
						if( role == _role ) {
							return true;
						}
					}
					return false;
				}
			}
		}

		public static string GeneratePassword() {
			string newPassword = string.Empty;
			for( int i = 0 ; i < 8; ++i) {
				newPassword += (char)OrionGlobals.GenerateRandInt(97,122);
			}
			return newPassword;
		}
		
		#endregion

		#region properties
        		
		/// <summary>Retorna a IIdentity deste User</summary>
		public IIdentity Identity
		{
			get { return identity; }
		}
		
		/// <summary>Indica o nick do utilizador</summary>
		public string Nick {
			get { return nick; }
			set { nick = value; }
		}
		
		/// <summary>Indica o nick do utilizador</summary>
		public string Mail {
			get { return mail; }
			set { mail = value; }
		}
		
		/// <summary>Indica o nick do utilizador</summary>
		public int UserId {
			get { return userId; }
			set { userId = value; }
		}
		
		/// <summary>Indica o nick do utilizador</summary>
		public int RulerId {
			get { return rulerId; }
			set { rulerId = value; }
		}
		
		/// <summary>Indica a aliança</summary>
		public int AllianceId {
			get { return allianceId; }
			set { 
				allianceId = value; 
				if( RulerId >= 0 ) {
					Universe.instance.getRuler(RulerId).AllianceId = value;
				}
			}
		}
		
		/// <summary>Indica o rank na aliança</summary>
		public AllianceMember.Role AllianceRank {
			get { return allianceRank; }
			set { 
				allianceRank = value;
				if( RulerId >= 0 ) {
					Universe.instance.getRuler(RulerId).AllianceRank = value;
				}
			}
		}
		
		/// <summary>Data de Registo</summary>
		public DateTime RegistDate {
			get { return registDate; }
			set { registDate = value; }
		}

		/// <summary>Data do ltimo Login</summary>
		public DateTime LastLogin {
			get { return lastLogin; }
			set { lastLogin = value; }
		}
		
		/// <summary>Indica a skin do utilizador</summary>
		public int Skin {
			get { return skin; }
			set { skin = value; }
		}
		
		/// <summary>Indica a linguagem do utilizador</summary>
		public string Lang {
			get { return lang; }
			set { lang = value; }
		}
		
		/// <summary>Indica o nick do utilizador</summary>
		public string Website {
			get { return website; }
			set { website = value; }
		}

		/// <summary>Indica o url das imagens do site</summary>
		public string ImagesDir {
			get { return imagesDir; }
			set {
				string a = value;
				a = a.Replace(@"\","/");

				if( a.EndsWith("/") || a.Trim() == string.Empty ) {
					imagesDir = value.Trim();
				} else {
					imagesDir = value.Trim() + "/";
				}
			}
		}

		/// <summary>Indica se o user est a usar as imagens no disco</summary>
		public bool HasImagesDir {
			get { return imagesDir != string.Empty; }
		}

		/// <summary>Indica o nick do utilizador</summary>
		public string Avatar {
			get {
				if( avatar != null && avatar != string.Empty ) {
					return avatar;
				}
				return "";
			}
			set { avatar = value.Trim(); }
		}

		/// <summary>Indica o nick do utilizador</summary>
		public static string DefaultAvatar {
			get {
				return OrionGlobals.getCommonImagePath("defaultAvatar.gif");
			}
		}
		
		/// <summary>Indica o nick do utilizador</summary>
		public string Msn {
			get { return msn; }
			set { msn = value; }
		}

		
		/// <summary>Indica o nick do utilizador</summary>
		public string Icq {
			get { return icq; }
			set { icq = value; }
		}

		/// <summary>Indica o nick do utilizador</summary>
		public string Jabber {
			get { return jabber; }
			set { jabber = value; }
		}
		
		/// <summary>Indica o nick do utilizador</summary>
		public string Aim {
			get { return aim; }
			set { aim = value; }
		}
		
		/// <summary>Indica o nick do utilizador</summary>
		public string Yahoo {
			get { return yahoo; }
			set { yahoo = value; }
		}
		
		/// <summary>Indica o nick do utilizador</summary>
		public string Signature {
			get { return signature; }
			set { signature = value; }
		}

		/// <summary>Indica o nome da Skin do utilizador</summary>
		public string SkinName {
			get { return _skinName; }
			set { _skinName = value; }
		}

		/// <summary>Indica o nome da Skin do utilizador</summary>
		public string SkinStyle {
			get { return _skinStyle; }
			set { _skinStyle = value; }
		}

		/// <summary>Indica o nome da Skin do utilizador</summary>
		public string SkinScript {
			get { return _skinScript; }
			set { _skinScript = value; }
		}
		
		/// <summary>Indica vitórias do utilizador</summary>
		public int Wins {
			get { return wins; }
			set { wins = value; }
		}
		
		/// <summary>Indica derrotas do utilizador</summary>
		public int Losses {
			get { return losses; }
			set { losses = value; }
		}

		#endregion

		#region IForumUser

		public string Name {
			get { 
				return Nick;
			}
		}

		public string Email {
			get { 
				return Mail;
			}
		}

		public bool IsAuthenticated {
			get {
				return !HttpContext.Current.User.IsInRole("guest");
			}
		}

		public object Location {
			get { 
				return Lang;
			}
		}

		public object HomePage {
			get {
				return Website;				
			}
		}

		public bool CanLogin {
			get {
				return true;
			}
		}

		public void UpdateUserInfo( int userID ) {
			UserUtility.bd.saveUser(this,"");
		}

		#endregion
	};

}
