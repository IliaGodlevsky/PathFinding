namespace GraphLib.Base.EndPointsInspection.Abstractions
{
    internal abstract class BaseEndPointsInspection
    {
        protected BaseEndPointsInspection(BaseEndPoints endPoints)
        {
            this.endPoints = endPoints;
        }

        protected readonly BaseEndPoints endPoints;
    }
}
