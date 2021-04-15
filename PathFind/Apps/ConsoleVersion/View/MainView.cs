using Common;
using ConsoleVersion.View.Interface;
using ConsoleVersion.ViewModel;
using GraphLib.Interfaces;
using GraphLib.Realizations;
using GraphViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using static ConsoleVersion.InputClass.Input;
using static ConsoleVersion.Resource.Resources;
using static ConsoleVersion.ViewModel.Menu;
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
            if (graph.Vertices.Any() && graph is Graph2D graph2D)
            {
                int pathFindingStatisticsOffset = graph2D.Length + HeightOfAbscissaView * 2 + HeightOfGraphParametresView;
                PathfindingStatisticsPosition = new Coordinate2D(0, pathFindingStatisticsOffset);
            }
        }

        static MainView()
        {
            int x = WidthOfOrdinateView;
            int y = HeightOfAbscissaView + HeightOfGraphParametresView;
            GraphFieldPosition = new Coordinate2D(x, y);
        }

        public MainView(IMainModel model)
        {
            mainModel = model as MainViewModel;
            menuActions = GetMenuMethodsAsDelegates<Action>(mainModel);
            menuActionsKeys = menuActions.Keys.ToArray();
            menu = CreateMenu(menuActionsKeys, columns: 3);
            menuValueRange = new ValueRange(menuActionsKeys.Length, 1);
        }

        public void Start()
        {
            while (true) menuActions[GetMenuOption()]();
        }

        private string GetMenuOption()
        {
            mainModel.DisplayGraph();
            Console.WriteLine(menu);
            int option = InputNumber(OptionInputMsg,
                menuValueRange) - 1;

            return menuActionsKeys[option];
        }

        private readonly Dictionary<string, Action> menuActions;
        private readonly MainViewModel mainModel;
        private readonly string menu;
        private readonly ValueRange menuValueRange;
        private readonly string[] menuActionsKeys;
    }
}
