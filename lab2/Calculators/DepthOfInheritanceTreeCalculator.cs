using lab2.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace lab2.Calculators
{
    internal class DepthOfInheritanceTreeCalculator : ICalculator
    {
        public static void Calculate(List<(Metrics.Metrics, List<(string className, int count)>)> metricsDictionary,
            List<string> classesNamesList, List<(string parent, string child)> parentToChildRelationList)
        {
            var result = new List<(string className, int depth)>();

            foreach (var className in classesNamesList)
                result.Add((className, GetDepthRecursive(className, parentToChildRelationList)));


            metricsDictionary.Add((Metrics.Metrics.DepthOfInheritanceTree, result.OrderByDescending(x => x.depth).ToList()));
        }

        private static int GetDepthRecursive(string parentName, List<(string parent, string child)> parentToChildRelationList)
        {
            List<(string parent, string child)> sortedByParentList = GetChildren(parentName, parentToChildRelationList).ToList();

            if (!sortedByParentList.Any())
                return 1;

            int maxDepth = 0;

            foreach (var node in sortedByParentList)
            {
                int depth = GetDepthRecursive(node.child, parentToChildRelationList) + 1;
                if (maxDepth < depth)
                    maxDepth = depth;
            }

            return maxDepth;
        }

        private static IEnumerable<(string, string)> GetChildren(string className, List<(string parent, string child)> parentToChildRelationList)
            => parentToChildRelationList.Where(x => x.parent.Equals(className));
    }
}
