using GraphLibrary.GraphCreate.GraphFieldFactory;
using GraphLibrary.GraphField;
using System.Drawing;

namespace WinFormsVersion.Model
{
    internal class WinFormsGraphFieldFactory : AbstractGraphFieldFactory
    {
        protected override IGraphField GetField()
        {
            return new WinFormsGraphField() { Location = new Point(4, 90) };
        }
    }
}
