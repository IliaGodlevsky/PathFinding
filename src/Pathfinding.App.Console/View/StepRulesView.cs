﻿using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using NStack;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Injection;
using Pathfinding.App.Console.Messages.View;
using Pathfinding.App.Console.ViewModel.Interface;
using Pathfinding.Domain.Core;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System.Data;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal sealed partial class StepRulesView : FrameView
    {
        private readonly CompositeDisposable disposables = [];

        private readonly IRequireStepRuleViewModel viewModel;

        public StepRulesView(
            [KeyFilter(KeyFilters.Views)] IMessenger messenger,
            IRequireStepRuleViewModel viewModel)
        {
            Initialize();
            var rules = Enum.GetValues<StepRules>()
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
            messenger.Register<CloseRunCreateViewMessage>(this, OnRunCreationViewClosed);
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

        private void OnRunCreationViewClosed(object recipient, CloseRunCreateViewMessage msg)
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
