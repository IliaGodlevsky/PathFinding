using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.ViewModel;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.Views
{
    internal sealed class GraphCreateView : View
    {
        public GraphCreateView(GraphCreatingViewModel model, ILog log) : base(model, log)
        {
            var graphAssembleMenu = model.GraphAssembles.CreateMenuList(columnsNumber: 1);
            model.GraphAssembleInpuMessage = graphAssembleMenu + MessagesTexts.GraphAssembleChoiceMsg;
        }
    }
}