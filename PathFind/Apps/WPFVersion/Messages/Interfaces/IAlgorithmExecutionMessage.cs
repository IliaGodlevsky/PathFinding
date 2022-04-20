using Commands.Interfaces;
using System.Collections.Generic;
using WPFVersion.ViewModel;

namespace WPFVersion.Messages.Interfaces
{
    internal interface IAlgorithmsExecutionMessage : IExecutable<IEnumerable<AlgorithmViewModel>>
    {

    }
}
