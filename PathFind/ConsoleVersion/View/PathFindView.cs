using Algorithm.AlgorithmCreating;
using ConsoleVersion.View.Interface;
using ConsoleVersion.ViewModel;

namespace ConsoleVersion.View
{
    internal class PathFindView : IView
    {
        public PathFindingViewModel Model { get; }

        public PathFindView(PathFindingViewModel model)
        {
            Model = model;

            model.AlgorithmKeys = AlgorithmFactory.AlgorithmsDescriptions;
            var algorithmMenu = Menu.CreateMenu(model.AlgorithmKeys);
            Model.AlgorithmKeyInputMessage = algorithmMenu + ConsoleVersionResources.ChooseAlrorithm;
            Model.StartVertexInputMessage = "\n" + ConsoleVersionResources.StartVertexPointInputMsg;
            Model.EndVertexInputMessage = ConsoleVersionResources.EndVertexCoordinateInputMsg;
        }

        public void Start()
        {
            Model.FindPath();
        }
    }
}
