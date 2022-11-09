using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.ViewModel;

namespace Pathfinding.App.Console.Views
{
    internal sealed class GraphCreateView : View
    {
        public GraphCreateView(GraphCreatingViewModel model) : base(model)
        {
            var graphAssembleMenu = model.GraphAssembles.CreateMenuList(columnsNumber: 1);
            model.GraphAssembleInpuMessage = graphAssembleMenu + MessagesTexts.GraphAssembleChoiceMsg;
        }
    }
}