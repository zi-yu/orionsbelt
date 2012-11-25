var Unit = new Object();

// ------------------------------------------------
// Utility Functions
// ------------------------------------------------

function AddBonusAttack(object, attack, target, terrain ) {
	
	
	attack += addUp( object.attackTargets, "unit", target.name );
	attack += addUp( object.attackTargets, "unit", target.category );
	attack += addUp( object.attackTargets, "terrain", terrain );
	attack += addUp( object.attackTargets, "level", target.level );
	
	return attack;
}

function AddBonusLive(object,live, target, terrain ) {
	
	live += addUp( object.defenseTargets, "unit", target.name );
	live += addUp( object.defenseTargets, "unit", target.category );
	live += addUp( object.defenseTargets, "terrain", terrain );
	live += addUp( object.defenseTargets, "level", target.level );
	
	return live;
}

function getMinAttack( target, terrain ) {
	return AddBonusAttack( this,this.minimumDamage,target, terrain);
}


function getMaxAttack( target, terrain ) {
	return AddBonusAttack( this,this.maximumDamage,target, terrain);
}


function getAttack( target, terrain ) 
{
	return AddBonusAttack( this,this.baseAttack, target, terrain);
}

function getLive( target, terrain )
{
	return AddBonusLive( this,this.hitPoints,target, terrain);
}

function addUp( hash, key, value )
{
	if( hash == null ) {
		return 0;
	}
	
	var specific = hash[key];
	if( specific == null ) {
		return 0;
	}
	
	var mod = specific[value];
	if( mod == null ) {
		return 0;
	}
	
	return mod;
}