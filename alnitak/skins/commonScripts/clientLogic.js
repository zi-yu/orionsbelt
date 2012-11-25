function Item(id) {
	this.id = id;
	this.node = document.getElementById(id);
	this.getQuantity = function() {
		var aux = document.getElementById( this.id + "_q" );
		return aux.childNodes[0].nodeValue;
	}
	this.isSpace = function() {
		if( this.node.innerHTML == "&nbsp;" )
			return true;
		return false;
	}
	this.insertSpace = function() {
		this.node.innerHTML = "&nbsp;";
	}
	this.getImage = function() {
		var img = this.node.firstChild;
		return img.src;
	}
	this.setClass = function( name ) {
		this.node.className = name;
	}
	this.setQuantity = function( value ) {
		var quant = document.getElementById( this.id + "_q" );
		quant.childNodes[0].nodeValue = value;
	}
	this.swapQuantity = function( element, value ) {
		var quant = document.createElement("div");
		quant.id = this.id + "_q";
	
		var innertext = document.createTextNode( value );
		quant.appendChild(innertext);

		this.node.appendChild(quant);
	}
	this.swapImage = function( element ) {
		//criar a imagem
		var imgElement = document.createElement("img");
		imgElement.src = element.getImage();
		imgElement.id = "img_" + element.id;
		
		//afectar os elementos pela ordem correcta
		this.node.appendChild(imgElement);
	}
	this.insert = function( element, value ){
		this.swapImage( element );
		this.swapQuantity( element, value );
	}
	this.insertImage = function(img) {
		this.node.innerHtml = "";
		//criar a imagem
		var imgElement = document.createElement("img");
		imgElement.src = img;
		imgElement.id = "img_vs_a";
		
		//afectar os elementos pela ordem correcta
		this.node.appendChild(imgElement);
	}
	this.removeAll = function() {
		while( this.node.hasChildNodes() )
			this.node.removeChild(this.node.firstChild);
	}
	this.hasChildNodes = function() {
		
		return this.node.hasChildNodes() && !this.isSpace();
	}
}

//estticas
var nom;

var lastSelection = null;
var mouseOverElement;

var theform = document.pageContent;

function selectedSector(id) {
	var selectedElement = new Item(id);
	
	//verificar se a imagem n  a da batalha
	if( isBattleImage( selectedElement ) )
		return;

	quantity = document.getElementById("quantity");
	
	if( document.pageContent.moves.value == 0)
		return;

	if( lastSelection == null )	 {
		noneSelected(selectedElement);
	} else {
		if( sameSector(selectedElement) ) {
			quantity.value = "";
			return;
		}
		oneSelected(selectedElement);
	}
}

function canMoveOut() {
	if( mouseOverElement != null ) {
		mouseOverElement.setClass("unselectedSector");
		mouseOverElement = null;
	}
}

//-----------------------------------------------------------------------------------
//------------------------verificar a seleco dos elementos-------------------------
function noneSelected(selectedElement) {
	if( !selectedElement.hasChildNodes() || isEnemyShip(selectedElement) )
		return;
	selectedElement.setClass("selectedSector");
	quantity.value = selectedElement.getQuantity();	
	lastSelection = selectedElement;
}

function sameSector( selectedElement ) {
	if( lastSelection.id == selectedElement.id ) {
		lastSelection.setClass("unselectedSector");
		lastSelection = null;
		return true;
	}
	return false;
}

function oneSelected(selectedElement) {
	if( canMoveOver(selectedElement.id) ) {
		if( isEnemyShip(selectedElement) ) {
			if( !moveEvent(selectedElement,true) )
				return;
		} else {
			if( !moveEvent(selectedElement,false) )
				return;
		}
		
		lastSelection = lastSelectionId = lastSelectionImg = null;
		
	
		//diminuir o nmero de movimentos	
		nom = document.getElementById("nom");
		nom.childNodes[0].nodeValue = String(--document.pageContent.moves.value);	
	}
}
//-----------------------------------------------------------------------------------
//-------------------------saber que tipo de movimento -----------------------------
function moveEvent(selectedElement,isBattle) {
	var lastSelectionQuantity = lastSelection.getQuantity();
	
	if( !validQuantity( Number( lastSelectionQuantity ) ) ) {
		showError("invalidQuantity");
		return false;
	}
		
	if( isBattle ) {
		selectedElement.removeAll();
		//var vsImg = theform.battleImage;
		selectedElement.insertImage( theform.battleImage.value );
		theform.movesMade.value += "battle:";
	}else{
		//mudar a nave
		selectedElement.removeAll();
		selectedElement.insert( lastSelection, quantity.value );
	}
	
	//gravar o movimento
	theform.movesMade.value += lastSelection.id + ":" + selectedElement.id + ":" + quantity.value + ";";
	
	lastSelection.setClass("unselectedSector");
	selectedElement.setClass("unselectedSector");
	
	//alterar a quantidade da nave de origem ( remover tudo ou diminuir a quantidade)
	if( quantity.value == lastSelectionQuantity ) {
		lastSelection.removeAll();
		lastSelection.insertSpace();
	} else {
		lastSelection.setQuantity( Number(lastSelectionQuantity) - Number(quantity.value) );
	}
	
	//limpar a textbox
	quantity.value = "";
	
	//apagar eventuais erros
	hideError();
	
	return true;
}

//-----------------------------------------------------------------------------------
//----------------------------validar a quantidade-----------------------------------
function validQuantity(q) {
	if( quantity.value == "" || !isPositiveInt( q ) )
		return false;	
	
	if( Number(quantity.value) <= q && Number(quantity.value) > 0)
		return true;
	
	return false;
}

//-----------------------------------------------------------------------------------
//------------------------verificar se o movimento  vlido--------------------------
function canMoveOver(id) {

	if( lastSelection != null ) {
	
		if( lastSelection.id == id )
			return false;
				
		var selectedElement = new Item(id);
		mouseOverElement = selectedElement;
		var source = lastSelection.id.split("_");
		var dest = id.split("_");
		
		if( canMove( source, dest, selectedElement ) ) {
			if( !selectedElement.hasChildNodes() ) {
				return true;
			}else{
				if( isEnemyShip(selectedElement) )
					return true;
				if( !( selectedElement.id.split("_")[0] == "src" ) )
					selectedElement.setClass("cantMove");
			}
		}
	}
	return false;
}

function canMove(source,dest,selectedElement){
	if( canMoveFromSource( source, dest, selectedElement ) )
		return true;	
	
	if( canMoveUpDown( source, dest, selectedElement  ) )
		return true;
		
	if( canMoveLeftDown( source, dest, selectedElement ) )
		return true;
		
	return false;
}

function canMoveFromSource( source, dest, selectedElement ) {
	//se pode mover da origem
	//var size = ;
	if( source[0] == "src") {
		if( dest[0] == theform.rowSize.value ) {
			selectedElement.setClass("canMove");
			return true;
		}
	}
	return false;
}

function canMoveUpDown( source, dest, selectedElement  ) {
	//pode mover para os lados
	if( source[0] == dest[0] ) {
		if( Number(dest[1]) + 1 == Number( source[1] ) || Number( dest[1] ) - 1 == Number( source[1] ) ) {
			selectedElement.setClass("canMove");
			return true;
		}
	}
	return false;
}

function canMoveLeftDown( source, dest, selectedElement ) {
	//pode mover para cima e para baixo
	if( source[1] == dest[1] ) {
		if( dest[0].charCodeAt(0) + 1 == source[0].charCodeAt(0) || dest[0].charCodeAt(0) - 1 == source[0].charCodeAt(0) ) {
			selectedElement.setClass("canMove");
			return true;
		}
	}
	return false;
}
//-----------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------
function isEnemyShip( element ) {

	if( element.hasChildNodes()) {
		var imgArray = element.getImage().split("/");
		var img = imgArray[imgArray.length-1].split(".")[0];
		imgArray = img.split("_");
		if( imgArray.length == 2 )
			return true;	
	}
	return false;
}

function isBattleImage( selectedElement ) {
	if( selectedElement.hasChildNodes() ) {
		var img = selectedElement.getImage();
		var imgArray = img.split("/");
		if ( imgArray[imgArray.length-1] == "vs.gif" )
			return true;
	}
	return false;
}
//-----------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------

function showError(id) {
	var error = document.getElementById( clientId + "_" + id );
	error.className = "errorVisible";
}

function hideError() {
	var error = document.getElementById( clientId + "_invalidQuantity");
	error.className = "invisible";
}

//-----------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------

function hasPlayed() {
	if( theform.movesMade.value == "" && theform.hasShipsToMove.value == "1" ) {
		return false
	}
	return true;
}