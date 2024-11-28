﻿using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class DeleteRunButton
    {
        private void Initialize()
        {
            Text = "Delete";
            X = Pos.Percent(66);
            Y = 0;
            Width = Dim.Percent(34);
        }
    }
}
