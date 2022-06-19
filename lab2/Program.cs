using lab2.Calculators;
using lab2.Helpers;
using System;
using System.Collections.Generic;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            var metricsDictionary = new Dictionary<Metrics.Metrics, double>();

            AttributeHidingFactorCalculator.Calculate(metricsDictionary);
            AttributeInheritanceFactorCalculator.Calculate(metricsDictionary);
            MethodInheritanceFactorCalculator.Calculate(metricsDictionary);
            PolymorphismObjectFactorCalculator.Calculate(metricsDictionary);
            МethodHidingFactorCalculator.Calculate(metricsDictionary);

            PrintMetricsDictionary(metricsDictionary);

            var metricsDictionaryForRelations = new List<(Metrics.Metrics, List<(string className, int count)>)>();

            var parser = new Parser();
            var parsingHelper = new ClassParsingHelper();

            parser.ParseAllFilesInDirectory();
            parsingHelper.ParseClasses(parser.AllFilesParsed);
            parsingHelper.ParseInheritedClasses(parser.AllFilesParsed);

            NumberOfChildrenCalculator.Calculate(metricsDictionaryForRelations,
                parsingHelper.ClassesNamesList, parsingHelper.ParentToChildRelationList);
            DepthOfInheritanceTreeCalculator.Calculate(metricsDictionaryForRelations,
                parsingHelper.ClassesNamesList, parsingHelper.ParentToChildRelationList);

            PrintMetricsDictionary(metricsDictionaryForRelations);
            Console.ReadKey();
        }

        private static void PrintMetricsDictionary(Dictionary<Metrics.Metrics, double> dictionary)
        {
            foreach (var item in dictionary)
                Console.WriteLine("[ Metric: " + item.Key + " ]" + " = " + item.Value);
        }

        private static void PrintMetricsDictionary(List<(Metrics.Metrics metric, List<(string className, int count)>)> dictionary)
        {
            foreach (var item in dictionary)
            {
                Console.WriteLine(" ");
                Console.WriteLine("[ Metric: " + item.metric + " ]");
                foreach (var relation in item.Item2)
                {
                    Console.WriteLine('\t' + relation.className + " : " + relation.count);
                }
            }

        }
    }
}
