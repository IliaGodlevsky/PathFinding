using GraphLib.GraphField;
using GraphLib.GraphFieldCreating;
using System.Drawing;

namespace WinFormsVersion.Model
{
    internal class WinFormsGraphFieldFactory : GraphFieldFactory
    {
        protected override IGraphField GetField()
        {
            return new WinFormsGraphField() { Location = new Point(4, 90) };
        }
    }
}
