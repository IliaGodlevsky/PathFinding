﻿using GraphLibrary.Constants;
using System.Windows.Controls;
using WpfVersion.Infrastructure;
using WpfVersion.Model.Graph;
using WpfVersion.Model.GraphFactory;
using WpfVersion.Model.RoleChanger;
using WpfVersion.Model.Vertex;
using WpfVersion.ViewModel.Resources;

namespace WpfVersion.ViewModel
{
    public class GraphCreateViewModel : BaseViewModel
    {
        public string Width { get; set; }
        public string Height { get; set; }
        public int ObstaclePercent { get; set; }

        private readonly MainWindowViewModel model;
        private WpfGraph graph;
        private Canvas graphField;

        public RelayCommand ConfirmCreateGraphCommand { get; }
        public RelayCommand CancelCreateGraphCommand { get; }

        public GraphCreateViewModel(MainWindowViewModel model)
        {
            this.model = model;
            ConfirmCreateGraphCommand = new RelayCommand(ExecuteConfirmCreateGraphCommand, 
                CanExecuteConfirmCreateGraphCommand);
            CancelCreateGraphCommand = new RelayCommand(obj=> model?.Window.Close(), obj => true);
        }

        private void ExecuteConfirmCreateGraphCommand(object param)
        {
            int width = int.Parse(Width);
            int height = int.Parse(Height);
            var factory = new RandomValuedWpfGraphFactory(ObstaclePercent, 
                width, 
                height, 
                Const.SIZE_BETWEEN_VERTICES);
            graph = (WpfGraph)factory.GetGraph();
            FillGraphField(ref graph, ref graphField);
            model.GraphField = graphField;
            model.Graph = graph;
            model.Window.Close();
            model.GraphParametres = string.Format(ViewModelResources.GraphParametresFormat, 
                model.Graph.Width, model.Graph.Height, model.Graph.ObstaclePercent);
            AdjustSizeOfMainWindow(graph.Width, graph.Height);
        }

        private bool CanExecuteConfirmCreateGraphCommand(object param)
        {
            if (int.TryParse(Width, out int width)
                && int.TryParse(Height, out int height))
                return width > 0 && height > 0;
            else
                return false;
        }

        public static void FillGraphField(ref WpfGraph graph, ref Canvas graphField)
        {
            var changer = new WpfRoleChanger(graph);
            graphField = new Canvas();
            for (int i = 0; i < graph.Width; i++)
            {
                for (int j = 0; j < graph.Height; j++)
                {
                    graphField.Children.Add(graph[i, j] as WpfVertex);
                    if (!graph[i, j].IsObstacle)
                        (graph[i, j] as WpfVertex).MouseLeftButtonDown += changer.SetStartPoint;
                    (graph[i, j] as WpfVertex).MouseRightButtonDown += changer.ReversePolarity;
                    Canvas.SetLeft(graph[i, j] as WpfVertex, Const.SIZE_BETWEEN_VERTICES * i);
                    Canvas.SetTop(graph[i, j] as WpfVertex, Const.SIZE_BETWEEN_VERTICES * j);
                }
            }
            graph.SetStart += changer.SetStartPoint;
            graph.SetEnd += changer.SetDestinationPoint;
        }
    }
}
