using Autofac.Features.AttributeFilters;
using DynamicData.Kernel;
using Pathfinding.ConsoleApp.Injection;
using System.Collections.Generic;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class RunsFrame : FrameView
    {
        public RunsFrame([KeyFilter(KeyFilters.GraphRunsView)] IEnumerable<Terminal.Gui.View> children)
        {
            Initialize();
            Add(children.AsArray());
        }
    }
}
