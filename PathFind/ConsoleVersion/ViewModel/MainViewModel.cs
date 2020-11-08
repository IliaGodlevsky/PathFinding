using ConsoleVersion.InputClass;
using ConsoleVersion.Model;
using ConsoleVersion.View;
using ConsoleVersion.Model.EventHolder;
using ConsoleVersion.Model.Vertex;
using System.Drawing;
using Console = Colorful.Console;
using GraphLib.Graphs;
using GraphViewModel;
using System;

namespace ConsoleVersion.ViewModel
{
    internal class MainViewModel : MainModel
    {
        public MainViewModel() : base()
        {            
            VertexEventHolder = new ConsoleVertexEventHolder();
            FieldFactory = new ConsoleGraphFieldFactory();
            DtoConverter = (dto) => new ConsoleVertex(dto);
        }

        public override void CreateNewGraph()
        {
            try
            {
                var model = new GraphCreatingViewModel(this);
                var view = new GraphCreateView(model);

                view.Start();
            }
            catch(Exception ex)
            {
                logger.Log(ex);
            }
        }

        public override void FindPath()
        {
            try
            {
                var model = new PathFindingViewModel(this);
                var view = new PathFindView(model);

                view.Start();
            }
            catch(Exception ex)
            {
                logger.Log(ex);
            }
        }

        public void Reverse()
        {
            try
            {
                var point = Input.InputPoint((Graph as Graph2d).Width, (Graph as Graph2d).Length);
                (Graph[point] as ConsoleVertex).ChangeRole();
            }
            catch(Exception ex)
            {
                logger.Log(ex);
            }
        }

        public void ChangeVertexValue()
        {
            try
            {
                var point = Input.InputPoint((Graph as Graph2d).Width, (Graph as Graph2d).Length);

                while (Graph[point].IsObstacle)
                {
                    point = Input.InputPoint((Graph as Graph2d).Width, (Graph as Graph2d).Length);
                }

                (Graph[point] as ConsoleVertex).ChangeCost();
            }
            catch(Exception ex)
            {
                logger.Log(ex);
            }
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

        public void DisplayGraph()
        {
            try
            {
                Console.Clear();
                Console.ForegroundColor = Color.White;
                Console.WriteLine(GraphParametres);
                (GraphField as ConsoleGraphField)?.ShowGraphWithFrames();
                Console.WriteLine(PathFindingStatistics);
            }
            catch(Exception ex)
            {
                logger.Log(ex);
            }
        }
    }
}
