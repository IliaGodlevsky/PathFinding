using Algorithm.Interfaces;
using System;

namespace Algorithm.Algos.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
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
