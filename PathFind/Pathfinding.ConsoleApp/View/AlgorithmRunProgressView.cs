using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.ViewModel.Interface;
using System.Reactive.Disposables;
using ReactiveMarbles.ObservableEvents;
using Terminal.Gui;
using System.Reactive.Linq;
using ReactiveUI;
using System.ComponentModel;
using Pathfinding.ConsoleApp.Messages.View;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class AlgorithmRunProgressView : FrameView, IReactiveObject
    {
        private const float FractionPerWheel = 0.015f; // 1,5%
        private const float ExtraFractionPerWheel = FractionPerWheel * 2.5f;

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

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
                .Do(x => Fraction -= FractionPerWheel)
                .Select(x => FractionPerWheel)
                .InvokeCommand(viewModel, x => x.ReverseNextCommand)
                .DisposeWith(disposables);
            leftLabel.Events().MouseClick
                .Where(x => x.MouseEvent.Flags.HasFlag(MouseFlags.Button1Pressed)
                    && x.MouseEvent.Flags.HasFlag(MouseFlags.ButtonCtrl))
                .Do(x => Fraction -= ExtraFractionPerWheel)
                .Select(x => ExtraFractionPerWheel)
                .InvokeCommand(viewModel, x => x.ReverseNextCommand)
                .DisposeWith(disposables);
            leftLabel.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.WheeledDown)
                .Do(x => Fraction -= FractionPerWheel)
                .Select(x => FractionPerWheel)
                .InvokeCommand(viewModel, x => x.ReverseNextCommand)
                .DisposeWith(disposables);
            leftLabel.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.WheeledUp)
                .Do(x => Fraction += FractionPerWheel)
                .Select(x => FractionPerWheel)
                .InvokeCommand(viewModel, x => x.ProcessNextCommand)
                .DisposeWith(disposables);
            leftLabel.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button2Clicked)
                .Do(x => Fraction = 0)
                .Select(x => Fraction)
                .InvokeCommand(viewModel, x => x.ProcessToCommand)
                .DisposeWith(disposables);
            rightLabel.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Pressed)
                .Do(x => Fraction += FractionPerWheel)
                .Select(x => FractionPerWheel)
                .InvokeCommand(viewModel, x => x.ProcessNextCommand)
                .DisposeWith(disposables);
            rightLabel.Events().MouseClick
                .Where(x => x.MouseEvent.Flags.HasFlag(MouseFlags.Button1Pressed)
                    && x.MouseEvent.Flags.HasFlag(MouseFlags.ButtonCtrl))
                .Do(x => Fraction += ExtraFractionPerWheel)
                .Select(x => ExtraFractionPerWheel)
                .InvokeCommand(viewModel, x => x.ProcessNextCommand)
                .DisposeWith(disposables);
            rightLabel.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.WheeledDown)
                .Do(x => Fraction -= FractionPerWheel)
                .Select(x => FractionPerWheel)
                .InvokeCommand(viewModel, x => x.ReverseNextCommand)
                .DisposeWith(disposables);
            rightLabel.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.WheeledUp)
                .Do(x => Fraction += FractionPerWheel)
                .Select(x => FractionPerWheel)
                .InvokeCommand(viewModel, x => x.ProcessNextCommand)
                .DisposeWith(disposables);
            rightLabel.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button2Clicked)
                .Do(x => Fraction = 1)
                .Select(x => Fraction)
                .InvokeCommand(viewModel, x => x.ProcessToCommand)
                .DisposeWith(disposables);
            bar.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .Select(x => (float)(x.MouseEvent.X + 1) / bar.Bounds.Width)
                .Do(x => Fraction = x)
                .Select(x => Fraction)
                .InvokeCommand(viewModel, x => x.ProcessToCommand)
                .DisposeWith(disposables);
            viewModel.WhenAnyValue(x => x.Fraction)
                .BindTo(bar, x => x.Fraction)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.Fraction)
                .BindTo(viewModel, x => x.Fraction)
                .DisposeWith(disposables);
        }

        public void RaisePropertyChanging(PropertyChangingEventArgs args)
        {
            PropertyChanging?.Invoke(this, args);
        }

        public void RaisePropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
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
