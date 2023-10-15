using Pathfinding.App.Console.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Units
{
    internal sealed class AlgorithmChooseUnit : Unit
    {
        public AlgorithmChooseUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IReadOnlyCollection<IConditionedMenuItem> conditioned)
            : base(menuItems, conditioned)
        {
        }
    }
}
