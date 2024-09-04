using Pathfinding.Shared.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Pathfinding.Shared.Primitives
{
    public class ExtendedObservableCollection<T> : ObservableCollection<T>
    {
        public ExtendedObservableCollection()
        {

        }

        public void AddRangeSuppress(IEnumerable<T> collection)
        {
            base.Items.AddRange(collection);
        }

        public void AddSuppress(T item)
        {
            base.Items.Add(item);
        }

        public void ClearSuppress() 
        {
            base.Items.Clear();
        }
    }
}
