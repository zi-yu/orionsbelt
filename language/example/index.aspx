<%@ Page Language="C#" AutoEventWireUp="false" Trace="True" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>
<script runat="server">

protected ILanguageInfo info = null;
protected string culture;

protected override void OnInit( EventArgs e )
{
	base.OnInit(e);
	culture = System.Globalization.CultureInfo.CurrentUICulture.Name;
	
	LanguageManager man = (LanguageManager) Application["LanguageManager"];
	info = man.getLanguageInfo( culture );
}

</script>
<html>
	<head>
		<title>
			<lang:Label Ref="page-title" runat="server"/>
		</title>
	</head>

	<body>
		<h2>
			<lang:Label Ref="page-title" runat="server"/>
		</h2>

		<lang:Label Ref="body" Target="index" runat="server"/>
		<p/>
		<lang:Label Ref="locale-info" Target="index" runat="server" /> 
		<%= culture %>
		
	</body>
	
</html>
