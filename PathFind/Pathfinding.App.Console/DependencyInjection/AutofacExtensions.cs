﻿using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Core.Resolving.Pipeline;
using Autofac.Features.Metadata;
using AutoMapper;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DataAccess.Mappers;
using Pathfinding.App.Console.DataAccess.Services;
using Pathfinding.App.Console.DataAccess.UnitOfWorks;
using Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Service = Pathfinding.App.Console.DataAccess.Services.Service;

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

        private static IRegistrationBuilder<Service, TActivatorData, SingleRegistrationStyle> UseUnitOfWork<TActivatorData, T>(
            this IRegistrationBuilder<Service, TActivatorData, SingleRegistrationStyle> builder)
            where T : IUnitOfWork, new()
        {
            return builder.ConfigurePipeline(p => p.Use(new UnitOfWorkResolveMiddleware<T>()));
        }

        public static IRegistrationBuilder<Service, TActivatorData, SingleRegistrationStyle> Cache<TActivatorData>(
            this IRegistrationBuilder<Service, TActivatorData, SingleRegistrationStyle> builder)
        {
            return builder.OnActivating(args =>
            {
                var cache = new CacheService(args.Instance);
                args.ReplaceInstance(cache);
            });
        }

        public static IRegistrationBuilder<Service, TActivatorData, SingleRegistrationStyle> UseInMemory<TActivatorData>(
             this IRegistrationBuilder<Service, TActivatorData, SingleRegistrationStyle> builder)
        {
            return builder.UseUnitOfWork<TActivatorData, InMemoryUnitOfWork>();
        }

        public static IRegistrationBuilder<Service, TActivatorData, SingleRegistrationStyle> UseSqlite<TActivatorData>(
            this IRegistrationBuilder<Service, TActivatorData, SingleRegistrationStyle> builder)
        {
            return builder.UseUnitOfWork<TActivatorData, SqliteUnitOfWork>();
        }

        public static IRegistrationBuilder<Service, TActivatorData, SingleRegistrationStyle> UseLiteDb<TActivatorData>(
            this IRegistrationBuilder<Service, TActivatorData, SingleRegistrationStyle> builder)
        {
            return builder.UseUnitOfWork<TActivatorData, LiteDbUnitOfWork>();
        }

        public static void RegisterMapper(this ContainerBuilder builder)
        {
            builder.RegisterType<DbProfile>().As<Profile>().SingleInstance();
            builder.Register(ctx =>
            {
                var profile = ctx.Resolve<Profile>();
                var config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
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