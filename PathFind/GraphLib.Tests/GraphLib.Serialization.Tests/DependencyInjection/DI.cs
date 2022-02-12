using Autofac;
using GraphLib.Interfaces.Factories;
using GraphLib.Serialization.Interfaces;
using GraphLib.Serialization.Serializers;
using GraphLib.TestRealizations.TestFactories;
using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace GraphLib.Serialization.Tests.DependencyInjection
{
    internal static class DI
    {
        public static IContainer CryptoSerializerContainer => cryptoSerializerContainer.Value;
        public static IContainer SerializerContainer => serializerContainer.Value;
        public static IContainer CompressSerializerContainer => compressGraphSerializer.Value;

        private static IContainer GraphSerializerConfigure(Action<ContainerBuilder> serializerRegister)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<TestGraphAssemble>().As<IGraphAssemble>().SingleInstance();
            serializerRegister(builder);
            builder.RegisterType<BinaryFormatter>().As<IFormatter>().SingleInstance();
            builder.RegisterType<TestVertexFromInfoFactory>().As<IVertexFromInfoFactory>().SingleInstance();
            builder.RegisterType<TestGraphFactory>().As<IGraphFactory>().SingleInstance();

            return builder.Build();
        }

        private static void RegisterGraphSerializer(ContainerBuilder builder)
        {
            builder.RegisterType<GraphSerializer>().As<IGraphSerializer>().SingleInstance();
        }

        private static void RegisterCryptoGraphSerializer(ContainerBuilder builder)
        {
            RegisterGraphSerializer(builder);
            builder.RegisterDecorator<CryptoGraphSerializer, IGraphSerializer>();
        }

        private static void RegisterCompressGraphSerializer(ContainerBuilder builder)
        {
            RegisterCryptoGraphSerializer(builder);
            builder.RegisterDecorator<CompressGraphSerializer, IGraphSerializer>();
        }

        private static readonly Lazy<IContainer> compressGraphSerializer
            = new Lazy<IContainer>(() => GraphSerializerConfigure(RegisterCompressGraphSerializer));

        private static readonly Lazy<IContainer> cryptoSerializerContainer
            = new Lazy<IContainer>(() => GraphSerializerConfigure(RegisterCryptoGraphSerializer));

        private static readonly Lazy<IContainer> serializerContainer
            = new Lazy<IContainer>(() => GraphSerializerConfigure(RegisterGraphSerializer));
    }
}
