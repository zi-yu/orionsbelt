<%@ Control Language="C#" Inherits="Alnitak.AddNewsPage" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>

<p/>
<p>
	Colocar a noticia em HTML!
</p>
		<asp:RequiredFieldValidator
			ControlToValidate="title"
			Display="Dynamic"
			InitialValue=""
			runat="server"
		>
				É preciso preencher o título!<br/>
		</asp:RequiredFieldValidator>
		
		<asp:RequiredFieldValidator
			ControlToValidate="message"
			Display="Dynamic"
			InitialValue=""
			runat="server"
		>
				É preciso preencher a mensagem!<br/>
		</asp:RequiredFieldValidator>

Título:
<asp:TextBox id="title" Style="width: 90%;" CssClass="textbox" runat="server" />
<br/>
Linguagem: <asp:DropDownList ID="languages" Runat="server" /> <br/>
Mensagem:<br/>
<asp:TextBox TextMode="MultiLine" Style="width: 90%; height: 200;" CssClass="textbox" id="message" runat="server" />
<p/>
<div align="center">
	<asp:Button Text="Enviar" id="send" CssClass="button" OnClick="SendNews" runat="server" />
</div>



