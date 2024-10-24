using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using NStack;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.ViewModel;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System.Data;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class StepRulesView : FrameView
    {
        private readonly CompositeDisposable disposables = new();

        private readonly StepRulesViewModel stepRulesViewModel;
        private readonly ustring[] radioLabels;

        public StepRulesView(StepRulesViewModel stepRulesViewModel,
            [KeyFilter(KeyFilters.Views)] IMessenger messenger)
        {
            Initialize();
            this.stepRulesViewModel = stepRulesViewModel;
            stepRules.RadioLabels = stepRulesViewModel.StepRules
                .Keys.Select(x => ustring.Make(x))
                .ToArray();
            radioLabels = stepRules.RadioLabels;
            messenger.Register<StepRuleViewModelChangedMessage>(this, OnViewModelChanged);
            messenger.Register<OpenStepRuleViewMessage>(this, OnOpen);
            messenger.Register<CloseStepRulesViewMessage>(this, OnStepRulesViewClose);
            messenger.Register<CloseAlgorithmCreationViewMessage>(this, OnRunCreationViewClosed);
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

        private void OnRunCreationViewClosed(object recipient, CloseAlgorithmCreationViewMessage msg)
        {
            disposables.Clear();
            Visible = false;
        }
    }
}
