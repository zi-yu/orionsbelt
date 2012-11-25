<%@ Page language="c#" Codebehind="globalError.aspx.cs" Inherits="Alnitak.GlobalError" AutoEventWireup="false" %>
<html>
	<head>
		<title>Orion's Belt</title>
		<style>
			body { background-color:e0dfe3;}
			p, table,h1 { font-size: 0.9em; font-family: arial;}
			h1 { font-size: 1.7em; }
			table { border:1px solid #9d9da1; background-color:white; }
			div { margin:100px; }
			#content1 { padding-top:50px; width:100px;  }
			#content2 { height:100%; padding-top:20px;padding-left:5px;padding-right:10px;padding-bottom:10px; }
			#contentTitle { padding:10px; }
			.red { color:red;}
		</style>
	</head>
	<body>		
		<div align="center">
			<table width="640px" height="310px" >
				<tr valign="top">
					<td id="content1" align="center" rowspan="2" >
						<img src="skins/commonImages/information.gif" />
					</td>
					<td id="contentTitle">
						<h1 >
							Orion's Belt is Down
						</h1>
						<b>English:</b>
						<p>
							Orion's Belt isn't available at the moment. Please try again later.
						</p>
						<p>
							If the problem persists, please contact the administrators for further information.
						</p>
					</td>
				</tr>
				<tr >
					<td id="contentTitle"  >
						<h1 >
							Orion's Belt Indisponível
						</h1>
						<b>Português:</b>
						<p>
							O jogo Orion's Belt não está disponivel de momento. Por favor tenta mais tarde.
						</p>
						<p>
							Se o problema persistir contacta os administradores para mais informação.
						</p>
					</td>
				</tr>	
			</table>
		</div>
	</body>
</html>
