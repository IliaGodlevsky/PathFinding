using ConsoleVersion.View.Interface;
using ConsoleVersion.ViewModel;

using static ConsoleVersion.Resource.Resources;

namespace ConsoleVersion.View
{
    internal class GraphCreateView : IView
    {
        private GraphCreatingViewModel Model { get; }

        public GraphCreateView(GraphCreatingViewModel model)
        {
            Model = model;

            Model.ObstaclePercentInputMessage = ObstaclePercentInputMsg;
            Model.WidthInputMessage = WidthInputMsg;
            Model.HeightInputMessage = HeightInputMsg;
        }

        public void Start()
        {
            Model.CreateGraph();
        }
    }
}
