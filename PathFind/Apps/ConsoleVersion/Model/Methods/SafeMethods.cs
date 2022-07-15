using ConsoleVersion.Attributes;
using ConsoleVersion.Delegates;
using System.Linq;
using System.Reflection;

namespace ConsoleVersion.Model.Methods
{
    internal sealed class SafeMethods : CompanionMethods<SafeAction, ExecuteSafeAttribute>
    {
        public SafeMethods(object target) : base(target)
        {
        }

        public override SafeAction GetMethods(MethodInfo info)
        {
            return GetMethodsInternal(info).FirstOrDefault() ?? EmptySafeAction;
        }

        private static void EmptySafeAction(Command action)
        {
            action.Invoke();
        }
    }
}
