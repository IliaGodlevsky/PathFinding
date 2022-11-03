using Shared.Process.Interface;
using System;

namespace Shared.Process.Extensions
{
    public static class IInterruptableExtensions
    {
        public static void Interrupt(this IInterruptable self, object sender, EventArgs e)
        {
            self.Interrupt();
        }
    }
}
