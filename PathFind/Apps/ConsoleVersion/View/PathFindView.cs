using ConsoleVersion.Resource;
using ConsoleVersion.View.Abstraction;
using ConsoleVersion.ViewModel;

namespace ConsoleVersion.View
{
    internal sealed class PathFindView : View<PathFindingViewModel>
    {
        public PathFindView(PathFindingViewModel model) : base(model)
        {
            string algorithmMenu = new MenuList(model.Algorithms.Keys).ToString();
            Model.AlgorithmKeyInputMessage = algorithmMenu + Resources.ChooseAlrorithm;
            Model.SourceVertexInputMessage = "\n" + Resources.StartVertexPointInputMsg;
            Model.TargetVertexInputMessage = Resources.EndVertexCoordinateInputMsg;
        }
    }
}
