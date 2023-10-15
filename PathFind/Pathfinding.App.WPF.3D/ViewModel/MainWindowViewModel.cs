using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.WPF._3D.Interface;
using Pathfinding.App.WPF._3D.Messages.PassValueMessages;
using Pathfinding.App.WPF._3D.Model;
using Pathfinding.GraphLib.Core.Interface;
using System;

namespace Pathfinding.App.WPF._3D.ViewModel
{
    public class MainWindowViewModel : ICache<IGraph<Vertex3D>>, IDisposable
    {
        private readonly IMessenger messenger;

        public IGraph<Vertex3D> Cache { get; private set; }

        public MainWindowViewModel(IMessenger messenger)
        {
            this.messenger = messenger;
            messenger.Register<GraphCreatedMessage>(this, SetGraph);
        }

        private void SetGraph(GraphCreatedMessage message)
        {
            Cache = message.Value;
        }

        public void Dispose()
        {
            messenger.Unregister(this);
        }
    }
}