using ConsoleVersion.Enums;
using ConsoleVersion.Forms;
using ConsoleVersion.InputClass;
using ConsoleVersion.ViewModel;
using GraphLibrary.Extensions;
using System.Collections.Generic;
using System.Text;
using Console = Colorful.Console;

namespace ConsoleVersion.View
{
    internal class MainView : IView
    {
        private const string newLine = "\n";
        private const string largeSpace = "   ";
        private const string tab = "\t";

        private delegate void MenuAction();
        private readonly Dictionary<MenuOption, MenuAction> menuActions;
        private readonly MainViewModel mainModel;
        private readonly string menu;

        public MainView()
        {
            mainModel = new MainViewModel();

            menuActions = new Dictionary<MenuOption, MenuAction>()
            {
                { MenuOption.PathFind, mainModel.PathFind },
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
            var descriptions = ((MenuOption)default).GetDescriptions<MenuOption>();
            foreach (var item in descriptions)
            {               
                int numberOf = descriptions.IndexOf(item);
                menu.Append(numberOf.IsEven() ? newLine : largeSpace + tab);
                menu.Append(string.Format(Res.ShowFormat, numberOf, item));
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
