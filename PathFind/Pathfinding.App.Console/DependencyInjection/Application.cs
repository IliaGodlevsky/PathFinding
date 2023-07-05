using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Serialization.Serializers.Decorators;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Realizations.Heuristics;
using Pathfinding.AlgorithmLib.Core.Realizations.StepRules;
using Pathfinding.AlgorithmLib.Factory;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.DependencyInjection.Attributes;
using Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Loggers;
using Pathfinding.App.Console.MenuItems;
using Pathfinding.App.Console.MenuItems.ColorMenuItems;
using Pathfinding.App.Console.MenuItems.EditorMenuItems;
using Pathfinding.App.Console.MenuItems.GraphMenuItems;
using Pathfinding.App.Console.MenuItems.KeysMenuItems;
using Pathfinding.App.Console.MenuItems.MainMenuItems;
using Pathfinding.App.Console.MenuItems.PathfindingHistoryMenuItems;
using Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems;
using Pathfinding.App.Console.MenuItems.PathfindingRangeMenuItems;
using Pathfinding.App.Console.MenuItems.PathfindingStatisticsMenuItems;
using Pathfinding.App.Console.MenuItems.PathfindingVisualizationMenuItems;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.InProcessActions;
using Pathfinding.App.Console.Model.PathfindingActions;
using Pathfinding.App.Console.Model.VertexActions;
using Pathfinding.App.Console.Model.Visualizations;
using Pathfinding.App.Console.Model.Visualizations.Visuals;
using Pathfinding.App.Console.Serialization;
using Pathfinding.App.Console.Settings;
using Pathfinding.App.Console.Units;
using Pathfinding.App.Console.ValueInput;
using Pathfinding.App.Console.ValueInput.UserInput;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Modules;
using Pathfinding.GraphLib.Core.Modules.Commands;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Factory.Realizations;
using Pathfinding.GraphLib.Factory.Realizations.CoordinateFactories;
using Pathfinding.GraphLib.Factory.Realizations.GraphAssembles;
using Pathfinding.GraphLib.Factory.Realizations.GraphFactories;
using Pathfinding.GraphLib.Factory.Realizations.NeighborhoodFactories;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers.Decorators;
using Pathfinding.GraphLib.Smoothing.Interface;
using Pathfinding.GraphLib.Smoothing.Realizations.MeanCosts;
using Pathfinding.Logging.Interface;
using Pathfinding.Logging.Loggers;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Executable;
using Shared.Extensions;
using Shared.Random;
using Shared.Random.Realizations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

using static Pathfinding.App.Console.DependencyInjection.PathfindingUnits;
using static Pathfinding.App.Console.DependencyInjection.RegistrationConstants;

namespace Pathfinding.App.Console.DependencyInjection.Registrations
{
    using AlgorithmFactory = IAlgorithmFactory<PathfindingProcess>;
    using Command = IPathfindingRangeCommand<Vertex>;

    internal static class Application
    {
        public static void Start()
        {
            var builder = new ContainerBuilder();
            foreach (var feature in GetFeatures())
            {
                feature.Apply(builder);
            }
            using (var scope = builder.Build())
            {
                var main = scope.Resolve<MainUnitMenuItem>();
                main.Execute();
            }
        }

        private static IReadOnlyCollection<IFeature> GetFeatures()
        {
            var nestedTypes = typeof(Application).GetNestedTypes(BindingFlags.NonPublic);
            var mandatory = nestedTypes
                .Where(member => Attribute.IsDefined(member, typeof(MandatoryAttribute)))
                .Select(member => (IFeature)Activator.CreateInstance(member));
            var optional = nestedTypes
                .Where(member => Attribute.IsDefined(member, typeof(OptionalAttribute)))
                .ToDictionary(type => type.Name).AsReadOnly();
            var applied = Features.Default.GetType().GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(ApplicationScopedSettingAttribute)))
                .Select(prop => (prop.Name, Value: prop.GetValue(Features.Default)))
                .Where(item => item.Value.Equals(true) && optional.ContainsKey(item.Name))
                .Select(item => (IFeature)Activator.CreateInstance(optional[item.Name]))
                .Concat(mandatory).ToList().AsReadOnly();
            return applied;
        }

        [Mandatory]
        private sealed class Main : IFeature
        {
            public void Apply(ContainerBuilder builder)
            {
                builder.RegisterType<ApplicationSettingsStore>().SingleInstance().AutoActivate();

                builder.RegisterType<Messenger>().As<IMessenger>().SingleInstance().RegisterRecievers();

                builder.RegisterType<AppLayout>().As<ICanRecieveMessage>().SingleInstance().AutoActivate();

                builder.RegisterUnit<MainUnit, AnswerExitMenuItem>();
                builder.RegisterUnits<ExitMenuItem>(Graph, Process, Range);

                builder.RegisterType<GraphsPathfindingHistory>().AsSelf().SingleInstance();

                builder.RegisterType<MainUnitMenuItem>().AsSelf().InstancePerDependency();
                builder.RegisterType<GraphCreateMenuItem>().Keyed<IMenuItem>(PathfindingUnits.Main).SingleInstance();
                builder.RegisterType<PathfindingProcessMenuItem>().Keyed<IConditionedMenuItem>(PathfindingUnits.Main).As<ICanRecieveMessage>().SingleInstance();

                builder.RegisterType<TotalVertexVisualization>().As<ITotalVisualization<Vertex>>().SingleInstance()
                    .ConfigurePipeline(p => p.Use(new TotalVisualizationMiddleware()));
                builder.RegisterVisualizedVertices<VisualizedTarget>(VisualizedType.Target);
                builder.RegisterVisualizedVertices<VisualizedSource>(VisualizedType.Source);
                builder.RegisterVisualizedVertices<VisualizedRegular>(VisualizedType.Regular);
                builder.RegisterVisualizedVertices<VisualizedPath>(VisualizedType.Path);
                builder.RegisterVisualizedVertices<VisualizedObstacle>(VisualizedType.Obstacle);
                builder.RegisterVisualizedVertices<VisualizedCrossedPath>(VisualizedType.Crossed);

                builder.RegisterType<PathfindingRangeMenuItem>().Keyed<IMenuItem>(Process).SingleInstance();
                builder.RegisterType<PathfindingAlgorithmMenuItem>().Keyed<IConditionedMenuItem>(Process)
                    .SingleInstance().ConfigurePipeline(p => p.Use(new PathfindingItemResolveMiddleware(Group, Order)));
                builder.RegisterType<ClearColorsMenuItem>().Keyed<IConditionedMenuItem>(Process).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<ClearGraphMenuItem>().Keyed<IConditionedMenuItem>(Process).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<ClearPathfindingRangeMenuItem>().Keyed<IConditionedMenuItem>(Range).SingleInstance();
                builder.RegisterType<EnterPathfindingRangeMenuItem>().Keyed<IConditionedMenuItem>(Range).As<ICanRecieveMessage>().SingleInstance()
                    .ConfigurePipeline(p => p.Use(new VertexActionResolveMiddlewear(PathfindingRange)));
                builder.RegisterType<IncludeInRangeAction>().Keyed<IVertexAction>(PathfindingRange).WithMetadata(PathfindingRange, nameof(Keys.Default.IncludeInRange));
                builder.RegisterType<ExcludeFromRangeAction>().Keyed<IVertexAction>(PathfindingRange).WithMetadata(PathfindingRange, nameof(Keys.Default.ExcludeFromRange));

                builder.RegisterType<AssembleGraphMenuItem>().Keyed<IConditionedMenuItem>(Graph).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<ResizeGraphMenuItem>().Keyed<IConditionedMenuItem>(Graph).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<ChooseGraphMenuItem>().Keyed<IConditionedMenuItem>(Graph).SingleInstance();
                builder.RegisterType<DeleteGraphMenuItem>().Keyed<IConditionedMenuItem>(Graph).SingleInstance().As<ICanRecieveMessage>();
                builder.RegisterType<EnterCostRangeMenuItem>().Keyed<IMenuItem>(Graph).SingleInstance();
                builder.RegisterType<EnterGraphParametresMenuItem>().Keyed<IMenuItem>(Graph).SingleInstance();
                builder.RegisterType<EnterObstaclePercentMenuItem>().Keyed<IMenuItem>(Graph).SingleInstance();

                builder.RegisterType<ViewFactory>().As<IViewFactory>().SingleInstance();

                builder.RegisterType<FileLog>().As<ILog>().SingleInstance();
                builder.RegisterType<ConsoleLog>().As<ILog>().SingleInstance();
                builder.RegisterType<DebugLog>().As<ILog>().SingleInstance();
                builder.RegisterComposite<Logs, ILog>().SingleInstance();

                builder.RegisterComposite<CompositeUndo, IUndo>().SingleInstance();
                builder.RegisterType<XorshiftRandom>().As<IRandom>().SingleInstance();

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

                builder.RegisterType<DefaultStepRule>().As<IStepRule>().SingleInstance();
                builder.RegisterType<EuclidianDistance>().As<IHeuristic>().SingleInstance();
            }
        }

        [Mandatory]
        private sealed class UserInput : IFeature
        {
            public void Apply(ContainerBuilder builder)
            {
                builder.RegisterType<ConsoleUserAnswerInput>().As<IInput<Answer>>().SingleInstance();
                builder.RegisterType<ConsoleUserIntInput>().As<IInput<int>>().SingleInstance();
                builder.RegisterType<ConsoleUserStringInput>().As<IInput<string>>().SingleInstance();
                builder.RegisterType<ConsoleUserKeyInput>().As<IInput<ConsoleKey>>().SingleInstance();
                builder.RegisterType<ConsoleUserTimeSpanInput>().As<IInput<TimeSpan>>().SingleInstance();
            }
        }

        [Mandatory]
        private sealed class BreadthAlgorithms : IFeature
        {
            public void Apply(ContainerBuilder builder)
            {
                builder.RegisterType<AStarLeeAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance().WithMetadata(Group, BreadthGroup).WithMetadata(Order, 2);
                builder.RegisterType<LeeAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance().WithMetadata(Group, BreadthGroup).WithMetadata(Order, 1);
            }
        }

        // !!!Do not change names of the classes, that are marked with [Optional] attribute.
        // They are mapped with properties in Features.settings file
        [Optional]
        private sealed class GraphSharing : IFeature
        {
            public void Apply(ContainerBuilder builder)
            {
                builder.RegisterType<SaveGraphMenuItem>().Keyed<IConditionedMenuItem>(Graph).SingleInstance();
                builder.RegisterType<LoadGraphMenuItem>().Keyed<IMenuItem>(Graph).SingleInstance();
                builder.RegisterType<SendGraphMenuItem>().Keyed<IConditionedMenuItem>(Graph).SingleInstance();
                builder.RegisterType<RecieveGraphMenuItem>().Keyed<IMenuItem>(Graph).SingleInstance();

                builder.RegisterType<FilePathInput>().As<IFilePathInput>().SingleInstance();
                builder.RegisterType<AddressInput>().As<IInput<(string, int)>>().SingleInstance();

                builder.RegisterInstance(Aes.Create()).As<SymmetricAlgorithm>().SingleInstance();

                builder.RegisterType<BinaryHistorySerializer>().As<ISerializer<GraphPathfindingHistory>>().SingleInstance();
                builder.RegisterType<BinaryGraphSerializer<Graph2D<Vertex>, Vertex>>().As<ISerializer<Graph2D<Vertex>>>().SingleInstance();

                builder.RegisterType<PathfindingHistorySerializer>().As<ISerializer<GraphsPathfindingHistory>>().SingleInstance();

                builder.RegisterDecorator<BufferedSerializer<GraphsPathfindingHistory>, ISerializer<GraphsPathfindingHistory>>();
                builder.RegisterDecorator<CompressSerializer<GraphsPathfindingHistory>, ISerializer<GraphsPathfindingHistory>>();
                builder.RegisterDecorator<CryptoSerializer<GraphsPathfindingHistory>, ISerializer<GraphsPathfindingHistory>>();
                builder.RegisterDecorator<ThreadSafeSerializer<GraphsPathfindingHistory>, ISerializer<GraphsPathfindingHistory>>();

                builder.RegisterType<VertexFromInfoFactory>().As<IVertexFromInfoFactory<Vertex>>().SingleInstance();
            }
        }

        [Optional]
        private sealed class GraphEditor : IFeature
        {
            public void Apply(ContainerBuilder builder)
            {
                builder.RegisterUnit<GraphEditorUnit, ExitMenuItem>();
                builder.RegisterType<EditorKeysMenuItem>().Keyed<IMenuItem>(PathfindingUnits.KeysUnit).SingleInstance();
                builder.RegisterType<EditorUnitMenuItem>().Keyed<IConditionedMenuItem>(PathfindingUnits.Main).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<ReverseVertexMenuItem>().Keyed<IConditionedMenuItem>(Editor).As<ICanRecieveMessage>().SingleInstance()
                    .ConfigurePipeline(p => p.Use(new VertexActionResolveMiddlewear(Reverse)));
                builder.RegisterType<NeighbourhoodMenuItem>().Keyed<IConditionedMenuItem>(Editor).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<ReverseVertexAction>().Keyed<IVertexAction>(Reverse).WithMetadata(Reverse, nameof(Keys.Default.ReverseVertex));
                builder.RegisterType<SmoothGraphMenuItem>().Keyed<IConditionedMenuItem>(Editor).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<ChangeCostMenuItem>().Keyed<IConditionedMenuItem>(Editor).SingleInstance()
                    .As<ICanRecieveMessage>().ConfigurePipeline(p => p.Use(new VertexActionResolveMiddlewear(ChangeCost)));
                builder.RegisterType<IncreaseCostAction>().Keyed<IVertexAction>(ChangeCost).WithMetadata(ChangeCost, nameof(Keys.Default.IncreaseCost));
                builder.RegisterType<DecreaseCostAction>().Keyed<IVertexAction>(ChangeCost).WithMetadata(ChangeCost, nameof(Keys.Default.DecreaseCost));
                builder.RegisterType<MeanCost>().As<IMeanCost>().SingleInstance();
            }
        }

        [Optional]
        private sealed class ColorEditor : IFeature
        {
            public void Apply(ContainerBuilder builder)
            {
                builder.RegisterUnit<ColorsUnit, ExitMenuItem>();
                builder.RegisterInstance(Colours.Default).As<SettingsBase>().SingleInstance();
                builder.RegisterType<ColorsUnitMenuItem>().Keyed<IMenuItem>(PathfindingUnits.Main).SingleInstance();
                builder.RegisterType<RegularVertexColorMenuItem>().Keyed<IMenuItem>(Colors).SingleInstance();
                builder.RegisterType<PathVertexColorMenuItem>().Keyed<IMenuItem>(Colors).SingleInstance();
                builder.RegisterType<SourceVertexColorMenuItem>().Keyed<IMenuItem>(Colors).SingleInstance();
                builder.RegisterType<TargetVertexColorMenuItem>().Keyed<IMenuItem>(Colors).SingleInstance();
                builder.RegisterType<CrossedPathVertexColorMenuItem>().Keyed<IMenuItem>(Colors).SingleInstance();
                builder.RegisterType<ObstacleVertexColorMenuItem>().Keyed<IMenuItem>(Colors).SingleInstance();
            }
        }

        [Optional]
        private sealed class TransitVertices : IFeature
        {
            public void Apply(ContainerBuilder builder)
            {
                builder.RegisterType<TransitKeysMenuItem>().Keyed<IMenuItem>(PathfindingUnits.KeysUnit).SingleInstance();
                builder.RegisterType<ExcludeTransitVertex<Vertex>>().Keyed<Command>(ExcludeCommand).WithMetadata(Order, 3).SingleInstance();
                builder.RegisterType<ReplaceTransitVerticesModule<Vertex>>().AsSelf().As<IUndo>().SingleInstance();
                builder.RegisterType<MarkTransitToReplaceAction>().Keyed<IVertexAction>(PathfindingRange).WithMetadata(PathfindingRange, nameof(Keys.Default.MarkToReplace));
                builder.RegisterType<ReplaceTransitVertexAction>().Keyed<IVertexAction>(PathfindingRange).WithMetadata(PathfindingRange, nameof(Keys.Default.ReplaceTransit));
                builder.RegisterType<ReplaceTransitIsolatedVertex<Vertex>>().Keyed<Command>(IncludeCommand).WithMetadata(Order, 1).SingleInstance();
                builder.RegisterType<IncludeTransitVertex<Vertex>>().Keyed<Command>(IncludeCommand).WithMetadata(Order, 6).SingleInstance();
                builder.RegisterType<TransitVertexColorMenuItem>().Keyed<IMenuItem>(Colors).SingleInstance();
                builder.RegisterVisualizedVertices<VisualizedTransit>(VisualizedType.Transit);
            }
        }

        [Optional]
        private sealed class WaveAlgorithms : IFeature
        {
            public void Apply(ContainerBuilder builder)
            {
                builder.RegisterType<DijkstraAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance().WithMetadata(Group, WaveGroup).WithMetadata(Order, 1);
                builder.RegisterType<RandomAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance().WithMetadata(Group, WaveGroup).WithMetadata(Order, 4);
                builder.RegisterType<AStarAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance().WithMetadata(Group, WaveGroup).WithMetadata(Order, 2);
                builder.RegisterType<IDAStarAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance().WithMetadata(Group, WaveGroup).WithMetadata(Order, 3);
            }
        }

        [Optional]
        private sealed class GreedyAlgorithms : IFeature
        {
            public void Apply(ContainerBuilder builder)
            {
                builder.RegisterType<DistanceFirstAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance().WithMetadata(Group, GreedGroup).WithMetadata(Order, 1);
                builder.RegisterType<DepthFirstAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance().WithMetadata(Group, GreedGroup).WithMetadata(Order, 2);
                builder.RegisterType<HeuristicCostGreedyAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance().WithMetadata(Group, GreedGroup).WithMetadata(Order, 3);
                builder.RegisterType<CostGreedyAlgorithmFactory>().As<AlgorithmFactory>().SingleInstance().WithMetadata(Group, GreedGroup).WithMetadata(Order, 4);
            }
        }

        [Optional]
        private sealed class PathfindingVisualization : IFeature
        {
            public void Apply(ContainerBuilder builder)
            {
                builder.RegisterUnit<PathfindingVisualizationUnit, ExitMenuItem>();
                builder.RegisterType<VisualizationMenuItem>().Keyed<IMenuItem>(Process).SingleInstance();
                builder.RegisterType<ApplyVisualizationMenuItem>().Keyed<IMenuItem>(Visual).SingleInstance();
                builder.RegisterType<EnterAnimationDelayMenuItem>().Keyed<IConditionedMenuItem>(Visual).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<VisitedVertexColorMenuItem>().Keyed<IMenuItem>(Colors).SingleInstance();
                builder.RegisterType<EnqueuedVertexColorMenuItem>().Keyed<IMenuItem>(Colors).SingleInstance();
                builder.RegisterVisualizedVertices<VisualizedVisited>(VisualizedType.Visited);
                builder.RegisterVisualizedVertices<VisualizedEnqueued>(VisualizedType.Enqueued);
            }
        }

        [Optional]
        private sealed class PathfindingHistory : IFeature
        {
            public void Apply(ContainerBuilder builder)
            {
                builder.RegisterUnit<PathfindingHistoryUnit, ExitMenuItem>();
                builder.RegisterType<HistoryMenuItem>().Keyed<IMenuItem>(Process).SingleInstance();
                builder.RegisterType<ApplyHistoryMenuItem>().Keyed<IMenuItem>(History).SingleInstance();
                builder.RegisterType<ClearHistoryMenuItem>().Keyed<IConditionedMenuItem>(History).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<ShowHistoryMenuItem>().Keyed<IConditionedMenuItem>(History).As<ICanRecieveMessage>().SingleInstance();
            }
        }

        [Optional]
        private sealed class VisualizationControl : IFeature
        {
            public void Apply(ContainerBuilder builder)
            {
                builder.RegisterType<PathfindingControlKeysMenuItem>().Keyed<IMenuItem>(PathfindingUnits.KeysUnit).SingleInstance();
                builder.RegisterType<SpeedUpAnimation>().As<IAnimationSpeedAction>().WithMetadata(Key, nameof(Keys.Default.SpeedUp));
                builder.RegisterType<SlowDownAnimation>().As<IAnimationSpeedAction>().WithMetadata(Key, nameof(Keys.Default.SpeedDown));
                builder.RegisterType<ResumeAlgorithm>().As<IPathfindingAction>().WithMetadata(Key, nameof(Keys.Default.ResumeAlgorithm));
                builder.RegisterType<PauseAlgorithm>().As<IPathfindingAction>().WithMetadata(Key, nameof(Keys.Default.PauseAlgorithm));
                builder.RegisterType<InterruptAlgorithm>().As<IPathfindingAction>().WithMetadata(Key, nameof(Keys.Default.InterruptAlgorithm));
                builder.RegisterType<PathfindingStepByStep>().As<IPathfindingAction>().WithMetadata(Key, nameof(Keys.Default.StepByStepPathfinding));
            }
        }

        [Optional]
        private sealed class PathfindingStatistics : IFeature
        {
            public void Apply(ContainerBuilder builder)
            {
                builder.RegisterUnit<PathfindingStatisticsUnit, ExitMenuItem>();
                builder.RegisterType<StatisticsMenuItem>().Keyed<IMenuItem>(Process).SingleInstance();
                builder.RegisterType<ApplyStatisticsMenuItem>().Keyed<IMenuItem>(Statistics).SingleInstance();
            }
        }

        [Optional]
        private sealed class KeysEditor : IFeature
        {
            public void Apply(ContainerBuilder builder)
            {
                builder.RegisterUnit<KeysUnit, ExitMenuItem>();
                builder.RegisterInstance(Keys.Default).As<SettingsBase>().SingleInstance();
                builder.RegisterType<KeysUnitMenuItem>().Keyed<IMenuItem>(PathfindingUnits.Main).SingleInstance();
                builder.RegisterType<RegularKeysMenuItem>().Keyed<IMenuItem>(PathfindingUnits.KeysUnit).SingleInstance();
            }
        }
    }
}
