using System.ComponentModel;

namespace WPFVersion3D.Enums
{
    internal enum AlgorithmStatuses
    {
        [Description("Started")]
        Started = 0,

        [Description("Interrupted")]
        Interrupted = 1,

        [Description("Finished")]
        Finished = 2,

        [Description("Failed")]
        Failed = 3
    }
}