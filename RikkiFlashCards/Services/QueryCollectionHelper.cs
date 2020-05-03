using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RikkiFlashCards.Services
{
    public static class QueryCollectionHelper
    {
        public static Dictionary<string,string> ToQueryValueDictionary(this IQueryCollection queryCollection)
        {
            var queryValueDictionary = new Dictionary<string, string>() { };
            IEnumerator<KeyValuePair<String, StringValues>> queryKVP = queryCollection.GetEnumerator();
            while (queryKVP.MoveNext())
            {
                queryValueDictionary.Add(queryKVP.Current.Key, queryKVP.Current.Value[0]);
            }
            return queryValueDictionary;
        }
    }
}
