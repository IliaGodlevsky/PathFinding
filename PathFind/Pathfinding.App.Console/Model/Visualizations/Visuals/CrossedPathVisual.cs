using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Settings;
using System;

namespace Pathfinding.App.Console.Model.Visualizations.Visuals
{
    internal sealed class CrossedPathVisual : Visual
    {
        protected override ConsoleColor Color
        {
            get => Colours.Default.CrossedPathColor;
            set => Colours.Default.CrossedPathColor = value;
        }

        protected override Tokens Token => Tokens.Crossed;

        public CrossedPathVisual(IMessenger messenger) : base(messenger)
        {
        }
    }
}
