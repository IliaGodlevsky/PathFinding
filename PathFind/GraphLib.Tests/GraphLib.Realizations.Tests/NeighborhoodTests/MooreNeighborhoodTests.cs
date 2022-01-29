using GraphLib.Realizations.Neighbourhoods;
using NUnit.Framework;
using System;

namespace GraphLib.Realizations.Tests.NeighborhoodTests
{
    [TestFixture]
    public class MooreNeighborhoodTests : BaseNeighborhoodTests<MooreNeighborhood>
    {
        protected override ulong GetExpectedNeighborhoodCount(int dimensionsCount)
        {
            return (ulong)Math.Pow(3, dimensionsCount) - 1;
        }
    }
}
