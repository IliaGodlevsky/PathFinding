using System;

namespace Algorithm.Interface
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
