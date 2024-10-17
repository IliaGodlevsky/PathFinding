using Autofac.Features.AttributeFilters;
using Pathfinding.ConsoleApp.Injection;
using System.Collections.Generic;
using System.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
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
