using Pathfinding.ConsoleApp.ViewModel.GraphCreateViewModels;
using System.Reactive.Disposables;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Linq;
using ReactiveUI;
using Terminal.Gui;
using System.Linq.Expressions;
using System;
using Pathfinding.Shared.Primitives;
using Pathfinding.Shared.Extensions;
using NStack;
using System.Windows;

namespace Pathfinding.ConsoleApp.View.GraphCreateViews
{
    internal sealed partial class GraphParametresView : Terminal.Gui.FrameView
    {
        private static readonly InclusiveValueRange<int> WidthRange = (75, 1);
        private static readonly InclusiveValueRange<int> LengthRange = (50, 1);
        private static readonly InclusiveValueRange<int> ObstaclesRange = (99, 1);

        private readonly GraphParametresViewModel viewModel;
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        public GraphParametresView(GraphParametresViewModel viewModel)
        {
            this.viewModel = viewModel;
            Initialize();
            BindTo(obstaclesInput, x => x.Obstacles);
            BindTo(graphWidthInput, x => x.Width);
            BindTo(graphLengthInput, x => x.Length);
        }

        private void BindTo(TextField field, Expression<Func<GraphParametresViewModel, int>> expression)
        {
            var u = ustring.Make("Hello");
            var s = u.ToString();
            field.Events()
                .TextChanged
                .Select(_ => field.Text)
                .Where(x => int.TryParse(x.ToString(), out _))
                .Select(x => int.Parse(x.ToString()))
                .BindTo(viewModel, expression)
                .DisposeWith(disposables);

            //field.WhenAnyValue(x => x.Text)
            //    .Where(x => int.TryParse(x.ToString(), out _))
            //    .Select(x => int.Parse(x.ToString()))
            //    .BindTo(viewModel, expression)
            //    .DisposeWith(disposables);
        }
    }
}
