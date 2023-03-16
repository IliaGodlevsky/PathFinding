﻿using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Features.Metadata;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
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

        public static IRegistrationBuilder<IMessenger, ConcreteReflectionActivatorData, SingleRegistrationStyle> RegisterRecievers(
            this IRegistrationBuilder<IMessenger, ConcreteReflectionActivatorData, SingleRegistrationStyle> builder)
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
    }
}