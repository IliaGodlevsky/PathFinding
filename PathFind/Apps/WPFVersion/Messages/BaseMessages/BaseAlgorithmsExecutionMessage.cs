using Common.Extensions.EnumerableExtensions;
using System.Collections.Generic;
using WPFVersion.Messages.Interfaces;
using WPFVersion.ViewModel;

namespace WPFVersion.Messages.BaseMessages
{
    internal abstract class BaseAlgorithmsExecutionMessage : IAlgorithmsExecutionMessage
    {
        public virtual void Execute(IEnumerable<AlgorithmViewModel> algorithms)
        {
            algorithms.ForEach(Execute);
        }

        protected abstract void Execute(AlgorithmViewModel model);
    }
}
