using lab2.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace lab2.Calculators
{
    internal class PolymorphismObjectFactorCalculator : ICalculator
    {
        public static void Calculate(Dictionary<Metrics.Metrics, double> metricsDictionary)
        {
            int overridenMethods = 0;
            int multipliedNewlyDeclaredMethodsWithChildrenCount = 0;

            var types = Assembly.LoadFrom(Constants.PathToDll).GetTypes();

            var notAbleToInheritTypes = types.Where(x => !x.IsAbstract && !x.IsInterface).ToList();

            foreach (var type in types)
            {
                int newlyDeclaredMethods = 0;

                var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                              BindingFlags.Static | BindingFlags.DeclaredOnly |
                                              BindingFlags.FlattenHierarchy);
                foreach (var method in methods)
                {
                    var reflectedType = method.GetBaseDefinition().ReflectedType;
                    if (method.ReflectedType != null && reflectedType != null && reflectedType.Name != method.ReflectedType.Name)
                        overridenMethods++;
                    else newlyDeclaredMethods++;
                }

                multipliedNewlyDeclaredMethodsWithChildrenCount += newlyDeclaredMethods * notAbleToInheritTypes.Count(x => type.IsAssignableFrom(x));
            }

            metricsDictionary.Add(Metrics.Metrics.PolymorphismObjectFactor,
                multipliedNewlyDeclaredMethodsWithChildrenCount == 0 ? 0 : (double)overridenMethods / multipliedNewlyDeclaredMethodsWithChildrenCount);
        }
    }
}
