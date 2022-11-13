using Autofac;
using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.MenuCommands;
using Pathfinding.App.Console.Model.MenuCommands.Attributes;
using Pathfinding.Logging.Interface;
using Shared.Extensions;
using Shared.Primitives.ValueRange;
using System;

namespace Pathfinding.App.Console.Views
{
    internal abstract class View : IView, IRequireIntInput, IDisposable
    {
        public event Action IterationStarted;

        private readonly IMenu menu;
        private readonly IDisplayable menuList;
        private readonly InclusiveValueRange<int> menuRange;
        private readonly ILog log;

        public IInput<int> IntInput { get; set; }

        private string OptionsMsg => MessagesTexts.MenuOptionChoiceMsg;

        private int MenuItemIndex => IntInput.Input(OptionsMsg, menuRange) - 1;

        private IMenuCommand MenuCommand => menu.Commands[MenuItemIndex];

        private bool IsClosureRequested { get; set; }

        protected View(IViewModel model, ILog log)
        {
            this.log = log;
            menu = new Menu(model);
            var columns = GetMenuColumnsNumber(model);
            menuList = menu.Commands.CreateMenuList(columns);
            menuRange = new InclusiveValueRange<int>(menu.Commands.Count, 1);
            model.ViewClosed += OnClosed;
        }

        public virtual void Display()
        {
            while (!IsClosureRequested)
            {
                IterationStarted?.Invoke();
                menuList.Display();
                ExecuteCommand(MenuCommand);
            }
        }

        private void ExecuteCommand(IMenuCommand command)
        {
            try
            {
                command.Execute();
            }
            catch (ConditionFailedException ex)
            {
                log.Warn(ex.Message);
            }
        }

        private int GetMenuColumnsNumber(IViewModel viewModel)
        {
            var attribute = viewModel.GetAttributeOrNull<MenuColumnsNumberAttribute>() ?? MenuColumnsNumberAttribute.Default;
            return attribute.MenuColumns;
        }

        public void Dispose()
        {
            IterationStarted = null;
        }

        private void OnClosed()
        {
            IsClosureRequested = true;
        }
    }
}