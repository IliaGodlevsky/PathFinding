using Pathfinding.App.Console.Settings;

namespace Pathfinding.App.Console.Model.Visualizations.Containers
{
    internal sealed class VisualizedTarget : VisualizedVertices
    {
        protected override string SettingKey { get; } = nameof(Colours.TargetColor);
    }
}
