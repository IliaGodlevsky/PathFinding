using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Settings;
using System;

namespace Pathfinding.App.Console.Model.Visualizations.Visuals
{
    internal sealed class RegularVisual : Visual
    {
        protected override ConsoleColor Color
        {
            get => Colours.Default.RegularColor;
            set => Colours.Default.RegularColor = value;
        }

        protected override Tokens Token => Tokens.Regular;

        public RegularVisual(IMessenger messenger) : base(messenger)
        {
        }
    }
}
