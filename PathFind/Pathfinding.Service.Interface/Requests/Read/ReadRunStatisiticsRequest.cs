namespace Pathfinding.Service.Interface.Requests.Read
{
    public class ReadRunStatisiticsRequest
    {
        public int GraphId { get; }

        public ReadRunStatisiticsRequest(int graphId)
        {
            GraphId = graphId;
        }
    }
}
