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

        private bool IsClosureRequested { get; set; }
        public IValueInput<int> IntInput { get; set; }

        private string OptionsMsg => MessagesTexts.MenuOptionChoiceMsg;
        private int MenuItemIndex => IntInput.InputValue(OptionsMsg, menuValueRange) - 1;
        private string MenuItem => menu.MenuActionsNames[MenuItemIndex];

        protected View(IViewModel model)
        {
            menu = new Menu<Action>(model);
            menuList = new MenuList(menu.MenuActionsNames);
            menuValueRange = new InclusiveValueRange<int>(menu.MenuActionsNames.Length, 1);
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

        public void OnClosed()
        {
            IsClosureRequested = true;
        }

        public void Dispose()
        {
            NewMenuIteration = null;
        }

        private readonly Menu<Action> menu;
        private readonly IDisplayable menuList;
        private readonly InclusiveValueRange<int> menuValueRange;
    }
}