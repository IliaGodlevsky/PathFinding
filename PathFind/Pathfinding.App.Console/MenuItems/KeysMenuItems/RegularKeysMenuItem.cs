using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.KeysMenuItems
{
    [HighPriority]
    internal sealed class RegularKeysMenuItem : KeysMenuItem
    {
        public RegularKeysMenuItem(IInput<int> input) : base(input)
        {
        }

        protected override IEnumerable<string> GetKeysNames()
        {
            yield return nameof(Keys.VertexUp);
            yield return nameof(Keys.VertexDown);
            yield return nameof(Keys.VertexLeft);
            yield return nameof(Keys.VertexRight);
            yield return nameof(Keys.IncludeInRange);
            yield return nameof(Keys.ExcludeFromRange);
        }

        public override string ToString()
        {
            return Languages.RegularKeys;
        }
    }
}
