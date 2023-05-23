using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.KeysMenuItems
{
    [MediumPriority]
    internal sealed class EditorKeysMenuItem : KeysMenuItem
    {
        public EditorKeysMenuItem(IInput<int> input) : base(input)
        {
        }

        protected override IEnumerable<string> GetKeysNames()
        {
            yield return nameof(Keys.ReverseVertex);
            yield return nameof(Keys.IncreaseCost);
            yield return nameof(Keys.DecreaseCost);
            yield return nameof(Keys.SmoothGraph);
            yield return nameof(Keys.ResetSmooth);
            yield return nameof(Keys.UndoSmooth);
            yield return nameof(Keys.SubmitSmooth);
        }

        public override string ToString()
        {
            return Languages.EditorKeys;
        }
    }
}
