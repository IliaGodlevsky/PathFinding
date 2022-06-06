using Common.Extensions.EnumerableExtensions;
using ConsoleVersion.Extensions;
using ConsoleVersion.ViewModel;

namespace ConsoleVersion.Views
{
    internal sealed class PathFindView : View
    {
        public PathFindView(PathFindingViewModel model) : base(model)
        {
            var algorithmMenu = model.Algorithms.GetItems1().ToMenuList();
            model.AlgorithmKeyInputMessage = algorithmMenu + MessagesTexts.AlgorithmChoiceMsg;
        }
    }
}
