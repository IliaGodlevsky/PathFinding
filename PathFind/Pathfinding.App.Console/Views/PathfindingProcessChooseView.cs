using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.ViewModel;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.Views
{
    internal sealed class PathfindingProcessChooseView : View
    {
        public PathfindingProcessChooseView(PathfindingProcessChooseViewModel model, ILog log) 
            : base(model, log)
        {
            var algorithmMenu = model.Factories.CreateMenuList(columnsNumber: 3);
            model.AlgorithmKeyInputMessage = algorithmMenu + MessagesTexts.AlgorithmChoiceMsg;
        }
    }
}
