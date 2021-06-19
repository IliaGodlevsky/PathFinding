using AssembleClassesLib.EventArguments;
using Common.Extensions;
using System.Linq;

namespace GraphViewModel
{
    public sealed class AlgorithmsKeysUpdate
    {
        public AlgorithmsKeysUpdate(PathFindingModel model)
        {
            this.model = model;
        }

        public void UpdateAlgorithmKeys(object sender, AssembleClassesEventArgs e)
        {
            var currentLoadedPluginsKeys = e.ClassesNames
                .ToArray();

            var addedAlgorithms = currentLoadedPluginsKeys
                .Except(model.AlgorithmKeys)
                .ToArray();

            var deletedAlgorithms = model.AlgorithmKeys
                .Except(currentLoadedPluginsKeys.AsEnumerable())
                .ToArray();

            if (addedAlgorithms.Any())
            {
                model.AlgorithmKeys.AddRange(addedAlgorithms);
            }
            if (deletedAlgorithms.Any())
            {
                model.AlgorithmKeys.RemoveRange(deletedAlgorithms);
            }
            if (addedAlgorithms.Any() || deletedAlgorithms.Any())
            {
                model.AlgorithmKeys = model.AlgorithmKeys
                    .OrderBy(key => key)
                    .ToList();
            }
        }

        private readonly PathFindingModel model;
    }
}
