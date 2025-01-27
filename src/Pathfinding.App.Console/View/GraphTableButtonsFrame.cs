using Autofac.Features.AttributeFilters;
using Pathfinding.App.Console.Injection;
using System.Collections.Generic;
using System.Linq;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal sealed partial class GraphTableButtonsFrame : FrameView
    {
        public GraphTableButtonsFrame([KeyFilter(KeyFilters.GraphTableButtons)] IEnumerable<Terminal.Gui.View> children)
        {
            Initialize();
            Add(children.ToArray());
        }
    }
}
