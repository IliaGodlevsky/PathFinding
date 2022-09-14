using ConsoleVersion.Attributes;
using ConsoleVersion.Delegates;
using System.Linq;
using System.Reflection;

namespace ConsoleVersion.Model.Methods
{
    internal sealed class SafeMethods : CompanionMethods<SafeAction, ExecuteSafeAttribute>
    {
        private static readonly SafeAction SafeAction;

        static SafeMethods()
        {
            SafeAction = new SafeAction(command => command.Invoke());
        }

        public SafeMethods(object target) : base(target)
        {

        }

        public override SafeAction GetMethods(MethodInfo info)
        {
            return GetMethodsInternal(info).SingleOrDefault() ?? SafeAction;
        }
    }
}