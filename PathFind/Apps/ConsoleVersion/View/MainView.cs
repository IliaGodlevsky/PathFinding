using Common.Extensions;
using Common.ValueRanges;
using ConsoleVersion.View.Interface;
using ConsoleVersion.ViewModel;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using GraphLib.Realizations.Graphs;
using GraphViewModel.Interfaces;
using System;
using static ConsoleVersion.InputClass.Input;
using static ConsoleVersion.Resource.Resources;

using Console = Colorful.Console;


namespace ConsoleVersion.View
{
    internal sealed class MainView : IView
    {
        public const int HeightOfAbscissaView = 2;
        public const int HeightOfGraphParametresView = 1;

        public static int GetYCoordinatePadding()
        {
            return GetWidthOfOrdinateView() - 1;
        }

        public static int GetWidthOfOrdinateView()
        {
            return Constants.GraphLengthValueRange.UpperValueOfRange.GetFlooredLog10() + 2;
        }

        public static int GetLateralDistanceBetweenVertices()
        {
            int costWidth = BaseVertexCost.CostRange.UpperValueOfRange.GetFlooredLog10();
            int width = Constants.GraphWidthValueRange.UpperValueOfRange.GetFlooredLog10();
            return (costWidth > width ? costWidth : width) + 2;
        }

        public static Coordinate2D GraphFieldPosition { get; set; }

        public static Coordinate2D PathfindingStatisticsPosition { get; set; }

        public static void UpdatePositionOfVisualElements(IGraph graph)
        {
            if (graph.HasVertices() && graph is Graph2D graph2D)
            {
                int pathFindingStatisticsOffset = graph2D.Length + HeightOfAbscissaView * 2 + HeightOfGraphParametresView;
                PathfindingStatisticsPosition = new Coordinate2D(0, pathFindingStatisticsOffset);
            }
        }

        static MainView()
        {
            int x = GetWidthOfOrdinateView();
            int y = HeightOfAbscissaView + HeightOfGraphParametresView;
            GraphFieldPosition = new Coordinate2D(x, y);
        }

        public MainView(IMainModel model)
        {
            mainModel = model as MainViewModel ?? throw new ArgumentException(nameof(model));
            mainModel.OnInterrupted += OnProgrammStopped;
            menu = new Menu<Action>(model);
            menuList = new MenuList(menu.MenuActionsNames, columns: 2);
            menuValueRange = new InclusiveValueRange<int>(menu.MenuActionsNames.Length, 1);
        }

        public void Start()
        {
            while (!mainModel.IsAppClosureRequested)
            {
                mainModel.DisplayGraph();
                Console.WriteLine(menuList);
                int menuItemIndex = InputNumber(
                    OptionInputMsg,
                    menuValueRange) - 1;
                string menuItem = menu.MenuActionsNames[menuItemIndex];
                menu.MenuActions[menuItem].Invoke();
            }
        }

        private void OnProgrammStopped(object sender, EventArgs e)
        {
            Console.WriteLine("Good bye");
            Console.ReadLine();
        }

        private readonly Menu<Action> menu;
        private readonly MenuList menuList;
        private readonly MainViewModel mainModel;
        private readonly InclusiveValueRange<int> menuValueRange;
    }
}