using Common.Extensions.EnumerableExtensions;
using ConsoleVersion.Attributes;
using System;
using System.Reflection;

namespace ConsoleVersion.Model.DelegateExtractors
{
    internal sealed class ValidationMethods : CompanionMethods<Func<bool>, ValidateAttribute>
    {
        public ValidationMethods(object target) : base(target)
        {
        }

        public override Func<bool> GetMethods(MethodInfo info)
        {
            return GetMethodsInternal(info).AggregateOrDefault((x, y) => (Func<bool>)Delegate.Combine(x, y));
        }
    }
}
