using WPFVersion.Enums;

namespace WPFVersion.Messages
{
    internal sealed class AlgorithmStatusesMessage
    {
        public AlgorithmStatus[] Statuses { get; }

        public AlgorithmStatusesMessage(AlgorithmStatus[] statuses)
        {
            Statuses = statuses;
        }
    }
}
