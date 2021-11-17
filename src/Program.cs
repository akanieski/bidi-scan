using System;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using GlobExpressions;
namespace BidirectionalUnicodeScanner
{
    class Program
    {
        static int Main(string[] args)
        {
            var files = Glob.Files(Environment.CurrentDirectory, args[0], GlobOptions.CaseInsensitive);
            var found = false;
            var total = 0;
            foreach (var file in files)
            {
                Console.WriteLine($"Scanning [{file}]..");
                var matchSets = new UnicodeMatch[]
                {
                new UnicodeMatch('\u202A', "LEFT-TO-RIGHT EMBEDDING"),
                new UnicodeMatch('\u202B', "RIGHT-TO-LEFT EMBEDDING"),
                new UnicodeMatch('\u202C', "POP DIRECTIONAL FORMATTING"),
                new UnicodeMatch('\u202D', "LEFT-TO-RIGHT OVERRIDE"),
                new UnicodeMatch('\u202E', "RIGHT-TO-LEFT OVERRIDE"),
                new UnicodeMatch('\u2066', "LEFT-TO-RIGHT ISOLATE"),
                new UnicodeMatch('\u2067', "RIGHT-TO-LEFT ISOLATE"),
                new UnicodeMatch('\u2068', "FIRST STRONG ISOLATE"),
                new UnicodeMatch('\u2069', "POP DIRECTIONAL ISOLATE"),
                };
                var fileContent = File.ReadAllLines(file);
                var matches = new ConcurrentBag<(int, int, UnicodeMatch)>();
                var parallelConfig = new ParallelOptions();
                parallelConfig.MaxDegreeOfParallelism = Environment.ProcessorCount;

                var _ = Parallel.For(0, fileContent.Count(), parallelOptions: parallelConfig, line_number =>
                {

                    var line = fileContent[line_number];

                    foreach (var uniMatcher in matchSets)
                    {
                        var indexes = new List<int>();
                        int index = line.IndexOf(uniMatcher.MatchExpression);
                        while (index >= 0)
                        {
                            matches.Add((line_number + 1, index, uniMatcher));
                            index = line.IndexOf(uniMatcher.MatchExpression, index + 1);
                        }
                    }

                });

                foreach (var m in matches.OrderBy(m => m.Item1).ThenBy(m => m.Item2))
                {
                    Console.WriteLine($"Found [{m.Item3.Description}] in [{file}] line {m.Item1}:{m.Item2}");
                }
                if (matches.Count() > 0) found = true;
                total += matches.Count();
            }
            Console.WriteLine($"Done. Found total of {total} matches in {files.Count()} files.");
            return found ? 1 : 0;
        }
    }
}
