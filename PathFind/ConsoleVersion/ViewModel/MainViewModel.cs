using ConsoleVersion.GraphLoader;
using ConsoleVersion.GraphSaver;
using ConsoleVersion.InputClass;
using ConsoleVersion.Model;
using ConsoleVersion.RoleChanger;
using ConsoleVersion.View;
using GraphLibrary.Model;
using GraphLibrary.RoleChanger;
using System;
using System.Drawing;

namespace ConsoleVersion.ViewModel
{
    public class MainViewModel : AbstractMainModel
    {
        private IVertexRoleChanger changer;
        public MainViewModel()
        {            
            Format = Res.GraphParametresFormat;
            saver = new ConsoleGraphSaver();
            loader = new ConsoleGraphLoader();
            filler = new ConsoleGraphFiller();
        }

        public override void CreateNewGraph()
        {
            var model = new CreateGraphViewModel(this);
            var view = new GraphCreateView(model);
            view.Start();
        }

        public override void PathFind()
        {
            var model = new PathFindViewModel(this);
            var view = new PathFindView(model);
            view.Start();
        }

        public void Reverse()
        {            
            if (Graph == null)
                return;
            changer = new ConsoleVertexRoleChanger(Graph);
            Console.WriteLine(Res.ReverseMsg);
            Point point = Input.InputPoint(Graph.Width, Graph.Height);
            changer.ReversePolarity(Graph[point.X, point.Y], new EventArgs());
        }
    }
}
