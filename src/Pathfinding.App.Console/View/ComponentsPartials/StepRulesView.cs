﻿using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal partial class StepRulesView
    {
        private readonly RadioGroup stepRules = new RadioGroup();

        private void Initialize()
        {
            stepRules.X = 1;
            stepRules.Y = 0;
            X = 0;
            Y = 1;
            Height = Dim.Percent(20);
            Width = Dim.Percent(30);
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
