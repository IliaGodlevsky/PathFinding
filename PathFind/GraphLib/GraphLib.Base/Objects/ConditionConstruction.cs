using Common.Interface;
using GraphLib.Interfaces;
using System;

namespace GraphLib.Base.Objects
{
    internal sealed class ConditionConstruction : IConditionConstruction<IVertex>
    {
        public ConditionConstruction(Action<IVertex> body,
            Predicate<IVertex> condition = null)
        {
            this.condition = condition;
            this.body = body;
        }

        public void Execute(IVertex param)
        {
            body.Invoke(param);
        }

        public bool IsCondition(IVertex param)
        {
            return condition == null || condition?.Invoke(param) == true;
        }

        private readonly Predicate<IVertex> condition;
        private readonly Action<IVertex> body;
    }
}
