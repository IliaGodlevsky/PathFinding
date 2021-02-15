using ConsoleVersion.InputClass;
using ConsoleVersion.Resource;
using ConsoleVersion.View.Interface;
using ConsoleVersion.ViewModel;
using GraphLib.Interface;
using GraphLib.Realizations;
using GraphViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Console = Colorful.Console;

namespace ConsoleVersion.View
{
    internal class MainView : IView
    {
        public const int HeightOfAbscissaView = 2;
        public const int HeightOfGraphParametresView = 1;
        public const int WidthOfOrdinateView = 3;
        public const int YCoordinatePadding = 2;

        public static Coordinate2D GraphFieldPosition { get; set; }

        public static Coordinate2D PathfindingStatisticsPosition { get; set; }

        public static void UpdatePositionOfVisualElements(IGraph graph)
        {
            if (graph.Vertices.Any())
            {
                var graph2D = graph as Graph2D;

                int pathfindingStatistsicsOffset = graph2D.Length
                    + HeightOfAbscissaView * 2 + HeightOfGraphParametresView;

                PathfindingStatisticsPosition
                  = new Coordinate2D(0, pathfindingStatistsicsOffset);
            }
        }

        static MainView()
        {
            GraphFieldPosition
                = new Coordinate2D(WidthOfOrdinateView, HeightOfAbscissaView + HeightOfGraphParametresView);
        }

        public MainView(IMainModel model)
        {
            mainModel = model as MainViewModel;
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
            Console.WriteLine(menu);
            int option = Input.InputNumber(
                Resources.OptionInputMsg,
                menuActions.Keys.Count, 1) - 1;

            return menuActions.Keys.ElementAt(option);
        }

        private readonly Dictionary<string, Action> menuActions;
        private readonly MainViewModel mainModel;
        private readonly string menu;
    }
}
