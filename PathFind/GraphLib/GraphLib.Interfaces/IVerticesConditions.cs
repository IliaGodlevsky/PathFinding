namespace GraphLib.Interfaces
{
    public interface IVerticesConditions
    {
        void ExecuteTheFirstTrue(IVertex vertex);

        void ResetAllExecutings();
    }
}
