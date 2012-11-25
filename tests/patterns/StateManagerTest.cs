using NUnit.Framework;
using DesignPatterns;

namespace DesignPatterns.Tests {

	[TestFixture]
	public class StateManagerTest {

		private StateManager manager;
		
		[SetUp]
		public void SetUp() {
			manager = new StateManager();
			StateManagerLoader.LoadXml( StateManagerLoaderTest.StateMachineExample, manager );
		}

		[Test]
		public void Candidatura() {
			Assert.AreEqual(manager.Current.Name,"AnaliseCandidatura","O primeiro estado devia ser Anlise Candidatura");
		}

		[Test]
		public void AnaliseCandidaturaAprovada() {
			manager.ProcessEvent( "CandidaturaAprovada" );
            Assert.AreEqual(manager.Current.Name,"AnaliseTecnica","O estado devia ser Projecto Activo");
		}

		[Test]
		public void AnaliseCandidaturaRejeitada() {
			manager.ProcessEvent( "CandidaturaRejeitada" );
			Assert.AreEqual(manager.Current.Name,"CandidaturaArquivada","O estado devia ser Candidatura Arquivada");
		}

		[Test]
		public void AnaliseTecnicaAprovado() {
			manager.ProcessEvent( "CandidaturaAprovada" );
			manager.ProcessEvent( "ProjectoAprovado" );
			Assert.AreEqual(manager.Current.Name,"DespachoComissaoFinanciamento","O estado devia ser Despacho Comisso Financiamento");
		}

		[Test]
		public void AnaliseTecnicaRejeitaado() {
			manager.ProcessEvent( "CandidaturaAprovada" );
			manager.ProcessEvent( "ProjectoRejeitado" );
			Assert.AreEqual(manager.Current.Name,"ProjectoArquivado","O estado devia ser Projecto Arquivado");
		}

		[Test]
		public void DespachoComissaoFinanciamentoAprovado() {
			manager.ProcessEvent( "CandidaturaAprovada" );
			manager.ProcessEvent( "ProjectoAprovado" );
			manager.ProcessEvent( "ProjectoAprovado" );
			Assert.AreEqual(manager.Current.Name,"FasePagamento","O estado devia ser Fase Pagamento");
		}

		[Test]
		public void DespachoComissaoFinanciamentoRejeitado() {
			manager.ProcessEvent( "CandidaturaAprovada" );
			manager.ProcessEvent( "ProjectoAprovado" );
			manager.ProcessEvent( "ProjectoRejeitado" );
			Assert.AreEqual(manager.Current.Name,"ProjectoArquivado","O estado devia ser Projecto Arquivado");
		}

		[Test]
		public void FasePagamento() {
			manager.ProcessEvent( "CandidaturaAprovada" );
			manager.ProcessEvent( "ProjectoAprovado" );
			manager.ProcessEvent( "ProjectoAprovado" );
			manager.ProcessEvent( "PagamentoConcluido" );

			Assert.AreEqual(manager.Current.Name,"ProjectoFechado","O estado devia ser ProjectoFechado");

			manager.ProcessEvent( "Reforco" );

			Assert.AreEqual(manager.Current.Name,"DespachoComissaoFinanciamento","Aps reforo o estado devia ser DespachoComissoFinanciamento");
			/**/
		}

		[Test]
		public void TestarReforcoNaFaseDePagamento() {
			manager.ProcessEvent( "CandidaturaAprovada" );
			manager.ProcessEvent( "ProjectoAprovado" );
			manager.ProcessEvent( "ProjectoAprovado" );
			manager.ProcessEvent( "Reforco" );

			Assert.AreEqual(manager.Current.Name,"DespachoComissaoFinanciamento","Aps reforo o estado devia ser DespachoComissoFinanciamento");
		}

		[Test]
		public void SuspenderReactivarProjecto() {
			manager.ProcessEvent( "CandidaturaAprovada" );
			manager.ProcessEvent( "ProjectoAprovado" );
			State s = manager.Current;
			
			manager.ProcessEvent( "SuspenderProjecto" );
			Assert.AreEqual(manager.Current.Name,"ProjectoSuspenso","O estado devia ser Projecto Suspenso");

			manager.ProcessEvent( "ActivarProjecto" );
			Assert.AreEqual(manager.Current.Name,s.Name,"O estado devia ser"+s.Name);
		}
	}
}