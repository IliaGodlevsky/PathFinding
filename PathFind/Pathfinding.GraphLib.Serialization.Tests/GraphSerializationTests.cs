using NUnit.Framework;
using Pathfinding.GraphLib.Factory.Realizations.GraphAssembles;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.UnitTest.Realizations.TestObjects;
using System.IO;

namespace Pathfinding.GraphLib.Serialization.Tests
{
    [TestFixture]
    public class GraphSerializationTests
    {
        [TestCaseSource(typeof(GraphSerializationData), nameof(GraphSerializationData.Data))]
        public void SaveLoadMethod_VariousGraphSerializers_Success(IGraphSerializer<TestGraph, TestVertex> serializer
            GraphAssemble<TestGraph, TestVertex> assemble)
        {
            using (var stream = new MemoryStream())
            {
                
            }
        }
    }
}