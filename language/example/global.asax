<%@ Application Language="C#" %>
<script runat="server">

protected void Application_Start( object src, EventArgs e )
{
	/*Language.LanguageManager man = new Language.LanguageManager(Context.Cache,"lang","pt");
	Application["LanguageManager"] = man;*/
}
/*
protected void Application_BeginRequest( object src, EventArgs e )
{
	string lang = Request.UserLa nguages[0];
	if( lang == "pt" )
		lang = "pt-PT";		// -> para não racair no default pt-BR

	System.Threading.Thread.CurrentThread.CurrentUICulture = 
		System.Globalization.CultureInfo.CreateSpecificCulture(lang);
}*/

</script>
