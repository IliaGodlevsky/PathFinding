using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Settings;
using System;

namespace Pathfinding.App.Console.Model.Visualizations.Visuals
{
    internal sealed class EnqueuedVisual : Visual
    {
        protected override ConsoleColor Color
        {
            get => Colours.Default.EnqueuedColor;
            set => Colours.Default.EnqueuedColor = value;
        }

        protected override IToken Token => Tokens.Enqueued;

        public EnqueuedVisual(IMessenger messenger) : base(messenger)
        {
        }

        public override void Visualize(Vertex vertex)
        {
            if (!vertex.IsVisualizedAsRange() && !vertex.IsVisualizedAsPath())
            {
                base.Visualize(vertex);
            }
        }
    }
}
