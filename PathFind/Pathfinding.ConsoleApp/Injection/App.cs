using Autofac;
using Autofac.Features.AttributeFilters;
using Autofac.Features.Metadata;
using AutoMapper;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.Model.Factories;
using Pathfinding.ConsoleApp.View;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Infrastructure.Business;
using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Infrastructure.Business.Algorithms.StepRules;
using Pathfinding.Infrastructure.Business.Commands;
using Pathfinding.Infrastructure.Business.Mappings;
using Pathfinding.Infrastructure.Business.Serializers;
using Pathfinding.Infrastructure.Business.Serializers.Decorators;
using Pathfinding.Infrastructure.Data.Pathfinding.Factories;
using Pathfinding.Infrastructure.Data.Sqlite;
using Pathfinding.Logging.Interface;
using Pathfinding.Logging.Loggers;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Service.Interface.Models.Undefined;
using System.Collections.Generic;

namespace Pathfinding.ConsoleApp.Injection
{
    internal static class App
    {
        public static ILifetimeScope Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<GraphFactory<GraphVertexModel>>().As<IGraphFactory<GraphVertexModel>>().SingleInstance();
            builder.RegisterType<GraphVertexModelFactory>().As<IVertexFactory<GraphVertexModel>>().SingleInstance();
            builder.RegisterType<GraphAssemble<GraphVertexModel>>().As<IGraphAssemble<GraphVertexModel>>().SingleInstance();

            builder.RegisterType<DefaultStepRule>().As<IStepRule>().WithMetadata(MetadataKeys.NameKey, "Default").SingleInstance();
            builder.RegisterType<LandscapeStepRule>().As<IStepRule>().WithMetadata(MetadataKeys.NameKey, "Lanscape").SingleInstance();

            builder.RegisterType<EuclidianDistance>().As<IHeuristic>().WithMetadata(MetadataKeys.NameKey, "Euclidian").SingleInstance();
            builder.RegisterType<ChebyshevDistance>().As<IHeuristic>().WithMetadata(MetadataKeys.NameKey, "Chebyshev").SingleInstance();
            builder.RegisterType<DiagonalDistance>().As<IHeuristic>().WithMetadata(MetadataKeys.NameKey, "Diagonal").SingleInstance();
            builder.RegisterType<ManhattanDistance>().As<IHeuristic>().WithMetadata(MetadataKeys.NameKey, "Manhattan").SingleInstance();
            builder.RegisterType<CosineDistance>().As<IHeuristic>().WithMetadata(MetadataKeys.NameKey, "Cosine").SingleInstance();

            builder.RegisterInstance("No").Keyed<string>(KeyFilters.SpreadLevels).WithMetadata(MetadataKeys.LevelKey, 0).SingleInstance();
            builder.RegisterInstance("Minimal").Keyed<string>(KeyFilters.SpreadLevels).WithMetadata(MetadataKeys.LevelKey, 1).SingleInstance();
            builder.RegisterInstance("Lowest").Keyed<string>(KeyFilters.SpreadLevels).WithMetadata(MetadataKeys.LevelKey, 2).SingleInstance();
            builder.RegisterInstance("Low").Keyed<string>(KeyFilters.SpreadLevels).WithMetadata(MetadataKeys.LevelKey, 3).SingleInstance();
            builder.RegisterInstance("Medium").Keyed<string>(KeyFilters.SpreadLevels).WithMetadata(MetadataKeys.LevelKey, 4).SingleInstance();
            builder.RegisterInstance("High").Keyed<string>(KeyFilters.SpreadLevels).WithMetadata(MetadataKeys.LevelKey, 5).SingleInstance();
            builder.RegisterInstance("Highest").Keyed<string>(KeyFilters.SpreadLevels).WithMetadata(MetadataKeys.LevelKey, 7).SingleInstance();
            builder.RegisterInstance("Extreme").Keyed<string>(KeyFilters.SpreadLevels).WithMetadata(MetadataKeys.LevelKey, 14).SingleInstance();

            builder.RegisterInstance("No").Keyed<string>(KeyFilters.SmoothLevels).WithMetadata(MetadataKeys.SmoothKey, 0).SingleInstance();
            builder.RegisterInstance("Low").Keyed<string>(KeyFilters.SmoothLevels).WithMetadata(MetadataKeys.SmoothKey, 1).SingleInstance();
            builder.RegisterInstance("Meduim").Keyed<string>(KeyFilters.SmoothLevels).WithMetadata(MetadataKeys.SmoothKey, 2).SingleInstance();
            builder.RegisterInstance("High").Keyed<string>(KeyFilters.SmoothLevels).WithMetadata(MetadataKeys.SmoothKey, 4).SingleInstance();
            builder.RegisterInstance("Extreme").Keyed<string>(KeyFilters.SmoothLevels).WithMetadata(MetadataKeys.SmoothKey, 7).SingleInstance();

            builder.RegisterType<MooreNeighborhoodFactory>().As<INeighborhoodFactory>().SingleInstance().WithMetadata(MetadataKeys.NameKey, NeighborhoodNames.Moore);
            builder.RegisterType<VonNeumannNeighborhoodFactory>().As<INeighborhoodFactory>().SingleInstance().WithMetadata(MetadataKeys.NameKey, NeighborhoodNames.VonNeumann);

            builder.RegisterType<BinarySerializer<VisitedVerticesModel>>()
                .As<ISerializer<IEnumerable<VisitedVerticesModel>>>().SingleInstance();
            builder.RegisterType<BinarySerializer<CoordinateModel>>()
                .As<ISerializer<IEnumerable<CoordinateModel>>>().SingleInstance();
            builder.RegisterType<BinarySerializer<CostCoordinatePair>>()
                .As<ISerializer<IEnumerable<CostCoordinatePair>>>().SingleInstance();

            builder.RegisterType<SubAlgorithmsMappingProfile>().As<Profile>().SingleInstance();
            builder.RegisterType<GraphStateMappingProfile>().As<Profile>().SingleInstance();
            builder.RegisterType<GraphMappingProfile<GraphVertexModel>>().As<Profile>().SingleInstance();
            builder.RegisterType<UntitledMappingProfile>().As<Profile>().SingleInstance();
            builder.RegisterType<HistoryMappingProfile<GraphVertexModel>>().As<Profile>().SingleInstance();
            builder.RegisterType<AlgorithmRunMappingProfile>().As<Profile>().SingleInstance();
            builder.RegisterType<VerticesMappingProfile<GraphVertexModel>>().As<Profile>().SingleInstance();
            builder.RegisterType<StatisticsMappingProfile>().As<Profile>().SingleInstance();

            builder.Register(context =>
            {
                var profiles = context.Resolve<IEnumerable<Profile>>();
                var mappingConfig = new MapperConfiguration(c => c.AddProfiles(profiles));
                return mappingConfig.CreateMapper(context.Resolve);
            }).As<IMapper>().SingleInstance();

            //builder.RegisterInstance(new ConnectionString(AppSettings.Default.LiteDb)).As<ConnectionString>().SingleInstance();
            builder.Register(_=> new SqliteUnitOfWorkFactory(AppSettings.Default.Sqlite)).As<IUnitOfWorkFactory>().SingleInstance();

            builder.RegisterType<RequestService<GraphVertexModel>>().As<IRequestService<GraphVertexModel>>().SingleInstance();

            builder.RegisterType<IncludeSourceVertex<GraphVertexModel>>().SingleInstance().WithAttributeFiltering()
                .Keyed<IPathfindingRangeCommand<GraphVertexModel>>(KeyFilters.IncludeCommands);
            builder.RegisterType<IncludeTargetVertex<GraphVertexModel>>().SingleInstance().WithAttributeFiltering()
                .Keyed<IPathfindingRangeCommand<GraphVertexModel>>(KeyFilters.IncludeCommands);
            builder.RegisterType<ReplaceIsolatedSourceVertex<GraphVertexModel>>().SingleInstance().WithAttributeFiltering()
                .Keyed<IPathfindingRangeCommand<GraphVertexModel>>(KeyFilters.IncludeCommands);
            builder.RegisterType<ReplaceIsolatedTargetVertex<GraphVertexModel>>().SingleInstance().WithAttributeFiltering()
                .Keyed<IPathfindingRangeCommand<GraphVertexModel>>(KeyFilters.IncludeCommands);
            builder.RegisterType<ExcludeSourceVertex<GraphVertexModel>>().SingleInstance().WithAttributeFiltering()
                .Keyed<IPathfindingRangeCommand<GraphVertexModel>>(KeyFilters.ExcludeCommands);
            builder.RegisterType<ExcludeTargetVertex<GraphVertexModel>>().SingleInstance().WithAttributeFiltering()
                .Keyed<IPathfindingRangeCommand<GraphVertexModel>>(KeyFilters.ExcludeCommands);
            builder.RegisterType<IncludeTransitVertex<GraphVertexModel>>().SingleInstance().WithAttributeFiltering()
                .Keyed<IPathfindingRangeCommand<GraphVertexModel>>(KeyFilters.IncludeCommands);
            builder.RegisterType<ReplaceTransitIsolatedVertex<GraphVertexModel>>().SingleInstance().WithAttributeFiltering()
                .Keyed<IPathfindingRangeCommand<GraphVertexModel>>(KeyFilters.IncludeCommands);
            builder.RegisterType<ExcludeTransitVertex<GraphVertexModel>>().SingleInstance().WithAttributeFiltering()
                .Keyed<IPathfindingRangeCommand<GraphVertexModel>>(KeyFilters.ExcludeCommands);

            builder.RegisterType<BinarySerializer<PathfindingHistorySerializationModel>>()
                .As<ISerializer<IEnumerable<PathfindingHistorySerializationModel>>>().SingleInstance();
            builder.RegisterGenericDecorator(typeof(BufferedSerializer<>), typeof(ISerializer<>));
            builder.RegisterGenericDecorator(typeof(CompressSerializer<>), typeof(ISerializer<>));

            builder.RegisterType<FileLog>().As<ILog>().SingleInstance();
            builder.RegisterType<ConsoleLog>().As<ILog>().SingleInstance();
            builder.RegisterComposite<Logs, ILog>().As<ILog>().SingleInstance();

            builder.RegisterType<WeakReferenceMessenger>().Keyed<IMessenger>(KeyFilters.Views).SingleInstance().WithAttributeFiltering();
            builder.RegisterType<WeakReferenceMessenger>().Keyed<IMessenger>(KeyFilters.ViewModels).SingleInstance().WithAttributeFiltering();

            builder.RegisterAssemblyTypes(typeof(BaseViewModel).Assembly).Where(x => x.Name.EndsWith("ViewModel"))
                .AsSelf().AsImplementedInterfaces().SingleInstance().WithAttributeFiltering().PreserveExistingDefaults();
            builder.RegisterType<HeuristicsViewModel>().AsSelf().SingleInstance().UsingConstructor(typeof(IEnumerable<Meta<IHeuristic>>));
            builder.RegisterType<StepRulesViewModel>().AsSelf().SingleInstance().UsingConstructor(typeof(IEnumerable<Meta<IStepRule>>));
            builder.RegisterType<SpreadViewModel>().AsSelf().SingleInstance().UsingConstructor(typeof(IEnumerable<Meta<string>>)).WithAttributeFiltering();
            builder.RegisterType<SmoothLevelViewModel>().AsSelf().SingleInstance().UsingConstructor(typeof(IEnumerable<Meta<string>>)).WithAttributeFiltering();
            builder.RegisterType<NeighborhoodFactoriesViewModel>().AsSelf().SingleInstance().UsingConstructor(typeof(IEnumerable<Meta<INeighborhoodFactory>>));

            builder.RegisterType<MainView>().AsSelf().WithAttributeFiltering();

            builder.RegisterType<RightPanelView>().Keyed<Terminal.Gui.View>(KeyFilters.MainWindow).WithAttributeFiltering();
            builder.RegisterType<GraphFieldView>().Keyed<Terminal.Gui.View>(KeyFilters.MainWindow).WithAttributeFiltering();
            builder.RegisterType<AlgorithmRunFieldView>().Keyed<Terminal.Gui.View>(KeyFilters.MainWindow).WithAttributeFiltering();

            builder.RegisterType<GraphPanel>().Keyed<Terminal.Gui.View>(KeyFilters.RightPanel).WithAttributeFiltering();
            builder.RegisterType<RunsPanel>().Keyed<Terminal.Gui.View>(KeyFilters.RightPanel).WithAttributeFiltering();

            builder.RegisterType<GraphsTableView>().Keyed<Terminal.Gui.View>(KeyFilters.GraphPanel).WithAttributeFiltering();
            builder.RegisterType<GraphTableButtonsFrame>().Keyed<Terminal.Gui.View>(KeyFilters.GraphPanel).WithAttributeFiltering();
            builder.RegisterType<GraphAssembleView>().Keyed<Terminal.Gui.View>(KeyFilters.GraphPanel).WithAttributeFiltering();

            builder.RegisterType<DeleteGraphButton>().Keyed<Terminal.Gui.View>(KeyFilters.GraphTableButtons).WithAttributeFiltering();
            builder.RegisterType<NewGraphButton>().Keyed<Terminal.Gui.View>(KeyFilters.GraphTableButtons).WithAttributeFiltering();
            builder.RegisterType<GraphImportView>().Keyed<Terminal.Gui.View>(KeyFilters.GraphTableButtons).WithAttributeFiltering();
            builder.RegisterType<GraphExportView>().Keyed<Terminal.Gui.View>(KeyFilters.GraphTableButtons).WithAttributeFiltering();

            builder.RegisterType<GraphNameView>().Keyed<Terminal.Gui.View>(KeyFilters.GraphAssembleView).WithAttributeFiltering();
            builder.RegisterType<GraphParametresView>().Keyed<Terminal.Gui.View>(KeyFilters.GraphAssembleView).WithAttributeFiltering();
            builder.RegisterType<NeighborhoodFactoryView>().Keyed<Terminal.Gui.View>(KeyFilters.GraphAssembleView).WithAttributeFiltering();
            builder.RegisterType<SmoothLevelView>().Keyed<Terminal.Gui.View>(KeyFilters.GraphAssembleView).WithAttributeFiltering();

            builder.RegisterType<RunsTableView>().Keyed<Terminal.Gui.View>(KeyFilters.RunsPanel).WithAttributeFiltering();
            builder.RegisterType<RunsTableButtonsFrame>().Keyed<Terminal.Gui.View>(KeyFilters.RunsPanel).WithAttributeFiltering();
            builder.RegisterType<AlgorithmCreationView>().Keyed<Terminal.Gui.View>(KeyFilters.RunsPanel).WithAttributeFiltering();
            builder.RegisterType<PathfindingProcessButtonsFrame>().Keyed<Terminal.Gui.View>(KeyFilters.RunsPanel).WithAttributeFiltering();

            builder.RegisterType<NewRunButton>().Keyed<Terminal.Gui.View>(KeyFilters.RunButtonsFrame).WithAttributeFiltering();
            builder.RegisterType<DeleteRunButton>().Keyed<Terminal.Gui.View>(KeyFilters.RunButtonsFrame).WithAttributeFiltering();

            builder.RegisterType<AlgorithmsView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmCreationView).WithAttributeFiltering();
            builder.RegisterType<AlgorithmParametresView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmCreationView).WithAttributeFiltering();

            builder.RegisterType<DijkstraAlgorithmView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmsListView).WithAttributeFiltering();
            builder.RegisterType<AStarAlgorithmView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmsListView).WithAttributeFiltering();
            builder.RegisterType<IDAStarAlgorithmView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmsListView).WithAttributeFiltering();
            builder.RegisterType<RandomAlgorithmView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmsListView).WithAttributeFiltering();
            builder.RegisterType<LeeAlgorithmView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmsListView).WithAttributeFiltering();
            builder.RegisterType<AStarLeeAlgorithmView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmsListView).WithAttributeFiltering();
            builder.RegisterType<CostGreedyAlgorithmView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmsListView).WithAttributeFiltering();
            builder.RegisterType<DistanceFirstAlgorithmView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmsListView).WithAttributeFiltering();
            builder.RegisterType<AStarGreedyAlgorithmView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmsListView).WithAttributeFiltering();
            builder.RegisterType<DepthRandomAlgorithmView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmsListView).WithAttributeFiltering();
            builder.RegisterType<SnakeAlgorithmView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmsListView).WithAttributeFiltering();
            builder.RegisterType<DepthFirstAlgorithmView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmsListView).WithAttributeFiltering();
            builder.RegisterType<BidirectDijkstraAlgorithmView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmsListView).WithAttributeFiltering();
            builder.RegisterType<BidirectAStarAlgorithmView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmsListView).WithAttributeFiltering();
            builder.RegisterType<BidirectLeeAlgorithmView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmsListView).WithAttributeFiltering();

            builder.RegisterType<StepRulesView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmParametresView).WithAttributeFiltering();
            builder.RegisterType<HeuristicsView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmParametresView).WithAttributeFiltering();
            builder.RegisterType<SpreadView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmParametresView).WithAttributeFiltering();

            builder.RegisterType<PathfindingProcessView>().Keyed<Terminal.Gui.View>(KeyFilters.CreateRunButtonsFrame).WithAttributeFiltering();
            builder.RegisterType<CloseAlgorithmCreationButton>().Keyed<Terminal.Gui.View>(KeyFilters.CreateRunButtonsFrame).WithAttributeFiltering();

            return builder.Build();
        }
    }
}