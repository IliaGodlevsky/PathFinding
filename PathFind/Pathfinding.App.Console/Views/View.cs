using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model.Menu;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.App.Console.Model.Menu.Exceptions;
using Pathfinding.Logging.Interface;
using Shared.Extensions;
using Shared.Primitives.ValueRange;
using System;

namespace Pathfinding.App.Console.Views
{
    internal abstract class View : IView, IRequireIntInput, IDisplayable, IDisposable
    {
        public event Action NewMenuCycleStarted;

        private readonly IMenuCommands menuCommands;
        private readonly IDisplayable menuList;
        private readonly InclusiveValueRange<int> menuRange;
        private readonly ILog log;

        public IInput<int> IntInput { get; set; }

        private string OptionsMsg => MessagesTexts.MenuOptionChoiceMsg;

        private int MenuItemIndex => IntInput.Input(OptionsMsg, menuRange) - 1;

        private IMenuCommand MenuCommand => menuCommands.Commands[MenuItemIndex];

        private bool IsClosureRequested { get; set; }

        protected View(IViewModel model, ILog log)
        {
            this.log = log;
            menuCommands = new MenuCommands(model);
            var columns = GetMenuColumnsNumber(model);
            menuList = menuCommands.Commands.CreateMenuList(columns);
            menuRange = new InclusiveValueRange<int>(menuCommands.Commands.Count, 1);
            model.ViewClosed += OnClosed;
            NewMenuCycleStarted += menuList.Display;
        }

        public virtual void Display()
        {
            while (!IsClosureRequested)
            {
                try
                {
                    NewMenuCycleStarted?.Invoke();
                    MenuCommand.Execute();
                }
                catch (ConditionFailedException ex)
                {
                    log.Warn(ex.Message);
                }
            }
        }

        private int GetMenuColumnsNumber(IViewModel viewModel)
        {
            var attribute = viewModel.GetAttributeOrNull<MenuColumnsNumberAttribute>() ?? MenuColumnsNumberAttribute.Default;
            return attribute.MenuColumns;
        }

        public void Dispose()
        {
            NewMenuCycleStarted = null;
        }

        private void OnClosed()
        {
            IsClosureRequested = true;
        }
    }
}