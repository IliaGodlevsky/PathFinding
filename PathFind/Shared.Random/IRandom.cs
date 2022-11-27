namespace Shared.Random
{
    /// <summary>
    /// Represents and interface of random number generator
    /// </summary>
    public interface IRandom
    {
        /// <summary>
        /// Returns an integer in the range between 
        /// <see cref="uint.MinValue"/> 
        /// and <see cref="uint.MaxValue"/> includingly
        /// </summary>
        /// <returns>An integer between <see cref="uint.MinValue"/> 
        /// and <see cref="uint.MaxValue"/></returns>
        uint NextUint();
    }
}