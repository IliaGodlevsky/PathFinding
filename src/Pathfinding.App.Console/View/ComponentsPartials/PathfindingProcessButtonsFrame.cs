﻿using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal sealed partial class PathfindingProcessButtonsFrame
    {
        private void Initialize()
        {
            X = 0;
            Y = Pos.Percent(90);
            Width = Dim.Fill();
            Height = Dim.Fill();
            Visible = false;
        }
    }
}