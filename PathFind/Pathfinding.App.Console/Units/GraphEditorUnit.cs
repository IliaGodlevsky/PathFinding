using Pathfinding.App.Console.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Units
{
    internal sealed class GraphEditorUnit : Unit
    {
        public GraphEditorUnit(IReadOnlyCollection<IMenuItem> menuItems)
            : base(menuItems)
        {
        }
    }
}
