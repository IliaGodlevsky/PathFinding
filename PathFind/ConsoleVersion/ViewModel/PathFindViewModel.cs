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
            model.Graph.Refresh();
            GraphShower.DisplayGraph(model);
            menu.ChooseStart();
            menu.ChooseEnd();
            GraphShower.DisplayGraph(model);
            Algorithm = menu.GetAlgorithmEnum();
            base.PathFind();
        }

        protected override void FindPreparations()
        {
            pathFindAlgorythm.PauseEvent = () => { };
        }

        protected override void ShowMessage(string message)
        {
            GraphShower.DisplayGraph(model);
            Console.WriteLine(message);
            Console.ReadLine();
        }
    }
}
