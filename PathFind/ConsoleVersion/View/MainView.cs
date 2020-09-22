using ConsoleVersion.Enums;
using ConsoleVersion.Forms;
using ConsoleVersion.InputClass;
using ConsoleVersion.View.Interface;
using ConsoleVersion.ViewModel;
using GraphLibrary.Extensions.SystemTypeExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Console = Colorful.Console;

namespace ConsoleVersion.View
{
    internal class MainView : IView
    {
        private const string newLine = "\n";
        private const string largeSpace = "   ";
        private const string tab = "\t";

        private readonly Dictionary<MenuOption, Action> menuActions;
        private readonly MainViewModel mainModel;
        private readonly string menu;

        public MainView()
        {
            mainModel = new MainViewModel();

            menuActions = new Dictionary<MenuOption, Action>()
            {
                { MenuOption.PathFind, mainModel.FindPath },
                { MenuOption.SaveGraph, mainModel.SaveGraph },
                { MenuOption.LoadGraph, mainModel.LoadGraph },
                { MenuOption.CreateGraph, mainModel.CreateNewGraph },
                { MenuOption.RefreshGraph, mainModel.ClearGraph },
                { MenuOption.Reverse, mainModel.Reverse },
                { MenuOption.ChangeValue, mainModel.ChangeVertexValue }
            };
            
            menu = GetMenu();
        }

        private string GetMenu()
        {           
            var menu = new StringBuilder();
            var enums = Enum.GetValues(typeof(MenuOption)).Cast<MenuOption>().ToList();
            for (int i = 0; i < enums.Count; i++)
            {
                menu.Append(i.IsEven() ? newLine : largeSpace + tab);
                int menuItem = i + 1;
                menu.AppendFormat(ConsoleVersionResources.MenuFormat, menuItem, enums[i].GetDescription());
            }
            return menu.ToString();
        }

        private MenuOption GetMenuOption()
        {
            Console.WriteLine(menu);
            return Input.InputOption();
        }

        public void Start()
        {
            GraphShower.DisplayGraph(mainModel);
            var menuOption = GetMenuOption();
            while (menuOption != MenuOption.Quit)
            {
                menuActions[menuOption]();
                GraphShower.DisplayGraph(mainModel);
                menuOption = GetMenuOption();
            }
        }
    }
}
