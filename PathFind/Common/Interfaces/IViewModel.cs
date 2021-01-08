using System;

namespace Common.Interfaces
{
    public interface IViewModel
    {
        event EventHandler OnWindowClosed;
    }
}
