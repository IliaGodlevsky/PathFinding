using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;
using System;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Infrastructure.Data.Extensions;

namespace Pathfinding.ConsoleApp.View
{
    public class VertexView : Label
    {
        private const int LabelWidth = 3;

        private readonly CompositeDisposable disposables;

        public VertexView(VertexModel viewModel)
        {
            disposables = new CompositeDisposable();
            viewModel.WhenAnyValue(x => x.IsObstacle)
                .Select(x => x 
                    ? Create(ColorContants.ObstacleVertexColor) 
                    : Create(ColorContants.RegularVertexColor))
                .BindTo(this, x => x.ColorScheme)
                .DisposeWith(disposables);
            viewModel.WhenAnyValue(x => x.Cost)
                .Select(x => x.CurrentCost.ToString())
                .Do(x => Text = x)
                .Subscribe()
                .DisposeWith(disposables);
            X = viewModel.Position.GetX() * LabelWidth;
            Y = viewModel.Position.GetY();
            Width = LabelWidth;
            Data = viewModel;
        }

        public void VisualizeAsSource()
        {
            ColorScheme = Create(ColorContants.SourceVertexColor);
        }

        public void VisualizeAsTarget()
        {
            ColorScheme = Create(ColorContants.TargetVertexColor);
        }

        public void VisualizeAsRegular()
        {
            ColorScheme = Create(ColorContants.RegularVertexColor);
        }

        public void VisualizeAsTransit()
        {
            ColorScheme = Create(ColorContants.TranstiVertexColor);
        }

        private ColorScheme Create(Color foreground)
        {
            return new() { Normal 
                = Application.Driver.MakeAttribute(foreground, ColorContants.BackgroundColor) };
        }

        protected override void Dispose(bool disposing)
        {
            disposables.Dispose();
            base.Dispose(disposing);
        }
    }
}
