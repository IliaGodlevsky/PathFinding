using Pathfinding.ConsoleApp.ViewModel;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;
using System;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Core;
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
                .Select(x => x ? Create(Color.Black) : Create(Color.White))
                .BindTo(this, x => x.ColorScheme)
                .DisposeWith(disposables);
            viewModel.WhenAnyValue(x => x.Cost)
                .Select(x => x.CurrentCost.ToString())
                .Do(x => this.Text = x)
                .Subscribe()
                .DisposeWith(disposables);
            X = viewModel.Position.GetX() * LabelWidth;
            Y = viewModel.Position.GetY();
            Width = LabelWidth;
            Text = viewModel.Cost.CurrentCost.ToString();
            Data = viewModel;
        }

        public void VisualizeAsSource()
        {
            ColorScheme = Create(Color.BrightMagenta);
        }

        public void VisualizeAsTarget()
        {
            ColorScheme = Create(Color.BrightRed);
        }

        public void VisualizeAsRegular()
        {
            ColorScheme = Create(Color.White);
        }

        public void VisualizeAsTransit()
        {
            ColorScheme = Create(Color.Green);
        }

        public void VisualizeAsObstacle()
        {
            ColorScheme = Create(Color.Black);
        }

        private ColorScheme Create(Color foreground)
        {
            return new() { Normal 
                = Application.Driver.MakeAttribute(foreground, Color.Black) };
        }

        protected override void Dispose(bool disposing)
        {
            disposables.Dispose();
            base.Dispose(disposing);
        }
    }
}
