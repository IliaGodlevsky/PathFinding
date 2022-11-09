using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Shared.Primitives.ValueRange;
using System;

namespace Pathfinding.App.Console.Views
{
    internal abstract class View : IView, IRequireIntInput, IDisposable
    {
        public event Action IterationStarted;

        private readonly IMenu menu;
        private readonly IDisplayable menuList;
        private readonly InclusiveValueRange<int> menuRange;

        public IInput<int> IntInput { get; set; }

        private string OptionsMsg => MessagesTexts.MenuOptionChoiceMsg;

        private int MenuItemIndex => IntInput.Input(OptionsMsg, menuRange) - 1;

        private IMenuCommand MenuCommand => menu.Commands[MenuItemIndex];

        private bool IsClosureRequested { get; set; }

        protected View(IViewModel model)
        {
            menu = new Menu(model);
            menuList = menu.Commands.CreateMenuList();
            menuRange = new InclusiveValueRange<int>(menu.Commands.Count, 1);
            model.ViewClosed += OnClosed;
        }

        public virtual void Display()
        {
            while (!IsClosureRequested)
            {
                IterationStarted?.Invoke();
                menuList.Display();
                MenuCommand.Execute();
            }
        }

        public void Dispose()
        {
            IterationStarted = null;
        }

        private void OnClosed()
        {
            IsClosureRequested = true;
        }
    }
}