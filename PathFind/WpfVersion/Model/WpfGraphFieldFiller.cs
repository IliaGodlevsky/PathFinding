using GraphLibrary.GraphCreate.GraphFieldFiller;
using GraphLibrary.Model;

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
