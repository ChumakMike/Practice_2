using lab2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lab2.Calculators
{
    internal class NumberOfChildrenCalculator : ICalculator
    {
        public static void Calculate(List<(Metrics.Metrics, List<(string className, int count)>)> metricsDictionary,
            List<string> classesNamesList, List<(string parent, string child)> parentToChildRelationList)
        {
            var result = new List<(string className, int count)>();

            foreach (string className in classesNamesList)
                result.Add(new ValueTuple<string, int>(className, GetChildrenCount(className, parentToChildRelationList)));

            metricsDictionary.Add((Metrics.Metrics.NumberOfChildren, result.OrderByDescending(x => x.count).ToList()));
        }

        private static int GetChildrenCount(string className, List<(string parent, string child)> parentToChildRelationList)
            => parentToChildRelationList.Count(x => x.parent.Equals(className));
    }
}
