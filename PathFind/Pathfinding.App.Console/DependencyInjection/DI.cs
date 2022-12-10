using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Serialization.Serializers.Decorators;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Realizations.StepRules;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.ViewModel;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Factory.Realizations;
using Pathfinding.GraphLib.Factory.Realizations.CoordinateFactories;
using Pathfinding.GraphLib.Factory.Realizations.GraphAssembles;
using Pathfinding.GraphLib.Factory.Realizations.GraphFactories;
using Pathfinding.GraphLib.Factory.Realizations.NeighborhoodFactories;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Modules;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers.Decorators;
using Pathfinding.GraphLib.Smoothing.Interface;
using Pathfinding.GraphLib.Smoothing.Realizations.MeanCosts;
using Pathfinding.Logging.Interface;
using Pathfinding.Logging.Loggers;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using Shared.Random;
using Shared.Random.Realizations;
using System;
using System.Collections.Generic;
using System.Linq;
using Pathfinding.App.Console.ValueInput.UserInput;
using Shared.Executable;
using Autofac.Core;
using Autofac.Features.Metadata;
using System.Reflection;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Core.Modules.Commands;
using Pathfinding.GraphLib.Core.Modules;
using Pathfinding.App.Console.Model.InProcessActions;
using Pathfinding.App.Console.Model.PathfindingActions;
using Pathfinding.App.Console.MenuItems.GraphMenuItems;
using Pathfinding.App.Console.MenuItems.MainMenuItems;
using Pathfinding.App.Console.MenuItems.PathfindingHistoryMenuItems;
using Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems;
using Pathfinding.App.Console.MenuItems.PathfindingRangeMenuItems;
using Pathfinding.App.Console.MenuItems.PathfindingStatisticsMenuItems;
using Pathfinding.App.Console.MenuItems.PathfindingVisualizationMenuItems;
using Autofac.Features.AttributeFilters;
using Pathfinding.App.Console.MenuItems;
using Pathfinding.AlgorithmLib.Factory;
using Pathfinding.App.Console.Model.VertexActions;

namespace Pathfinding.App.Console.DependencyInjection
{
    using Command = IPathfindingRangeCommand<Vertex>;
    using Commands = IReadOnlyCollection<IPathfindingRangeCommand<Vertex>>;
    using AlgorithmFactory = IAlgorithmFactory<PathfindingProcess>;
    using GraphSerializer = IGraphSerializer<Graph2D<Vertex>, Vertex>;

    internal static class DI
    {
        private const string Key = "Key";
        private const string Order = "Order";

        private enum CommandType { Include, Exclude }

        private static readonly Lazy<IContainer> container = new Lazy<IContainer>(Configure);

        public static IContainer Container => container.Value;

        private static Assembly[] Assemblies => AppDomain.CurrentDomain.GetAssemblies();

        private static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConsoleUserAnswerInput>().As<IInput<Answer>>().SingleInstance();
            builder.RegisterType<ConsoleUserIntInput>().As<IInput<int>>().SingleInstance();
            builder.RegisterType<ConsoleUserStringInput>().As<IInput<string>>().SingleInstance();
            builder.RegisterType<ConsoleUserKeyInput>().As<IInput<ConsoleKey>>().SingleInstance();
            builder.RegisterType<ConsoleUserTimeSpanInput>().As<IInput<TimeSpan>>().SingleInstance();

            builder.RegisterType<ConsoleKeystrokesHook>().AsSelf().SingleInstance();

            builder.RegisterAssemblyTypes(Assemblies).AssignableTo<IUnit>().Except<PathfindingVisualizationUnit>()
                .AsSelf().SingleInstance().PropertiesAutowired().AutoActivate().WithAttributeFiltering();
            builder.RegisterType<PathfindingVisualizationUnit>().AsSelf().AutoActivate().WithAttributeFiltering()
                .SingleInstance().PropertiesAutowired().OnActivated(OnVisualizationViewModelActivated);

            builder.RegisterType<MainUnitMenuItem>().AsSelf().InstancePerDependency();
            builder.RegisterType<GraphCreateMenuItem>().Keyed<IMenuItem>(typeof(MainUnit)).SingleInstance().AutoActivate();
            builder.RegisterType<PathfindingProcessMenuItem>().Keyed<IMenuItem>(typeof(MainUnit)).SingleInstance().AutoActivate();
            builder.RegisterType<PathfindingRangeMenuItem>().Keyed<IMenuItem>(typeof(PathfindingProcessUnit)).SingleInstance().AutoActivate();
            //builder.RegisterType<StatisticsMenuItem>().Keyed<IMenuItem>(typeof(PathfindingProcessUnit)).SingleInstance().AutoActivate();
            //builder.RegisterType<VisualizationMenuItem>().Keyed<IMenuItem>(typeof(PathfindingProcessUnit)).SingleInstance().AutoActivate();
            //builder.RegisterType<HistoryMenuItem>().Keyed<IMenuItem>(typeof(PathfindingProcessUnit)).SingleInstance().AutoActivate();

            //builder.RegisterType<ChangeCostMenuItem>().Keyed<IMenuItem>(typeof(GraphCreatingUnit)).SingleInstance().AutoActivate();
            builder.RegisterType<AssembleGraphMenuItem>().Keyed<IMenuItem>(typeof(GraphCreatingUnit)).SingleInstance().AutoActivate();
            //builder.RegisterType<ResizeGraphMenuItem>().Keyed<IMenuItem>(typeof(GraphCreatingUnit)).SingleInstance().AutoActivate();
            builder.RegisterType<EnterCostRangeMenuItem>().Keyed<IMenuItem>(typeof(GraphCreatingUnit)).SingleInstance().AutoActivate();
            builder.RegisterType<EnterGraphAssembleMenuItem>().Keyed<IMenuItem>(typeof(GraphCreatingUnit)).SingleInstance().AutoActivate();
            builder.RegisterType<EnterGraphParametresMenuItem>().Keyed<IMenuItem>(typeof(GraphCreatingUnit)).SingleInstance().AutoActivate();
            builder.RegisterType<EnterObstaclePercentMenuItem>().Keyed<IMenuItem>(typeof(GraphCreatingUnit)).SingleInstance().AutoActivate();
            //builder.RegisterType<LoadGraphMenuItem>().Keyed<IMenuItem>(typeof(GraphCreatingUnit)).SingleInstance().AutoActivate();
            //builder.RegisterType<SaveGraphMenuItem>().Keyed<IMenuItem>(typeof(GraphCreatingUnit)).SingleInstance().AutoActivate();
            //builder.RegisterType<ReverseVertexMenuItem>().Keyed<IMenuItem>(typeof(GraphCreatingUnit)).SingleInstance().AutoActivate();
            //builder.RegisterType<SmoothGraphMenuItem>().Keyed<IMenuItem>(typeof(GraphCreatingUnit)).SingleInstance().AutoActivate();

            builder.RegisterType<ApplyHistoryMenuItem>().Keyed<IMenuItem>(typeof(PathfindingHistoryUnit)).SingleInstance().AutoActivate();
            builder.RegisterType<ClearHistoryMenuItem>().Keyed<IMenuItem>(typeof(PathfindingHistoryUnit)).SingleInstance().AutoActivate();
            builder.RegisterType<ShowHistoryMenuItem>().Keyed<IMenuItem>(typeof(PathfindingHistoryUnit)).SingleInstance().AutoActivate();

            builder.RegisterType<PathfindingAlgorithmMenuItem>().Keyed<IMenuItem>(typeof(PathfindingProcessUnit)).SingleInstance().AutoActivate();
            builder.RegisterType<ClearColorsMenuItem>().Keyed<IMenuItem>(typeof(PathfindingProcessUnit)).SingleInstance().AutoActivate();
            builder.RegisterType<ClearGraphMenuItem>().Keyed<IMenuItem>(typeof(PathfindingProcessUnit)).SingleInstance().AutoActivate();
            builder.RegisterType<ClearPathfindingRangeMenuItem>().Keyed<IMenuItem>(typeof(PathfindingRangeUnit)).SingleInstance().AutoActivate();
            builder.RegisterType<EnterPathfindingRangeMenuItem>().Keyed<IMenuItem>(typeof(PathfindingRangeUnit)).SingleInstance().AutoActivate().OnActivated(OnEnterPathfindingRangeActivated);
            //builder.RegisterType<MarkTransitToReplaceAction>().As<IVertexAction>().SingleInstance().WithMetadata(Key, ConsoleKey.R);
            //builder.RegisterType<ReplaceTransitVertexAction>().As<IVertexAction>().SingleInstance().WithMetadata(Key, ConsoleKey.P);

            builder.RegisterType<ApplyStatisticsMenuItem>().Keyed<IMenuItem>(typeof(PathfindingStatisticsUnit)).SingleInstance().AutoActivate();

            builder.RegisterType<ApplyVisualizationMenuItem>().Keyed<IMenuItem>(typeof(PathfindingVisualizationUnit)).SingleInstance().AutoActivate();
            builder.RegisterType<EnterAnimationDelayMenuItem>().Keyed<IMenuItem>(typeof(PathfindingVisualizationUnit)).SingleInstance().AutoActivate();

            builder.RegisterType<ViewFactory>().As<IViewFactory>().SingleInstance();

            //builder.RegisterType<SpeedUpAnimation>().As<IAnimationSpeedAction>().WithMetadata(Key, ConsoleKey.UpArrow).SingleInstance();
            //builder.RegisterType<SlowDownAnimation>().As<IAnimationSpeedAction>().WithMetadata(Key, ConsoleKey.DownArrow).SingleInstance();

            builder.RegisterType<ResumeAlgorithm>().As<IPathfindingAction>().WithMetadata(Key, ConsoleKey.Enter).SingleInstance();
            builder.RegisterType<PauseAlgorithm>().As<IPathfindingAction>().WithMetadata(Key, ConsoleKey.P).SingleInstance();
            builder.RegisterType<InterruptAlgorithm>().As<IPathfindingAction>().WithMetadata(Key, ConsoleKey.Escape).SingleInstance();
            //builder.RegisterType<PathfindingStepByStep>().As<IPathfindingAction>().WithMetadata(Key, ConsoleKey.W).SingleInstance();

            builder.RegisterType<FileLog>().As<ILog>().SingleInstance();
            builder.RegisterType<ColorConsoleLog>().As<ILog>().SingleInstance();
            builder.RegisterType<MailLog>().As<ILog>().SingleInstance();
            builder.RegisterComposite<Logs, ILog>().SingleInstance();

            builder.RegisterType<Messenger>().As<IMessenger>().SingleInstance();

            builder.RegisterType<VisualPathfindingRange<Vertex>>().As<IPathfindingRange<Vertex>>().SingleInstance();
            builder.RegisterType<PathfindingRangeBuilder<Vertex>>().As<IPathfindingRangeBuilder<Vertex>>().As<IUndo>()
                .SingleInstance().OnActivated(OnPathfindingRangeBuilderActivated);
            builder.RegisterType<IncludeSourceVertex<Vertex>>().Keyed<Command>(CommandType.Include).WithMetadata(Order, 2).SingleInstance();
            builder.RegisterType<IncludeTargetVertex<Vertex>>().Keyed<Command>(CommandType.Include).WithMetadata(Order, 4).SingleInstance();
            //builder.RegisterType<IncludeTransitVertex<Vertex>>().Keyed<Command>(CommandType.Include).WithMetadata(Order, 6).SingleInstance();
            builder.RegisterType<ReplaceTransitIsolatedVertex<Vertex>>().Keyed<Command>(CommandType.Include).WithMetadata(Order, 1).SingleInstance();
            builder.RegisterType<ReplaceIsolatedSourceVertex<Vertex>>().Keyed<Command>(CommandType.Include).WithMetadata(Order, 3).SingleInstance();
            builder.RegisterType<ReplaceIsolatedTargetVertex<Vertex>>().Keyed<Command>(CommandType.Include).WithMetadata(Order, 5).SingleInstance();
            builder.RegisterType<ExcludeTransitVertex<Vertex>>().Keyed<Command>(CommandType.Exclude).WithMetadata(Order, 3).SingleInstance();
            builder.RegisterType<ExcludeSourceVertex<Vertex>>().Keyed<Command>(CommandType.Exclude).WithMetadata(Order, 1).SingleInstance();
            builder.RegisterType<ExcludeTargetVertex<Vertex>>().Keyed<Command>(CommandType.Exclude).WithMetadata(Order, 2).SingleInstance();
            //builder.RegisterType<ReplaceTransitVerticesModule<Vertex>>().AsSelf().As<IUndo>().SingleInstance();

            builder.RegisterComposite<CompositeUndo, IUndo>().SingleInstance();

            builder.RegisterType<PseudoRandom>().As<IRandom>().SingleInstance();

            builder.RegisterType<GraphAssemble<Graph2D<Vertex>, Vertex>>().As<IGraphAssemble<Graph2D<Vertex>, Vertex>>().SingleInstance();
            builder.RegisterType<CostFactory>().As<IVertexCostFactory>().SingleInstance();
            builder.RegisterType<VertexFactory>().As<IVertexFactory<Vertex>>().SingleInstance();
            builder.RegisterType<GraphFieldFactory>().As<IGraphFieldFactory<Graph2D<Vertex>, Vertex, GraphField>>().SingleInstance();
            builder.RegisterType<Coordinate2DFactory>().As<ICoordinateFactory>().SingleInstance();
            builder.RegisterType<Graph2DFactory<Vertex>>().As<IGraphFactory<Graph2D<Vertex>, Vertex>>().SingleInstance();
            builder.RegisterDecorator<Graph2dWrapFactory, IGraphFactory<Graph2D<Vertex>, Vertex>>();
            builder.RegisterType<MooreNeighborhoodFactory>().As<INeighborhoodFactory>().SingleInstance();
            builder.RegisterType<VertexVisualization>().As<IVisualization<Vertex>>().SingleInstance();

            builder.RegisterType<RootMeanSquareCost>().As<IMeanCost>().SingleInstance();

            builder.RegisterType<InFileSerializationModule<Graph2D<Vertex>, Vertex>>()
                .As<IGraphSerializationModule<Graph2D<Vertex>, Vertex>>().SingleInstance();
            builder.RegisterType<PathInput>().As<IPathInput>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<BinaryGraphSerializer<Graph2D<Vertex>, Vertex>>().As<GraphSerializer>().SingleInstance();
            builder.RegisterDecorator<CompressGraphSerializer<Graph2D<Vertex>, Vertex>, GraphSerializer>();
            builder.RegisterDecorator<CryptoGraphSerializer<Graph2D<Vertex>, Vertex>, GraphSerializer>();
            builder.RegisterType<VertexFromInfoFactory>().As<IVertexFromInfoFactory<Vertex>>().SingleInstance();

            builder.RegisterType<DijkstraAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance();
            //builder.RegisterType<AStarAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance();
            //builder.RegisterType<IDAStarAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance();
            builder.RegisterType<LeeAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance();
            //builder.RegisterType<AStarLeeAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance();
            //builder.RegisterType<CostGreedyAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance();
            //builder.RegisterType<DistanceFirstAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance();
            //builder.RegisterType<DepthFirstAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance();
            //builder.RegisterType<HeuristicCostGreedyAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance();
            builder.RegisterType<LandscapeStepRule>().As<IStepRule>().SingleInstance();

            return builder.Build();
        }

        private static void OnVisualizationViewModelActivated(IActivatedEventArgs<PathfindingVisualizationUnit> args)
        {
            args.Instance.PathfindingActions = args.Context.ResolveWithMetadata<IPathfindingAction>();
            args.Instance.AnimationActions = args.Context.ResolveWithMetadata<IAnimationSpeedAction>();
        }

        private static void OnEnterPathfindingRangeActivated(IActivatedEventArgs<EnterPathfindingRangeMenuItem> args)
        {
            var actions = args.Context.ResolveWithMetadata<IVertexAction>();
            args.Instance.Actions.AddRange(actions);
        }

        private static void OnPathfindingRangeBuilderActivated(IActivatedEventArgs<PathfindingRangeBuilder<Vertex>> args)
        {
            args.Instance.IncludeCommands = args.Context.ResolveKeyed(CommandType.Include);
            args.Instance.ExcludeCommands = args.Context.ResolveKeyed(CommandType.Exclude);
        }

        private static Commands ResolveKeyed(this IComponentContext context, CommandType key)
        {
            return context.ResolveKeyed<IEnumerable<Meta<Command>>>(key)
                .OrderBy(x => x.Metadata[Order])
                .Select(x => x.Value)
                .ToReadOnly();
        }

        private static IReadOnlyDictionary<ConsoleKey, TValue> ResolveWithMetadata<TValue>(this IComponentContext context)
        {
            return context.Resolve<IEnumerable<Meta<TValue>>>()
                .ToDictionary(action => (ConsoleKey)action.Metadata[Key], action => action.Value)
                .ToReadOnly();
        }
    }
}