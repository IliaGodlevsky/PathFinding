using ConsoleVersion.InputClass;
using ConsoleVersion.View.Interface;
using ConsoleVersion.ViewModel;
using GraphLib.Coordinates;
using GraphLib.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using Console = Colorful.Console;

namespace ConsoleVersion.View
{
    internal class MainView : IView
    {
        public static Coordinate2D GraphFieldBodyConsoleStartCoordinate { get; set; }

        public static Coordinate2D PathfindingStatisticsConsoleStartCoordinate { get; set; }

        public static Coordinate2D MainMenuPosition { get; set; }

        public static void SetMenuPositions(Graph2D graph)
        {
            PathfindingStatisticsConsoleStartCoordinate = new Coordinate2D(0, graph.Length + 5);
            MainMenuPosition = new Coordinate2D(0, graph.Length + 6);
        }

        static MainView()
        {
            GraphFieldBodyConsoleStartCoordinate = new Coordinate2D(3, 3);
        }

        public MainView()
        {
            mainModel = new MainViewModel();
            menuActions = Menu.GetMenuMethodsAsDelegates<Action>(mainModel);
            menu = Menu.CreateMenu(menuActions.Keys, 3);
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
            if (MainMenuPosition != null)
            {
                var position = MainMenuPosition;
                Console.SetCursorPosition(position.X, position.Y);
            }
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
