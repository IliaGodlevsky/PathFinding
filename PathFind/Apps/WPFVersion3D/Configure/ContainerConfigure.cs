using Algorithm.Realizations;
using AssembleClassesLib.Interface;
using AssembleClassesLib.Realizations;
using AssembleClassesLib.Realizations.LoadMethods;
using Autofac;
using GraphLib.Base;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations;
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
using System.Runtime.Serialization.Formatters.Soap;
using System.Security.Cryptography;
using WPFVersion3D.Interface;
using WPFVersion3D.Model;
using WPFVersion3D.ViewModel;

namespace WPFVersion3D.Configure
{
    internal static class ContainerConfigure
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindowViewModel>().AsSelf().InstancePerLifetimeScope().PropertiesAutowired();

            builder.RegisterType<EndPoints>().As<BaseEndPoints>().SingleInstance();
            builder.RegisterType<Vertex3DEventHolder>().As<IVertexEventHolder>().SingleInstance();
            builder.RegisterType<GraphField3DFactory>().As<IGraphFieldFactory>().SingleInstance();

            builder.RegisterType<FileLog>().As<ILog>().SingleInstance();
            builder.RegisterType<MessageBoxLog>().As<ILog>().SingleInstance();
            builder.RegisterType<Logs>().AsSelf().SingleInstance();

            builder.RegisterType<ConcreteGraphAssembleClasses>().AsSelf().SingleInstance();
            builder.RegisterType<GraphAssemble>().As<IGraphAssemble>().SingleInstance();
            builder.RegisterType<Vertex3DFactory>().As<IVertexFactory>().SingleInstance();
            builder.RegisterType<Vertex3DCostFactory>().As<IVertexCostFactory>().SingleInstance();
            builder.RegisterType<Coordinate3DFactory>().As<ICoordinateFactory>().SingleInstance();
            builder.RegisterType<Graph3DFactory>().As<IGraphFactory>().SingleInstance();
            builder.RegisterType<CardinalNeighboursCoordinatesFactory>().As<INeighboursCoordinatesFactory>().SingleInstance();
            builder.RegisterType<CubicModel3DFactory>().As<IModel3DFactory>().SingleInstance();

            builder.RegisterType<SaveLoadGraph>().As<ISaveLoadGraph>().SingleInstance();
            builder.RegisterType<PathInput>().As<IPathInput>().SingleInstance();
            builder.RegisterType<GraphSerializer>().As<IGraphSerializer>().SingleInstance();
            builder.RegisterDecorator<CryptoGraphSerializer, IGraphSerializer>();
            builder.RegisterType<AesCryptoServiceProvider>().As<SymmetricAlgorithm>().SingleInstance();
            builder.RegisterType<SoapFormatter>().As<IFormatter>().SingleInstance();
            builder.RegisterType<Vertex3DSerializationInfoConverter>().As<IVertexSerializationInfoConverter>().SingleInstance();

            builder.RegisterType<ConcreteAssembleAlgorithmClasses>().AsSelf().SingleInstance();
            builder.RegisterType<AssembleLoadPath>().As<IAssembleLoadPath>().SingleInstance();
            builder.RegisterType<AllDirectories>().As<IAssembleSearchOption>().SingleInstance();
            builder.RegisterType<LoadFrom>().As<IAssembleLoadMethod>().SingleInstance();

            return builder.Build();
        }
    }
}
