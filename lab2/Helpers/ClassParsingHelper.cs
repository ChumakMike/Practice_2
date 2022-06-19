using lab2.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace lab2.Helpers
{
    internal class ClassParsingHelper
    {
        public List<string> ClassesNamesList { get; set; }
        public List<(string parent, string child)> ParentToChildRelationList { get; set; }

        public void ParseClasses(string parsedString)
        {
            List<string> allClasses = GetMatches(parsedString, "class\\s*[a-zA-Z]*\\s*[:{]");
            ClassesNamesList = new List<string>();

            foreach (var inheritedClass in allClasses)
            {
                if (inheritedClass.Length < 8)
                    continue;

                string className = inheritedClass.Substring(6);

                ClassesNamesList.AddValue(className.Remove(className.Length - 2).Trim());
            }
        }

        public void ParseInheritedClasses(string parsedString)
        {
            List<string> inheritedClasses = GetMatches(parsedString, "class\\s*[a-zA-Z]*\\s*:\\s*[a-zA-Z]*");
            ParentToChildRelationList = new List<(string parent, string child)>();

            foreach (var inheritedClass in inheritedClasses)
            {
                if (inheritedClass.Length < 7)
                    continue;

                var classNames = inheritedClass.Substring(6).Split(':');

                if (classNames.Length != 2)
                    continue;

                if (ClassesNamesList.Contains(classNames.Last().Trim()))
                    ParentToChildRelationList.Add((classNames.Last().Trim(), classNames.First().Trim()));
            }
        }

        private List<string> GetMatches(string parsedString, string pattern)
        {
            var rgx = new Regex(pattern);

            List<string> result = new List<string>();

            Match firstMatch = rgx.Match(parsedString);
            if (firstMatch.Success)
            {
                result.Add(firstMatch.Value);
                foreach (Match m in rgx.Matches(parsedString, firstMatch.Index + firstMatch.Length))
                    result.Add(m.Value);
            }

            return result;
        }
    }
}
