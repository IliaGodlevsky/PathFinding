using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Features.Scanning;
#if DEBUG
using Pathfinding.App.Console.ValueInput.UserInput;
#elif !DEBUG
using Pathfinding.App.Console.ValueInput.UserInput;
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
using Pathfinding.GraphLib.Core.Interface;
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
using Pathfinding.GraphLib.Subscriptions;
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
using System.Reflection;

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
            builder.RegisterType<ConsoleUserAnswerInput>().As<IInput<Answer>>().SingleInstance();
            builder.RegisterType<ConsoleUserIntInput>().As<IInput<int>>().SingleInstance();
            builder.RegisterType<ConsoleUserStringInput>().As<IInput<string>>().SingleInstance();
            builder.RegisterType<ConsoleUserKeyInput>().As<IInput<ConsoleKey>>().SingleInstance();
            builder.RegisterType<ConsoleUserTimeSpanInput>().As<IInput<TimeSpan>>().SingleInstance();
#endif
            builder.RegisterType<ConsoleKeystrokesHook>().AsSelf().InstancePerDependency().PropertiesAutowired();

            builder.RegisterType<MainViewModel>().AsSelf().As<ICache<Graph2D<Vertex>>>().SingleInstance().PropertiesAutowired();
            LocalAssemblyTypes.Where(type => type.Implements<IViewModel>()).Register(builder)
                .Except<MainViewModel>().AsSelf().PropertiesAutowired().InstancePerLifetimeScope();
            LocalAssemblyTypes.Where(type => type.Implements<IView>()).Register(builder)
                .AsSelf().PropertiesAutowired().OnActivated(OnViewActivated).InstancePerLifetimeScope();

            builder.RegisterType<Messenger>().As<IMessenger>().SingleInstance();
            builder.RegisterType<ConsoleVisualPathfindingRange>().As<VisualPathfindingRange<Vertex>>().SingleInstance();

            builder.RegisterType<FileLog>().As<ILog>().SingleInstance();
            builder.RegisterType<ConsoleLog>().As<ILog>().SingleInstance();
            builder.RegisterType<MailLog>().As<ILog>().SingleInstance();
            builder.RegisterComposite<Logs, ILog>().SingleInstance();

            LocalAssemblyTypes.Where(type => type.Implements<IGraphSubscription<Vertex>>())
                .Register(builder).AsImplementedInterfaces().SingleInstance().PropertiesAutowired();
            builder.RegisterComposite<GraphSubscriptions<Vertex>, IGraphSubscription<Vertex>>().SingleInstance();

            builder.RegisterType<PseudoRandom>().As<IRandom>().SingleInstance();
            builder.RegisterType<GraphAssemble<Graph2D<Vertex>, Vertex>>()
                .As<IGraphAssemble<Graph2D<Vertex>, Vertex>>().SingleInstance();
            builder.RegisterType<CostFactory>().As<IVertexCostFactory>().SingleInstance();
            builder.RegisterType<VertexFactory>().As<IVertexFactory<Vertex>>().SingleInstance();
            builder.RegisterType<GraphFieldFactory>().As<IGraphFieldFactory<Graph2D<Vertex>, Vertex, GraphField>>().SingleInstance();
            builder.RegisterType<Coordinate2DFactory>().As<ICoordinateFactory>().SingleInstance();
            builder.RegisterType<Graph2DFactory<Vertex>>().As<IGraphFactory<Graph2D<Vertex>, Vertex>>().SingleInstance();
            builder.RegisterType<MooreNeighborhoodFactory>().As<INeighborhoodFactory>().SingleInstance();
            builder.RegisterType<RootMeanSquareCost>().As<IMeanCost>().SingleInstance();
            builder.RegisterType<VertexVisualization>().As<IVisualization<Vertex>>().SingleInstance();

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

        private static void OnViewActivated(IActivatedEventArgs<object> e)
        {
            var view = (IView)e.Instance;
            var mainModel = e.Context.Resolve<MainViewModel>();
            view.IterationStarted += mainModel.DisplayGraph;
        }

        private static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> Register(this IEnumerable<Type> types,
            ContainerBuilder builder)
        {
            return builder.RegisterTypes(types.ToArray());
        }
    }
}