using ConsoleVersion.View;
using GraphLibrary.GraphField;
using GraphLibrary.GraphFieldCreating;

namespace ConsoleVersion.Model
{
    internal class ConsoleGraphFieldFactory : GraphFieldFactory
    {
        protected override IGraphField CreateField()
        {
            return new ConsoleGraphField();
        }
    }
}
