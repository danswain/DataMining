using System.Collections;
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

        public static IEnumerable<T>  CombineSets<T>(this IEnumerable<IEnumerable<T>> intputEnumerable)
        {
            var combined = new List<T>();
            foreach (var array in intputEnumerable)
            {
                foreach(var item in array)
                {
                    combined.Add(item);
                }
            }

            return combined;
        }
    }
}