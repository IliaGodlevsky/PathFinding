using Pathfinding.App.Console.Model;
using System;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.Extensions
{
    internal static class ConsoleKeystrokesHookExtensions
    {
        public static async void StartAsync(this ConsoleKeystrokesHook self, object sender, EventArgs e)
        {
            await Task.Run(self.Start);
        }
    }
}
