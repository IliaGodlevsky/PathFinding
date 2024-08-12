using Pathfinding.App.Console.ViewModel;
using System.Collections.Generic;
using System.Linq;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    public class GraphAssembleView : Window
    {
        public GraphAssembleView(GraphAssembleViewModel viewModel,
            IEnumerable<Terminal.Gui.View> childViews)
        {
            Data = viewModel;
            Add(childViews.ToArray());
        }
    }
}
