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
    public class VertexView : Label
    {
        private const int LabelWidth = 3;

        private readonly CompositeDisposable disposables = new();
        private readonly VertexModel model;

        public VertexView(VertexModel model)
        {
            X = model.Position.GetX() * LabelWidth;
            Y = model.Position.GetY();
            Width = LabelWidth;
            Data = model;
            this.model = model;
            model.WhenAnyValue(x => x.IsObstacle)
               .Select(x => x
                            ? Create(ColorConstants.ObstacleVertexColor)
                            : Create(ColorConstants.RegularVertexColor))
               .BindTo(this, x => x.ColorScheme)
               .DisposeWith(disposables);
            BindTo(x => x.IsRegular, ColorConstants.RegularVertexColor);
            BindTo(x => x.IsTarget, ColorConstants.TargetVertexColor);
            BindTo(x => x.IsSource, ColorConstants.SourceVertexColor);
            BindTo(x => x.IsTransit, ColorConstants.TranstiVertexColor);

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

        private void BindTo(Expression<Func<VertexModel, bool>> expression, Color toColor)
        {
            model.WhenAnyValue(expression)
               .Where(x => x)
               .Select(x => Create(toColor))
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
