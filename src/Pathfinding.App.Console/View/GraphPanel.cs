using Autofac.Features.AttributeFilters;
using Pathfinding.App.Console.Injection;
using System.Collections.Generic;
using System.Linq;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal sealed partial class GraphPanel : FrameView
    {
        public GraphPanel([KeyFilter(KeyFilters.GraphPanel)] IEnumerable<Terminal.Gui.View> children)
        {
            Initialize();
            Add(children.ToArray());
        }
    }
}
