<%@ Page Language="C#" %>
<%@ Register TagPrefix="yaf" Namespace="yaf" Assembly="Alnitak" %>

<script runat="server">
public void Page_Error(object sender,System.EventArgs e)
{
	yaf.Utils.LogToMail(Server.GetLastError());
}
</script>

<html>
	<head>
		<meta name="Description" content="A bulletin board system written in ASP.NET">
		<meta name="Keywords" content="Yet Another Forum.net, Forum, ASP.NET, BB, Bulletin Board, opensource">

		<title runat="server" id="ForumTitle">This title is overwritten</title>
	</head>
	<body >

		<img src="images/logo.jpg"  />
		<br />

		<form runat="server" enctype="multipart/form-data">
			<yaf:forum runat="server" />
		</form>

	</body>
</html>
