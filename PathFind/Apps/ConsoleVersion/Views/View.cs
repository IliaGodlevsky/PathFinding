using Common.Interface;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Model;
using System;
using System.Linq;
using ValueRange;

namespace ConsoleVersion.Views
{
    internal abstract class View : IView, IRequireIntInput, IDisposable
    {
        public event Action NewMenuIteration;

        private readonly IMenu menu;
        private readonly IDisplayable menuList;
        private readonly InclusiveValueRange<int> menuRange;

        public IInput<int> IntInput { get; set; }

        private string[] MenuActionsNames { get; }

        private int MenuSize => MenuActionsNames.Length;

        private string OptionsMsg => MessagesTexts.MenuOptionChoiceMsg;

        private int MenuItemIndex => IntInput.Input(OptionsMsg, menuRange) - 1;

        private string MenuItem => MenuActionsNames[MenuItemIndex];

        private IMenuCommand MenuCommand => menu.MenuCommands[MenuItem];

        private bool IsClosureRequested { get; set; }

        protected View(IViewModel model)
        {
            menu = new Menu(model);
            MenuActionsNames = menu.MenuCommands.Keys.ToArray();
            menuList = menu.MenuCommands.Keys.ToMenuList();
            menuRange = new InclusiveValueRange<int>(MenuSize, 1);
            model.WindowClosed += OnClosed;
        }

        public virtual void Display()
        {
            while (!IsClosureRequested)
            {
                NewMenuIteration?.Invoke();
                menuList.Display();
                MenuCommand.Execute();
            }
        }

        public void Dispose()
        {
            NewMenuIteration = null;
        }

        private void OnClosed()
        {
            IsClosureRequested = true;
        }
    }
}