using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Settings;
using Shared.Extensions;
using System.Linq;

namespace Pathfinding.App.Console.Model.VertexActions.NeighbourhoodActions
{
    internal sealed class ExitAction : IVertexAction
    {
        private ActiveVertex Active { get; }

        public ExitAction(ActiveVertex active)
        {
            Active = active;
        }

        public void Invoke(Vertex vertex)
        {
            if (Active.Current is not null)
            {
                using (Cursor.HideCursor())
                {
                    Active.Current.Neighbours.OfType<Vertex>()
                        .Where(v => v.Color == Colours.Default.NeighbourhoodColor)
                        .ForEach(v => v.VisualizeAsRegular());
                }
                Active.Current = null;
                Active.Availiable.Clear();
            }
        }
    }
}
