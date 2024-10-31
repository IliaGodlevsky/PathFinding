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
        private const int LabelWidth = 3;

        private readonly CompositeDisposable disposables = new();
        private readonly GraphVertexModel model;

        public GraphVertexView(GraphVertexModel model)
        {
            X = model.Position.GetX() * LabelWidth;
            Y = model.Position.GetY();
            Width = LabelWidth;
            this.model = model;
            BindTo(x => x.IsObstacle, ColorConstants.ObstacleVertexColor,
                ColorConstants.RegularVertexColor, 0);
            BindTo(x => x.IsTarget, ColorConstants.TargetVertexColor,
                ColorConstants.RegularVertexColor, 1);
            BindTo(x => x.IsSource, ColorConstants.SourceVertexColor, 
                ColorConstants.RegularVertexColor, 1);
            BindTo(x => x.IsTransit, ColorConstants.TranstiVertexColor,
                ColorConstants.RegularVertexColor, 1);

            model.WhenAnyValue(x => x.Cost)
                .Select(x => x.CurrentCost.ToString())
                .Do(x => Text = x)
                .Subscribe()
                .DisposeWith(disposables);
        }

        private ColorScheme Create(Color foreground)
        {
            return new()
            {
                Normal = Application.Driver.MakeAttribute(foreground, ColorConstants.BackgroundColor)
            };
        }

        private void BindTo(Expression<Func<GraphVertexModel, bool>> expression, Color toColor, 
            Color falseColor, int skip)
        {
            model.WhenAnyValue(expression)
               .Skip(skip)
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
