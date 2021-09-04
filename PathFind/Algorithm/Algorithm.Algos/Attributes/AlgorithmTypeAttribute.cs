using Algorithm.Interfaces;
using GraphLib.Interfaces;
using System;

namespace Algorithm.Algos.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    internal sealed class AlgorithmTypeAttribute : Attribute
    {
        public Type AlgorithmType { get; }

        public AlgorithmTypeAttribute(Type algorithmType)
        {
            if (!typeof(IAlgorithm).IsAssignableFrom(algorithmType))
            {
                string message = $"{algorithmType.Name} is not an algorithm type";
                throw new ArgumentException(message, nameof(algorithmType));
            }

            AlgorithmType = algorithmType;
        }
    }
}
