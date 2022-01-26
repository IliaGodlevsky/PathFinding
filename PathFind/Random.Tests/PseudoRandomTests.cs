using Common.Tests;
using NUnit.Framework;
using Random.Interface;
using Random.Realizations.Generators;

namespace Random.Tests
{
    [TestFixture]
    public class PseudoRandomTests : RandomTests
    {
        protected override IRandom Random => new PseudoRandom();
    }
}
