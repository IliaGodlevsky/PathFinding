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
    internal abstract class CompanionMethods<TResult, TAttribute> : ICompanionMethods<TResult>
        where TResult : Delegate
        where TAttribute : MethodAttribute
    {
        private const BindingFlags MethodAccessModificators = NonPublic | Instance | Public;

        private readonly object target;
        private readonly Type type;

        protected CompanionMethods(object target)
        {
            this.target = target;
            type = target.GetType();
        }

        public abstract TResult GetMethods(MethodInfo targetMethod);

        protected IEnumerable<TResult> GetMethodsInternal(MethodInfo info)
        {
            return info
                .GetCustomAttributes<TAttribute>()
                .Select(attribute => GetMethod(type, attribute))
                .Where(method => method != null)
                .Select(method => CreateDelegateOrNull(target, method))
                .Where(method => method != null);
        }

        private TResult CreateDelegateOrNull(object target, MethodInfo info)
        {
            info.TryCreateDelegate(target, out TResult action);
            return action;
        }

        private MethodInfo GetMethod(Type type, TAttribute attribute)
        {
            return type.GetMethod(attribute.MethodName, MethodAccessModificators);
        }
    }
}
