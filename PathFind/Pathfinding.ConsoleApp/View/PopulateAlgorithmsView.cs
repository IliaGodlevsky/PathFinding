using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.ViewModel.Interface;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class PopulateAlgorithmsView : FrameView
    {
        private const double DefaultWeight = 1;
        private static readonly InclusiveValueRange<double> WeightRange = (5, 0);

        private readonly CompositeDisposable disposables = new();
        private readonly IRequireHeuristicsViewModel heuristicsViewModel;

        public PopulateAlgorithmsView(
            [KeyFilter(KeyFilters.Views)] IMessenger messenger,
            IRequireHeuristicsViewModel heuristicsViewModel)
        {
            Initialize();
            weightTextField.Events().TextChanging
                .Select(x =>
                {
                    double value;
                    if (string.IsNullOrEmpty(x.NewText.ToString()))
                    {
                        return -1;
                    }
                    else if (double.TryParse(x.NewText.ToString(), out value))
                    {
                        if (!WeightRange.Contains(value))
                        {
                            value = WeightRange.ReturnInRange(value);
                            x.NewText = value.ToString();
                        }
                        return value;
                    }
                    return -1;
                })
                .BindTo(heuristicsViewModel, x => x.FromWeight)
                .DisposeWith(disposables);
            toWeightTextField.Events().TextChanging
                .Select(x =>
                {
                    double value;
                    if (string.IsNullOrEmpty(x.NewText.ToString()))
                    {
                        return -1;
                    }
                    else if (double.TryParse(x.NewText.ToString(), out value))
                    {
                        if (value == -1)
                        {
                            x.NewText = string.Empty;
                            return -1;
                        }
                        if (!WeightRange.Contains(value))
                        {
                            value = WeightRange.ReturnInRange(value);
                            x.NewText = value.ToString();
                        }
                        if (double.TryParse(weightTextField.Text.ToString(), out var weight)
                            && value < weight)
                        {
                            value = weight;
                            x.NewText = value.ToString();
                        }
                    }
                    return value;
                })
                .BindTo(heuristicsViewModel, x => x.ToWeight)
                .DisposeWith(disposables);
            toWeightTextField.Events()
                .TextChanged
                .Do(x =>
                {
                    if (double.TryParse(weightTextField.Text.ToString(), out var weight)
                        && double.TryParse(toWeightTextField.Text.ToString(), out var toWeight))
                    {
                        var parsed = double.TryParse(stepTextField.Text.ToString(), out var step);
                        if (parsed && step == 0 && toWeight > weight)
                        {
                            double value = toWeight - weight;
                            var round = Math.Round(value, 3);
                            stepTextField.Text = round.ToString();
                        }
                        else if (toWeight - weight < step || toWeight == weight)
                        {
                            stepTextField.Text = (toWeight - weight).ToString();
                        }
                    }
                })
                .Subscribe()
                .DisposeWith(disposables);
            weightTextField.Events()
                .TextChanged
                .Do(x =>
                {
                    if (double.TryParse(weightTextField.Text.ToString(), out var weight)
                        && double.TryParse(toWeightTextField.Text.ToString(), out var toWeight))
                    {
                        var parsed = double.TryParse(stepTextField.Text.ToString(), out var step);
                        if (weight >= toWeight)
                        {
                            toWeightTextField.Text = weightTextField.Text;
                            stepTextField.Text = 0.ToString();
                        }
                        else if (parsed && step == 0 || toWeight - weight < step)
                        {
                            double value = toWeight - weight;
                            var round = Math.Round(value, 3);
                            stepTextField.Text = round.ToString();
                        }
                    }
                })
                .Subscribe()
                .DisposeWith(disposables);
            stepTextField.Events().TextChanging
                .Select(x =>
                {
                    if (string.IsNullOrEmpty(x.NewText.ToString()))
                    {
                        return -1;
                    }
                    else if (double.TryParse(x.NewText.ToString(), out var value)
                        && !WeightRange.Contains(value))
                    {
                        if (value == -1)
                        {
                            x.NewText = string.Empty;
                            return -1;
                        }

                        var returned = WeightRange.ReturnInRange(value);
                        x.NewText = returned.ToString();
                        return returned;
                    }
                    else if (double.TryParse(x.NewText.ToString(), out var val))
                    {
                        if (double.TryParse(weightTextField.Text.ToString(), out var weight)
                            && double.TryParse(toWeightTextField.Text.ToString(), out var toWeight))
                        {
                            var range = Math.Round(toWeight - weight, 3);
                            if (val > range)
                            {
                                val = range;
                                x.NewText = val.ToString();
                            }
                        }
                        return val;
                    }
                    return -1;
                })
                .BindTo(heuristicsViewModel, x => x.Step)
                .DisposeWith(disposables);
            messenger.Register<OpenAlgorithmsPopulateViewMessage>(this, OnOpen);
            messenger.Register<CloseAlgorithmsPopulateViewMessage>(this, OnViewClosed);
            messenger.Register<CloseAlgorithmCreationViewMessage>(this, OnRunCreationViewClosed);
            messenger.Register<OpenHeuristicsViewMessage>(this, OnHeuristicsOpen);
            this.heuristicsViewModel = heuristicsViewModel;
        }

        private void OnHeuristicsOpen(object recipient, OpenHeuristicsViewMessage msg)
        {
            heuristicsViewModel.FromWeight = DefaultWeight;
            heuristicsViewModel.ToWeight = DefaultWeight;
            heuristicsViewModel.Step = 0;
        }

        private void OnOpen(object recipient, OpenAlgorithmsPopulateViewMessage msg)
        {
            weightTextField.Text = DefaultWeight.ToString();
            toWeightTextField.Text = DefaultWeight.ToString();
            stepTextField.Text = 0.ToString();
            heuristicsViewModel.FromWeight = DefaultWeight;
            heuristicsViewModel.ToWeight = DefaultWeight;
            heuristicsViewModel.Step = 0;
            Visible = true;
        }

        private void OnViewClosed(object recipient, CloseAlgorithmsPopulateViewMessage msg)
        {
            Close();
        }

        private void OnRunCreationViewClosed(object recipient, CloseAlgorithmCreationViewMessage msg)
        {
            Close();
        }

        private void Close()
        {
            heuristicsViewModel.FromWeight = null;
            heuristicsViewModel.ToWeight = null;
            heuristicsViewModel.Step = null;
            Visible = false;
        }
    }
}
