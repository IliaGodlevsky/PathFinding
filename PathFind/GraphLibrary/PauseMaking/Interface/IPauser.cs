using System;

namespace GraphLibrary.PauseMaking.Interface
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
