using GalaSoft.MvvmLight.Messaging;
using GraphLib.Serialization.Serializers.Decorators;
using Pathfinding.Logging.Interface;
using System;
using System.Reflection;
using Pathfinding.Logging.Loggers;
using Pathfinding.App.WPF._3D.Model;
using Pathfinding.App.WPF._3D.ViewModel;
using Pathfinding.GraphLib.Subscriptions;
using Pathfinding.GraphLib.Core.Interface;
using Autofac;
using Pathfinding.App.WPF._3D.Extensions;
using Shared.Extensions;
using Pathfinding.App.WPF._3D.Interface;
using Shared.Random.Realizations;
using Shared.Random;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Factory.Realizations.CoordinateFactories;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Realizations.GraphFactories;
using Pathfinding.GraphLib.Factory.Realizations.NeighborhoodFactories;
using Pathfinding.VisualizationLib.Core.Interface;
using Pathfinding.App.WPF._2D.Model;
using Pathfinding.App.WPF._3D.Model3DFactories;
using Pathfinding.GraphLib.Factory.Realizations.GraphAssembles;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Modules;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers.Decorators;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.GraphLib.Core.Realizations.Range;
using Shared.Executable;

namespace Pathfinding.App.WPF._3D.DependencyInjection
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

            builder.RegisterType<Wpf3DPathfindingRange>().As<PathfindingRange<Vertex3D>>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<Wpf3DVertexReverseModule>().As<IGraphSubscription<Vertex3D>>().SingleInstance();
            builder.RegisterType<Wpf3dReplaceIntermediateVerticesModule>().As<ReplaceIntermediateVerticesModule<Vertex3D>>()
               .As<IGraphSubscription<Vertex3D>>().As<IUndo>().SingleInstance();

            builder.RegisterComposite<CompositeUndo, IUndo>().SingleInstance();

            builder.RegisterType<FileLog>().As<ILog>().SingleInstance();
            builder.RegisterType<MessageBoxLog>().As<ILog>().SingleInstance();
            builder.RegisterType<MailLog>().As<ILog>().SingleInstance();
            builder.RegisterComposite<Logs, ILog>().SingleInstance();

            builder.RegisterComposite<GraphSubscriptions<Vertex3D>, IGraphSubscription<Vertex3D>>().SingleInstance();

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
