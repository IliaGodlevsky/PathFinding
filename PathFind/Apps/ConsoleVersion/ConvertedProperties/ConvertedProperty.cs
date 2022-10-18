using ConsoleVersion.Interface;
using System;

namespace ConsoleVersion.ConvertedProperties
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
