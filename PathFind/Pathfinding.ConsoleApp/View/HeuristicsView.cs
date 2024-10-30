using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using NStack;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class HeuristicsView : FrameView
    {
        private static readonly InclusiveValueRange<double> WeightRange = (5, 0);
        private const double DefaultWeight = 1;

        private readonly HeuristicsViewModel viewModel;
        private readonly ustring[] radioLabels;

        private readonly CompositeDisposable disposables = new();

        public HeuristicsView([KeyFilter(KeyFilters.Views)] IMessenger messenger,
            HeuristicsViewModel viewModel)
        {
            Initialize();
            this.viewModel = viewModel;
            radioLabels = viewModel.Heuristics.Keys.Select(ustring.Make).ToArray();
            heuristics.RadioLabels = radioLabels;
            messenger.Register<HeuristicsViewModelChangedMessage>(this, OnViewModelChanged);
            messenger.Register<OpenHeuristicsViewMessage>(this, OnOpen);
            messenger.Register<CloseHeuristicsViewMessage>(this, OnHeuristicsViewClosed);
            messenger.Register<CloseAlgorithmCreationViewMessage>(this, OnRunCreationViewClosed);
        }

        private void OnViewModelChanged(object recipient, HeuristicsViewModelChangedMessage msg)
        {
            var heuristic = viewModel.Heuristics;
            disposables.Clear();
            heuristics.Events()
               .SelectedItemChanged
               .Where(x => x.SelectedItem > -1)
               .Select(x => x.SelectedItem)
               .Select(x => (Name: radioLabels[x].ToString(),
                        Rule: heuristic[radioLabels[x].ToString()]))
               .BindTo(msg.ViewModel, x => x.Heuristic)
               .DisposeWith(disposables);
            heuristics.SelectedItem = 0;
            weightTextField.Events()
                .TextChanging
                .Select(x =>
                {
                    if (string.IsNullOrEmpty(x.NewText.ToString()))
                    {
                        return -1;
                    }
                    else if (double.TryParse(x.NewText.ToString(), out var value)
                        && !WeightRange.Contains(value))
                    {
                        var returned = WeightRange.ReturnInRange(value);
                        x.NewText = returned.ToString();
                        return returned;
                    }
                    else if (double.TryParse(x.NewText.ToString(), out var val))
                    {
                        return val;
                    }
                    return -1;
                })
                .BindTo(msg.ViewModel, x => x.Weight)
                .DisposeWith(disposables);
            msg.ViewModel.Weight = DefaultWeight;
            weightTextField.Text = DefaultWeight.ToString();
        }

        private void OnOpen(object recipient, OpenHeuristicsViewMessage msg)
        {
            Visible = true;
        }

        private void OnHeuristicsViewClosed(object recipient, CloseHeuristicsViewMessage msg)
        {
            disposables.Clear();
            Visible = false;
        }

        private void OnRunCreationViewClosed(object recipient, CloseAlgorithmCreationViewMessage msg)
        {
            disposables.Clear();
            Visible = false;
        }
    }
}
