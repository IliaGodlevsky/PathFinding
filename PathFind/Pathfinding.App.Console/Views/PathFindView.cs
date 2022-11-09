using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.ViewModel;

namespace Pathfinding.App.Console.Views
{
    internal sealed class PathFindView : View
    {
        public PathFindView(PathFindingViewModel model) : base(model)
        {
            var algorithmMenu = model.Algorithms.CreateMenuList();
            model.AlgorithmKeyInputMessage = algorithmMenu + MessagesTexts.AlgorithmChoiceMsg;
        }
    }
}
