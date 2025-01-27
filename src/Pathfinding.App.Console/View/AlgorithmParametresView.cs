using Autofac.Features.AttributeFilters;
using Pathfinding.App.Console.Injection;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
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
