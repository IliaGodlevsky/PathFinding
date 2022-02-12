using WPFVersion.Messages;
using WPFVersion.ViewModel;

namespace WPFVersion.Extensions
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

        public static void RecieveMessage(this AlgorithmViewModel model, UpdateStatisticsMessage message)
        {
            model.Time = message.Time;
            model.PathCost = message.PathCost;
            model.PathLength = message.PathLength;
            model.VisitedVerticesCount = message.VisitedVertices;
        }
    }
}
