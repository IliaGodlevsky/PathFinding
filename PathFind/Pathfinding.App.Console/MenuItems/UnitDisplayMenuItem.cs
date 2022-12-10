using Pathfinding.App.Console.Interface;
using Pathfinding.Logging.Interface;
using System;

namespace Pathfinding.App.Console.MenuItems
{
    internal abstract class UnitDisplayMenuItem<TUnit> : IMenuItem
        where TUnit : IUnit
    {
        private readonly IViewFactory viewFactory;
        private readonly TUnit unit;
        private readonly ILog log;

        public abstract int Order { get; }

        protected UnitDisplayMenuItem(IViewFactory viewFactory,
            TUnit unit, ILog log)
        {
            this.viewFactory = viewFactory;
            this.unit = unit;
            this.log = log;
        }

        public virtual void Execute()
        {
            try
            {
                var view = viewFactory.CreateView(unit);
                view.Display();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public virtual bool CanBeExecuted() => true;
    }
}