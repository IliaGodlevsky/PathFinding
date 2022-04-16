using Common.Extensions.EnumerableExtensions;
using System.Collections.Generic;
using WPFVersion3D.Interface;
using WPFVersion3D.ViewModel;

namespace WPFVersion3D.Messages.ActionMessages
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
