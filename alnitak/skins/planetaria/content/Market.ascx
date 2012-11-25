<%@ Control Language="c#" EnableViewState="True" Inherits="Alnitak.Market" %>
<%@ Register TagPrefix="orion" Namespace="Alnitak" Assembly="Alnitak" %>
<%@ Register TagPrefix="lang" Namespace="Language" Assembly="Language" %>

<script type="text/javascript">

	<%= MarketInformation() %>
	
	function OptionsClick()
	{
		var options = document.getElementById("<%= options.ClientID %>");
		var selected = options.value;
		ResetOperationTable();

		if( selected == "buy" ) {
			FillResourceList(buy, buyArray, buyArrayCount, captions);
		} else {
			FillResourceList(sell, sellArray, sellArrayCount, captions);
		}

		SetHTML("operation", captions[selected]);
		
		document.pageContent.currentOperation.value = selected;
	}
	
	function FillResourceList( items, itemsArray, count, captions )
	{
		var resources = document.getElementById("<%= resources.ClientID %>");
		resources.options.length = 0;

		for( var i = 0; i < count; ++i ) {
			var resource = itemsArray[i];
			var option = new Option(captions[resource], resource, false, false);
			resources.options.add(option);
		}
	}

	function SetHTML( name, text )
	{
		try {
			var operation = document.getElementById(name);
			operation.innerHTML = text;
		} catch( ex ) {
			alert("name:" + name + " text:" + text +" error:" + ex.message);
		}
	}

	function ResetOperationTable()
	{
		SetHTML("operation", "-");
		SetHTML("resourceLabel", "-");
		SetHTML("price", "-");
		SetHTML("total", "-");
		document.getElementById("<%= quantity.ClientID %>").value = "";
	}

	function ResourcesClick( quant )
	{
		var resources = document.getElementById("<%= resources.ClientID %>");
		var selected = resources.value;
		var price = window[document.pageContent.currentOperation.value][selected];

		if( selected == "" ) {
			return;
		}

		SetHTML("resourceLabel", captions[selected]);
		SetHTML("price", price);

		var quantity = document.getElementById("<%= quantity.ClientID %>");
		if( quant == -1 ) {
			quantity.value = window[document.pageContent.currentOperation.value+"Totals"][selected];
		} else {
			quantity.value = quant;
		}
		document.pageContent.resource.value = selected;

		SetHTML("total", quantity.value * price);
	}


	function QuantityDown( elem )
	{
		elem.originalValue = elem.value;
	}

	function QuantityChanged( elem )
	{
		if( !isPositiveInt(elem.value) ) {
			elem.value= elem.originalValue;
		} else {
			ResourcesClick(elem.value);
		}
	}
	
</script>

<orion:PlanetNavigation id="planetNavigation" runat="server" />
<orion:QueueErrorReport id="operationReport" runat="server" />

<div id="market">
	<table>
		<tr>
			<td>
				<asp:ListBox id="options" onclick="OptionsClick();" runat="server" />
			</td>
			<td>
				<asp:ListBox id="resources" onclick="ResourcesClick(-1);" runat="server" />
			</td>
			<td>
				<table id="market_operations">
					<tr>
						<td>
							<lang:Label ref="operation" runat="server" />
						</td>
						<td>
							<div id="operation">-</div>
						</td>
					</tr>
					<tr>
						<td>
							<lang:Label ref="recurso" runat="server" />
						</td>
						<td>
							<div id="resourceLabel">-</div>
						</td>
					</tr>
					<tr>
						<td>
							<lang:Label ref="Price" runat="server" /> / 1
						</td>
						<td>
							<div id="price">-</div>
						</td>
					</tr>
					<tr>
						<td>
							<lang:Label ref="quantidade" runat="server" />
						</td>
						<td>
							<asp:TextBox onkeydown="QuantityDown(this);" onkeyup="QuantityChanged(this);" id="quantity" runat="server" />
						</td>
					</tr>
					<tr>
						<td>
							<lang:Label ref="Total" runat="server" />
						</td>
						<td>
							<div id="total">-</operation>
						</td>
					</tr>
					<tr>
						<td>
						</td>
						<td>
							<asp:Button id="operate" OnClick="Operate" runat="server" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<div class=nav>
		<lang:Label ref="PlanetMoney" runat="server" />: <b><asp:Label id="planetMoney" runat="server" /></b><br/>
		<lang:Label ref="PlanetMarkets" runat="server" />: <asp:Label id="planetMarkets" runat="server" />
	</div>
	<table id="list">
		<tr>
			<td valign="top">
				<orion:MarketItemList id="sellTable" runat="server" />
			</td>
			<td valign="top">
				<orion:MarketItemList id="buyTable" runat="server" />
			</td>
		</tr>
	</table>
</div>

<ul class="help_zone">
	<li><asp:HyperLink runat="server" id="marketHelp" /></li>
</ul>
