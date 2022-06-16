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
    internal abstract class DelegateExtractor<TResult, TDelegate, TAttribute> : IDelegateExtractor<TDelegate, TResult>
        where TDelegate : Delegate
        where TAttribute : MethodAttribute
    {
        private const BindingFlags MethodAccessModificators = NonPublic | Instance | Public;

        public abstract TResult Extract(MethodInfo info, object target);

        protected virtual IEnumerable<TDelegate> ExtractInternal(MethodInfo info, object target)
        {
            var type = target.GetType();
            return info
                .GetCustomAttributes<TAttribute>()
                .Select(attribute => GetMethod(type, attribute))
                .Select(method => CreateDelegateOrNull(target, method))
                .Where(method => method != null);
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
