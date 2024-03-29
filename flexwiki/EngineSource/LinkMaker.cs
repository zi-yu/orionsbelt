#region License Statement
// Copyright (c) Microsoft Corporation.  All rights reserved.
//
// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software.
#endregion

using System;
using System.Text;
using System.Web;


namespace FlexWiki
{
	/// <summary>
	/// LinkMaker understand how to make links to the various pages that make the wiki work
	/// </summary>
	[ExposedClass("LinkMaker", "Builds hyperlinks to various important pages")]
	public class LinkMaker : BELObject
	{
		private string	_SiteURL;

		public LinkMaker(string siteURL)
		{
			_SiteURL = siteURL;
		}

		public string SiteURL()
		{
			return _SiteURL;
		}

		AbsoluteTopicName _ReturnToTopicForEditLinks;

		public AbsoluteTopicName ReturnToTopicForEditLinks
		{
			get
			{
				return _ReturnToTopicForEditLinks;
			}
			set
			{
				_ReturnToTopicForEditLinks = value;
			}
		}

		public string LinkToUser(string user)
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("<a href=\"mailto:");
			builder.Append(user);
			builder.Append("\">");
			builder.Append(user);
			builder.Append("</a>");			
			return builder.ToString();
		}

		[ExposedMethod(ExposedMethodFlags.CachePolicyNone, "Answer a link to the given image")]
		public string LinkToImage(string s)
		{
			return SimpleLinkTo(s);
		}

		[ExposedMethod(ExposedMethodFlags.CachePolicyNone, "Answer a link to the search page for the given namespace")]
		public string LinkToSearchNamespace(string ns)
		{
			return SimpleLinkTo("search.aspx" + (ns != null ? "?namespace=" + ns : "") );
		}

		[ExposedMethod(ExposedMethodFlags.CachePolicyNone, "Answer a link to search for the given string in the given namespace")]
		public string LinkToSearchFor(string Namespace, string searchExpression)
		{
			return SimpleLinkTo("search.aspx" + (Namespace != null ? "?namespace=" + Namespace : "") + (searchExpression != null ? "?search=" + HttpUtility.UrlEncode(searchExpression) : "") );
		}

		[ExposedMethod(ExposedMethodFlags.CachePolicyNone, "Answer a link to a list of all the versions for the given topic")]
		public string LinkToVersions(string fullTopicName)
		{
			return SimpleLinkTo("versions.aspx" + (fullTopicName != null ? "?topic=" + HttpUtility.UrlEncode(fullTopicName) : "") );
		}

		public string LinkToQuicklink()
		{
			return SimpleLinkTo("quicklink.aspx");
		}

		[ExposedMethod(ExposedMethodFlags.CachePolicyNone, "Answer a link to the recent changes list for the given namespace")]
		public string LinkToRecentChanges(string ns)
		{
			return SimpleLinkTo("lastmodified.aspx"+ (ns != null ? "?namespace=" + ns : "") );
		}

		[ExposedMethod(ExposedMethodFlags.CachePolicyNone, "Answer a link to the page that allows the given topic to be renamed")]
		public string LinkToRename(string fullyQualifiedTopicName)
		{
			return SimpleLinkTo("rename.aspx"+ (fullyQualifiedTopicName != null ? "?topic=" + fullyQualifiedTopicName: "") );
		}

		[ExposedMethod(ExposedMethodFlags.CachePolicyNone, "Answer a link to the list of subscriptions for the given namespace (or null for all)")]
		public string LinkToSubscriptions(string ns)
		{
			return SimpleLinkTo("rssface.aspx"+ (ns != null ? "?namespace=" + ns : "") );
		}

		[ExposedMethod(ExposedMethodFlags.CachePolicyNone, "Answer a link to the lost and found for the given namespace")]
		public string LinkToLostAndFound(string ns)
		{
			return SimpleLinkTo("lostandfound.aspx"+ (ns != null ? "?namespace=" + ns : "") );
		}

		public virtual string SimpleLinkTo(string s)
		{
			StringBuilder builder = new StringBuilder();
			builder.Append(SiteURL());
			builder.Append(s);
			return builder.ToString();
		}

		[ExposedMethod(ExposedMethodFlags.CachePolicyNone, "Answer a link to the given topic")]
		public string LinkToTopic(string topic)
		{
			return TopicLink(topic, false);
		}

		[ExposedMethod(ExposedMethodFlags.CachePolicyNone, "Answer a link to display the given topic with diffs highlighted")]
		public string LinkToTopicWithDiffs(string topic)
		{
			return TopicLink(topic, true);
		}

		public string LinkToTopic(TopicName topic)
		{
			return LinkToTopic(topic, false);
		}

		public string LinkToTopic(TopicName topic, bool showDiffs)
		{
			return TopicLink(topic.FullnameWithVersion, showDiffs);
		}

		public virtual string TopicLink(string top, bool showDiffs)
		{
			StringBuilder builder = new StringBuilder();
			builder.Append(SiteURL());
			builder.Append("default.aspx/");
			builder.Append(top);
			string query = "";
			if (showDiffs)
			{
				if (query != "")
					query += "&";
				query += "diff=y";
			}

			if (query != "")
			{
				builder.Append("?");
				builder.Append(query);
			}

			return builder.ToString();
		}
	

		[ExposedMethod(ExposedMethodFlags.CachePolicyNone, "Answer a link to the page that allows a user to login")]
		public string LinkToLogin(string topic)
		{
			StringBuilder builder = new StringBuilder();
			builder.Append(SiteURL());
			builder.Append("login.aspx?ReturnURL=default.aspx\\");
			builder.Append(topic);
			return builder.ToString();
		}

		public string LinkToLogin(TopicName topic)
		{
			return LinkToLogin(topic.FullnameWithVersion);
		}

		public string LinkToLogoff(TopicName topic)
		{
			return LinkToLogoff(topic);
		}


		[ExposedMethod(ExposedMethodFlags.CachePolicyNone, "Answer a link to the page that logs off the current user")]
		public string LinkToLogoff(string topic)
		{
			StringBuilder builder = new StringBuilder();
			builder.Append(SiteURL());
			builder.Append("logoff.aspx");
			return builder.ToString();
		}
		
		[ExposedMethod(ExposedMethodFlags.CachePolicyNone, "Answer a link to the 'print' view of the given topic")]
		public string LinkToPrintView(string topic)
		{
			return PrintLink(topic);
		}
			
		public string LinkToPrintView(TopicName topic)
		{
			return PrintLink(topic.FullnameWithVersion);
		}

		string PrintLink(string top)
		{
			StringBuilder builder = new StringBuilder();
			builder.Append(SiteURL());
			builder.Append("print.aspx/");
			builder.Append(top);
			return builder.ToString();
		}

		public string LinkToEditTopic(AbsoluteTopicName topic)
		{
			return EditLink(topic.Fullname);
		}

		[ExposedMethod(ExposedMethodFlags.CachePolicyNone, "Answer a link to the page that lets the user edit the given topic")]
		public string LinkToEditTopic(string topic)
		{
			return EditLink(topic);
		}

		string EditLink(string top)
		{
			StringBuilder builder = new StringBuilder();
			builder.Append(SiteURL());
			builder.Append("wikiedit.aspx?topic=");
			builder.Append(HttpUtility.UrlEncode(top));
			if (ReturnToTopicForEditLinks != null)
				builder.Append("&return=" + HttpUtility.UrlEncode(ReturnToTopicForEditLinks.Fullname));

			return builder.ToString();
		}


		[ExposedMethod(ExposedMethodFlags.CachePolicyNone, "Answer a link to the topic that processes a restore")]
		public string LinkToRestore(string topic)
		{
			return RestoreLink(topic);
		}

		public string LinkToRestore(TopicName topic)
		{
			return RestoreLink(topic.FullnameWithVersion);
		}

		/// <summary>
		/// Creates the Restore link
		/// </summary>
		/// <param name="topic">TopicName for the link</param>
		/// <returns></returns>
		string RestoreLink(string top)
		{
			StringBuilder builder = new StringBuilder();
			builder.Append(SiteURL());
			builder.Append("default.aspx/");
			builder.Append(top);
			string query = "";
			query += "&";
			query += "restore=y";
			builder.Append("?");
			builder.Append(query);
			return builder.ToString();
		}
		/// <summary>
		/// Creates the Change User Profile link
		/// </summary>
		/// <param name="topic">TopicName for the link</param>
		/// <returns></returns>
		public string LinkToChangeUserProfile(int userID)
		{
			StringBuilder builder = new StringBuilder();
			builder.Append(SiteURL());
			builder.Append("UserProfile.aspx?UserID=");
			builder.Append(userID.ToString());
			builder.Append("&Mode=2");
			return builder.ToString();
		}
		/// <summary>
		/// Creates the Create User Profile link
		/// </summary>
		/// <param name="topic">TopicName for the link</param>
		/// <returns></returns>
		public string LinkToCreateUserProfile()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append(SiteURL());
			builder.Append("UserProfile.aspx?UserID=0");
			builder.Append("&Mode=1");
			return builder.ToString();
		}

	}
}
