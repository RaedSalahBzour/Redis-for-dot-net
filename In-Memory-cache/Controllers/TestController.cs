using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace In_Memory_cache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public IMemoryCache memoryCache { get; }

        public TestController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }
        [HttpGet(Name = "getSetValuesList")]
        public IEnumerable<int> GetSetValuesList()
        {
            string cacheKey = "values";

            if (!memoryCache.TryGetValue(cacheKey, out List<int> cacheValue))
            {
                //here must be like data from database this is mock data
                cacheValue = Enumerable.Range(1, 5).Select(index => index).ToList();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(10))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

                memoryCache.Set(cacheKey, cacheValue, cacheEntryOptions);
            }
            return cacheValue;
        }
    }
}
