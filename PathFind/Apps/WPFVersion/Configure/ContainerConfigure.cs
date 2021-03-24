using Autofac;
using GraphLib.Base;
using GraphLib.Interface;
using GraphLib.Realizations.Factories;
using GraphLib.Serialization;
using GraphLib.Serialization.Interfaces;
using GraphViewModel.Interfaces;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using WPFVersion.Model;
using WPFVersion.Model.EventHolder;
using WPFVersion.ViewModel;

namespace WPFVersion.Configure
{
    internal static class ContainerConfigure
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EndPoints>().As<BaseEndPoints>().SingleInstance();
            builder.RegisterType<MainWindowViewModel>().As<IMainModel>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<VertexFactory>().As<IVertexFactory>().SingleInstance();
            builder.RegisterType<CostFactory>().As<IVertexCostFactory>().SingleInstance();
            builder.RegisterType<Coordinate2DFactory>().As<ICoordinateFactory>().SingleInstance();
            builder.RegisterType<PathInput>().As<IPathInput>().SingleInstance();
            builder.RegisterType<Graph2DFactory>().As<IGraphFactory>().SingleInstance();
            builder.RegisterType<GraphAssembler>().As<IGraphAssembler>().SingleInstance();
            builder.RegisterType<GraphSerializer>().As<IGraphSerializer>().SingleInstance();
            builder.RegisterType<VertexEventHolder>().As<IVertexEventHolder>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<GraphFieldFactory>().As<BaseGraphFieldFactory>().SingleInstance();
            builder.RegisterType<BinaryFormatter>().As<IFormatter>().SingleInstance();
            builder.RegisterType<VertexSerializationInfoConverter>().As<IVertexSerializationInfoConverter>().SingleInstance();

            return builder.Build();
        }
    }
}
