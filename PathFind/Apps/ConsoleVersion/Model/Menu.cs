using Common.Extensions;
using Common.Extensions.EnumerableExtensions;
using ConsoleVersion.Attributes;
using ConsoleVersion.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConsoleVersion.Model
{
    internal sealed class Menu<TAction> where TAction : Delegate
    {
        private readonly object target;

        private readonly Lazy<IReadOnlyDictionary<string, TAction>> menuActions;
        private readonly Lazy<IReadOnlyList<string>> menuActionsNames;

        public IReadOnlyDictionary<string, TAction> MenuActions => menuActions.Value;

        public IReadOnlyList<string> MenuActionsNames => menuActionsNames.Value;

        public Menu(object target)
        {
            this.target = target;
            menuActions = new Lazy<IReadOnlyDictionary<string, TAction>>(GetMenuActions);
            menuActionsNames = new Lazy<IReadOnlyList<string>>(MenuActions.Keys.ToArray);
        }

        private IReadOnlyDictionary<string, TAction> GetMenuActions()
        {
            return target
                .GetType()
                .GetMethods()
                .Where(IsMenuAction)
                .OrderByDescending(GetMenuActionPriority)
                .SelectMany(CreateNameActionPair)
                .ToDictionary();
        }

        private IEnumerable<KeyValuePair<string, TAction>> CreateNameActionPair(MethodInfo methodInfo)
        {
            if (methodInfo.TryCreateDelegate(target, out TAction action))
            {
                var attribute = methodInfo.GetAttributeOrNull<MenuItemAttribute>();
                yield return new KeyValuePair<string, TAction>(attribute.Header, action);
            }
        }

        private static bool IsMenuAction(MethodInfo method)
        {
            return Attribute.IsDefined(method, typeof(MenuItemAttribute));
        }

        private static MenuItemPriority GetMenuActionPriority(MethodInfo method)
        {
            return method.GetAttributeOrNull<MenuItemAttribute>().Priority;
        }
    }
}