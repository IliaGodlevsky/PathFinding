using Common.Extensions;
using Common.Extensions.EnumerableExtensions;
using Common.Interface;
using ConsoleVersion.Attributes;
using ConsoleVersion.Commands;
using ConsoleVersion.Interface;
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
        private static readonly Action<Action> DefaultSafeAction = action => action.Invoke();

        private readonly IViewModel target;
        private readonly Type targetType;

        private readonly Lazy<IReadOnlyDictionary<string, IMenuCommand>> menuActions;

        public IReadOnlyDictionary<string, IMenuCommand> MenuCommands => menuActions.Value;

        public Menu(IViewModel target)
        {
            this.target = target;
            targetType = target.GetType();
            menuActions = new Lazy<IReadOnlyDictionary<string, IMenuCommand>>(GetMenuCommands);
        }

        private IReadOnlyDictionary<string, IMenuCommand> GetMenuCommands()
        {
            return targetType
                .GetMethods(MethodAccessModificators)
                .Where(IsMenuItemMethod)
                .OrderByOrderAttribute()
                .SelectMany(CreateNameCommandPair)
                .ToReadOnlyDictionary();
        }

        private IEnumerable<KeyValuePair<string, IMenuCommand>> CreateNameCommandPair(MethodInfo methodInfo)
        {
            if (methodInfo.TryCreateDelegate(target, out Action action))
            {
                var safeAction = GetSafeMethodOrNull(methodInfo) ?? DefaultSafeAction;
                var validationMethods = GetValidationMethods(methodInfo);
                var command = new Action(() => safeAction.Invoke(action));
                string header = methodInfo.GetAttributeOrNull<MenuItemAttribute>().Header;
                var menuCommand = new MenuCommand(command, validationMethods);
                yield return new KeyValuePair<string, IMenuCommand>(header, menuCommand);
            }
        }

        private Action<Action> GetSafeMethodOrNull(MethodInfo method)
        {
            return CreateDelegates<Action<Action>, ExecuteSafeAttribute>(method).FirstOrDefault();
        }

        private Func<bool>[] GetValidationMethods(MethodInfo method)
        {
            return CreateDelegates<Func<bool>, PreValidationMethodAttribute>(method).ToArray();
        }

        private IEnumerable<TDelegate> CreateDelegates<TDelegate, TAttribute>(MethodInfo self)
            where TDelegate : Delegate
            where TAttribute : BaseMethodAttribute
        {
            return self
                .GetCustomAttributes<TAttribute>()
                .Select(GetMethod)
                .Select(CreateDelegateOrNull<TDelegate>);
        }

        private TDelegate CreateDelegateOrNull<TDelegate>(MethodInfo info)
            where TDelegate : Delegate
        {
            info.TryCreateDelegate(target, out TDelegate action);
            return action;
        }

        private MethodInfo GetMethod(BaseMethodAttribute attribute)
        {
            return targetType.GetMethod(attribute.MethodName, MethodAccessModificators);
        }

        private bool IsMenuItemMethod(MethodInfo method)
        {
            return Attribute.IsDefined(method, typeof(MenuItemAttribute));
        }
    }
}