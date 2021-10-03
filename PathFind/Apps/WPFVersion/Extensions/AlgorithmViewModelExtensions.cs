using WPFVersion.Enums;
using WPFVersion.ViewModel;

namespace WPFVersion.Extensions
{
    internal static class AlgorithmViewModelExtensions
    {
        public static bool TryInterrupt(this AlgorithmViewModel model)
        {
            if (model.Status == AlgorithmStatus.Started)
            {
                model.Interrupt();
                model.Status = AlgorithmStatus.Interrupted;
                return true;
            }

            return false;
        }
    }
}
