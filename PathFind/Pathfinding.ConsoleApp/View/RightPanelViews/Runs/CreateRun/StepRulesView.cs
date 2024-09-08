using NStack;
using Pathfinding.ConsoleApp.ViewModel;
using System.Linq;
using Terminal.Gui;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Linq;
using System.Data;
using ReactiveUI;
using System.Reactive.Disposables;

namespace Pathfinding.ConsoleApp.View.RightPanelViews.Runs.CreateRun
{
    internal sealed partial class StepRulesView : FrameView
    {
        private readonly CompositeDisposable disposables = new();

        private readonly IRequireStepRuleViewModel viewModel;
        private readonly StepRulesViewModel stepRulesViewModel;

        public StepRulesView(IRequireStepRuleViewModel viewModel,
            StepRulesViewModel stepRulesViewModel)
        {
            Initialize();
            this.viewModel = viewModel;
            this.stepRulesViewModel = stepRulesViewModel;
            stepRules.RadioLabels = stepRulesViewModel.StepRules
                .Keys.Select(x => ustring.Make(x))
                .ToArray();
            var radioLabels = stepRules.RadioLabels;
            var rules = stepRulesViewModel.StepRules;
            stepRules.Events()
                .SelectedItemChanged
                .Where(x => x.SelectedItem > -1)
                .Select(x => x.SelectedItem)
                .Select(x => (Name: radioLabels[x].ToString(), Rule: rules[radioLabels[x].ToString()]))
                .BindTo(viewModel, x => x.StepRule)
                .DisposeWith(disposables);
            stepRules.SelectedItem = 0;
        }
    }
}
