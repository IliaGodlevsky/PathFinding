using Algorithm.Factory;
using Algorithm.Interfaces;
using Algorithm.Realizations.StepRules;
using Autofac;
using Common.Extensions;
using ConsoleVersion.Enums;
using ConsoleVersion.Interface;
using ConsoleVersion.Model;
using ConsoleVersion.ValueInput;
using ConsoleVersion.View;
using ConsoleVersion.ViewModel;
using GraphLib.Base;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Factories;
using GraphLib.Realizations.Factories.CoordinateFactories;
using GraphLib.Realizations.Factories.GraphAssembles;
using GraphLib.Realizations.Factories.GraphFactories;
using GraphLib.Realizations.Factories.NeighboursCoordinatesFactories;
using GraphLib.Realizations.MeanCosts;
using GraphLib.Serialization;
using GraphLib.Serialization.Interfaces;
using GraphLib.Serialization.Serializers;
using Logging.Interface;
using Logging.Loggers;
using Random.Interface;
using Random.Realizations;
using System;
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
            var meanCost = context.Resolve<IMeanCost>();
            return new SmoothedGraphAssemble(randomGraphAssemble, costFactory, meanCost);
        }

        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EnumConsoleValueInput<Answer>>().As<IValueInput<Answer>>().SingleInstance();
            builder.RegisterType<Int32ConsoleValueInput>().As<IValueInput<int>>().SingleInstance();

            builder.RegisterType<MainView>().As<IView>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<MainViewModel>().AsSelf().SingleInstance().PropertiesAutowired();

            builder.RegisterType<EndPoints>().As<BaseEndPoints>().SingleInstance();
            builder.RegisterType<VertexEventHolder>().As<IVertexEventHolder>().SingleInstance().PropertiesAutowired();
            builder.RegisterType<GraphFieldFactory>().As<IGraphFieldFactory>().SingleInstance();

            builder.RegisterType<FileLog>().As<ILog>().SingleInstance();
            builder.RegisterType<ConsoleLog>().As<ILog>().SingleInstance();
            builder.RegisterType<MailLog>().As<ILog>().SingleInstance();
            builder.RegisterComposite<Logs, ILog>().SingleInstance();
            // Graph creating block
            builder.RegisterType<PseudoRandom>().As<IRandom>().SingleInstance();
            builder.RegisterType<GraphAssemble>().As<IGraphAssemble>().SingleInstance().Named<IGraphAssemble>(GraphAssemble);
            builder.Register(RegisterSmoothedGraphAssemble).As<IGraphAssemble>().SingleInstance();
            builder.RegisterType<CostFactory>().As<IVertexCostFactory>().SingleInstance();
            builder.RegisterType<VertexFactory>().As<IVertexFactory>().SingleInstance();
            builder.RegisterType<Coordinate2DFactory>().As<ICoordinateFactory>().SingleInstance();
            builder.RegisterType<Graph2DFactory>().As<IGraphFactory>().SingleInstance();
            builder.RegisterType<MooreNeighborhoodFactory>().As<INeighborhoodFactory>().SingleInstance();
            builder.RegisterType<RootMeanSquareCost>().As<IMeanCost>().SingleInstance();
            // Graph serailizing block
            builder.RegisterType<GraphSerializationModule>().AsSelf().SingleInstance();
            builder.RegisterType<PathInput>().As<IPathInput>().SingleInstance();
            builder.RegisterType<GraphSerializer>().As<IGraphSerializer>().SingleInstance();
            builder.RegisterDecorator<CryptoGraphSerializer, IGraphSerializer>();
            builder.RegisterType<BinaryFormatter>().As<IFormatter>().SingleInstance();
            builder.RegisterType<VertexFromInfoFactory>().As<IVertexFromInfoFactory>().SingleInstance();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(type => type.ImplementsAll(typeof(IAlgorithmFactory)))
                .As<IAlgorithmFactory>().SingleInstance();

            builder.RegisterType<LandscapeStepRule>().As<IStepRule>().SingleInstance();
            builder.RegisterDecorator<WalkStepRule, IStepRule>();
            builder.RegisterDecorator<RatedStepRule, IStepRule>();

            return builder.Build();
        }
    }
}
