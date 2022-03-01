using System;

namespace Common.Interface
{
    public interface ICloneable<out T> : ICloneable
    {
        new T Clone();
    }
}
