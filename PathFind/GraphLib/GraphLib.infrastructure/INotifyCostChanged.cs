namespace GraphLib.Infrastructure
{
    public interface INotifyCostChanged
    {
        event CostChangedEventHandler CostChanged;
    }
}
