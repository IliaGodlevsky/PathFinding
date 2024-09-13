using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using NStack;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using System.Linq;
using System.Reactive.Disposables;
using Terminal.Gui;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Linq;
using ReactiveUI;
using Pathfinding.ConsoleApp.ViewModel;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class HeuristicsView : FrameView
    {
        private readonly HeuristicsViewModel viewModel;
        private readonly ustring[] radioLabels;

        private readonly CompositeDisposable disposables = new();

        public HeuristicsView([KeyFilter(KeyFilters.Views)]IMessenger messenger,
            HeuristicsViewModel viewModel) 
        {
            Initialize();
            this.viewModel = viewModel;
            var heuristicsNames = viewModel.Heuristics.Keys.ToArray();
            radioLabels = heuristicsNames.Select(x => ustring.Make(x)).ToArray();
            heuristics.RadioLabels = radioLabels;
            messenger.Register<HeuristicsViewModelChangedMessage>(this, OnViewModelChanged);
            messenger.Register<OpenHeuristicsViewMessage>(this, OnOpen);
            messenger.Register<CloseHeuristicsViewMessage>(this, OnHeuristicsViewClosed);
            messenger.Register<CloseRunCreationViewMessage>(this, OnRunCreationViewClosed);
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

        private void OnRunCreationViewClosed(object recipient, CloseRunCreationViewMessage msg)
        {
            disposables.Clear();
            Visible = false;
        }
    }
}
