using ConsoleVersion.View.Abstraction;
using ConsoleVersion.ViewModel;
using static ConsoleVersion.Resource.Resources;

namespace ConsoleVersion.View
{
    internal sealed class GraphCreateView : View<GraphCreatingViewModel>
    {
        public GraphCreateView(GraphCreatingViewModel model) : base(model)
        {
            var keys = model.GraphAssembles.Keys;
            string graphAssembleMenu = new MenuList(keys, 1).ToString();
            Model.GraphAssembleInpuMessage = graphAssembleMenu + ChooseGraphAssemble;
            Model.ObstaclePercentInputMessage = ObstaclePercentInputMsg;
            Model.WidthInputMessage = WidthInputMsg;
            Model.HeightInputMessage = HeightInputMsg;
        }
    }
}