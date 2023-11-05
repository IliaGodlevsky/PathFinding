using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Core.Resolving.Pipeline;
using Autofac.Features.Metadata;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Visualizations;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DependencyInjection
{
    internal static class AutofacExtensions
    {
        public static void RegisterUnits<TExit>(this ContainerBuilder builder, IParametresFactory factory, params Type[] units)
            where TExit : IMenuItem
        {
            var resolveMiddleware = new UnitResolveMiddleware(RegistrationConstants.UnitTypeKey, factory, units);
            builder.RegisterTypes(units).WithMetadata(RegistrationConstants.UnitTypeKey, type => type).AsSelf()
                .AsImplementedInterfaces().AutoActivate().SingleInstance().ConfigurePipeline(p => p.Use(resolveMiddleware));
            foreach (var unit in units)
            {
                builder.RegisterType<TExit>().Keyed<IMenuItem>(unit).SingleInstance();
            }
        }

        public static void RegisterUnit<TUnit, TExit>(this ContainerBuilder builder, IParametresFactory factory)
            where TUnit : IUnit
            where TExit : IMenuItem
        {
            builder.RegisterUnits<TExit>(factory, typeof(TUnit));
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
            builder.RegisterType<T>().As<IVisualizedVertices>()
                .WithMetadata(RegistrationConstants.VisualizedTypeKey, colorKey).SingleInstance();
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

        public static void ChangeParametres(this ResolveRequestContext context,
            params Parameter[] parameters)
        {
            context.ChangeParameters(parameters.AsEnumerable());
        }
    }
}