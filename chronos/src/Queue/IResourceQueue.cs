// created on 5/22/04 at 8:14 a

using Chronos.Resources;
using Chronos.Info.Results;

namespace Chronos.Queue {

	public interface IResourceOueue {
	
		/// <summary>Indica o máximo de items que este queue suporta</summary>
		int QueueCapacity { get; }
	
		/// <summary>Agenda recursos para construcção</summary>
		/// <remarks>
		/// Se a Queue com tarefas, será colocada no fim. Se estiver vazia
		///	comecerá a ser construído o recurso no próximo turno.
		/// </remarks>
		void queue( string type, string resource );
		
		/// <summary>Agenda recursos para construcção</summary>
		/// <remarks>
		/// Se a Queue com tarefas, será colocada no fim. Se estiver vazia
		///	comecerá a ser construído o recurso no próximo turno. Este método não
		///	verifica se há recursos suficientes para criar este recurso. Usar o método
		///	canQueue para esse efeito. Por outro lado, o custo só será cobrado
		///	quando esta factory sair da queue e começar a ser desenvolvida.
		/// </remarks>
		void queue( string type, string resource, int quantity );
		
		/// <summary>Verifica se os custos de um recurso existem</summary>
		Result canQueue( string type, string resource, int quantity );
		
		/// <summary>Retorna um array com todas as tarefas em lista de espera</summary>
		/// <remarks>
		///	O array vem organizado por ordem de prioridade. O primeiro índice corresponde
		///	à próxima tarefa a ser executada. O array retornado é um novo objecto
		///	só com as referências. Não se altera o queue através dele.
		/// <remarks>
		QueueItem[] getQueueList( string type );
		
		/// <summary>Retorna o número de tarefas em lista de espera</summary>
		/// <remarks>Se há uma tarefa a ser executada, essa não é contabilizada</remarks>
		int queueCount( string type );
		
		/// <summary>Retorna a tarefa que está neste momento a ser realizada</summary>
		/// <remarks>Retorna null caso não haja nenhuma tarefa currente.</remarks>
		QueueItem current( string type );
		
		/// <summary>Cancela a tarefa que se está a realizar</summary>
		/// <remarks>
		///	Será tirada uma nova tarefa da queue no próximo turno. Os custos não são
		///	repostos.
		///	</remarks>
		void cancel( string type );
	
	};

}
