﻿using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class GraphAssembleView
    {
        private readonly Button createButton = new Button("Create");
        private readonly Button cancelButton = new Button("Cancel");

        private void Initialize()
        {
            X = Pos.Center();
            Y = Pos.Center();
            Width = Dim.Fill();
            Height = Dim.Fill();
            Visible = false;
            Border = new Border()
            {
                BorderStyle = BorderStyle.None,
                BorderThickness = new Thickness(0)
            };

            createButton.X = Pos.Percent(35);
            createButton.Y = Pos.Percent(85) + 1;
            cancelButton.X = Pos.Right(createButton) + 2;
            cancelButton.Y = Pos.Percent(85) + 1;
            Add(createButton, cancelButton);
        }
    }
}
