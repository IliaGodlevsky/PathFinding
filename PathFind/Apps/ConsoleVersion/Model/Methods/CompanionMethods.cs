using Common.Extensions;
using ConsoleVersion.Attributes;
using ConsoleVersion.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static System.Reflection.BindingFlags;

namespace ConsoleVersion.Model.Methods
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
            return GetCustomAttributes(info)
                .Select(GetMethod)
                .Select(CreateDelegateOrNull);
        }

        protected virtual IEnumerable<TAttribute> GetCustomAttributes(MethodInfo info)
        {
            return info.GetCustomAttributes<TAttribute>();
        }

        private TResult CreateDelegateOrNull(MethodInfo info)
        {
            info.TryCreateDelegate(target, out TResult action);
            return action;
        }

        private MethodInfo GetMethod(TAttribute attribute)
        {
            return type.GetMethod(attribute.MethodName, MethodAccessModificators);
        }
    }
}
