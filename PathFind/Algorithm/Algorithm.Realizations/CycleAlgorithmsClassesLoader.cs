using Algorithm.Interfaces;
using Common.EventArguments;
using Common.EventHandlers;
using Common.Interface;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Algorithm.Realizations
{
    public sealed class CycleAlgorithmsClassesLoader : IClassLoader<IAlgorithm>, IInterrupted
    {
        public event PluginsLoaderEventHandler OnPluginsUpdated
        {
            add => pluginsLoader.OnPluginsUpdated += value;
            remove => pluginsLoader.OnPluginsUpdated -= value;
        }

        public event EventHandler OnInterrupted;

        public CycleAlgorithmsClassesLoader(IClassLoader<IAlgorithm> pluginsLoader)
        {
            this.pluginsLoader = pluginsLoader;
            LoadedPluginsKeys = pluginsLoader.LoadedPluginsKeys;
            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
            LoadedPluginsKeys = pluginsLoader.LoadedPluginsKeys;
        }

        public string[] LoadedPluginsKeys { get; private set; }

        public IAlgorithm CreateObject(string key, params object[] parametres)
        {
            return pluginsLoader.CreateObject(key, parametres);
        }

        public void Interrupt()
        {
            tokenSource.Cancel();
            var args = new PluginsLoaderEventArgs(LoadedPluginsKeys);
            OnInterrupted?.Invoke(this, args);
            Dispose();
        }

        public void LoadPlugins()
        {
            Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    pluginsLoader.LoadPlugins();
                    LoadedPluginsKeys = pluginsLoader.LoadedPluginsKeys;
                }
            }, token);
        }

        public void Dispose()
        {
            tokenSource.Cancel();
            tokenSource.Dispose();
            pluginsLoader.Dispose();
            OnInterrupted = null;
        }

        private readonly IClassLoader<IAlgorithm> pluginsLoader;
        private readonly CancellationToken token;
        private readonly CancellationTokenSource tokenSource;
    }
}