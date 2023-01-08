using Autofac;
using Autofac.Builder;
using Autofac.Features.Metadata;
using Autofac.Features.Scanning;
using Shared.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Pathfinding.App.Console.DependencyInjection
{
    using Builder = IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle>;
    internal static class AutofacExtensions
    {
        public static IReadOnlyDictionary<TKey, TValue> ResolveWithMetadata<TKey, TValue>(this IComponentContext context, string key)
        {
            return context.Resolve<IEnumerable<Meta<TValue>>>()
                .ToDictionary(action => (TKey)action.Metadata[key], action => action.Value)
                .ToReadOnly();
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
    }
}
