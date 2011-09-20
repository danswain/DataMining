using System;
using System.Collections.Generic;
using System.Linq;

namespace DataMining.MarketBasketAnalysis
{
    class Program
    {

        static void Main(string[] args)
        {
            var set1 = new int[] { 1, 2, 3, 4 };
            var set2 = new int[] { 1, 2 };
            var set3 = new int[] { 1, 3, 4 };
            var set4 = new int[] { 2, 3 };
            var set5 = new int[] { 3, 4 };
            var set6 = new int[] { 2, 4 };

            var setsCollection = new List<int[]>{set1,set2,set3,set4,set5,set6};

            var combinedSets = setsCollection.CombineSets();

            var frequencies = GetFrequencies(combinedSets);

            var minSupportReached = frequencies.Where(x => x.Frequency >= 2);

            var supports = GenerateCandidates(minSupportReached.ToList(), 
                (name, firstVal, secondVal) => setsCollection.Count(set => set.Contains(firstVal) && set.Contains(secondVal)));

            var minSets = supports.Where(x => x.Value >= 2).ToDictionary(x => x.Key);

            double com = combinedSets.Count();
            double support = minSets["3-4"].Value / com;

            double freq = frequencies.Count(x => x.Value == 3);
            double confidence = support/(freq);



            Console.ReadLine();
        }

        private static Dictionary<string, int> GenerateCandidates(IList<Item> minSupportReached, Func<string, int, int, int> func)
        {
            var candidateSupport = new Dictionary<string, int>();

            for (int i = 0; i < minSupportReached.Count; i++)
            {
                for (int j = i + 1; j < minSupportReached.Count; j++)
                {
                    var candidateName = minSupportReached[i].Name + "-" + minSupportReached[j].Name;
                    int support = func(candidateName, minSupportReached[i].Value, minSupportReached[j].Value);
                    candidateSupport.Add(candidateName, support);
                }
            }

            return candidateSupport;
        }

        private static IEnumerable<Item> GetFrequencies(IEnumerable<int> dataSet)
        {



            var result = dataSet.Distinct().Select(x => new Item
                                                        {
                                                            Value = x,
                                                            Name = x.ToString(),
                                                            Frequency = dataSet.Count(y => y == x)
                                                        });


            return result;
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public int Frequency { get; set; }

        public int Value { get; set; }
    }
}
