using Common.ValueRanges;
using ConsoleVersion.EventArguments;
using ConsoleVersion.View.Interface;
using ConsoleVersion.ViewModel;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Realizations.Coordinates;
using GraphLib.Realizations.Graphs;
using GraphViewModel.Interfaces;
using Interruptable.EventArguments;
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

        public static int YCoordinatePadding => WidthOfOrdinateView - 1;

        public static int WidthOfOrdinateView => Constants.GraphLengthValueRange.UpperValueOfRange.ToString().Length + 1;

        public static int GetLateralDistanceBetweenVertices()
        {
            int currentCostWidth = currentMaxValueOfRange.ToString().Length;
            var previousCostWidth = previousMaxValueOfRange.ToString().Length;
            int costWidth = Math.Max(currentCostWidth, previousCostWidth);
            int width = Constants.GraphWidthValueRange.UpperValueOfRange.ToString().Length;
            return (costWidth >= width ? costWidth + 2 : width + width - costWidth);
        }

        private void OnProgrammStopped(object sender, InterruptEventArgs e)
        {
            var workTime = e.When - startTime;
            string format = @"dd\.hh\:mm\:ss";
            string time = workTime.ToString(format);
            string message = $"Work time: {time}\n";
            message += "Thank you and good bye";
            Console.WriteLine(message);
            Console.ReadLine();
        }

        private static void OnNewGraphCreated(object sender, NewGraphCreatedEventArgs e)
        {
            previousMaxValueOfRange = currentMaxValueOfRange;
            if (e.NewGraph.HasVertices() && e.NewGraph is Graph2D graph2D)
            {
                int pathFindingStatisticsOffset = graph2D.Length + HeightOfAbscissaView * 2 + HeightOfGraphParametresView;
                PathfindingStatisticsPosition = new Coordinate2D(0, pathFindingStatisticsOffset);
            }
        }

        private static void OnCostRangeChanged(object sender, CostRangeChangedEventArgs e)
        {
            int upperValueRange = e.NewValueRange.UpperValueOfRange;
            int lowerValueRange = e.NewValueRange.LowerValueOfRange;
            int max = Math.Max(upperValueRange, Math.Abs(lowerValueRange));
            previousMaxValueOfRange = Math.Max(currentMaxValueOfRange, previousMaxValueOfRange);
            currentMaxValueOfRange = max;
        }

        public static Coordinate2D GraphFieldPosition { get; set; }

        public static Coordinate2D PathfindingStatisticsPosition { get; set; }

        static MainView()
        {
            int x = WidthOfOrdinateView;
            int y = HeightOfAbscissaView + HeightOfGraphParametresView;
            GraphFieldPosition = new Coordinate2D(x, y);
        }

        public MainView(IMainModel model)
        {
            mainModel = model as MainViewModel ?? throw new ArgumentException(nameof(model));
            mainModel.OnInterrupted += OnProgrammStopped;
            mainModel.OnCostRangeChanged += OnCostRangeChanged;
            mainModel.OnNewGraphCreated += OnNewGraphCreated;
            menu = new Menu<Action>(mainModel);
            menuList = new MenuList(menu.MenuActionsNames, columns: 2);
            menuValueRange = new InclusiveValueRange<int>(menu.MenuActionsNames.Length, 1);
            var args = new CostRangeChangedEventArgs(BaseVertexCost.CostRange);
            OnCostRangeChanged(this, args);
            startTime = DateTime.Now;
        }

        public void Start()
        {
            while (!mainModel.IsInterruptRequested)
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

        private readonly Menu<Action> menu;
        private readonly MenuList menuList;
        private readonly MainViewModel mainModel;
        private readonly InclusiveValueRange<int> menuValueRange;

        private static int previousMaxValueOfRange;
        private static int currentMaxValueOfRange;
        private readonly DateTime startTime;
    }
}