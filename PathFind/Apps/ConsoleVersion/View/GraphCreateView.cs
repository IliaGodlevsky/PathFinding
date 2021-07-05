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
            string graphAssembleMenu = new MenuList(model.GraphAssembles.Keys.ToArray(), 1).ToString();
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
