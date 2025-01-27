using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Injection;
using Pathfinding.App.Console.Messages.View;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.ViewModel.Interface;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal sealed partial class AlgorithmRunProgressView : FrameView
    {
        private const float FractionPerClick = 0.015f;
        private const float ExtraFractionPerClick = FractionPerClick * 3;

        private readonly CompositeDisposable disposables = new();

        public AlgorithmRunProgressView(
            [KeyFilter(KeyFilters.Views)] IMessenger messenger,
            IAlgorithmRunFieldViewModel viewModel)
        {
            Initialize();
            messenger.Register<CloseAlgorithmRunFieldViewMessage>(this, OnAlgorithmFieldClosed);
            messenger.Register<OpenAlgorithmRunViewMessage>(this, OnOpenAlgorithmField);
            var leftLabelMouseEvent = leftLabel.Events().MouseClick
                .Where(x => viewModel.SelectedRun != AlgorithmRevisionModel.Empty);
            var rightLabelMouseEvent = rightLabel.Events().MouseClick
                .Where(x => viewModel.SelectedRun != AlgorithmRevisionModel.Empty);
            leftLabelMouseEvent
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Pressed)
                .Select(x => bar.Fraction - FractionPerClick)
                .BindTo(viewModel, x => x.SelectedRun.Fraction)
                .DisposeWith(disposables);
            leftLabelMouseEvent
                .Where(x => x.MouseEvent.Flags.HasFlag(MouseFlags.Button1Pressed)
                    && x.MouseEvent.Flags.HasFlag(MouseFlags.ButtonCtrl))
                .Select(x => bar.Fraction - ExtraFractionPerClick)
                .BindTo(viewModel, x => x.SelectedRun.Fraction)
                .DisposeWith(disposables);
            leftLabelMouseEvent
                .Where(x => x.MouseEvent.Flags == MouseFlags.WheeledDown)
                .Select(x => bar.Fraction - FractionPerClick)
                .BindTo(viewModel, x => x.SelectedRun.Fraction)
                .DisposeWith(disposables);
            leftLabelMouseEvent
                .Where(x => x.MouseEvent.Flags == MouseFlags.WheeledUp)
                .Select(x => bar.Fraction + FractionPerClick)
                .BindTo(viewModel, x => x.SelectedRun.Fraction)
                .DisposeWith(disposables);
            leftLabelMouseEvent
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button2Clicked)
                .Select(x => AlgorithmRevisionModel.FractionRange.LowerValueOfRange)
                .BindTo(viewModel, x => x.SelectedRun.Fraction)
                .DisposeWith(disposables);
            rightLabelMouseEvent
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Pressed)
                .Select(x => bar.Fraction + FractionPerClick)
                .BindTo(viewModel, x => x.SelectedRun.Fraction)
                .DisposeWith(disposables);
            rightLabelMouseEvent
                .Where(x => x.MouseEvent.Flags.HasFlag(MouseFlags.Button1Pressed)
                    && x.MouseEvent.Flags.HasFlag(MouseFlags.ButtonCtrl))
                .Select(x => bar.Fraction + ExtraFractionPerClick)
                .BindTo(viewModel, x => x.SelectedRun.Fraction)
                .DisposeWith(disposables);
            rightLabelMouseEvent
                .Where(x => x.MouseEvent.Flags == MouseFlags.WheeledDown)
                .Select(x => bar.Fraction - FractionPerClick)
                .BindTo(viewModel, x => x.SelectedRun.Fraction)
                .DisposeWith(disposables);
            rightLabelMouseEvent
                .Where(x => x.MouseEvent.Flags == MouseFlags.WheeledUp)
                .Select(x => bar.Fraction + FractionPerClick)
                .BindTo(viewModel, x => x.SelectedRun.Fraction)
                .DisposeWith(disposables);
            rightLabelMouseEvent
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button2Clicked)
                .Select(x => AlgorithmRevisionModel.FractionRange.UpperValueOfRange)
                .BindTo(viewModel, x => x.SelectedRun.Fraction)
                .DisposeWith(disposables);
            bar.Events().MouseClick
                .Where(x => viewModel.SelectedRun != AlgorithmRevisionModel.Empty)
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Select(x => (float)Math.Round((float)(x.MouseEvent.X + 1) / bar.Bounds.Width, 3))
                .BindTo(viewModel, x => x.SelectedRun.Fraction)
                .DisposeWith(disposables);
            viewModel.WhenAnyValue(x => x.SelectedRun.Fraction)
                .BindTo(bar, x => x.Fraction)
                .DisposeWith(disposables);
        }

        private void OnAlgorithmFieldClosed(object recipient, CloseAlgorithmRunFieldViewMessage msg)
        {
            rightLabel.Visible = false;
            leftLabel.Visible = false;
            bar.Visible = false;
        }

        private void OnOpenAlgorithmField(object recipient, OpenAlgorithmRunViewMessage msg)
        {
            rightLabel.Visible = true;
            leftLabel.Visible = true;
            bar.Visible = true;
        }

        protected override void Dispose(bool disposing)
        {
            disposables.Dispose();
            base.Dispose(disposing);
        }
    }
}
