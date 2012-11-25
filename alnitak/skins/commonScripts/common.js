var theform = document.pageContent;

function Browser() {

	var agent = navigator.userAgent.toLowerCase();
	
	this.mozilla  = ((agent.indexOf('mozilla')!=-1) && ((agent.indexOf('spoofer')==-1) && (agent.indexOf('compatible') == -1)));
	this.ie   = (agent.indexOf("msie") != -1);
		
	/*this.major = parseInt(navigator.appVersion);
	this.minor = parseFloat(navigator.appVersion);

	this.win   = (agent.indexOf("win")!=-1);
	this.mac   = (agent.indexOf("mac")!=-1);
	
	this.ie5 = (agent.indexOf("msie 5")!=-1);
	this.ie6 = (agent.indexOf("msie 6")!=-1);
	this.ns4 = (this.ns && (this.major <= 4));
	this.ns6 = (agent.indexOf("netscape6")!=-1);
	this.ns7 = (agent.indexOf("netscape/7")!=-1);
	
	this.safari = (agent.indexOf("safari")!=-1);
	
	this.opera = (agent.indexOf("opera")!=-1);
	this.opera7 = (agent.indexOf("opera 7")!=-1);*/
}

var browser = new Browser();

function init() {
	this.document.getElementById("prepage").style.display = "none";
	return true;
}

function isPositiveInt( number )
{
	if( number == null ) {
		return false;
	}
	
	var regex = /^\d+$/;

	return regex.test(number);
}

function overResource( line )
{
	line.oldClassName = line.className;
	line.className = "overResource_" + line.oldClassName;
	
	var lines = line.childNodes;

	for( var i = 0; i < lines.length; ++i ) {
		lines[i].oldClassName = lines[i].className;
		lines[i].className = "overResource_" + lines[i].oldClassName;
	}
}

function outResource( line )
{
	line.className = line.oldClassName;
	
	var lines = line.childNodes;

	for( var i = 0; i < lines.length; ++i ) {
		lines[i].className = lines[i].oldClassName;
	}
}

function borderOn( obj ){
	obj.className = "selectedCell";
}

function borderOff( obj ){
	obj.className = "deselectedCell";
}

/*************** TRAVEL ************************/

var oldCtrl = null;

function borderOnOff( ctrl ) {
	if( ctrl.className == "selectedCell" ) {
		ctrl.className = "deselectedCell";
	} else {
		ctrl.className = "selectedCell";
		if( oldCtrl != null && oldCtrl != ctrl ) {
			oldCtrl.className = "deselectedCell";
		}
	}
	oldCtrl = ctrl;
}

// --Tabs--

function tabClick( ctrl, cont ) {
	if( ctrl.className == "unselectedTab" ) {
		
		//tratar de trocar as tabs
		var oldTab = document.getElementById( theform.oldTabCtrl.value );
		oldTab.className = "unselectedTab";
				
		ctrl.className = "selectedTab";
		theform.oldTabCtrl.value = ctrl.id;
							
		//tratar de trocar os conteudos
		var oldContent = document.getElementById( theform.oldTabContent.value );
		oldContent.className = "unselectedTabContents";
			
		var content = document.getElementById( cont );
		content.className = "selectedTabContents";
		
		theform.oldTabContent.value = cont;
		
		//garantir que, se o utilizador clicar na tab simples,
		//ele limpar qq click que tenha sido feito na tab advanced
		if( theform.oldTabCtrl.value == "tab1" && theform.planetClicked != null) {
			theform.planetClicked.value = "";
		}
	}
}


/*************** MENU ************************/

var timeOn;

function showHideFilter( parent, eventObj )
{
	var filter = document.getElementById("filterTable");
	
	if( filter.className == "filteron" ) {
		timeOn = setTimeout("hideFilter()", 200);
	} else {
		var x = getAbsX(parent) - filter.offsetWidth + parent.offsetWidth + 2;
		var y = getAbsY(parent) - 1;
		
		filter.style.left = x  + "px";
		filter.style.top = y + "px";
		
		filter.className = "filteron";
	}
	eventObj.cancelBubble = true;
}

function hideFilter()
{
	var filter = document.getElementById("filterTable");
	filter.className = "filteroff";
}

function overFilter()
{
	if( timeOn ) {
		clearTimeout(timeOn);
	}
}

function getAbsX(elt) 
{
	return parseInt(elt.x) ? elt.x : getAbsPos(elt,"Left"); 
}

function getAbsY(elt) 
{
	return parseInt(elt.y) ? elt.y : getAbsPos(elt,"Top"); 
}

function getAbsPos(elt,which) 
{
	iPos = 0;
	while (elt != null) {
		iPos += elt["offset" + which];
		elt = elt.offsetParent;
	}
	return iPos;
}

/**** WIKI ****/

var timeout = null;
var tipOffTimeout = null;

function TopicTipOn(anchor, id, event)
{
	var TopicTip = document.getElementById("TopicTip");
	var targetY = document.body.scrollTop + event.clientY + 18;
	var targetX = document.body.scrollLeft + event.clientX + 12;
	
	TopicTip.style.left = targetX + "px";
	TopicTip.style.top = targetY + "px";
	
	var tip = 	document.getElementById(id);
	TopicTip.innerHTML = tip.innerHTML;
	TopicTip.style.display = 'block';
	if (tipOffTimeout != null)
		window.clearTimeout(tipOffTimeout);
	tipOffTimeout = window.setTimeout("TopicTipOff()", 4000, "JavaScript");
}

function TopicTipOff()
{
	var TopicTip = document.getElementById("TopicTip");
	if (tipOffTimeout != null)
		window.clearTimeout(tipOffTimeout);
	tipOffTimeout = null;				
	TopicTip.style.display = 'none';
}

//===================================================================================
//=============================== Battle Stuff ======================================
//===================================================================================

function isEnemyShip( element ) {
	if( element.hasChildNodes()) {
		var img = element.node.firstChild.id;
		if(img.indexOf("enemy") != -1 ) {
			return true;
		}
	}
	return false;
}
/* ============================= ITEM ================================ */

function Item(id) {
	this.id = id;
	this.node = document.getElementById(id);
	this.hasAttack = false;
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
	this.getQuantity = function() {
		return this.node.firstChild.title;
	}
	this.getChildId = function() {
		return this.node.firstChild.id;
	}
	this.setQuantity = function(value) {
		this.node.firstChild.title = value;
	}
	this.insert = function(element,quantity) {
		if( !this.hasChildNodes() )	{
			var imgElement = document.createElement("img");
			imgElement.src = element.getImage();
			imgElement.id = element.getChildId();
			imgElement.title = quantity;
			this.node.appendChild(imgElement);
		}else{
			this.setQuantity( Number(this.getQuantity()) + Number(quantity) );
		}
	}
	this.setClass = function( name ) {
		this.node.className = name;
	}
	this.removeAll = function() {
		this.node.innerHTML = "";
	}
	this.hasChildNodes = function() {
		return this.node.hasChildNodes() && !this.isSpace();
	}
	this.getImageName = function() {
		var img = this.getImage();
		var imgArray = img.split("/");
		var name = imgArray[imgArray.length-1].split(".");
		return name[0];
	}
	this.getCleanImageName = function() {
		var name = this.getChildId();
		var pos = name.split("_");
		return pos[0].toLowerCase();
	}
	this.getPosition = function() {
		var name = this.getImageName();
		var pos = name.split("_");
		return pos[1];
	}
	this.setPosition = function( p ) {
		var pos = this.getCleanImageName();
		
		var image = theform.imagePath.value;
		
		image+= pos + "_" + p + ".gif"
		this.node.firstChild.src = image;
	}
	this.equal = function( element ) {
		return element.getCleanImageName() != this.getCleanImageName();
	}
}

var img = null;
var x,y,tmp_x,tmp_y;

function setTmpXY(id) {
	var td = document.getElementById(id);
	tmp_x = getAbsX(td);
	tmp_y = getAbsY(td);
}

function showImage(mouseOverElement,image,event) {
	if( img == null ) {
		var td = document.getElementById(mouseOverElement.id);
		
		img = document.getElementById(image);
		
		x = getAbsX(td);
		y = getAbsY(td);
		img.style.left = x + 2 + "px";
		img.style.top = y + 2 + "px";
		img.className = "visible";
	}
	event.cancelBubble = true;
}

function undo(){}
function setPosition(e){}

function resetMoves() {
	if( theform.movesMade.value == "" ) {
		return;
	}
	
	var m = theform.movesMade.value.split(";");
	theform.movesMade.value = "";
	for( var i = m.length-2; i >= 0; --i ) {
		parseMove( m[i] );
	}
}

//===================================================================================
//============================ ScreenShot Viewer ====================================
//===================================================================================

function setPicture(currValue) {
	var picture = document.getElementById("currentPicture");
	picture.src = document.pageContent.imagesPath.value + currValue + ".png";
}

function prevPicture() {
	var curr = document.getElementById("currentPictureNumber");
	var currValue = Number(curr.innerHTML);
	if(currValue == 1) {
		currValue = Number(document.getElementById("maxPicture").innerHTML);
	}else{
		--currValue;
	}
	
	curr.innerHTML = currValue;
	setPicture(currValue);
}
function nextPicture() {
	var curr = document.getElementById("currentPictureNumber");
	var currValue = Number(curr.innerHTML);
	var maxValue = Number(document.getElementById("maxPicture").innerHTML);
	if(currValue == maxValue) {
		currValue = 1;
	}else{
		++currValue;
	}
	curr.innerHTML = currValue;
	setPicture(currValue);
}

//===================== Information ===============================

function getItem( id ) {
	var element = document.getElementById(id);
	if(element.item == null) {
		var e = new Item(id);
		element.item = e;
	}
	return element.item;
}

function fillInformations( parentID ) {
	var image = document.getElementById( parentID ).firstChild;
	if( image != null && image.id != null ) {
	
		var id = image.id.split("_")[0].toLowerCase();
		
		fillInfo( "shipType",Unit[id].name );
		fillInfo( "baseAttack",Unit[id].baseAttack );
		fillInfo( "baseDefense",Unit[id].baseDefense );
		fillInfo( "baseLive",Unit[id].hitPoints );
		fillInfo( "movementCost",Unit[id].movementCost );
		fillInfo( "movementType",Unit[id].movementType );
		fillInfo( "range",Unit[id].range );
		fillInfo( "unitQuant",image.title );
		
		var item = getItem( parentID );
		
		var i = item.hasAttack?"no.gif":"yes.gif";
		fillInfo( "hasAttack", "<img src='" + document.pageContent.imagePath.value + i + "' />" );
		
		i = Unit[id].canStrikeBack?"yes.gif":"no.gif";
		fillInfo( "hasStrikeBack", "<img src='" + document.pageContent.imagePath.value + i + "' />" );
		
		i = Unit[id].catapultAttack?"yes.gif":"no.gif";
		fillInfo( "hasCatapultAttack", "<img src='" + document.pageContent.imagePath.value + i + "' />" );
		
		i = Unit[id].canDamageBehindUnits?"yes.gif":"no.gif";
		fillInfo( "hasDamageBehindUnits", "<img src='" + document.pageContent.imagePath.value + i + "' />" );
		
		i = Unit[id].tripleAttack?"yes.gif":"no.gif";
		fillInfo( "hasTripleAttack", "<img src='" + document.pageContent.imagePath.value + i + "' />" );
		
		i = Unit[id].replicatorAttack?"yes.gif":"no.gif";
		fillInfo( "hasReplicator", "<img src='" + document.pageContent.imagePath.value + i + "' />" );
	}
}

function fillInfo( id, value ) {
	var elem = document.getElementById(id);
	if( elem.hasChildNodes()) {
		elem.removeChild(elem.firstChild);
	}
	elem.innerHTML = value;
}

function calculatePenalty( damage, src, dst) {
	distance = penalty(src,dst);
	if( distance < 4 ) {
		return damage;
	}
	var percent = (7 - distance)*0.25;
		
	return Math.round( (percent * damage) );
}

function penalty( src, dst) {

	var s_x = Number(src[0]);
	var s_y = Number(src[1]);
	var d_x = Number(dst[0]);
	var d_y = Number(dst[1]);
	
	if( s_y == d_y ) {
		return Math.abs(s_x-d_x);
	}
	return Math.abs(s_y-d_y);
}

function fillAttackInfo( parentID, targetID ) {
	
	var image = document.getElementById( parentID[0] + "_" + parentID[1] ).firstChild;
		
	var id = image.id.split("_")[0].toLowerCase();
	var terrain = document.getElementById("terrain").innerHTML;
	var quant = Number(lastSelection.getQuantity());
	
	var target = getItem(targetID[0] + "_" + targetID[1]);
	var name = target.getCleanImageName();
	var tquant = target.getQuantity();
	
	var min = quant*Unit[id].getMinAttack(Unit[name],terrain);
	var max = quant*Unit[id].getMaxAttack(Unit[name],terrain);
	
	fillInfo( "attackAgainst", min );
	fillInfo( "defenseAgainst", max );
	
	fillInfo( "targetDefense", tquant*Unit[name].baseDefense );
	fillInfo( "targetLive", tquant*Unit[name].getLive(Unit[id],terrain) );
	
	var minD = min/Unit[name].getLive(Unit[id],terrain) - 0.5;
	var maxD = max/Unit[name].getLive(Unit[id],terrain) - 0.5;
	var d = calculatePenalty(Math.round(minD),parentID,targetID) + "-" +  calculatePenalty(Math.round(maxD),parentID,targetID);
	fillInfo( "damage", d );
}

function eraseInformations() {
	fillInfo( "shipType","");
	fillInfo( "baseAttack","");
	fillInfo( "baseDefense","");
	fillInfo( "movementCost","");
	fillInfo( "movementType","");
	fillInfo( "range","");
	fillInfo( "unitQuant","");
	fillInfo( "hasAttack","");
	fillInfo( "hasStrikeBack","");
}

function eraseAttackInfo() {
	fillInfo( "attackAgainst", "" );
	fillInfo( "defenseAgainst", "" );
	fillInfo( "damage", "" );
	fillInfo( "targetDefense", "" );
	fillInfo( "targetLive", "" );
}