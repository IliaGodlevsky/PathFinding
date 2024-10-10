using Autofac;
using Autofac.Features.AttributeFilters;
using AutoMapper;
using CommunityToolkit.Mvvm.Messaging;
using LiteDB;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.Model.Factories;
using Pathfinding.ConsoleApp.View;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Infrastructure.Business;
using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Infrastructure.Business.Algorithms.StepRules;
using Pathfinding.Infrastructure.Business.Commands;
using Pathfinding.Infrastructure.Business.Mappings;
using Pathfinding.Infrastructure.Business.Serializers;
using Pathfinding.Infrastructure.Business.Serializers.Decorators;
using Pathfinding.Infrastructure.Data.LiteDb;
using Pathfinding.Infrastructure.Data.Pathfinding.Factories;
using Pathfinding.Logging.Interface;
using Pathfinding.Logging.Loggers;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Service.Interface.Models.Undefined;
using System.Collections.Generic;

namespace Pathfinding.ConsoleApp.Injection
{
    internal static class Container
    {
#pragma warning disable IDE0060
        public static ILifetimeScope BuildApp(string[] args)
#pragma warning restore IDE0060
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<GraphFactory<VertexModel>>().As<IGraphFactory<VertexModel>>().SingleInstance();
            builder.RegisterType<VertexModelFactory>().As<IVertexFactory<VertexModel>>().SingleInstance();
            builder.RegisterType<GraphAssemble<VertexModel>>().As<IGraphAssemble<VertexModel>>().SingleInstance();

            builder.RegisterInstance(new[]
            {
                ("No", 0),
                ("Low", 1),
                ("Medium", 2),
                ("High", 4),
                ("Extreme", 6)
            }).Keyed<IEnumerable<(string Name, int Level)>>(KeyFilters.SmoothLevels).SingleInstance();
            builder.RegisterInstance(new[]
            {
                ("No", 0),
                ("Minimal", 1),
                ("Lowest", 2),
                ("Low", 3),
                ("Medium", 4),
                ("High", 5),
                ("Highest", 7),
                ("Extreme", 14)
            }).Keyed<IEnumerable<(string Name, int Level)>>(KeyFilters.SpreadLevels).SingleInstance();
            builder.RegisterInstance(new[]
            {
                ("Moore", (INeighborhoodFactory)new MooreNeighborhoodFactory()),
                ("Neumann", new VonNeumannNeighborhoodFactory())
            }).As<IEnumerable<(string Name, INeighborhoodFactory Factory)>>().SingleInstance();
            builder.RegisterInstance(new[]
            {
                ("Default", (IStepRule)new DefaultStepRule()),
                ("Lanscape", new LandscapeStepRule())
            }).As<IEnumerable<(string Name, IStepRule Rule)>>().SingleInstance();
            builder.RegisterInstance(new[]
            {
                ("Euclidian", (IHeuristic)new EuclidianDistance()),
                ("Chebyshev", new ChebyshevDistance()),
                ("Manhattan", new ManhattanDistance()),
                ("Cosine", new CosineDistance())
            }).As<IEnumerable<(string Name, IHeuristic Heuristic)>>().SingleInstance();

            builder.RegisterInstance(new ConnectionString()
            {
                Filename = "pathfinding.litedb",
                Connection = ConnectionType.Shared,
                AutoRebuild = true
            }).As<ConnectionString>().SingleInstance();
            builder.RegisterType<LiteDbInFileUnitOfWorkFactory>().As<IUnitOfWorkFactory>().SingleInstance();

            builder.RegisterAutoMapper();
            builder.RegisterType<RequestService<VertexModel>>().As<IRequestService<VertexModel>>().SingleInstance();
            builder.RegisterDecorator<CachedRequestService<VertexModel>, IRequestService<VertexModel>>();

            builder.RegisterType<IncludeSourceVertex<VertexModel>>().SingleInstance().WithAttributeFiltering()
                .Keyed<IPathfindingRangeCommand<VertexModel>>(KeyFilters.IncludeCommands);
            builder.RegisterType<IncludeTargetVertex<VertexModel>>().SingleInstance().WithAttributeFiltering()
                .Keyed<IPathfindingRangeCommand<VertexModel>>(KeyFilters.IncludeCommands);
            builder.RegisterType<IncludeTransitVertex<VertexModel>>().SingleInstance().WithAttributeFiltering()
                .Keyed<IPathfindingRangeCommand<VertexModel>>(KeyFilters.IncludeCommands);
            builder.RegisterType<ReplaceTransitIsolatedVertex<VertexModel>>().SingleInstance().WithAttributeFiltering()
                .Keyed<IPathfindingRangeCommand<VertexModel>>(KeyFilters.IncludeCommands);
            builder.RegisterType<ReplaceIsolatedSourceVertex<VertexModel>>().SingleInstance().WithAttributeFiltering()
                .Keyed<IPathfindingRangeCommand<VertexModel>>(KeyFilters.IncludeCommands);
            builder.RegisterType<ReplaceIsolatedTargetVertex<VertexModel>>().SingleInstance().WithAttributeFiltering()
                .Keyed<IPathfindingRangeCommand<VertexModel>>(KeyFilters.IncludeCommands);
            builder.RegisterType<ExcludeSourceVertex<VertexModel>>().SingleInstance().WithAttributeFiltering()
                .Keyed<IPathfindingRangeCommand<VertexModel>>(KeyFilters.ExcludeCommands);
            builder.RegisterType<ExcludeTargetVertex<VertexModel>>().SingleInstance().WithAttributeFiltering()
                .Keyed<IPathfindingRangeCommand<VertexModel>>(KeyFilters.ExcludeCommands);
            builder.RegisterType<ExcludeTransitVertex<VertexModel>>().SingleInstance().WithAttributeFiltering()
                .Keyed<IPathfindingRangeCommand<VertexModel>>(KeyFilters.ExcludeCommands);

            builder.RegisterGenericDecorator(typeof(BufferedSerializer<>), typeof(ISerializer<>));
            builder.RegisterGenericDecorator(typeof(CompressSerializer<>), typeof(ISerializer<>));

            builder.RegisterType<JsonSerializer<List<PathfindingHistorySerializationModel>>>()
                .As<ISerializer<List<PathfindingHistorySerializationModel>>>().SingleInstance();

            builder.RegisterType<FileLog>().As<ILog>().SingleInstance();
            //builder.RegisterType<NullLog>().As<ILog>().SingleInstance();
            builder.RegisterType<ConsoleLog>().As<ILog>().SingleInstance();
            builder.RegisterComposite<Logs, ILog>().As<ILog>().SingleInstance();

            builder.RegisterType<WeakReferenceMessenger>().Keyed<IMessenger>(KeyFilters.Views).SingleInstance().WithAttributeFiltering();
            builder.RegisterType<WeakReferenceMessenger>().Keyed<IMessenger>(KeyFilters.ViewModels).SingleInstance().WithAttributeFiltering();

            builder.RegisterViewModels();

            builder.RegisterType<MainView>().AsSelf().WithAttributeFiltering();

            builder.RegisterType<RightPanelView>().Keyed<Terminal.Gui.View>(KeyFilters.MainWindow).WithAttributeFiltering();
            builder.RegisterType<GraphFrameView>().Keyed<Terminal.Gui.View>(KeyFilters.RightPanel).WithAttributeFiltering();
            builder.RegisterType<GraphsTableView>().Keyed<Terminal.Gui.View>(KeyFilters.GraphFrame).WithAttributeFiltering();
            builder.RegisterType<ButtonsFrameView>().Keyed<Terminal.Gui.View>(KeyFilters.GraphFrame).WithAttributeFiltering();
            builder.RegisterType<LoadGraphButton>().Keyed<Terminal.Gui.View>(KeyFilters.GraphTableButtons).WithAttributeFiltering();
            builder.RegisterType<DeleteGraphButton>().Keyed<Terminal.Gui.View>(KeyFilters.GraphTableButtons).WithAttributeFiltering();
            builder.RegisterType<SaveGraphButton>().Keyed<Terminal.Gui.View>(KeyFilters.GraphTableButtons).WithAttributeFiltering();
            builder.RegisterType<NewGraphButton>().Keyed<Terminal.Gui.View>(KeyFilters.GraphTableButtons).WithAttributeFiltering();
            builder.RegisterType<CreateGraphView>().Keyed<Terminal.Gui.View>(KeyFilters.GraphFrame).WithAttributeFiltering();
            builder.RegisterType<GraphNameView>().Keyed<Terminal.Gui.View>(KeyFilters.CreateGraphView).WithAttributeFiltering();
            builder.RegisterType<GraphParametresView>().Keyed<Terminal.Gui.View>(KeyFilters.CreateGraphView).WithAttributeFiltering();
            builder.RegisterType<NeighborhoodFactoryView>().Keyed<Terminal.Gui.View>(KeyFilters.CreateGraphView).WithAttributeFiltering();
            builder.RegisterType<SmoothLevelView>().Keyed<Terminal.Gui.View>(KeyFilters.CreateGraphView).WithAttributeFiltering();
            builder.RegisterType<GraphFieldView>().Keyed<Terminal.Gui.View>(KeyFilters.MainWindow).WithAttributeFiltering();
            builder.RegisterType<RunsFrame>().Keyed<Terminal.Gui.View>(KeyFilters.RightPanel).WithAttributeFiltering();
            builder.RegisterType<RunsTableView>().Keyed<Terminal.Gui.View>(KeyFilters.GraphRunsView).WithAttributeFiltering();
            builder.RegisterType<RunsTableButtonsFrame>().Keyed<Terminal.Gui.View>(KeyFilters.GraphRunsView).WithAttributeFiltering();
            builder.RegisterType<NewRunButton>().Keyed<Terminal.Gui.View>(KeyFilters.RunButtonsFrame).WithAttributeFiltering();
            builder.RegisterType<DeleteRunButton>().Keyed<Terminal.Gui.View>(KeyFilters.RunButtonsFrame).WithAttributeFiltering();
            builder.RegisterType<AlgorithmsListView>().Keyed<Terminal.Gui.View>(KeyFilters.NewRunView).WithAttributeFiltering();
            builder.RegisterType<RunCreationView>().Keyed<Terminal.Gui.View>(KeyFilters.GraphRunsView).WithAttributeFiltering();
            builder.RegisterType<AlgorithmSettingsView>().Keyed<Terminal.Gui.View>(KeyFilters.NewRunView).WithAttributeFiltering();
            builder.RegisterType<DijkstraAlgorithmListItem>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmsListView).WithAttributeFiltering();
            builder.RegisterType<DistanceFirstAlgorithmListItem>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmsListView).WithAttributeFiltering();
            builder.RegisterType<HeuristicsCostAlgorithmListItem>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmsListView).WithAttributeFiltering();
            builder.RegisterType<CostGreedyAlgorithmListItem>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmsListView).WithAttributeFiltering();
            builder.RegisterType<DepthFirstAlgorithmListView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmsListView).WithAttributeFiltering();
            builder.RegisterType<AStarAlgorithmListItem>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmsListView).WithAttributeFiltering();
            builder.RegisterType<LeeAlgorithmListItem>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmsListView).WithAttributeFiltering();
            builder.RegisterType<AStarLeeAlgorithmListView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmsListView).WithAttributeFiltering();
            builder.RegisterType<IDAStarAlgorithmListItem>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmsListView).WithAttributeFiltering();
            builder.RegisterType<RandomAlgorithmListItem>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmsListView).WithAttributeFiltering();
            builder.RegisterType<StepRulesView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmParametresView).WithAttributeFiltering();
            builder.RegisterType<HeuristicsView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmParametresView).WithAttributeFiltering();
            builder.RegisterType<SpreadView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmParametresView).WithAttributeFiltering();
            builder.RegisterType<RunCreationButtonsFrame>().Keyed<Terminal.Gui.View>(KeyFilters.GraphRunsView).WithAttributeFiltering();
            builder.RegisterType<CreateRunButton>().Keyed<Terminal.Gui.View>(KeyFilters.CreateRunButtonsFrame).WithAttributeFiltering();
            builder.RegisterType<CloseRunCreationViewButton>().Keyed<Terminal.Gui.View>(KeyFilters.CreateRunButtonsFrame).WithAttributeFiltering();
            builder.RegisterType<AlgorithmRunView>().Keyed<Terminal.Gui.View>(KeyFilters.MainWindow).WithAttributeFiltering();

            return builder.Build();
        }

        private static void RegisterViewModels(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(BaseViewModel).Assembly)
                .Where(x => x.Name.EndsWith("ViewModel")).AsSelf()
                .AsImplementedInterfaces().SingleInstance().WithAttributeFiltering();
        }

        private static void RegisterAutoMapper(this ContainerBuilder builder)
        {
            builder.RegisterType<JsonSerializer<IEnumerable<VisitedVerticesModel>>>()
                .As<ISerializer<IEnumerable<VisitedVerticesModel>>>().SingleInstance();
            builder.RegisterType<JsonSerializer<IEnumerable<CoordinateModel>>>()
                .As<ISerializer<IEnumerable<CoordinateModel>>>().SingleInstance();
            builder.RegisterType<JsonSerializer<IEnumerable<int>>>()
                .As<ISerializer<IEnumerable<int>>>().SingleInstance();
            builder.RegisterType<JsonSerializer<IEnumerable<CostCoordinatePair>>>()
                .As<ISerializer<IEnumerable<CostCoordinatePair>>>().SingleInstance();

            builder.RegisterType<SubAlgorithmsMappingProfile>().As<Profile>().SingleInstance();
            builder.RegisterType<GraphStateMappingProfile>().As<Profile>().SingleInstance();
            builder.RegisterType<GraphMappingProfile<VertexModel>>().As<Profile>().SingleInstance();
            builder.RegisterType<UntitledMappingProfile>().As<Profile>().SingleInstance();
            builder.RegisterType<HistoryMappingProfile<VertexModel>>().As<Profile>().SingleInstance();
            builder.RegisterType<AlgorithmRunMappingProfile>().As<Profile>().SingleInstance();
            builder.RegisterType<VerticesMappingProfile<VertexModel>>().As<Profile>().SingleInstance();
            builder.RegisterType<StatisticsMappingProfile>().As<Profile>().SingleInstance();

            builder.Register(context =>
            {
                var profiles = context.Resolve<IEnumerable<Profile>>();
                var mappingConfig = new MapperConfiguration(c => c.AddProfiles(profiles));
                return mappingConfig.CreateMapper(context.Resolve);
            }).As<IMapper>().SingleInstance();
        }
    }
}
