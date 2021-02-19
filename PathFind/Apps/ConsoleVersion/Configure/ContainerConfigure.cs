using Autofac;
using ConsoleVersion.App;
using ConsoleVersion.Model;
using ConsoleVersion.View;
using ConsoleVersion.View.Interface;
using ConsoleVersion.ViewModel;
using GraphLib.Base;
using GraphLib.Base.EndPoints;
using GraphLib.Interface;
using GraphLib.Realizations.Factories;
using GraphLib.Serialization;
using GraphLib.Serialization.Interfaces;
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
            
            builder.RegisterType<Application>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<MainView>().As<IView>().InstancePerLifetimeScope();
            builder.RegisterType<EndPoints>().As<BaseEndPoints>().SingleInstance();
            builder.RegisterType<CostFactory>().As<IVertexCostFactory>().SingleInstance();
            builder.RegisterType<MainViewModel>().As<IMainModel>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<VertexFactory>().As<IVertexFactory>().SingleInstance();
            builder.RegisterType<PathInput>().As<IPathInput>().SingleInstance();
            builder.RegisterType<Coordinate2DFactory>().As<ICoordinateFactory>().SingleInstance();
            builder.RegisterType<Graph2DFactory>().As<IGraphFactory>().SingleInstance();
            builder.RegisterType<GraphAssembler>().As<IGraphAssembler>().SingleInstance();
            builder.RegisterType<GraphSerializer>().As<IGraphSerializer>().SingleInstance();
            builder.RegisterType<VertexEventHolder>().As<IVertexEventHolder>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<GraphFieldFactory>().As<BaseGraphFieldFactory>().SingleInstance();
            builder.RegisterType<BinaryFormatter>().As<IFormatter>().SingleInstance();
            builder.RegisterType<VertexSerializationInfoConverter>()
                .As<IVertexSerializationInfoConverter>().SingleInstance();

            return builder.Build();
        }
    }
}
