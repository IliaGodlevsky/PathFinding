using ConsoleVersion.Enums;
using ConsoleVersion.Forms;
using ConsoleVersion.Graph;
using ConsoleVersion.InputClass;
using ConsoleVersion.ViewModel;
using GraphLibrary.Extensions;
using System.Collections.Generic;
using System.Text;
using Console = Colorful.Console;

namespace ConsoleVersion.View
{
    public class MainView : IView
    {
        private delegate void MenuAction();
        private readonly Dictionary<MenuOption, MenuAction> menuActions;
        private readonly MainViewModel mainModel;
        private readonly string menu;
        public MainView()
        {
            mainModel = new MainViewModel();

            menuActions = new Dictionary<MenuOption, MenuAction>
            {
                { MenuOption.PathFind, mainModel.PathFind },
                { MenuOption.Save, mainModel.SaveGraph },
                { MenuOption.Load, mainModel.LoadGraph },
                { MenuOption.Create, mainModel.CreateNewGraph },
                { MenuOption.Refresh, mainModel.ClearGraph },
                { MenuOption.Reverse, mainModel.Reverse }
            };

            menu = GetMenu();
        }

        private string GetMenu()
        {
            const string newLine = "\n";
            const string bigSpace = "  ";
            const string tab = "\t";
            
            var stringBuilder = new StringBuilder();
            MenuOption menu = default;

            foreach (var item in menu.GetDescriptions<MenuOption>())
            {               
                int numberOf = menu.GetDescriptions<MenuOption>().IndexOf(item);
                stringBuilder.Append(numberOf.IsEven() ? newLine : bigSpace + tab);
                stringBuilder.Append(string.Format(Res.ShowFormat, numberOf, item));
            }

            return stringBuilder.ToString();
        }

        private MenuOption GetOption()
        {
            Console.WriteLine(menu);
            return Input.InputOption();
        }

        public void Start()
        {
            GraphShower.DisplayGraph(mainModel);
            var option = GetOption();
            while (option != MenuOption.Quit)
            {
                menuActions[option]();
                GraphShower.DisplayGraph(mainModel);
                option = GetOption();
            }
        }
    }
}
