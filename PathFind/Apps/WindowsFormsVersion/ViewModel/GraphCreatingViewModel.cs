using Autofac;
using Common.Interface;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Extensions;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Graphs;
using GraphLib.ViewModel;
using Logging.Interface;
using System;
using System.Collections.Generic;
using ValueRange.Extensions;
using WindowsFormsVersion.DependencyInjection;
using WindowsFormsVersion.Enums;
using WindowsFormsVersion.Messeges;
using WindowsFormsVersion.Model;

namespace WindowsFormsVersion.ViewModel
{
    internal class GraphCreatingViewModel : GraphCreatingModel<Graph2D<Vertex>, Vertex>, IViewModel, IDisposable
    {
        public event Action WindowClosed;

        public GraphCreatingViewModel(IEnumerable<IGraphAssemble<Graph2D<Vertex>, Vertex>> graphAssembles, ILog log)
            : base(log, graphAssembles)
        {
            messenger = DI.Container.Resolve<IMessenger>();
        }

        public override async void CreateGraph()
        {
            try
            {
                var graph = await SelectedGraphAssemble.AssembleGraphAsync(ObstaclePercent, Width, Length);
                var message = new GraphCreatedMessage(graph);
                messenger.Send(message, MessageTokens.MainModel);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public void CreateGraph(object sender, EventArgs e)
        {
            if (CanExecuteConfirmGraphAssembleChoice())
            {
                CreateGraph();
                WindowClosed?.Invoke();
            }
        }

        public void CancelCreateGraph(object sender, EventArgs e)
        {
            WindowClosed?.Invoke();
        }

        private bool CanExecuteConfirmGraphAssembleChoice()
        {
            return SelectedGraphAssemble != null
                && Constants.GraphWidthValueRange.Contains(Width)
                && Constants.GraphLengthValueRange.Contains(Length);
        }

        public void Dispose()
        {
            WindowClosed = null;
        }

        private readonly IMessenger messenger;
    }
}
