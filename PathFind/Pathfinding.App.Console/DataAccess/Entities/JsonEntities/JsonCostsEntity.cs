namespace Pathfinding.App.Console.DataAccess.Entities.JsonEntities
{
    public class JsonCostsEntity : IIdentityItem<long>
    {
        public long Id { get; set; }

        public long AlgorithmId { get; set; }

        public int[] Costs { get; set; }
    }
}
