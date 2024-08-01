using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Views;
using Pathfinding.Logging.Interface;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems
{
    internal abstract class UnitDisplayMenuItem<TUnit> : IMenuItem
        where TUnit : IUnit
    {
        private readonly IInput<int> intInput;
        private readonly TUnit unit;
        private readonly ILog log;

        protected UnitDisplayMenuItem(IInput<int> intInput,
            TUnit unit, ILog log)
        {
            this.intInput = intInput;
            this.unit = unit;
            this.log = log;
        }

        public virtual async Task ExecuteAsync(CancellationToken token = default)
        {
            try
            {
                var view = new View(unit, intInput);
                await view.DisplayAsync(token);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }
}