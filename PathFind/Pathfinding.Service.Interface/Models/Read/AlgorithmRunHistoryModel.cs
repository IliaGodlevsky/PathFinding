using Pathfinding.Service.Interface.Models.Undefined;

namespace Pathfinding.Service.Interface.Models.Read
{
    public class AlgorithmRunHistoryModel
    {
        public AlgorithmRunModel Run { get; set; }

        public RunStatisticsModel Statistics { get; set; }

        public GraphInformationModel GraphInfo { get; set; }
    }
}
