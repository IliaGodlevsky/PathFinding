using Common.ValueRanges;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using GraphViewModel.Interfaces;
using Interruptable.EventArguments;
using System;

using static ConsoleVersion.Resource.Resources;

namespace ConsoleVersion.View.Abstraction
{
    internal abstract class View<TModel> : IView, IRequireInt32Input
        where TModel : IModel
    {
        public event Action NewMenuIteration;

        public void OnInterrupted(object sender, ProcessEventArgs e)
        {
            IsInterruptRequested = true;
        }

        private bool IsInterruptRequested { get; set; }

        protected TModel Model { get; }

        public IValueInput<int> Int32Input { get; set; }

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
                NewMenuIteration?.Invoke();
                menuList.Display();
                int menuItemIndex = Int32Input.InputValue(OptionInputMsg,
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