using Common.Interface;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Model;
using ConsoleVersion.ValueInput;
using System;
using ValueRange;

namespace ConsoleVersion.Views
{
    internal abstract class View : IView, IRequireIntInput, IDisposable
    {
        public event Action NewMenuIteration;

        public void OnClosed()
        {
            IsInterruptRequested = true;
        }

        private bool IsInterruptRequested { get; set; }

        public ConsoleValueInput<int> IntInput { get; set; }

        protected View(IViewModel model)
        {
            menu = new Menu<Action>(model);
            menuList = new MenuList(menu.MenuActionsNames);
            menuValueRange = new InclusiveValueRange<int>(menu.MenuActionsNames.Length, 1);
            model.WindowClosed += OnClosed;
        }

        public virtual void Start()
        {
            while (!IsInterruptRequested)
            {
                NewMenuIteration?.Invoke();
                menuList.Display();
                int menuItemIndex = IntInput.InputValue(MessagesTexts.MenuOptionChoiceMsg, menuValueRange) - 1;
                string menuItem = menu.MenuActionsNames[menuItemIndex];
                menu.MenuActions[menuItem].Invoke();
            }
        }

        public void Dispose()
        {
            NewMenuIteration = null;
        }

        private readonly Menu<Action> menu;
        private readonly MenuList menuList;
        private readonly InclusiveValueRange<int> menuValueRange;
    }
}