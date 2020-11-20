using ConsoleVersion.Model;
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

            Model.ObstaclePercentInputMessage = ConsoleVersionResources.PercentMsg;
            Model.WidthInputMessage = ConsoleVersionResources.WidthMsg;
            Model.HeightInputMessage = ConsoleVersionResources.HeightMsg;
        }

        public void Start()
        {
            Model.CreateGraph(() => new ConsoleVertex());
        }
    }
}
