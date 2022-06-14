using ConsoleVersion.Attributes;
using System;
using System.Linq;
using System.Reflection;

namespace ConsoleVersion.Model.DelegateExtractors
{
    internal sealed class SafeMethodExtractor : DelegateExtractor<Action<Action>, Action<Action>, ExecuteSafeAttribute>
    {
        public override Action<Action> Extract(MethodInfo info, object target)
        {
            return ExtractInternal(info, target).FirstOrDefault() ?? EmptySafeAction;
        }

        private static void EmptySafeAction(Action action)
        {
            action.Invoke();
        }
    }
}
