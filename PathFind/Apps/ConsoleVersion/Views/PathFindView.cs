using Common.Extensions.EnumerableExtensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Model;
using ConsoleVersion.ViewModel;
using System.Linq;

namespace ConsoleVersion.Views
{
    internal sealed class PathFindView : View, IView
    {
        public PathFindView(PathFindingViewModel model) : base(model)
        {
            string algorithmMenu = new MenuList(model.Algorithms.GetItems1()).ToString();
            model.AlgorithmKeyInputMessage = algorithmMenu + MessagesTexts.AlgorithmChoiceMsg;
        }
    }
}
