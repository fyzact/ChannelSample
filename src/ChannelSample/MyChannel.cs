using System.Collections.Concurrent;

namespace ChannelSample
{
    public sealed class MyChannel<T>
    {
        ConcurrentQueue<T> _queue = new ConcurrentQueue<T>();
        SemaphoreSlim _semaphore = new SemaphoreSlim(0);
        public void Write(T item)
        {
            _queue.Enqueue(item);
            _semaphore.Release();

        }

        public async Task<T> ReadAsync()
        {
            await _semaphore.WaitAsync().ConfigureAwait(false);
            _queue.TryDequeue(out T item);
            return item;
        }
    }
}
