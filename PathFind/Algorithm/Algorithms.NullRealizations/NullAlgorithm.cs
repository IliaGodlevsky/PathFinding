using Algorithm.Interfaces;
using SingletonLib;

namespace Algorithm.NullRealizations
{
    public sealed class NullAlgorithm : Singleton<NullAlgorithm, IAlgorithm<IGraphPath>>, IAlgorithm<IGraphPath>
    {
        public IGraphPath FindPath() => NullGraphPath.Interface;

        private NullAlgorithm()
        {

        }
    }
}
