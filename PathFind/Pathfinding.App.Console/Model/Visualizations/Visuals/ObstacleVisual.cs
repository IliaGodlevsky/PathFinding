using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Settings;
using System;

namespace Pathfinding.App.Console.Model.Visualizations.Visuals
{
    internal sealed class ObstacleVisual : Visual
    {
        protected override ConsoleColor Color
        {
            get => Colours.Default.ObstacleColor;
            set => Colours.Default.ObstacleColor = value;
        }

        protected override IToken Token => Tokens.Obstacle;

        public ObstacleVisual(IMessenger messenger) : base(messenger)
        {
        }
    }
}
