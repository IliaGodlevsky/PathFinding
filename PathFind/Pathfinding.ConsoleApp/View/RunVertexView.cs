using Pathfinding.ConsoleApp.Model;
using ReactiveUI;
using System;
using System.Linq.Expressions;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed class RunVertexView : VertexView<RunVertexModel>
    {
        private readonly CompositeDisposable disposables = new();

        public RunVertexView(RunVertexModel model) : base(model)
        {
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
