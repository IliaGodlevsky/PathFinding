using Algorithm.Interfaces;
using GraphLib.Interfaces;
using NullObject.Attributes;

namespace Algorithm.NullRealizations
{
    /// <summary>
    /// A step rule that represents a null (default) 
    /// analog for <see cref="IStepRule"/> interface
    /// </summary>
    [Null]
    public sealed class NullStepRule : IStepRule
    {
        public double CalculateStepCost(IVertex neighbour, IVertex current)
        {
            return default;
        }
    }
}
