using GraphLib.Base;
using GraphLib.Interface;

namespace WPFVersion.Model
{
    internal sealed class GraphFieldFactory : BaseGraphFieldFactory
    {
        protected override IGraphField GetField()
        {
            return new GraphField();
        }
    }
}
