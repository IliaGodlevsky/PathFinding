using Autofac;
using GraphLib.Interfaces.Factories;
using GraphLib.Serialization.Interfaces;
using GraphLib.Serialization.Serializers;
using GraphLib.TestRealizations.TestFactories;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace GraphLib.Serialization.Tests.Configure
{
    internal static class ContainerConfigure
    {
        public static IContainer GraphSerializerConfigure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<TestGraphAssemble>().As<IGraphAssemble>().SingleInstance();
            builder.RegisterType<GraphSerializer>().As<IGraphSerializer>().SingleInstance();
            builder.RegisterType<BinaryFormatter>().As<IFormatter>().SingleInstance();
            builder.RegisterType<TestVertexInfoSerializationConverter>()
                .As<IVertexSerializationInfoConverter>().SingleInstance();
            builder.RegisterType<TestGraphFactory>().As<IGraphFactory>().SingleInstance();

            return builder.Build();
        }

        public static IContainer CryptoGraphSerializerConfigure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<TestGraphAssemble>().As<IGraphAssemble>().SingleInstance();
            builder.RegisterType<CryptoGraphSerializer>().As<IGraphSerializer>().SingleInstance();
            builder.RegisterType<DESCryptoServiceProvider>().As<SymmetricAlgorithm>().SingleInstance();
            builder.RegisterType<BinaryFormatter>().As<IFormatter>().SingleInstance();
            builder.RegisterType<TestVertexInfoSerializationConverter>()
                .As<IVertexSerializationInfoConverter>().SingleInstance();
            builder.RegisterType<TestGraphFactory>().As<IGraphFactory>().SingleInstance();

            return builder.Build();
        }
        
    }
}
