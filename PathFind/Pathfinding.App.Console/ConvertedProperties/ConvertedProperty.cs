using Pathfinding.App.Console.Interface;
using System;

namespace Pathfinding.App.Console.ConvertedProperties
{
    internal abstract class ConvertedProperty<TFrom, TTo> : IProperty<TTo>
    {
        private readonly Lazy<TTo> value;

        public TTo Value => value.Value;

        protected ConvertedProperty(TFrom from)
        {
            value = new Lazy<TTo>(() => ConvertTo(from));
        }

        public override string ToString() => Value.ToString();

        protected abstract TTo ConvertTo(TFrom from);
    }
}
