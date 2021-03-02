using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using static Algorithm.Realizations.AlgorithmsFactory;

namespace GraphViewModel
{
    public abstract class BasePluginsWatcher
    {
        public string FolderPath { private get; set; }

        public BasePluginsWatcher()
        {
            tokenSource = new CancellationTokenSource();
        }

        public void StopWatching(object sender, EventArgs e)
        {
            tokenSource.Cancel();
        }

        public void StartWatching()
        {
            token = tokenSource.Token;
            Task.Run(WatchFolder);
        }

        protected abstract void UpdateAlgorithmsKeys();

        protected IEnumerable<string> GetAddedAlgorithms()
        {
            return GetAlgorithmsDescriptions().Except(currentAlgorithms);
        }

        protected IEnumerable<string> GetDeletedAlgorithms()
        {
            return currentAlgorithms.Except(GetAlgorithmsDescriptions());
        }

        protected string Name(string key)
        {
            return key;
        }

        private void WatchFolder()
        {
            while (!token.IsCancellationRequested)
            {
                currentAlgorithms = GetAlgorithmsDescriptions();
                LoadAlgorithms(FolderPath);
                UpdateAlgorithmsKeys();
            }
        }

        private CancellationToken token;
        private IEnumerable<string> currentAlgorithms;

        private readonly CancellationTokenSource tokenSource;
    }
}
