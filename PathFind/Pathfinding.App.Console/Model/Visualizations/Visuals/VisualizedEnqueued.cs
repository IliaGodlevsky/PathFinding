using Pathfinding.App.Console.Settings;

namespace Pathfinding.App.Console.Model.Visualizations.Visuals
{
    internal sealed class VisualizedEnqueued : VisualizedVertices
    {
        protected override string SettingKey { get; } = nameof(Colours.EnqueuedColor);

        public override void Visualize(Vertex vertex)
        {
            if (!vertex.IsVisualizedAsRange() && !vertex.IsVisualizedAsPath())
            {
                base.Visualize(vertex);
            }
        }
    }
}
