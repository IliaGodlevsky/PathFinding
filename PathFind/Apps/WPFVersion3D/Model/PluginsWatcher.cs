using Common.Extensions;
using GraphLib.ViewModel;
using GraphViewModel;
using System.Linq;
using System.Windows;

namespace WPFVersion3D.Model
{
    internal sealed class PluginsWatcher : BasePluginsWatcher
    {
        public PluginsWatcher(PathFindingModel viewModel) : base()
        {
            this.viewModel = viewModel;
        }

        protected override void UpdateAlgorithmsKeys()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var addedAlgorithms = GetAddedAlgorithms();
                var deletedAlgorithms = GetDeletedAlgorithms();
                if (addedAlgorithms.Any())
                {
                    viewModel.AlgorithmKeys.AddRange(addedAlgorithms);
                }
                if (deletedAlgorithms.Any())
                {
                    viewModel.AlgorithmKeys.RemoveRange(deletedAlgorithms);
                }
                if (addedAlgorithms.Any() || deletedAlgorithms.Any())
                {
                    viewModel.AlgorithmKeys = viewModel.AlgorithmKeys.OrderBy(Name).ToList();
                }
            });
        }

        private readonly PathFindingModel viewModel;
    }
}
