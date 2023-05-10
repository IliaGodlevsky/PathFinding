using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Settings;
using System;

namespace Pathfinding.App.Console.Model.Visualizations.Visuals
{
    internal sealed class VisitedVisual : Visual
    {
        protected override ConsoleColor Color
        {
            get => Colours.Default.VisitedColor;
            set => Colours.Default.VisitedColor = value;
        }

        public VisitedVisual(IMessenger messenger) : base(messenger)
        {
        }

        protected override Tokens Token => Tokens.Visited;

        public override void Visualize(Vertex vertex)
        {
            if (!vertex.IsVisualizedAsRange() && !vertex.IsVisualizedAsPath())
            {
                base.Visualize(vertex);
            }
        }
    }
}
