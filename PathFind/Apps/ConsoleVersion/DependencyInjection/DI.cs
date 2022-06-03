using Algorithm.Base;
using Algorithm.Factory.Interface;
using Algorithm.Interfaces;
using Algorithm.Realizations.StepRules;
using Autofac;
using Autofac.Core;
using Common.Extensions;
using Common.Interface;
using ConsoleVersion.Enums;
using ConsoleVersion.Interface;
using ConsoleVersion.Model;
using ConsoleVersion.ValueInput.UserInput;
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
using System.Reflection;

using static Common.Extensions.MemberInfoExtensions;

namespace ConsoleVersion.DependencyInjection
{
    internal static class DI
    {
        private static readonly Lazy<IContainer> container = new Lazy<IContainer>(Configure);

        public static IContainer Container => container.Value;

        private static Assembly[] Assemblies => AppDomain.CurrentDomain.GetAssemblies();

        private static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConsoleUserEnumInput<Answer>>().As<IInput<Answer>>().SingleInstance();
            builder.RegisterType<ConsoleUserIntInput>().As<IInput<int>>().SingleInstance();
            builder.RegisterType<ConsoleUserStringInput>().As<IInput<string>>().SingleInstance();
            builder.RegisterType<ConsoleUserKeyInput>().As<IInput<ConsoleKey>>().SingleInstance();

            builder.RegisterType<ConsoleKeystrokesHook>().AsSelf().InstancePerDependency().PropertiesAutowired();

            builder.RegisterType<CustomSmoothLevel>().AsSelf().As<ISmoothLevel>().SingleInstance().PropertiesAutowired();

            builder.RegisterType<MainViewModel>().AsSelf().SingleInstance().PropertiesAutowired();
            builder.RegisterAssemblyTypes(Assemblies).Where(type => type.Implements<IViewModel>()).Except<MainViewModel>().AsSelf()
                .PropertiesAutowired().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assemblies).Where(type => type.Implements<IView>()).AsSelf().PropertiesAutowired()
                .OnActivated(OnViewActivated).InstancePerLifetimeScope();

            builder.RegisterType<Messenger>().As<IMessenger>().SingleInstance();
            builder.RegisterType<EndPoints>().As<BaseEndPoints>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<GraphEvents>().As<IGraphEvents>().SingleInstance().PropertiesAutowired();

            builder.RegisterType<FileLog>().As<ILog>().SingleInstance();
            builder.RegisterType<ConsoleLog>().As<ILog>().SingleInstance();
            builder.RegisterType<MailLog>().As<ILog>().SingleInstance();
            builder.RegisterComposite<Logs, ILog>().SingleInstance();

            builder.RegisterComposite<CompositeGraphEvents, IGraphEvents>().SingleInstance();

            builder.RegisterType<KnuthRandom>().As<IRandom>().SingleInstance();
            builder.RegisterType<GraphAssemble>().As<IGraphAssemble>().SingleInstance();
            builder.RegisterType<CostFactory>().As<IVertexCostFactory>().SingleInstance();
            builder.RegisterType<VertexFactory>().As<IVertexFactory>().SingleInstance();
            builder.RegisterType<GraphFieldFactory>().As<IGraphFieldFactory>().SingleInstance();
            builder.RegisterType<Coordinate2DFactory>().As<ICoordinateFactory>().SingleInstance();
            builder.RegisterType<Graph2DFactory>().As<IGraphFactory>().SingleInstance();
            builder.RegisterType<MooreNeighborhoodFactory>().As<INeighborhoodFactory>().SingleInstance();
            builder.RegisterType<RootMeanSquareCost>().As<IMeanCost>().SingleInstance();
            builder.RegisterType<VertexVisualization>().As<IVisualization<Vertex>>().SingleInstance();

            builder.RegisterType<InFileSerializationModule>().As<IGraphSerializationModule>().SingleInstance();
            builder.RegisterType<PathInput>().As<IPathInput>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<BinaryGraphSerializer>().As<IGraphSerializer>().SingleInstance();
            builder.RegisterDecorator<CompressGraphSerializer, IGraphSerializer>();
            //builder.RegisterDecorator<CryptoGraphSerializer, IGraphSerializer>();
            builder.RegisterType<VertexFromInfoFactory>().As<IVertexFromInfoFactory>().SingleInstance();

            builder.RegisterAssemblyTypes(Assemblies).Where(type => type.Implements<IAlgorithmFactory<PathfindingAlgorithm>>())
                .As<IAlgorithmFactory<PathfindingAlgorithm>>().SingleInstance();
            builder.RegisterType<LandscapeStepRule>().As<IStepRule>().SingleInstance();

            return builder.Build();
        }

        private static void OnViewActivated(IActivatedEventArgs<object> e)
        {
            var view = (IView)e.Instance;
            var mainModel = e.Context.Resolve<MainViewModel>();
            view.NewMenuIteration += mainModel.DisplayGraph;
        }
    }
}
