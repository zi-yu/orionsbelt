using System;
using System.Collections;
using System.Web.UI;
using Chronos.Core;
using Language;

namespace Alnitak {
	public abstract class ShowFleet : Control {
		
		protected Ruler ruler = null;
		protected ILanguageInfo info = CultureModule.getLanguage();
		protected string title = string.Empty;

		#region protected

		/// <summary>Retorna o Ruler da sessão</summary>
		protected Ruler getRuler() {
			User user = (User) Page.User;
			return Universe.instance.getRuler(user.RulerId);
		}

		protected void registerScript() {
			
			string question = CultureModule.getLanguage().getContent("fleet_removeFleet");

			string script = @"
				<script language='javascript'>
					function removeFleet( fleet ) {
						var theform = document.pageContent;
						var resp = confirm('"+question+@"');
						if( resp == true ) {
							theform.fleetToRemove.value = fleet;
							theform.submit();
						}
					}
				</script>";
			
			Page.RegisterClientScriptBlock("removeFleet",script);
			Page.RegisterHiddenField("fleetToRemove","");
		}

		protected void remove( string name ) {
			Chronos.Core.Fleet srcFleet = ruler.getFleet( name );

			if( srcFleet != null ) {
				if( srcFleet.Owner != ruler ) {
					Planet p = (Planet)srcFleet.Owner;
					Chronos.Core.Fleet dstFleet = p.getPlanetFleet();
					IDictionaryEnumerator iter = srcFleet.Ships.GetEnumerator();
					while( iter.MoveNext() ) {
						dstFleet.swapShips( srcFleet , iter.Key.ToString() , int.Parse( iter.Value.ToString() ) );						
						iter = srcFleet.Ships.GetEnumerator();
					}

					p.removeFleet( srcFleet );
					Information.AddInformation( info.getContent("fleet_removeOk"));
				}else {
					if( !srcFleet.HasShips ) {
						ruler.removeUniverseFleet(srcFleet.Id);
						Information.AddInformation( info.getContent("fleet_removeOkUniverse"));
					}
				}
			}
			
		}

		#endregion

		#region overrided

		/// <summary>
		/// carrega todas as informações sobre o Ruler, introduz as informações no repeater
		/// e carrega os contrôlos
		/// </summary>
		protected override void OnInit(EventArgs e) {
			ruler = getRuler();

			string name = Page.Request.Form["fleetToRemove"];
			if( name != null && name != string.Empty ) {
				remove( name );	
			}

			//regista o codigo de script para esconder/mostrar a fleet
			OrionGlobals.registerShowHideScript( Page );
			base.OnInit (e);
		}

		protected override void OnPreRender(EventArgs e) {
			registerScript();
			base.OnPreRender (e);
		}

		#endregion

		#region Render

		#region Fleet Title

		private string GetFleetName( Chronos.Core.Fleet fleet ) {
			if( fleet.IsDefenseFleet ) {
				return info.getContent("defenseFleet");
			}

			Planet planet = fleet.Owner as Planet;
			if( planet != null ) {
				return FleetPlanetName(planet, fleet.Name);
			}else {
				return FleetUniverseName(fleet);
			}
		}

		private string FleetPlanetName(Planet planet, string name) {
			if( planet.IsInBattle) {
				return name + " - " + info.getContent("fleet_inBattle");
			}else {
				string link = OrionGlobals.getSectionBaseUrl("Fleet") + "?id=" + planet.Id;
				return string.Format( "<a href='{0}'>{1}</a>", link, name );
			}
		}

		private string FleetUniverseName(Chronos.Core.Fleet fleet) {
			if( fleet.IsInBattle) {
				return fleet.Name + " - " + info.getContent("fleet_inBattle");
			}
			return fleet.Name;
		}

		private string GetRemoveCode( string name ) {
			return string.Format(@"
				<a href='javascript:removeFleet({0})'>
					<img src='{1}' title='{2}' />
				</a>",
				"\""+name+"\"",
				OrionGlobals.getCommonImagePath("remove.gif"),
				info.getContent("fleet_remove")
			);
		}

		private void WriteHeader( HtmlTextWriter writer, Chronos.Core.Fleet fleet ) {
			writer.WriteLine( "<table class='frame' width='400' cellpadding='0' cellspacing='0'><tr>" );

			writer.WriteLine( 
				@"<td class='smallPadding' width='360'>
					<img src='{0}' onClick=" +  "\"" + "show('fleet_{1}',this);\"" +@"/>
					<b><span class='fleetName'>{2}</b>
				</td>
				",OrionGlobals.getCommonImagePath( "plus.gif" ),fleet.Id,GetFleetName( fleet )
			);

			if( fleet.CanBeRemoved ) {
				writer.WriteLine( @"
					<td align='left' width='40'>
						{0}					
					</td>
				", GetRemoveCode(fleet.Name) );	
			}else {
				writer.WriteLine("<td></td>");
			}
		}

		#endregion

		#region Fleet Information

		private void WriteFleetInformation( HtmlTextWriter writer, Chronos.Core.Fleet fleet) {
			string state = info.getContent("fleet_state");
			string information = string.Empty;

            if( fleet.IsMoving ){
				state += info.getContent("fleet_inMovement");
				string arrival = string.Format(info.getContent("fleet_turnsToGo"), fleet.HoursToArrive );
				information = string.Format("<b>{0}:</b> {1}<br/>{2}", info.getContent("fleet_coordinate"), fleet.DestinyCoordinate.ToString(), arrival);
			}else{
				if( fleet.IsInBattle ) {
					state += info.getContent("fleet_inBattle");
					information = info.getContent("fleet_battleCoordinate") + fleet.Coordinate.ToString();
				}else{
					if( fleet.IsMoveable ) {
						state += info.getContent("fleet_stopped");
						if( fleet.Owner is Planet ) {
							Planet p = ((Planet)fleet.Owner);
							string link = OrionGlobals.getSectionBaseUrl("Fleet") + "?id=" + p.Id;
							information = string.Format( "{0}<a href='{1}'>{2}</a>", info.getContent("fleet_location"), link, p.Name );
						} else {
							information = info.getContent("fleet_location") + fleet.Coordinate.ToString();
						}
					}else{
						return;
					}
				}
			}

			writer.WriteLine( @"
				<tr>
					<td class='borderTop' colspan='3' style='padding: 5px 0px 5px 0px;'>
						{0}<br/>
						{1}
					</td>
				</tr>",state,information
			);
		}
	
		#endregion

		#region Ships

		private void WriteShips( HtmlTextWriter writer, Chronos.Core.Fleet fleet ) {
			foreach( DictionaryEntry ship in fleet.Ships ) {
				string name = string.Empty ,quant = string.Empty;
				if( ship.Key != null ) {
					name = ship.Key.ToString();
				}
				if( ship.Value != null ) {
					quant = ship.Value.ToString();
				}

				writer.WriteLine( 
					@"<tr height='40'>
						<td width='80' align='center' class='borderTop'>
							<img src='{0}.gif' />
						</td>
						<td width='120' align='center' class='borderTop'>
							{1}
						</td>
						<td width='200' align='center' class='borderTop smallPadding'>
							{2}
						</td>
					</tr>",
					OrionGlobals.getCommonImagePath( name.ToLower() ),
					quant,
					info.getContent(name)
				);
			}
		}

		#endregion

		#region Hidden Content
		
		private void WriteHiddenContent( HtmlTextWriter writer, Chronos.Core.Fleet fleet ) {
			writer.WriteLine( "<tr><td colspan='2' ><table border='0' id='fleet_{0}' cellpadding='0' cellspacing='0' style='display:none;'>",fleet.Id );
			
			WriteFleetInformation(writer,fleet);

			writer.WriteLine( 
				@"<tr>
					<td align='center' class='borderTop'>
						&nbsp;
					</td>
					<td align='center' class='borderTop'>
						{0}
					</td>
					<td align='center' class='borderTop'>
						{1}
					</td>
				</tr>",info.getContent( "fleet_quant" ),info.getContent( "fleet_type" )
			);

			WriteShips(writer,fleet);

			writer.WriteLine( "</table></td></tr>" );
		}

		#endregion

		#region NoShips
		private void WriteNoShips( HtmlTextWriter writer, int index ) {
			writer.WriteLine( @"<tr >
				<td colspan='2'>
					<table id='fleet_{0}' cellpadding='0' cellspacing='0'  width='400' style='display:none;'>
						<tr>
							<td width='400' class='borderTop smallPadding'>
								{1}
							</td>
						</tr>
					</table>
				</td>
			</tr>",index,info.getContent( "military_noShipsInThisFleet" ) );

		}
		#endregion

		protected override void Render(HtmlTextWriter writer) {

			ArrayList fleets = getAllFleets();

			if( fleets.Count != 0 ) {
				writer.WriteLine( 
					@"<div class='planetInfoZoneTitle' width='100%' style='margin-bottom:2px;'>
						<b>{0}</b>
					</div>", title );


				foreach( Chronos.Core.Fleet fleet in fleets ) {
					WriteHeader( writer, fleet );
					
					if( fleet.HasShips) {
						WriteHiddenContent( writer, fleet );
					}else {
						WriteNoShips(writer, fleet.Id );					
					}

					writer.WriteLine( "</table><br/>" );
				}
			}
		}

		#endregion

		#region Abstract

		/// <summary>
		/// quando overrided, retorna as fleets correctas
		/// </summary>
		/// <returns></returns>
		protected abstract ArrayList getAllFleets();

		#endregion
	}

}
