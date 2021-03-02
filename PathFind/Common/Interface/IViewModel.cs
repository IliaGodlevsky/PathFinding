using System;

namespace Common.Interface
{
    public interface IViewModel
    {
        event EventHandler OnWindowClosed;
    }
}
