using Autofac;
using ConsoleVersion.Enums;
using ConsoleVersion.Model;
using ConsoleVersion.ValueInput;
using ConsoleVersion.ValueInput.Interface;
using ConsoleVersion.View;
using ConsoleVersion.View.Interface;
using ConsoleVersion.ViewModel;
using GraphLib.Base;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Factories;
using GraphLib.Realizations.Factories.CoordinateFactories;
using GraphLib.Realizations.Factories.GraphAssembles;
using GraphLib.Realizations.Factories.GraphFactories;
using GraphLib.Realizations.Factories.NeighboursCoordinatesFactories;
using GraphLib.Serialization.Interfaces;
using GraphLib.Serialization.Serializers;
using GraphViewModel;
using GraphViewModel.Interfaces;
using Logging.Interface;
using Logging.Loggers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleVersion.Configure
{
    internal static class ContainerConfigure
    {
        private const string GraphAssemble = nameof(GraphAssemble);

        private static SmoothedGraphAssemble RegisterSmoothedGraphAssemble(IComponentContext context)
        {
            var randomGraphAssemble = context.ResolveNamed<IGraphAssemble>(GraphAssemble);
            var costFactory = context.Resolve<IVertexCostFactory>();
            return new SmoothedGraphAssemble(randomGraphAssemble, costFactory);
        }

        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.Register(container => EnumValueInput<Answer>.Instance).As<IValueInput<Answer>>();
            builder.Register(container => Int32ValueInput.Instance).As<IValueInput<int>>();

            builder.RegisterType<MainView>().As<IView>().SingleInstance();
            builder.RegisterType<MainViewModel>().AsSelf().SingleInstance().PropertiesAutowired();

            builder.RegisterType<EndPoints>().As<BaseEndPoints>().SingleInstance();
            builder.RegisterType<VertexEventHolder>().As<IVertexEventHolder>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<GraphFieldFactory>().As<IGraphFieldFactory>().SingleInstance();

            builder.RegisterType<FileLog>().As<ILog>().SingleInstance();
            builder.RegisterType<ConsoleLog>().As<ILog>().SingleInstance();
            builder.RegisterType<MailLog>().As<ILog>().SingleInstance();
            builder.RegisterComposite<Logs, ILog>().SingleInstance();

            builder.RegisterType<GraphAssemble>().As<IGraphAssemble>().SingleInstance().Named<IGraphAssemble>(GraphAssemble);
            builder.Register(RegisterSmoothedGraphAssemble).As<IGraphAssemble>().SingleInstance();
            builder.RegisterType<CostFactory>().As<IVertexCostFactory>().SingleInstance();
            builder.RegisterType<VertexFactory>().As<IVertexFactory>().SingleInstance();
            builder.RegisterType<Coordinate2DFactory>().As<ICoordinateFactory>().SingleInstance();
            builder.RegisterType<Graph2DFactory>().As<IGraphFactory>().SingleInstance();
            builder.RegisterType<AroundNeighboursCoordinatesFactory>().As<INeighboursCoordinatesFactory>().SingleInstance();

            builder.RegisterType<SaveLoadGraph>().As<ISaveLoadGraph>().SingleInstance();
            builder.RegisterType<PathInput>().As<IPathInput>().SingleInstance();
            builder.RegisterType<GraphSerializer>().As<IGraphSerializer>().SingleInstance();
            builder.RegisterDecorator<CryptoGraphSerializer, IGraphSerializer>();
            builder.RegisterType<BinaryFormatter>().As<IFormatter>().SingleInstance();
            builder.RegisterType<VertexFromInfoFactory>().As<IVertexFromInfoFactory>().SingleInstance();

            return builder.Build();
        }
    }
}
