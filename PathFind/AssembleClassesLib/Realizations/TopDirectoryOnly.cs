using AssembleClassesLib.Interface;
using System.IO;

namespace AssembleClassesLib.Realizations
{
    public sealed class TopDirectoryOnly : IAssembleSearchOption
    {
        public SearchOption SearchOption => SearchOption.TopDirectoryOnly;
    }
}
