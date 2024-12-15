using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.ViewModel.Interface;
using System.Reactive.Disposables;
using ReactiveMarbles.ObservableEvents;
using Terminal.Gui;
using System.Reactive.Linq;
using ReactiveUI;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.Shared.Primitives;
using Pathfinding.Shared.Extensions;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class AlgorithmRunProgressView : FrameView
    {
        private static readonly InclusiveValueRange<float> FractionRange = new(1, 0);

        private const float FractionPerWheel = 0.015f;
        private const float ExtraFractionPerWheel = FractionPerWheel * 3;

        private readonly CompositeDisposable disposables = new();

        public AlgorithmRunProgressView(
            [KeyFilter(KeyFilters.Views)] IMessenger messenger,
            IAlgorithmRunFieldViewModel viewModel)
        {
            Initialize();
            messenger.Register<CloseAlgorithmRunFieldViewMessage>(this, OnAlgorithmFieldClosed);
            messenger.Register<OpenAlgorithmRunViewMessage>(this, OnOpenAlgorithmField);
            leftLabel.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Pressed)
                .Select(x => FractionRange.ReturnInRange(bar.Fraction - FractionPerWheel))
                .Do(x => bar.Fraction = x)
                .BindTo(viewModel, x => x.SelectedRun.Fraction)
                .DisposeWith(disposables);
            leftLabel.Events().MouseClick
                .Where(x => x.MouseEvent.Flags.HasFlag(MouseFlags.Button1Pressed)
                    && x.MouseEvent.Flags.HasFlag(MouseFlags.ButtonCtrl))
                .Select(x => FractionRange.ReturnInRange(bar.Fraction - ExtraFractionPerWheel))
                .Do(x => bar.Fraction = x)
                .BindTo(viewModel, x => x.SelectedRun.Fraction)
                .DisposeWith(disposables);
            leftLabel.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.WheeledDown)
                .Select(x => FractionRange.ReturnInRange(bar.Fraction - FractionPerWheel))
                .Do(x => bar.Fraction = x)
                .BindTo(viewModel, x => x.SelectedRun.Fraction)
                .DisposeWith(disposables);
            leftLabel.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.WheeledUp)
                .Select(x => FractionRange.ReturnInRange(bar.Fraction + FractionPerWheel))
                .Do(x => bar.Fraction = x)
                .BindTo(viewModel, x => x.SelectedRun.Fraction)
                .DisposeWith(disposables);
            leftLabel.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button2Clicked)
                .Select(x => FractionRange.LowerValueOfRange)
                .BindTo(viewModel, x => x.SelectedRun.Fraction)
                .DisposeWith(disposables);
            rightLabel.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Pressed)
                .Select(x => FractionRange.ReturnInRange(bar.Fraction + FractionPerWheel))
                .Do(x => bar.Fraction = x)
                .BindTo(viewModel, x => x.SelectedRun.Fraction)
                .DisposeWith(disposables);
            rightLabel.Events().MouseClick
                .Where(x => x.MouseEvent.Flags.HasFlag(MouseFlags.Button1Pressed)
                    && x.MouseEvent.Flags.HasFlag(MouseFlags.ButtonCtrl))
                .Select(x => FractionRange.ReturnInRange(bar.Fraction + ExtraFractionPerWheel))
                .Do(x => bar.Fraction = x)
                .BindTo(viewModel, x => x.SelectedRun.Fraction)
                .DisposeWith(disposables);
            rightLabel.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.WheeledDown)
                .Select(x => FractionRange.ReturnInRange(bar.Fraction - FractionPerWheel))
                .Do(x => bar.Fraction = x)
                .BindTo(viewModel, x => x.SelectedRun.Fraction)
                .DisposeWith(disposables);
            rightLabel.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.WheeledUp)
                .Select(x => FractionRange.ReturnInRange(bar.Fraction + FractionPerWheel))
                .Do(x => bar.Fraction = x)
                .BindTo(viewModel, x => x.SelectedRun.Fraction)
                .DisposeWith(disposables);
            rightLabel.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button2Clicked)
                .Select(x => FractionRange.UpperValueOfRange)
                .Do(x => bar.Fraction = x)
                .BindTo(viewModel, x => x.SelectedRun.Fraction)
                .DisposeWith(disposables);
            bar.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Select(x => (float)(x.MouseEvent.X + 1) / bar.Bounds.Width)
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
