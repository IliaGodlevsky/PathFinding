using GraphLib.Interfaces;
using Priority_Queue;
using System.Diagnostics;

namespace Algorithm.Сompanions
{
    [DebuggerDisplay("{Vertex.Position.ToString()}")]
    public class Node : StablePriorityQueueNode
    {
        public Node(IVertex vertex, double value = float.PositiveInfinity)
        {
            Vertex = vertex;
            Priority = (float)value;
        }

        public IVertex Vertex { get; }
    }
}