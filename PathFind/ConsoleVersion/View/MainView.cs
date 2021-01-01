using ConsoleVersion.InputClass;
using ConsoleVersion.View.Interface;
using ConsoleVersion.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Console = Colorful.Console;

namespace ConsoleVersion.View
{
    internal class MainView : IView
    {
        public MainView()
        {
            mainModel = new MainViewModel();
            menuViewModel = new MenuViewModel(mainModel);
            menuActions = menuViewModel.GetMenuActions<Action>();
            menu = MenuViewModel.CreateMenu(menuActions.Keys);
        }

        public void Start()
        {
            var menuOption = GetMenuOption();
            while (true)
            {
                menuActions[menuOption]();
                menuOption = GetMenuOption();
            }
        }

        private string GetMenuOption()
        { 
            mainModel.DisplayGraph();
            Console.WriteLine(menu);
            int option = Input.InputNumber(
                ConsoleVersionResources.OptionInputMsg,
                menuActions.Keys.Count, 1) - 1;

            return menuActions.Keys.ElementAt(option);
        }

        private readonly Dictionary<string, Action> menuActions;
        private readonly MainViewModel mainModel;
        private readonly MenuViewModel menuViewModel;
        private readonly string menu;
    }
}
