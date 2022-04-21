using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;

namespace WPFVersion3D.Model
{
    internal sealed class GraphField3DFactory : IGraphFieldFactory
    {
        public IGraphField CreateGraphField(IGraph graph)
        {
            return new GraphField3D((Graph3D)graph);  
        }
    }
}
