<xsl:stylesheet version='1.0' 
     xmlns:xsl='http://www.w3.org/1999/XSL/Transform'
     xmlns:toLower="urn:ToLower" >

	<xsl:output omit-xml-declaration='yes' method='text'
       media-type='text/javascript' indent='no' />
       
	<xsl:param name="topObject" select="'Ship'"/>
 
	<xsl:template match="battle">
// ------------------------------------------------------
// '<xsl:value-of select="../@value" />' Definition
// ------------------------------------------------------
<xsl:value-of select="$topObject" />["<xsl:value-of select="toLower:Operate(../@value)" />"] = new Object();
<xsl:value-of select="$topObject" />["<xsl:value-of select="toLower:Operate(../@value)" />"].getAttack = getAttack;
<xsl:value-of select="$topObject" />["<xsl:value-of select="toLower:Operate(../@value)" />"].getMinAttack = getMinAttack;
<xsl:value-of select="$topObject" />["<xsl:value-of select="toLower:Operate(../@value)" />"].getMaxAttack = getMaxAttack;
<xsl:value-of select="$topObject" />["<xsl:value-of select="toLower:Operate(../@value)" />"].getLive = getLive;
<xsl:value-of select="$topObject" />["<xsl:value-of select="toLower:Operate(../@value)" />"].name = "<xsl:value-of select="../@value" />";
<xsl:value-of select="$topObject" />["<xsl:value-of select="toLower:Operate(../@value)" />"].category = "<xsl:value-of select="@unitType" />";
<xsl:value-of select="$topObject" />["<xsl:value-of select="toLower:Operate(../@value)" />"].baseAttack = <xsl:value-of select="attack/@base" />;
<xsl:value-of select="$topObject" />["<xsl:value-of select="toLower:Operate(../@value)" />"].baseDefense = <xsl:value-of select="defense/@base" />;
<xsl:value-of select="$topObject" />["<xsl:value-of select="toLower:Operate(../@value)" />"].maximumDamage = <xsl:value-of select="attack/@maximumDamage" />;
<xsl:value-of select="$topObject" />["<xsl:value-of select="toLower:Operate(../@value)" />"].minimumDamage = <xsl:value-of select="attack/@minimumDamage" />;
<xsl:value-of select="$topObject" />["<xsl:value-of select="toLower:Operate(../@value)" />"].hitPoints = <xsl:value-of select="defense/@hitPoints" />;
<xsl:value-of select="$topObject" />["<xsl:value-of select="toLower:Operate(../@value)" />"].canStrikeBack = <xsl:apply-templates select="defense" mode="canStrikeBack" />;
<xsl:value-of select="$topObject" />["<xsl:value-of select="toLower:Operate(../@value)" />"].catapultAttack = <xsl:apply-templates select="attack" mode="catapultAttack" />;
<xsl:value-of select="$topObject" />["<xsl:value-of select="toLower:Operate(../@value)" />"].tripleAttack = <xsl:apply-templates select="attack" mode="tripleAttack" />;
<xsl:value-of select="$topObject" />["<xsl:value-of select="toLower:Operate(../@value)" />"].replicatorAttack = <xsl:apply-templates select="attack" mode="replicatorAttack" />;
<xsl:value-of select="$topObject" />["<xsl:value-of select="toLower:Operate(../@value)" />"].canDamageBehindUnits = <xsl:apply-templates select="attack" mode="canDamageBehindUnits" />;
<xsl:value-of select="$topObject" />["<xsl:value-of select="toLower:Operate(../@value)" />"].range = <xsl:value-of select="attack/@range" />;
<xsl:value-of select="$topObject" />["<xsl:value-of select="toLower:Operate(../@value)" />"].movementCost = <xsl:value-of select="movement/@cost" />;
<xsl:value-of select="$topObject" />["<xsl:value-of select="toLower:Operate(../@value)" />"].movementType = "<xsl:value-of select="movement/@type" />";
<xsl:value-of select="$topObject" />["<xsl:value-of select="toLower:Operate(../@value)" />"].level = "<xsl:value-of select="movement/@level" />";
		<xsl:call-template name="targets">
			<xsl:with-param name="targets" select="'defenseTargets'" />
			<xsl:with-param name="type" select="'defense'" />
			<xsl:with-param name="typeNode" select="defense" />
		</xsl:call-template>
		<xsl:call-template name="targets">
			<xsl:with-param name="targets" select="'attackTargets'" />
			<xsl:with-param name="type" select="'attack'" />
			<xsl:with-param name="typeNode" select="attack" />
		</xsl:call-template>
	</xsl:template>
	
	<xsl:template name="targets" >
		<xsl:param name="targets" select="'attackTargets'" />
		<xsl:param name="type" select="'attack'" />
		<xsl:param name="typeNode" select="attack" />
// '<xsl:value-of select="../@value" />' <xsl:value-of select="$type" /> targets
<xsl:value-of select="$topObject" />["<xsl:value-of select="toLower:Operate(../@value)" />"].<xsl:value-of select="$targets" /> = new Object();
<xsl:value-of select="$topObject" />["<xsl:value-of select="toLower:Operate(../@value)" />"].<xsl:value-of select="$targets" />["terrain"] = new Object();
<xsl:apply-templates select="$typeNode/target[@type='terrain']" >
	<xsl:with-param name="targets" select="$targets" />
</xsl:apply-templates>
<xsl:value-of select="$topObject" />["<xsl:value-of select="toLower:Operate(../@value)" />"].<xsl:value-of select="$targets" />["unit"] = new Object();
<xsl:apply-templates select="$typeNode/target[@type='unit']" >
	<xsl:with-param name="targets" select="$targets" />
</xsl:apply-templates>
<xsl:value-of select="$topObject" />["<xsl:value-of select="toLower:Operate(../@value)" />"].<xsl:value-of select="$targets" />["level"] = new Object();
<xsl:apply-templates select="$typeNode/target[@type='level']" >
	<xsl:with-param name="targets" select="$targets" />
</xsl:apply-templates>
	</xsl:template>
  
 	<xsl:template match="target" >
 		<xsl:param name="targets" select="'attackTargets'" />
		<xsl:value-of select="$topObject" />["<xsl:value-of select="toLower:Operate(../../../@value)" />"].<xsl:value-of select="$targets" />["<xsl:value-of select="@type" />"]["<xsl:value-of select="@key" />"] = <xsl:value-of select="@value" />;
	</xsl:template> 

	<xsl:template match="defense" mode="canStrikeBack" >
		<xsl:choose>
			<xsl:when test="@canStrikeBack">
				<xsl:value-of select="translate(@canStrikeBack, 'TF', 'tf')" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:text>false</xsl:text>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template> 
	
	<xsl:template match="attack" mode="canDamageBehindUnits" >
		<xsl:choose>
			<xsl:when test="@canDamageBehindUnits">
				<xsl:value-of select="translate(@canDamageBehindUnits, 'TF', 'tf')" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:text>false</xsl:text>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template> 
	
	<xsl:template match="attack" mode="catapultAttack" >
		<xsl:choose>
			<xsl:when test="@catapultAttack">
				<xsl:value-of select="translate(@catapultAttack, 'TF', 'tf')" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:text>false</xsl:text>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
	<xsl:template match="attack" mode="tripleAttack" >
		<xsl:choose>
			<xsl:when test="@tripleAttack">
				<xsl:value-of select="translate(@tripleAttack, 'TF', 'tf')" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:text>false</xsl:text>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
	<xsl:template match="attack" mode="replicatorAttack" >
		<xsl:choose>
			<xsl:when test="@replicatorAttack">
				<xsl:value-of select="translate(@replicatorAttack, 'TF', 'tf')" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:text>false</xsl:text>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template> 

</xsl:stylesheet>
