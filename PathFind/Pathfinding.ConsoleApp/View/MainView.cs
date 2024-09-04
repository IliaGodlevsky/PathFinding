using Autofac.Features.AttributeFilters;
using Pathfinding.ConsoleApp.Injection;
using System.Collections.Generic;
using System.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed class MainView : Window
    {
        public MainView([KeyFilter(KeyFilters.MainWindow)]IEnumerable<Terminal.Gui.View> children)
        {
            Colors.Base.Normal = Application.Driver.MakeAttribute(Color.White, Color.Black);
            X = 0;
            Y = 0;
            Height = Dim.Fill();
            Width = Dim.Fill();
            Add(children.ToArray());
        }
    }
}
