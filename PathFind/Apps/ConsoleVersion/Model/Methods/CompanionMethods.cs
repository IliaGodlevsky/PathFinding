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
            return info
                .GetCustomAttributes<TAttribute>()
                .Select(attr => type.GetMethod(attr.MethodName, MethodAccessModificators))
                .Select(CreateDelegateOrNull);
        }

        private TResult CreateDelegateOrNull(MethodInfo method)
        {
            method.TryCreateDelegate(target, out TResult action);
            return action;
        }
    }
}
