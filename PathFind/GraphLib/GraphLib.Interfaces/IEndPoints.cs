namespace GraphLib.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEndPoints
    {
        /// <summary>
        /// 
        /// </summary>
        IVertex End { get; }

        /// <summary>
        /// 
        /// </summary>
        IVertex Start { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        bool IsEndPoint(IVertex vertex);
    }
}
