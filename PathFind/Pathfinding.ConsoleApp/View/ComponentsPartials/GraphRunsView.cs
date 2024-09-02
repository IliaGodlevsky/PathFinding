using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class GraphRunsView
    {
        private void Initialize()
        {
            X = 0;
            Y = Pos.Percent(50);
            Width = Dim.Fill();
            Height = Dim.Percent(50);
            Border = new Border()
            {
                BorderStyle = BorderStyle.Rounded,
                Padding = new Thickness(1, 0, 1, 0),
                Title = "Runs"
            };
        }
    }
}
