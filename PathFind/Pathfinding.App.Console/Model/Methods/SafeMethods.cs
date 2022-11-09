using Pathfinding.App.Console.Attributes;
using Pathfinding.App.Console.Delegates;
using System.Linq;
using System.Reflection;

namespace Pathfinding.App.Console.Model.Methods
{
    internal sealed class SafeMethods : CompanionMethods<SafeAction, ExecuteSafeAttribute>
    {
        private static readonly SafeAction EmptySafeAction;

        static SafeMethods()
        {
            EmptySafeAction = new SafeAction(command => command.Invoke());
        }

        public SafeMethods(object target) : base(target)
        {

        }

        public override SafeAction GetMethods(MethodInfo info)
        {
            return GetMethodsInternal(info).SingleOrDefault() ?? EmptySafeAction;
        }
    }
}