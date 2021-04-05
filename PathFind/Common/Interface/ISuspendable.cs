﻿using System;

namespace Common.Interface
{
    /// <summary>
    /// Presents methods and events for stopping the process
    /// </summary>
    public interface ISuspendable
    {
        event Action OnInterrupted;

        void Suspend(int duration);
    }
}
