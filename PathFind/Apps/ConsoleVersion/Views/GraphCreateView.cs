using ConsoleVersion.Extensions;
using ConsoleVersion.ViewModel;

namespace ConsoleVersion.Views
{
    internal sealed class GraphCreateView : View
    {
        public GraphCreateView(GraphCreatingViewModel model) : base(model)
        {
            var graphAssembleMenu = model.GraphAssembles.Keys.ToMenuList(columnsNumber: 1);
            model.GraphAssembleInpuMessage = graphAssembleMenu + MessagesTexts.GraphAssembleChoiceMsg;
        }
    }
}