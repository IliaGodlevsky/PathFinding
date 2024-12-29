using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal abstract class VertexView<T> : Label
        where T : IVertex
    {
        protected static readonly ColorScheme ObstacleColor = Create(AppSettings.Default.BackgroundColor);
        protected static readonly ColorScheme RegularColor = Create(AppSettings.Default.RegularVertexColor);
        protected static readonly ColorScheme VisitedColor = Create(AppSettings.Default.VisitedVertexColor);
        protected static readonly ColorScheme EnqueuedColor = Create(AppSettings.Default.EnqueuedVertexColor);
        protected static readonly ColorScheme SourceColor = Create(AppSettings.Default.SourceVertexColor);
        protected static readonly ColorScheme TargetColor = Create(AppSettings.Default.TargetVertexColor);
        protected static readonly ColorScheme TransitColor = Create(AppSettings.Default.TranstiVertexColor);
        protected static readonly ColorScheme PathColor = Create(AppSettings.Default.PathVertexColor);
        protected static readonly ColorScheme CrossedPathColor = Create(AppSettings.Default.CrossedPathColor);

        protected readonly T model;
        protected const int LabelWidth = GraphFieldView.DistanceBetweenVertices;

        protected VertexView(T model)
        {
            this.model = model;
            X = model.Position.GetX() * LabelWidth;
            Y = model.Position.GetY();
            Width = LabelWidth;
        }

        private static ColorScheme Create(Color foreground)
        {
            var driver = Application.Driver;
            var backgroundColor = AppSettings.Default.BackgroundColor;
            var attribute = driver.MakeAttribute(foreground, backgroundColor);
            return new() { Normal = attribute };
        }
    }
}
