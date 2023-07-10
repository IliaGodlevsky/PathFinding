using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Views;
using Pathfinding.Logging.Interface;
using System;

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

        public virtual void Execute()
        {
            try
            {
                var view = new View(unit, intInput);
                view.Display();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }
}