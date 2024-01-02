using Autofac;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Realizations.Heuristics;
using Pathfinding.AlgorithmLib.Core.Realizations.StepRules;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.TransferObjects;
using Pathfinding.App.Console.DAL.Services;
using Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Loggers;
using Pathfinding.App.Console.MenuItems;
using Pathfinding.App.Console.MenuItems.ColorMenuItems;
using Pathfinding.App.Console.MenuItems.EditorMenuItems;
using Pathfinding.App.Console.MenuItems.GraphMenuItems;
using Pathfinding.App.Console.MenuItems.GraphSharingMenuItems;
using Pathfinding.App.Console.MenuItems.KeysMenuItems;
using Pathfinding.App.Console.MenuItems.MainMenuItems;
using Pathfinding.App.Console.MenuItems.PathfindingHistoryMenuItems;
using Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems;
using Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems.AlgorithmMenuItems;
using Pathfinding.App.Console.MenuItems.PathfindingRangeMenuItems;
using Pathfinding.App.Console.MenuItems.PathfindingStatisticsMenuItems;
using Pathfinding.App.Console.MenuItems.PathfindingVisualizationMenuItems;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.InProcessActions;
using Pathfinding.App.Console.Model.PathfindingActions;
using Pathfinding.App.Console.Model.VertexActions;
using Pathfinding.App.Console.Model.Visualizations;
using Pathfinding.App.Console.Model.Visualizations.Containers;
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
using Pathfinding.GraphLib.Factory.Interface;
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
using Shared.Executable;
using Shared.Random;
using Shared.Random.Realizations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using static Pathfinding.App.Console.DependencyInjection.PathfindingUnits;
using static Pathfinding.App.Console.DependencyInjection.RegistrationConstants;

namespace Pathfinding.App.Console.DependencyInjection.Registrations
{
    using Command = IPathfindingRangeCommand<Vertex>;

    internal sealed partial class Application : IDisposable
    {
        private static IEnumerable<IComponent> GetComponents()
        {
            return typeof(Application).GetNestedTypes(BindingFlags.NonPublic)
                .Where(member => typeof(IComponent).IsAssignableFrom(member))
                .Select(member => (IComponent)Activator.CreateInstance(member));
        }

        private sealed class Main : IComponent
        {
            public void Apply(ContainerBuilder builder)
            {
                builder.RegisterType<ApplicationSettingsStore>().SingleInstance().AutoActivate();

                builder.RegisterType<WeakReferenceMessenger>().As<IMessenger>().SingleInstance().RegisterRecievers();

                builder.RegisterType<AppLayout>().As<ICanRecieveMessage>().SingleInstance().AutoActivate();

                builder.RegisterUnit<MainUnit, AnswerExitMenuItem>(new UnitParamtresFactory());
                builder.RegisterUnits<ExitMenuItem>(new UnitParamtresFactory(), Graph, PathfindingUnits.Process, Range);

                builder.RegisterType<Service>().As<IService>().SingleInstance();
                builder.RegisterDecorator<CacheService, IService>();
                builder.RegisterType<InMemoryUnitOfWorkFactory>().As<IUnitOfWorkFactory>().SingleInstance();

                builder.RegisterMapper();

                builder.RegisterType<MainUnitMenuItem>().AsSelf().InstancePerDependency();
                builder.RegisterType<GraphCreateMenuItem>().Keyed<IMenuItem>(PathfindingUnits.Main).SingleInstance();
                builder.RegisterType<PathfindingProcessMenuItem>().Keyed<IMenuItem>(PathfindingUnits.Main)
                    .As<ICanRecieveMessage>().SingleInstance();

                builder.RegisterType<VisualizationContainer>().CommunicateContainers().SingleInstance()
                    .AsImplementedInterfaces().ConfigurePipeline(p => p.Use(new AllVisualizedVerticesMiddleware()));
                builder.RegisterVisualizionContainer<VisualizedTarget>(Constants.TargetColorKey);
                builder.RegisterVisualizionContainer<VisualizedSource>(Constants.SourceColorKey);
                builder.RegisterVisualizionContainer<VisualizedRegular>(Constants.RegularColorKey);
                builder.RegisterVisualizionContainer<VisualizedPath>(Constants.PathColorKey);
                builder.RegisterVisualizionContainer<VisualizedObstacle>(Constants.ObstacleColorKey);
                builder.RegisterVisualizionContainer<VisualizedCrossedPath>(Constants.CrossedPathColorKey);

                builder.RegisterUnit<AlgorithmChooseUnit, ExitMenuItem>(new UnitParamtresFactory());
                builder.RegisterType<AlgorithmsUnitMenuItem>().Keyed<IMenuItem>(Process).SingleInstance();

                builder.RegisterType<PathfindingRangeMenuItem>().Keyed<IMenuItem>(Process).SingleInstance();
                builder.RegisterType<ClearColorsMenuItem>().Keyed<IMenuItem>(Process).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<ClearGraphMenuItem>().Keyed<IMenuItem>(Process).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<ClearPathfindingRangeMenuItem>().Keyed<IMenuItem>(Range).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<EnterPathfindingRangeMenuItem>().Keyed<IMenuItem>(Range).As<ICanRecieveMessage>().SingleInstance()
                    .ConfigurePipeline(p => p.Use(new VertexActionResolveMiddlewear(PathfindingRange)));
                builder.RegisterType<IncludeInRangeAction>().Keyed<IVertexAction>(PathfindingRange)
                    .WithMetadata(PathfindingRange, nameof(Keys.Default.IncludeInRange));
                builder.RegisterType<ExcludeFromRangeAction>().Keyed<IVertexAction>(PathfindingRange)
                    .WithMetadata(PathfindingRange, nameof(Keys.Default.ExcludeFromRange));

                builder.RegisterType<AssembleGraphMenuItem>().Keyed<IMenuItem>(Graph).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<ChooseGraphMenuItem>().Keyed<IMenuItem>(Graph).SingleInstance();
                builder.RegisterType<ChooseNeighbourhoodMenuItem>().Keyed<IMenuItem>(Graph).SingleInstance()
                    .ConfigurePipeline(p => p.Use(new KeyResolveMiddlware<string, INeighborhoodFactory>(Neighbourhood)));
                builder.RegisterType<DeleteGraphMenuItem>().Keyed<IMenuItem>(Graph).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<EnterCostRangeMenuItem>().Keyed<IMenuItem>(Graph).SingleInstance();
                builder.RegisterType<EnterGraphParametresMenuItem>().Keyed<IMenuItem>(Graph).SingleInstance();
                builder.RegisterType<EnterObstaclePercentMenuItem>().Keyed<IMenuItem>(Graph).SingleInstance();

                builder.RegisterType<FileLog>().As<ILog>().SingleInstance();
                builder.RegisterType<ConsoleLog>().As<ILog>().SingleInstance();
                builder.RegisterType<DebugLog>().As<ILog>().SingleInstance();
                builder.RegisterComposite<Logs, ILog>().SingleInstance();

                builder.RegisterComposite<CompositeUndo, IUndo>().SingleInstance();
                builder.RegisterType<XorshiftRandom>().As<IRandom>().SingleInstance();

                builder.RegisterType<PathfindingRange<Vertex>>().Named<IPathfindingRange<Vertex>>(PathfindingRange).SingleInstance();
                builder.RegisterDecorator<IPathfindingRange<Vertex>>((с, inner) => new VisualPathfindingRange<Vertex>(inner), PathfindingRange).SingleInstance();
                builder.RegisterType<PathfindingRangeBuilder<Vertex>>().As<IPathfindingRangeBuilder<Vertex>>().As<IUndo>()
                    .SingleInstance().ConfigurePipeline(p => p.Use(new RangeBuilderResolveMiddlewear(Order)));
                builder.RegisterType<IncludeSourceVertex<Vertex>>().Keyed<Command>(IncludeCommand).WithMetadata(Order, 2).SingleInstance();
                builder.RegisterType<IncludeTargetVertex<Vertex>>().Keyed<Command>(IncludeCommand).WithMetadata(Order, 4).SingleInstance();
                builder.RegisterType<ReplaceIsolatedSourceVertex<Vertex>>().Keyed<Command>(IncludeCommand).WithMetadata(Order, 3).SingleInstance();
                builder.RegisterType<ReplaceIsolatedTargetVertex<Vertex>>().Keyed<Command>(IncludeCommand).WithMetadata(Order, 5).SingleInstance();
                builder.RegisterType<ExcludeSourceVertex<Vertex>>().Keyed<Command>(ExcludeCommand).WithMetadata(Order, 1).SingleInstance();
                builder.RegisterType<ExcludeTargetVertex<Vertex>>().Keyed<Command>(ExcludeCommand).WithMetadata(Order, 2).SingleInstance();

                builder.RegisterType<GraphAssemble<Vertex>>().As<IGraphAssemble<Vertex>>().SingleInstance();
                builder.RegisterType<VertexFactory>().As<IVertexFactory<Vertex>>().SingleInstance();
                builder.RegisterType<GraphFactory<Vertex>>().As<IGraphFactory<Vertex>>().SingleInstance();

                builder.RegisterType<MooreNeighborhoodFactory>().Keyed<INeighborhoodFactory>(Neighbourhood)
                    .SingleInstance().WithMetadata(Neighbourhood, nameof(Languages.MooreNeighbourhood));
                builder.RegisterType<VonNeumannNeighborhoodFactory>().Keyed<INeighborhoodFactory>(Neighbourhood)
                    .SingleInstance().WithMetadata(Neighbourhood, nameof(Languages.VonNeumannNeighbourhood));

                builder.RegisterType<DefaultStepRule>().Keyed<IStepRule>(PathfindingAlgorithms).SingleInstance()
                    .WithMetadata(PathfindingAlgorithms, nameof(Languages.Default));
                builder.RegisterType<LandscapeStepRule>().Keyed<IStepRule>(PathfindingAlgorithms).SingleInstance()
                    .WithMetadata(PathfindingAlgorithms, nameof(Languages.Landscape));

                builder.RegisterType<EuclidianDistance>().Keyed<IHeuristic>(PathfindingAlgorithms).SingleInstance()
                    .WithMetadata(PathfindingAlgorithms, nameof(Languages.Euclidian));
                builder.RegisterType<ManhattanDistance>().Keyed<IHeuristic>(PathfindingAlgorithms).SingleInstance()
                    .WithMetadata(PathfindingAlgorithms, nameof(Languages.Manhattan));
                builder.RegisterType<ChebyshevDistance>().Keyed<IHeuristic>(PathfindingAlgorithms).SingleInstance()
                    .WithMetadata(PathfindingAlgorithms, nameof(Languages.Chebyshev));
                builder.RegisterType<CosineDistance>().Keyed<IHeuristic>(PathfindingAlgorithms).SingleInstance()
                    .WithMetadata(PathfindingAlgorithms, nameof(Languages.CosDistance));
            }
        }

        private sealed class UserInput : IComponent
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

        private sealed class BreadthAlgorithms : IComponent
        {
            public void Apply(ContainerBuilder builder)
            {
                builder.RegisterType<AStarLeeAlgorithmMenuItem>().Keyed<IMenuItem>(PathfindingUnits.Algorithms).SingleInstance()
                    .ConfigurePipeline(p => p.Use(new KeyResolveMiddlware<string, IHeuristic>(PathfindingAlgorithms)));
                builder.RegisterType<LeeAlgorithmMenuItem>().Keyed<IMenuItem>(PathfindingUnits.Algorithms).SingleInstance();
            }
        }

        private sealed class GraphSharing : IComponent
        {
            public void Apply(ContainerBuilder builder)
            {
                builder.RegisterUnit<GraphSharingUnit, ExitMenuItem>(new UnitParamtresFactory());
                builder.RegisterType<GraphSharingUnitMenuItem>().Keyed<IMenuItem>(PathfindingUnits.Main).SingleInstance();

                builder.RegisterType<SaveGraphMenuItem>().Keyed<IMenuItem>(Sharing).SingleInstance();
                builder.RegisterType<LoadGraphMenuItem>().Keyed<IMenuItem>(Sharing).SingleInstance();
                builder.RegisterType<SendGraphMenuItem>().Keyed<IMenuItem>(Sharing).SingleInstance();
                builder.RegisterType<RecieveGraphMenuItem>().Keyed<IMenuItem>(Sharing).SingleInstance();
                builder.RegisterType<SaveGraphOnlyMenuItem>().Keyed<IMenuItem>(Sharing).SingleInstance();
                builder.RegisterType<LoadGraphOnlyMenuItem>().Keyed<IMenuItem>(Sharing).SingleInstance();
                // TODO: Implement saving of pathfinding range only with graph!
                //builder.RegisterType<SavePathfindingRangeMenuItem>().Keyed<IMenuItem>(Sharing).SingleInstance();
                //builder.RegisterType<LoadPathfindingRangeMenuItem>().Keyed<IMenuItem>(Sharing)
                //    .As<ICanRecieveMessage>().SingleInstance();

                builder.RegisterType<FilePathInput>().As<IFilePathInput>().SingleInstance();
                builder.RegisterType<AddressInput>().As<IInput<(string, int)>>().SingleInstance();

                builder.RegisterType<BinaryCoordinatesSerializer>().As<ISerializer<IEnumerable<ICoordinate>>>().SingleInstance();
                builder.RegisterType<BinaryIntArraySerializer>().As<ISerializer<IEnumerable<int>>>().SingleInstance();
                builder.RegisterType<BinaryAlgorithmSerializer>().As<ISerializer<IEnumerable<AlgorithmSerializationDto>>>().SingleInstance();
                builder.RegisterType<BinaryGraphSerializer>().As<ISerializer<GraphSerializationDto>>().SingleInstance();
                builder.RegisterType<PathfindingHistorySerializer>().As<ISerializer<IEnumerable<PathfindingHistorySerializationDto>>>().SingleInstance();

                builder.RegisterDecorator<BufferedSerializer<IEnumerable<PathfindingHistorySerializationDto>>, ISerializer<IEnumerable<PathfindingHistorySerializationDto>>>();
                builder.RegisterDecorator<CompressSerializer<IEnumerable<PathfindingHistorySerializationDto>>, ISerializer<IEnumerable<PathfindingHistorySerializationDto>>>();
                builder.RegisterDecorator<ThreadSafeSerializer<IEnumerable<PathfindingHistorySerializationDto>>, ISerializer<IEnumerable<PathfindingHistorySerializationDto>>>();

                builder.RegisterType<VertexFromInfoFactory>().As<IVertexFromInfoFactory<Vertex>>().SingleInstance();
            }
        }

        private sealed class GraphEditor : IComponent
        {
            public void Apply(ContainerBuilder builder)
            {
                builder.RegisterUnit<GraphEditorUnit, ExitMenuItem>(new UnitParamtresFactory());
                builder.RegisterType<EditorKeysMenuItem>().Keyed<IMenuItem>(PathfindingUnits.KeysUnit).SingleInstance();
                builder.RegisterType<EditorUnitMenuItem>().Keyed<IMenuItem>(PathfindingUnits.Main).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<ReverseVertexMenuItem>().Keyed<IMenuItem>(Editor).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<NeighbourhoodMenuItem>().Keyed<IMenuItem>(Editor).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<ChangeCostMenuItem>().Keyed<IMenuItem>(Editor).SingleInstance().As<ICanRecieveMessage>();
                builder.RegisterType<SmoothGraphMenuItem>().Keyed<IMenuItem>(Editor).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<MeanCost>().As<IMeanCost>().SingleInstance();
            }
        }

        private sealed class ColorEditor : IComponent
        {
            public void Apply(ContainerBuilder builder)
            {
                builder.RegisterUnit<ColorsUnit, ExitMenuItem>(new UnitParamtresFactory());
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

        private sealed class TransitVertices : IComponent
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
                builder.RegisterVisualizionContainer<VisualizedTransit>(Constants.TransitColorKey);
            }
        }

        private sealed class WaveAlgorithms : IComponent
        {
            public void Apply(ContainerBuilder builder)
            {
                var stepRuleResolveMiddleware = new KeyResolveMiddlware<string, IStepRule>(PathfindingAlgorithms);
                var heuristicsResolveMiddleware = new KeyResolveMiddlware<string, IHeuristic>(PathfindingAlgorithms);
                var combinedResolveMiddleware = new CombinedAlgorithmsResolveMiddleware(PathfindingAlgorithms);

                builder.RegisterType<DijkstraAlgorithmMenuItem>().Keyed<IMenuItem>(PathfindingUnits.Algorithms).SingleInstance()
                    .ConfigurePipeline(p => p.Use(stepRuleResolveMiddleware));
                builder.RegisterType<AStarAlgorithmMenuItem>().Keyed<IMenuItem>(PathfindingUnits.Algorithms).SingleInstance()
                    .ConfigurePipeline(p => p.Use(combinedResolveMiddleware));
                builder.RegisterType<IDAStarAlgorithmMenuItem>().Keyed<IMenuItem>(PathfindingUnits.Algorithms).SingleInstance()
                    .ConfigurePipeline(p => p.Use(combinedResolveMiddleware));
                builder.RegisterType<RandomAlgorithmMenuItem>().Keyed<IMenuItem>(PathfindingUnits.Algorithms).SingleInstance();
            }
        }

        private sealed class GreedyAlgorithms : IComponent
        {
            public void Apply(ContainerBuilder builder)
            {
                builder.RegisterType<CostGreedyAlgorithmMenuItem>().Keyed<IMenuItem>(PathfindingUnits.Algorithms).SingleInstance()
                    .ConfigurePipeline(p => p.Use(new KeyResolveMiddlware<string, IStepRule>(PathfindingAlgorithms)));
                builder.RegisterType<DepthFirstAlgorithmMenuItem>().Keyed<IMenuItem>(PathfindingUnits.Algorithms).SingleInstance()
                    .ConfigurePipeline(p => p.Use(new KeyResolveMiddlware<string, IHeuristic>(PathfindingAlgorithms)));
            }
        }

        private sealed class PathfindingVisualization : IComponent
        {
            public void Apply(ContainerBuilder builder)
            {
                builder.RegisterUnit<PathfindingVisualizationUnit, ExitMenuItem>(new VisualizationUnitParametresFactory());
                builder.RegisterType<VisualizationMenuItem>().Keyed<IMenuItem>(PathfindingUnits.Process).SingleInstance();
                builder.RegisterType<ApplyVisualizationMenuItem>().Keyed<IMenuItem>(Visual).SingleInstance();
                builder.RegisterType<EnterAnimationDelayMenuItem>().Keyed<IMenuItem>(Visual).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<VisitedVertexColorMenuItem>().Keyed<IMenuItem>(Colors).SingleInstance();
                builder.RegisterType<EnqueuedVertexColorMenuItem>().Keyed<IMenuItem>(Colors).SingleInstance();
                builder.RegisterVisualizionContainer<VisualizedVisited>(Constants.VisitedColorKey);
                builder.RegisterVisualizionContainer<VisualizedEnqueued>(Constants.EnqueuedColorKey);
            }
        }

        private sealed class PathfindingHistory : IComponent
        {
            public void Apply(ContainerBuilder builder)
            {
                builder.RegisterUnit<PathfindingHistoryUnit, ExitMenuItem>(new UnitParamtresFactory());
                builder.RegisterType<HistoryMenuItem>().Keyed<IMenuItem>(PathfindingUnits.Process).SingleInstance();
                builder.RegisterType<ApplyHistoryMenuItem>().Keyed<IMenuItem>(History).SingleInstance();
                //builder.RegisterType<ClearHistoryMenuItem>().Keyed<IMenuItem>(History).As<ICanRecieveMessage>().SingleInstance();
                builder.RegisterType<ShowHistoryMenuItem>().Keyed<IMenuItem>(History).As<ICanRecieveMessage>().SingleInstance();
            }
        }

        private sealed class VisualizationControl : IComponent
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

        private sealed class PathfindingStatistics : IComponent
        {
            public void Apply(ContainerBuilder builder)
            {
                builder.RegisterUnit<PathfindingStatisticsUnit, ExitMenuItem>(new UnitParamtresFactory());
                builder.RegisterType<StatisticsMenuItem>().Keyed<IMenuItem>(PathfindingUnits.Process).SingleInstance();
                builder.RegisterType<ApplyStatisticsMenuItem>().Keyed<IMenuItem>(PathfindingUnits.Statistics).SingleInstance();
            }
        }

        private sealed class KeysEditor : IComponent
        {
            public void Apply(ContainerBuilder builder)
            {
                builder.RegisterUnit<KeysUnit, ExitMenuItem>(new UnitParamtresFactory());
                builder.RegisterInstance(Keys.Default).As<SettingsBase>().SingleInstance();
                builder.RegisterType<KeysUnitMenuItem>().Keyed<IMenuItem>(PathfindingUnits.Main).SingleInstance();
                builder.RegisterType<RegularKeysMenuItem>().Keyed<IMenuItem>(PathfindingUnits.KeysUnit).SingleInstance();
            }
        }
    }
}
