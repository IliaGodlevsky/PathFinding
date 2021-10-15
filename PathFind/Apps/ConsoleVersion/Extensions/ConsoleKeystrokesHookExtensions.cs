using ConsoleVersion.Model;
using System;

namespace ConsoleVersion.Extensions
{
    internal static class ConsoleKeystrokesHookExtensions
    {
        public static void CancelHookingConsoleKeystrokes(this ConsoleKeystrokesHook self, object sender, EventArgs e)
        {
            self.CancelHookingConsoleKeystrokes();
        }
    }
}
