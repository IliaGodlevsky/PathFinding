using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace WPFVersion3D.Extensions
{
    public static class VisualCollection3DExtensions
    {
        public static void AddRange(this Visual3DCollection collection, IEnumerable<Visual3D> range )
        {
            foreach (var item in range)
            {
                collection.Add(item);
            }
        }
    }
}
