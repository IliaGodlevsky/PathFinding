using ConsoleVersion.Forms;
using ConsoleVersion.Graph;
using ConsoleVersion.PathFindAlgorithmMenu;
using GraphLibrary.Model;
using System;

namespace ConsoleVersion.ViewModel
{
    public class PathFindViewModel : AbstractPathFindModel
    {
        private readonly PathFindMenu menu;

        public PathFindViewModel(IMainModel model) : base(model)
        {
            menu = new PathFindMenu(model.Graph as ConsoleGraph);
            badResultMessage = Res.BadResultMsg;
        }

        public override void PathFind()
        {
            Console.Clear();
            model.Graph.Refresh();
            Console.WriteLine(model.GraphParametres);
            GraphShower.ShowGraph(model.Graph as ConsoleGraph);
            menu.ChooseStart();
            menu.ChooseEnd();
            Console.Clear();
            Console.WriteLine(model.GraphParametres);
            GraphShower.ShowGraph(model.Graph as ConsoleGraph);
            Algorithm = menu.GetAlgorithmEnum();
            base.PathFind();
        }

        protected override void FindPreparations()
        {
            pathFindAlgorythm.PauseEvent = () => { };
        }

        protected override void ShowMessage(string message)
        {
            Console.Clear();
            Console.WriteLine(model.GraphParametres);
            GraphShower.ShowGraph(model.Graph as ConsoleGraph);
            Console.WriteLine(message);
            Console.ReadLine();
        }
    }
}
