using Autofac;
using ConsoleVersion.App;
using ConsoleVersion.Model;
using ConsoleVersion.View;
using ConsoleVersion.View.Interface;
using ConsoleVersion.ViewModel;
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
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleVersion.Configure
{
    internal static class ContainerConfigure
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Application>().As<IApplication>().InstancePerLifetimeScope();
            builder.RegisterType<MainView>().As<IView>().InstancePerLifetimeScope();
            builder.RegisterType<MainViewModel>().As<IMainModel>().InstancePerLifetimeScope();
            builder.RegisterType<VertexFactory>().As<IVertexFactory>().SingleInstance();
            builder.RegisterType<PathInput>().As<IPathInput>().SingleInstance();
            builder.RegisterType<Coordinate2DFactory>().As<ICoordinateFactory>().SingleInstance();
            builder.RegisterType<Graph2DFactory>().As<IGraphFactory>().SingleInstance();
            builder.RegisterType<GraphAssembler>().As<IGraphAssembler>().SingleInstance();
            builder.RegisterType<GraphSerializer>().As<IGraphSerializer>().SingleInstance();
            builder.RegisterType<VertexEventHolder>().As<IVertexEventHolder>().SingleInstance();
            builder.RegisterType<GraphFieldFactory>().As<BaseGraphFieldFactory>().SingleInstance();
            builder.RegisterType<BinaryFormatter>().As<IFormatter>().SingleInstance();
            builder.RegisterType<VertexSerializationInfoConverter>().As<IVertexSerializationInfoConverter>().SingleInstance();

            return builder.Build();
        }
    }
}
