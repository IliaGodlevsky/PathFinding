using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages;

namespace Pathfinding.ConsoleApp.ViewModel.GraphCreateViewModels
{
    internal sealed class GraphParametresViewModel
    {
        private readonly IMessenger messenger;

        public int Width { get; set; }

        public int Length { get; set; }

        public int Obstacles { get; set; }

        public GraphParametresViewModel([KeyFilter(KeyFilters.ViewModels)]IMessenger messenger) 
        {
            this.messenger = messenger;
            messenger.Register<GraphParametresRequest>(this, OnGraphParametresRequestRecieved);
        }

        private void OnGraphParametresRequestRecieved(object recipient, GraphParametresRequest request)
        {
            request.Width = Width;
            request.Length = Length;
            request.Obstacles = Obstacles;
        }
    }
}
