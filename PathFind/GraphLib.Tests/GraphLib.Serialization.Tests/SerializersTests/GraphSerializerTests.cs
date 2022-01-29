using Autofac;
using GraphLib.Serialization.Tests.DependencyInjection;
using NUnit.Framework;

namespace GraphLib.Serialization.Tests.SerializersTests
{
    [TestFixture]
    internal class GraphSerializerTests : BaseGraphSerializerTests
    {
        protected override IContainer Container => DI.SerializerContainer;
    }
}
