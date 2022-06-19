using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace lab2.Helpers
{
    internal class Parser
    {
        public string AllFilesParsed { get; set; }
        public void ParseAllFilesInDirectory()
        {
            var listing = new List<string>();
            var files = Directory.GetFiles(Constants.PathToCsFiles, "*.cs", SearchOption.AllDirectories);

            foreach (var file in files)
                listing.AddRange(File.ReadAllLines(file).ToList());

            AllFilesParsed = string.Join("\n", listing);
        }
    }
}
