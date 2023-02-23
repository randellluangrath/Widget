namespace Widget.Contracts.Interfaces;

public interface IWidgetCache<T, TKey>
{
    Task<T> GetOrCreateAsync(TKey key, Func<Task<T>> createItem);
    void Remove(object key);
}