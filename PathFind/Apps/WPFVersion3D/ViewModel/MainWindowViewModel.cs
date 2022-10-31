using GalaSoft.MvvmLight.Messaging;
using GraphLib.Realizations.Graphs;
using System;
using WPFVersion3D.Interface;
using WPFVersion3D.Messages.PassValueMessages;
using WPFVersion3D.Model;

namespace WPFVersion3D.ViewModel
{
    public class MainWindowViewModel : ICache<Graph3D<Vertex3D>>, IDisposable
    {
        private readonly IMessenger messenger;

        public Graph3D<Vertex3D> Cache { get; private set; }

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