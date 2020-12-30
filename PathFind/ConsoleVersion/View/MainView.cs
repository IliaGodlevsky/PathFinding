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
            menuViewModel = new MenuViewModel<MainViewModel>(mainModel);
            menuActions = menuViewModel.GetMenuActions<Action>();
            menuItemsNames = menuActions.Keys.ToArray();
            menu = menuViewModel.CreateMenu(columns: 3);
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
            var option = Input.InputNumber(
                ConsoleVersionResources.OptionInputMsg,
                menuItemsNames.Length, 1) - 1;

            return menuItemsNames[option];
        }


        private readonly string[] menuItemsNames;
        private readonly Dictionary<string, Action> menuActions;
        private readonly MainViewModel mainModel;
        private readonly MenuViewModel<MainViewModel> menuViewModel;
        private readonly string menu;
    }
}
