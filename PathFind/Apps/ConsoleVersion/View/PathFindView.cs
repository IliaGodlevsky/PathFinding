using Common.ValueRanges;
using ConsoleVersion.Resource;
using ConsoleVersion.View.Interface;
using ConsoleVersion.ViewModel;
using System;

using static ConsoleVersion.InputClass.Input;
using static ConsoleVersion.Resource.Resources;

namespace ConsoleVersion.View
{
    internal sealed class PathFindView : IView
    {
        public PathFindingViewModel Model { get; }

        public PathFindView(PathFindingViewModel model)
        {
            Model = model;
            string algorithmMenu = new MenuList(model.Algorithms.Keys).ToString();
            menu = new Menu<Action>(Model);
            menuList = new MenuList(menu.MenuActionsNames);
            Model.AlgorithmKeyInputMessage = algorithmMenu + Resources.ChooseAlrorithm;
            Model.SourceVertexInputMessage = "\n" + Resources.StartVertexPointInputMsg;
            Model.TargetVertexInputMessage = Resources.EndVertexCoordinateInputMsg;
            menuValueRange = new InclusiveValueRange<int>(menu.MenuActionsNames.Length, 1);
        }

        public void Start()
        {
            while (!Model.IsInterruptRequested)
            {
                Model.DisplayGraph();
                menuList.Display();
                Console.WriteLine();
                int menuItemIndex = InputNumber(OptionInputMsg,
                    menuValueRange) - 1;
                string menuItem = menu.MenuActionsNames[menuItemIndex];
                menu.MenuActions[menuItem].Invoke();
            }
        }

        private readonly Menu<Action> menu;
        private readonly MenuList menuList;
        private readonly InclusiveValueRange<int> menuValueRange;
    }
}
