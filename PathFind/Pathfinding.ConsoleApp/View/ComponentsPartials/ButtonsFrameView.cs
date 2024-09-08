using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View.ButtonsFrameViews
{
    internal sealed partial class ButtonsFrameView : FrameView
    {
        private void Initialize()
        {
            Border = new Border()
            {
                BorderStyle = BorderStyle.Rounded,
                DrawMarginFrame = false,
                Padding = new Thickness(0)
            };
            X = 0;
            Y = Pos.Percent(87);
            Width = Dim.Fill();
            Height = Dim.Percent(15);
        }
    }
}
