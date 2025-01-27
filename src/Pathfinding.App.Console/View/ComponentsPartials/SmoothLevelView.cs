﻿using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal sealed partial class SmoothLevelView
    {
        private readonly RadioGroup smoothLevels = new RadioGroup();

        private void Initialize()
        {
            X = Pos.Percent(70) + 1;
            Y = Pos.Percent(25) + 1;
            Width = Dim.Fill(1);
            Height = Dim.Percent(40);
            Border = new Border()
            {
                BorderStyle = BorderStyle.Rounded,
                Padding = new Thickness(0),
                Title = "Smooth"
            };

            smoothLevels.X = 1;
            smoothLevels.Y = 1;
            Add(smoothLevels);
        }
    }
}
