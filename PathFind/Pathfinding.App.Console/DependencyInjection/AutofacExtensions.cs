using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Features.Metadata;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
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

        public static void RegisterVisual<TVisual>(this ContainerBuilder builder, VisualType type)
            where TVisual : IVisual
        {
            builder.RegisterType<TVisual>().As<IVisual>().As<ICanRecieveMessage>()
                .WithMetadata(RegistrationConstants.VisualTypeKey, type).SingleInstance();
        }
    }
}