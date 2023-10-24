using Pathfinding.App.Console.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Units
{
    internal sealed class ColorsUnit : Unit
    {
        public ColorsUnit(IReadOnlyCollection<IMenuItem> menuItems)
            : base(menuItems)
        {
        }
    }
}
