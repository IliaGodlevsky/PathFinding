using NStack;
using Pathfinding.ConsoleApp.ViewModel;
using System.Linq;
using Terminal.Gui;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Linq;
using System.Data;
using ReactiveUI;
using System.Reactive.Disposables;
using CommunityToolkit.Mvvm.Messaging;
using Autofac.Features.AttributeFilters;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;

namespace Pathfinding.ConsoleApp.View.RightPanelViews.Runs.CreateRun
{
    internal sealed partial class StepRulesView : FrameView
    {
        private readonly CompositeDisposable disposables = new();

        private readonly StepRulesViewModel stepRulesViewModel;
        private readonly IMessenger messenger;
        private readonly ustring[] radioLabels;

        public StepRulesView(StepRulesViewModel stepRulesViewModel,
            [KeyFilter(KeyFilters.Views)]IMessenger messenger)
        {
            Initialize();
            this.stepRulesViewModel = stepRulesViewModel;
            this.messenger = messenger;
            stepRules.RadioLabels = stepRulesViewModel.StepRules
                .Keys.Select(x => ustring.Make(x))
                .ToArray();
            radioLabels = stepRules.RadioLabels;
            messenger.Register<StepRuleViewModelChangedMessage>(this, OnViewModelChanged);
            messenger.Register<OpenStepRuleViewMessage>(this, OnOpen);
            messenger.Register<CloseStepRulesViewMessage>(this, OnStepRulesViewClose);
            messenger.Register<CloseRunCreationViewMessage>(this, OnRunCreationViewClosed);
        }

        private void OnViewModelChanged(object recipient, StepRuleViewModelChangedMessage msg)
        {
            var rules = stepRulesViewModel.StepRules;
            disposables.Clear();
            stepRules.Events()
               .SelectedItemChanged
               .Where(x => x.SelectedItem > -1)
               .Select(x => x.SelectedItem)
               .Select(x => (Name: radioLabels[x].ToString(),
                        Rule: rules[radioLabels[x].ToString()]))
               .BindTo(msg.ViewModel, x => x.StepRule)
               .DisposeWith(disposables);
            stepRules.SelectedItem = 0;
        }

        private void OnOpen(object recipient, OpenStepRuleViewMessage msg)
        {
            Visible = true;
        }

        private void OnStepRulesViewClose(object recipient, CloseStepRulesViewMessage msg)
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
