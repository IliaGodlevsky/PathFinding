using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View.GraphCreateViews
{
    internal sealed partial class GraphParametresView
    {
        private readonly Label graphWidthLabel = new Label("Width");
        private readonly TextField graphWidthInput = new TextField();
        private readonly Label graphLengthLabel = new Label("Length");
        private readonly TextField graphLengthInput = new TextField();
        private readonly Label obstaclesLabel = new Label("Obstacles");
        private readonly TextField obstaclesInput = new TextField();

        private void Initialize()
        {
            X = 1;
            Y = Pos.Percent(15) + 1;
            Width = Dim.Percent(35);
            Height = Dim.Percent(40);
            Border = new Border()
            {
                BorderStyle = BorderStyle.Rounded,
                Padding = new Thickness(0),
                Title = "Parametres"
            };

            graphWidthLabel.X = 1;
            graphWidthLabel.Y = 1;
            graphWidthLabel.Width = Dim.Percent(50, true);

            graphWidthInput.X = Pos.Right(graphWidthLabel) + 1;
            graphWidthInput.Y = 1;
            graphWidthInput.Width = Dim.Fill(1);

            graphLengthLabel.X = 1;
            graphLengthLabel.Y = Pos.Bottom(graphWidthLabel) + 1;
            graphLengthLabel.Width = Dim.Percent(50, true);

            graphLengthInput.X = Pos.Right(graphLengthLabel) + 1;
            graphLengthInput.Y = Pos.Bottom(graphWidthInput) + 1;
            graphLengthInput.Width = Dim.Fill(1);

            obstaclesLabel.X = 1;
            obstaclesLabel.Y = Pos.Bottom(graphLengthLabel) + 1;
            obstaclesLabel.Width = Dim.Percent(50, true);

            obstaclesInput.X = Pos.Right(obstaclesLabel) + 1;
            obstaclesInput.Y = Pos.Bottom(graphLengthInput) + 1;
            obstaclesInput.Width = Dim.Fill(1);
            Add(graphWidthLabel, graphWidthInput, graphLengthLabel, graphLengthInput,
                obstaclesLabel, obstaclesInput);
        }
    }
}
