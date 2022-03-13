using Algorithm.Factory;
using Algorithm.Interfaces;
using Algorithm.Realizations.StepRules;
using Autofac;
using Autofac.Core;
using Common.Extensions;
using Common.Interface;
using ConsoleVersion.Enums;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Messages;
using ConsoleVersion.Model;
using ConsoleVersion.ValueInput.UserInput;
using ConsoleVersion.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base.EndPoints;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Factories;
using GraphLib.Realizations.Factories.CoordinateFactories;
using GraphLib.Realizations.Factories.GraphAssembles;
using GraphLib.Realizations.Factories.GraphFactories;
using GraphLib.Realizations.Factories.NeighboursCoordinatesFactories;
using GraphLib.Realizations.MeanCosts;
using GraphLib.Serialization;
using GraphLib.Serialization.Interfaces;
using GraphLib.Serialization.Serializers;
using Logging.Interface;
using Logging.Loggers;
using Random.Interface;
using Random.Realizations.Generators;
using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleVersion.DependencyInjection
{
    internal static class DI
    {
        public static IContainer Container => container.Value;

        private const string GraphAssemble = nameof(GraphAssemble);
        private static Assembly[] Assemblies => AppDomain.CurrentDomain.GetAssemblies();

        private static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConsoleUserEnumInput<Answer>>().As<IInput<Answer>>().SingleInstance();
            builder.RegisterType<ConsoleUserIntInput>().As<IInput<int>>().SingleInstance();
            builder.RegisterType<ConsoleUserStringInput>().As<IInput<string>>().SingleInstance(); 
            builder.RegisterType<ConsoleUserKeyInput>().As<IInput<ConsoleKey>>().SingleInstance();

            builder.RegisterType<MainViewModel>().AsSelf().SingleInstance().PropertiesAutowired();
            builder.RegisterType<EndPointsViewModel>().AsSelf().PropertiesAutowired()
                .InstancePerLifetimeScope().OnActivated(OnEndPointsViewModelActivated);
            builder.RegisterAssemblyTypes(Assemblies).Where(Implements<IViewModel>).Except<MainViewModel>()
                .Except<EndPointsViewModel>().AsSelf().PropertiesAutowired().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assemblies).Where(Implements<IView>).AsSelf().PropertiesAutowired()
                .OnActivated(OnViewActivated).InstancePerLifetimeScope();

            builder.RegisterType<Messenger>().As<IMessenger>().SingleInstance();
            builder.RegisterType<EndPoints>().As<BaseEndPoints>().SingleInstance();
            builder.RegisterType<VertexEventHolder>().As<IVertexEventHolder>().SingleInstance().PropertiesAutowired();

            builder.RegisterType<FileLog>().As<ILog>().SingleInstance();
            builder.RegisterType<ConsoleLog>().As<ILog>().SingleInstance();
            builder.RegisterType<MailLog>().As<ILog>().SingleInstance();
            builder.RegisterComposite<Logs, ILog>().SingleInstance();

            builder.RegisterType<KnuthRandom>().As<IRandom>().SingleInstance();
            builder.RegisterType<GraphAssemble>().As<IGraphAssemble>().SingleInstance().Named<IGraphAssemble>(GraphAssemble);
            builder.Register(RegisterSmoothedGraphAssemble).As<IGraphAssemble>().SingleInstance();
            builder.RegisterType<CostFactory>().As<IVertexCostFactory>().SingleInstance();
            builder.RegisterType<VertexFactory>().As<IVertexFactory>().SingleInstance();
            builder.RegisterType<GraphFieldFactory>().As<IGraphFieldFactory>().SingleInstance();
            builder.RegisterType<Coordinate2DFactory>().As<ICoordinateFactory>().SingleInstance();
            builder.RegisterType<Graph2DFactory>().As<IGraphFactory>().SingleInstance();
            builder.RegisterType<MooreNeighborhoodFactory>().As<INeighborhoodFactory>().SingleInstance();
            builder.RegisterType<RootMeanSquareCost>().As<IMeanCost>().SingleInstance();
            builder.RegisterType<VertexVisualization>().As<IVisualization<Vertex>>().SingleInstance();

            builder.RegisterType<GraphSerializationModule>().AsSelf().SingleInstance();
            builder.RegisterType<PathInput>().As<IPathInput>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<GraphSerializer>().As<IGraphSerializer>().SingleInstance();
            builder.RegisterDecorator<CompressGraphSerializer, IGraphSerializer>();
            builder.RegisterDecorator<CryptoGraphSerializer, IGraphSerializer>();
            builder.RegisterType<BinaryFormatter>().As<IFormatter>().SingleInstance();
            builder.RegisterType<VertexFromInfoFactory>().As<IVertexFromInfoFactory>().SingleInstance();

            builder.RegisterAssemblyTypes(Assemblies).Where(Implements<IAlgorithmFactory>)
                .As<IAlgorithmFactory>().SingleInstance();
            builder.RegisterType<LandscapeStepRule>().As<IStepRule>().SingleInstance();

            return builder.Build();
        }

        private static void OnViewActivated(IActivatedEventArgs<object> e)
        {
            var view = (IView)e.Instance;
            var mainModel = e.Context.Resolve<MainViewModel>();
            view.NewMenuIteration += mainModel.DisplayGraph;
        }

        private static void OnEndPointsViewModelActivated(IActivatedEventArgs<object> e)
        {
            var messenger = e.Context.Resolve<IMessenger>();
            var message = new ClaimGraphMessage(MessageTokens.EndPointsViewModel);
            messenger.Forward(message, MessageTokens.Everyone);
        }

        private static SmoothedGraphAssemble RegisterSmoothedGraphAssemble(IComponentContext context)
        {
            var randomGraphAssemble = context.ResolveNamed<IGraphAssemble>(GraphAssemble);
            var costFactory = context.Resolve<IVertexCostFactory>();
            var meanCost = context.Resolve<IMeanCost>();
            return new SmoothedGraphAssemble(randomGraphAssemble, costFactory, meanCost);
        }

        private static bool Implements<TInterface>(Type type)
        {
            return type.ImplementsAll(typeof(TInterface));
        }

        private static readonly Lazy<IContainer> container = new Lazy<IContainer>(Configure);
    }
}
