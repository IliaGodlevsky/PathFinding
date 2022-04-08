using System.ComponentModel;

namespace WPFVersion3D.Enums
{
    internal enum AlgorithmStatuses
    {
        [Description("Started")]
        Started = 0,

        [Description("Paused")]
        Paused = 1,

        [Description("Interrupted")]
        Interrupted = 2,

        [Description("Finished")]
        Finished = 3,

        [Description("Failed")]
        Failed = 4
    }
}