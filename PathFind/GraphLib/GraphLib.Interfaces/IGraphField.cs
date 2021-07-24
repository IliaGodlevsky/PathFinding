namespace GraphLib.Interfaces
{
    /// <summary>
    /// Reprsents a field for displaying graph
    /// </summary>
    public interface IGraphField
    {
        /// <summary>
        /// Addes a vertex to the graph field
        /// </summary>
        /// <param name="vertex"></param>
        void Add(IVertex vertex);
    }
}
