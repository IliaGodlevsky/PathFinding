using Pathfinding.App.Console.Menu.Interface;
using Pathfinding.App.Console.Menu.Realizations.Attributes;
using Pathfinding.App.Console.Menu.Realizations.Delegates;
using Pathfinding.App.Console.ViewModel;
using Shared.Extensions;
using Shared.Primitives.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using static System.Reflection.BindingFlags;

namespace Pathfinding.App.Console.Menu.Realizations
{
    internal sealed class MenuCommands : IMenuCommands
    {
        private const BindingFlags MethodAccessModificators
            = FlattenHierarchy | NonPublic | Instance | Public;

        private readonly IViewModel viewModel;
        private readonly Type viewModelType;
        private readonly Lazy<IReadOnlyList<IMenuCommand>> commands;

        public IReadOnlyList<IMenuCommand> Commands => commands.Value;

        public MenuCommands(IViewModel viewModel)
        {
            this.viewModel = viewModel;
            viewModelType = viewModel.GetType();
            commands = new Lazy<IReadOnlyList<IMenuCommand>>(GetMenuCommands);
        }

        private IReadOnlyList<IMenuCommand> GetMenuCommands()
        {
            return viewModelType
                .GetMethods(MethodAccessModificators)
                .Where(IsMenuItem)
                .OrderByOrderAttribute()
                .SelectMany(CreateMenuCommand)
                .ToReadOnly();
        }

        private IEnumerable<IMenuCommand> CreateMenuCommand(MethodInfo targetMethod)
        {
            if (targetMethod.TryCreateDelegate(viewModel, out Command command))
            {
                string header = targetMethod.GetAttributeOrNull<MenuItemAttribute>().Header;
                var safeAttribute = targetMethod.GetAttributeOrNull<ExecuteSafeAttribute>();
                var safeMethodName = safeAttribute?.MethodName ?? string.Empty;
                var safeMethod = viewModelType.GetMethod(safeMethodName, MethodAccessModificators);
                var safeAction = CreateDelegateOrNull<SafeAction>(safeMethod);
                Command menuCommand = safeAction == null ? command : () => safeAction(command);
                var conditions = targetMethod
                    .GetCustomAttributes<ConditionAttribute>()
                    .OrderByOrderAttribute()
                    .Select(CreateConditionInfo)
                    .Select(GetConditionPair)
                    .ToReadOnly();
                yield return new MenuCommand(header, menuCommand, conditions);
            }
        }

        private (Condition Condition, string Message) CreateConditionInfo(ConditionAttribute attribute)
        {
            var method = viewModelType.GetMethod(attribute.MethodName, MethodAccessModificators);
            var condition = CreateDelegateOrNull<Condition>(method);
            var messageAttribute = method.GetAttributeOrNull<FailMessageAttribute>();
            return (condition, messageAttribute?.Message ?? "Message missing");
        }

        private ConditionPair GetConditionPair((Condition Condition, string Message) info)
        {
            return new ConditionPair(info.Condition, info.Message);
        }

        private static bool IsMenuItem(MethodInfo methodInfo)
        {
            return Attribute.IsDefined(methodInfo, typeof(MenuItemAttribute));
        }

        private TDelegate CreateDelegateOrNull<TDelegate>(MethodInfo info)
            where TDelegate : Delegate
        {
            info.TryCreateDelegate(viewModel, out TDelegate del);
            return del;
        }
    }
}