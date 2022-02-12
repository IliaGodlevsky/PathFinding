using WPFVersion3D.Messages;
using WPFVersion3D.ViewModel;

namespace WPFVersion3D.Extensions
{
    internal static class AlgorithmViewModelExtensions
    {
        public static void InterruptIfStarted(this AlgorithmViewModel model)
        {
            if (model.IsStarted)
            {
                model.Interrupt();
            }
        }

        public static void RecieveMessage(this AlgorithmViewModel model, UpdateAlgorithmStatisticsMessage message)
        {
            model.Time = message.Time;
            model.PathCost = message.PathCost;
            model.PathLength = message.PathLength;
            model.VisitedVerticesCount = message.VisitedVertices;
        }
    }
}