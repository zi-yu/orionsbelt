using System;
using System.Web;
using System.Reflection;
using yaf.pages;

namespace yaf
{
	public class Config
	{
		private	System.Xml.XmlNode m_section;

		public Config(System.Xml.XmlNode node)
		{
			m_section = node;
		}

		public string this[string key]
		{
			get
			{
				System.Xml.XmlNode node = m_section.SelectSingleNode(key);
				if(node!=null)
					return node.InnerText;
				else
					return null;
			}
		}

		static public Config ConfigSection
		{
			get
			{
				Config config = (Config)System.Configuration.ConfigurationSettings.GetConfig("yafnet");
				if(config==null)
					throw new ApplicationException("Failed to get configuration from Web.config");
				return config;
			}
		}

		static public bool IsDotNetNuke
		{
			get
			{
				object obj = HttpContext.Current.Items["PortalSettings"];
				return obj!=null && obj.GetType().ToString().ToLower().IndexOf("dotnetnuke")>=0;
			}
		}

		static public bool IsRainbow
		{
			get
			{
				object obj = HttpContext.Current.Items["PortalSettings"];
				return obj!=null && obj.GetType().ToString().ToLower().IndexOf("rainbow")>=0;
			}
		}

		static public IUrlBuilder UrlBuilder
		{
			get
			{
				if(HttpContext.Current.Application["yaf_UrlBuilder"]==null)
				{
					string urlAssembly = "yaf.UrlBuilder,Alnitak";
					
					HttpContext.Current.Application["yaf_UrlBuilder"] = Activator.CreateInstance(Type.GetType(urlAssembly));
				}

				return (IUrlBuilder)HttpContext.Current.Application["yaf_UrlBuilder"];
			}
		}
	}
}
