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
using System.Windows.Input;
using ValueRange.Extensions;
using WPFVersion.DependencyInjection;
using WPFVersion.Infrastructure;
using WPFVersion.Messages.DataMessages;
using WPFVersion.Model;

namespace WPFVersion.ViewModel
{
    public class GraphCreatingViewModel : GraphCreatingModel<Graph2D<Vertex>, Vertex>, IViewModel, IDisposable
    {
        public event Action WindowClosed;

        public ICommand ConfirmCreateGraphCommand { get; }

        public ICommand CancelCreateGraphCommand { get; }

        public GraphCreatingViewModel(IEnumerable<IGraphAssemble<Graph2D<Vertex>, Vertex>> graphAssembles, ILog log)
            : base(log, graphAssembles)
        {
            messenger = DI.Container.Resolve<IMessenger>();
            ConfirmCreateGraphCommand = new RelayCommand(ExecuteConfirmCreateGraphCommand,
                CanExecuteConfirmCreateGraphCommand);
            CancelCreateGraphCommand = new RelayCommand(ExecuteCloseWindowCommand);
        }

        public override async void CreateGraph()
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

        private readonly IMessenger messenger;
    }
}
