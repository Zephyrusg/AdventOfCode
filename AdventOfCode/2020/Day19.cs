using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2020D19
    {
        public static string[] Rules = File.ReadAllLines(".\\2020\\Input\\InputDay19Rules.txt");
        public static string[] Messages = File.ReadAllLines(".\\2020\\Input\\InputDay19Entries.txt");

        public int Part1()
        {
            // Parse the rules into a dictionary
            Dictionary<int, string> rules = ParseRules(Rules);

            // Generate regex pattern for rule 0
            string rule0Pattern = "^" + ResolveRule(rules, 0) + "$";

            // Count valid messages for rule 0
            int validCount = Messages.Count(message => Regex.IsMatch(message, rule0Pattern));

            return validCount;
        }

        public int Part2()
        {
            // Parse the rules into a dictionary
            Dictionary<int, string> rules = ParseRules(Rules);

            // Update rules for part 2
            rules[8] = "42 | 42 8";
            rules[11] = "42 31 | 42 11 31";

            // Generate regex patterns for rules 42 and 31
            string rule42 = ResolveRule(rules, 42);
            string rule31 = ResolveRule(rules, 31);

            // Count valid messages for rule 0 with recursive rules
            int validCount = Messages.Count(message => MatchesRule0(message, rule42, rule31));

            return validCount;
        }

        static Dictionary<int, string> ParseRules(string[] rulesInput)
        {
            var rules = new Dictionary<int, string>();
            foreach (var line in rulesInput)
            {
                var parts = line.Split(": ", 2);
                rules[int.Parse(parts[0])] = parts[1];
            }
            return rules;
        }

        static string ResolveRule(Dictionary<int, string> rules, int ruleId)
        {
            string rule = rules[ruleId];
            if (rule.Contains('"')) // Base case: single character rule
            {
                return rule.Trim('"');
            }

            // Resolve sub-rules
            var options = rule.Split(" | ")
                              .Select(part => string.Join("",
                                  part.Split(' ').Select(subRule => ResolveRule(rules, int.Parse(subRule)))));

            return options.Count() > 1 ? $"({string.Join("|", options)})" : options.First();
        }

        static bool MatchesRule0(string message, string rule42, string rule31)
        {
            int chunkSize = Regex.Match(message, rule42).Value.Length;

            // Split the message into chunks of size matching rule 42
            var chunks = Enumerable.Range(0, message.Length / chunkSize)
                                    .Select(i => message.Substring(i * chunkSize, chunkSize))
                                    .ToArray();

            // Match chunks with rules 42 and 31
            var matches42 = chunks.Select(chunk => Regex.IsMatch(chunk, $"^{rule42}$")).ToArray();
            var matches31 = chunks.Select(chunk => Regex.IsMatch(chunk, $"^{rule31}$")).ToArray();

            // Ensure all rule 31 matches follow rule 42 matches
            int first31 = Array.IndexOf(matches31, true);
            if (first31 == -1) return false;

            if (!matches42.Take(first31).All(x => x) || !matches31.Skip(first31).All(x => x))
            {
                return false;
            }

            int count42 = first31;
            int count31 = matches31.Count(x => x);

            return count42 > count31;
        }
    }
}
