﻿using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal abstract class VertexView<T> : Label
        where T : IVertex
    {
        protected static readonly ColorScheme ObstacleColor = Create(Settings.Default.BackgroundColor);
        protected static readonly ColorScheme RegularColor = Create(Settings.Default.RegularVertexColor);
        protected static readonly ColorScheme VisitedColor = Create(Settings.Default.VisitedVertexColor);
        protected static readonly ColorScheme EnqueuedColor = Create(Settings.Default.EnqueuedVertexColor);
        protected static readonly ColorScheme SourceColor = Create(Settings.Default.SourceVertexColor);
        protected static readonly ColorScheme TargetColor = Create(Settings.Default.TargetVertexColor);
        protected static readonly ColorScheme TransitColor = Create(Settings.Default.TranstiVertexColor);
        protected static readonly ColorScheme PathColor = Create(Settings.Default.PathVertexColor);
        protected static readonly ColorScheme CrossedPathColor = Create(Settings.Default.CrossedPathColor);

        protected readonly T model;
        protected const int LabelWidth = GraphFieldView.DistanceBetweenVertices;

        protected VertexView(T model)
        {
            this.model = model;
            X = model.Position.GetX() * LabelWidth;
            Y = model.Position.GetY();
            Width = LabelWidth;
        }

        private static ColorScheme Create(string foreground)
        {
            var driver = Application.Driver;
            var backgroundColor = Enum.Parse<Color>(Settings.Default.BackgroundColor);
            var attribute = driver.MakeAttribute(Enum.Parse<Color>(foreground), backgroundColor);
            return new() { Normal = attribute };
        }
    }
}
