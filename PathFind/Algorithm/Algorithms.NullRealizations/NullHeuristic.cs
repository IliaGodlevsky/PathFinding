using Algorithm.Interfaces;
using GraphLib.Interfaces;
using SingletonLib;

namespace Algorithm.NullRealizations
{
    public sealed class NullHeuristic : Singleton<NullHeuristic, IHeuristic>, IHeuristic
    {
        public double Calculate(IVertex first, IVertex second)
        {
            return default;
        }

        private NullHeuristic()
        {

        }
    }
}
