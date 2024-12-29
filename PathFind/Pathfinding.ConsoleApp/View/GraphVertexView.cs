using Pathfinding.ConsoleApp.Model;
using ReactiveUI;
using System;
using System.Linq.Expressions;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed class GraphVertexView : VertexView<GraphVertexModel>
    {
        private readonly CompositeDisposable disposables = new();

        public GraphVertexView(GraphVertexModel model)
            : base(model)
        {
            BindTo(x => x.IsTarget, TargetColor, RegularColor);
            BindTo(x => x.IsSource, SourceColor, RegularColor);
            BindTo(x => x.IsTransit, TransitColor, RegularColor);
            BindTo(x => x.IsObstacle, ObstacleColor, RegularColor);

            model.WhenAnyValue(x => x.Cost)
                .Select(x => x.CurrentCost.ToString())
                .Do(x => Text = x)
                .Subscribe()
                .DisposeWith(disposables);
        }

        private void BindTo(Expression<Func<GraphVertexModel, bool>> expression, ColorScheme toColor,
            ColorScheme falseColor)
        {
            model.WhenAnyValue(expression)
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
