using Common.Extensions;
using Common.Extensions.EnumerableExtensions;
using ConsoleVersion.Attributes;
using ConsoleVersion.Commands;
using ConsoleVersion.Enums;
using ConsoleVersion.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConsoleVersion.Model
{
    internal sealed class Menu
    {
        private readonly object target;
        private readonly Type targetType;

        private readonly Lazy<IReadOnlyDictionary<string, MenuCommand>> menuActions;
        private readonly Lazy<IReadOnlyList<string>> menuActionsNames;

        public IReadOnlyDictionary<string, MenuCommand> MenuActions => menuActions.Value;

        public IReadOnlyList<string> MenuActionsNames => menuActionsNames.Value;

        public Menu(object target)
        {
            this.target = target;
            targetType = target.GetType();
            menuActions = new Lazy<IReadOnlyDictionary<string, MenuCommand>>(GetMenuActions);
            menuActionsNames = new Lazy<IReadOnlyList<string>>(MenuActions.Keys.ToArray);
        }

        private IReadOnlyDictionary<string, MenuCommand> GetMenuActions()
        {
            return targetType
                .GetMethods()
                .Where(IsMenuAction)
                .OrderByDescending(GetMenuActionPriority)
                .SelectMany(CreateNameCommandPair)
                .ToDictionary();
        }

        private IEnumerable<KeyValuePair<string, MenuCommand>> CreateNameCommandPair(MethodInfo methodInfo)
        {
            if (methodInfo.TryCreateDelegate(target, out Action action))
            {
                var checkMethod = GetCheckMethod(methodInfo);
                var attribute = methodInfo.GetAttributeOrNull<MenuItemAttribute>();
                var menuCommand = new MenuCommand(action, checkMethod);
                yield return new KeyValuePair<string, MenuCommand>(attribute.Header, menuCommand);
            }
        }

        private Func<bool> GetCheckMethod(MethodInfo method)
        {
            var attribute = method.GetAttributeOrNull<ExecutionCheckMethodAttribute>();
            if (attribute != null)
            {
                var flags = BindingFlags.NonPublic | BindingFlags.Instance;
                var checkMethod = targetType.GetMethod(attribute.MethodName, flags);
                checkMethod.TryCreateDelegate(target, out Func<bool> predicate);
                return predicate;
            }
            return null;
        }

        private bool IsMenuAction(MethodInfo method)
        {
            return Attribute.IsDefined(method, typeof(MenuItemAttribute));
        }

        private MenuItemPriority GetMenuActionPriority(MethodInfo method)
        {
            return method.GetAttributeOrNull<MenuItemAttribute>().Priority;
        }
    }
}