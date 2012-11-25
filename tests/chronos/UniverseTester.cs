namespace Chronos.Tests {
	
	using System;
	using System.Collections;
	using Chronos;
	using Chronos.Resources;
	using Chronos.Alliances;
	using Chronos.Core;
	using NUnit.Framework;
	
	[TestFixture]
	public class UniverseTester {
    	
		private Universe universe;
		private Hashtable _factories;

    	[SetUp]
    	public void startTest() {

			_factories = Universe.factories;

			//verificar de o universe  singleton
			universe = Universe.instance;
			Assert.IsNotNull(universe,"Universe not set to an instance of an object");
			Assert.AreEqual(Universe.instance,universe,"Instance of the universe is not equal");
			
			universe.init();
			
			//verificar as cenas l dentro
			Assert.IsNotNull(Universe.factories,"factories shouldn't be null @ Universe");
			Assert.IsNotNull(universe.alliances,"alliances shouldn't be null @ Universe");
			Assert.AreEqual(1,universe.alliances.Count,"alliances should have one alliance" );
		}
    	
		[Test]
    	public void addNewUsers() {
			//criar uns quantos users
			Ruler pyro = new Ruler( _factories, "Pyro" );
			Ruler axanta = new Ruler( _factories, "axanta" );
			Ruler pre = new Ruler( _factories, "pre" );
			Ruler jc = new Ruler( _factories, "jc" );
			Ruler ninhas = new Ruler( _factories, "ninhas" );
			Ruler vanessa = new Ruler( _factories, "vanessa" );

			//adicion-los ao universo
			universe.addRulerToUniverse( pyro , "Jupiter" );
			universe.addRulerToUniverse( axanta, "Super Tia" );
			universe.addRulerToUniverse( pre, "Developer" );
			universe.addRulerToUniverse( jc, "Tunning" );
			universe.addRulerToUniverse( ninhas, "Sapa" );
			universe.addRulerToUniverse( vanessa, "Mimi" );

			Assert.AreEqual( universe.rulerCount, 6, "Universe should have 6 rulers!" );

			//verificar o nmero de planetas do universo
			Assert.AreEqual( universe.planetCount, 6, "Universe should have 6 planets!" );
		}

		public void testAlliances() {
			Ruler pyro = new Ruler( _factories, "Pyro" );
			Ruler axanta = new Ruler( _factories, "axanta" );
			Ruler pre = new Ruler( _factories, "pre" );
			Ruler jc = new Ruler( _factories, "jc" );
			Ruler ninhas = new Ruler( _factories, "ninhas" );
			Ruler vanessa = new Ruler( _factories, "vanessa" );

			//adicion-los ao universo
			universe.addRulerToUniverse( pyro , "Jupiter" );
			universe.addRulerToUniverse( axanta, "Super Tia" );
			universe.addRulerToUniverse( pre, "Developer" );
			universe.addRulerToUniverse( jc, "Tunning" );
			universe.addRulerToUniverse( ninhas, "Sapa" );
			universe.addRulerToUniverse( vanessa, "Mimi" );

			//um lembra-se de criar de aliana
			universe.createAlliance("Pyro's Alliance", pyro );
			//outro adiciona-se a essa aliana
			universe.changeRulerAlliance("Pyro's Alliance", pre );

			//verificar as alianas
			Assert.AreEqual( universe.allianceCount, 2, "Universe should have 2 alliances" );
			Assert.AreEqual( 4,((Alliance)universe.alliances["no-alliance"]).Members.Length, "No alliance should have 4 members" );

		}

		public void testTurn()
		{
			IDictionaryEnumerator enumerator = universe.alliances.GetEnumerator();
			while( enumerator.MoveNext() ){
				Alliance alliance = (Alliance)universe.alliances[enumerator.Key];
				for(int i = 0 ; i < alliance.Members.Length ; ++i ) {
					Planet[] planets = ((AllianceMember)alliance.Members.GetValue(i)).Ruler.Planets;

					foreach( Planet planet in planets ) {
						planet.addResource("Intrinsic","energy",30);
						planet.addResource("Intrinsic","mp",1600);
						planet.addResource("Intrinsic","gold",1000);
						planet.addResource("Intrinsic","labor",4000);
					}
				}
			}

			universe.turn();
		}

		[Test]
		public void conquerPlanet(){
			Ruler ruler = new Ruler( Globals.factories, "pre" );
			Ruler newOwner = new Ruler( Globals.factories, "pyro" );
			
			universe.addRulerToUniverse( ruler, "Marte" );
			universe.addRulerToUniverse( newOwner, "Terra" );
			
			universe.addPlanet( "NewEarth", ruler );

			Assert.AreEqual( universe.planetCount , 3 , "Universe should have 3 Planets");
			Assert.IsTrue( ruler.Planets.Length == 2 , "Pre should have 2 Planets");
			Assert.IsTrue( newOwner.Planets.Length == 1 , "Pyro should have 1 Planet");
			
			int i = ruler.getIndex( "NewEarth");
			Assert.AreEqual( 0, i, "O planeta NewEarth devia estar no indice 0");
			
			Planet p = ruler.Planets[i];
			Fleet fleet = Globals.CreateFleetToConquer(newOwner);

			ruler.removePlanet( p );

			universe.conquerPlanet( p.Coordinate, "Jupiter", fleet );

			Assert.IsTrue( ruler.Planets.Length == 1 , "Pre should have 1 planet");
			Assert.IsTrue( newOwner.Planets.Length == 2 , "Pyro should have 2 planets");
			Assert.IsTrue( p.Owner == newOwner , "The Owner of the NewEarth planet should be Pyro");
		}
	};
}
