namespace GraphLib.Base.BaseEndPointsConditions
{
    internal abstract class BaseEndPointsCondition
    {
        protected BaseEndPointsCondition(BaseEndPoints endPoints)
        {
            this.endPoints = endPoints;
        }

        protected readonly BaseEndPoints endPoints;
    }
}
