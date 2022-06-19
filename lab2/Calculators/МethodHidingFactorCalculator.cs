using lab2.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace lab2.Calculators
{
    internal class МethodHidingFactorCalculator : ICalculator
    {
        public static void Calculate(Dictionary<Metrics.Metrics, double> metricsDictionary)
        {
            int privateMethods = 0;
            int publicMethods = 0;

            var types = Assembly.LoadFrom(Constants.PathToDll).GetTypes();

            foreach (var type in types)
            {
                var methodsList = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                                 BindingFlags.Static | BindingFlags.DeclaredOnly |
                                                 BindingFlags.FlattenHierarchy);
                privateMethods += methodsList.Count(x => x.IsPrivate);
                publicMethods += methodsList.Count(x => x.IsPublic);
            }

            metricsDictionary.Add(Metrics.Metrics.МethodHidingFactor, privateMethods + publicMethods == 0 ? 0 : (double)privateMethods / (privateMethods + publicMethods));
        }
    }
}
