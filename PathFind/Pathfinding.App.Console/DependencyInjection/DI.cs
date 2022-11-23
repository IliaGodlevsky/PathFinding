using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Features.Scanning;
#if DEBUG
using Pathfinding.App.Console.ValueInput.UserInput;
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
using Pathfinding.AlgorithmLib.Core.Realizations.Heuristics;
using Pathfinding.App.Console.ValueInput.RecordingInput;
using Pathfinding.App.Console.ValueInput.ProgrammedInput.FromFile;
using Pathfinding.App.Console.ValueInput.RandomInput;

namespace Pathfinding.App.Console.DependencyInjection
{
    internal static class DI
    {
        private static readonly Lazy<IContainer> container = new Lazy<IContainer>(Configure);

        public static IContainer Container => container.Value;

        private static readonly Type[] LocalAssemblyTypes = typeof(DI).Assembly.GetTypes();

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
            builder.RegisterType<FromFileProgrammedAnswerInput>().As<IInput<Answer>>().SingleInstance();
            builder.RegisterType<FromFileProgrammedIntInput>().As<IInput<int>>().SingleInstance();
            builder.RegisterType<FromFileProgrammedStringInput>().As<IInput<string>>().SingleInstance();
            builder.RegisterType<RandomKeyInput>().As<IInput<ConsoleKey>>().SingleInstance();
            builder.RegisterType<RandomTimeSpanInput>().As<IInput<TimeSpan>>().SingleInstance();
#endif
            builder.RegisterType<ConsoleKeystrokesHook>().AsSelf().SingleInstance().PropertiesAutowired();

            builder.RegisterType<MainViewModel>().AsSelf().PropertiesAutowired().AsImplementedInterfaces().SingleInstance();
            LocalAssemblyTypes.Where(type => type.Implements<IViewModel>()).Where(type => !type.IsInstancePerLifetimeScope())
                .Register(builder).Except<MainViewModel>().AsSelf().AsImplementedInterfaces().PropertiesAutowired();
            LocalAssemblyTypes.Where(type => type.Implements<IViewModel>()).Where(type => type.IsInstancePerLifetimeScope())
                .Register(builder).AsSelf().AsImplementedInterfaces().PropertiesAutowired().InstancePerLifetimeScope();
            LocalAssemblyTypes.Where(type => type.Implements<IView>()).Register(builder).AsSelf().PropertiesAutowired();

            builder.RegisterType<VertexVisualization>().As<IVisualization<Vertex>>().SingleInstance();

            builder.RegisterType<FileLog>().As<ILog>().SingleInstance();
            builder.RegisterType<ColorConsoleLog>().As<ILog>().SingleInstance();
            builder.RegisterType<MailLog>().As<ILog>().SingleInstance();
            builder.RegisterComposite<Logs, ILog>().SingleInstance();

            builder.RegisterType<Messenger>().As<IMessenger>().SingleInstance();
            builder.RegisterType<ConsolePathfindingRangeAdapter>().AsSelf().As<PathfindingRangeAdapter<Vertex>>()
                .As<IPathfindingRangeAdapter<Vertex>>().SingleInstance();

            builder.RegisterType<PseudoRandom>().As<IRandom>().SingleInstance();
            
            builder.RegisterType<PathfindingRangeFactory>().As<IPathfindingRangeFactory>().SingleInstance();
            builder.RegisterType<GraphAssemble<Graph2D<Vertex>, Vertex>>().As<IGraphAssemble<Graph2D<Vertex>, Vertex>>().SingleInstance();
            builder.RegisterType<CostFactory>().As<IVertexCostFactory>().SingleInstance();
            builder.RegisterType<VertexFactory>().As<IVertexFactory<Vertex>>().SingleInstance();
            builder.RegisterType<GraphFieldFactory>().As<IGraphFieldFactory<Graph2D<Vertex>, Vertex, GraphField>>().SingleInstance();
            builder.RegisterType<Coordinate2DFactory>().As<ICoordinateFactory>().SingleInstance();
            builder.RegisterType<Graph2DFactory<Vertex>>().As<IGraphFactory<Graph2D<Vertex>, Vertex>>().SingleInstance();
            builder.RegisterType<MooreNeighborhoodFactory>().As<INeighborhoodFactory>().SingleInstance();

            builder.RegisterType<RootMeanSquareCost>().As<IMeanCost>().SingleInstance();

            builder.RegisterType<InFileSerializationModule<Graph2D<Vertex>, Vertex>>()
                .As<IGraphSerializationModule<Graph2D<Vertex>, Vertex>>().SingleInstance();
            builder.RegisterType<PathInput>().As<IPathInput>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<BinaryGraphSerializer<Graph2D<Vertex>, Vertex>>()
                .As<IGraphSerializer<Graph2D<Vertex>, Vertex>>().SingleInstance();
            builder.RegisterDecorator<CompressGraphSerializer<Graph2D<Vertex>, Vertex>, IGraphSerializer<Graph2D<Vertex>, Vertex>>();
            builder.RegisterDecorator<CryptoGraphSerializer<Graph2D<Vertex>, Vertex>, IGraphSerializer<Graph2D<Vertex>, Vertex>>();
            builder.RegisterDecorator<ThreadSafeGraphSerializer<Graph2D<Vertex>, Vertex>, IGraphSerializer<Graph2D<Vertex>, Vertex>>();
            builder.RegisterType<VertexFromInfoFactory>().As<IVertexFromInfoFactory<Vertex>>().SingleInstance();

            typeof(IAlgorithmFactory<>).Assembly.GetTypes().Where(type => type.Implements<IAlgorithmFactory<PathfindingProcess>>())
                .Register(builder).As<IAlgorithmFactory<PathfindingProcess>>().SingleInstance();
            builder.RegisterType<LandscapeStepRule>().As<IStepRule>().SingleInstance();

            return builder.Build();
        }

        private static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> Register(this IEnumerable<Type> types,
            ContainerBuilder builder)
        {
            return builder.RegisterTypes(types.ToArray());
        }
    }
}