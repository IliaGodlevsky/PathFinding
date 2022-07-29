using Common.Attrbiutes;
using Common.Extensions;
using Common.Extensions.EnumerableExtensions;
using ConsoleVersion.Attributes;
using ConsoleVersion.Commands;
using ConsoleVersion.Delegates;
using ConsoleVersion.Interface;
using ConsoleVersion.Model.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using static System.Reflection.BindingFlags;

namespace ConsoleVersion.Model
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
            var menuCommand = new MenuCommand(header, method);
            var condition = conditionMethods.GetMethods(methodInfo);
            return condition is null
                ? menuCommand
                : new ConditionedMenuCommand(menuCommand, condition);
        }

        private int GetOrder(MethodInfo method)
        {
            return method.GetAttributeOrNull<OrderAttribute>()?.Order 
                ?? OrderAttribute.Default.Order;
        }

        private bool IsMenuItem(MethodInfo methodInfo)
        {
            return Attribute.IsDefined(methodInfo, typeof(MenuItemAttribute));
        }
    }
}