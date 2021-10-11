using ConsoleVersion.Interface;
using ConsoleVersion.View.Abstraction;
using ConsoleVersion.ViewModel;
using System.Linq;

namespace ConsoleVersion.View
{
    internal sealed class PathFindView : View<PathFindingViewModel>, IView
    {
        public PathFindView(PathFindingViewModel model) : base(model)
        {
            string algorithmMenu = new MenuList(model.Algorithms.Select(item => item.Item1)).ToString();
            Model.AlgorithmKeyInputMessage = algorithmMenu + MessagesTexts.AlgorithmChoiceMsg;
        }
    }
}
