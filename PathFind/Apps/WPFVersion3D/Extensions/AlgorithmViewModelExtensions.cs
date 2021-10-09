using WPFVersion3D.Enums;
using WPFVersion3D.Messages;
using WPFVersion3D.ViewModel;

namespace WPFVersion3D.Extensions
{
    internal static class AlgorithmViewModelExtensions
    {
        public static bool TryInterrupt(this AlgorithmViewModel model)
        {
            if (model.IsStarted())
            {
                model.Interrupt();
                return true;
            }

            return false;
        }

        public static void RecieveMessage(this AlgorithmViewModel model, UpdateAlgorithmStatisticsMessage message)
        {
            model.Time = message.Time;
            model.PathCost = message.PathCost;
            model.PathLength = message.PathLength;
            model.VisitedVerticesCount = message.VisitedVertices;
        }

        public static bool IsStarted(this AlgorithmViewModel model)
        {
            return model.Status == AlgorithmStatuses.Started;
        }
    }
}