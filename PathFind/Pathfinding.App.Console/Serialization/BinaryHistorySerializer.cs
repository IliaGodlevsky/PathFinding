//using Pathfinding.App.Console.Extensions;
//using Pathfinding.GraphLib.Factory.Interface;
//using Pathfinding.GraphLib.Serialization.Core.Interface;
//using System.IO;
//using System.Text;

//namespace Pathfinding.App.Console.Serialization
//{
//    internal sealed class BinaryHistorySerializer : ISerializer<IPathfindingHistory>
//    {
//        private readonly ICoordinateFactory factory;

//        public BinaryHistorySerializer(ICoordinateFactory factory)
//        {
//            this.factory = factory;
//        }

//        public IPathfindingHistory DeserializeFrom(Stream stream)
//        {
//            using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
//            {
//                return reader.ReadHistory(factory);
//            }
//        }

//        public void SerializeTo(IPathfindingHistory history, Stream stream)
//        {
//            using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
//            {
//                writer.WriteHistory(history);
//            }
//        }
//    }
//}
