using ConsoleVersion.View.Interface;
using ConsoleVersion.ViewModel;

namespace ConsoleVersion.View
{
    internal class GraphCreateView : IView
    {
        private GraphCreatingViewModel Model { get; set; }

        public GraphCreateView(GraphCreatingViewModel model)
        {
            Model = model;

            Model.ObstaclePercentInputMessage = ConsoleVersionResources.ObstaclePercentInputMsg;
            Model.WidthInputMessage = ConsoleVersionResources.WidthInputMsg;
            Model.HeightInputMessage = ConsoleVersionResources.HeightInputMsg;
        }

        public void Start()
        {
            Model.CreateGraph();
        }
    }
}
