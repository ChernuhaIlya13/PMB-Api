using System.Collections.Generic;
using System.Linq;

namespace PMB.Integration.AllBestBets.Temporary
{
    public class TemporaryRequest
    {
        public int PerPage { get; set; }
        
        public int SearchFilter { get; set; }
        
        public bool IsLive { get; set; }
        
        public DefaultFilterInfo DefaultFilter { get; set; }
        
        public class DefaultFilterInfo
        {
            public int[] BookmakerIds { get; set; }
            
            public int Id { get; set; }
        }

        public IEnumerable<KeyValuePair<string, string>> ToFormContent()
        {
            var formContent = new List<KeyValuePair<string, string>>
            {
                new("per_page", PerPage.ToString()),
                new("search_filter[]", SearchFilter.ToString()),
                new("is_live", IsLive.ToString().ToLower()),
                new("default_filter[id]", DefaultFilter.Id.ToString())
            };

            formContent.AddRange(DefaultFilter.BookmakerIds
                .Select(bookmakerId => new KeyValuePair<string, string>("default_filter[bookmakers1][]", bookmakerId.ToString())));

            return formContent;
        }
    }
}