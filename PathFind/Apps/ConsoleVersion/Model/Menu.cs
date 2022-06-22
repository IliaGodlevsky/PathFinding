using Common.Attrbiutes;
using Common.Extensions;
using ConsoleVersion.Attributes;
using ConsoleVersion.Commands;
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
        private readonly ICompanionMethods<Func<bool>> validationMethods;
        private readonly ICompanionMethods<Action<Action>> safeMethods;

        private readonly Lazy<IReadOnlyList<IMenuCommand>> menuActions;

        public IReadOnlyList<IMenuCommand> Commands => menuActions.Value;

        public Menu(object target)
        {
            safeMethods = new SafeMethods(target);
            validationMethods = new ValidationMethods(target);
            this.target = target;
            targetType = target.GetType();
            menuActions = new Lazy<IReadOnlyList<IMenuCommand>>(GetMenuCommands);
        }

        private IReadOnlyList<IMenuCommand> GetMenuCommands()
        {
            return targetType
                .GetMethods(MethodAccessModificators)
                .Where(IsMenuItem)
                .OrderBy(GetOrder)
                .SelectMany(CreateNameCommandPair)
                .ToArray();
        }

        private IEnumerable<IMenuCommand> CreateNameCommandPair(MethodInfo commandMethod)
        {
            if (commandMethod.TryCreateDelegate(target, out Action action))
            {
                var safeAction = safeMethods.GetMethods(commandMethod);
                var validation = validationMethods.GetMethods(commandMethod);
                Action command = () => safeAction.Invoke(action);
                string header = commandMethod.GetAttributeOrNull<MenuItemAttribute>().Header;
                yield return new MenuCommand(header, command, validation);
            }
        }

        private bool IsMenuItem(MethodInfo methodInfo)
        {
            return Attribute.IsDefined(methodInfo, typeof(MenuItemAttribute));
        }

        private static int GetOrder(MethodInfo methodInfo)
        {
            return methodInfo.GetAttributeOrNull<OrderAttribute>()?.Order ?? OrderAttribute.Default.Order;
        }
    }
}