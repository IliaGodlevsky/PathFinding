using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Settings;
using System;

namespace Pathfinding.App.Console.Model.Visualizations.Visuals
{
    internal sealed class TargetVisual : Visual
    {
        protected override ConsoleColor Color
        {
            get => Colours.Default.TargetColor;
            set => Colours.Default.TargetColor = value;
        }

        protected override IToken Token => Tokens.Target;

        public TargetVisual(IMessenger messenger) : base(messenger)
        {
        }
    }
}
