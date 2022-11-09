using Pathfinding.App.Console.Attributes;
using Pathfinding.App.Console.Commands;
using Pathfinding.App.Console.Delegates;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model.Methods;
using Shared.Extensions;
using Shared.Primitives.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using static System.Reflection.BindingFlags;

namespace Pathfinding.App.Console.Model
{
    internal sealed class Menu : IMenu
    {
        private const BindingFlags MethodAccessModificators = NonPublic | Instance | Public;

        private readonly object target;
        private readonly Type targetType;
        private readonly ICompanionMethods<Condition> conditionMethods;
        private readonly ICompanionMethods<SafeAction> safeMethods;

        private readonly Lazy<IReadOnlyList<IMenuCommand>> commands;

        public IReadOnlyList<IMenuCommand> Commands => commands.Value;

        public Menu(object target)
        {
            safeMethods = new SafeMethods(target);
            conditionMethods = new ConditionMethods(target);
            this.target = target;
            targetType = target.GetType();
            commands = new Lazy<IReadOnlyList<IMenuCommand>>(GetMenuCommands);
        }

        private IReadOnlyList<IMenuCommand> GetMenuCommands()
        {
            return targetType
                .GetMethods(MethodAccessModificators)
                .Where(IsMenuItem)
                .OrderBy(GetOrder)
                .Select(CreateCommandDelegate)
                .Where(del => del is not null)
                .Select(CreateMenuCommand)
                .ToReadOnly();
        }

        private Command CreateCommandDelegate(MethodInfo commandMethod)
        {
            return commandMethod.TryCreateDelegate(target, out Command action) ? action : null;
        }

        private IMenuCommand CreateMenuCommand(Command command)
        {
            var methodInfo = command.Method;
            var safeAction = safeMethods.GetMethods(methodInfo);
            Command method = () => safeAction.Invoke(command);
            string header = methodInfo.GetAttributeOrNull<MenuItemAttribute>().Header;
            var condition = conditionMethods.GetMethods(methodInfo);
            return CreateMenuCommand(condition, method, header);
        }

        private static IMenuCommand CreateMenuCommand(Condition condition, Command method, string header)
        {
            return condition is null
                ? new MenuCommand(header, method)
                : new ConditionedMenuCommand(header, method, condition);
        }

        private static int GetOrder(MethodInfo method)
        {
            return method.GetAttributeOrNull<OrderAttribute>()?.Order
                ?? OrderAttribute.Default.Order;
        }

        private static bool IsMenuItem(MethodInfo methodInfo)
        {
            return Attribute.IsDefined(methodInfo, typeof(MenuItemAttribute));
        }
    }
}