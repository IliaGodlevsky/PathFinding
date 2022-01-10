using System.ComponentModel;

namespace WPFVersion.Enums
{
    internal enum AlgorithmStatus
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