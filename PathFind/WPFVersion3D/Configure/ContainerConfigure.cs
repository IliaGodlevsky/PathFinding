using Autofac;
using GraphLib.Coordinates.Infrastructure.Factories;
using GraphLib.Coordinates.Infrastructure.Factories.Interfaces;
using GraphLib.EventHolder.Interface;
using GraphLib.GraphFieldCreating;
using GraphLib.Graphs.Factories;
using GraphLib.Graphs.Factories.Interfaces;
using GraphLib.Graphs.Serialization;
using GraphLib.Graphs.Serialization.Factories.Interfaces;
using GraphLib.Graphs.Serialization.Interfaces;
using GraphLib.Vertex.Infrastructure.Factories.Interfaces;
using GraphViewModel.Interfaces;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using WPFVersion3D.Model;
using WPFVersion3D.ViewModel;

namespace WPFVersion3D.Configure
{
    internal static class ContainerConfigure
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindowViewModel>().As<IMainModel>().InstancePerLifetimeScope();
            builder.RegisterType<Vertex3DFactory>().As<IVertexFactory>().SingleInstance();
            builder.RegisterType<Coordinate3DFactory>().As<ICoordinateFactory>().SingleInstance();
            builder.RegisterType<PathInput>().As<IPathInput>().SingleInstance();
            builder.RegisterType<Graph3DFactory>().As<IGraphFactory>().SingleInstance();
            builder.RegisterType<GraphAssembler>().As<IGraphAssembler>().SingleInstance();
            builder.RegisterType<GraphSerializer>().As<IGraphSerializer>().SingleInstance();
            builder.RegisterType<Vertex3DEventHolder>().As<IVertexEventHolder>().SingleInstance();
            builder.RegisterType<GraphField3DFactory>().As<BaseGraphFieldFactory>().SingleInstance();
            builder.RegisterType<SoapFormatter>().As<IFormatter>().SingleInstance();
            builder.RegisterType<Vertex3DSerializationInfoConverter>().As<IVertexSerializationInfoConverter>().SingleInstance();

            return builder.Build();
        }
    }
}
