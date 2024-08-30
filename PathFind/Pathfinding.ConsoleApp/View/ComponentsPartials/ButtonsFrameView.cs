using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View.ButtonsFrameViews
{
    internal sealed partial class ButtonsFrameView : FrameView
    {
        //private readonly Button newButton = new Button("New");
        //private readonly Button saveButton = new Button("Save");
        //private readonly Button loadButton = new Button("Load");
        //private readonly Button deleteButton = new Button("Delete");

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

            //saveButton.X = Pos.Right(newButton);
            //saveButton.Y = 0;
            //saveButton.Width = Dim.Percent(25);

            //loadButton.X = Pos.Right(saveButton);
            //loadButton.Y = 0;
            //loadButton.Width = Dim.Percent(25);

            //deleteButton.X = Pos.Right(loadButton);
            //deleteButton.Y = 0;
            //deleteButton.Width = Dim.Percent(25);

            //newButton.X = 0;
            //newButton.Y = 0;
            //newButton.Width = Dim.Percent(25);

            //Add(newButton, saveButton, loadButton, deleteButton);
        }
    }
}
