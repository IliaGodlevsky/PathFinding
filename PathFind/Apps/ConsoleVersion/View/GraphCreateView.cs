using ConsoleVersion.View.Interface;
using ConsoleVersion.ViewModel;
using System.Linq;
using static ConsoleVersion.Resource.Resources;

namespace ConsoleVersion.View
{
    internal sealed class GraphCreateView : IView
    {
        private GraphCreatingViewModel Model { get; }

        public GraphCreateView(GraphCreatingViewModel model)
        {
            Model = model;

            var graphAssembleMenu = Menu.CreateMenu(model.GraphAssembleKeys.ToArray(), 1);
            Model.GraphAssembleInpuMessage = graphAssembleMenu + ChooseGraphAssemble;
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
