using ConsoleVersion.InputClass;
using ConsoleVersion.View.Interface;
using ConsoleVersion.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Console = Colorful.Console;

namespace ConsoleVersion.View
{
    internal class MainView : IView
    {
        public MainView()
        {
            mainModel = new MainViewModel();
            menuActions = MenuViewModel.GetMenuMethodsAsDelegates<Action>(mainModel);
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
        private readonly string menu;
    }
}
