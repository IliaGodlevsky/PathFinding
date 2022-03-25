using Common.Extensions;
using Common.Extensions.EnumerableExtensions;
using Common.Interface;
using ConsoleVersion.Attributes;
using ConsoleVersion.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

using static System.Reflection.BindingFlags;

namespace ConsoleVersion.Model
{
    internal sealed class Menu
    {
        private const BindingFlags MethodAccessModificators = NonPublic | Instance | Public;

        private readonly object target;
        private readonly Type targetType;

        private readonly Lazy<IReadOnlyDictionary<string, MenuCommand>> menuActions;
        private readonly Lazy<IReadOnlyList<string>> menuActionsNames;

        public IReadOnlyDictionary<string, MenuCommand> MenuActions => menuActions.Value;

        public IReadOnlyList<string> MenuActionsNames => menuActionsNames.Value;

        public Menu(IViewModel target)
        {
            this.target = target;
            targetType = target.GetType();
            menuActions = new Lazy<IReadOnlyDictionary<string, MenuCommand>>(GetMenuActions);
            menuActionsNames = new Lazy<IReadOnlyList<string>>(MenuActions.Keys.ToArray);
        }

        private IReadOnlyDictionary<string, MenuCommand> GetMenuActions()
        {
            return targetType
                .GetMethods(MethodAccessModificators)
                .Where(IsMenuItemMethod)
                .OrderBy(GetMenuItemOrder)
                .SelectMany(CreateNameCommandPair)
                .ToDictionary();
        }

        private IEnumerable<KeyValuePair<string, MenuCommand>> CreateNameCommandPair(MethodInfo methodInfo)
        {
            if (methodInfo.TryCreateDelegate(target, out Action action))
            {
                var command = action;
                TryCreateSafeAction(methodInfo, action, out command);
                var validationMethods = GetValidationMethods(methodInfo);
                var description = methodInfo.GetAttributeOrNull<DescriptionAttribute>();
                string name = description?.Description ?? string.Empty;
                var menuCommand = new MenuCommand(command, validationMethods);
                yield return new KeyValuePair<string, MenuCommand>(name, menuCommand);
            }
        }

        private bool TryCreateSafeAction(MethodInfo info, Action toWrap, out Action safeAction)
        {
            safeAction = toWrap;
            var tryCatchWrapName = info.GetAttributeOrNull<ExecuteSafeAttribute>();
            if (tryCatchWrapName != null)
            {
                var method = targetType.GetMethod(tryCatchWrapName.MethodName, MethodAccessModificators);
                if (method.TryCreateDelegate(target, out Action<Action> wrap))
                {
                    safeAction = new Action(() => wrap.Invoke(toWrap));
                    return true;
                }
            }
            return false;
        }

        private Func<bool>[] GetValidationMethods(MethodInfo method)
        {
            return method
                .GetCustomAttributes<PreValidationMethodAttribute>()
                .Select(attribute => attribute.MethodName)
                .Select(GetMethod)
                .Select(CreatePredicate)
                .ToArray();
        }

        private Func<bool> CreatePredicate(MethodInfo info)
        {
            info.TryCreateDelegate(target, out Func<bool> predicate);
            return predicate;
        }

        private MethodInfo GetMethod(string methodName)
        {
            return targetType.GetMethod(methodName, MethodAccessModificators);
        }

        private bool IsMenuItemMethod(MethodInfo method)
        {
            return Attribute.IsDefined(method, typeof(MenuItemAttribute));
        }

        private int GetMenuItemOrder(MethodInfo method)
        {
            return method.GetAttributeOrNull<MenuItemAttribute>().Order;
        }
    }
}