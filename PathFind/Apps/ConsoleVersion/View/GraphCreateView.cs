using ConsoleVersion.View.Abstraction;
using ConsoleVersion.View.Interface;
using ConsoleVersion.ViewModel;
using static ConsoleVersion.Resource.Resources;

namespace ConsoleVersion.View
{
    internal sealed class GraphCreateView : View<GraphCreatingViewModel>, IView
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