﻿using Autofac.Features.AttributeFilters;
using Pathfinding.ConsoleApp.Injection;
using System.Collections.Generic;
using System.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed class MainView : Window
    {
        private const Color ForegroundColor = Color.Gray;
        private const Color BackgroundColor = ColorConstants.BackgroundColor;

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
            var attribute = driver.MakeAttribute(ForegroundColor, BackgroundColor);
            Colors.Base.Normal = attribute;
        }
    }
}
