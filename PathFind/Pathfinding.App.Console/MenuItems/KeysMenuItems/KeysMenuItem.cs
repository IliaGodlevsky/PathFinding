using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.KeysMenuItems
{
    internal abstract class KeysMenuItem : IMenuItem
    {
        private readonly IInput<int> input;
        private readonly IReadOnlyList<string> keys;
        private readonly IReadOnlyList<ConsoleKey> consoleKeys;
        private readonly MenuList keysMenuList;
        private readonly MenuList consoleKeysMenuList;

        protected static readonly Keys Keys = Keys.Default;

        protected KeysMenuItem(IInput<int> input)
        {
            this.input = input;
            keys = GetKeysNames().ToList().AsReadOnly();
            keysMenuList = keys
                .Select(key => Languages.ResourceManager.GetString(key) ?? key)
                .Select(item => item.ToString())
                .Append(Languages.Quit)
                .CreateMenuList(columnsNumber: 3);
            consoleKeys = GetConsoleKeys()
                .ToList()
                .AsReadOnly();
            consoleKeysMenuList = consoleKeys
                .Select(key => key.ToString())
                .Append(Languages.Quit)
                .CreateMenuList(columnsNumber: 5);
        }

        public void Execute()
        {
            string message = string.Format(Languages.ChooseKeyMsg, keysMenuList + "\n");
            int index = GetIndex(message, keys.Count + 1, 1) - 1;
            while (index != keys.Count)
            {
                string name = keys[index];
                var current = Keys[name];
                string msg = string.Format(Languages.InputKeyMsg, consoleKeysMenuList + "\n", current);
                index = GetIndex(msg, consoleKeys.Count + 1, 1) - 1;
                if (index != consoleKeys.Count)
                {
                    Keys[name] = consoleKeys[index];
                }
                index = GetIndex(message, keys.Count + 1, 1) - 1;
            }
        }

        private int GetIndex(string message, int limit, int bottom)
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                return input.Input(message, limit, bottom);
            }
        }

        protected virtual IEnumerable<ConsoleKey> GetConsoleKeys()
        {
            for (var key = ConsoleKey.A; key <= ConsoleKey.Z; key++)
            {
                yield return key;
            }
            yield return ConsoleKey.UpArrow;
            yield return ConsoleKey.DownArrow;
            yield return ConsoleKey.LeftArrow;
            yield return ConsoleKey.RightArrow;
            yield return ConsoleKey.Enter;
            yield return ConsoleKey.Escape;
        }

        protected abstract IEnumerable<string> GetKeysNames();
    }
}
