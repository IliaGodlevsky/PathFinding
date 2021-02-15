using System;

namespace Algorithm.Interfaces
{
    /// <summary>
    /// Presents methods and events for intermitting algorithm
    /// </summary>
    public interface IIntermit
    {
        event Action OnIntermitted;

        void Intermit();
    }
}
