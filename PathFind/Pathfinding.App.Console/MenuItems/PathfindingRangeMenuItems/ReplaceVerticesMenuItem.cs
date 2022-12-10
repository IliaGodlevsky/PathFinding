using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;

namespace Pathfinding.App.Console.MenuItems.PathfindingRangeMenuItems
{
    internal abstract class ReplaceVerticesMenuItem : RangeMenuItem
    {
        protected abstract string Message { get; }

        protected abstract Vertex Vertex { get; }

        protected ReplaceVerticesMenuItem(IPathfindingRangeBuilder<Vertex> rangeBuilder, 
            IMessenger messenger, IInput<int> input) : base(rangeBuilder, messenger, input)
        {

        }

        public override void Execute()
        {
            using (Cursor.CleanUpAfter())
            {
                ExcludeFromRange(Vertex);
                IncludeInRange(InputVertex());
            }
        }

        protected void ExcludeFromRange(Vertex vertex)
        {
            using (Cursor.UseCurrentPosition())
            {
                rangeBuilder.Exclude(vertex);
            }
        }

        protected Vertex InputVertex()
        {
            System.Console.WriteLine(Message);
            return input.InputVertex(graph, rangeBuilder.Range);
        }
    }
}
