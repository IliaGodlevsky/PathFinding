using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Model;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class RunViewModel : ReactiveObject
    {
        private readonly IMessenger messenger;

        public ObservableCollection<VertexModel> GraphState { get; private set; } = new();

        public ObservableCollection<VertexModel> Range { get; private set; } = new();

        public ObservableCollection<VertexModel> Path { get; private set; } = new();

        public ObservableCollection<(VertexModel Visited, ObservableCollection<VertexModel>)> SubAlgorithms { get; private set; } = new();

        public RunViewModel(IMessenger messenger)
        {
            this.messenger = messenger;
        }
    }
}
