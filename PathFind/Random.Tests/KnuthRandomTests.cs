using Common.Tests;
using NUnit.Framework;
using Random.Interface;
using Random.Realizations.Generators;

namespace Random.Tests
{
    [TestFixture]
    public class KnuthRandomTests : RandomTests
    {
        protected override IRandom GetRandom()
        {
            return new KnuthRandom();
        }
    }
}
