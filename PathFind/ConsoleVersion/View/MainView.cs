using Common.Extensions;
using ConsoleVersion.Enums;
using ConsoleVersion.InputClass;
using ConsoleVersion.View.Interface;
using ConsoleVersion.ViewModel;
using GraphLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Console = Colorful.Console;

namespace ConsoleVersion.View
{
    internal class MainView : IView
    {
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
                { MenuOption.Reverse, mainModel.ChangeStatus },
                { MenuOption.ChangeValue, mainModel.ChangeVertexCost },
                { MenuOption.MakeWeighted, () => { mainModel.Graph.ToWeighted(); } },
                { MenuOption.MakeUnweigted, () => { mainModel.Graph.ToUnweighted(); } }
            };
            
            menu = GetMenu();
        }

        public void Start()
        {
            var menuOption = GetMenuOption();

            while (menuOption != MenuOption.Quit)
            {
                menuActions[menuOption]();
                menuOption = GetMenuOption();
            }
        }

        private MenuOption GetMenuOption()
        {
            mainModel.DisplayGraph();
            Console.WriteLine(menu);
            return Input.InputOption();
        }

        private string GetMenu()
        {
            var menu = new StringBuilder();

            var menuOptions = Enum.GetValues(typeof(MenuOption)).OfType<MenuOption>();
            foreach (var menuOption in menuOptions)
            {
                string viewElement = menuOption.GetValue().IsEven() ? newLine : largeSpace + tab;
                int enumValue = menuOption.GetValue();
                string enumDescription = menuOption.GetDescription();
                string format = ConsoleVersionResources.MenuFormat;
                string menuItem = string.Format(format, enumValue, enumDescription);
                menu.Append(viewElement).Append(menuItem);
            }

            return menu.ToString();
        }

        private const string newLine = "\n";
        private const string largeSpace = "   ";
        private const string tab = "\t";

        private readonly Dictionary<MenuOption, Action> menuActions;
        private readonly MainViewModel mainModel;
        private readonly string menu;
    }
}
