using Common.Interfaces;
using System.Collections.Generic;

namespace GraphLib.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGraph : IEnumerable<IVertex>, IDefault
    {       
        IEnumerable<int> DimensionsSizes { get; }

        IVertex this[ICoordinate coordinate] { get; set; }

        IVertex this[int index] { get; set; }

        IVertex Start { get; set; }

        IVertex End { get; set; }

        string GetFormattedData(string format);
    }
}