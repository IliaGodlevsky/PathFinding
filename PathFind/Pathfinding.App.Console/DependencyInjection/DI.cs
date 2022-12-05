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
using Pathfinding.App.Console.Views;
using Pathfinding.App.Console.Model.InProcessActions;
using Pathfinding.App.Console.Model.PathfindingActions;

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

        private enum ParentModel { Main, Pathfinding, Process }

        private static readonly Lazy<IContainer> container = new Lazy<IContainer>(Configure);

        public static IContainer Container => container.Value;

        private static readonly Assembly[] Assemblies = AppDomain.CurrentDomain.GetAssemblies();

        private static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConsoleUserAnswerInput>().As<IInput<Answer>>().SingleInstance();
            builder.RegisterType<ConsoleUserIntInput>().As<IInput<int>>().SingleInstance();
            builder.RegisterType<ConsoleUserStringInput>().As<IInput<string>>().SingleInstance();
            builder.RegisterType<ConsoleUserKeyInput>().As<IInput<ConsoleKey>>().SingleInstance();
            builder.RegisterType<ConsoleUserTimeSpanInput>().As<IInput<TimeSpan>>().SingleInstance();

            builder.RegisterType<ConsoleKeystrokesHook>().AsSelf().SingleInstance().PropertiesAutowired();

            builder.RegisterType<MainViewModel>().AsSelf().PropertiesAutowired().SingleInstance().AutoActivate().OnActivated(OnMainViewModelActivated);
            builder.RegisterType<PathfindingViewModel>().Keyed<IViewModel>(ParentModel.Main).PropertiesAutowired().AutoActivate().SingleInstance().OnActivated(OnPathfindingModelActivated);
            builder.RegisterType<PathfindingProcessViewModel>().Keyed<IViewModel>(ParentModel.Pathfinding).AutoActivate().PropertiesAutowired().SingleInstance().OnActivated(OnProcessViewModelActivated);
            builder.RegisterType<GraphCreatingViewModel>().Keyed<IViewModel>(ParentModel.Main).PropertiesAutowired().SingleInstance().AutoActivate();
            builder.RegisterType<GraphLoadViewModel>().Keyed<IViewModel>(ParentModel.Main).PropertiesAutowired().SingleInstance().AutoActivate();
            builder.RegisterType<GraphSaveViewModel>().Keyed<IViewModel>(ParentModel.Main).PropertiesAutowired().SingleInstance().AutoActivate();
            builder.RegisterType<GraphSmoothViewModel>().Keyed<IViewModel>(ParentModel.Main).PropertiesAutowired().SingleInstance().AutoActivate();
            builder.RegisterType<PathfindingHistoryViewModel>().Keyed<IViewModel>(ParentModel.Process).PropertiesAutowired().SingleInstance().AutoActivate();
            builder.RegisterType<PathfindingProcessChooseViewModel>().Keyed<IViewModel>(ParentModel.Process).AutoActivate().PropertiesAutowired().SingleInstance();
            builder.RegisterType<PathfindingRangeViewModel>().Keyed<IViewModel>(ParentModel.Pathfinding).AutoActivate().SingleInstance().PropertiesAutowired();
            builder.RegisterType<PathfindingVisualizationViewModel>().Keyed<IViewModel>(ParentModel.Process).AutoActivate().SingleInstance().PropertiesAutowired();
            builder.RegisterType<VertexStateViewModel>().Keyed<IViewModel>(ParentModel.Main).AutoActivate().SingleInstance().PropertiesAutowired();

            builder.RegisterType<View>().AsSelf().PropertiesAutowired().InstancePerDependency();

            builder.RegisterType<PathfindingVisualizationViewModel>().As<IViewModel>().InstancePerLifetimeScope().PropertiesAutowired()
                .OnActivated(OnVisualizationViewModelActivated);           
            builder.RegisterType<SpeedUpAnimation>().As<IAnimationSpeedAction>().WithMetadata(Key, ConsoleKey.UpArrow).SingleInstance();
            builder.RegisterType<SlowDownAnimation>().As<IAnimationSpeedAction>().WithMetadata(Key, ConsoleKey.DownArrow).SingleInstance();
            builder.RegisterType<ResumeAlgorithm>().As<IPathfindingAction>().WithMetadata(Key, ConsoleKey.Enter).SingleInstance();
            builder.RegisterType<PauseAlgorithm>().As<IPathfindingAction>().WithMetadata(Key, ConsoleKey.P).SingleInstance();
            builder.RegisterType<InterruptAlgorithm>().As<IPathfindingAction>().WithMetadata(Key, ConsoleKey.Escape).SingleInstance();
            builder.RegisterType<PathfindingStepByStep>().As<IPathfindingAction>().WithMetadata(Key, ConsoleKey.W).SingleInstance();

            builder.RegisterType<ConsoleVertexReverseModule>().AsSelf().SingleInstance();
            builder.RegisterType<ConsoleVertexChangeCostModule>().AsSelf().PropertiesAutowired();

            builder.RegisterType<FileLog>().As<ILog>().SingleInstance();
            builder.RegisterType<ColorConsoleLog>().As<ILog>().SingleInstance();
            builder.RegisterType<MailLog>().As<ILog>().SingleInstance();
            builder.RegisterComposite<Logs, ILog>().SingleInstance();

            builder.RegisterType<Messenger>().As<IMessenger>().SingleInstance();

            builder.RegisterType<VisualPathfindingRange<Vertex>>().As<IPathfindingRange<Vertex>>().SingleInstance();
            builder.RegisterType<PathfindingRangeBuilder<Vertex>>().As<IPathfindingRangeBuilder<Vertex>>()
                .SingleInstance().OnActivated(OnPathfindingRangeBuilderActivated);
            builder.RegisterType<IncludeTargetVertex<Vertex>>().Keyed<Command>(CommandType.Include).WithMetadata(Order, 2).SingleInstance();
            builder.RegisterType<IncludeTransitVertex<Vertex>>().Keyed<Command>(CommandType.Include).WithMetadata(Order, 3).SingleInstance();
            builder.RegisterType<IncludeSourceVertex<Vertex>>().Keyed<Command>(CommandType.Include).WithMetadata(Order, 1).SingleInstance();
            builder.RegisterType<ExcludeTargetVertex<Vertex>>().Keyed<Command>(CommandType.Exclude).WithMetadata(Order, 2).SingleInstance();
            builder.RegisterType<ExcludeSourceVertex<Vertex>>().Keyed<Command>(CommandType.Exclude).WithMetadata(Order, 1).SingleInstance();
            builder.RegisterType<ReplaceTransitVerticesModule<Vertex>>().AsSelf().As<IUndo>().SingleInstance();

            builder.RegisterComposite<CompositeUndo, IUndo>().SingleInstance();

            builder.RegisterType<PseudoRandom>().As<IRandom>().SingleInstance();
            
            builder.RegisterType<GraphAssemble<Graph2D<Vertex>, Vertex>>().As<IGraphAssemble<Graph2D<Vertex>, Vertex>>().SingleInstance();
            builder.RegisterType<CostFactory>().As<IVertexCostFactory>().SingleInstance();
            builder.RegisterType<VertexFactory>().As<IVertexFactory<Vertex>>().SingleInstance();
            builder.RegisterType<GraphFieldFactory>().As<IGraphFieldFactory<Graph2D<Vertex>, Vertex, GraphField>>().SingleInstance();
            builder.RegisterType<Coordinate2DFactory>().As<ICoordinateFactory>().SingleInstance();
            builder.RegisterType<Graph2DFactory<Vertex>>().As<IGraphFactory<Graph2D<Vertex>, Vertex>>().SingleInstance();
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

            builder.RegisterAssemblyTypes(typeof(IAlgorithmFactory<>).Assembly)
                .AssignableTo<AlgorithmFactory>().As<AlgorithmFactory>().SingleInstance();
            builder.RegisterType<LandscapeStepRule>().As<IStepRule>().SingleInstance();

            return builder.Build();
        }

        private static void OnProcessViewModelActivated(IActivatedEventArgs<PathfindingProcessViewModel> args)
        {
            args.Instance.Children = args.Context.ResolveKeyed(ParentModel.Process);
        }

        private static void OnPathfindingModelActivated(IActivatedEventArgs<PathfindingViewModel> args)
        {
            args.Instance.Children = args.Context.ResolveKeyed(ParentModel.Pathfinding);
        }

        private static void OnMainViewModelActivated(IActivatedEventArgs<MainViewModel> args)
        {
            args.Instance.Children = args.Context.ResolveKeyed(ParentModel.Main);
        }

        private static void OnVisualizationViewModelActivated(IActivatedEventArgs<PathfindingVisualizationViewModel> args)
        {
            args.Instance.PathfindingActions = args.Context.ResolveWithMetadata<IPathfindingAction>();
            args.Instance.AnimationActions = args.Context.ResolveWithMetadata<IAnimationSpeedAction>();
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

        private static IReadOnlyCollection<IViewModel> ResolveKeyed(this IComponentContext context, ParentModel key)
        {
            return context.ResolveKeyed<IEnumerable<IViewModel>>(key).ToReadOnly();
        }
    }
}