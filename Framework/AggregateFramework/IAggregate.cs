namespace AggregateFramework
{
    public interface IAggregate { }

    public interface IAggregate<out T> : IAggregate where T : class
    {
        T GetState();
    }
}
