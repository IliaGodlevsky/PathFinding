using System;

namespace GraphLibrary.PauseMaker.Interface
{
    public interface IPauseProvider
    {
        event Action PauseEvent;
        void Pause();
    }
}
