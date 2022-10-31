using Algorithm.Base;
using Algorithm.Factory.Interface;
using Algorithm.Interfaces;
using Algorithm.Realizations.StepRules;
using Autofac;
using Common.Extensions;
using Common.Interface;
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
using System.Reflection;
using WPFVersion.Extensions;
using WPFVersion.Interface;
using WPFVersion.Model;
using WPFVersion.ViewModel;

namespace WPFVersion.DependencyInjection
{
    internal static class DI
    {
        private static readonly Lazy<IContainer> container = new Lazy<IContainer>(Configure);

        private static Assembly[] Assemblies => AppDomain.CurrentDomain.GetAssemblies();

        public static IContainer Container => container.Value;

        private static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindowViewModel>().As<ICache<Graph2D<Vertex>>>().AsSelf().SingleInstance();
            builder.RegisterAssemblyTypes(Assemblies).Where(type => type.Implements<IViewModel>()).AsSelf().InstancePerDependency();
            builder.RegisterAssemblyTypes(Assemblies).Where(type => type.IsAppWindow()).AsSelf().InstancePerDependency();

            builder.RegisterType<Messenger>().As<IMessenger>().SingleInstance();
            builder.RegisterType<EndPoints>().As<BaseEndPoints<Vertex>>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<GraphEvents>().As<IGraphEvents<Vertex>>().SingleInstance();

            builder.RegisterType<FileLog>().As<ILog>().SingleInstance();
            builder.RegisterType<MessageBoxLog>().As<ILog>().SingleInstance();
            builder.RegisterType<MailLog>().As<ILog>().SingleInstance();
            builder.RegisterComposite<Logs, ILog>().SingleInstance();

            builder.RegisterComposite<CompositeGraphEvents<Vertex>, IGraphEvents<Vertex>>().SingleInstance();

            builder.RegisterType<CryptoRandom>().As<IRandom>().SingleInstance();
            builder.RegisterDecorator<ThreadSafeRandom, IRandom>();
            builder.RegisterType<GraphAssemble<Graph2D<Vertex>, Vertex>>().As<IGraphAssemble<Graph2D<Vertex>, Vertex>>().SingleInstance();
            builder.RegisterType<VertexFactory>().As<IVertexFactory<Vertex>>().SingleInstance();
            builder.RegisterType<CostFactory>().As<IVertexCostFactory>().SingleInstance();
            builder.RegisterType<Coordinate2DFactory>().As<ICoordinateFactory>().SingleInstance();
            builder.RegisterType<Graph2DFactory<Vertex>>().As<IGraphFactory<Graph2D<Vertex>, Vertex>>().SingleInstance();
            builder.RegisterType<GraphFieldFactory>().As<IGraphFieldFactory<Graph2D<Vertex>, Vertex, GraphField>>().SingleInstance();
            builder.RegisterType<MooreNeighborhoodFactory>().As<INeighborhoodFactory>().SingleInstance();
            builder.RegisterType<GeometricMeanCost>().As<IMeanCost>().SingleInstance();
            builder.RegisterType<VertexVisualization>().As<IVisualization<Vertex>>().SingleInstance();

            builder.RegisterType<InFileSerializationModule<Graph2D<Vertex>, Vertex>>().As<IGraphSerializationModule<Graph2D<Vertex>, Vertex>>().SingleInstance();
            builder.RegisterType<PathInput>().As<IPathInput>().SingleInstance();
            builder.RegisterType<XmlGraphSerializer<Graph2D<Vertex>, Vertex>>().As<IGraphSerializer<Graph2D<Vertex>, Vertex>>().SingleInstance();
            builder.RegisterDecorator<CompressGraphSerializer<Graph2D<Vertex>, Vertex>, IGraphSerializer<Graph2D<Vertex>, Vertex>>();
            builder.RegisterDecorator<CryptoGraphSerializer<Graph2D<Vertex>, Vertex>, IGraphSerializer<Graph2D<Vertex>, Vertex>>();
            builder.RegisterDecorator<ThreadSafeGraphSerializer<Graph2D<Vertex>, Vertex>, IGraphSerializer<Graph2D<Vertex>, Vertex>>();
            builder.RegisterType<VertexFromInfoFactory>().As<IVertexFromInfoFactory<Vertex>>().SingleInstance();

            builder.RegisterAssemblyTypes(Assemblies)
                .Where(type => type.Implements<IAlgorithmFactory<PathfindingAlgorithm>>())
                .As<IAlgorithmFactory<PathfindingAlgorithm>>().SingleInstance();

            builder.RegisterType<DefaultStepRule>().As<IStepRule>().SingleInstance();
            builder.RegisterDecorator<CardinalStepRule, IStepRule>();

            return builder.Build();
        }
    }
}