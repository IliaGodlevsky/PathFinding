using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using NStack;
using Pathfinding.ConsoleApp.Extensions;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.ViewModel.Interface;
using Pathfinding.Domain.Core;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System;
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

        private readonly IRequireStepRuleViewModel viewModel;

        public StepRulesView(
            [KeyFilter(KeyFilters.Views)] IMessenger messenger,
            IRequireStepRuleViewModel viewModel)
        {
            Initialize();
            var rules = Enum.GetValues(typeof(StepRules))
                .Cast<StepRules>()
                .ToDictionary(x => x.ToStringRepresentation());
            var labels = rules.Select(x => ustring.Make(x.Key)).ToArray();
            var values = labels.Select(x => rules[x.ToString()]).ToList();
            stepRules.RadioLabels = labels;
            stepRules.Events().SelectedItemChanged
               .Where(x => x.SelectedItem > -1)
               .Select(x => values[x.SelectedItem])
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
