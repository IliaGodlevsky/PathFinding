using Common.Extensions;
using ConsoleVersion.Attributes;
using ConsoleVersion.Enums;
using ConsoleVersion.InputClass;
using ConsoleVersion.Model;
using ConsoleVersion.View;
using GraphLib.Extensions;
using GraphLib.Graphs;
using GraphViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using Console = Colorful.Console;

namespace ConsoleVersion.ViewModel
{
    internal class MainViewModel : MainModel
    {
        private string[] MethodsDescriptions { get; set; }

        public MainViewModel() : base()
        {
            VertexEventHolder = new ConsoleVertexEventHolder();
            FieldFactory = new ConsoleGraphFieldFactory();
            InfoConverter = (info) => new ConsoleVertex(info);
        }

        [Menu("Make unweighted")]
        public void MakeGraphUnweighted()
        {
            Graph.ToUnweighted();
        }

        [Menu("Make weighted")]
        public void MakeGraphWeighted()
        {
            Graph.ToWeighted();
        }

        [Menu("Create new graph", MenuItemPriority.First)]
        public override void CreateNewGraph()
        {
            var model = new GraphCreatingViewModel(this);
            var view = new GraphCreateView(model);

            view.Start();
        }

        [Menu("Find path", MenuItemPriority.High)]
        public override void FindPath()
        {
            var model = new PathFindingViewModel(this);
            model.OnPathNotFound += OnPathNotFound;
            var view = new PathFindView(model);

            view.Start();
        }

        [Menu("Reverse vertes")]
        public void ReverseVertex()
        {
            var upperPossibleXValue = (Graph as Graph2D).Width - 1;
            var upperPossibleYValue = (Graph as Graph2D).Length - 1;

            var point = Input.InputPoint(upperPossibleXValue, upperPossibleYValue);

            (Graph[point] as ConsoleVertex).Reverse();
        }

        [Menu("Change vertex cost", MenuItemPriority.Low)]
        public void ChangeVertexCost()
        {
            if (!Graph.IsDefault)
            {
                var graph2D = Graph as Graph2D;

                var upperPossibleXValue = graph2D.Width - 1;
                var upperPossibleYValue = graph2D.Length - 1;

                var point = Input.InputPoint(upperPossibleXValue, upperPossibleYValue);

                while (Graph[point].IsObstacle)
                {
                    point = Input.InputPoint(upperPossibleXValue, upperPossibleYValue);
                }

                (Graph[point] as ConsoleVertex).ChangeCost();
            }
        }

        [Menu("Clear graph", MenuItemPriority.High)]
        public override void ClearGraph()
        {
            base.ClearGraph();
        }

        [Menu("Save graph")]
        public override void SaveGraph()
        {
            base.SaveGraph();
        }

        [Menu("Load graph")]
        public override void LoadGraph()
        {
            base.LoadGraph();
        }

        [Menu("Quit programm", MenuItemPriority.Last)]
        public void Quit()
        {
            Environment.Exit(0);
        }

        public void DisplayGraph()
        {
            Console.Clear();
            Console.ForegroundColor = Color.White;
            Console.WriteLine(GraphParametres);
            var field = GraphField as ConsoleGraphField;
            field?.ShowGraphWithFrames();
            Console.WriteLine(PathFindingStatistics);
        }

        public string CreateMenu()
        {
            var methods = GetMenuMethods().ToArray();

            var menu = new StringBuilder("\n");

            for (int i = 0; i < methods.Length; i++)
            {
                var attribute = methods[i].GetAttribute<MenuAttribute>();
                var description = attribute.Description;
                var menuItem = string.Format(ConsoleVersionResources.MenuFormat, i + 1, description);
                menu.AppendLine(menuItem);
            }

            return menu.ToString();
        }

        public Dictionary<string, Action> GetMenuActions()
        {
            var dictionary = new Dictionary<string, Action>();
            var methods = GetMenuMethods();

            foreach (var method in methods)
            {
                var action = (Action)method.CreateDelegate(typeof(Action), this);
                var attribute = method.GetAttribute<MenuAttribute>();
                var description = attribute.Description;
                dictionary.Add(description, action);
            }

            MethodsDescriptions = dictionary.Keys.ToArray();

            return dictionary;
        }

        public string GetMethodDescription()
        {
            var option = Input.InputNumber(
                ConsoleVersionResources.OptionInputMsg,
                MethodsDescriptions.Length, 1) - 1;

            return MethodsDescriptions[option];
        }

        protected override string GetSavingPath()
        {
            return GetPath();
        }

        protected override string GetLoadingPath()
        {
            return GetPath();
        }

        private IEnumerable<MethodInfo> GetMenuMethods()
        {
            bool IsMenuMethod(MethodInfo method) 
                => method.GetAttribute<MenuAttribute>() != null;

            int ByPriority(MethodInfo method) 
                => method.GetAttribute<MenuAttribute>().MenuItemPriority.GetValue<int>();

            return typeof(MainViewModel)
                .GetMethods()
                .Where(IsMenuMethod)
                .OrderBy(ByPriority);
        }


        private string GetPath()
        {
            Console.Write("Enter path: ");
            return Console.ReadLine();
        }

        private void OnPathNotFound(string message)
        {
            DisplayGraph();
            Console.WriteLine(message);
            Console.ReadLine();
        }
    }
}
