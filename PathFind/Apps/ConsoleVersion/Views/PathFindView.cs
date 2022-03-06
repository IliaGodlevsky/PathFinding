using Common.Extensions.EnumerableExtensions;
using ConsoleVersion.Model;
using ConsoleVersion.ViewModel;

namespace ConsoleVersion.Views
{
    internal sealed class PathFindView : View
    {
        public PathFindView(PathFindingViewModel model) : base(model)
        {
            string algorithmMenu = new MenuList(model.Algorithms.GetItems1()).ToString();
            model.AlgorithmKeyInputMessage = algorithmMenu + MessagesTexts.AlgorithmChoiceMsg;
        }
    }
}
