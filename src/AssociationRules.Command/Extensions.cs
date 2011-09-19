using System.Collections.Generic;
using System.Linq;

namespace DataMining.MarketBasketAnalysis
{
    public static class Extensions
    {
        public static IEnumerable<T> Combine<T>(this IEnumerable<T> collection,IEnumerable<T> anotherCollection)
        {
            var combinedCollection = new List<T>();
            combinedCollection.AddRange(collection.Select(x => x));
            combinedCollection.AddRange(anotherCollection.Select(x => x));

            return combinedCollection;
        }

        public static IEnumerable<T>  CombineSets<T>(this IEnumerable<T> collection)
        {
            var combined = new List<T>();
            foreach (var item in collection)
            {
                return null;
            }

            return null;
        }
    }
}