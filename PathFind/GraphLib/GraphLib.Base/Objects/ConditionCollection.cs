using Common.Extensions;
using Common.Interface;
using GraphLib.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Base.Objects
{
    internal sealed class ConditionCollection : IEnumerable<IConditionConstruction<IVertex>>
    {
        public ConditionCollection()
        {
            conditions = new List<IConditionConstruction<IVertex>>();
        }

        public void Add(IConditionConstruction<IVertex> condition)
        {
            conditions.Add(condition);
        }

        public void Add(Predicate<IVertex> condition, Action<IVertex> body)
        {
            Add(new ConditionConstruction(body, condition));
        }

        public void PerfromFirstExecutable(IVertex param)
        {
            conditions
                .FirstOrDefault(condition => condition.IsCondition(param))
                ?.Execute(param);
        }

        public void PerformAllExecutable(IVertex param)
        {
            conditions
                .Where(condition => condition.IsCondition(param))
                .ForEach(body => body.Execute(param));
        }

        public IEnumerator<IConditionConstruction<IVertex>> GetEnumerator()
        {
            return conditions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return conditions.GetEnumerator();
        }

        private readonly List<IConditionConstruction<IVertex>> conditions;
    }
}
