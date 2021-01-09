using GraphLib.GraphField;
using GraphLib.GraphFieldCreating;

namespace WPFVersion.Model
{
    internal class WpfGraphFieldFactory : GraphFieldFactory
    {
        protected override IGraphField GetField()
        {
            return new WpfGraphField();
        }
    }
}
