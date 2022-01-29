using GraphLib.Realizations.Neighbourhoods;
using NUnit.Framework;

namespace GraphLib.Realizations.Tests.NeighborhoodTests
{
    [TestFixture]
    public class VonNeumannNeighborhoodTest : BaseNeighborhoodTests<VonNeumannNeighborhood>
    {
        protected override ulong GetExpectedNeighborhoodCount(int dimensionsCount)
        {
            return (ulong)(2 * dimensionsCount);
        }
    }
}