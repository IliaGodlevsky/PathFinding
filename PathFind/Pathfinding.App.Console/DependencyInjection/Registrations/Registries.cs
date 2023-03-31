using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.History.Interface;
using Pathfinding.AlgorithmLib.History;
using Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.MenuItems;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.App.Console.MenuItems.EditorMenuItems;
using Pathfinding.App.Console.MenuItems.GraphMenuItems;
using Pathfinding.App.Console.Model.VertexActions;
using System;
using Pathfinding.App.Console.MenuItems.MainMenuItems;
using Pathfinding.App.Console.MenuItems.ColorMenuItems;
using Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems;
using Pathfinding.App.Console.MenuItems.PathfindingHistoryMenuItems;
using Pathfinding.App.Console.Model.PathfindingActions;
using Pathfinding.App.Console.Model.InProcessActions;
using Pathfinding.GraphLib.Core.Modules.Commands;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Smoothing.Interface;
using Pathfinding.GraphLib.Smoothing.Realizations.MeanCosts;
using Pathfinding.Logging.Interface;
using Pathfinding.Logging.Loggers;
using Shared.Executable;
using Shared.Random.Realizations;
using Shared.Random;
using Pathfinding.GraphLib.Core.Modules;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Factory.Realizations.CoordinateFactories;
using Pathfinding.GraphLib.Factory.Realizations.GraphAssembles;
using Pathfinding.GraphLib.Factory.Realizations.GraphFactories;
using Pathfinding.GraphLib.Factory.Realizations.NeighborhoodFactories;
using Pathfinding.GraphLib.Factory.Realizations;
using Pathfinding.VisualizationLib.Core.Interface;
using Pathfinding.App.Console.Model.Visualizations;
using GraphLib.Serialization.Serializers.Decorators;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers.Decorators;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers;
using Pathfinding.App.Console.MenuItems.PathfindingVisualizationMenuItems;
using Pathfinding.AlgorithmLib.Factory;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Realizations.Heuristics;
using Pathfinding.AlgorithmLib.Core.Realizations.StepRules;
using Pathfinding.App.Console.ValueInput.UserInput;
using Pathfinding.App.Console.MenuItems.PathfindingStatisticsMenuItems;
using Pathfinding.App.Console.MenuItems.PathfindingRangeMenuItems;

using static Pathfinding.App.Console.DependencyInjection.PathfindingUnits;
using static Pathfinding.App.Console.DependencyInjection.RegistrationConstants;

namespace Pathfinding.App.Console.DependencyInjection.Registrations
{
    using AlgorithmFactory = IAlgorithmFactory<PathfindingProcess>;
    using Command = IPathfindingRangeCommand<Vertex>;
    using GraphSerializer = IGraphSerializer<Graph2D<Vertex>, Vertex>;

    internal static class Registries
    {
        public static readonly IRegistry Initial = new InitialRegistration();
        public static readonly IRegistry UserInput = new UserInputRegistration();
        public static readonly IRegistry GraphEditor = new GraphEditorRegistration();
        public static readonly IRegistry ColorEditor = new ColorEditorRegistration();
        public static readonly IRegistry TransitVertices = new TransitVerticesRegistration();
        public static readonly IRegistry WaveAlgorithms = new WaveAlgorithmsRegistration();
        public static readonly IRegistry GreedyAlgorithms = new GreedyAlgorithmsRegistration();
        public static readonly IRegistry BreadthAlgorithms = new BreadthAlgorithmsRegistration();
        public static readonly IRegistry PathfindingVisualization = new PathfindingVisualizationRegistration();
        public static readonly IRegistry PathfindingHistory = new PathfindingHistoryRegistration();
        public static readonly IRegistry VisualizationControl = new VisualizationControlRegistration();
        public static readonly IRegistry PathfindingStatistics = new PathfindingStatisticsRegistration();

        private sealed class InitialRegistration : IRegistry
        {
            public void Configure(ContainerBuilder builder)
            {
                builder.RegisterType<Messenger>().As<IMessenger>().SingleInstance().RegisterRecievers();
                builder.RegisterType<Screen>().AsSelf().SingleInstance().AutoActivate();

                builder.RegisterTypes(AllUnits).SingleInstance().WithMetadata(UnitTypeKey, type => type).AsSelf()
                    .AsImplementedInterfaces().AutoActivate().ConfigurePipeline(p => p.Use(new UnitResolveMiddleware(UnitTypeKey)));

                builder.RegisterType<History<PathfindingHistoryVolume>>()
                    .As<IHistoryRepository<IHistoryVolume<ICoordinate>>>().SingleInstance();

                builder.RegisterType<ExitMenuItem>().Keyed(typeof(IMenuItem), WithoutMain).SingleInstance();

                builder.RegisterType<MainUnitMenuItem>().AsSelf().InstancePerDependency();

                builder.RegisterType<AnswerExitMenuItem>().Keyed<IMenuItem>(Main).SingleInstance();

                builder.RegisterType<GraphCreateMenuItem>().Keyed<IMenuItem>(Main).SingleInstance();
                builder.RegisterType<PathfindingProcessMenuItem>().Keyed<IConditionedMenuItem>(Main).As<ICanRecieveMessage>().SingleInstance();

                builder.RegisterType<ApplyStatisticsMenuItem>().Keyed<IMenuItem>(Statistics).SingleInstance();

                builder.RegisterType<TotalVertexVisualization>().As<ITotalVisualization<Vertex>>().As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<PathVisualization>().As<IPathVisualization<Vertex>>().As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<RangeVisualization>().As<IRangeVisualization<Vertex>>().SingleInstance();

                builder.RegisterType<PathfindingRangeMenuItem>().Keyed<IMenuItem>(Process).SingleInstance();
                builder.RegisterType<PathfindingAlgorithmMenuItem>().Keyed<IConditionedMenuItem>(Process)
                    .SingleInstance().ConfigurePipeline(p => p.Use(new PathfindingItemResolveMiddleware(Group, Order)));
                builder.RegisterType<ClearColorsMenuItem>().Keyed<IConditionedMenuItem>(Process).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<ClearGraphMenuItem>().Keyed<IConditionedMenuItem>(Process).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<ClearPathfindingRangeMenuItem>().Keyed<IConditionedMenuItem>(Range).SingleInstance();
                builder.RegisterType<EnterPathfindingRangeMenuItem>().Keyed<IConditionedMenuItem>(Range).As<ICanRecieveMessage>().SingleInstance()
                    .ConfigurePipeline(p => p.Use(new VertexActionResolveMiddlewear(PathfindingRange)));
                builder.RegisterType<IncludeInRangeAction>().Keyed<IVertexAction>(PathfindingRange).WithMetadata(PathfindingRange, ConsoleKey.Enter);
                builder.RegisterType<ExcludeFromRangeAction>().Keyed<IVertexAction>(PathfindingRange).WithMetadata(PathfindingRange, ConsoleKey.X);

                builder.RegisterType<AssembleGraphMenuItem>().Keyed<IConditionedMenuItem>(Graph).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<ResizeGraphMenuItem>().Keyed<IConditionedMenuItem>(Graph).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<EnterCostRangeMenuItem>().Keyed<IMenuItem>(Graph).SingleInstance();
                builder.RegisterType<EnterGraphAssembleMenuItem>().Keyed<IMenuItem>(Graph).SingleInstance();
                builder.RegisterType<EnterGraphParametresMenuItem>().Keyed<IMenuItem>(Graph).SingleInstance();
                builder.RegisterType<EnterObstaclePercentMenuItem>().Keyed<IMenuItem>(Graph).SingleInstance();
                builder.RegisterType<LoadGraphMenuItem>().Keyed<IMenuItem>(Graph).SingleInstance();
                builder.RegisterType<SaveGraphMenuItem>().Keyed<IConditionedMenuItem>(Graph).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<RecieveGraphMenuItem>().Keyed<IMenuItem>(Graph).SingleInstance();
                builder.RegisterType<SendGraphMenuItem>().Keyed<IConditionedMenuItem>(Graph).As<ICanRecieveMessage>().SingleInstance();

                builder.RegisterType<ViewFactory>().As<IViewFactory>().SingleInstance();

                builder.RegisterType<FileLog>().As<ILog>().SingleInstance();
                builder.RegisterType<ConsoleLog>().As<ILog>().SingleInstance();
                builder.RegisterType<DebugLog>().As<ILog>().SingleInstance();
                builder.RegisterComposite<Logs, ILog>().SingleInstance();

                builder.RegisterComposite<CompositeUndo, IUndo>().SingleInstance();
                builder.RegisterType<CryptoRandom>().As<IRandom>().SingleInstance();
                builder.RegisterType<MeanCost>().As<IMeanCost>().SingleInstance();

                builder.RegisterType<PathfindingRange<Vertex>>().As<IPathfindingRange<Vertex>>().SingleInstance();
                builder.RegisterDecorator<VisualPathfindingRange<Vertex>, IPathfindingRange<Vertex>>();
                builder.RegisterType<PathfindingRangeBuilder<Vertex>>().As<IPathfindingRangeBuilder<Vertex>>().As<IUndo>()
                    .SingleInstance().ConfigurePipeline(p => p.Use(new RangeBuilderResolveMiddlewear(Order)));
                builder.RegisterType<IncludeSourceVertex<Vertex>>().Keyed<Command>(IncludeCommand).WithMetadata(Order, 2).SingleInstance();
                builder.RegisterType<IncludeTargetVertex<Vertex>>().Keyed<Command>(IncludeCommand).WithMetadata(Order, 4).SingleInstance();
                builder.RegisterType<ReplaceIsolatedSourceVertex<Vertex>>().Keyed<Command>(IncludeCommand).WithMetadata(Order, 3).SingleInstance();
                builder.RegisterType<ReplaceIsolatedTargetVertex<Vertex>>().Keyed<Command>(IncludeCommand).WithMetadata(Order, 5).SingleInstance();
                builder.RegisterType<ExcludeSourceVertex<Vertex>>().Keyed<Command>(ExcludeCommand).WithMetadata(Order, 1).SingleInstance();
                builder.RegisterType<ExcludeTargetVertex<Vertex>>().Keyed<Command>(ExcludeCommand).WithMetadata(Order, 2).SingleInstance();

                builder.RegisterType<GraphAssemble<Graph2D<Vertex>, Vertex>>().As<IGraphAssemble<Graph2D<Vertex>, Vertex>>().SingleInstance();
                builder.RegisterType<CostFactory>().As<IVertexCostFactory>().SingleInstance();
                builder.RegisterType<VertexFactory>().As<IVertexFactory<Vertex>>().SingleInstance();
                builder.RegisterType<GraphFieldFactory>().As<IGraphFieldFactory<Graph2D<Vertex>, Vertex, GraphField>>().SingleInstance();
                builder.RegisterType<Coordinate2DFactory>().As<ICoordinateFactory>().SingleInstance();
                builder.RegisterType<Graph2DFactory<Vertex>>().As<IGraphFactory<Graph2D<Vertex>, Vertex>>().SingleInstance();
                builder.RegisterDecorator<Graph2DWrapFactory, IGraphFactory<Graph2D<Vertex>, Vertex>>();
                builder.RegisterType<MooreNeighborhoodFactory>().As<INeighborhoodFactory>().SingleInstance();

                builder.RegisterType<PathInput>().As<IPathInput>().SingleInstance();
                builder.RegisterType<BinaryGraphSerializer<Graph2D<Vertex>, Vertex>>().As<GraphSerializer>().SingleInstance();
                builder.RegisterDecorator<CompressGraphSerializer<Graph2D<Vertex>, Vertex>, GraphSerializer>();
                builder.RegisterDecorator<CryptoGraphSerializer<Graph2D<Vertex>, Vertex>, GraphSerializer>();
                builder.RegisterType<VertexFromInfoFactory>().As<IVertexFromInfoFactory<Vertex>>().SingleInstance();

                builder.RegisterType<DefaultStepRule>().As<IStepRule>().SingleInstance();
                builder.RegisterType<EuclidianDistance>().As<IHeuristic>().SingleInstance();
            }
        }

        private sealed class GraphEditorRegistration : IRegistry
        {
            public void Configure(ContainerBuilder builder)
            {
                builder.RegisterType<EditorUnitMenuItem>().Keyed<IConditionedMenuItem>(Main).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<ReverseVertexMenuItem>().Keyed<IConditionedMenuItem>(Editor).As<ICanRecieveMessage>().SingleInstance()
                    .ConfigurePipeline(p => p.Use(new VertexActionResolveMiddlewear(Reverse)));
                builder.RegisterType<ReverseVertexAction>().Keyed<IVertexAction>(Reverse).WithMetadata(Reverse, ConsoleKey.Enter);
                builder.RegisterType<SmoothGraphMenuItem>().Keyed<IConditionedMenuItem>(Editor).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<ChangeCostMenuItem>().Keyed<IConditionedMenuItem>(Editor).SingleInstance()
                    .As<ICanRecieveMessage>().ConfigurePipeline(p => p.Use(new VertexActionResolveMiddlewear(ChangeCost)));
                builder.RegisterType<IncreaseCostAction>().Keyed<IVertexAction>(ChangeCost).WithMetadata(ChangeCost, ConsoleKey.UpArrow);
                builder.RegisterType<DecreaseCostAction>().Keyed<IVertexAction>(ChangeCost).WithMetadata(ChangeCost, ConsoleKey.DownArrow);
            }
        }

        private sealed class ColorEditorRegistration : IRegistry
        {
            public void Configure(ContainerBuilder builder)
            {
                builder.RegisterType<ColorsUnitMenuItem>().Keyed<IMenuItem>(Main).SingleInstance();
                builder.RegisterType<GraphColorsMenuItem>().Keyed<IMenuItem>(Colors).SingleInstance();
                builder.RegisterType<PathColorsMenuItem>().Keyed<IMenuItem>(Colors).SingleInstance();
                builder.RegisterType<RangeColorsMenuItem>().Keyed<IMenuItem>(Colors).SingleInstance();
                builder.RegisterType<PathfindingColorsMenuItem>().Keyed<IMenuItem>(Colors).SingleInstance();
            }
        }

        private sealed class TransitVerticesRegistration : IRegistry
        {
            public void Configure(ContainerBuilder builder)
            {
                builder.RegisterType<ExcludeTransitVertex<Vertex>>().Keyed<Command>(ExcludeCommand).WithMetadata(Order, 3).SingleInstance();
                builder.RegisterType<ReplaceTransitVerticesModule<Vertex>>().AsSelf().As<IUndo>().SingleInstance();
                builder.RegisterType<MarkTransitToReplaceAction>().Keyed<IVertexAction>(PathfindingRange).WithMetadata(PathfindingRange, ConsoleKey.R);
                builder.RegisterType<ReplaceTransitVertexAction>().Keyed<IVertexAction>(PathfindingRange).WithMetadata(PathfindingRange, ConsoleKey.P);
                builder.RegisterType<ReplaceTransitIsolatedVertex<Vertex>>().Keyed<Command>(IncludeCommand).WithMetadata(Order, 1).SingleInstance();
                builder.RegisterType<IncludeTransitVertex<Vertex>>().Keyed<Command>(IncludeCommand).WithMetadata(Order, 6).SingleInstance();
            }
        }

        private sealed class BreadthAlgorithmsRegistration : IRegistry
        {
            public void Configure(ContainerBuilder builder)
            {
                builder.RegisterType<AStarLeeAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance().WithMetadata(Group, BreadthGroup).WithMetadata(Order, 2);
                builder.RegisterType<LeeAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance().WithMetadata(Group, BreadthGroup).WithMetadata(Order, 1);
            }
        }

        private sealed class WaveAlgorithmsRegistration : IRegistry
        {
            public void Configure(ContainerBuilder builder)
            {
                builder.RegisterType<DijkstraAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance().WithMetadata(Group, WaveGroup).WithMetadata(Order, 1);
                builder.RegisterType<RandomAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance().WithMetadata(Group, WaveGroup).WithMetadata(Order, 4);
                builder.RegisterType<AStarAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance().WithMetadata(Group, WaveGroup).WithMetadata(Order, 2);
                builder.RegisterType<IDAStarAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance().WithMetadata(Group, WaveGroup).WithMetadata(Order, 3);
            }
        }

        private sealed class GreedyAlgorithmsRegistration : IRegistry
        {
            public void Configure(ContainerBuilder builder)
            {
                builder.RegisterType<DistanceFirstAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance().WithMetadata(Group, GreedGroup).WithMetadata(Order, 1);
                builder.RegisterType<DepthFirstAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance().WithMetadata(Group, GreedGroup).WithMetadata(Order, 2);
                builder.RegisterType<HeuristicCostGreedyAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance().WithMetadata(Group, GreedGroup).WithMetadata(Order, 3);
                builder.RegisterType<CostGreedyAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance().WithMetadata(Group, GreedGroup).WithMetadata(Order, 4);
            }
        }

        private sealed class PathfindingVisualizationRegistration : IRegistry
        {
            public void Configure(ContainerBuilder builder)
            {
                builder.RegisterType<VisualizationMenuItem>().Keyed<IMenuItem>(Process).SingleInstance();
                builder.RegisterType<ApplyVisualizationMenuItem>().Keyed<IMenuItem>(Visual).SingleInstance();
                builder.RegisterType<EnterAnimationDelayMenuItem>().Keyed<IConditionedMenuItem>(Visual).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<PathfindingVisualization>().As<IPathfindingVisualization<Vertex>>().SingleInstance();
            }
        }

        private sealed class PathfindingHistoryRegistration : IRegistry
        {
            public void Configure(ContainerBuilder builder)
            {
                builder.RegisterType<HistoryMenuItem>().Keyed<IMenuItem>(Process).SingleInstance();
                builder.RegisterType<ApplyHistoryMenuItem>().Keyed<IMenuItem>(History).SingleInstance();
                builder.RegisterType<ClearHistoryMenuItem>().Keyed<IConditionedMenuItem>(History).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<ShowHistoryMenuItem>().Keyed<IConditionedMenuItem>(History).As<ICanRecieveMessage>().SingleInstance();
            }
        }

        private sealed class VisualizationControlRegistration : IRegistry
        {
            public void Configure(ContainerBuilder builder)
            {
                builder.RegisterType<SpeedUpAnimation>().As<IAnimationSpeedAction>().WithMetadata(Key, ConsoleKey.UpArrow);
                builder.RegisterType<SlowDownAnimation>().As<IAnimationSpeedAction>().WithMetadata(Key, ConsoleKey.DownArrow);
                builder.RegisterType<ResumeAlgorithm>().As<IPathfindingAction>().WithMetadata(Key, ConsoleKey.Enter);
                builder.RegisterType<PauseAlgorithm>().As<IPathfindingAction>().WithMetadata(Key, ConsoleKey.P);
                builder.RegisterType<InterruptAlgorithm>().As<IPathfindingAction>().WithMetadata(Key, ConsoleKey.Escape);
                builder.RegisterType<PathfindingStepByStep>().As<IPathfindingAction>().WithMetadata(Key, ConsoleKey.W);
            }
        }

        private sealed class PathfindingStatisticsRegistration : IRegistry
        {
            public void Configure(ContainerBuilder builder)
            {
                builder.RegisterType<StatisticsMenuItem>().Keyed<IMenuItem>(Process).SingleInstance();
                builder.RegisterType<ApplyStatisticsMenuItem>().Keyed<IMenuItem>(PathfindingStatistics).SingleInstance();
            }
        }

        private sealed class UserInputRegistration : IRegistry
        {
            public void Configure(ContainerBuilder builder)
            {
                builder.RegisterType<ConsoleUserAnswerInput>().As<IInput<Answer>>().SingleInstance();
                builder.RegisterType<ConsoleUserIntInput>().As<IInput<int>>().SingleInstance();
                builder.RegisterType<ConsoleUserStringInput>().As<IInput<string>>().SingleInstance();
                builder.RegisterType<ConsoleUserKeyInput>().As<IInput<ConsoleKey>>().SingleInstance();
                builder.RegisterType<ConsoleUserTimeSpanInput>().As<IInput<TimeSpan>>().SingleInstance();
            }
        }
    }
}
