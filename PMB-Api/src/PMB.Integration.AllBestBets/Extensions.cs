using System.Collections.Generic;
using System.Linq;

namespace PMB.Integration.AllBestBets
{
    public static class Extensions
    {
        public static IEnumerable<KeyValuePair<string, string>> ToContent(this ArbApiRequest request)
        {
            return new KeyValuePair<string, string>[]
            {
                new("access_token", request.Access_token),
                new("per_page", request.Per_page.ToString()),
                new("search_filter", request.Search_filter.FirstOrDefault().ToString()),
                new("sort_by", request.Sort_by.ToString().ToLower())
            };
        }
    }
}