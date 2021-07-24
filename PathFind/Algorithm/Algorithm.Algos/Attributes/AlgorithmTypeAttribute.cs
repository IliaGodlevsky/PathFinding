using System;

namespace Algorithm.Algos.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    internal sealed class AlgorithmTypeAttribute : Attribute
    {
        public AlgorithmTypeAttribute(Type algorithmType)
        {
            AlgorithmType = algorithmType;
        }

        public Type AlgorithmType { get; }
    }
}
