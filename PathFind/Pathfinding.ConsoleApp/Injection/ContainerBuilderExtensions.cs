using Autofac;
using Autofac.Features.AttributeFilters;
using AutoMapper;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.View;
using Pathfinding.ConsoleApp.View.ButtonsFrameViews;
using Pathfinding.ConsoleApp.View.GraphCreateViews;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.ConsoleApp.ViewModel.ButtonsFrameViewModels;
using Pathfinding.ConsoleApp.ViewModel.Factories;
using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Infrastructure.Business;
using Pathfinding.Infrastructure.Business.Commands;
using Pathfinding.Infrastructure.Business.Mappings;
using Pathfinding.Infrastructure.Business.Serializers;
using Pathfinding.Infrastructure.Business.Serializers.Decorators;
using Pathfinding.Infrastructure.Data.InMemory;
using Pathfinding.Infrastructure.Data.Pathfinding.Factories;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Service.Interface.Models.Undefined;
using System.Collections.Generic;

namespace Pathfinding.ConsoleApp.Injection
{
    internal static class ContainerBuilderExtensions
    {
        public static IContainer BuildApp(this ContainerBuilder builder)
        {
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
            builder.RegisterType<GraphRunsView>().Keyed<Terminal.Gui.View>(KeyFilters.RightPanel).WithAttributeFiltering();
            builder.RegisterType<RunsTableView>().Keyed<Terminal.Gui.View>(KeyFilters.GraphRunsView).WithAttributeFiltering();

            builder.RegisterType<InMemoryUnitOfWorkFactory>().As<IUnitOfWorkFactory>().SingleInstance();
            builder.RegisterAutoMapper();
            builder.RegisterType<RequestService<VertexViewModel>>().As<IRequestService<VertexViewModel>>().SingleInstance();

            builder.RegisterType<WeakReferenceMessenger>().Keyed<IMessenger>(KeyFilters.Views).SingleInstance().WithAttributeFiltering();
            builder.RegisterType<WeakReferenceMessenger>().Keyed<IMessenger>(KeyFilters.ViewModels).SingleInstance().WithAttributeFiltering();

            builder.RegisterType<CreateGraphViewModel>().AsSelf().SingleInstance().WithAttributeFiltering();
            builder.RegisterType<SaveGraphButtonModel>().AsSelf().SingleInstance().WithAttributeFiltering();
            builder.RegisterType<LoadGraphButtonModel>().AsSelf().SingleInstance().WithAttributeFiltering();
            builder.RegisterType<GraphTableViewModel>().AsSelf().SingleInstance().WithAttributeFiltering();
            builder.RegisterType<GraphFieldViewModel>().AsSelf().SingleInstance().WithAttributeFiltering();
            builder.RegisterType<DeleteGraphButtonModel>().AsSelf().SingleInstance().WithAttributeFiltering();

            builder.RegisterType<IncludeSourceVertex<VertexViewModel>>().As<IPathfindingRangeCommand<VertexViewModel>>().SingleInstance();
            builder.RegisterType<IncludeTargetVertex<VertexViewModel>>().As<IPathfindingRangeCommand<VertexViewModel>>().SingleInstance();
            builder.RegisterType<IncludeTransitVertex<VertexViewModel>>().As<IPathfindingRangeCommand<VertexViewModel>>().SingleInstance();
            builder.RegisterType<ReplaceTransitIsolatedVertex<VertexViewModel>>().As<IPathfindingRangeCommand<VertexViewModel>>().SingleInstance();
            builder.RegisterType<ReplaceIsolatedSourceVertex<VertexViewModel>>().As<IPathfindingRangeCommand<VertexViewModel>>().SingleInstance();
            builder.RegisterType<ReplaceIsolatedTargetVertex<VertexViewModel>>().As<IPathfindingRangeCommand<VertexViewModel>>().SingleInstance();

            builder.RegisterType<GraphFactory<VertexViewModel>>().As<IGraphFactory<VertexViewModel>>().SingleInstance();
            builder.RegisterType<VertexViewModelFactory>().As<IVertexFactory<VertexViewModel>>().SingleInstance();
            builder.RegisterType<GraphAssemble<VertexViewModel>>().As<IGraphAssemble<VertexViewModel>>().SingleInstance();

            builder.RegisterType<JsonSerializer<GraphSerializationModel>>()
                .As<ISerializer<GraphSerializationModel>>().SingleInstance();

            builder.RegisterGenericDecorator(typeof(CompressSerializer<>), typeof(ISerializer<>));

            builder.Register(_ => new List<(string Name, int Level)>()
            {
                ("No", 0), ("Low", 1), ("Medium", 2), ("High", 4)
            }).As<IEnumerable<(string Name, int Level)>>().SingleInstance();

            builder.Register(_ => new List<(string Name, INeighborhoodFactory Factory)>()
            {
                ("Moore", new MooreNeighborhoodFactory()),
                ("Von Neimann", new VonNeumannNeighborhoodFactory())
            }).As<IEnumerable<(string Name, INeighborhoodFactory Factory)>>().SingleInstance();

            return builder.Build();
        }

        private static void RegisterAutoMapper(this ContainerBuilder builder)
        {
            builder.RegisterType<JsonSerializer<IEnumerable<VisitedVerticesModel>>>()
                .As<ISerializer<IEnumerable<VisitedVerticesModel>>>().SingleInstance();
            builder.RegisterType<JsonSerializer<IEnumerable<CoordinateModel>>>()
                .As<ISerializer<IEnumerable<CoordinateModel>>>().SingleInstance();
            builder.RegisterType<JsonSerializer<IEnumerable<int>>>()
                .As<ISerializer<IEnumerable<int>>>().SingleInstance();

            builder.RegisterType<SubAlgorithmsMappingProfile>().As<Profile>().SingleInstance();
            builder.RegisterType<GraphStateMappingProfile>().As<Profile>().SingleInstance();
            builder.RegisterType<GraphMappingProfile<VertexViewModel>>().As<Profile>().SingleInstance();
            builder.RegisterType<UntitledMappingProfile>().As<Profile>().SingleInstance();
            builder.RegisterType<HistoryMappingProfile<VertexViewModel>>().As<Profile>().SingleInstance();
            builder.RegisterType<AlgorithmRunMappingProfile>().As<Profile>().SingleInstance();
            builder.RegisterType<VerticesMappingProfile<VertexViewModel>>().As<Profile>().SingleInstance();
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
