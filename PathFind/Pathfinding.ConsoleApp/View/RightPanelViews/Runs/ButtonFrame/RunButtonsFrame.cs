using Autofac.Features.AttributeFilters;
using Pathfinding.ConsoleApp.Injection;
using System.Collections.Generic;
using System.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View.RightPanelViews.Runs.ButtonFrame
{
    internal sealed partial class RunButtonsFrame : FrameView
    {
        public RunButtonsFrame([KeyFilter(KeyFilters.RunButtonsFrame)] IEnumerable<Terminal.Gui.View> children)
        {
            Initialize();
            Add(children.ToArray());
        }
    }
}
