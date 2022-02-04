namespace GraphLib.Base.EndPoints.EndPointsInspection
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
