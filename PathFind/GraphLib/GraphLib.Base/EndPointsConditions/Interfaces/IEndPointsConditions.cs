using GraphLib.Interfaces;

namespace GraphLib.Base.EndPointsConditions.Interfaces
{
    public interface IEndPointsConditions
    {
        void ExecuteTheFirstTrue(IVertex vertex);

        void ResetAllExecutings();
    }
}
