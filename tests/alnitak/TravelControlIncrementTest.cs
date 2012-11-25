using NUnit.Framework;

namespace Alnitak.Tests {
	[TestFixture]
	public class TravelControlIncrementTest {
		private int max = 1;

		[SetUp]
		public void startTest() {}

		[Test]
		public void TestGalaxyUtil() {
			GalaxyUtil incGalaxy = new GalaxyUtil();
			string coord = "1";

			incGalaxy.increment(ref coord, max);
			Assert.AreEqual("2", coord, "Incremento da galxia falhou: 1 para 2");

			incGalaxy.increment(ref coord, max);
			Assert.AreEqual("3", coord, "Incremento da galxia falhou: 2 para 3");

			incGalaxy.increment(ref coord, max);
			Assert.AreEqual("1", coord, "Incremento da galxia falhou: 3 para 1");
		}

		[Test]
		public void DecrementGalaxy() {
			GalaxyUtil incGalaxy = new GalaxyUtil();
			string coord = "1";

			incGalaxy.decrement(ref coord, max);
			Assert.AreEqual("3", coord, "Incremento da galxia falhou: 1 para 3");

			incGalaxy.decrement(ref coord, max);
			Assert.AreEqual("2", coord, "Incremento da galxia falhou: 3 para 2");

			incGalaxy.decrement(ref coord, max);
			Assert.AreEqual("1", coord, "Incremento da galxia falhou: 2 para 1");
		}

		[Test]
		public void SystemUtil() {
			SystemUtil incSystem = new SystemUtil();

			string coord = "1:1";

			incSystem.increment(ref coord, max);
			Assert.AreEqual("1:2", coord, "Incremento do Sistem falhou: 1:1 para 1:2");

			incSystem.increment(ref coord, max);
			Assert.AreEqual("1:3", coord, "Incremento do Sistem falhou: 1:1 para 1:2");

			coord = "1:19";

			incSystem.increment(ref coord, max);
			Assert.AreEqual("1:20", coord, "Incremento do Sistem falhou: 1:19 para 1:20");

			incSystem.increment(ref coord, max);
			Assert.AreEqual("2:1", coord, "Incremento do Sistem falhou: 1:20 para 2:1");

			coord = "2:20";

			incSystem.increment(ref coord, max);
			Assert.AreEqual("3:1", coord, "Incremento do Sistem falhou: 2:20 para 3:1");


			coord = "3:20";

			incSystem.increment(ref coord, max);
			Assert.AreEqual("1:1", coord, "Incremento do Sistem falhou: 3:20 para 1:1");
		}


		[Test]
		public void DecrementSystem() {
			SystemUtil incSystem = new SystemUtil();

			string coord = "1:1";

			incSystem.decrement(ref coord, max);
			Assert.AreEqual("3:20", coord, "Incremento do Sistem falhou: 1:1 para 3:20");

			incSystem.decrement(ref coord, max);
			Assert.AreEqual("3:19", coord, "Incremento do Sistem falhou: 3:20 para 3:19");

			coord = "3:2";

			incSystem.decrement(ref coord, max);
			Assert.AreEqual("3:1", coord, "Incremento do Sistem falhou: 3:2 para 3:1");

			incSystem.decrement(ref coord, max);
			Assert.AreEqual("2:20", coord, "Incremento do Sistem falhou: 3:1 para 2:20");

			coord = "2:1";

			incSystem.decrement(ref coord, max);
			Assert.AreEqual("1:20", coord, "Incremento do Sistem falhou: 2:1 para 1:20");
		}

		[Test]
		public void SectorUtil() {
			SectorUtil incSector = new SectorUtil();

			string coord = "1:1:1";

			incSector.increment(ref coord, max);
			Assert.AreEqual("1:1:2", coord);

			incSector.increment(ref coord, max);
			Assert.AreEqual("1:1:3", coord);

			coord = "1:1:19";

			incSector.increment(ref coord, max);
			Assert.AreEqual("1:1:20", coord);

			incSector.increment(ref coord, max);
			Assert.AreEqual("1:2:1", coord);

			coord = "1:2:20";

			incSector.increment(ref coord, max);
			Assert.AreEqual("1:3:1", coord);

			for (int i = 4; i <= 20; ++i) {
				for (int j = 0; j < 20; ++j) {
					incSector.increment(ref coord, max);
				}
				Assert.AreEqual("1:" + i + ":1", coord);
			}

			for (int j = 0; j < 20; ++j) {
				incSector.increment(ref coord, max);
			}
			Assert.AreEqual("2:1:1", coord);

			coord = "3:20:20";
			incSector.increment(ref coord, max);
			Assert.AreEqual("1:1:1", coord);
		}


		[Test]
		public void DecrementSector() {
			SectorUtil incSector = new SectorUtil();

			string coord = "1:1:1";

			incSector.increment(ref coord, max);
			Assert.AreEqual("1:1:2", coord);

			incSector.increment(ref coord, max);
			Assert.AreEqual("1:1:3", coord);

			coord = "1:1:19";

			incSector.increment(ref coord, max);
			Assert.AreEqual("1:1:20", coord);

			incSector.increment(ref coord, max);
			Assert.AreEqual("1:2:1", coord);

			coord = "1:2:20";

			incSector.increment(ref coord, max);
			Assert.AreEqual("1:3:1", coord);

			for (int i = 4; i <= 20; ++i) {
				for (int j = 0; j < 20; ++j) {
					incSector.increment(ref coord, max);
				}
				Assert.AreEqual("1:" + i + ":1", coord);
			}

			for (int j = 0; j < 20; ++j) {
				incSector.increment(ref coord, max);
			}
			Assert.AreEqual("2:1:1", coord);

			coord = "2:20:19";

			incSector.increment(ref coord, max);
			Assert.AreEqual("2:20:20", coord);

			incSector.increment(ref coord, max);
			Assert.AreEqual("3:1:1", coord);

			coord = "3:20:20";

			incSector.increment(ref coord, max);
			Assert.AreEqual("1:1:1", coord);
		}
	}
}