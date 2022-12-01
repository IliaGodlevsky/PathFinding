using Autofac;
#if DEBUG
#elif !DEBUG
using Pathfinding.App.Console.ValueInput.ProgrammedInput;
using Pathfinding.App.Console.ValueInput.RandomInput;
#endif
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
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.ValueInput.UserInput;
using Shared.Executable;
using Autofac.Core;
using Autofac.Features.Metadata;
using System.Reflection;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Core.Modules.Commands;
using Pathfinding.GraphLib.Core.Modules;

namespace Pathfinding.App.Console.DependencyInjection
{
    using Command = IPathfindingRangeCommand<Vertex>;
    using Commands = IReadOnlyCollection<IPathfindingRangeCommand<Vertex>>;
    using AlgorithmFactory = IAlgorithmFactory<PathfindingProcess>;
    using GraphSerializer = IGraphSerializer<Graph2D<Vertex>, Vertex>;

    internal static class DI
    {
        private const string Order = "Order";

        private enum CommandType { Include, Exclude }

        private static readonly Lazy<IContainer> container = new Lazy<IContainer>(Configure);

        public static IContainer Container => container.Value;

        private static readonly Assembly[] Assemblies = AppDomain.CurrentDomain.GetAssemblies();

        private static IContainer Configure()
        {
            var builder = new ContainerBuilder();
#if !DEBUG
            builder.RegisterType<ConsoleProgrammedAnswerInput>().As<IInput<Answer>>().SingleInstance();
            builder.RegisterType<ConsoleProgrammedIntInput>().As<IInput<int>>().SingleInstance();
            builder.RegisterType<ConsoleProgrammedStringInput>().As<IInput<string>>().SingleInstance();
            builder.RegisterType<RandomKeyInput>().As<IInput<ConsoleKey>>().SingleInstance();
            builder.RegisterType<RandomTimeSpanInput>().As<IInput<TimeSpan>>().SingleInstance();
#elif DEBUG
            builder.RegisterType<ConsoleUserAnswerInput>().As<IInput<Answer>>().SingleInstance();
            builder.RegisterType<ConsoleUserIntInput>().As<IInput<int>>().SingleInstance();
            builder.RegisterType<ConsoleUserStringInput>().As<IInput<string>>().SingleInstance();
            builder.RegisterType<ConsoleUserKeyInput>().As<IInput<ConsoleKey>>().SingleInstance();
            builder.RegisterType<ConsoleUserTimeSpanInput>().As<IInput<TimeSpan>>().SingleInstance();
#endif
            builder.RegisterType<ConsoleKeystrokesHook>().AsSelf().SingleInstance().PropertiesAutowired();

            builder.RegisterType<MainViewModel>().AsSelf().PropertiesAutowired().AsImplementedInterfaces().SingleInstance();
            builder.RegisterAssemblyTypes(Assemblies).Where(type => type.Implements<IViewModel>())
                .Where(type => !type.IsInstancePerLifetimeScope()).Except<MainViewModel>().AsSelf().AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterAssemblyTypes(Assemblies).Where(type => type.Implements<IViewModel>())
                .Where(type => type.IsInstancePerLifetimeScope()).AsSelf().AsImplementedInterfaces().PropertiesAutowired().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assemblies).Where(type => type.Implements<IView>()).AsSelf().PropertiesAutowired();

            builder.RegisterType<ConsoleVertexReverseModule>().AsSelf().SingleInstance();
            builder.RegisterType<ConsoleVertexChangeCostModule>().AsSelf().PropertiesAutowired();
            builder.RegisterType<FileLog>().As<ILog>().SingleInstance();
            builder.RegisterType<ColorConsoleLog>().As<ILog>().SingleInstance();
            builder.RegisterType<MailLog>().As<ILog>().SingleInstance();
            builder.RegisterComposite<Logs, ILog>().SingleInstance();

            builder.RegisterType<Messenger>().As<IMessenger>().SingleInstance();

            builder.RegisterType<VisualPathfindingRange<Vertex>>().As<IPathfindingRange<Vertex>>().SingleInstance();
            builder.RegisterType<PathfindingRangeBuilder<Vertex>>().As<IPathfindingRangeBuilder<Vertex>>()
                .SingleInstance().OnActivated(OnPathfindingRangeActivated);
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
                .Where(type => type.Implements<AlgorithmFactory>()).As<AlgorithmFactory>().SingleInstance();
            builder.RegisterType<LandscapeStepRule>().As<IStepRule>().SingleInstance();

            return builder.Build();
        }

        private static void OnPathfindingRangeActivated(IActivatedEventArgs<PathfindingRangeBuilder<Vertex>> args)
        {
            args.Instance.IncludeCommands = ResolveKeyed(args.Context, CommandType.Include);
            args.Instance.ExcludeCommands = ResolveKeyed(args.Context, CommandType.Exclude);
        }

        private static Commands ResolveKeyed(IComponentContext context, CommandType key)
        {
            return context.ResolveKeyed<IEnumerable<Meta<Command>>>(key)
                .OrderBy(x => x.Metadata[Order])
                .Select(x => x.Value)
                .ToReadOnly();
        }
    }
}