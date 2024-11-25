using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using NStack;
using Pathfinding.ConsoleApp.Extensions;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.ViewModel.Interface;
using Pathfinding.Domain.Core;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class HeuristicsView : FrameView
    {
        private const double DefaultWeight = 1;

        private static readonly InclusiveValueRange<double> WeightRange = (5, 0);

        private readonly ustring[] radioLabels;
        private readonly CompositeDisposable disposables = new();
        private readonly IRequireHeuristicsViewModel heuristicsViewModel;

        public HeuristicsView(
            [KeyFilter(KeyFilters.Views)] IMessenger messenger,
            IRequireHeuristicsViewModel heuristicsViewModel)
        {
            Initialize();
            var heurs = Enum.GetValues(typeof(HeuristicFunctions))
                .Cast<HeuristicFunctions>()
                .ToDictionary(x => x.ToStringRepresentation());
            var labels = heurs.Keys.Select(ustring.Make).ToArray();
            var values = labels.Select(x => heurs[x.ToString()]).ToList();
            radioLabels = labels;
            heuristics.RadioLabels = radioLabels;
            heuristics.Events()
               .SelectedItemChanged
               .Where(x => x.SelectedItem > -1)
               .Select(x =>values[x.SelectedItem])
               .BindTo(heuristicsViewModel, x => x.Heuristic)
               .DisposeWith(disposables);
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
                .BindTo(heuristicsViewModel, x => x.Weight)
                .DisposeWith(disposables);
            messenger.Register<OpenHeuristicsViewMessage>(this, OnOpen);
            messenger.Register<CloseHeuristicsViewMessage>(this, OnHeuristicsViewClosed);
            messenger.Register<CloseAlgorithmCreationViewMessage>(this, OnRunCreationViewClosed);
            this.heuristicsViewModel = heuristicsViewModel;
        }

        private void OnOpen(object recipient, OpenHeuristicsViewMessage msg)
        {
            heuristicsViewModel.Weight = DefaultWeight;
            weightTextField.Text = DefaultWeight.ToString();
            heuristics.SelectedItem = 0;
            Visible = true;
        }

        private void OnHeuristicsViewClosed(object recipient, CloseHeuristicsViewMessage msg)
        {
            heuristicsViewModel.Weight = DefaultWeight;
            weightTextField.Text = DefaultWeight.ToString();
            heuristicsViewModel.Heuristic = default;
            Visible = false;
        }

        private void OnRunCreationViewClosed(object recipient, CloseAlgorithmCreationViewMessage msg)
        {
            heuristicsViewModel.Weight = DefaultWeight;
            weightTextField.Text = DefaultWeight.ToString();
            heuristicsViewModel.Heuristic = default;
            Visible = false;
        }
    }
}
