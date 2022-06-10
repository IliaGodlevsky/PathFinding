using ConsoleVersion.Extensions;
using ConsoleVersion.ViewModel;
using System.Linq;

namespace ConsoleVersion.Views
{
    internal sealed class GraphCreateView : View
    {
        public GraphCreateView(GraphCreatingViewModel model) : base(model)
        {
            var graphAssembleMenu = model.GraphAssembles.Select(assemble => assemble.ToString()).ToMenuList(columnsNumber: 1);
            model.GraphAssembleInpuMessage = graphAssembleMenu + MessagesTexts.GraphAssembleChoiceMsg;
        }
    }
}