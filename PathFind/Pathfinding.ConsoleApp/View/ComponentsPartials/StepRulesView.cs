using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
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
            Height = Dim.Percent(45);
            Width = Dim.Percent(55);
            Border = new Border()
            {
                BorderStyle = BorderStyle.Rounded,
                Title = "Step rules"
            };
            Visible = false;
            Add(stepRules);
        }
    }
}
