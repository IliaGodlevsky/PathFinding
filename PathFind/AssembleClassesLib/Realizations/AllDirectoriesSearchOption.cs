using AssembleClassesLib.Interface;
using System.IO;

namespace AssembleClassesLib.Realizations
{
    public sealed class AllDirectories : IAssembleSearchOption
    {
        public SearchOption SearchOption => SearchOption.AllDirectories;
    }
}
