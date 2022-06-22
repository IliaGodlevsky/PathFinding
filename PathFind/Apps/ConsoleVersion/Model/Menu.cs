using Common.Attrbiutes;
using Common.Extensions;
using Common.Extensions.EnumerableExtensions;
using Common.Interface;
using ConsoleVersion.Attributes;
using ConsoleVersion.Commands;
using ConsoleVersion.Interface;
using ConsoleVersion.Model.DelegateExtractors;
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
        private readonly ICompanionMethods<Func<bool>[]> validationMethods;
        private readonly ICompanionMethods<Action<Action>> safeMethods;

        private readonly Lazy<IReadOnlyDictionary<string, IMenuCommand>> menuActions;

        public IReadOnlyDictionary<string, IMenuCommand> MenuCommands => menuActions.Value;

        public Menu(object target)
        {
            safeMethods = new SafeMethods(target);
            validationMethods = new ValidationMethods(target);
            this.target = target;
            targetType = target.GetType();
            menuActions = new Lazy<IReadOnlyDictionary<string, IMenuCommand>>(GetMenuCommands);
        }

        private IReadOnlyDictionary<string, IMenuCommand> GetMenuCommands()
        {
            return targetType
                .GetMethods(MethodAccessModificators)
                .Where(method => Attribute.IsDefined(method, typeof(MenuItemAttribute)))
                .OrderBy(item => item.GetAttributeOrNull<OrderAttribute>()?.Order ?? OrderAttribute.Default.Order)
                .SelectMany(CreateNameCommandPair)
                .ToReadOnlyDictionary();
        }

        private IEnumerable<KeyValuePair<string, IMenuCommand>> CreateNameCommandPair(MethodInfo methodInfo)
        {
            if (methodInfo.TryCreateDelegate(target, out Action action))
            {
                var safeAction = safeMethods.GetMethods(methodInfo);
                var validationMethods = this.validationMethods.GetMethods(methodInfo);
                var command = new Action(() => safeAction.Invoke(action));
                string header = methodInfo.GetAttributeOrNull<MenuItemAttribute>().Header;
                var menuCommand = new MenuCommand(command, validationMethods);
                yield return new KeyValuePair<string, IMenuCommand>(header, menuCommand);
            }
        }
    }
}