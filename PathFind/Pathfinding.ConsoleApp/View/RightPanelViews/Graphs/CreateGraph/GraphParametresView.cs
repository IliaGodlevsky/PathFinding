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
    internal sealed partial class GraphParametresView : FrameView
    {
        private static readonly InclusiveValueRange<int> WidthRange = (58, 1);
        private static readonly InclusiveValueRange<int> LengthRange = (49, 1);
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
            VisibleChanged += OnVisibilityChanged;
        }

        private void OnVisibilityChanged()
        {
            if (Visible)
            {
                graphWidthInput.Text = string.Empty;
                graphLengthInput.Text = string.Empty;
                obstaclesInput.Text = string.Empty;
            }
        }

        private void BindTo(TextField field,
            Expression<Func<CreateGraphViewModel, int>> expression,
            InclusiveValueRange<int> range)
        {
            field.Events()
                .TextChanging
                .Select(x =>
                {
                    if (string.IsNullOrEmpty(x.NewText.ToString()))
                    {
                        x.NewText = string.Empty;
                        return -1;
                    }
                    else if (!int.TryParse(x.NewText.ToString(), out var value))
                    {
                        x.Cancel = true;
                        return -1;
                    }
                    else
                    {
                        var returned = range.ReturnInRange(value);
                        x.NewText = returned.ToString();
                        return returned;
                    }
                })
                .BindTo(viewModel, expression)
                .DisposeWith(disposables);
        }
    }
}
