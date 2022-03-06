using ConsoleVersion.Model;
using ConsoleVersion.ViewModel;

namespace ConsoleVersion.Views
{
    internal sealed class GraphCreateView : View
    {
        public GraphCreateView(GraphCreatingViewModel model) : base(model)
        {
            var keys = model.GraphAssembles.Keys;
            string graphAssembleMenu = new MenuList(keys, 1).ToString();
            model.GraphAssembleInpuMessage = graphAssembleMenu + MessagesTexts.GraphAssembleChoiceMsg;
        }
    }
}