using Common.ValueRanges;
using ConsoleVersion.View.Interface;
using GraphViewModel.Interfaces;
using Interruptable.Interface;
using System;
using static ConsoleVersion.InputClass.Input;
using static ConsoleVersion.Resource.Resources;

namespace ConsoleVersion.View.Abstraction
{
    internal abstract class View<TModel> : IView
        where TModel : IModel, IInterruptable
    {
        public event Action OnNewMenuIteration;

        protected TModel Model { get; }

        protected View(TModel model)
        {
            Model = model;
            menu = new Menu<Action>(Model);
            menuList = new MenuList(menu.MenuActionsNames);
            menuValueRange = new InclusiveValueRange<int>(menu.MenuActionsNames.Length, 1);
        }

        public void Start()
        {
            while (!Model.IsInterruptRequested)
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