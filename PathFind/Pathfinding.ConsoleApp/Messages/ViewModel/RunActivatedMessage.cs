using Pathfinding.ConsoleApp.Model;
using Pathfinding.Service.Interface.Models.Read;

namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal class RunActivatedMessage
    {
        public RunVisualizationModel Run { get; }

        public RunActivatedMessage(RunVisualizationModel run)
        {
            Run = run;
        }
    }
}
