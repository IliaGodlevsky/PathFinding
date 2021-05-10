using Algorithm.Realizations;
using AssembleClassesLib.Interface;
using AssembleClassesLib.Realizations;
using AssembleClassesLib.Realizations.LoadMethods;
using Autofac;
using ConsoleVersion.Model;
using ConsoleVersion.View;
using ConsoleVersion.View.Interface;
using ConsoleVersion.ViewModel;
using GraphLib.Base;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Factories;
using GraphLib.Serialization;
using GraphLib.Serialization.Interfaces;
using GraphViewModel.Interfaces;
using Logging;
using Logging.Interface;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleVersion.Configure
{
    internal static class ContainerConfigure
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainView>().As<IView>().InstancePerLifetimeScope();
            builder.RegisterType<MainViewModel>().As<IMainModel>().InstancePerLifetimeScope().PropertiesAutowired();

            builder.RegisterType<EndPoints>().As<BaseEndPoints>().SingleInstance();
            builder.RegisterType<PathInput>().As<IPathInput>().SingleInstance();
            builder.RegisterType<VertexEventHolder>().As<IVertexEventHolder>().SingleInstance();
            builder.RegisterType<GraphFieldFactory>().As<BaseGraphFieldFactory>().SingleInstance();

            builder.RegisterType<FileLog>().As<ILog>().SingleInstance();
            builder.RegisterType<ConsoleLog>().As<ILog>().SingleInstance();
            builder.RegisterType<Logs>().AsSelf().SingleInstance();

            builder.RegisterType<GraphAssemble>().As<IGraphAssemble>().SingleInstance();
            builder.RegisterType<CostFactory>().As<IVertexCostFactory>().SingleInstance();
            builder.RegisterType<VertexFactory>().As<IVertexFactory>().SingleInstance();
            builder.RegisterType<Coordinate2DFactory>().As<ICoordinateFactory>().SingleInstance();
            builder.RegisterType<Graph2DFactory>().As<IGraphFactory>().SingleInstance();
            builder.RegisterType<CoordinateAroundRadarFactory>().As<ICoordinateRadarFactory>().SingleInstance();

            builder.RegisterType<GraphSerializer>().As<IGraphSerializer>().SingleInstance();
            builder.RegisterType<BinaryFormatter>().As<IFormatter>().SingleInstance();
            builder.RegisterType<VertexSerializationInfoConverter>().As<IVertexSerializationInfoConverter>().SingleInstance();

            builder.RegisterType<ConcreteAssembleAlgorithmClasses>().As<IAssembleClasses>().SingleInstance();
            builder.RegisterType<AssembleLoadPath>().As<IAssembleLoadPath>().SingleInstance();
            builder.RegisterType<AllDirectories>().As<IAssembleSearchOption>().SingleInstance();
            builder.RegisterType<LoadFrom>().As<IAssembleLoadMethod>().SingleInstance();

            return builder.Build();
        }
    }
}
