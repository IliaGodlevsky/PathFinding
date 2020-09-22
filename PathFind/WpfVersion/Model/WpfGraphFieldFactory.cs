using GraphLibrary.GraphField;
using GraphLibrary.GraphFieldCreating;

namespace WpfVersion.Model
{
    internal class WpfGraphFieldFactory : GraphFieldFactory
    {
        protected override IGraphField CreateField()
        {
            return new WpfGraphField();
        }
    }
}
