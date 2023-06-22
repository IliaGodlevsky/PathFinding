namespace Pathfinding.App.Console.DataAccess.Entities.JsonEntities
{
    public class JsonInformationEntity : IIdentityItem<long>
    {
        public long Id { get; set; }

        public long GraphId { get; set; }

        public string Description { get; set; }
    }
}
