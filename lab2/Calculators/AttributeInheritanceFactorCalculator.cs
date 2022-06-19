using lab2.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace lab2.Calculators
{
    internal class AttributeInheritanceFactorCalculator : ICalculator
    {
        public static void Calculate(Dictionary<Metrics.Metrics, double> metricsDictionary)
        {
            int overridenProperties = 0;
            int allProperties = 0;

            var types = Assembly.LoadFrom(Constants.PathToDll).GetTypes();

            foreach (var type in types)
            {
                allProperties += type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                                BindingFlags.Static | BindingFlags.FlattenHierarchy).Length;
                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                                BindingFlags.Static | BindingFlags.DeclaredOnly |
                                                BindingFlags.FlattenHierarchy);

                overridenProperties += properties.Count(x =>
                {
                    var reflectedType = x.GetGetMethod(false);
                    return x.ReflectedType != null && reflectedType != null && reflectedType.Name != x.ReflectedType.Name;
                });
            }

            metricsDictionary.Add(Metrics.Metrics.AttributeInheritanceFactor, allProperties == 0 ? 0 : (double)(allProperties - overridenProperties) / allProperties);
        }
    }
}
