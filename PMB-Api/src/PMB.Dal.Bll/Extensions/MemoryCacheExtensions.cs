using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Caching.Memory;

namespace PMB.Dal.Bll.Extensions
{
    public static class MemoryCacheExtensions
    {
        public static IReadOnlyCollection<string> GetKeys(this IMemoryCache cache)
        {
            var field = typeof(MemoryCache).GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);
            var collection = field!.GetValue(cache) as ICollection;
            
            var items = new List<string>();
            
            if (collection != null)
            {
                foreach (var item in collection)
                {
                    var methodInfo = item.GetType().GetProperty("Key");
                    var val = methodInfo!.GetValue(item);
                    items.Add(val!.ToString());
                }
            }

            return items;
        }
    }
}