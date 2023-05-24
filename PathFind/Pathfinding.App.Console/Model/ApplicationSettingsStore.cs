using System;
using System.Collections.Generic;
using System.Configuration;

namespace Pathfinding.App.Console.Model
{
    internal sealed class ApplicationSettingsStore : IDisposable
    {
        private readonly IEnumerable<SettingsBase> settings;

        public ApplicationSettingsStore(IEnumerable<SettingsBase> settings)
        {
            this.settings = settings;
        }

        public void Dispose()
        {
            foreach (var setting in settings)
            {
                setting.Save();
            }
        }
    }
}
