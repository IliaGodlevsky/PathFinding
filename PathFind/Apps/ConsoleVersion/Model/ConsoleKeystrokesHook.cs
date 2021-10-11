using System;
using System.Linq;

namespace ConsoleVersion.Model
{
    internal sealed class ConsoleKeystrokesHook : IDisposable
    {
        public event Action KeystrokeHooked;

        private bool IsHookingRequired { get; set; }

        public ConsoleKeystrokesHook(ConsoleKey key, params ConsoleKey[] consoleKeys)
        {
            this.consoleKeys = consoleKeys.Append(key).ToArray();
        }

        public void CancelHookingConsoleKeystrokes(object sender, EventArgs e)
        {
            IsHookingRequired = false;
        }

        public void StartHookingConsoleKeystrokes()
        {
            IsHookingRequired = true;
            while (IsHookingRequired)
            {
                var key = Console.ReadKey(true).Key;
                if (consoleKeys.Contains(key))
                {
                    KeystrokeHooked?.Invoke();
                    IsHookingRequired = false;
                }
            }
        }

        public void Dispose()
        {
            KeystrokeHooked = null;
        }

        private readonly ConsoleKey[] consoleKeys;
    }
}
