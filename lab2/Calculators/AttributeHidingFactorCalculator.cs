using lab2.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace lab2.Calculators
{
    internal class AttributeHidingFactorCalculator : ICalculator
    {
        public static void Calculate(Dictionary<Metrics.Metrics, double> metricsDictionary)
        {
            int privateFields = 0;
            int publicFields = 0;

            var types = Assembly.LoadFrom(Constants.PathToDll).GetTypes();

            foreach (var type in types)
            {
                var fieldsList = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                                BindingFlags.Static | BindingFlags.DeclaredOnly |
                                                BindingFlags.FlattenHierarchy);
                privateFields += fieldsList.Count(x => x.IsPrivate);
                publicFields += fieldsList.Count(x => x.IsPublic);
            }

            metricsDictionary.Add(Metrics.Metrics.AttributeHidingFactor, privateFields + publicFields == 0 ? 0 : (double)privateFields / (privateFields + publicFields));
        }
    }
}
