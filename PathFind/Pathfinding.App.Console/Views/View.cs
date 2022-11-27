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

        private string OptionsMsg => menuList + "\n" + MessagesTexts.MenuOptionChoiceMsg;

        private int MenuItemIndex => IntInput.Input(OptionsMsg, menuRange) - 1;

        private bool IsClosureRequested { get; set; }

        protected View(IViewModel model, ILog log)
        {
            this.log = log;
            menuCommands = new MenuCommands(model);
            var columns = GetMenuColumnsNumber(model);
            menuList = menuCommands.Commands.CreateMenuList(columns);
            menuRange = new InclusiveValueRange<int>(menuCommands.Commands.Count, 1);
            model.ViewClosed += OnClosed;
        }

        public virtual void Display()
        {
            IMenuCommand command;
            while (!IsClosureRequested)
            {
                Screen.SetCursorPositionUnderMenu(1);
                try
                {                   
                    NewMenuCycleStarted?.Invoke();
                    using (Cursor.CleanUpAfter())
                    {
                        command = menuCommands.Commands[MenuItemIndex];
                    }
                    command.Execute();
                }
                catch (ConditionFailedException ex)
                {
                    log.Warn(ex.Message);
                }
            }
        }

        private int GetMenuColumnsNumber(IViewModel viewModel)
        {
            return viewModel.GetAttributeOrDefault<MenuColumnsNumberAttribute>().MenuColumns;
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