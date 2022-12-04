using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model.Menu;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.App.Console.Model.Menu.Exceptions;
using Pathfinding.Logging.Interface;
using Shared.Extensions;
using Shared.Primitives.ValueRange;

namespace Pathfinding.App.Console.Views
{
    internal sealed class View<TViewModel> : IRequireIntInput, IDisplayable
        where TViewModel : IViewModel
    {
        private readonly IMenuCommands menuCommands;
        private readonly IDisplayable menuList;
        private readonly InclusiveValueRange<int> menuRange;
        private readonly ILog log;

        public IInput<int> IntInput { get; set; }

        private string OptionsMsg => menuList + "\n" + MessagesTexts.MenuOptionChoiceMsg;

        private int MenuItemIndex => IntInput.Input(OptionsMsg, menuRange) - 1;

        private bool IsClosureRequested { get; set; }

        public View(TViewModel model, ILog log)
        {
            this.log = log;
            menuCommands = new MenuCommands(model);
            var columns = GetMenuColumnsNumber(model);
            menuList = menuCommands.Commands.CreateMenuList(columns);
            menuRange = new InclusiveValueRange<int>(menuCommands.Commands.Count, 1);
            model.ViewClosed += OnClosed;
        }

        public void Display()
        {
            IMenuCommand command;
            while (!IsClosureRequested)
            {
                Screen.SetCursorPositionUnderMenu(1);
                try
                {
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

        private void OnClosed()
        {
            IsClosureRequested = true;
        }
    }
}