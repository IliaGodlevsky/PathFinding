using Interruptable.Interface;
using System;

namespace Interruptable.Extensions
{
    public static class IInterruptableExtensions
    {
        public static void Interrupt(this IInterruptable self, object sender, EventArgs e)
        {
            self.Interrupt();
        }
    }
}
