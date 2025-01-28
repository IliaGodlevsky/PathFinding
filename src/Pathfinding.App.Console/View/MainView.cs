﻿using Autofac.Features.AttributeFilters;
using Pathfinding.App.Console.Injection;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal sealed class MainView : Window
    {
        public MainView([KeyFilter(KeyFilters.MainWindow)] IEnumerable<Terminal.Gui.View> children)
        {
            X = 0;
            Y = 0;
            Height = Dim.Fill();
            Width = Dim.Fill();
            Border = new() { DrawMarginFrame = false, BorderThickness = new(0) };
            Add(children.ToArray());
            Loaded += OnActivate;
        }

        private void OnActivate()
        {
            var driver = Application.Driver;
            var backgroundColor = Enum.Parse<Color>(Settings.Default.BackgroundColor);
            var foregroundColor = Enum.Parse<Color>(Settings.Default.ForegroundColor);
            var attribute = driver.MakeAttribute(foregroundColor, backgroundColor);
            Colors.Base.Normal = attribute;
        }
    }
}
