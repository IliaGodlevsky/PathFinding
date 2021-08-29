using GraphLib.Interfaces;

namespace GraphLib.Base.EndPointsCondition.Interface
{
    public interface IEndPointsCondition
    {
        bool IsTrue(IVertex vertex);

        void Execute(IVertex vertex);
    }
}
