using AssembleClassesLib.Interface;
using AssembleClassesLib.Realizations;
using AssembleClassesLib.Realizations.AssembleClassesImpl;
using AssembleClassesLib.Realizations.LoadMethods;
using AssembleClassesLib.Tests.Infrastructure;
using Autofac;

namespace AssembleClassesLib.Tests.Configure
{
    internal static class ContainerConfigure
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<AssembleClasses>().As<IAssembleClasses>().SingleInstance();
            builder.RegisterType<AssempleLoadPath>().As<IAssembleLoadPath>().SingleInstance();
            builder.RegisterType<LoadFrom>().As<IAssembleLoadMethod>().SingleInstance();
            builder.RegisterType<TopDirectoryOnly>().As<IAssembleSearchOption>().SingleInstance();

            return builder.Build();
        }
    }
}
