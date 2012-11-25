var lastSelection = null;
var quantity = document.getElementById("quantity");
var mouseOverElement;

function selected( id ) {
	
	if(id.split("_").length == 2 && lastSelection == null ) {
		return;
	}
	
	var selectedElement = new Item(id);
	
	if( document.getElementById("moves").innerHTML == 0) {
		return;
	}
	
	if( lastSelection == null ) {
		noneSelected(selectedElement);
		fillInformations(id);		
	} else {
		if( sameSector(selectedElement) ) {
			quantity.value = "";
			fillInfo("minquantity","");
			fillInfo("maxquantity","");
			return;
		}
		oneSelected(selectedElement);
	}
}

function hideImage() {
	if( img != null && ( tmp_x != x || tmp_y != y ) ) {
		img.className = "invisible";
		img = null;
	}
}
// =================== First CLICK ===================

function isSrcShip( element ) {
	if( element.hasChildNodes()) {
		if( element.node.id.indexOf("_") == -1 )
			return true;
	}
	return false;
}

function noneSelected(selectedElement) {
	if( !selectedElement.hasChildNodes() || isEnemyShip(selectedElement) ) {
		return;
	}
	
	selectedElement.setClass("selectedSector");
	quantity.value = selectedElement.getQuantity();	
	
	min = Math.round( 0.5+(Number(selectedElement.getQuantity())*0.2) );
	
	fillInfo("minquantity",min);
	fillInfo("maxquantity",quantity.value - min)
	
	lastSelection = selectedElement;
}

// =================== Second CLICK ===================

function sameSector( selectedElement ) {
	if( lastSelection.id == selectedElement.id ) {
		lastSelection.setClass("");
		lastSelection = null;
		return true;
	}
	return false;
}

function oneSelected(selectedElement) {
	if( canMoveOver(selectedElement.id,null) ) {
		var quant = Number(lastSelection.getQuantity());
		var quantitySelected = Number(quantity.value);
		
		if( quantitySelected <= quant && quantity.value > 0 ) {
					
			var minRest = Math.round( 0.5+(Number(lastSelection.getQuantity())*0.2) );
			
			if( quantitySelected < minRest ) {
				RaiseMinimumMoveError(quantitySelected,lastSelection.getCleanImageName(),minRest);
				return;
			}
			
			var quantRest = quant - quantitySelected;
			if( quantRest < minRest && quantRest != 0 ) {
				RaiseMinimumRestError(quant - quantitySelected,lastSelection.getCleanImageName(),minRest);
				return;
			}
			
			selectedElement.removeAll();
			selectedElement.insert(lastSelection,quantity.value);
			var total = lastSelection.getQuantity() - quantity.value;
			if( total > 0 ) {
				lastSelection.setQuantity( total);
			}else{
				lastSelection.removeAll();
			}
			
			registerMove(lastSelection.id,selectedElement.id,quantity.value);
			
			selectedElement.setClass("");
			lastSelection.setClass("");
			lastSelection = null;
		}else{
			RaiseQuantityError();
		}
	}
}

function canMoveOver(id,event) {
	hideImage();
	if( lastSelection != null ) {
		if( lastSelection.id == id )
			return false;
		mouseOverElement = new Item(id);
		
		if( isSrcShip(mouseOverElement) ) {
			return false;
		}
		
		if( mouseOverElement.hasChildNodes() || isEnemyShip(mouseOverElement) ) {
			showImage(mouseOverElement,"cannotMove",event);
			//mouseOverElement.setClass("cannotMove");
			return;
		}
		
		var dest = id.split("_");
				
		if( dest[0] == 7 || dest[0] == 8  ) {
			mouseOverElement.setClass("canMove");
			return true;		
		}
	}
	return false;
}

function canMoveOut() {
	if( mouseOverElement != null ) {
		mouseOverElement.setClass("");
		mouseOverElement = null;
	}
}

//===================== Information ===============================

function fillInformations( id ) {

	var idl = id.toLowerCase()
	fillInfo( "shipType",Unit[idl].name );
	fillInfo( "baseAttack",Unit[idl].baseAttack );
	fillInfo( "baseDefense",Unit[idl].baseDefense );
	fillInfo( "baseLive",Unit[idl].hitPoints );
	fillInfo( "movementCost",Unit[idl].movementCost );
	fillInfo( "movementType",Unit[idl].movementType );
	fillInfo( "range",Unit[idl].range );
	fillInfo( "unitQuant",document.getElementById(id+"_unit").title );
}

function fillInfo( id, value ) {
	var elem = document.getElementById(id);
	if( elem.hasChildNodes()) {
		elem.removeChild(elem.firstChild);
	}
	elem.innerHTML = value;
}

//===================== Check ===============================

function checkSrcShips() {
	
	var nodes = document.getElementById("srcShips").childNodes;
	
	for (var i = 0; i < nodes.length; ++i) {
		var td = nodes[i].firstChild.childNodes;
		for (var j = 0; j < td.length; ++j) {
			if( td[j].hasChildNodes() ) {
				RaiseShipsLeftError();
				return false;
			}
		}
	}
	return true;
}

var theform = document.pageContent;

//===================== Moves ===============================

function registerMove( srcId, dstId, total ) {
	theform.movesMade.value += "move:"+srcId+"-"+dstId+"-"+total+";";
}

//===================== Undo ===============================

function undo() {
	if( theform.movesMade.value == "" )
		return;
	var m = theform.movesMade.value.split(";");
	theform.movesMade.value = "";
	for( var i = 0; i < m.length-2 ; ++i ) {
		theform.movesMade.value += m[i] + ";";
	}
	parseMove( m[m.length-2] );
}

function parseMove( move ) {
	var m = move.split(":");
	mFinal = m[1].split("-");
		
	var src = new Item(mFinal[0]);
	var dst = new Item(mFinal[1]);
	
	if( src.hasChildNodes() ) {
		src.setQuantity( Number(src.getQuantity()) + Number(dst.getQuantity()) );
	}else {
		src.insert(dst, dst.getQuantity());
	}
	src.setClass("");
	dst.removeAll();
	dst.insertSpace();
}