using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Core.Resolving.Pipeline;
using Autofac.Features.Metadata;
using AutoMapper;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DAL.Models.Mappers;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;
using Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DependencyInjection
{
    internal static class AutofacExtensions
    {
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
                var recievers = e.Context.Resolve<ICanReceiveMessage[]>();
                recievers.ForEach(r => r.RegisterHandlers(e.Instance));
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

        public static void ChangeParametres(this ResolveRequestContext context,
            params Parameter[] parameters)
        {
            context.ChangeParameters(parameters.AsEnumerable());
        }
    }
}