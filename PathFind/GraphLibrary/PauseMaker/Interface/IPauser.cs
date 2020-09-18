using System;

namespace GraphLibrary.PauseMaker.Interface
{
    /// <summary>
    /// Presents methods and events for pausing processes
    /// </summary>
    public interface IPauseProvider
    {
        event Action PauseEvent;
        void Pause();
    }
}
