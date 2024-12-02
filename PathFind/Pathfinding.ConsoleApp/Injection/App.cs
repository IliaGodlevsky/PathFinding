using Autofac;
using Autofac.Features.AttributeFilters;
using AutoMapper;
using AutoMapper.Configuration;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.View;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Infrastructure.Business;
using Pathfinding.Infrastructure.Business.Commands;
using Pathfinding.Infrastructure.Business.Mappings;
using Pathfinding.Infrastructure.Business.Serializers;
using Pathfinding.Infrastructure.Business.Serializers.Decorators;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Infrastructure.Data.Sqlite;
using Pathfinding.Logging.Interface;
using Pathfinding.Logging.Loggers;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Service.Interface.Models.Serialization;
using System.Collections.Generic;

namespace Pathfinding.ConsoleApp.Injection
{
    internal static class App
    {
        public static ILifetimeScope Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<GraphAssemble<GraphVertexModel>>()
                .As<IGraphAssemble<GraphVertexModel>>().SingleInstance();
            builder.RegisterType<GraphAssemble<RunVertexModel>>()
                .As<IGraphAssemble<RunVertexModel>>().SingleInstance();

            builder.Register(_ => new SqliteUnitOfWorkFactory(AppSettings.Default.ConnectionString))
                .As<IUnitOfWorkFactory>().SingleInstance();

            builder.RegisterType<GraphMappingProfile<GraphVertexModel>>().As<Profile>().SingleInstance();
            builder.RegisterType<UntitledMappingProfile>().As<Profile>().SingleInstance();
            builder.RegisterType<HistoryMappingProfile<GraphVertexModel>>().As<Profile>().SingleInstance();
            builder.RegisterType<VerticesMappingProfile<GraphVertexModel>>().As<Profile>().SingleInstance();
            builder.RegisterType<StatisticsMappingProfile>().As<Profile>().SingleInstance();
            builder.RegisterType<MapperConfiguration>().As<IConfigurationProvider>().SingleInstance();
            builder.RegisterType<MapperConfigurationExpression>().AsSelf().SingleInstance()
                .OnActivating(x => x.Instance.AddProfiles(x.Context.Resolve<IEnumerable<Profile>>()));
            builder.RegisterType<Mapper>().As<IMapper>().SingleInstance();

            builder.RegisterType<RequestService<GraphVertexModel>>().As<IRequestService<GraphVertexModel>>().SingleInstance();
            //builder.RegisterDecorator<CachedRequestService<GraphVertexModel>, IRequestService<GraphVertexModel>>();

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

            builder.RegisterType<WeakReferenceMessenger>().Keyed<IMessenger>(KeyFilters.Views)
                .SingleInstance().WithAttributeFiltering();
            builder.RegisterType<WeakReferenceMessenger>().Keyed<IMessenger>(KeyFilters.ViewModels)
                .SingleInstance().WithAttributeFiltering();

            builder.RegisterAssemblyTypes(typeof(BaseViewModel).Assembly)
                .SingleInstance().Where(x => x.Name.EndsWith("ViewModel")).AsSelf()
                .AsImplementedInterfaces().WithAttributeFiltering();

            builder.RegisterType<MainView>().AsSelf().WithAttributeFiltering();

            builder.RegisterType<RightPanelView>().Keyed<Terminal.Gui.View>(KeyFilters.MainWindow).WithAttributeFiltering();
            builder.RegisterType<GraphFieldView>().Keyed<Terminal.Gui.View>(KeyFilters.MainWindow).WithAttributeFiltering();
            builder.RegisterType<AlgorithmRunFieldView>().Keyed<Terminal.Gui.View>(KeyFilters.MainWindow).WithAttributeFiltering();
            builder.RegisterType<AlgorithmRunProgressView>().Keyed<Terminal.Gui.View>(KeyFilters.MainWindow).WithAttributeFiltering();

            builder.RegisterType<GraphPanel>().Keyed<Terminal.Gui.View>(KeyFilters.RightPanel).WithAttributeFiltering();
            builder.RegisterType<RunsPanel>().Keyed<Terminal.Gui.View>(KeyFilters.RightPanel).WithAttributeFiltering();
            
            builder.RegisterType<GraphsTableView>().Keyed<Terminal.Gui.View>(KeyFilters.GraphPanel).WithAttributeFiltering();
            builder.RegisterType<GraphTableButtonsFrame>().Keyed<Terminal.Gui.View>(KeyFilters.GraphPanel).WithAttributeFiltering();
            builder.RegisterType<GraphAssembleView>().Keyed<Terminal.Gui.View>(KeyFilters.GraphPanel).WithAttributeFiltering();
            builder.RegisterType<GraphUpdateView>().Keyed<Terminal.Gui.View>(KeyFilters.GraphPanel).WithAttributeFiltering();

            builder.RegisterType<NewGraphButton>().Keyed<Terminal.Gui.View>(KeyFilters.GraphTableButtons).WithAttributeFiltering();
            builder.RegisterType<UpdateGraphButton>().Keyed<Terminal.Gui.View>(KeyFilters.GraphTableButtons).WithAttributeFiltering();
            builder.RegisterType<GraphCopyView>().Keyed<Terminal.Gui.View>(KeyFilters.GraphTableButtons).WithAttributeFiltering();
            builder.RegisterType<GraphExportView>().Keyed<Terminal.Gui.View>(KeyFilters.GraphTableButtons).WithAttributeFiltering();
            builder.RegisterType<GraphImportView>().Keyed<Terminal.Gui.View>(KeyFilters.GraphTableButtons).WithAttributeFiltering();
            builder.RegisterType<DeleteGraphButton>().Keyed<Terminal.Gui.View>(KeyFilters.GraphTableButtons).WithAttributeFiltering();

            builder.RegisterType<GraphNameView>().Keyed<Terminal.Gui.View>(KeyFilters.GraphAssembleView).WithAttributeFiltering();
            builder.RegisterType<GraphParametresView>().Keyed<Terminal.Gui.View>(KeyFilters.GraphAssembleView).WithAttributeFiltering();
            builder.RegisterType<NeighborhoodFactoryView>().Keyed<Terminal.Gui.View>(KeyFilters.GraphAssembleView).WithAttributeFiltering();
            builder.RegisterType<SmoothLevelView>().Keyed<Terminal.Gui.View>(KeyFilters.GraphAssembleView).WithAttributeFiltering();

            builder.RegisterType<GraphNameUpdateView>().Keyed<Terminal.Gui.View>(KeyFilters.GraphUpdateView).WithAttributeFiltering();
            builder.RegisterType<SmoothLevelUpdateView>().Keyed<Terminal.Gui.View>(KeyFilters.GraphUpdateView).WithAttributeFiltering();
            builder.RegisterType<NeighborhoodFactoryUpdateView>().Keyed<Terminal.Gui.View>(KeyFilters.GraphUpdateView).WithAttributeFiltering();

            builder.RegisterType<RunsTableView>().Keyed<Terminal.Gui.View>(KeyFilters.RunsPanel).WithAttributeFiltering();
            builder.RegisterType<RunsTableButtonsFrame>().Keyed<Terminal.Gui.View>(KeyFilters.RunsPanel).WithAttributeFiltering();
            builder.RegisterType<AlgorithmCreationView>().Keyed<Terminal.Gui.View>(KeyFilters.RunsPanel).WithAttributeFiltering();
            builder.RegisterType<PathfindingProcessButtonsFrame>().Keyed<Terminal.Gui.View>(KeyFilters.RunsPanel).WithAttributeFiltering();

            builder.RegisterType<NewRunButton>().Keyed<Terminal.Gui.View>(KeyFilters.RunButtonsFrame).WithAttributeFiltering();
            builder.RegisterType<AlgorithmUpdateView>().Keyed<Terminal.Gui.View>(KeyFilters.RunButtonsFrame).WithAttributeFiltering();
            builder.RegisterType<DeleteRunButton>().Keyed<Terminal.Gui.View>(KeyFilters.RunButtonsFrame).WithAttributeFiltering();

            builder.RegisterType<AlgorithmsView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmCreationView).WithAttributeFiltering();
            builder.RegisterType<AlgorithmParametresView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmCreationView).WithAttributeFiltering();

            builder.RegisterType<StepRulesView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmParametresView).WithAttributeFiltering();
            builder.RegisterType<HeuristicsView>().Keyed<Terminal.Gui.View>(KeyFilters.AlgorithmParametresView).WithAttributeFiltering();

            builder.RegisterType<PathfindingProcessView>().Keyed<Terminal.Gui.View>(KeyFilters.CreateRunButtonsFrame).WithAttributeFiltering();
            builder.RegisterType<CloseAlgorithmCreationButton>().Keyed<Terminal.Gui.View>(KeyFilters.CreateRunButtonsFrame).WithAttributeFiltering();

            return builder.Build();
        }
    }
}