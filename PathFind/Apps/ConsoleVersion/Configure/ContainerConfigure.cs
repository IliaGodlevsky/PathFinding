﻿using Algorithm.Realizations;
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

            builder.RegisterType<MainView>().As<IView>().InstancePerLifetimeScope();
            builder.RegisterType<MainViewModel>().As<IMainModel>().InstancePerLifetimeScope().PropertiesAutowired();

            builder.RegisterType<EndPoints>().As<BaseEndPoints>().SingleInstance();
            builder.RegisterType<VertexEventHolder>().As<IVertexEventHolder>().SingleInstance();
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
            builder.RegisterType<VertexSerializationInfoConverter>().As<IVertexSerializationInfoConverter>().SingleInstance();

            builder.RegisterType<ConcreteAssembleAlgorithmClasses>().As<IAssembleClasses>().SingleInstance();
            builder.RegisterType<AssembleLoadPath>().As<IAssembleLoadPath>().SingleInstance();
            builder.RegisterType<AllDirectories>().As<IAssembleSearchOption>().SingleInstance();
            builder.RegisterType<LoadFrom>().As<IAssembleLoadMethod>().SingleInstance();

            return builder.Build();
        }
    }
}
