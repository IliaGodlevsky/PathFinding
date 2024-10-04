using Pathfinding.ConsoleApp.Model;
using Pathfinding.Infrastructure.Data.Extensions;
using ReactiveUI;
using System;
using System.Linq.Expressions;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    public class VertexView : Label
    {
        private const int LabelWidth = 3;

        private readonly CompositeDisposable disposables;

        private readonly VertexModel model;

        public VertexView(VertexModel model)
        {
            disposables = new CompositeDisposable();
            X = model.Position.GetX() * LabelWidth;
            Y = model.Position.GetY();
            Width = LabelWidth;
            Data = model;
            this.model = model;
            model.WhenAnyValue(x => x.IsObstacle)
               .Select(x => x 
                            ? Create(ColorContants.ObstacleVertexColor) 
                            : Create(ColorContants.RegularVertexColor))
               .BindTo(this, x => x.ColorScheme)
               .DisposeWith(disposables);
            BindTo(x => x.IsRegular, ColorContants.RegularVertexColor);
            BindTo(x => x.IsTarget, ColorContants.TargetVertexColor);
            BindTo(x => x.IsSource, ColorContants.SourceVertexColor);
            BindTo(x => x.IsTransit, ColorContants.TranstiVertexColor);
            
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
                Normal = Application.Driver.MakeAttribute(foreground, ColorContants.BackgroundColor)
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
