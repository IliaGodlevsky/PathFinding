using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Settings;
using System;

namespace Pathfinding.App.Console.Model.Visualizations.Visuals
{
    internal sealed class TransitVisual : Visual
    {
        protected override ConsoleColor Color
        {
            get => Colours.Default.TransitColor;
            set => Colours.Default.TransitColor = value;
        }

        protected override Tokens Token => Tokens.Transit;

        public TransitVisual(IMessenger messenger) : base(messenger)
        {
        }
    }
}
