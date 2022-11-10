using Autofac;
using Common.Interface;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Extensions;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Graphs;
using GraphLib.ViewModel;
using Pathfinding.Logging.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ValueRange.Extensions;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Infrastructure.Commands;
using WPFVersion3D.Messages.PassValueMessages;
using WPFVersion3D.Model;

namespace WPFVersion3D.ViewModel
{
    public class GraphCreatingViewModel : GraphCreatingModel<Graph3D<Vertex3D>, Vertex3D>, IViewModel, IDisposable
    {
        public event Action WindowClosed;

        private readonly IMessenger messenger;

        public int Height { get; set; }

        public ICommand CreateGraphCommand { get; }

        public ICommand CancelCreateGraphCommand { get; }

        public GraphCreatingViewModel(IEnumerable<IGraphAssemble<Graph3D<Vertex3D>, Vertex3D>> graphAssembles, ILog log)
            : base(log, graphAssembles)
        {
            messenger = DI.Container.Resolve<IMessenger>();
            SelectedGraphAssemble = graphAssembles.FirstOrDefault();
            CreateGraphCommand = new RelayCommand(ExecuteCreateGraphCommand, CanExecuteCreateGraphCommand);
            CancelCreateGraphCommand = new RelayCommand(obj => CloseWindow());
        }

        public override async void CreateGraph()
        {
            try
            {
                var graph = await SelectedGraphAssemble.AssembleGraphAsync(ObstaclePercent, Width, Length, Height);
                messenger.Send(new GraphCreatedMessage(graph));
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public void Dispose()
        {
            WindowClosed = null;
        }

        private void ExecuteCreateGraphCommand(object param)
        {
            CreateGraph();
            CloseWindow();
        }

        private void CloseWindow()
        {
            WindowClosed?.Invoke();
        }

        private bool CanExecuteCreateGraphCommand(object sender)
        {
            return SelectedGraphAssemble != null
                && Constants.GraphWidthValueRange.Contains(Width)
                && Constants.GraphLengthValueRange.Contains(Length)
                && Constants.GraphHeightValueRange.Contains(Height);
        }
    }
}