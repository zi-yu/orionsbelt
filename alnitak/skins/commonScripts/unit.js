
// ------------------------------------------------------
// 'ColonyShip' Definition
// ------------------------------------------------------
Unit["colonyship"] = new Object();
Unit["colonyship"].getAttack = getAttack;
Unit["colonyship"].getMinAttack = getMinAttack;
Unit["colonyship"].getMaxAttack = getMaxAttack;
Unit["colonyship"].getLive = getLive;
Unit["colonyship"].name = "ColonyShip";
Unit["colonyship"].category = "special";
Unit["colonyship"].baseAttack = 230;
Unit["colonyship"].baseDefense = 8000;
Unit["colonyship"].maximumDamage = 250;
Unit["colonyship"].minimumDamage = 200;
Unit["colonyship"].hitPoints = 9200;
Unit["colonyship"].canStrikeBack = true;
Unit["colonyship"].catapultAttack = false;
Unit["colonyship"].tripleAttack = false;
Unit["colonyship"].replicatorAttack = false;
Unit["colonyship"].canDamageBehindUnits = false;
Unit["colonyship"].range = 1;
Unit["colonyship"].movementCost = 6;
Unit["colonyship"].movementType = "all";
Unit["colonyship"].level = "air";
		
// 'ColonyShip' defense targets
Unit["colonyship"].defenseTargets = new Object();
Unit["colonyship"].defenseTargets["terrain"] = new Object();
Unit["colonyship"].defenseTargets["unit"] = new Object();
Unit["colonyship"].defenseTargets["level"] = new Object();

// 'ColonyShip' attack targets
Unit["colonyship"].attackTargets = new Object();
Unit["colonyship"].attackTargets["terrain"] = new Object();
Unit["colonyship"].attackTargets["unit"] = new Object();
Unit["colonyship"].attackTargets["level"] = new Object();

// ------------------------------------------------------
// 'FlagShip' Definition
// ------------------------------------------------------
Unit["flagship"] = new Object();
Unit["flagship"].getAttack = getAttack;
Unit["flagship"].getMinAttack = getMinAttack;
Unit["flagship"].getMaxAttack = getMaxAttack;
Unit["flagship"].getLive = getLive;
Unit["flagship"].name = "FlagShip";
Unit["flagship"].category = "special";
Unit["flagship"].baseAttack = 1;
Unit["flagship"].baseDefense = 8000;
Unit["flagship"].maximumDamage = 2;
Unit["flagship"].minimumDamage = 1;
Unit["flagship"].hitPoints = 10000;
Unit["flagship"].canStrikeBack = false;
Unit["flagship"].catapultAttack = false;
Unit["flagship"].tripleAttack = false;
Unit["flagship"].replicatorAttack = false;
Unit["flagship"].canDamageBehindUnits = false;
Unit["flagship"].range = 1;
Unit["flagship"].movementCost = 1;
Unit["flagship"].movementType = "all";
Unit["flagship"].level = "air";
		
// 'FlagShip' defense targets
Unit["flagship"].defenseTargets = new Object();
Unit["flagship"].defenseTargets["terrain"] = new Object();
Unit["flagship"].defenseTargets["unit"] = new Object();
Unit["flagship"].defenseTargets["level"] = new Object();

// 'FlagShip' attack targets
Unit["flagship"].attackTargets = new Object();
Unit["flagship"].attackTargets["terrain"] = new Object();
Unit["flagship"].attackTargets["unit"] = new Object();
Unit["flagship"].attackTargets["level"] = new Object();

// ------------------------------------------------------
// 'Rain' Definition
// ------------------------------------------------------
Unit["rain"] = new Object();
Unit["rain"].getAttack = getAttack;
Unit["rain"].getMinAttack = getMinAttack;
Unit["rain"].getMaxAttack = getMaxAttack;
Unit["rain"].getLive = getLive;
Unit["rain"].name = "Rain";
Unit["rain"].category = "light";
Unit["rain"].baseAttack = 50;
Unit["rain"].baseDefense = 70;
Unit["rain"].maximumDamage = 80;
Unit["rain"].minimumDamage = 40;
Unit["rain"].hitPoints = 60;
Unit["rain"].canStrikeBack = false;
Unit["rain"].catapultAttack = false;
Unit["rain"].tripleAttack = false;
Unit["rain"].replicatorAttack = false;
Unit["rain"].canDamageBehindUnits = false;
Unit["rain"].range = 1;
Unit["rain"].movementCost = 1;
Unit["rain"].movementType = "all";
Unit["rain"].level = "air";
		
// 'Rain' defense targets
Unit["rain"].defenseTargets = new Object();
Unit["rain"].defenseTargets["terrain"] = new Object();
Unit["rain"].defenseTargets["unit"] = new Object();
Unit["rain"].defenseTargets["level"] = new Object();

// 'Rain' attack targets
Unit["rain"].attackTargets = new Object();
Unit["rain"].attackTargets["terrain"] = new Object();
Unit["rain"].attackTargets["unit"] = new Object();
Unit["rain"].attackTargets["unit"]["heavy"] = 1200;
	Unit["rain"].attackTargets["level"] = new Object();

// ------------------------------------------------------
// 'Toxic' Definition
// ------------------------------------------------------
Unit["toxic"] = new Object();
Unit["toxic"].getAttack = getAttack;
Unit["toxic"].getMinAttack = getMinAttack;
Unit["toxic"].getMaxAttack = getMaxAttack;
Unit["toxic"].getLive = getLive;
Unit["toxic"].name = "Toxic";
Unit["toxic"].category = "light";
Unit["toxic"].baseAttack = 450;
Unit["toxic"].baseDefense = 600;
Unit["toxic"].maximumDamage = 590;
Unit["toxic"].minimumDamage = 350;
Unit["toxic"].hitPoints = 700;
Unit["toxic"].canStrikeBack = false;
Unit["toxic"].catapultAttack = false;
Unit["toxic"].tripleAttack = false;
Unit["toxic"].replicatorAttack = false;
Unit["toxic"].canDamageBehindUnits = false;
Unit["toxic"].range = 2;
Unit["toxic"].movementCost = 1;
Unit["toxic"].movementType = "normal";
Unit["toxic"].level = "air";
		
// 'Toxic' defense targets
Unit["toxic"].defenseTargets = new Object();
Unit["toxic"].defenseTargets["terrain"] = new Object();
Unit["toxic"].defenseTargets["unit"] = new Object();
Unit["toxic"].defenseTargets["level"] = new Object();

// 'Toxic' attack targets
Unit["toxic"].attackTargets = new Object();
Unit["toxic"].attackTargets["terrain"] = new Object();
Unit["toxic"].attackTargets["unit"] = new Object();
Unit["toxic"].attackTargets["unit"]["animal"] = 2000;
	Unit["toxic"].attackTargets["level"] = new Object();

// ------------------------------------------------------
// 'Raptor' Definition
// ------------------------------------------------------
Unit["raptor"] = new Object();
Unit["raptor"].getAttack = getAttack;
Unit["raptor"].getMinAttack = getMinAttack;
Unit["raptor"].getMaxAttack = getMaxAttack;
Unit["raptor"].getLive = getLive;
Unit["raptor"].name = "Raptor";
Unit["raptor"].category = "light";
Unit["raptor"].baseAttack = 280;
Unit["raptor"].baseDefense = 400;
Unit["raptor"].maximumDamage = 500;
Unit["raptor"].minimumDamage = 200;
Unit["raptor"].hitPoints = 500;
Unit["raptor"].canStrikeBack = false;
Unit["raptor"].catapultAttack = false;
Unit["raptor"].tripleAttack = false;
Unit["raptor"].replicatorAttack = false;
Unit["raptor"].canDamageBehindUnits = false;
Unit["raptor"].range = 2;
Unit["raptor"].movementCost = 1;
Unit["raptor"].movementType = "all";
Unit["raptor"].level = "air";
		
// 'Raptor' defense targets
Unit["raptor"].defenseTargets = new Object();
Unit["raptor"].defenseTargets["terrain"] = new Object();
Unit["raptor"].defenseTargets["terrain"]["ice"] = 200;
	Unit["raptor"].defenseTargets["unit"] = new Object();
Unit["raptor"].defenseTargets["level"] = new Object();

// 'Raptor' attack targets
Unit["raptor"].attackTargets = new Object();
Unit["raptor"].attackTargets["terrain"] = new Object();
Unit["raptor"].attackTargets["terrain"]["ice"] = 200;
	Unit["raptor"].attackTargets["unit"] = new Object();
Unit["raptor"].attackTargets["unit"]["light"] = 1000;
	Unit["raptor"].attackTargets["level"] = new Object();

// ------------------------------------------------------
// 'Anubis' Definition
// ------------------------------------------------------
Unit["anubis"] = new Object();
Unit["anubis"].getAttack = getAttack;
Unit["anubis"].getMinAttack = getMinAttack;
Unit["anubis"].getMaxAttack = getMaxAttack;
Unit["anubis"].getLive = getLive;
Unit["anubis"].name = "Anubis";
Unit["anubis"].category = "light";
Unit["anubis"].baseAttack = 200;
Unit["anubis"].baseDefense = 750;
Unit["anubis"].maximumDamage = 460;
Unit["anubis"].minimumDamage = 230;
Unit["anubis"].hitPoints = 500;
Unit["anubis"].canStrikeBack = false;
Unit["anubis"].catapultAttack = false;
Unit["anubis"].tripleAttack = false;
Unit["anubis"].replicatorAttack = false;
Unit["anubis"].canDamageBehindUnits = false;
Unit["anubis"].range = 1;
Unit["anubis"].movementCost = 1;
Unit["anubis"].movementType = "all";
Unit["anubis"].level = "air";
		
// 'Anubis' defense targets
Unit["anubis"].defenseTargets = new Object();
Unit["anubis"].defenseTargets["terrain"] = new Object();
Unit["anubis"].defenseTargets["terrain"]["rock"] = 100;
	Unit["anubis"].defenseTargets["unit"] = new Object();
Unit["anubis"].defenseTargets["unit"]["heavy"] = 3000;
	Unit["anubis"].defenseTargets["level"] = new Object();

// 'Anubis' attack targets
Unit["anubis"].attackTargets = new Object();
Unit["anubis"].attackTargets["terrain"] = new Object();
Unit["anubis"].attackTargets["terrain"]["rock"] = 100;
	Unit["anubis"].attackTargets["unit"] = new Object();
Unit["anubis"].attackTargets["level"] = new Object();

// ------------------------------------------------------
// 'Samurai' Definition
// ------------------------------------------------------
Unit["samurai"] = new Object();
Unit["samurai"].getAttack = getAttack;
Unit["samurai"].getMinAttack = getMinAttack;
Unit["samurai"].getMaxAttack = getMaxAttack;
Unit["samurai"].getLive = getLive;
Unit["samurai"].name = "Samurai";
Unit["samurai"].category = "light";
Unit["samurai"].baseAttack = 40;
Unit["samurai"].baseDefense = 200;
Unit["samurai"].maximumDamage = 70;
Unit["samurai"].minimumDamage = 30;
Unit["samurai"].hitPoints = 450;
Unit["samurai"].canStrikeBack = false;
Unit["samurai"].catapultAttack = false;
Unit["samurai"].tripleAttack = false;
Unit["samurai"].replicatorAttack = true;
Unit["samurai"].canDamageBehindUnits = false;
Unit["samurai"].range = 1;
Unit["samurai"].movementCost = 1;
Unit["samurai"].movementType = "all";
Unit["samurai"].level = "air";
		
// 'Samurai' defense targets
Unit["samurai"].defenseTargets = new Object();
Unit["samurai"].defenseTargets["terrain"] = new Object();
Unit["samurai"].defenseTargets["unit"] = new Object();
Unit["samurai"].defenseTargets["level"] = new Object();

// 'Samurai' attack targets
Unit["samurai"].attackTargets = new Object();
Unit["samurai"].attackTargets["terrain"] = new Object();
Unit["samurai"].attackTargets["unit"] = new Object();
Unit["samurai"].attackTargets["level"] = new Object();

// ------------------------------------------------------
// 'Panther' Definition
// ------------------------------------------------------
Unit["panther"] = new Object();
Unit["panther"].getAttack = getAttack;
Unit["panther"].getMinAttack = getMinAttack;
Unit["panther"].getMaxAttack = getMaxAttack;
Unit["panther"].getLive = getLive;
Unit["panther"].name = "Panther";
Unit["panther"].category = "light";
Unit["panther"].baseAttack = 300;
Unit["panther"].baseDefense = 300;
Unit["panther"].maximumDamage = 500;
Unit["panther"].minimumDamage = 250;
Unit["panther"].hitPoints = 450;
Unit["panther"].canStrikeBack = false;
Unit["panther"].catapultAttack = false;
Unit["panther"].tripleAttack = false;
Unit["panther"].replicatorAttack = false;
Unit["panther"].canDamageBehindUnits = true;
Unit["panther"].range = 1;
Unit["panther"].movementCost = 1;
Unit["panther"].movementType = "all";
Unit["panther"].level = "ground";
		
// 'Panther' defense targets
Unit["panther"].defenseTargets = new Object();
Unit["panther"].defenseTargets["terrain"] = new Object();
Unit["panther"].defenseTargets["unit"] = new Object();
Unit["panther"].defenseTargets["level"] = new Object();

// 'Panther' attack targets
Unit["panther"].attackTargets = new Object();
Unit["panther"].attackTargets["terrain"] = new Object();
Unit["panther"].attackTargets["unit"] = new Object();
Unit["panther"].attackTargets["level"] = new Object();

// ------------------------------------------------------
// 'Krill' Definition
// ------------------------------------------------------
Unit["krill"] = new Object();
Unit["krill"].getAttack = getAttack;
Unit["krill"].getMinAttack = getMinAttack;
Unit["krill"].getMaxAttack = getMaxAttack;
Unit["krill"].getLive = getLive;
Unit["krill"].name = "Krill";
Unit["krill"].category = "medium";
Unit["krill"].baseAttack = 1500;
Unit["krill"].baseDefense = 1000;
Unit["krill"].maximumDamage = 1300;
Unit["krill"].minimumDamage = 1200;
Unit["krill"].hitPoints = 1100;
Unit["krill"].canStrikeBack = true;
Unit["krill"].catapultAttack = false;
Unit["krill"].tripleAttack = false;
Unit["krill"].replicatorAttack = false;
Unit["krill"].canDamageBehindUnits = false;
Unit["krill"].range = 3;
Unit["krill"].movementCost = 2;
Unit["krill"].movementType = "all";
Unit["krill"].level = "air";
		
// 'Krill' defense targets
Unit["krill"].defenseTargets = new Object();
Unit["krill"].defenseTargets["terrain"] = new Object();
Unit["krill"].defenseTargets["unit"] = new Object();
Unit["krill"].defenseTargets["level"] = new Object();

// 'Krill' attack targets
Unit["krill"].attackTargets = new Object();
Unit["krill"].attackTargets["terrain"] = new Object();
Unit["krill"].attackTargets["unit"] = new Object();
Unit["krill"].attackTargets["level"] = new Object();

// ------------------------------------------------------
// 'Pretorian' Definition
// ------------------------------------------------------
Unit["pretorian"] = new Object();
Unit["pretorian"].getAttack = getAttack;
Unit["pretorian"].getMinAttack = getMinAttack;
Unit["pretorian"].getMaxAttack = getMaxAttack;
Unit["pretorian"].getLive = getLive;
Unit["pretorian"].name = "Pretorian";
Unit["pretorian"].category = "medium";
Unit["pretorian"].baseAttack = 350;
Unit["pretorian"].baseDefense = 2800;
Unit["pretorian"].maximumDamage = 620;
Unit["pretorian"].minimumDamage = 110;
Unit["pretorian"].hitPoints = 5550;
Unit["pretorian"].canStrikeBack = true;
Unit["pretorian"].catapultAttack = false;
Unit["pretorian"].tripleAttack = false;
Unit["pretorian"].replicatorAttack = false;
Unit["pretorian"].canDamageBehindUnits = false;
Unit["pretorian"].range = 3;
Unit["pretorian"].movementCost = 2;
Unit["pretorian"].movementType = "diagonal";
Unit["pretorian"].level = "air";
		
// 'Pretorian' defense targets
Unit["pretorian"].defenseTargets = new Object();
Unit["pretorian"].defenseTargets["terrain"] = new Object();
Unit["pretorian"].defenseTargets["unit"] = new Object();
Unit["pretorian"].defenseTargets["unit"]["heavy"] = 500;
	Unit["pretorian"].defenseTargets["level"] = new Object();

// 'Pretorian' attack targets
Unit["pretorian"].attackTargets = new Object();
Unit["pretorian"].attackTargets["terrain"] = new Object();
Unit["pretorian"].attackTargets["unit"] = new Object();
Unit["pretorian"].attackTargets["level"] = new Object();

// ------------------------------------------------------
// 'Vector' Definition
// ------------------------------------------------------
Unit["vector"] = new Object();
Unit["vector"].getAttack = getAttack;
Unit["vector"].getMinAttack = getMinAttack;
Unit["vector"].getMaxAttack = getMaxAttack;
Unit["vector"].getLive = getLive;
Unit["vector"].name = "Vector";
Unit["vector"].category = "medium";
Unit["vector"].baseAttack = 2000;
Unit["vector"].baseDefense = 1200;
Unit["vector"].maximumDamage = 2300;
Unit["vector"].minimumDamage = 1900;
Unit["vector"].hitPoints = 1500;
Unit["vector"].canStrikeBack = false;
Unit["vector"].catapultAttack = true;
Unit["vector"].tripleAttack = false;
Unit["vector"].replicatorAttack = false;
Unit["vector"].canDamageBehindUnits = false;
Unit["vector"].range = 3;
Unit["vector"].movementCost = 3;
Unit["vector"].movementType = "normal";
Unit["vector"].level = "air";
		
// 'Vector' defense targets
Unit["vector"].defenseTargets = new Object();
Unit["vector"].defenseTargets["terrain"] = new Object();
Unit["vector"].defenseTargets["terrain"]["forest"] = 400;
	Unit["vector"].defenseTargets["unit"] = new Object();
Unit["vector"].defenseTargets["level"] = new Object();

// 'Vector' attack targets
Unit["vector"].attackTargets = new Object();
Unit["vector"].attackTargets["terrain"] = new Object();
Unit["vector"].attackTargets["terrain"]["forest"] = 400;
	Unit["vector"].attackTargets["unit"] = new Object();
Unit["vector"].attackTargets["unit"]["heavy"] = 300;
	Unit["vector"].attackTargets["level"] = new Object();

// ------------------------------------------------------
// 'Kamikaze' Definition
// ------------------------------------------------------
Unit["kamikaze"] = new Object();
Unit["kamikaze"].getAttack = getAttack;
Unit["kamikaze"].getMinAttack = getMinAttack;
Unit["kamikaze"].getMaxAttack = getMaxAttack;
Unit["kamikaze"].getLive = getLive;
Unit["kamikaze"].name = "Kamikaze";
Unit["kamikaze"].category = "medium";
Unit["kamikaze"].baseAttack = 4000;
Unit["kamikaze"].baseDefense = 1;
Unit["kamikaze"].maximumDamage = 5300;
Unit["kamikaze"].minimumDamage = 3800;
Unit["kamikaze"].hitPoints = 10;
Unit["kamikaze"].canStrikeBack = false;
Unit["kamikaze"].catapultAttack = false;
Unit["kamikaze"].tripleAttack = false;
Unit["kamikaze"].replicatorAttack = false;
Unit["kamikaze"].canDamageBehindUnits = false;
Unit["kamikaze"].range = 1;
Unit["kamikaze"].movementCost = 1;
Unit["kamikaze"].movementType = "all";
Unit["kamikaze"].level = "air";
		
// 'Kamikaze' defense targets
Unit["kamikaze"].defenseTargets = new Object();
Unit["kamikaze"].defenseTargets["terrain"] = new Object();
Unit["kamikaze"].defenseTargets["unit"] = new Object();
Unit["kamikaze"].defenseTargets["level"] = new Object();

// 'Kamikaze' attack targets
Unit["kamikaze"].attackTargets = new Object();
Unit["kamikaze"].attackTargets["terrain"] = new Object();
Unit["kamikaze"].attackTargets["unit"] = new Object();
Unit["kamikaze"].attackTargets["level"] = new Object();

// ------------------------------------------------------
// 'Kahuna' Definition
// ------------------------------------------------------
Unit["kahuna"] = new Object();
Unit["kahuna"].getAttack = getAttack;
Unit["kahuna"].getMinAttack = getMinAttack;
Unit["kahuna"].getMaxAttack = getMaxAttack;
Unit["kahuna"].getLive = getLive;
Unit["kahuna"].name = "Kahuna";
Unit["kahuna"].category = "medium";
Unit["kahuna"].baseAttack = 1000;
Unit["kahuna"].baseDefense = 1300;
Unit["kahuna"].maximumDamage = 1300;
Unit["kahuna"].minimumDamage = 950;
Unit["kahuna"].hitPoints = 1200;
Unit["kahuna"].canStrikeBack = false;
Unit["kahuna"].catapultAttack = false;
Unit["kahuna"].tripleAttack = false;
Unit["kahuna"].replicatorAttack = false;
Unit["kahuna"].canDamageBehindUnits = true;
Unit["kahuna"].range = 2;
Unit["kahuna"].movementCost = 2;
Unit["kahuna"].movementType = "all";
Unit["kahuna"].level = "ground";
		
// 'Kahuna' defense targets
Unit["kahuna"].defenseTargets = new Object();
Unit["kahuna"].defenseTargets["terrain"] = new Object();
Unit["kahuna"].defenseTargets["terrain"]["terrest"] = 400;
	Unit["kahuna"].defenseTargets["unit"] = new Object();
Unit["kahuna"].defenseTargets["level"] = new Object();
Unit["kahuna"].defenseTargets["level"]["air"] = 400;
	
// 'Kahuna' attack targets
Unit["kahuna"].attackTargets = new Object();
Unit["kahuna"].attackTargets["terrain"] = new Object();
Unit["kahuna"].attackTargets["terrain"]["terrest"] = 400;
	Unit["kahuna"].attackTargets["unit"] = new Object();
Unit["kahuna"].attackTargets["level"] = new Object();

// ------------------------------------------------------
// 'Eagle' Definition
// ------------------------------------------------------
Unit["eagle"] = new Object();
Unit["eagle"].getAttack = getAttack;
Unit["eagle"].getMinAttack = getMinAttack;
Unit["eagle"].getMaxAttack = getMaxAttack;
Unit["eagle"].getLive = getLive;
Unit["eagle"].name = "Eagle";
Unit["eagle"].category = "medium";
Unit["eagle"].baseAttack = 1100;
Unit["eagle"].baseDefense = 1200;
Unit["eagle"].maximumDamage = 1200;
Unit["eagle"].minimumDamage = 1000;
Unit["eagle"].hitPoints = 1400;
Unit["eagle"].canStrikeBack = false;
Unit["eagle"].catapultAttack = true;
Unit["eagle"].tripleAttack = false;
Unit["eagle"].replicatorAttack = false;
Unit["eagle"].canDamageBehindUnits = false;
Unit["eagle"].range = 3;
Unit["eagle"].movementCost = 2;
Unit["eagle"].movementType = "diagonal";
Unit["eagle"].level = "air";
		
// 'Eagle' defense targets
Unit["eagle"].defenseTargets = new Object();
Unit["eagle"].defenseTargets["terrain"] = new Object();
Unit["eagle"].defenseTargets["terrain"]["desert"] = 100;
	Unit["eagle"].defenseTargets["unit"] = new Object();
Unit["eagle"].defenseTargets["unit"]["heavy"] = 400;
	Unit["eagle"].defenseTargets["level"] = new Object();

// 'Eagle' attack targets
Unit["eagle"].attackTargets = new Object();
Unit["eagle"].attackTargets["terrain"] = new Object();
Unit["eagle"].attackTargets["terrain"]["desert"] = 200;
	Unit["eagle"].attackTargets["unit"] = new Object();
Unit["eagle"].attackTargets["unit"]["medium"] = 400;
	Unit["eagle"].attackTargets["level"] = new Object();

// ------------------------------------------------------
// 'Scarab' Definition
// ------------------------------------------------------
Unit["scarab"] = new Object();
Unit["scarab"].getAttack = getAttack;
Unit["scarab"].getMinAttack = getMinAttack;
Unit["scarab"].getMaxAttack = getMaxAttack;
Unit["scarab"].getLive = getLive;
Unit["scarab"].name = "Scarab";
Unit["scarab"].category = "medium";
Unit["scarab"].baseAttack = 1900;
Unit["scarab"].baseDefense = 2300;
Unit["scarab"].maximumDamage = 2300;
Unit["scarab"].minimumDamage = 1800;
Unit["scarab"].hitPoints = 2200;
Unit["scarab"].canStrikeBack = false;
Unit["scarab"].catapultAttack = false;
Unit["scarab"].tripleAttack = false;
Unit["scarab"].replicatorAttack = false;
Unit["scarab"].canDamageBehindUnits = false;
Unit["scarab"].range = 2;
Unit["scarab"].movementCost = 1;
Unit["scarab"].movementType = "front";
Unit["scarab"].level = "air";
		
// 'Scarab' defense targets
Unit["scarab"].defenseTargets = new Object();
Unit["scarab"].defenseTargets["terrain"] = new Object();
Unit["scarab"].defenseTargets["unit"] = new Object();
Unit["scarab"].defenseTargets["level"] = new Object();
Unit["scarab"].defenseTargets["level"]["ground"] = 500;
	
// 'Scarab' attack targets
Unit["scarab"].attackTargets = new Object();
Unit["scarab"].attackTargets["terrain"] = new Object();
Unit["scarab"].attackTargets["unit"] = new Object();
Unit["scarab"].attackTargets["level"] = new Object();

// ------------------------------------------------------
// 'Driller' Definition
// ------------------------------------------------------
Unit["driller"] = new Object();
Unit["driller"].getAttack = getAttack;
Unit["driller"].getMinAttack = getMinAttack;
Unit["driller"].getMaxAttack = getMaxAttack;
Unit["driller"].getLive = getLive;
Unit["driller"].name = "Driller";
Unit["driller"].category = "medium";
Unit["driller"].baseAttack = 1500;
Unit["driller"].baseDefense = 1500;
Unit["driller"].maximumDamage = 1700;
Unit["driller"].minimumDamage = 1250;
Unit["driller"].hitPoints = 1500;
Unit["driller"].canStrikeBack = false;
Unit["driller"].catapultAttack = false;
Unit["driller"].tripleAttack = true;
Unit["driller"].replicatorAttack = false;
Unit["driller"].canDamageBehindUnits = false;
Unit["driller"].range = 1;
Unit["driller"].movementCost = 2;
Unit["driller"].movementType = "all";
Unit["driller"].level = "ground";
		
// 'Driller' defense targets
Unit["driller"].defenseTargets = new Object();
Unit["driller"].defenseTargets["terrain"] = new Object();
Unit["driller"].defenseTargets["unit"] = new Object();
Unit["driller"].defenseTargets["level"] = new Object();
Unit["driller"].defenseTargets["level"]["ground"] = 500;
	
// 'Driller' attack targets
Unit["driller"].attackTargets = new Object();
Unit["driller"].attackTargets["terrain"] = new Object();
Unit["driller"].attackTargets["unit"] = new Object();
Unit["driller"].attackTargets["level"] = new Object();

// ------------------------------------------------------
// 'Crusader' Definition
// ------------------------------------------------------
Unit["crusader"] = new Object();
Unit["crusader"].getAttack = getAttack;
Unit["crusader"].getMinAttack = getMinAttack;
Unit["crusader"].getMaxAttack = getMaxAttack;
Unit["crusader"].getLive = getLive;
Unit["crusader"].name = "Crusader";
Unit["crusader"].category = "heavy";
Unit["crusader"].baseAttack = 2600;
Unit["crusader"].baseDefense = 2200;
Unit["crusader"].maximumDamage = 2900;
Unit["crusader"].minimumDamage = 2350;
Unit["crusader"].hitPoints = 2400;
Unit["crusader"].canStrikeBack = false;
Unit["crusader"].catapultAttack = false;
Unit["crusader"].tripleAttack = false;
Unit["crusader"].replicatorAttack = false;
Unit["crusader"].canDamageBehindUnits = false;
Unit["crusader"].range = 6;
Unit["crusader"].movementCost = 4;
Unit["crusader"].movementType = "front";
Unit["crusader"].level = "air";
		
// 'Crusader' defense targets
Unit["crusader"].defenseTargets = new Object();
Unit["crusader"].defenseTargets["terrain"] = new Object();
Unit["crusader"].defenseTargets["unit"] = new Object();
Unit["crusader"].defenseTargets["level"] = new Object();

// 'Crusader' attack targets
Unit["crusader"].attackTargets = new Object();
Unit["crusader"].attackTargets["terrain"] = new Object();
Unit["crusader"].attackTargets["unit"] = new Object();
Unit["crusader"].attackTargets["level"] = new Object();

// ------------------------------------------------------
// 'Nova' Definition
// ------------------------------------------------------
Unit["nova"] = new Object();
Unit["nova"].getAttack = getAttack;
Unit["nova"].getMinAttack = getMinAttack;
Unit["nova"].getMaxAttack = getMaxAttack;
Unit["nova"].getLive = getLive;
Unit["nova"].name = "Nova";
Unit["nova"].category = "heavy";
Unit["nova"].baseAttack = 2700;
Unit["nova"].baseDefense = 1900;
Unit["nova"].maximumDamage = 3800;
Unit["nova"].minimumDamage = 2500;
Unit["nova"].hitPoints = 2000;
Unit["nova"].canStrikeBack = false;
Unit["nova"].catapultAttack = false;
Unit["nova"].tripleAttack = false;
Unit["nova"].replicatorAttack = false;
Unit["nova"].canDamageBehindUnits = false;
Unit["nova"].range = 5;
Unit["nova"].movementCost = 4;
Unit["nova"].movementType = "normal";
Unit["nova"].level = "air";
		
// 'Nova' defense targets
Unit["nova"].defenseTargets = new Object();
Unit["nova"].defenseTargets["terrain"] = new Object();
Unit["nova"].defenseTargets["unit"] = new Object();
Unit["nova"].defenseTargets["level"] = new Object();

// 'Nova' attack targets
Unit["nova"].attackTargets = new Object();
Unit["nova"].attackTargets["terrain"] = new Object();
Unit["nova"].attackTargets["unit"] = new Object();
Unit["nova"].attackTargets["unit"]["animal"] = 4000;
	Unit["nova"].attackTargets["level"] = new Object();

// ------------------------------------------------------
// 'Bozer' Definition
// ------------------------------------------------------
Unit["bozer"] = new Object();
Unit["bozer"].getAttack = getAttack;
Unit["bozer"].getMinAttack = getMinAttack;
Unit["bozer"].getMaxAttack = getMaxAttack;
Unit["bozer"].getLive = getLive;
Unit["bozer"].name = "Bozer";
Unit["bozer"].category = "heavy";
Unit["bozer"].baseAttack = 3200;
Unit["bozer"].baseDefense = 2800;
Unit["bozer"].maximumDamage = 3450;
Unit["bozer"].minimumDamage = 3100;
Unit["bozer"].hitPoints = 2800;
Unit["bozer"].canStrikeBack = true;
Unit["bozer"].catapultAttack = false;
Unit["bozer"].tripleAttack = false;
Unit["bozer"].replicatorAttack = false;
Unit["bozer"].canDamageBehindUnits = false;
Unit["bozer"].range = 5;
Unit["bozer"].movementCost = 4;
Unit["bozer"].movementType = "front";
Unit["bozer"].level = "ground";
		
// 'Bozer' defense targets
Unit["bozer"].defenseTargets = new Object();
Unit["bozer"].defenseTargets["terrain"] = new Object();
Unit["bozer"].defenseTargets["terrain"]["forest"] = 500;
	Unit["bozer"].defenseTargets["terrain"]["terrest"] = 150;
	Unit["bozer"].defenseTargets["unit"] = new Object();
Unit["bozer"].defenseTargets["level"] = new Object();
Unit["bozer"].defenseTargets["level"]["ground"] = -2000;
	
// 'Bozer' attack targets
Unit["bozer"].attackTargets = new Object();
Unit["bozer"].attackTargets["terrain"] = new Object();
Unit["bozer"].attackTargets["unit"] = new Object();
Unit["bozer"].attackTargets["level"] = new Object();
Unit["bozer"].attackTargets["level"]["air"] = 3000;
	
// ------------------------------------------------------
// 'Fenix' Definition
// ------------------------------------------------------
Unit["fenix"] = new Object();
Unit["fenix"].getAttack = getAttack;
Unit["fenix"].getMinAttack = getMinAttack;
Unit["fenix"].getMaxAttack = getMaxAttack;
Unit["fenix"].getLive = getLive;
Unit["fenix"].name = "Fenix";
Unit["fenix"].category = "heavy";
Unit["fenix"].baseAttack = 2500;
Unit["fenix"].baseDefense = 2800;
Unit["fenix"].maximumDamage = 2900;
Unit["fenix"].minimumDamage = 2450;
Unit["fenix"].hitPoints = 2950;
Unit["fenix"].canStrikeBack = false;
Unit["fenix"].catapultAttack = false;
Unit["fenix"].tripleAttack = false;
Unit["fenix"].replicatorAttack = false;
Unit["fenix"].canDamageBehindUnits = true;
Unit["fenix"].range = 4;
Unit["fenix"].movementCost = 3;
Unit["fenix"].movementType = "normal";
Unit["fenix"].level = "air";
		
// 'Fenix' defense targets
Unit["fenix"].defenseTargets = new Object();
Unit["fenix"].defenseTargets["terrain"] = new Object();
Unit["fenix"].defenseTargets["unit"] = new Object();
Unit["fenix"].defenseTargets["level"] = new Object();

// 'Fenix' attack targets
Unit["fenix"].attackTargets = new Object();
Unit["fenix"].attackTargets["terrain"] = new Object();
Unit["fenix"].attackTargets["unit"] = new Object();
Unit["fenix"].attackTargets["unit"]["medium"] = 200;
	Unit["fenix"].attackTargets["level"] = new Object();

// ------------------------------------------------------
// 'Doomer' Definition
// ------------------------------------------------------
Unit["doomer"] = new Object();
Unit["doomer"].getAttack = getAttack;
Unit["doomer"].getMinAttack = getMinAttack;
Unit["doomer"].getMaxAttack = getMaxAttack;
Unit["doomer"].getLive = getLive;
Unit["doomer"].name = "Doomer";
Unit["doomer"].category = "heavy";
Unit["doomer"].baseAttack = 6000;
Unit["doomer"].baseDefense = 500;
Unit["doomer"].maximumDamage = 6200;
Unit["doomer"].minimumDamage = 5800;
Unit["doomer"].hitPoints = 800;
Unit["doomer"].canStrikeBack = false;
Unit["doomer"].catapultAttack = true;
Unit["doomer"].tripleAttack = false;
Unit["doomer"].replicatorAttack = false;
Unit["doomer"].canDamageBehindUnits = true;
Unit["doomer"].range = 3;
Unit["doomer"].movementCost = 3;
Unit["doomer"].movementType = "diagonal";
Unit["doomer"].level = "air";
		
// 'Doomer' defense targets
Unit["doomer"].defenseTargets = new Object();
Unit["doomer"].defenseTargets["terrain"] = new Object();
Unit["doomer"].defenseTargets["unit"] = new Object();
Unit["doomer"].defenseTargets["level"] = new Object();

// 'Doomer' attack targets
Unit["doomer"].attackTargets = new Object();
Unit["doomer"].attackTargets["terrain"] = new Object();
Unit["doomer"].attackTargets["unit"] = new Object();
Unit["doomer"].attackTargets["level"] = new Object();

// ------------------------------------------------------
// 'Taurus' Definition
// ------------------------------------------------------
Unit["taurus"] = new Object();
Unit["taurus"].getAttack = getAttack;
Unit["taurus"].getMinAttack = getMinAttack;
Unit["taurus"].getMaxAttack = getMaxAttack;
Unit["taurus"].getLive = getLive;
Unit["taurus"].name = "Taurus";
Unit["taurus"].category = "heavy";
Unit["taurus"].baseAttack = 5000;
Unit["taurus"].baseDefense = 3500;
Unit["taurus"].maximumDamage = 5500;
Unit["taurus"].minimumDamage = 3000;
Unit["taurus"].hitPoints = 3800;
Unit["taurus"].canStrikeBack = false;
Unit["taurus"].catapultAttack = false;
Unit["taurus"].tripleAttack = true;
Unit["taurus"].replicatorAttack = false;
Unit["taurus"].canDamageBehindUnits = true;
Unit["taurus"].range = 3;
Unit["taurus"].movementCost = 4;
Unit["taurus"].movementType = "front";
Unit["taurus"].level = "ground";
		
// 'Taurus' defense targets
Unit["taurus"].defenseTargets = new Object();
Unit["taurus"].defenseTargets["terrain"] = new Object();
Unit["taurus"].defenseTargets["unit"] = new Object();
Unit["taurus"].defenseTargets["level"] = new Object();

// 'Taurus' attack targets
Unit["taurus"].attackTargets = new Object();
Unit["taurus"].attackTargets["terrain"] = new Object();
Unit["taurus"].attackTargets["unit"] = new Object();
Unit["taurus"].attackTargets["level"] = new Object();

// ------------------------------------------------------
// 'Spider' Definition
// ------------------------------------------------------
Unit["spider"] = new Object();
Unit["spider"].getAttack = getAttack;
Unit["spider"].getMinAttack = getMinAttack;
Unit["spider"].getMaxAttack = getMaxAttack;
Unit["spider"].getLive = getLive;
Unit["spider"].name = "Spider";
Unit["spider"].category = "animal";
Unit["spider"].baseAttack = 1400;
Unit["spider"].baseDefense = 2000;
Unit["spider"].maximumDamage = 1850;
Unit["spider"].minimumDamage = 1350;
Unit["spider"].hitPoints = 1200;
Unit["spider"].canStrikeBack = true;
Unit["spider"].catapultAttack = false;
Unit["spider"].tripleAttack = false;
Unit["spider"].replicatorAttack = false;
Unit["spider"].canDamageBehindUnits = false;
Unit["spider"].range = 3;
Unit["spider"].movementCost = 2;
Unit["spider"].movementType = "all";
Unit["spider"].level = "ground";
		
// 'Spider' defense targets
Unit["spider"].defenseTargets = new Object();
Unit["spider"].defenseTargets["terrain"] = new Object();
Unit["spider"].defenseTargets["unit"] = new Object();
Unit["spider"].defenseTargets["unit"]["heavy"] = 500;
	Unit["spider"].defenseTargets["level"] = new Object();

// 'Spider' attack targets
Unit["spider"].attackTargets = new Object();
Unit["spider"].attackTargets["terrain"] = new Object();
Unit["spider"].attackTargets["unit"] = new Object();
Unit["spider"].attackTargets["unit"]["heavy"] = 500;
	Unit["spider"].attackTargets["level"] = new Object();

// ------------------------------------------------------
// 'BlackWidow' Definition
// ------------------------------------------------------
Unit["blackwidow"] = new Object();
Unit["blackwidow"].getAttack = getAttack;
Unit["blackwidow"].getMinAttack = getMinAttack;
Unit["blackwidow"].getMaxAttack = getMaxAttack;
Unit["blackwidow"].getLive = getLive;
Unit["blackwidow"].name = "BlackWidow";
Unit["blackwidow"].category = "animal";
Unit["blackwidow"].baseAttack = 2200;
Unit["blackwidow"].baseDefense = 2800;
Unit["blackwidow"].maximumDamage = 2400;
Unit["blackwidow"].minimumDamage = 2000;
Unit["blackwidow"].hitPoints = 2000;
Unit["blackwidow"].canStrikeBack = true;
Unit["blackwidow"].catapultAttack = true;
Unit["blackwidow"].tripleAttack = true;
Unit["blackwidow"].replicatorAttack = false;
Unit["blackwidow"].canDamageBehindUnits = false;
Unit["blackwidow"].range = 2;
Unit["blackwidow"].movementCost = 3;
Unit["blackwidow"].movementType = "all";
Unit["blackwidow"].level = "ground";
		
// 'BlackWidow' defense targets
Unit["blackwidow"].defenseTargets = new Object();
Unit["blackwidow"].defenseTargets["terrain"] = new Object();
Unit["blackwidow"].defenseTargets["unit"] = new Object();
Unit["blackwidow"].defenseTargets["level"] = new Object();

// 'BlackWidow' attack targets
Unit["blackwidow"].attackTargets = new Object();
Unit["blackwidow"].attackTargets["terrain"] = new Object();
Unit["blackwidow"].attackTargets["unit"] = new Object();
Unit["blackwidow"].attackTargets["level"] = new Object();

// ------------------------------------------------------
// 'Squid' Definition
// ------------------------------------------------------
Unit["squid"] = new Object();
Unit["squid"].getAttack = getAttack;
Unit["squid"].getMinAttack = getMinAttack;
Unit["squid"].getMaxAttack = getMaxAttack;
Unit["squid"].getLive = getLive;
Unit["squid"].name = "Squid";
Unit["squid"].category = "animal";
Unit["squid"].baseAttack = 1500;
Unit["squid"].baseDefense = 1800;
Unit["squid"].maximumDamage = 1600;
Unit["squid"].minimumDamage = 1400;
Unit["squid"].hitPoints = 200;
Unit["squid"].canStrikeBack = false;
Unit["squid"].catapultAttack = false;
Unit["squid"].tripleAttack = false;
Unit["squid"].replicatorAttack = false;
Unit["squid"].canDamageBehindUnits = true;
Unit["squid"].range = 3;
Unit["squid"].movementCost = 2;
Unit["squid"].movementType = "all";
Unit["squid"].level = "ground";
		
// 'Squid' defense targets
Unit["squid"].defenseTargets = new Object();
Unit["squid"].defenseTargets["terrain"] = new Object();
Unit["squid"].defenseTargets["terrain"]["water"] = 2300;
	Unit["squid"].defenseTargets["unit"] = new Object();
Unit["squid"].defenseTargets["level"] = new Object();

// 'Squid' attack targets
Unit["squid"].attackTargets = new Object();
Unit["squid"].attackTargets["terrain"] = new Object();
Unit["squid"].attackTargets["terrain"]["water"] = 500;
	Unit["squid"].attackTargets["unit"] = new Object();
Unit["squid"].attackTargets["unit"]["heavy"] = 500;
	Unit["squid"].attackTargets["level"] = new Object();
