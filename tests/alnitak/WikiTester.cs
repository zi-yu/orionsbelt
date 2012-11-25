// created on 11/17/2004 at 4:53 PM

using System;
using System.Collections;
using Chronos.Resources;
using Chronos.Info.Results;
using Chronos.Core;
using Chronos;
using Chronos.Utils;
using Alnitak;
using NUnit.Framework;

namespace Chronos.Tests {
	
	[TestFixture]
	public class WikiTester {
		
		#region Instance Fields
		
		private const string someLink = "http://buu.com/ruler/default.aspx";
		private const string wikiBase = "http://buu.com/wiki/default.aspx";
		private const string wikiTopic = "http://buu.com/wiki/default.aspx/Buu.Topic";
		
		#endregion
		
		#region WikiTopic Tests
		
		[Test]
		public void test_ParseSomeLink()
		{
			OrionTopic topic = Wiki.ParseTopic(someLink);
			Assert.AreEqual(null, topic);
		}
		
		[Test]
		public void test_ParseWikiBase()
		{
			OrionTopic topic = Wiki.ParseTopic(wikiBase);
			Assert.AreEqual(null, topic);
		}
		
		[Test]
		public void test_ParseWikiTopic()
		{
			OrionTopic topic = Wiki.ParseTopic(wikiTopic);
			Assert.IsTrue(topic != null, "#1");
			Assert.AreEqual("Buu.Topic", topic.FullTopic, "#2");
		}
		
		#endregion
		
	};
	
}
