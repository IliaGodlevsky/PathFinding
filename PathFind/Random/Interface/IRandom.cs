namespace Random.Interface
{
    /// <summary>
    /// An interface of random number generator
    /// </summary>
    public interface IRandom
    {
        /// <summary>
        /// Returns a random number within the range 
        /// of <paramref name="minValue"/> 
        /// to <paramref name="maxValue"/>
        /// inclusivly
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns>A random number within 
        /// the range inclusivly</returns>
        int Next(int minValue, int maxValue);
    }
}
