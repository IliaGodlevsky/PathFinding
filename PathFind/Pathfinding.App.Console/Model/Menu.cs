using Pathfinding.App.Console.Attributes;
using Pathfinding.App.Console.Commands;
using Pathfinding.App.Console.Delegates;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model.Methods;
using Shared.Extensions;
using Shared.Primitives.Attributes;
using Shared.Primitives.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using static System.Reflection.BindingFlags;

namespace Pathfinding.App.Console.Model
{
    internal sealed class Menu : IMenu
    {
        private const BindingFlags MethodAccessModificators 
            = FlattenHierarchy | NonPublic | Instance | Public;

        private readonly IViewModel viewModel;
        private readonly Type viewModelType;
        private readonly ICompanionMethods<Condition> conditionMethods;
        private readonly ICompanionMethods<SafeAction> safeMethods;

        private readonly Lazy<IReadOnlyList<IMenuCommand>> commands;

        public IReadOnlyList<IMenuCommand> Commands => commands.Value;

        public Menu(IViewModel viewModel)
        {
            safeMethods = new SafeMethods(viewModel);
            conditionMethods = new ConditionMethods(viewModel);
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
                .Select(CreateCommandDelegate)
                .Where(del => del is not null)
                .Select(CreateMenuCommand)
                .ToReadOnly();
        }

        private Command CreateCommandDelegate(MethodInfo commandMethod)
        {
            return commandMethod.TryCreateDelegate(viewModel, out Command action) ? action : null;
        }

        private IMenuCommand CreateMenuCommand(Command command)
        {
            var methodInfo = command.Method;
            var safeAction = safeMethods.GetMethods(methodInfo);
            Command method = () => safeAction.Invoke(command);
            string header = methodInfo.GetAttributeOrNull<MenuItemAttribute>().Description;
            var condition = conditionMethods.GetMethods(methodInfo);
            return CreateMenuCommand(condition, method, header);
        }

        private static IMenuCommand CreateMenuCommand(Condition condition, Command method, string header)
        {
            return condition is null
                ? new MenuCommand(header, method)
                : new ConditionedMenuCommand(header, method, condition);
        }

        private static bool IsMenuItem(MethodInfo methodInfo)
        {
            return Attribute.IsDefined(methodInfo, typeof(MenuItemAttribute));
        }
    }
}