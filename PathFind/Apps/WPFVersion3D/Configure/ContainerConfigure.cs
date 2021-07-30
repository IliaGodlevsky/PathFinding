using Autofac;
using GraphLib.Base;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
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
using System.Web.UI;
using WPFVersion3D.Interface;
using WPFVersion3D.Model;
using WPFVersion3D.Model3DFactories;
using WPFVersion3D.ViewModel;

namespace WPFVersion3D.Configure
{
    internal static class ContainerConfigure
    {
        private const string GraphAssembleName = nameof(GraphAssembleName);
        private static SmoothedGraphAssemble RegisterSmoothedGraphAssemble(IComponentContext context)
        {
            var randomGraphAssemble = context.ResolveNamed<IGraphAssemble>(GraphAssembleName);
            var costFactory = context.Resolve<IVertexCostFactory>();
            return new SmoothedGraphAssemble(randomGraphAssemble, costFactory);
        }

        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindowViewModel>().AsSelf().InstancePerLifetimeScope().PropertiesAutowired();

            builder.RegisterType<EndPoints>().As<BaseEndPoints>().SingleInstance();
            builder.RegisterType<Vertex3DEventHolder>().As<IVertexEventHolder>().SingleInstance();
            builder.RegisterType<GraphField3DFactory>().As<IGraphFieldFactory>().SingleInstance();

            builder.RegisterType<FileLog>().As<ILog>().SingleInstance();
            builder.RegisterType<MessageBoxLog>().As<ILog>().SingleInstance();
            builder.RegisterType<MailLog>().As<ILog>().SingleInstance();
            builder.RegisterComposite<Logs, ILog>().SingleInstance();

            builder.RegisterType<GraphAssemble>().Named<IGraphAssemble>(GraphAssembleName).As<IGraphAssemble>().SingleInstance();
            builder.Register(RegisterSmoothedGraphAssemble).As<IGraphAssemble>().SingleInstance();
            builder.RegisterType<Vertex3DFactory>().As<IVertexFactory>().SingleInstance();
            builder.RegisterType<Vertex3DCostFactory>().As<IVertexCostFactory>().SingleInstance();
            builder.RegisterType<Coordinate3DFactory>().As<ICoordinateFactory>().SingleInstance();
            builder.RegisterType<Graph3DFactory>().As<IGraphFactory>().SingleInstance();
            builder.RegisterType<CardinalNeighboursCoordinatesFactory>().As<INeighboursCoordinatesFactory>().SingleInstance();
            builder.RegisterType<CubicModel3DFactory>().As<IModel3DFactory>().SingleInstance();

            builder.RegisterType<SaveLoadGraph>().As<ISaveLoadGraph>().SingleInstance();
            builder.RegisterType<PathInput>().As<IPathInput>().SingleInstance();
            builder.RegisterType<GraphSerializer>().As<IGraphSerializer>().SingleInstance();
            builder.RegisterDecorator<CompressGraphSerializer, IGraphSerializer>();
            builder.RegisterDecorator<CryptoGraphSerializer, IGraphSerializer>();
            builder.RegisterType<ObjectStateFormatter>().As<IFormatter>().SingleInstance();
            builder.RegisterType<Vertex3DFromInfoFactory>().As<IVertexFromInfoFactory>().SingleInstance();

            return builder.Build();
        }
    }
}
