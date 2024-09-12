using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Undefined;
using System.Collections.Generic;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class CreateRandomAlgorithmRunViewModel : CreateRunButtonViewModel
    {
        public CreateRandomAlgorithmRunViewModel(IRequestService<VertexModel> service,
            [KeyFilter(KeyFilters.ViewModels)]IMessenger messenger, ILog logger) 
            : base(service, messenger, logger)
        {
        }

        protected override string AlgorithmId => "Random";

        protected override void AppendStatistics(RunStatisticsModel model)
        {
            
        }

        protected override PathfindingProcess GetAlgorithm(IEnumerable<VertexModel> pathfindingRange)
        {
            return new RandomAlgorithm(pathfindingRange);
        }
    }
}
