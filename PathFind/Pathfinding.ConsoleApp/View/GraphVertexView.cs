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
    internal sealed class GraphVertexView : Label
    {
        private static readonly ColorScheme ObstacleColor = Create(ColorConstants.ObstacleVertexColor);
        private static readonly ColorScheme RegularColor = Create(ColorConstants.RegularVertexColor);
        private static readonly ColorScheme SourceColor = Create(ColorConstants.SourceVertexColor);
        private static readonly ColorScheme TargetColor = Create(ColorConstants.TargetVertexColor);
        private static readonly ColorScheme TransitColor = Create(ColorConstants.TranstiVertexColor);

        private const int LabelWidth = 3;

        private readonly CompositeDisposable disposables = new();
        private readonly GraphVertexModel model;

        public GraphVertexView(GraphVertexModel model)
        {
            X = model.Position.GetX() * LabelWidth;
            Y = model.Position.GetY();
            Width = LabelWidth;
            this.model = model;
            BindTo(x => x.IsObstacle, ObstacleColor, RegularColor, 0);
            BindTo(x => x.IsTarget, TargetColor, RegularColor, 1);
            BindTo(x => x.IsSource, SourceColor, RegularColor, 1);
            BindTo(x => x.IsTransit, TransitColor, RegularColor, 1);

            model.WhenAnyValue(x => x.Cost)
                .Select(x => x.CurrentCost.ToString())
                .Do(x => Text = x)
                .Subscribe()
                .DisposeWith(disposables);
        }

        private static ColorScheme Create(Color foreground)
        {
            var driver = Application.Driver;
            var attribute = driver.MakeAttribute(foreground, ColorConstants.BackgroundColor);
            return new() { Normal = attribute };
        }

        private void BindTo(Expression<Func<GraphVertexModel, bool>> expression, ColorScheme toColor,
            ColorScheme falseColor, int skip)
        {
            model.WhenAnyValue(expression)
               .Skip(skip)
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
