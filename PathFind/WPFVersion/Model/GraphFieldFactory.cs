using GraphLib.GraphField;
using GraphLib.GraphFieldCreating;

namespace WPFVersion.Model
{
    internal class GraphFieldFactory : BaseGraphFieldFactory
    {
        protected override IGraphField GetField()
        {
            return new GraphField();
        }
    }
}
