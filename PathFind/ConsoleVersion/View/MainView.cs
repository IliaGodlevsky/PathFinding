using ConsoleVersion.Enums;
using ConsoleVersion.Forms;
using ConsoleVersion.Graph;
using ConsoleVersion.GraphFactory;
using ConsoleVersion.InputClass;
using ConsoleVersion.ViewModel;
using GraphLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Console = Colorful.Console;

namespace ConsoleVersion.View
{
    public class MainView : IView
    {
        private delegate void MenuAction();
        private readonly Dictionary<MenuOption, MenuAction> menuActions;
        private readonly MainViewModel mainModel;
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
        }

        private void ShowMenu()
        {
            var stringBuilder = new StringBuilder();
            MenuOption menu = default;
            var descriptions = menu.GetDescriptions<MenuOption>();

            foreach (var item in descriptions)
            {
                int numberOf = descriptions.IndexOf(item);
                if (numberOf.IsEven())
                    stringBuilder.Append("\n");
                else
                    stringBuilder.Append("  \t");
                stringBuilder.Append(string.Format(Res.ShowFormat, numberOf, item));
            }

            Console.WriteLine(stringBuilder.ToString());
        }

        private MenuOption GetOption()
        {
            Console.Clear();
            GraphShower.ShowGraph(mainModel.Graph as ConsoleGraph);
            Console.WriteLine(mainModel?.Statistics);
            ShowMenu();
            return Input.InputOption();
        }

        public void Start()
        {
            var factory = new RandomValuedConsoleGraphFactory(
                percentOfObstacles: 25, width: 25, height: 25);
            mainModel.Graph = factory.GetGraph();
            var option = GetOption();
            while (option != Enum.GetValues(typeof(MenuOption)).Cast<MenuOption>().First())
            {
                menuActions[option].Invoke();
                option = GetOption();
            }
        }
    }
}
