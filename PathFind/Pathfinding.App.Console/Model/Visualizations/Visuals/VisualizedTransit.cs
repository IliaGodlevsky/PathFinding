using Pathfinding.App.Console.Settings;

namespace Pathfinding.App.Console.Model.Visualizations.Visuals
{
    internal sealed class VisualizedTransit : VisualizedVertices
    {
        protected override string SettingKey { get; } = nameof(Colours.TransitColor);
    }
}
