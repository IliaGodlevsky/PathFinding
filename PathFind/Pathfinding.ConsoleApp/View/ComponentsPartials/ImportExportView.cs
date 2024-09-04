using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class ImportExportView
    {
        private readonly CheckBox withRangeCheckbox = new CheckBox("With range");
        private readonly CheckBox withRunsCheckbox = new CheckBox("With runs");
        private readonly Button okButton = new Button("Ok");
        private readonly Button cancelButton = new Button("Cancel");

        private void Initialize()
        {
            X = Pos.Center();
            Y = Pos.Center();
            Width = Dim.Percent(10);
            Height = Dim.Percent(10);
            Visible = false;
            ClearOnVisibleFalse = true;
            Border = new Border()
            {
                BorderStyle = BorderStyle.None,
                BorderThickness = new Thickness(0)
            };

            withRangeCheckbox.X = 1;
            withRangeCheckbox.Y = 1;
            withRunsCheckbox.Y = Pos.Bottom(withRangeCheckbox) + 1;
            withRunsCheckbox.X = 1;
            okButton.X = 1;
            okButton.Y = Pos.Bottom(withRunsCheckbox) + 1;
            cancelButton.X = Pos.Right(okButton) + 1;
            cancelButton.Y = Pos.Bottom(withRunsCheckbox) + 1;

            Add(withRangeCheckbox, withRangeCheckbox, okButton, cancelButton);
        }
    }
}
