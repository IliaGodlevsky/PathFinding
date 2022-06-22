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

        private readonly IMenu menu;
        private readonly IDisplayable menuList;
        private readonly InclusiveValueRange<int> menuRange;

        public IInput<int> IntInput { get; set; }

        private int MenuSize => menu.Commands.Count;

        private string OptionsMsg => MessagesTexts.MenuOptionChoiceMsg;

        private int MenuItemIndex => IntInput.Input(OptionsMsg, menuRange) - 1;

        private IMenuCommand MenuCommand => menu.Commands[MenuItemIndex];

        private bool IsClosureRequested { get; set; }

        protected View(IViewModel model)
        {
            menu = new Menu(model);
            menuList = menu.Commands.CreateMenuList();
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