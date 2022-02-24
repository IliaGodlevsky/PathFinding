using ConsoleVersion.Model;
using System;
using System.Threading.Tasks;

namespace ConsoleVersion.Extensions
{
    internal static class ConsoleKeystrokesHookExtensions
    {
        public static async void StartAsync(this ConsoleKeystrokesHook self, object sender, EventArgs e)
        {
            await Task.Run(self.Start);
        }
    }
}
