using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Settings;
using System;

namespace Pathfinding.App.Console.Model.Visualizations.Visuals
{
    internal sealed class SourceVisual : Visual
    {
        protected override ConsoleColor Color
        {
            get => Colours.Default.SourceColor;
            set => Colours.Default.SourceColor = value;
        }

        protected override Tokens Token => Tokens.Source;

        public SourceVisual(IMessenger messenger) : base(messenger)
        {
        }
    }
}
