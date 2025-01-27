using Autofac.Features.AttributeFilters;
using DynamicData.Kernel;
using Pathfinding.App.Console.Injection;
using System.Collections.Generic;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal sealed partial class RunsPanel : FrameView
    {
        public RunsPanel([KeyFilter(KeyFilters.RunsPanel)] IEnumerable<Terminal.Gui.View> children)
        {
            Initialize();
            Add(children.AsArray());
        }
    }
}
