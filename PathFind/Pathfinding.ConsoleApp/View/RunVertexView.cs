using NStack;
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

            BindTo(x => x.IsObstacle, ColorContants.ObstacleVertexColor, 0);
            BindTo(x => x.IsTarget, ColorContants.TargetVertexColor);
            BindTo(x => x.IsSource, ColorContants.SourceVertexColor);
            BindTo(x => x.IsTransit, ColorContants.TranstiVertexColor);
            BindTo(x => x.IsPath, ColorContants.PathVertexColor);
            BindTo(x => x.IsVisited, ColorContants.VisitedVertexColor);
            BindTo(x => x.IsEnqueued, ColorContants.EnqueuedVertexColor);
            BindTo(x => x.IsCrossedPath, ColorContants.CrossedPathColor);
            model.WhenAnyValue(x => x.Cost)
                .Select(x => ustring.Make(x.ToString()))
                .BindTo(this, x => x.Text)
                .DisposeWith(disposables);
            Data = model;
        }

        private ColorScheme Create(Color foreground)
        {
            return new()
            {
                Normal
                = Application.Driver.MakeAttribute(foreground, ColorContants.BackgroundColor)
            };
        }

        private void BindTo(Expression<Func<RunVertexModel, bool>> expression,
            Color toColor, int toSkip = 1)
        {
            model.WhenAnyValue(expression)
               .Skip(toSkip)
               .Select(x => x ? Create(toColor) : Create(ColorContants.RegularVertexColor))
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
