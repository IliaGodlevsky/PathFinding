using ConsoleVersion.View.Interface;
using ConsoleVersion.ViewModel;
using System;
using System.Collections.Generic;
using Console = Colorful.Console;

namespace ConsoleVersion.View
{
    internal class MainView : IView
    {
        public MainView()
        {
            mainModel = new MainViewModel();
            menuActions = mainModel.GetMenuActions();
            menu = mainModel.CreateMenu();
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
            return mainModel.GetMethodDescription();
        }

        private readonly Dictionary<string, Action> menuActions;
        private readonly MainViewModel mainModel;
        private readonly string menu;
    }
}
