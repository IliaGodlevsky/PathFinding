using Algorithm.Base;
using Algorithm.Factory.Interface;
using Autofac;
using Common.Extensions;
using Common.Interface;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base.EndPoints;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations;
using GraphLib.Realizations.Factories.CoordinateFactories;
using GraphLib.Realizations.Factories.GraphAssembles;
using GraphLib.Realizations.Factories.GraphFactories;
using GraphLib.Realizations.Factories.NeighboursCoordinatesFactories;
using GraphLib.Realizations.Graphs;
using GraphLib.Serialization.Interfaces;
using GraphLib.Serialization.Modules;
using GraphLib.Serialization.Serializers;
using GraphLib.Serialization.Serializers.Decorators;
using Pathfinding.Logging.Interface;
using Random.Interface;
using Random.Realizations.Generators;
using System;
using System.Reflection;
using WPFVersion3D.Extensions;
using WPFVersion3D.Interface;
using WPFVersion3D.Model;
using WPFVersion3D.Model3DFactories;
using WPFVersion3D.ViewModel;
using Pathfinding.Logging.Loggers;

namespace WPFVersion3D.DependencyInjection
{
    internal static class DI
    {
        private static readonly Lazy<IContainer> container = new Lazy<IContainer>(Configure);

        public static IContainer Container => container.Value;

        private static Assembly[] Assemblies => AppDomain.CurrentDomain.GetAssemblies();

        private static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindowViewModel>().AsSelf().AsImplementedInterfaces().SingleInstance();
            builder.RegisterAssemblyTypes(Assemblies).Where(type => type.Implements<IViewModel>()).AsSelf().InstancePerDependency();
            builder.RegisterAssemblyTypes(Assemblies).Where(type => type.IsAppWindow()).AsSelf().InstancePerDependency();

            builder.RegisterType<EndPoints>().As<BaseEndPoints<Vertex3D>>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<GraphEvents>().As<IGraphEvents<Vertex3D>>().SingleInstance();

            builder.RegisterType<FileLog>().As<ILog>().SingleInstance();
            builder.RegisterType<MessageBoxLog>().As<ILog>().SingleInstance();
            builder.RegisterType<MailLog>().As<ILog>().SingleInstance();
            builder.RegisterComposite<Logs, ILog>().SingleInstance();

            builder.RegisterComposite<CompositeGraphEvents<Vertex3D>, IGraphEvents<Vertex3D>>().SingleInstance();

            builder.RegisterType<KnuthRandom>().As<IRandom>().SingleInstance();
            builder.RegisterDecorator<ThreadSafeRandom, IRandom>();
            builder.RegisterType<Vertex3DFactory>().As<IVertexFactory<Vertex3D>>().SingleInstance();
            builder.RegisterType<Vertex3DCostFactory>().As<IVertexCostFactory>().SingleInstance();
            builder.RegisterType<Coordinate3DFactory>().As<ICoordinateFactory>().SingleInstance();
            builder.RegisterType<Graph3DFactory<Vertex3D>>().As<IGraphFactory<Graph3D<Vertex3D>, Vertex3D>>().SingleInstance();
            builder.RegisterType<GraphField3DFactory>().As<IGraphFieldFactory<Graph3D<Vertex3D>, Vertex3D, GraphField3D>>().SingleInstance();
            builder.RegisterType<VonNeumannNeighborhoodFactory>().As<INeighborhoodFactory>().SingleInstance();
            builder.RegisterType<CubicModel3DFactory>().As<IModel3DFactory>().SingleInstance();
            builder.RegisterType<GraphAssemble<Graph3D<Vertex3D>, Vertex3D>>().As<IGraphAssemble<Graph3D<Vertex3D>, Vertex3D>>().SingleInstance();
            builder.RegisterType<Messenger>().As<IMessenger>().SingleInstance();
            builder.RegisterType<VertexVisualization>().As<IVisualization<Vertex3D>>().SingleInstance();

            builder.RegisterType<InFileSerializationModule<Graph3D<Vertex3D>, Vertex3D>>().As<IGraphSerializationModule<Graph3D<Vertex3D>, Vertex3D>>().SingleInstance();
            builder.RegisterType<PathInput>().As<IPathInput>().SingleInstance();
            builder.RegisterType<XmlGraphSerializer<Graph3D<Vertex3D>, Vertex3D>>().As<IGraphSerializer<Graph3D<Vertex3D>, Vertex3D>>().SingleInstance();
            builder.RegisterDecorator<CompressGraphSerializer<Graph3D<Vertex3D>, Vertex3D>, IGraphSerializer<Graph3D<Vertex3D>, Vertex3D>>();
            builder.RegisterDecorator<CryptoGraphSerializer<Graph3D<Vertex3D>, Vertex3D>, IGraphSerializer<Graph3D<Vertex3D>, Vertex3D>>();
            builder.RegisterType<Vertex3DFromInfoFactory>().As<IVertexFromInfoFactory<Vertex3D>>().SingleInstance();

            builder.RegisterAssemblyTypes(Assemblies)
                .Where(type => type.Implements<IAlgorithmFactory<PathfindingProcess>>())
                .As<IAlgorithmFactory<PathfindingProcess>>().SingleInstance();

            return builder.Build();
        }
    }
}
