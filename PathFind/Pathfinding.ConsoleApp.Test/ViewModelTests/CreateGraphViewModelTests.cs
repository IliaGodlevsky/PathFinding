using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Logging.Loggers;
using Pathfinding.TestUtils.Attributes;

namespace Pathfinding.ConsoleApp.Test.ViewModelTests
{
    [TestFixture, UnitTest]
    public class CreateGraphViewModelTests
    {
        public void CreateGraphCommand_FullData_ShouldCreate()
        {
            var viewModel = new CreateGraphViewModel(null, null, null, new NullLog());
        }
    }
}
