using ReactiveUI;
using System.Threading.Tasks;
using System;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal abstract class BaseViewModel : ReactiveObject
    {
        public async Task ExecuteSafe(Func<Task> action, Action<Exception, string> log)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                log(ex, ex.Message);
            }
        }
    }
}
