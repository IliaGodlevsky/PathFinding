using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using NStack;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.ViewModel.Interface;
using Pathfinding.Domain.Core;
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

        private readonly ustring[] radioLabels;
        private readonly IRequireStepRuleViewModel viewModel;

        public StepRulesView(
            [KeyFilter(KeyFilters.Views)] IMessenger messenger,
            IRequireStepRuleViewModel viewModel)
        {
            Initialize();
            var rules = new[] { StepRuleNames.Default, StepRuleNames.Landscape };
            stepRules.RadioLabels = rules.Select(ustring.Make).ToArray();
            radioLabels = stepRules.RadioLabels;
            disposables.Clear();
            stepRules.Events()
               .SelectedItemChanged
               .Where(x => x.SelectedItem > -1)
               .Select(x => radioLabels[x.SelectedItem].ToString())
               .BindTo(viewModel, x => x.StepRule)
               .DisposeWith(disposables);
            stepRules.SelectedItem = 0;
            messenger.Register<OpenStepRuleViewMessage>(this, OnOpen);
            messenger.Register<CloseStepRulesViewMessage>(this, OnStepRulesViewClose);
            messenger.Register<CloseAlgorithmCreationViewMessage>(this, OnRunCreationViewClosed);
            this.viewModel = viewModel;
        }

        private void OnOpen(object recipient, OpenStepRuleViewMessage msg)
        {
            stepRules.SelectedItem = 0;
            Visible = true;
        }

        private void OnStepRulesViewClose(object recipient, CloseStepRulesViewMessage msg)
        {
            Close();
        }

        private void OnRunCreationViewClosed(object recipient, CloseAlgorithmCreationViewMessage msg)
        {
            Close();
        }
        
        private void Close()
        {
            viewModel.StepRule = default;
            Visible = false;
        }
    }
}
