using System;

namespace Common.Interface
{
    /// <summary>
    /// Creates a deep copy of an object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICloneable<out T> : ICloneable
    {
        /// <summary>
        /// Returns a deep copy of an object, 
        /// that implements this method
        /// </summary>
        /// <returns>A deep copy of an object</returns>
        new T Clone();
    }
}
