using ConsoleVersion.Extensions;
using ConsoleVersion.ViewModel;
using System.Linq;

namespace ConsoleVersion.Views
{
    internal sealed class PathFindView : View
    {
        public PathFindView(PathFindingViewModel model) : base(model)
        {
            var algorithmMenu = model.Algorithms.Select(algorithm => algorithm.ToString()).ToMenuList();
            model.AlgorithmKeyInputMessage = algorithmMenu + MessagesTexts.AlgorithmChoiceMsg;
        }
    }
}
