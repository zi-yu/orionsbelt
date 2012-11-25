// created on 5/17/04 at 10:47 a

using System.IO;
using System.Configuration;
using System.Collections.Specialized;
using NUnit.Framework;
using Chronos.Utils;

namespace Chronos.Tests {

	[TestFixture]
	public class ConfigTester {
	
		#region Instance Fields
		
		private string unixUrl = "file:///path/to/assemly/assembly.dll";
    	private string unixPath = "/path/to/assemly";
    	
		private string winUrl = "file:///c:/path/to/assemly/assembly.dll";
    	private string winPath = "c:\\path\\to\\assemly";
    	
    	
    	private string unixTrim = "/path/to/assemly/assembly.dll";
    	private string winTrim = "c:/path/to/assemly/assembly.dll";
		
		#endregion
	
		#region Config Directories Tests
    
    	[Test]
    	public void testBaseConfigDir()
    	{
    		string baseDir = Platform.BaseDir;
    		Assert.IsTrue( Directory.Exists(baseDir), (" " + baseDir + " not found") );
		}
    
    	[Test]
    	public void testResourceConfigDir()
    	{
    		string confDir = Platform.ResourceConfigDir;
    		Assert.IsTrue( Directory.Exists(confDir), (" " + confDir + " not found") );
		}
		
		[Test]
    	public void testGeneralConfigDir()
    	{
    		string baseDir = Platform.GeneralConfigDir;
    		Assert.IsTrue( Directory.Exists(baseDir), (" " + baseDir + " not found") );
		}
		
		[Test]
    	public void testLocation()
    	{
    		Assert.IsTrue( Platform.ConfigDir.IndexOf("..") < 0, "No '..' allowed." );
		}
		
		#endregion
		
		#region Platform Functional Methods Tests
		
		[Test]
    	public void testTrimWinCodeBase()
    	{	
    		if( Platform.WinPath ) {
    			Assert.AreEqual( Platform.trimCodeBase(winUrl), winTrim );
    		}
		}
		
		[Test]
    	public void testTrimUnixCodeBase()
    	{   	
			if( Platform.UnixPath ) {	
    			Assert.AreEqual( Platform.trimCodeBase(unixUrl), unixTrim );
    		}
		}
	
		[Test]
    	public void testUnixPath()
    	{   		
    		if( Platform.UnixPath ) {
    			Assert.AreEqual( Platform.codeBaseToPath(unixUrl), unixPath );
    		}
		}
		
		[Test]
    	public void testWinPath()
    	{	
    		if( Platform.WinPath ) {
    			Assert.AreEqual( Platform.codeBaseToPath(winUrl), winPath );
    		}
		}
		
		#endregion
	};

}