using ConsoleVersion.Attributes;
using System;
using System.Linq;
using System.Reflection;

namespace ConsoleVersion.Model.DelegateExtractors
{
    internal sealed class SafeMethods : CompanionMethods<Action<Action>, Action<Action>, ExecuteSafeAttribute>
    {
        public SafeMethods(object target) : base(target)
        {
        }

        public override Action<Action> GetMethods(MethodInfo info)
        {
            return ExtractInternal(info).FirstOrDefault() ?? EmptySafeAction;
        }

        private static void EmptySafeAction(Action action)
        {
            action.Invoke();
        }
    }
}
