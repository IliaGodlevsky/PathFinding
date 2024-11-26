using System.Globalization;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class HeuristicsView : FrameView
    {
        private readonly RadioGroup heuristics = new RadioGroup();
        private readonly Label weightLabel = new Label("Weight");
        private readonly TextField weightTextField = new TextField();

        private void Initialize()
        {
            heuristics.X = 1;
            heuristics.Y = 1;
            X = Pos.Percent(55);
            Y = 1;
            Height = Dim.Percent(50);
            Width = Dim.Percent(40);
            Border = new Border()
            {
                BorderStyle = BorderStyle.Rounded,
                Title = "Heuristics"
            };
            Visible = false;

            weightLabel.Y = Pos.Bottom(heuristics) + 1;
            weightLabel.X = 1;
            weightTextField.X = Pos.Right(weightLabel) + 2;
            weightTextField.Y = Pos.Bottom(heuristics) + 1;
            weightTextField.Width = Dim.Percent(35);
            var decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            weightTextField.KeyPress += (args) =>
            {
                var keyChar = (char)args.KeyEvent.KeyValue;
                if (args.KeyEvent.Key == Key.Backspace ||
                    args.KeyEvent.Key == Key.Delete ||
                    args.KeyEvent.Key == Key.CursorLeft ||
                    args.KeyEvent.Key == Key.CursorRight ||
                    args.KeyEvent.Key == Key.Home ||
                    args.KeyEvent.Key == Key.End)
                {
                    return;
                }
                if (char.IsDigit(keyChar))
                {
                    if (weightTextField.Text.Length + 1 <= 4)
                    {
                        return;
                    }
                }
                if (keyChar.ToString() == decimalSeparator
                  && !weightTextField.Text.ToString().Contains(decimalSeparator))
                {
                    return;
                }

                args.Handled = true;
            };
            Add(weightLabel, weightTextField, heuristics);
        }
    }
}
