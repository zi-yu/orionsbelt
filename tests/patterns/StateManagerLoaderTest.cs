
//#define DEBUG_STATE_MANAGER_LOADER

using System.Collections;
using NUnit.Framework;
using DesignPatterns;
using Chronos.Utils;
using System.IO;

namespace DesignPatterns.Tests {

	[TestFixture]
	public class StateManagerLoaderTest {
		
		[SetUp]
		public void SetUp() {
		}

		[Test]
		public void Load() {
			StateManager manager = new StateManager();
			StateManagerLoader.LoadXml( StateMachineExample , manager );

			Assert.AreEqual(manager.AllStates.Count,11,"Devia ter 11 estados");
			
#if DEBUG_STATE_MANAGER_LOADER
			Log.log("----------- DEBUG_STATE_MANAGER_LOADER ----------------");
			foreach( string key in manager.AllStates.Keys ) {
				Log.log(key);
			}
#endif
			CheckState(manager.AllStates, "Candidatura");
			CheckState(manager.AllStates, "AnaliseCandidatura");
			CheckState(manager.AllStates, "CandidaturaArquivada");
			CheckState(manager.AllStates, "ProjectoActivo");
			CheckState(manager.AllStates, "ProjectoSuspenso");
			CheckState(manager.AllStates, "AnaliseTecnica");
			CheckState(manager.AllStates, "ProjectoArquivado");
			CheckState(manager.AllStates, "DespachoComissaoFinanciamento");
			CheckState(manager.AllStates, "FaseFinal");
			CheckState(manager.AllStates, "FasePagamento");
			CheckState(manager.AllStates, "ProjectoFechado");
		}
		
		private void CheckState( Hashtable all, string state )
		{
			Assert.IsNotNull( all[state], "Devia Haver este estado: " + state);
		}
		
		#region State Machine Example
		
		public static string StateMachineExample {
			get { return stateMachineExample; }
		}
		
		private static string stateMachineExample = @"<?xml version='1.0' ?>
<States start='Candidatura'>
	<State name='Candidatura' directSon='AnaliseCandidatura' />

	<State name='AnaliseCandidatura' parent='Candidatura'>
		<Event name='CandidaturaAprovada' newState='ProjectoActivo' />
		<Event name='CandidaturaRejeitada' newState='CandidaturaArquivada' />
	</State>

	<State name='CandidaturaArquivada' parent='Candidatura' />

	<State name='ProjectoActivo' directSon='AnaliseTecnica'>
		<Event name='SuspenderProjecto' newState='ProjectoSuspenso' saveCurrentState='true' />
	</State>

	<State name='ProjectoSuspenso'>
		<Event name='ActivarProjecto' newStateFromHistory='true' />
	</State>

	<State name='AnaliseTecnica' parent='ProjectoActivo'>
		<Event name='ProjectoAprovado' newState='DespachoComissaoFinanciamento' />
		<Event name='ProjectoRejeitado' newState='ProjectoArquivado' />
	</State>

	<State name='ProjectoArquivado' parent='ProjectoActivo'>
		<Event name='ReenquadrarProjecto' newState='Candidatura' />
	</State>

	<State name='DespachoComissaoFinanciamento' parent='ProjectoActivo'>
		<Event name='ProjectoAprovado' newState='FaseFinal' />
		<Event name='ProjectoRejeitado' newState='ProjectoArquivado' />
	</State>

	<State name='FaseFinal' directSon='FasePagamento' parent='ProjectoActivo'>
		<Event name='Reforco' newState='DespachoComissaoFinanciamento' />
	</State>

	<State name='FasePagamento' parent='FaseFinal'>
		<Event name='PagamentoConcluido' newState='ProjectoFechado' />
	</State>

	<State name='ProjectoFechado' parent='FaseFinal' />
</States>		
		";
		
		#endregion
	}
}