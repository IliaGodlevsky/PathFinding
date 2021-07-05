using GraphLib.Interfaces;
using GraphLib.Serialization.Exceptions;
using System.IO;

namespace GraphLib.Serialization.Interfaces
{
    /// <summary>
    /// Interface for graph serializers
    /// </summary>
    public interface IGraphSerializer
    {
        /// <summary>
        /// Saves graph in <see cref="Stream"/>
        /// </summary>
        /// <param name="graph">graph for serialize</param>
        /// <param name="stream">stream in what 
        /// graph will be serialize</param>
        /// <exception cref="CantSerializeGraphException"/>
        void SaveGraph(IGraph graph, Stream stream);

        /// <summary>
        /// Deserizlize graph from <see cref="Stream"/>
        /// </summary>
        /// <param name="stream">stream from 
        /// what graph will be deserialized</param>
        /// <returns>Graph from stream</returns>
        /// <exception cref="CantSerializeGraphException"/>
        IGraph LoadGraph(Stream stream);
    }
}
