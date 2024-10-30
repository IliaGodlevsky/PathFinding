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
        private const int LabelWidth = 3;
        private readonly CompositeDisposable disposables = new();

        private readonly RunVertexModel model;

        public RunVertexView(RunVertexModel model)
        {
            this.model = model;
            X = model.Position.GetX() * LabelWidth;
            Y = model.Position.GetY();
            Width = LabelWidth;
            Text = model.Cost.CurrentCost.ToString();

            BindTo(x => x.IsObstacle, ColorConstants.ObstacleVertexColor,
                ColorConstants.RegularVertexColor, 0);
            BindTo(x => x.IsTarget, ColorConstants.TargetVertexColor);
            BindTo(x => x.IsSource, ColorConstants.SourceVertexColor);
            BindTo(x => x.IsTransit, ColorConstants.TranstiVertexColor);
            BindTo(x => x.IsPath, ColorConstants.PathVertexColor);
            BindTo(x => x.IsVisited, ColorConstants.VisitedVertexColor);
            BindTo(x => x.IsEnqueued, ColorConstants.EnqueuedVertexColor);
            BindTo(x => x.IsCrossedPath, ColorConstants.CrossedPathColor);

            Data = model;
        }

        private ColorScheme Create(Color foreground)
        {
            var driver = Application.Driver;
            var attribute = driver.MakeAttribute(foreground, ColorConstants.BackgroundColor);
            return new() { Normal = attribute };
        }

        private void BindTo(Expression<Func<RunVertexModel, bool>> expression,
            Color toColor, Color falseColor = ColorConstants.RegularVertexColor, int toSkip = 1)
        {
            model.WhenAnyValue(expression)
               .Skip(toSkip)
               .Select(x => x ? Create(toColor) : Create(falseColor))
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
