using ConsoleVersion.Attributes;
using System;
using System.Linq;
using System.Reflection;

namespace ConsoleVersion.Model.DelegateExtractors
{
    internal sealed class ValidationMethods : CompanionMethods<Func<bool>[], Func<bool>, ValidateAttribute>
    {
        public ValidationMethods(object target) : base(target)
        {
        }

        public override Func<bool>[] GetMethods(MethodInfo info)
        {
            return ExtractInternal(info).ToArray();
        }
    }
}
