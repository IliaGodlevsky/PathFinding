﻿using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal sealed partial class GraphExportButton : Button
    {
        private void Initialize()
        {
            X = Pos.Percent(50.01f);
            Y = 0;
            Width = Dim.Percent(16.67f);
            Text = "Save";
        }
    }
}
