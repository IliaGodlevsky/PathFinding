using Common.Interface;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Model;
using System;
using ValueRange;

namespace ConsoleVersion.Views
{
    internal abstract class View : IView, IRequireIntInput, IDisposable
    {
        public event Action NewMenuIteration;

        private readonly Menu<Action> menu;
        private readonly IDisplayable menuList;
        private readonly InclusiveValueRange<int> menuRange;

        private bool IsClosureRequested { get; set; }

        public IInput<int> IntInput { get; set; }

        private int MenuSize => menu.MenuActionsNames.Length;

        private string OptionsMsg => MessagesTexts.MenuOptionChoiceMsg;

        private int MenuItemIndex => IntInput.Input(OptionsMsg, menuRange) - 1;

        private string MenuItem => menu.MenuActionsNames[MenuItemIndex];

        protected View(IViewModel model)
        {
            menu = new Menu<Action>(model);
            menuList = new MenuList(menu.MenuActionsNames);
            menuRange = new InclusiveValueRange<int>(MenuSize, 1);
            model.WindowClosed += OnClosed;
        }

        public virtual void Display()
        {
            while (!IsClosureRequested)
            {
                NewMenuIteration?.Invoke();
                menuList.Display();
                menu.MenuActions[MenuItem].Invoke();
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