using Autofac.Features.AttributeFilters;
using Pathfinding.ConsoleApp.Injection;
using System.Collections.Generic;
using System.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed class RightPanelView : Window
    {
        public RightPanelView([KeyFilter(KeyFilters.RightPanel)] IEnumerable<Terminal.Gui.View> children)
        {
            X = Pos.Percent(66);
            Y = 0;
            Width = Dim.Percent(34);
            Height = Dim.Fill();
            Border = new Border();

            Add(children.ToArray());
        }
    }
}
