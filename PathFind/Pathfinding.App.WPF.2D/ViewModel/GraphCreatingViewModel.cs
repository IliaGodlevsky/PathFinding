using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.WPF._2D.Infrastructure;
using Pathfinding.App.WPF._2D.Interface;
using Pathfinding.App.WPF._2D.Messages.DataMessages;
using Pathfinding.App.WPF._2D.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Extensions;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.Logging.Interface;
using Shared.Extensions;
using Shared.Primitives.Extensions;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using WPFVersion.DependencyInjection;

namespace Pathfinding.App.WPF._2D.ViewModel
{
    public class GraphCreatingViewModel : IViewModel, IDisposable
    {
        public event Action WindowClosed;

        private readonly ILog log;
        private readonly IMessenger messenger;

        public ICommand ConfirmCreateGraphCommand { get; }

        public ICommand CancelCreateGraphCommand { get; }

        public int Width { get; set; }

        public int Length { get; set; }

        public int ObstaclePercent { get; set; }

        public IReadOnlyList<IGraphAssemble<Graph2D<Vertex>, Vertex>> GraphAssembles { get; }

        public IGraphAssemble<Graph2D<Vertex>, Vertex> SelectedGraphAssemble { get; set; }

        public GraphCreatingViewModel(ILog log, IEnumerable<IGraphAssemble<Graph2D<Vertex>, Vertex>> graphAssembles)
        {
            this.log = log;
            GraphAssembles = graphAssembles.ToReadOnly();
            messenger = DI.Container.Resolve<IMessenger>();
            ConfirmCreateGraphCommand = new RelayCommand(ExecuteConfirmCreateGraphCommand, CanExecuteConfirmCreateGraphCommand);
            CancelCreateGraphCommand = new RelayCommand(ExecuteCloseWindowCommand);
        }

        private async void CreateGraph()
        {
            try
            {
                var graph = await SelectedGraphAssemble.AssembleGraphAsync(ObstaclePercent, Width, Length);
                messenger.Send(new GraphCreatedMessage(graph));
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private void ExecuteConfirmCreateGraphCommand(object param)
        {
            CreateGraph();
            ExecuteCloseWindowCommand(null);
        }

        private void ExecuteCloseWindowCommand(object param)
        {
            WindowClosed?.Invoke();
        }

        private bool CanExecuteConfirmCreateGraphCommand(object sender)
        {
            return SelectedGraphAssemble != null
                && Constants.GraphWidthValueRange.Contains(Width)
                && Constants.GraphLengthValueRange.Contains(Length);
        }

        public void Dispose()
        {
            WindowClosed = null;
        }
    }
}
