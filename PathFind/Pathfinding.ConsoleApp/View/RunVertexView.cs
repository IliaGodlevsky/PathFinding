using Pathfinding.ConsoleApp.Model;
using Pathfinding.Infrastructure.Data.Extensions;
using ReactiveUI;
using System;
using System.Linq.Expressions;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed class RunVertexView : Label
    {
        private static readonly ColorScheme ObstacleColor = Create(ColorConstants.ObstacleVertexColor);
        private static readonly ColorScheme RegularColor = Create(ColorConstants.RegularVertexColor);
        private static readonly ColorScheme VisitedColor = Create(ColorConstants.VisitedVertexColor);
        private static readonly ColorScheme EnqueuedColor = Create(ColorConstants.EnqueuedVertexColor);
        private static readonly ColorScheme SourceColor = Create(ColorConstants.SourceVertexColor);
        private static readonly ColorScheme TargetColor = Create(ColorConstants.TargetVertexColor);
        private static readonly ColorScheme TransitColor = Create(ColorConstants.TranstiVertexColor);
        private static readonly ColorScheme PathColor = Create(ColorConstants.PathVertexColor);
        private static readonly ColorScheme CrossedPathColor = Create(ColorConstants.CrossedPathColor);

        private const int LabelWidth = GraphFieldView.DistanceBetweenVertices;
        private readonly CompositeDisposable disposables = new();

        private readonly RunVertexModel model;

        public RunVertexView(RunVertexModel model)
        {
            this.model = model;
            X = model.Position.GetX() * LabelWidth;
            Y = model.Position.GetY();
            Width = LabelWidth;

            model.WhenAnyValue(x => x.Cost)
                .Select(x => x.CurrentCost.ToString())
                .Do(x => Text = x)
                .Subscribe()
                .DisposeWith(disposables);

            BindTo(x => x.IsObstacle, ObstacleColor, RegularColor, 0);
            BindTo(x => x.IsTarget, TargetColor, RegularColor);
            BindTo(x => x.IsSource, SourceColor, RegularColor);
            BindTo(x => x.IsTransit, TransitColor, RegularColor);
            BindTo(x => x.IsPath, PathColor, VisitedColor);
            BindTo(x => x.IsVisited, VisitedColor, EnqueuedColor);
            BindTo(x => x.IsEnqueued, EnqueuedColor, RegularColor);
            BindTo(x => x.IsCrossedPath, CrossedPathColor, PathColor);
        }

        private static ColorScheme Create(Color foreground)
        {
            var driver = Application.Driver;
            var attribute = driver.MakeAttribute(foreground, ColorConstants.BackgroundColor);
            return new() { Normal = attribute };
        }

        private void BindTo(Expression<Func<RunVertexModel, bool>> expression,
            ColorScheme toColor, ColorScheme falseColor, int toSkip = 1)
        {
            model.WhenAnyValue(expression)
               .Skip(toSkip)
               .Select(x => x ? toColor : falseColor)
               .BindTo(this, x => x.ColorScheme)
               .DisposeWith(disposables);
        }

        protected override void Dispose(bool disposing)
        {
            disposables.Dispose();
            base.Dispose(disposing);
        }
    }
}
