namespace AggregateFramework
{
    public interface IAggregate { }

    public interface IAggregate<T> : IAggregate where T : class
    {
        T GetState();
    }
}
