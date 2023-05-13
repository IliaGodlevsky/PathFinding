using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Settings;
using System;

namespace Pathfinding.App.Console.Model.Visualizations.Visuals
{
    internal sealed class PathVisual : Visual
    {
        protected override ConsoleColor Color
        {
            get => Colours.Default.PathColor;
            set => Colours.Default.PathColor = value;
        }

        protected override Tokens Token => Tokens.Path;

        public PathVisual(IMessenger messenger) : base(messenger)
        {
        }
    }
}
