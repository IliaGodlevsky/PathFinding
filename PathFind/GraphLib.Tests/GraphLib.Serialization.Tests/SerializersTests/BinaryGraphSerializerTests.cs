﻿using Autofac;
using GraphLib.Serialization.Tests.DependencyInjection;
using NUnit.Framework;

namespace GraphLib.Serialization.Tests.SerializersTests
{
    [TestFixture]
    public class BinaryGraphSerializerTests : BaseGraphSerializerTests
    {
        protected override IContainer Container => DI.BinaryGraphSerializerContainer;
    }
}
