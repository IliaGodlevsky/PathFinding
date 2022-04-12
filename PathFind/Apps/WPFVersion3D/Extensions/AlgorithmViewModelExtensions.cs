using WPFVersion3D.Messages.PassValueMessages;
using WPFVersion3D.ViewModel;

namespace WPFVersion3D.Extensions
{
    internal static class AlgorithmViewModelExtensions
    {
        public static void UpdateStatistics(this AlgorithmViewModel model, UpdateAlgorithmStatisticsMessage message)
        {
            model.Time = message.Time;
            model.PathCost = message.PathCost;
            model.PathLength = message.PathLength;
            model.VisitedVerticesCount = message.VisitedVertices;
        }
    }
}