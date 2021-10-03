using WPFVersion3D.Enums;
using WPFVersion3D.ViewModel;

namespace WPFVersion3D.Extensions
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
