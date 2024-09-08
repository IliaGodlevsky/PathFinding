using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View.RightPanelViews.Runs.CreateRun
{
    internal partial class StepRulesView
    {
        private readonly RadioGroup stepRules = new RadioGroup();

        private void Initialize()
        {
            stepRules.X = 1;
            stepRules.Y = 1;
            X = 0;
            Y = 0;
            Height = Dim.Percent(33);
            Width = Dim.Percent(45);
            Border = new Border()
            {
                BorderStyle = BorderStyle.Rounded,
                Title = "Step rules"
            };
            Add(stepRules);
        }
    }
}
