using Algorithm.Base;
using Algorithm.Factory.Interface;
using Algorithm.Interfaces;
using Algorithm.Realizations.StepRules;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Features.Scanning;
using Common.Extensions;
using Common.Interface;
using ConsoleVersion.Interface;
using ConsoleVersion.Model;
#if DEBUG
using ConsoleVersion.ValueInput.ProgrammedInput;
using ConsoleVersion.ValueInput.RandomInput;
#elif !DEBUG
using ConsoleVersion.ValueInput.UserInput;
#endif
using ConsoleVersion.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base.EndPoints;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations;
using GraphLib.Realizations.Factories;
using GraphLib.Realizations.Factories.CoordinateFactories;
using GraphLib.Realizations.Factories.GraphAssembles;
using GraphLib.Realizations.Factories.GraphFactories;
using GraphLib.Realizations.Factories.NeighboursCoordinatesFactories;
using GraphLib.Realizations.Graphs;
using GraphLib.Realizations.MeanCosts;
using GraphLib.Serialization.Interfaces;
using GraphLib.Serialization.Modules;
using GraphLib.Serialization.Serializers;
using GraphLib.Serialization.Serializers.Decorators;
using Logging.Interface;
using Logging.Loggers;
using Random.Interface;
using Random.Realizations.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using static Common.Extensions.MemberInfoExtensions;

namespace ConsoleVersion.DependencyInjection
{
    internal static class DI
    {
        private static readonly Lazy<IContainer> container = new Lazy<IContainer>(Configure);

        public static IContainer Container => container.Value;

        private static readonly Type[] LocalAssemblyTypes = typeof(DI).Assembly.GetTypes();

        private static Assembly AlgorithmsAssembly => typeof(IAlgorithmFactory<>).Assembly;

        private static IContainer Configure()
        {
            var builder = new ContainerBuilder();
#if DEBUG
            builder.RegisterType<ConsoleProgrammedAnswerInput>().As<IInput<Answer>>().SingleInstance();
            builder.RegisterType<ConsoleProgrammedIntInput>().As<IInput<int>>().SingleInstance();
            builder.RegisterType<ConsoleProgrammedStringInput>().As<IInput<string>>().SingleInstance();
            builder.RegisterType<RandomKeyInput>().As<IInput<ConsoleKey>>().SingleInstance();
            builder.RegisterType<RandomTimeSpanInput>().As<IInput<TimeSpan>>().SingleInstance();
#elif !DEBUG
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
            builder.RegisterType<EndPoints>().As<BaseEndPoints<Vertex>>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<GraphEvents>().As<IGraphEvents<Vertex>>().SingleInstance().PropertiesAutowired();

            builder.RegisterType<FileLog>().As<ILog>().SingleInstance();
            builder.RegisterType<ConsoleLog>().As<ILog>().SingleInstance();
            builder.RegisterType<MailLog>().As<ILog>().SingleInstance();
            builder.RegisterComposite<Logs, ILog>().SingleInstance();

            builder.RegisterComposite<CompositeGraphEvents<Vertex>, IGraphEvents<Vertex>>().SingleInstance();

            builder.RegisterType<PseudoRandom>().As<IRandom>().SingleInstance();
            builder.RegisterType<GraphAssemble<Graph2D<Vertex>, Vertex>>().As<IGraphAssemble<Graph2D<Vertex>, Vertex>>().SingleInstance();
            builder.RegisterType<CostFactory>().As<IVertexCostFactory>().SingleInstance();
            builder.RegisterType<VertexFactory>().As<IVertexFactory<Vertex>>().SingleInstance();
            builder.RegisterType<GraphFieldFactory>().As<IGraphFieldFactory<Graph2D<Vertex>, Vertex, GraphField>>().SingleInstance();
            builder.RegisterType<Coordinate2DFactory>().As<ICoordinateFactory>().SingleInstance();
            builder.RegisterType<Graph2DFactory<Vertex>>().As<IGraphFactory<Graph2D<Vertex>, Vertex>>().SingleInstance();
            builder.RegisterType<MooreNeighborhoodFactory>().As<INeighborhoodFactory>().SingleInstance();
            builder.RegisterType<RootMeanSquareCost>().As<IMeanCost>().SingleInstance();
            builder.RegisterType<VertexVisualization>().As<IVisualization<Vertex>>().SingleInstance();

            builder.RegisterType<InFileSerializationModule<Graph2D<Vertex>, Vertex>>().As<IGraphSerializationModule<Graph2D<Vertex>, Vertex>>().SingleInstance();
            builder.RegisterType<PathInput>().As<IPathInput>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<BinaryGraphSerializer<Graph2D<Vertex>, Vertex>>().As<IGraphSerializer<Graph2D<Vertex>, Vertex>>().SingleInstance();
            builder.RegisterDecorator<CompressGraphSerializer<Graph2D<Vertex>, Vertex>, IGraphSerializer<Graph2D<Vertex>, Vertex>>();
            builder.RegisterDecorator<CryptoGraphSerializer<Graph2D<Vertex>, Vertex>, IGraphSerializer<Graph2D<Vertex>, Vertex>>();
            builder.RegisterDecorator<ThreadSafeGraphSerializer<Graph2D<Vertex>, Vertex>, IGraphSerializer<Graph2D<Vertex>, Vertex>>();
            builder.RegisterType<VertexFromInfoFactory>().As<IVertexFromInfoFactory<Vertex>>().SingleInstance();

            AlgorithmsAssembly.GetTypes().Where(type => type.Implements<IAlgorithmFactory<PathfindingProcess>>())
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