using Pathfinding.App.Console.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Units
{
    internal sealed class KeysUnit : Unit
    {
        public KeysUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IReadOnlyCollection<IConditionedMenuItem> conditioned)
            : base(menuItems, conditioned)
        {
        }
    }
}
