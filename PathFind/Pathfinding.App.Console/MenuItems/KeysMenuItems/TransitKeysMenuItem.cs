using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.KeysMenuItems
{
    [LowPriority]
    internal sealed class TransitKeysMenuItem : KeysMenuItem
    {
        public TransitKeysMenuItem(IInput<int> input) : base(input)
        {
        }

        protected override IEnumerable<string> GetKeysNames()
        {
            yield return nameof(Keys.ReplaceTransit);
            yield return nameof(Keys.MarkToReplace);
        }

        public override string ToString()
        {
            return Languages.TransitKeys;
        }
    }
}
