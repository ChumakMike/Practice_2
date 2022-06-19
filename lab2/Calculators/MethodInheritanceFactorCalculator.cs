using lab2.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace lab2.Calculators
{
    internal class MethodInheritanceFactorCalculator : ICalculator
    {
        public static void Calculate(Dictionary<Metrics.Metrics, double> metricsDictionary)
        {
            int overridenMethods = 0;
            int allMethods = 0;

            var types = Assembly.LoadFrom(Constants.PathToDll).GetTypes();

            foreach (var type in types)
            {
                allMethods += type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy).Length;
                var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                              BindingFlags.Static | BindingFlags.DeclaredOnly |
                                              BindingFlags.FlattenHierarchy);

                overridenMethods += methods.Count(x =>
                {
                    var reflectedType = x.GetBaseDefinition().ReflectedType;
                    return x.ReflectedType != null && reflectedType != null && reflectedType.Name != x.ReflectedType.Name;
                });
            }

            metricsDictionary.Add(Metrics.Metrics.MethodInheritanceFactor, allMethods == 0 ? 0 : (double)(allMethods - overridenMethods) / allMethods);
        }
    }
}
