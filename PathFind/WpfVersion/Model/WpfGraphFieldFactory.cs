using GraphLib.GraphField;
using GraphLib.GraphFieldCreating;

namespace WpfVersion.Model
{
    internal class WpfGraphFieldFactory : GraphFieldFactory
    {
        protected override IGraphField GetField()
        {
            return new WpfGraphField();
        }
    }
}
