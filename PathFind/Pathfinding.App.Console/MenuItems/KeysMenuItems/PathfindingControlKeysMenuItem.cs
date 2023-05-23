using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.KeysMenuItems
{
    [MediumPriority]
    internal sealed class PathfindingControlKeysMenuItem : KeysMenuItem
    {
        public PathfindingControlKeysMenuItem(IInput<int> input) : base(input)
        {
        }

        protected override IEnumerable<string> GetKeysNames()
        {
            yield return nameof(Keys.SpeedUp);
            yield return nameof(Keys.SpeedDown);
            yield return nameof(Keys.StepByStepPathfinding);
            yield return nameof(Keys.PauseAlgorithm);
            yield return nameof(Keys.InterruptAlgorithm);
            yield return nameof(Keys.ResumeAlgorithm);
        }

        public override string ToString()
        {
            return Languages.PathfindingControlKeys;
        }
    }
}
