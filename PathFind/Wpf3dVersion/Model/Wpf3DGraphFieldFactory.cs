using GraphLib.GraphField;
using GraphLib.GraphFieldCreating;

namespace Wpf3dVersion.Model
{
    public class Wpf3DGraphFieldFactory : GraphFieldFactory
    {
        protected override IGraphField GetField()
        {
            return new Wpf3dGraphField();
        }
    }
}
