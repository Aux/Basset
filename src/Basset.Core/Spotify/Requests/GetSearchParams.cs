using System.Collections.Generic;

namespace Basset.Spotify
{
    public class GetSearchParams : QueryMap
    {
        public string Query { get; set; }
        public string Type { get; set; }
        public int Limit { get; set; } = 5;

        public override IDictionary<string, string> CreateQueryMap()
        {
            var dict = new Dictionary<string, string>();
            dict["limit"] = Limit.ToString();
            dict["type"] = Type;
            dict["q"] = Query;
            return dict;
        }
    }
}
