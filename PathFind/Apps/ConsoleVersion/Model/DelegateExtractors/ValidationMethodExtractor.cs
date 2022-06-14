using ConsoleVersion.Attributes;
using System;
using System.Linq;
using System.Reflection;

namespace ConsoleVersion.Model.DelegateExtractors
{
    internal sealed class ValidationMethodExtractor : DelegateExtractor<Func<bool>[], Func<bool>, ValidateAttribute>
    {
        public override Func<bool>[] Extract(MethodInfo info, object target)
        {
            return ExtractInternal(info, target).ToArray();
        }
    }
}
