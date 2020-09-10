using GraphLibrary.GraphCreate.GraphFieldFactory;
using GraphLibrary.GraphField;

namespace WpfVersion.Model
{
    internal class WpfGraphFieldFactory : AbstractGraphFieldFactory
    {
        protected override IGraphField GetField()
        {
            return new WpfGraphField();
        }
    }
}
