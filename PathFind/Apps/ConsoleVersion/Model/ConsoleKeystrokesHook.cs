using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleVersion.Model
{
    internal sealed class ConsoleKeystrokesHook
    {
        public static ConsoleKeystrokesHook Instance => instance.Value;

        private bool IsHookingRequired { get; set; }

        private ConsoleKeystrokesHook()
        {
            actionsPerKeystrokes = new List<Tuple<Action, ConsoleKey[]>>();
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
                var action = actionsPerKeystrokes.Find(item => item.Item2.Contains(key));
                if (action != null)
                {
                    action.Item1.Invoke();
                }
            }
        }

        public void Register(Action action, ConsoleKey key, params ConsoleKey[] keys)
        {
            var keystrokes = keys.Append(key).ToArray();
            var actionPerKeystrokes = new Tuple<Action, ConsoleKey[]>(action, keystrokes);
            actionsPerKeystrokes.Add(actionPerKeystrokes);
        }

        public void Unregister(Action action)
        {
            var actionPerKeystrokes = actionsPerKeystrokes.Find(item => item.Item1.Equals(action));
            if (actionsPerKeystrokes != null)
            {
                actionsPerKeystrokes.Remove(actionPerKeystrokes);
            }
        }

        private readonly List<Tuple<Action, ConsoleKey[]>> actionsPerKeystrokes;
        private static readonly Lazy<ConsoleKeystrokesHook> instance 
            = new Lazy<ConsoleKeystrokesHook>(() => new ConsoleKeystrokesHook());
    }
}