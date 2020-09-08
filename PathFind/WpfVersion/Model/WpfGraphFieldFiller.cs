using GraphLibrary.GraphCreate.GraphFieldFiller;
using GraphLibrary.GraphField;

namespace WpfVersion.Model
{
    internal class WpfGraphFieldFiller : AbstractGraphFieldFiller
    {
        protected override IGraphField GetField()
        {
            return new WpfGraphField();
        }
    }
}
