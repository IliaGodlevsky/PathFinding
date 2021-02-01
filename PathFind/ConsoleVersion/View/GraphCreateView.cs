using ConsoleVersion.Resource;
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

            Model.ObstaclePercentInputMessage = Resources.ObstaclePercentInputMsg;
            Model.WidthInputMessage = Resources.WidthInputMsg;
            Model.HeightInputMessage = Resources.HeightInputMsg;
        }

        public void Start()
        {
            Model.CreateGraph();
        }
    }
}
