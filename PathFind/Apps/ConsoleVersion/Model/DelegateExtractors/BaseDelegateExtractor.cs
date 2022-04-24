using Common.Extensions;
using ConsoleVersion.Attributes;
using ConsoleVersion.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static System.Reflection.BindingFlags;

namespace ConsoleVersion.Model.DelegateExtractors
{
    internal abstract class BaseDelegateExtractor<TDelegate, TAttribute> : IDelegateExtractor<TDelegate>
        where TDelegate : Delegate
        where TAttribute : BaseMethodAttribute
    {
        private const BindingFlags MethodAccessModificators = NonPublic | Instance | Public;

        public IEnumerable<TDelegate> Extract(MethodInfo info, object target)
        {
            var type = target.GetType();
            return info
                .GetCustomAttributes<TAttribute>()
                .Select(attribute => GetMethod(type, attribute))
                .Select(method => CreateDelegateOrNull(target, method));
        }

        private TDelegate CreateDelegateOrNull(object target, MethodInfo info)
        {
            info.TryCreateDelegate(target, out TDelegate action);
            return action;
        }

        private MethodInfo GetMethod(Type type, TAttribute attribute)
        {
            return type.GetMethod(attribute.MethodName, MethodAccessModificators);
        }
    }
}
