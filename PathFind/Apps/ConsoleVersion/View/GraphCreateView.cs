using ConsoleVersion.View.Interface;
using ConsoleVersion.ViewModel;
using static ConsoleVersion.Resource.Resources;

namespace ConsoleVersion.View
{
    internal sealed class GraphCreateView : IView
    {
        private GraphCreatingViewModel Model { get; }

        public GraphCreateView(GraphCreatingViewModel model)
        {
            Model = model;
            var keys = model.GraphAssembles.Keys;
            string graphAssembleMenu = new MenuList(keys, 1).ToString();
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
