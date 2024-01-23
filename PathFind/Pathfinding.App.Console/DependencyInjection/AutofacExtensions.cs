using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Core.Resolving.Pipeline;
using Autofac.Features.Metadata;
using AutoMapper;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DependencyInjection
{
    internal static class AutofacExtensions
    {
        public static void RegisterUnits(this ContainerBuilder builder, params Type[] units)
        {
            units.ForEach(unit => builder.RegisterUnit(new UnitParamtresFactory(), unit));
        }

        public static void RegisterUnit<TUnit>(this ContainerBuilder builder,
            IParametresFactory factory)
            where TUnit : IUnit
        {
            builder.RegisterUnit(factory, typeof(TUnit));
        }

        public static void RegisterUnit<TUnit>(this ContainerBuilder builder)
            where TUnit : IUnit
        {
            builder.RegisterUnits(typeof(TUnit));
        }

        public static void RegisterUnit(this ContainerBuilder builder,
            IParametresFactory factory, Type unit)
        {
            var resolveMiddleware 
                = new UnitResolveMiddleware(RegistrationConstants.UnitTypeKey, unit, factory);
            builder.RegisterType(unit).AsSelf().AsImplementedInterfaces().AutoActivate()
                .SingleInstance().ConfigurePipeline(p => p.Use(resolveMiddleware));
        }

        public static IReadOnlyDictionary<TKey, TValue> ResolveWithMetadata<TKey, TValue>(this IComponentContext context, string key)
        {
            return context.Resolve<IEnumerable<Meta<TValue>>>()
                .ToDictionary(action => (TKey)action.Metadata[key], action => action.Value)
                .AsReadOnly();
        }

        public static IReadOnlyDictionary<TKey, TValue> ResolveWithMetadataKeyed<TKey, TValue>(this IComponentContext context, string key)
        {
            return context.ResolveKeyed<IEnumerable<Meta<TValue>>>(key)
                .ToDictionary(action => (TKey)action.Metadata[key], action => action.Value)
                .AsReadOnly();
        }

        public static IRegistrationBuilder<IMessenger, TActivatorData, SingleRegistrationStyle> RegisterRecievers<TActivatorData>(
            this IRegistrationBuilder<IMessenger, TActivatorData, SingleRegistrationStyle> builder)
        {
            return builder.OnActivated(e =>
            {
                var recievers = e.Context.Resolve<ICanRecieveMessage[]>();
                recievers.ForEach(r => r.RegisterHanlders(e.Instance));
            });
        }

        public static void RegisterVisualizionContainer<T>(this ContainerBuilder builder, string colorKey)
            where T : IVisualizedVertices
        {
            builder.RegisterType<T>().As<IVisualizedVertices>().SingleInstance()
                .WithMetadata(RegistrationConstants.VisualizedTypeKey, colorKey);
        }

        public static IRegistrationBuilder<ITotalVisualization<Vertex>, TActivatorData, SingleRegistrationStyle> CommunicateContainers<TActivatorData>(
             this IRegistrationBuilder<ITotalVisualization<Vertex>, TActivatorData, SingleRegistrationStyle> builder)
        {
            return builder.OnActivated(e =>
            {
                var containers = e.Context.Resolve<IVisualizedVertices[]>();
                foreach (var container in containers)
                {
                    var except = containers.Except(container);
                    container.Containers.AddRange(except);
                }
            });
        }

        public static void RegisterMapper(this ContainerBuilder builder)
        {
            var profileTypes = typeof(Program).Assembly.GetTypes()
                .Where(t => typeof(Profile).IsAssignableFrom(t))
                .ToArray();

            builder.RegisterTypes(profileTypes).As<Profile>().SingleInstance();

            builder.Register(ctx =>
            {
                var profiles = ctx.Resolve<Profile[]>();
                var config = new MapperConfiguration(c => c.AddProfiles(profiles));
                return config.CreateMapper(ctx.Resolve);
            }).As<IMapper>().SingleInstance();
        }

        public static void ChangeParametres(this ResolveRequestContext context,
            params Parameter[] parameters)
        {
            context.ChangeParameters(parameters.AsEnumerable());
        }
    }
}