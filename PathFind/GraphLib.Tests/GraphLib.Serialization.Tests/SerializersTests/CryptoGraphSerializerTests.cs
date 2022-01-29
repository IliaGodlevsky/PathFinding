using Autofac;
using GraphLib.Serialization.Tests.DependencyInjection;
using NUnit.Framework;

namespace GraphLib.Serialization.Tests.SerializersTests
{
    [TestFixture]
    internal class CryptoGraphSerializerTests : BaseGraphSerializerTests
    {
        protected override IContainer Container => DI.CryptoSerializerContainer;
    }
}
