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
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using WindowsFormsVersion.EventHolder;
using WindowsFormsVersion.Forms;
using WindowsFormsVersion.Model;
using WindowsFormsVersion.ViewModel;

namespace WindowsFormsVersion.Configure
{
    internal static class ContainerConfigure
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindow>().As<Form>().InstancePerLifetimeScope();
            builder.RegisterType<MainWindowViewModel>().As<IMainModel>().InstancePerLifetimeScope();
            builder.RegisterType<VertexFactory>().As<IVertexFactory>().SingleInstance();
            builder.RegisterType<PathInput>().As<IPathInput>().SingleInstance();
            builder.RegisterType<Coordinate2DFactory>().As<ICoordinateFactory>().SingleInstance();
            builder.RegisterType<Graph2DFactory>().As<IGraphFactory>().SingleInstance();
            builder.RegisterType<GraphFiller>().As<IGraphFiller>().SingleInstance();
            builder.RegisterType<GraphSerializer>().As<IGraphSerializer>().SingleInstance();
            builder.RegisterType<VertexEventHolder>().As<IVertexEventHolder>().SingleInstance();
            builder.RegisterType<GraphFieldFactory>().As<BaseGraphFieldFactory>().SingleInstance();
            builder.RegisterType<BinaryFormatter>().As<IFormatter>().SingleInstance();
            builder.RegisterType<VertexSerializationInfoConverter>().As<IVertexSerializationInfoConverter>().SingleInstance();

            return builder.Build();
        }
    }
}
