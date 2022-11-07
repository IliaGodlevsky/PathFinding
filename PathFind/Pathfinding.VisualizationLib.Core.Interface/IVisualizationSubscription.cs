using System.Collections.Generic;

namespace Pathfinding.VisualizationLib.Core.Interface
{
    public interface IVisualizationSubscription
    {
        void Subscribe(IEnumerable<IVisualizable> visualizables);

        void Unsubscribe(IEnumerable<IVisualizable> visualizables);
    }
}
