using GraphLib.Interfaces;

namespace GraphLib.Base.EndPointsConditions.Interfaces
{
    public interface IVerticesConditions
    {
        void ExecuteTheFirstTrue(IVertex vertex);

        void ResetAllExecutings();
    }
}
