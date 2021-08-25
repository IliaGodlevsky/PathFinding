using GraphLib.Interfaces;

namespace GraphLib.Base.BaseEndPointsConditions
{
    public interface IEndPointsCondition
    {
        bool IsTrue(IVertex vertex);

        void Execute(IVertex vertex);
    }
}
