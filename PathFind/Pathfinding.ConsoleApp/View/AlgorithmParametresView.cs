using Autofac.Features.AttributeFilters;
using Pathfinding.ConsoleApp.Injection;
using System.Collections.Generic;
using System.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class AlgorithmParametresView : FrameView
    {
        public AlgorithmParametresView([KeyFilter(KeyFilters.AlgorithmParametresView)] 
            IEnumerable<Terminal.Gui.View> children)
        {
            X = Pos.Percent(25);
            Y = 0;
            Width = Dim.Fill();
            Height = Dim.Percent(90);
            Border = new();
            Add(children.ToArray());
        }
    }
}
