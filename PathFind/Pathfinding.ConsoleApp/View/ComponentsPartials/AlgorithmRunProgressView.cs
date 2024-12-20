﻿using ReactiveUI;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal partial class AlgorithmRunProgressView
    {
        private readonly ProgressBar bar = new();
        private readonly Label leftLabel = new("-");
        private readonly Label rightLabel = new("+");

        public float Fraction
        {
            get => bar.Fraction;
            set
            {
                if (value < 0) bar.Fraction = 0;
                else if (value > 1) bar.Fraction = 1;
                else bar.Fraction = value;
                this.RaisePropertyChanged();
            }
        }

        private void Initialize()
        {
            X = 0;
            Y = Pos.Percent(95);
            Width = Dim.Percent(66);
            Height = Dim.Fill();
            var driver = Application.Driver;
            bar.ColorScheme = new()
            {
                Normal = driver.MakeAttribute(Color.DarkGray, Color.Black)
            };
            Border = new()
            {
                BorderBrush = Color.BrightYellow,
                BorderStyle = BorderStyle.Rounded
            };
            Fraction = 0;

            rightLabel.Width = Dim.Percent(3);
            leftLabel.Width = Dim.Percent(3);
            bar.Width = Dim.Percent(94);

            bar.X = Pos.Right(leftLabel);
            bar.Y = Pos.Center();
            bar.ProgressBarStyle = ProgressBarStyle.Blocks;
            bar.ProgressBarFormat = ProgressBarFormat.Framed;

            leftLabel.WantContinuousButtonPressed = true;
            leftLabel.TextAlignment = TextAlignment.Centered;
            leftLabel.Y = Pos.Center();
            leftLabel.X = 1;

            rightLabel.X = Pos.Right(bar) + 1;
            rightLabel.Y = Pos.Center();
            rightLabel.TextAlignment = TextAlignment.Centered;
            rightLabel.WantContinuousButtonPressed = true;

            Add(rightLabel, bar, leftLabel);
            rightLabel.Visible = false;
            leftLabel.Visible = false;
            bar.Visible = false;
        }
    }
}
