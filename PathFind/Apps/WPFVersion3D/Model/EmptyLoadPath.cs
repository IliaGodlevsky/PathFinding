using AssembleClassesLib.Interface;

namespace WPFVersion3D.Model
{
    internal sealed class EmptyLoadPath : IAssembleLoadPath
    {
        public string LoadPath => string.Empty;
    }
}
