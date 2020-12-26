using Common.Extensions;
using ConsoleVersion.InputClass;
using ConsoleVersion.Model;
using ConsoleVersion.View;
using GraphLib.Extensions;
using GraphLib.Graphs;
using GraphViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using Console = Colorful.Console;

namespace ConsoleVersion.ViewModel
{
    internal class MainViewModel : MainModel
    {
        private string[] MethodsDescriptions { get; set; }
        public const string QuitCommand = "Quit";

        public MainViewModel() : base()
        {
            VertexEventHolder = new ConsoleVertexEventHolder();
            FieldFactory = new ConsoleGraphFieldFactory();
            InfoConverter = (info) => new ConsoleVertex(info);
        }

        [Description("Make unweighted")]
        public void MakeGraphUnweighted()
        {
            Graph.ToUnweighted();
        }

        [Description("Make weighted")]
        public void MakeGraphWeighted()
        {
            Graph.ToWeighted();
        }

        [Description("Create new graph")]
        public override void CreateNewGraph()
        {
            var model = new GraphCreatingViewModel(this);
            var view = new GraphCreateView(model);

            view.Start();
        }

        [Description("Find path")]
        public override void FindPath()
        {
            var model = new PathFindingViewModel(this);
            model.OnPathNotFound += OnPathNotFound;
            var view = new PathFindView(model);

            view.Start();
        }

        [Description("Reverse vertes")]
        public void ReverseVertex()
        {
            var upperPossibleXValue = (Graph as Graph2D).Width - 1;
            var upperPossibleYValue = (Graph as Graph2D).Length - 1;

            var point = Input.InputPoint(upperPossibleXValue, upperPossibleYValue);

            (Graph[point] as ConsoleVertex).Reverse();
        }

        [Description("Change vertex cost")]
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

        [Description("Save graph")]
        public override void SaveGraph()
        {
            base.SaveGraph();
        }

        [Description("Load graph")]
        public override void LoadGraph()
        {
            base.LoadGraph();
        }

        [Description(QuitCommand)]
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
            var methods = typeof(MainViewModel)
                .GetMethods()
                .Where(method => method.GetAttribute<DescriptionAttribute>() != null)
                .ToArray();

            var menu = new StringBuilder();

            for (int i = 0; i < methods.Length; i++)
            {
                var attribute = methods[i].GetAttribute<DescriptionAttribute>();
                var description = attribute.Description;
                var menuItem = string.Format(ConsoleVersionResources.MenuFormat, i + 1, description);
                menu.AppendLine(menuItem);
            }

            return menu.ToString();
        }

        public Dictionary<string, Action> GetMenuActions()
        {
            var dictionary = new Dictionary<string, Action>();
            var methods = typeof(MainViewModel)
                .GetMethods()
                .Where(method => method.GetAttribute<DescriptionAttribute>() != null);

            foreach (var method in methods)
            {
                var action = (Action)method.CreateDelegate(typeof(Action), this);
                var attribute = method.GetAttribute<DescriptionAttribute>();
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
