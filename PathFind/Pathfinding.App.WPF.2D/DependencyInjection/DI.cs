using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Serialization.Serializers.Decorators;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Realizations.StepRules;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.WPF._2D.DependencyInjection;
using Pathfinding.App.WPF._2D.Extensions;
using Pathfinding.App.WPF._2D.Interface;
using Pathfinding.App.WPF._2D.Model;
using Pathfinding.App.WPF._2D.ViewModel;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Modules;
using Pathfinding.GraphLib.Core.Modules.Commands;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Core.Realizations;
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
using Shared.Executable;
using Shared.Random;
using Shared.Random.Realizations;
using System;
using System.Linq;
using System.Reflection;

using static Pathfinding.App.WPF._2D.DependencyInjection.RegistrationConstants;

namespace WPFVersion.DependencyInjection
{
    using AlgorithmFactory = IAlgorithmFactory<PathfindingProcess>;
    using Command = IPathfindingRangeCommand<Vertex>;
    using Graph = Graph2D<Vertex>;
    using GraphSerializer = IGraphSerializer<Graph2D<Vertex>, Vertex>;

    internal static class DI
    {
        private static readonly Lazy<IContainer> container = new(Configure);

        private static Assembly[] Assemblies => AppDomain.CurrentDomain.GetAssemblies();

        public static IContainer Container => container.Value;

        private static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            //View and view models registration
            builder.RegisterType<MainWindowViewModel>().As<ICache<Graph2D<Vertex>>>().AsSelf().SingleInstance();
            builder.RegisterAssemblyTypes(Assemblies).AssignableTo<IViewModel>().AsSelf().InstancePerDependency().PropertiesAutowired();
            builder.RegisterAssemblyTypes(Assemblies).Where(type => type.IsAppWindow()).AsSelf().InstancePerDependency();
            // Common
            builder.RegisterType<Messenger>().As<IMessenger>().SingleInstance();
            builder.RegisterComposite<CompositeUndo, IUndo>().SingleInstance();
            builder.RegisterType<GeometricMeanCost>().As<IMeanCost>().SingleInstance();
            // Pathfinding range registrations
            builder.RegisterType<PathfindingRange<Vertex>>().As<IPathfindingRange<Vertex>>().SingleInstance();
            builder.RegisterDecorator<VisualPathfindingRange<Vertex>, IPathfindingRange<Vertex>>();
            builder.RegisterType<PathfindingRangeBuilder<Vertex>>().As<IPathfindingRangeBuilder<Vertex>>().As<IUndo>()
                .SingleInstance().ConfigurePipeline(p => p.Use(new RangeBuilderConfigurationMiddlewear()));
            builder.RegisterType<IncludeSourceVertex<Vertex>>().Keyed<Command>(IncludeCommand).WithMetadata(Order, 2).SingleInstance();
            builder.RegisterType<IncludeTargetVertex<Vertex>>().Keyed<Command>(IncludeCommand).WithMetadata(Order, 4).SingleInstance();
            builder.RegisterType<IncludeTransitVertex<Vertex>>().Keyed<Command>(IncludeCommand).WithMetadata(Order, 6).SingleInstance();
            builder.RegisterType<ReplaceTransitIsolatedVertex<Vertex>>().Keyed<Command>(IncludeCommand).WithMetadata(Order, 1).SingleInstance();
            builder.RegisterType<ReplaceIsolatedSourceVertex<Vertex>>().Keyed<Command>(IncludeCommand).WithMetadata(Order, 3).SingleInstance();
            builder.RegisterType<ReplaceIsolatedTargetVertex<Vertex>>().Keyed<Command>(IncludeCommand).WithMetadata(Order, 5).SingleInstance();
            builder.RegisterType<ExcludeTransitVertex<Vertex>>().Keyed<Command>(ExcludeCommand).WithMetadata(Order, 3).SingleInstance();
            builder.RegisterType<ExcludeSourceVertex<Vertex>>().Keyed<Command>(ExcludeCommand).WithMetadata(Order, 1).SingleInstance();
            builder.RegisterType<ExcludeTargetVertex<Vertex>>().Keyed<Command>(ExcludeCommand).WithMetadata(Order, 2).SingleInstance();
            builder.RegisterType<ReplaceTransitVerticesModule<Vertex>>().AsSelf().As<IUndo>().SingleInstance();
            // Graph subscriptions registrations
            builder.RegisterType<PathfindingRangeBuilderSubscription>().As<IGraphSubscription<Vertex>>().SingleInstance();
            builder.RegisterType<ReplaceTransitVerticesSubscribtion>().As<IGraphSubscription<Vertex>>().SingleInstance();
            builder.RegisterType<VertexChangeCostSubscription>().As<IGraphSubscription<Vertex>>().SingleInstance();
            builder.RegisterType<VertexReverseModuleSubscription>().As<IGraphSubscription<Vertex>>().SingleInstance();
            builder.RegisterComposite<GraphSubscriptions<Vertex>, IGraphSubscription<Vertex>>().SingleInstance();
            // Logging registration
            builder.RegisterType<FileLog>().As<ILog>().SingleInstance();
            builder.RegisterType<MessageBoxLog>().As<ILog>().SingleInstance();
            builder.RegisterComposite<Logs, ILog>().SingleInstance();
            // Random number generator registrations
            builder.RegisterType<CryptoRandom>().As<IRandom>().SingleInstance();
            builder.RegisterDecorator<ThreadSafeRandom, IRandom>();
            // Graph registrations
            builder.RegisterType<GraphAssemble<Graph, Vertex>>().As<IGraphAssemble<Graph, Vertex>>().SingleInstance();
            builder.RegisterType<VertexFactory>().As<IVertexFactory<Vertex>>().SingleInstance();
            builder.RegisterType<CostFactory>().As<IVertexCostFactory>().SingleInstance();
            builder.RegisterType<Coordinate2DFactory>().As<ICoordinateFactory>().SingleInstance();
            builder.RegisterType<Graph2DFactory<Vertex>>().As<IGraphFactory<Graph, Vertex>>().SingleInstance();
            builder.RegisterDecorator<Graph2dWrapFactory, IGraphFactory<Graph, Vertex>>();
            builder.RegisterType<GraphFieldFactory>().As<IGraphFieldFactory<Graph, Vertex, GraphField>>().SingleInstance();
            builder.RegisterType<MooreNeighborhoodFactory>().As<INeighborhoodFactory>().SingleInstance();
            builder.RegisterType<VertexVisualization>().As<ITotalVisualization<Vertex>>().SingleInstance();
            // Serialization registrations
            builder.RegisterType<InFileSerializationModule<Graph, Vertex>>()
                .As<IGraphSerializationModule<Graph, Vertex>>().SingleInstance();
            builder.RegisterType<PathInput>().As<IPathInput>().SingleInstance();
            builder.RegisterType<XmlGraphSerializer<Graph, Vertex>>().As<GraphSerializer>().SingleInstance();
            builder.RegisterDecorator<CompressGraphSerializer<Graph, Vertex>, GraphSerializer>();
            builder.RegisterDecorator<CryptoGraphSerializer<Graph, Vertex>, GraphSerializer>();
            builder.RegisterType<VertexFromInfoFactory>().As<IVertexFromInfoFactory<Vertex>>().SingleInstance();
            // Algorithms registrations
            builder.RegisterAssemblyTypes(Assemblies).AssignableTo<AlgorithmFactory>()
                .As<AlgorithmFactory>().SingleInstance();
            builder.RegisterType<DefaultStepRule>().As<IStepRule>().SingleInstance();
            builder.RegisterDecorator<CardinalStepRule, IStepRule>();

            return builder.Build();
        }
    }
}