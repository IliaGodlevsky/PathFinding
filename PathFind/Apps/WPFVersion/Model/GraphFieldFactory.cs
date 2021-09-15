using GraphLib.Base;
using GraphLib.Interfaces;

namespace WPFVersion.Model
{
    internal sealed class GraphFieldFactory : BaseGraphFieldFactory, IGraphFieldFactory
    {
        protected override IGraphField GetField()
        {
            return new GraphField();
        }
    }
}
