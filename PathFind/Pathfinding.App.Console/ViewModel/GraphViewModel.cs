using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface;
using ReactiveUI;

namespace Pathfinding.App.Console.ViewModel
{
    public class GraphViewModel : ReactiveObject, ICanReceiveMessage, IEntity<int>
    {
        private IGraph<VertexViewModel> graph;

        public int Id { get; set; }

        public IGraph<VertexViewModel> Graph 
        {
            get => graph;
            set => this.RaiseAndSetIfChanged(ref graph, value); 
        }

        public GraphViewModel() 
        {
            
        }

        public void RegisterHandlers(IMessenger messenger)
        {
            throw new System.NotImplementedException();
        }

        private void SetGraph()
        {

        }
    }
}
