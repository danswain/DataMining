﻿using System;
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

            var candidateSetsThatMeetMinimumSupport = supports.Where(x => x.Value >= 2).ToDictionary(x => x.Key);


            var candidateDictionary = new Dictionary<string, Result>();

            foreach (var candidate in candidateSetsThatMeetMinimumSupport)
            {
                //double support = candidateSetsThatMeetMinimumSupport["3-4"].Value;
                double support = candidate.Value.Value;
                //double probability3 = frequencies.Where(x => x.Name == "3").SingleOrDefault().Frequency;
                double probabilityA = frequencies.Where(x => x.Name == candidate.Key[0].ToString()).SingleOrDefault().Frequency;

                //double probability4 = frequencies.Where(x => x.Name == "4").SingleOrDefault().Frequency;
                double probabilityB = frequencies.Where(x => x.Name == candidate.Key[2].ToString()).SingleOrDefault().Frequency;

                //double probability3_4 = support;
                double probabilityA_B = support;

                //Final
                //double confidence = probability3_4 / probability3;

                double confidence = probabilityA_B / probabilityA;

                //double lift = confidence / (probability3 / setsCollection.Count * probability4 / setsCollection.Count);
                double lift = confidence / (probabilityA / setsCollection.Count * probabilityB / setsCollection.Count);

                candidateDictionary.Add(candidate.Key,new Result
                                                          {
                                                              Name = candidate.Key,
                                                              Confidence = confidence,
                                                              Lift = lift
                                                          });

            }




            foreach (var result in candidateDictionary)
            {
                if(result.Value.Lift >1)
                Console.WriteLine(string.Format("{0} POSITIVELY CORRELATED Confidence->{1} Lift->{2}",result.Value.Name,result.Value.Confidence,result.Value.Lift));
                if (result.Value.Lift == 1)
                    Console.WriteLine(string.Format("{0} ARE INDEPENDANT Confidence->{1} Lift->{2}",result.Value.Name,result.Value.Confidence,result.Value.Lift));
                if (result.Value.Lift < 1)
                    Console.WriteLine(string.Format("{0} NEGATIVELY CORRELATED Confidence->{1} Lift->{2}", result.Value.Name, result.Value.Confidence, result.Value.Lift));
            }            

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

    public class Result
    {
        public double Lift { get; set; }
        public double Confidence { get; set; }
        public string Name { get; set; }
    }

    public class Item
    {
        public string Name { get; set; }
        public int Frequency { get; set; }

        public int Value { get; set; }
    }
}
