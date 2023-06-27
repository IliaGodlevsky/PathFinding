using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Settings;

namespace Pathfinding.App.Console.Model.VertexActions.NeighbourhoodActions
{
    internal sealed class ExitAction : IVertexAction
    {
        private ActiveVertex Active { get; }

        public ExitAction(ActiveVertex active)
        {
            Active = active;
        }

        public void Do(Vertex vertex)
        {
            if (Active.Current != null)
            {
                foreach (Vertex neighbour in Active.Current.Neighbours)
                {
                    if (neighbour.Color == Colours.Default.NeighbourhoodColor)
                    {
                        neighbour.VisualizeAsRegular();
                    }
                }
                Active.Current = null;
                Active.Availiable.Clear();
            }
        }
    }
}
