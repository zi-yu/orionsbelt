/* ============================= MOVES ================================ */
function Moves() {
	this.none = function( src, dst ) {
		return false;
	}
	
	this.all = function( src, dst ) {
		var s_x = Number(src[0]);
		var s_y = Number(src[1]);
		var d_x = Number(dst[0]);
		var d_y = Number(dst[1]);

		if( d_x <= s_x + 1 && d_x >= s_x - 1 ) {
			if( d_y <= s_y + 1 && d_y >= s_y - 1 ) {
				return true;
			}
		}
		return false;
	}
	this.normal = function( src, dst ) {
		var s_x = Number(src[0]);
		var s_y = Number(src[1]);
		var d_x = Number(dst[0]);
		var d_y = Number(dst[1]);

		if( d_x <= s_x + 1 && d_x >= s_x - 1 && s_y == d_y ) {
			return true;
		}
		
		if( d_y <= s_y + 1 && d_y >= s_y - 1 && s_x == d_x ) {
			return true;
		}
		return false;
	}
	this.front = function( src, dst ) {
		return this[lastSelection.getPosition()](src,dst);
	}
	this.diagonal = function( src, dst ) {
		var s_x = Number(src[0]);
		var s_y = Number(src[1]);
		var d_x = Number(dst[0]);
		var d_y = Number(dst[1]);

		if( d_x == s_x+1 && d_y == s_y+1 ||
			d_x == s_x-1 && d_y == s_y-1 ||
			d_x == s_x+1 && d_y == s_y-1 ||
			d_x == s_x-1 && d_y == s_y+1 ) {
			return true;
		}
		
		return false;
	}
	this.n = function(src, dst) {
		var s_x = Number(src[0]);
		var d_x = Number(dst[0]);
		var s_y = Number(src[1]);
		var d_y = Number(dst[1]);
			
		if( d_x == s_x-1 && s_y == d_y )
			return true;
		return false;
	}
	this.s = function(src, dst) {
		var s_x = Number(src[0]);
		var d_x = Number(dst[0]);
		var s_y = Number(src[1]);
		var d_y = Number(dst[1]);
			
		if( d_x == s_x+1  && s_y == d_y )
			return true;
		return false;
	}
	this.w = function(src, dst) {
		var s_y = Number(src[1]);
		var d_y = Number(dst[1]);
		var s_x = Number(src[0]);
		var d_x = Number(dst[0]);
		
		if( d_y == s_y-1 && s_x == d_x )
			return true;
		return false;
	}
	this.e = function(src, dst) {
		var s_y = Number(src[1]);
		var d_y = Number(dst[1]);
		var s_x = Number(src[0]);
		var d_x = Number(dst[0]);
		
		if( d_y == s_y+1 && s_x == d_x )
			return true;
		return false;
	}
	this.incrementMoves = function( quant ) {
		var m = document.getElementById("moves");
		var mValue = Number( m.innerHTML );
		m.innerHTML = mValue + quant;
	}
	this.hasMoves = function( quant ) {
		var m = Number( document.getElementById("moves").innerHTML );		

		if( m < quant ) {
			RaiseMovesError();
			return false;
		}
		return true;
	}
	this.decrementMoves = function( quant ) {
		var m = document.getElementById("moves");
		var mValue = Number( m.innerHTML );
		
		m.innerHTML = mValue - quant;
	}
}

var movesObj = new Moves();

/* ============================= ATTACK ================================ */

function Attack( source, dest, range, unit ) {
	this.r = range;
	this.s_x = Number(source[0]);
	this.s_y = Number(source[1]);
	this.d_x = Number(dest[0]);
	this.d_y = Number(dest[1]);
	this.unit = unit;
	
	//Verifica se o caminho está livre
	this.checkPathVert = function( stat, src, dst ) {
		for( var i = src; i < dst; ++i ) {
			var item = new Item(i+"_"+stat);
			if( item.hasChildNodes() && !unit.catapultAttack ) {
				return false;
			}
		}
		return true;
	}
	//Verifica se o caminho está livre Horizontalment
	this.checkPathHoriz = function( stat, src, dst ) {
		for( var i = src; i < dst; ++i ) {
			var item = new Item(stat+"_"+i);
			if( item.hasChildNodes() && !unit.catapultAttack ) {
				return false;
			}
		}
		return true;
	}
	this.n = function() {
		var v = this.s_x-this.r;
		if( this.checkPathVert(this.s_y,this.d_x+1,this.s_x) ){
			return this.d_x < this.s_x && this.d_x >= v && this.s_y == this.d_y;
		}
		return false;
	}
	this.s = function() {
		var v = this.s_x+this.r;
		if( this.checkPathVert(this.s_y,this.s_x+1,this.d_x) ){
			return this.d_x > this.s_x && this.d_x <= v && this.s_y == this.d_y;
		}
		return false;
	}
	this.w = function() {
		var v = this.s_y-this.r;
		if( this.checkPathHoriz(this.s_x,this.d_y+1,this.s_y) ) {
			return this.d_y < this.s_y && this.d_y >= v && this.s_x == this.d_x;
		}
		return false;
	}
	this.e = function() {
		var v = this.s_y+this.r;
		if( this.checkPathHoriz(this.s_x,this.s_y+1,this.d_y) ) {
			return this.d_y > this.s_y && this.d_y <= v && this.s_x == this.d_x;
		}
		return false;
	}
}

/* ====================================================================== */
var lastSelection = null;
var quantity = document.getElementById("quantity");
var mouseOverElement;

function selected( id ) {
	var selectedElement = getItem( id );
		
	if( document.getElementById("moves").innerHTML == 0 ) {
		if( lastSelection != null ) {
			resetSelection(null);
		}
		return;
	}
	
	if( selectedElement.hasAttack ) {
		RaiseAlreadyAttackedError();
		return;
	}
	
	if( lastSelection == null ) {
		if(selectedElement.isSpace())
			return;
		if( !isEnemyShip( selectedElement ) ) {
			noneSelected(selectedElement);
			fillInformations(id);
		}
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

function attack() {
	if( currentEnemyId == null ) {
		return;
	}
	var elem = getItem(currentEnemyId);
	if( isEnemyShip(elem) && canAttack(currentEnemyId.split("_") ) && !lastSelection.hasAttack ) {
		lastSelection.hasAttack = true;
		registerBattle(lastSelection.id,elem.id);
		if( img != null ) {
			img.className = "invisible";
			img = null;
		}
		movesObj.decrementMoves(1);
		lastSelection.setClass("");
		lastSelection = null;
		eraseAttackInfo();
		eraseInformations();
	}
}

function hideImage() {
	if( img != null && ( tmp_x != x || tmp_y != y ) ) {
		img.className = "invisible";
		img = null;
		eraseAttackInfo();
	}
}

// =================== First CLICK ===================

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

function resetSelection( s ) {
	if( s != null ) {
		s.setClass("");
	}
	if(lastSelection != null) {
		lastSelection.setClass("");
		lastSelection = null;
	}
}

function oneSelected(selectedElement) {
	var id = lastSelection.getChildId().toLowerCase().split("_");
	var cost = Unit[id[0]].movementCost;

	if( canMoveOver(selectedElement.id,null) && movesObj.hasMoves( cost ) ) {
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
			
			var total = lastSelection.getQuantity() - quantity.value;
			if( total > 0 ) {
				cost *= 2;
				if( !movesObj.hasMoves( cost ) ) {
					return;
				}
			}
			
			if( !selectedElement.hasChildNodes() ) {
				selectedElement.removeAll();
			}
			
			selectedElement.insert(lastSelection,quantity.value);
			selectedElement.node.item = selectedElement;
			
			if( total > 0 ) {
				lastSelection.setQuantity( total );
			}else{
				lastSelection.removeAll();
				lastSelection.insertSpace();
				lastSelection.node.item = null;
			}
			
			registerMove(lastSelection.id,selectedElement.id,quantity.value);
			movesObj.decrementMoves( cost );
			
			resetSelection(selectedElement);
			eraseInformations();
		}else{
			RaiseQuantityError();
		}
	}
}

function canMoveOver(id,event) {
	fillInformations(id);
	hideImage();
	if( lastSelection != null ) {
		
		if( lastSelection.id == id ){
			return false;
		}
		mouseOverElement = new Item(id);
		
		var dest = id.split("_");
		
		if( isEnemyShip(mouseOverElement) && canAttack(dest) ) {
			currentEnemyId = id;
			showImage(mouseOverElement,"enemy",event);
			return false;
		}
		
		if( canMove(dest) ) {
			if(isEnemyShip(mouseOverElement)) {
				showImage(mouseOverElement,"cannotMove",event);
				return false;
			}
			
			if( mouseOverElement.hasChildNodes() && lastSelection.equal(mouseOverElement) ) {
				showImage(mouseOverElement,"cannotMove",event);
				return false;
			}
			
			mouseOverElement.setClass("canMove");
			return true;
		}
	}
	return false;
}

function canMoveOut() {
	if( mouseOverElement != null ) {		
		if(mouseOverElement.node.className != "enemyBorder" ) {
			mouseOverElement.setClass("");
		}
		mouseOverElement = null;
	}
}

function canMove(dst) {
	var src = lastSelection.id.split("_");
	
	var id = lastSelection.getChildId().toLowerCase().split("_");
		
	var movType = Unit[id[0]].movementType;
	if( movType == "") {
		return false;
	}
	return movesObj[movType](src,dst);
}

function canAttack( dst ) {
	if( lastSelection != null && !lastSelection.hasAttack )  {
		
		var src = lastSelection.id.split("_");
		
		var i = lastSelection.getImageName().split("_");
		
		var unit = Unit[i[0].toLowerCase()];
		
		var r = unit.range;
		var attack = new Attack( src, dst, r, unit );
			
		var can = attack[lastSelection.getPosition()]();
		if ( can ) {
			fillAttackInfo( src, dst );
		}
		return can;
	}
	return false;
}

var currentEnemyId = null;

var theform = document.pageContent;

//===================== Register ===============================

function registerMove( srcId, dstId, total ) {
	theform.movesMade.value += "move:"+srcId+"-"+dstId+"-"+total+";";
}

function registerBattle( srcId, dstId ) {
	theform.movesMade.value += "battle:"+srcId+"-"+dstId+";";
}

function registerRotation( id, posold,  pos ) {
	theform.movesMade.value += "rotation:"+id+"-"+posold+"-"+pos+";";
}

// ========================= Position ================================0

function setPosition( pos ) {
	if(lastSelection != null && lastSelection.getPosition() != pos && movesObj.hasMoves(1) ) {
		var oldPos = lastSelection.getPosition();
		lastSelection.setPosition( pos );
		registerRotation( lastSelection.id, oldPos, pos );
		var cost = Number( document.getElementById("movementCost").innerHTML );
		movesObj.decrementMoves( 1 );
		
		if( !movesObj.hasMoves(1) ) {
			resetSelection(null);
		}
	}
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
	params = m[1].split("-");
	
	undoObj[m[0]]( params );
}


/* =============================== UNDO ================================= */

var undoObj = new Undo();

function Undo() {
	this.move = function( params ) {
		var src = document.getElementById(params[0]);
		if(src.item == null ) {
			src.item = new Item(params[0]);
		}
		var dst = document.getElementById(params[1]);
		var quant = Number(params[2]);
		var dstQuant = Number(dst.item.getQuantity());
		
		var moveCost = 1;
		if( src.item.hasChildNodes() ) {
			src.item.setQuantity( Number(src.item.getQuantity()) + quant );
			moveCost = 2;
		} else {
			src.item.removeAll();
			src.item.insert(dst.item, quant );
		}
		
		var unitName = src.item.getCleanImageName();
		
		movesObj.incrementMoves( Number( Unit[unitName].movementCost )*moveCost );
		
		src.hasAttack = dst.hasAttack;
		if(lastSelection != null ) {
			lastSelection.setClass("");
			lastSelection = null;
		}
		
		if( dstQuant > quant ) {
			dst.item.setQuantity( dstQuant - quant );
		}else{
			if( dstQuant == quant || dstQuant < quant ) {
				dst.item.removeAll();
				dst.item.insertSpace();
				dst.item = null;
			}
		}	
	}
	this.rotation = function( params ) {
		var src = document.getElementById(params[0]);
		var oldpos = params[1];
		var newpos = params[2];
		
		src.item.setPosition( oldpos );
		movesObj.incrementMoves( 1 );
	}
	this.battle = function( params ) {
		var src = document.getElementById(params[0]).item;
		src.hasAttack = false;
		movesObj.incrementMoves( 1 );
	}
}

