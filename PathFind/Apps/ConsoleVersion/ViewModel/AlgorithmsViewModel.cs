using Algorithm.Base;
using Algorithm.Factory.Interface;
using Common.Interface;
using ConsoleVersion.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleVersion.ViewModel
{
    internal class AlgorithmsViewModel : IViewModel, IRequireIntInput
    {
        public event Action WindowClosed;

        public IInput<int> IntInput { get; set; }

        public AlgorithmsViewModel(IEnumerable<IAlgorithmFactory<PathfindingAlgorithm>> algorithmFactories)
        {

        }
    }
}
