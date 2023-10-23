using Microsoft.Extensions.Caching.Memory;

namespace SDK;

public class MyMemoryCache
{
    public MemoryCache Cache { get; } = new MemoryCache(
        new MemoryCacheOptions
        {
            SizeLimit = 1024,
        });
}