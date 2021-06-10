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
        public IDictionary<string, TAction> MenuActions => menuActions.Value;
        public string[] MenuActionsNames => menuActionsNames.Value;

        public Menu(object target)
        {
            this.target = target;
            menuActions = new Lazy<IDictionary<string, TAction>>(GetMenuActions);
            menuActionsNames = new Lazy<string[]>(MenuActions.Keys.ToArray);
        }

        private IDictionary<string, TAction> GetMenuActions()
        {
            return target
                .GetType()
                .GetMethods()
                .Where(IsMenuAction)
                .OrderByDescending(GetMenuActionPriority)
                .SelectMany(CreateActionPair)
                .AsDictionary();
        }

        private IEnumerable<KeyValuePair<string, TAction>> CreateActionPair(MethodInfo methodInfo)
        {
            if (methodInfo.TryCreateDelegate(target, out TAction action))
            {
                var attribute = methodInfo.GetAttributeOrNull<MenuItemAttribute>();               
                var header = attribute?.Header ?? methodInfo.Name;
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

        private readonly object target;

        private readonly Lazy<IDictionary<string, TAction>> menuActions;
        private readonly Lazy<string[]> menuActionsNames;
    }
}
