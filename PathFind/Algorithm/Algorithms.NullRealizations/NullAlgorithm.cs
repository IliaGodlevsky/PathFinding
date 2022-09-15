using Algorithm.Interfaces;
using SingletonLib;

namespace Algorithm.NullRealizations
{
    public sealed class NullAlgorithm : Singleton<NullAlgorithm, IAlgorithm>, IAlgorithm
    {
        public IGraphPath FindPath() => NullGraphPath.Interface;

        private NullAlgorithm()
        {

        }
    }
}
