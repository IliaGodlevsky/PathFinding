using ConsoleVersion.Interface;
using ConsoleVersion.View.Abstraction;
using ConsoleVersion.ViewModel;

namespace ConsoleVersion.View
{
    internal sealed class GraphCreateView : View<GraphCreatingViewModel>, IView
    {
        public GraphCreateView(GraphCreatingViewModel model) : base(model)
        {
            var keys = model.GraphAssembles.Keys;
            string graphAssembleMenu = new MenuList(keys, 1).ToString();
            Model.GraphAssembleInpuMessage = graphAssembleMenu + MessagesTexts.GraphAssembleChoiceMsg;
        }
    }
}