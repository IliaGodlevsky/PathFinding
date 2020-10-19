using GraphLibrary.GraphField;
using GraphLibrary.GraphFieldCreating;

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
