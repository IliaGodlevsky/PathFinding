using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.App.Console.Model.Menu.Delegates;
using Shared.Extensions;
using Shared.Primitives.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using static System.Reflection.BindingFlags;

namespace Pathfinding.App.Console.Model.Menu
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
                var safeAction = targetMethod
                    .GetCustomAttributes<ExecuteSafeAttribute>()
                    .Select(GetMethod)
                    .Select(CreateDelegateOrNull<SafeAction>)
                    .FirstOrDefault();
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
            var method = GetMethod(attribute);
            var condition = CreateDelegateOrNull<Condition>(method);
            var messageAttribute = method.GetAttributeOrNull<FailMessageAttribute>();
            return (condition, messageAttribute?.Message ?? FailMessageAttribute.Default.Message);
        }

        private MethodInfo GetMethod(IMethodMark attribute)
        {
            return viewModelType.GetMethod(attribute.MethodName, MethodAccessModificators);
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