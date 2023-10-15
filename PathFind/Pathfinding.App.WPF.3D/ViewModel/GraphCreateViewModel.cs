using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.WPF._3D.DependencyInjection;
using Pathfinding.App.WPF._3D.Infrastructure.Commands;
using Pathfinding.App.WPF._3D.Interface;
using Pathfinding.App.WPF._3D.Messages.PassValueMessages;
using Pathfinding.App.WPF._3D.Model;
using Pathfinding.GraphLib.Factory.Extensions;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Factory.Realizations.Layers;
using Pathfinding.Logging.Interface;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using Shared.Random;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Pathfinding.App.WPF._3D.ViewModel
{
    using GraphAssemble = IGraphAssemble<Vertex3D>;

    public class GraphCreatingViewModel : IViewModel, IDisposable
    {
        public event Action WindowClosed;

        private readonly IRandom random;
        private readonly INeighborhoodFactory neighborhoodFactory;
        private readonly IVertexCostFactory costFactory;
        private readonly ILog log;
        private readonly IMessenger messenger;

        public int Width { get; set; }

        public int Length { get; set; }

        public int Height { get; set; }

        public int ObstaclePercent { get; set; }

        private InclusiveValueRange<int> CostRange { get; set; } = new InclusiveValueRange<int>(4, 1);

        public ICommand CreateGraphCommand { get; }

        public ICommand CancelCreateGraphCommand { get; }

        public GraphAssemble SelectedGraphAssemble { get; set; }

        public GraphCreatingViewModel(IRandom random, INeighborhoodFactory neighborhoodFactory,
            IVertexCostFactory costFactory, ILog log)
        {
            this.messenger = DI.Container.Resolve<IMessenger>();
            this.random = random;
            this.neighborhoodFactory = neighborhoodFactory;
            this.costFactory = costFactory;
            this.log = log;
            CreateGraphCommand = new RelayCommand(ExecuteCreateGraphCommand, CanExecuteCreateGraphCommand);
            CancelCreateGraphCommand = new RelayCommand(obj => CloseWindow());
        }

        private async void CreateGraph()
        {
            try
            {
                var layers = GetLayers();
                var graph = await SelectedGraphAssemble.AssembleGraphAsync(layers, Width, Length, Height);
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

        private IReadOnlyCollection<ILayer> GetLayers()
        {
            return new ILayer[]
            {
                new ObstacleLayer(random, ObstaclePercent),
                new NeighborhoodLayer(neighborhoodFactory),
                new VertexCostLayer(costFactory, CostRange, random)
            };
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