using AssembleClassesLib.EventArguments;
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
            var orderedAlgorithmNames = e.ClassesNames.OrderBy(Name);
            var orderedAlgorithmKeys = model.AlgorithmKeys.OrderBy(Name);
            if (!orderedAlgorithmNames.SequenceEqual(orderedAlgorithmKeys))
            {
                model.AlgorithmKeys = orderedAlgorithmNames.ToList();
            }
        }

        private string Name(string name)
        {
            return name;
        }

        private readonly PathFindingModel model;
    }
}
