namespace Pathfinding.Service.Interface.Requests.Read
{
    public class ReadRunCountRequest
    {
        public int GraphId { get; set; }

        public ReadRunCountRequest(int graphId)
        {
            GraphId = graphId;
        }
    }
}
