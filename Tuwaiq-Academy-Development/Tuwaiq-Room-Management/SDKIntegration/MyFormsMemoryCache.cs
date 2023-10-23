using Microsoft.Extensions.Caching.Memory;

namespace SDKIntegration;

public class MyFormsMemoryCache
{
    public MemoryCache Cache { get; } = new MemoryCache(
        new MemoryCacheOptions
        {
            // SizeLimit = 1024,
        });
}