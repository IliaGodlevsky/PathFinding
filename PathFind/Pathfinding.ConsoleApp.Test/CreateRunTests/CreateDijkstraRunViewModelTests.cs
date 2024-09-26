using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Infrastructure.Business.Algorithms.StepRules;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.TestUtils.Attributes;

namespace Pathfinding.ConsoleApp.Test.CreateRunTests
{
    [TestFixture, UnitTest]
    internal sealed class CreateDijkstraRunViewModelTests : CreateRunButtonViewModelTests
    {
        protected override CreateRunButtonViewModel GetViewModel(IMessenger messenger, IRequestService<VertexModel> service, ILog logger)
        {
            var viewModel = new CreateDijkstraRunViewModel(service, messenger, logger);
            viewModel.StepRule = ("Default", new DefaultStepRule());
            return viewModel;
        }
    }
}
