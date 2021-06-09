using Common.Extensions;
using ConsoleVersion.Attributes;
using ConsoleVersion.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConsoleVersion.View
{    
    internal sealed class Menu<TAction> where TAction : Delegate
    {
        public Dictionary<string, TAction> MenuActions => menuActions.Value;
        public string[] MenuActionsNames => menuActionsNames.Value;

        public Menu(object target)
        {
            this.target = target;
            menuActions = new Lazy<Dictionary<string, TAction>>(GetMenuActions);
            menuActionsNames = new Lazy<string[]>(MenuActions.Keys.ToArray);
        }

        private Dictionary<string, TAction> GetMenuActions()
        {
            return target
                .GetType()
                .GetMethods()
                .Where(IsMenuAction)
                .OrderByDescending(GetMenuActionPriority)
                .SelectMany(CreateActionPair)
                .ToDictionary();
        }

        private IEnumerable<KeyValuePair<string, TAction>> CreateActionPair(MethodInfo methodInfo)
        {
            if (methodInfo.TryCreateDelegate(target, out TAction action))
            {
                var header = GetMenuItemHeader(methodInfo);
                yield return new KeyValuePair<string, TAction>(header, action);
            }
        }

        private bool IsMenuAction(MethodInfo method)
        {
            return Attribute.IsDefined(method, typeof(MenuItemAttribute));
        }

        private MenuItemPriority GetMenuActionPriority(MethodInfo method)
        {
            var attribute = method.GetAttributeOrNull<MenuItemAttribute>();
            return attribute?.Priority ?? default;
        }

        private string GetMenuItemHeader(MethodInfo method)
        {
            var attribute = method.GetAttributeOrNull<MenuItemAttribute>();
            return attribute?.Header ?? method.Name;
        }

        private readonly object target;

        private readonly Lazy<Dictionary<string, TAction>> menuActions;
        private readonly Lazy<string[]> menuActionsNames;
    }
}
