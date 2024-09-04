using System.Reactive.Disposables;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Linq;
using ReactiveUI;
using Terminal.Gui;
using System.Linq.Expressions;
using System;
using Pathfinding.Shared.Primitives;
using Pathfinding.Shared.Extensions;
using Pathfinding.ConsoleApp.ViewModel;

namespace Pathfinding.ConsoleApp.View.GraphCreateViews
{
    internal sealed partial class GraphParametresView : Terminal.Gui.FrameView
    {
        private static readonly InclusiveValueRange<int> WidthRange = (58, 1);
        private static readonly InclusiveValueRange<int> LengthRange = (50, 1);
        private static readonly InclusiveValueRange<int> ObstaclesRange = (99, 0);

        private readonly CreateGraphViewModel viewModel;
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        public GraphParametresView(CreateGraphViewModel viewModel)
        {
            this.viewModel = viewModel;
            Initialize();
            BindTo(obstaclesInput, x => x.Obstacles, ObstaclesRange);
            BindTo(graphWidthInput, x => x.Width, WidthRange);
            BindTo(graphLengthInput, x => x.Length, LengthRange);
        }

        private void BindTo(TextField field,
            Expression<Func<CreateGraphViewModel, int>> expression,
            InclusiveValueRange<int> range)
        {
            field.Events()
                .TextChanging
                .Where(x => int.TryParse(x.NewText.ToString(), out _))
                .Do(x =>
                {
                    var value = x.NewText.ToString();
                    var parsed = int.Parse(value);
                    var returned = range.ReturnInRange(parsed);
                    x.NewText = returned.ToString();
                })
                .Select(x => int.Parse(x.NewText.ToString()))
                .BindTo(viewModel, expression)
                .DisposeWith(disposables);
        }
    }
}
