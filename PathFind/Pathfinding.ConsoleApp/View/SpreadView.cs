using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using NStack;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.ViewModel;
using System.Linq;
using System.Reactive.Disposables;
using Terminal.Gui;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Linq;
using ReactiveUI;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class SpreadView : FrameView
    {
        private readonly CompositeDisposable disposables = new();

        private readonly SpreadViewModel stepRulesViewModel;
        private readonly IMessenger messenger;
        private readonly ustring[] radioLabels;

        public SpreadView(SpreadViewModel stepRulesViewModel,
            [KeyFilter(KeyFilters.Views)] IMessenger messenger)
        {
            Initialize();
            this.stepRulesViewModel = stepRulesViewModel;
            this.messenger = messenger;
            spreadLevels.RadioLabels = stepRulesViewModel.SpreadLevels
                .Keys.Select(x => ustring.Make(x))
                .ToArray();
            radioLabels = spreadLevels.RadioLabels;
            messenger.Register<SpreadViewModelChangedMessage>(this, OnViewModelChanged);
            messenger.Register<OpenSpreadViewMessage>(this, OnOpen);
            messenger.Register<CloseSpreadViewMessage>(this, OnStepRulesViewClose);
            messenger.Register<CloseRunCreationViewMessage>(this, OnRunCreationViewClosed);
        }

        private void OnViewModelChanged(object recipient, SpreadViewModelChangedMessage msg)
        {
            var rules = stepRulesViewModel.SpreadLevels;
            disposables.Clear();
            spreadLevels.Events()
               .SelectedItemChanged
               .Where(x => x.SelectedItem > -1)
               .Select(x => x.SelectedItem)
               .Select(x => (Name: radioLabels[x].ToString(),
                        Rule: rules[radioLabels[x].ToString()]))
               .BindTo(msg.ViewModel, x => x.SpreadLevel)
               .DisposeWith(disposables);
            spreadLevels.SelectedItem = 0;
        }

        private void OnOpen(object recipient, OpenSpreadViewMessage msg)
        {
            Visible = true;
        }

        private void OnStepRulesViewClose(object recipient, CloseSpreadViewMessage msg)
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
