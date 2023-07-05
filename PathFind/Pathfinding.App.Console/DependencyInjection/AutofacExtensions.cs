using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Core.Resolving.Pipeline;
using Autofac.Features.Metadata;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DependencyInjection.ConfigurationMiddlewears;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.MenuItems;
using Pathfinding.App.Console.Model.Visualizations;
using Shared.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DependencyInjection
{
    internal static class AutofacExtensions
    {
        public static void RegisterUnits<TExit>(this ContainerBuilder builder, params Type[] units)
            where TExit : ExitMenuItem
        {
            builder.RegisterTypes(units).WithMetadata(RegistrationConstants.UnitTypeKey, type => type)
                .AsSelf().AsImplementedInterfaces().AutoActivate().SingleInstance()
                .ConfigurePipeline(p => p.Use(new UnitResolveMiddleware(RegistrationConstants.UnitTypeKey)));
            builder.RegisterType<TExit>().Keyed(typeof(IMenuItem), units).SingleInstance();
        }

        public static void RegisterUnit<TUnit, TExit>(this ContainerBuilder builder) 
            where TUnit : IUnit
            where TExit : ExitMenuItem
        {
            builder.RegisterUnits<TExit>(typeof(TUnit));
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

        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> Keyed<TLimit, TActivatorData, TRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> self, Type serviceType, IEnumerable keys)
        {
            foreach (var key in keys)
            {
                self.Keyed(key, serviceType);
            }
            return self;
        }

        public static IRegistrationBuilder<IMessenger, TActivatorData, SingleRegistrationStyle> RegisterRecievers<TActivatorData>(
            this IRegistrationBuilder<IMessenger, TActivatorData, SingleRegistrationStyle> builder)
        {
            static void Register(IActivatedEventArgs<IMessenger> args)
            {
                var recievers = args.Context.Resolve<ICanRecieveMessage[]>();
                foreach (var reciever in recievers)
                {
                    reciever.RegisterHanlders(args.Instance);
                }
            }
            return builder.OnActivated(Register);
        }

        public static void RegisterVisualizedVertices<T>(this ContainerBuilder builder, VisualizedType type)
            where T : IVisualizedVertices
        {
            builder.RegisterType<T>().As<IVisualizedVertices>()
                .WithMetadata(RegistrationConstants.VisualizedTypeKey, type).SingleInstance();
        }

        public static void ChangeParametres(this ResolveRequestContext context,
            params Parameter[] parameters)
        {
            context.ChangeParameters(parameters.AsEnumerable());
        }
    }
}