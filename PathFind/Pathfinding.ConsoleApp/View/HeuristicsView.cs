using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using NStack;
using Pathfinding.ConsoleApp.Extensions;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.ViewModel.Interface;
using Pathfinding.Domain.Core;
using Pathfinding.Shared.Extensions;
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
            heuristics.Events().SelectedItemChanged
               .Where(x => x.SelectedItem > -1)
               .Select(x => values[x.SelectedItem])
               .BindTo(heuristicsViewModel, x => x.Heuristic)
               .DisposeWith(disposables);
            messenger.Register<OpenHeuristicsViewMessage>(this, OnOpen);
            messenger.Register<CloseHeuristicsViewMessage>(this, OnHeuristicsViewClosed);
            messenger.Register<CloseAlgorithmCreationViewMessage>(this, OnRunCreationViewClosed);
            this.heuristicsViewModel = heuristicsViewModel;
        }

        private void OnOpen(object recipient, OpenHeuristicsViewMessage msg)
        {
            heuristics.SelectedItem = 0;
            
            Visible = true;
        }

        private void OnHeuristicsViewClosed(object recipient, CloseHeuristicsViewMessage msg)
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
            heuristicsViewModel.Heuristic = null;
            heuristicsViewModel.ToWeight = null;
            heuristicsViewModel.Step = null;
            Visible = false;
        }
    }
}
