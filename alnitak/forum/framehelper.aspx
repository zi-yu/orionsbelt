<%@ Page Language="C#" %>
<%@ Register TagPrefix="yaf" Namespace="yaf" Assembly="Alnitak" %>

<script language="C#" runat="server">
	Alnitak.MasterSkinInfo masterSkinInfo = (Alnitak.MasterSkinInfo)HttpContext.Current.Items["MasterSkinInfo"];
	//Alnitak.OrionGlobals.resolveBase(  + "/" + masterSkinInfo.masterSkinStyle );
</script>

<html>
<head>
<meta http-equiv="refresh" content="600">
</head>
<body>
<form runat="server" enctype="multipart/form-data" ID="Form1">
	<link type='text/css' rel='stylesheet' href='<%= Alnitak.OrionGlobals.AppPath + masterSkinInfo.masterSkinName +"/" + masterSkinInfo.masterSkinStyle%>' />
	<yaf:forum runat="server" ID="Forum1" NAME="Forum1"/>
</form>
</body>
</html>
