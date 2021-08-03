using Common.ValueRanges;
using ConsoleVersion.View.Interface;
using GraphViewModel.Interfaces;
using Interruptable.EventArguments;
using System;
using static ConsoleVersion.InputClass.Input;
using static ConsoleVersion.Resource.Resources;

namespace ConsoleVersion.View.Abstraction
{
    internal abstract class View<TModel> : IView
        where TModel : IModel
    {
        public event Action OnNewMenuIteration;

        public void OnInterrupted(object sender, InterruptEventArgs e)
        {
            IsInterruptRequested = true;
        }

        private bool IsInterruptRequested { get; set; }

        protected TModel Model { get; }

        protected View(TModel model)
        {
            Model = model;
            menu = new Menu<Action>(Model);
            menuList = new MenuList(menu.MenuActionsNames);
            menuValueRange = new InclusiveValueRange<int>(menu.MenuActionsNames.Length, 1);
        }

        public virtual void Start()
        {
            while (!IsInterruptRequested)
            {
                OnNewMenuIteration?.Invoke();
                menuList.Display();
                int menuItemIndex = InputNumber(OptionInputMsg,
                    menuValueRange) - 1;
                string menuItem = menu.MenuActionsNames[menuItemIndex];
                menu.MenuActions[menuItem].Invoke();
            }
        }

        private readonly Menu<Action> menu;
        private readonly MenuList menuList;
        private readonly InclusiveValueRange<int> menuValueRange;
    }
}