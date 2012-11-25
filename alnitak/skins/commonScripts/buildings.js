
// ------------------------------------------------------
// 'Turret' Definition
// ------------------------------------------------------
Unit["turret"] = new Object();
Unit["turret"].getAttack = getAttack;
Unit["turret"].getMinAttack = getMinAttack;
Unit["turret"].getMaxAttack = getMaxAttack;
Unit["turret"].getLive = getLive;
Unit["turret"].name = "Turret";
Unit["turret"].category = "building";
Unit["turret"].baseAttack = 6500000;
Unit["turret"].baseDefense = 7000000;
Unit["turret"].maximumDamage = 7500000;
Unit["turret"].minimumDamage = 6000000;
Unit["turret"].hitPoints = 8500000;
Unit["turret"].canStrikeBack = true;
Unit["turret"].catapultAttack = true;
Unit["turret"].tripleAttack = false;
Unit["turret"].replicatorAttack = false;
Unit["turret"].canDamageBehindUnits = true;
Unit["turret"].range = 2;
Unit["turret"].movementCost = 0;
Unit["turret"].movementType = "none";
Unit["turret"].level = "ground";
		
// 'Turret' defense targets
Unit["turret"].defenseTargets = new Object();
Unit["turret"].defenseTargets["terrain"] = new Object();
Unit["turret"].defenseTargets["unit"] = new Object();
Unit["turret"].defenseTargets["level"] = new Object();

// 'Turret' attack targets
Unit["turret"].attackTargets = new Object();
Unit["turret"].attackTargets["terrain"] = new Object();
Unit["turret"].attackTargets["unit"] = new Object();
Unit["turret"].attackTargets["level"] = new Object();

// ------------------------------------------------------
// 'IonCannon' Definition
// ------------------------------------------------------
Unit["ioncannon"] = new Object();
Unit["ioncannon"].getAttack = getAttack;
Unit["ioncannon"].getMinAttack = getMinAttack;
Unit["ioncannon"].getMaxAttack = getMaxAttack;
Unit["ioncannon"].getLive = getLive;
Unit["ioncannon"].name = "IonCannon";
Unit["ioncannon"].category = "building";
Unit["ioncannon"].baseAttack = 8000000;
Unit["ioncannon"].baseDefense = 10000000;
Unit["ioncannon"].maximumDamage = 9000000;
Unit["ioncannon"].minimumDamage = 7000000;
Unit["ioncannon"].hitPoints = 12000000;
Unit["ioncannon"].canStrikeBack = true;
Unit["ioncannon"].catapultAttack = true;
Unit["ioncannon"].tripleAttack = true;
Unit["ioncannon"].replicatorAttack = false;
Unit["ioncannon"].canDamageBehindUnits = false;
Unit["ioncannon"].range = 3;
Unit["ioncannon"].movementCost = 0;
Unit["ioncannon"].movementType = "none";
Unit["ioncannon"].level = "ground";
		
// 'IonCannon' defense targets
Unit["ioncannon"].defenseTargets = new Object();
Unit["ioncannon"].defenseTargets["terrain"] = new Object();
Unit["ioncannon"].defenseTargets["unit"] = new Object();
Unit["ioncannon"].defenseTargets["level"] = new Object();

// 'IonCannon' attack targets
Unit["ioncannon"].attackTargets = new Object();
Unit["ioncannon"].attackTargets["terrain"] = new Object();
Unit["ioncannon"].attackTargets["unit"] = new Object();
Unit["ioncannon"].attackTargets["level"] = new Object();
