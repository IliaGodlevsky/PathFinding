using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Core;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using System.Collections.Generic;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class CreateRandomAlgorithmRunViewModel : CreateRunButtonViewModel
    {
        public CreateRandomAlgorithmRunViewModel(
            IRequestService<VertexModel> service,
            [KeyFilter(KeyFilters.ViewModels)] IMessenger messenger, ILog logger)
            : base(service, messenger, logger)
        {
        }

        public override string AlgorithmId => AlgorithmNames.Random;

        protected override PathfindingProcess GetAlgorithm(IEnumerable<VertexModel> pathfindingRange)
        {
            return new RandomAlgorithm(pathfindingRange);
        }
    }
}
