using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.WPF._3D.Extensions;
using Pathfinding.App.WPF._3D.Interface;
using Pathfinding.App.WPF._3D.Model;
using Pathfinding.App.WPF._3D.Model3DFactories;
using Pathfinding.App.WPF._3D.ViewModel;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Modules;
using Pathfinding.GraphLib.Core.Modules.Commands;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Factory.Realizations;
using Pathfinding.GraphLib.Factory.Realizations.GraphAssembles;
using Pathfinding.GraphLib.Factory.Realizations.GraphFactories;
using Pathfinding.GraphLib.Factory.Realizations.NeighborhoodFactories;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Modules;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers;
using Pathfinding.GraphLib.Subscriptions;
using Pathfinding.Logging.Interface;
using Pathfinding.Logging.Loggers;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Executable;
using Shared.Random;
using Shared.Random.Realizations;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using static Pathfinding.App.WPF._3D.DependencyInjection.RegistrationConstants;

namespace Pathfinding.App.WPF._3D.DependencyInjection
{
    using AlgorithmFactory = IAlgorithmFactory<PathfindingProcess>;
    using Command = IPathfindingRangeCommand<Vertex3D>;

    internal static class DI
    {
        private static readonly Lazy<IContainer> container = new Lazy<IContainer>(Configure);

        public static IContainer Container => container.Value;

        private static Assembly[] Assemblies => AppDomain.CurrentDomain.GetAssemblies();

        private static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindowViewModel>().AsSelf().AsImplementedInterfaces().SingleInstance();
            builder.RegisterAssemblyTypes(Assemblies).AssignableTo<IViewModel>().AsSelf().InstancePerDependency().PropertiesAutowired();
            builder.RegisterAssemblyTypes(Assemblies).Where(type => type.IsAppWindow()).AsSelf().InstancePerDependency();

            builder.RegisterType<Messenger>().As<IMessenger>().SingleInstance();

            builder.RegisterType<PathfindingRange<Vertex3D>>().As<IPathfindingRange<Vertex3D>>().SingleInstance();
            builder.RegisterDecorator<VisualPathfindingRange<Vertex3D>, IPathfindingRange<Vertex3D>>();
            builder.RegisterType<PathfindingRangeBuilder<Vertex3D>>().As<IPathfindingRangeBuilder<Vertex3D>>().As<IUndo>()
                .SingleInstance().ConfigurePipeline(p => p.Use(new RangeBuilderConfigurationMiddlewear()));
            builder.RegisterType<IncludeSourceVertex<Vertex3D>>().Keyed<Command>(IncludeCommand).WithMetadata(Order, 1).SingleInstance();
            builder.RegisterType<IncludeTargetVertex<Vertex3D>>().Keyed<Command>(IncludeCommand).WithMetadata(Order, 3).SingleInstance();
            builder.RegisterType<ReplaceIsolatedSourceVertex<Vertex3D>>().Keyed<Command>(IncludeCommand).WithMetadata(Order, 2).SingleInstance();
            builder.RegisterType<ReplaceIsolatedTargetVertex<Vertex3D>>().Keyed<Command>(IncludeCommand).WithMetadata(Order, 4).SingleInstance();
            builder.RegisterType<ExcludeSourceVertex<Vertex3D>>().Keyed<Command>(ExcludeCommand).WithMetadata(Order, 1).SingleInstance();
            builder.RegisterType<ExcludeTargetVertex<Vertex3D>>().Keyed<Command>(ExcludeCommand).WithMetadata(Order, 2).SingleInstance();

            builder.RegisterType<FileLog>().As<ILog>().SingleInstance();
            builder.RegisterType<MessageBoxLog>().As<ILog>().SingleInstance();
            builder.RegisterComposite<Logs, ILog>().SingleInstance();

            builder.RegisterType<VertexReverseModuleSubscription>().As<IGraphSubscription<Vertex3D>>().SingleInstance();
            builder.RegisterType<PathfindingRangeBuilderSubscription>().As<IGraphSubscription<Vertex3D>>().SingleInstance();
            builder.RegisterComposite<GraphSubscriptions<Vertex3D>, IGraphSubscription<Vertex3D>>().SingleInstance();

            builder.RegisterComposite<CompositeUndo, IUndo>().SingleInstance();

            builder.RegisterType<KnuthRandom>().As<IRandom>().SingleInstance();
            builder.RegisterDecorator<ThreadSafeRandom, IRandom>();

            builder.RegisterType<Vertex3DFactory>().As<IVertexFactory<Vertex3D>>().SingleInstance();
            builder.RegisterType<GraphFactory<Vertex3D>>().As<IGraphFactory<Vertex3D>>().SingleInstance();
            builder.RegisterType<GraphField3DFactory>().As<IGraphFieldFactory<Vertex3D, GraphField3D>>().SingleInstance();
            builder.RegisterType<VonNeumannNeighborhoodFactory>().As<INeighborhoodFactory>().SingleInstance();
            builder.RegisterType<CubicModel3DFactory>().As<IModel3DFactory>().SingleInstance();
            builder.RegisterType<GraphAssemble<Vertex3D>>().As<IGraphAssemble<Vertex3D>>().SingleInstance();
            builder.RegisterType<VertexVisualization>().As<ITotalVisualization<Vertex3D>>().SingleInstance();

            builder.RegisterType<InFileSerializationModule<Vertex3D>>().As<IGraphSerializationModule<Vertex3D>>().SingleInstance();
            builder.RegisterType<PathInput>().As<IPathInput>().SingleInstance();
            builder.RegisterType<BinaryGraphSerializer<Vertex3D>>().As<ISerializer<IGraph<Vertex3D>>>().SingleInstance();
            //builder.RegisterDecorator<CompressSerializer<IGraph<Vertex3D>>, ISerializer<IGraph<Vertex3D>>>();
            //builder.RegisterDecorator<CryptoSerializer<IGraph<Vertex3D>>, ISerializer<IGraph<Vertex3D>>>();
            builder.RegisterType<Vertex3DFromInfoFactory>().As<IVertexFromInfoFactory<Vertex3D>>().SingleInstance();

            builder.RegisterAssemblyTypes(Assemblies).AssignableTo<AlgorithmFactory>().As<AlgorithmFactory>().SingleInstance();

            return builder.Build();
        }
    }
}
