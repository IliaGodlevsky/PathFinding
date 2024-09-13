using Autofac.Features.AttributeFilters;
using Pathfinding.ConsoleApp.Injection;
using System.Collections.Generic;
using System.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class GraphFrameView : FrameView
    {
        public GraphFrameView([KeyFilter(KeyFilters.GraphFrame)] IEnumerable<Terminal.Gui.View> children)
        {
            Initialize();
            Add(children.ToArray());
        }
    }
}
