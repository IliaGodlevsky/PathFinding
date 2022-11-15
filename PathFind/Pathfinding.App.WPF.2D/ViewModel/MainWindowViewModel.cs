using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.WPF._2D.Infrastructure.EventArguments;
using Pathfinding.App.WPF._2D.Infrastructure.EventHandlers;
using Pathfinding.App.WPF._2D.Interface;
using Pathfinding.App.WPF._2D.Messages.ActionMessages;
using Pathfinding.App.WPF._2D.Messages.DataMessages;
using Pathfinding.App.WPF._2D.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using System;
using System.Windows.Input;
using WPFVersion.DependencyInjection;

namespace Pathfinding.App.WPF._2D.ViewModel
{
    public class MainWindowViewModel : ICache<Graph2D<Vertex>>, IDisposable
    {
        internal event GraphCreatedEventHandler GraphCreated;

        private readonly IMessenger messenger;

        private bool IsEditorModeEnabled { get; set; } = false;

        public Graph2D<Vertex> Cached { get; private set; } = Graph2D<Vertex>.Empty;

        public MainWindowViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<GraphCreatedMessage>(this, SetGraph);
        }

        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl && IsEditorModeEnabled)
            {
                IsEditorModeEnabled = false;
                messenger.Send(new StopEditorModeMessage());
            }
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl && !IsEditorModeEnabled)
            {
                IsEditorModeEnabled = true;
                messenger.Send(new StartEditorModeMessage());
            }
        }

        public void Dispose()
        {
            messenger.Unregister(this);
        }

        private void SetGraph(GraphCreatedMessage message)
        {
            Cached = message.Graph;
            GraphCreated?.Invoke(this, new GraphCreatedEventArgs(Cached));
        }
    }
}