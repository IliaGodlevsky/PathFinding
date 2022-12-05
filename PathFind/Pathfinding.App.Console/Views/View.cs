using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model.Menu;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.App.Console.Model.Menu.Delegates;
using Pathfinding.App.Console.Model.Menu.Exceptions;
using Pathfinding.Logging.Interface;
using Shared.Extensions;
using Shared.Primitives.ValueRange;

namespace Pathfinding.App.Console.Views
{
    internal sealed class View: IRequireIntInput, IDisplayable
    {
        private readonly IMenuCommands menuCommands;
        private readonly IDisplayable menuList;
        private readonly InclusiveValueRange<int> menuRange;
        private readonly ILog log;

        public IInput<int> IntInput { get; set; }

        private string OptionsMsg => menuList + "\n" + MessagesTexts.MenuOptionChoiceMsg;

        private int MenuItemIndex => IntInput.Input(OptionsMsg, menuRange) - 1;

        private bool IsClosureRequested { get; set; }

        public View(IViewModel model, ILog log)
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
            while (!IsClosureRequested)
            {
                Screen.SetCursorPositionUnderMenu(1);
                try
                {
                    var command = menuCommands.Commands[InputItemIndex()];
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

        private int InputItemIndex()
        {
            using (Cursor.CleanUpAfter())
            {
                return MenuItemIndex;
            }
        }

        private void OnClosed()
        {
            IsClosureRequested = true;
        }
    }
}