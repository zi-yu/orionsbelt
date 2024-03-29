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

namespace FlexWiki
{
	/// <summary>
	/// Summary description for TopicIsAmbiguousException.
	/// </summary>
	public class TopicIsAmbiguousException : ApplicationException
	{
		public TopicIsAmbiguousException() : base()
		{
		}
		public TopicIsAmbiguousException(string message) : base(message)
		{
		}

		TopicName _Topic;

		public TopicName Topic
		{
			get
			{
				return _Topic;
			}
			set
			{
				_Topic = value;
			}
		}

		public static TopicIsAmbiguousException ForTopic(TopicName topic)
		{
			TopicIsAmbiguousException answer = new TopicIsAmbiguousException("Topic is ambiguous: " + topic.ToString());
			answer.Topic = topic;
			return answer;
		}

	}
}
