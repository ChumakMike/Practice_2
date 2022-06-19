using System;
using System.Collections.Generic;

namespace lab2.Interfaces
{
    public interface ICalculator
    {
        static void Calculate(Dictionary<Metrics.Metrics, double> metricsDictionary) =>
            throw new NotImplementedException();
        static void Calculate(List<(Metrics.Metrics, List<(string className, int count)>)> metricsDictionary,
            List<string> classesNamesList, List<(string parent, string child)> parentToChildRelationList) => throw new NotImplementedException();
    }
}
