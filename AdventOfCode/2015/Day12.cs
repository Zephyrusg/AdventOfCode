using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2015D12
    {
        public static string Lines = File.ReadAllText(".\\2015\\Input\\inputDay12.txt");

        public int Part1()
        {
            int SumNumbers(JsonElement json)
            {
                switch (json.ValueKind)
                {
                    case JsonValueKind.Number:
                        return json.GetInt32();
                    case JsonValueKind.Array:
                        return json.EnumerateArray().Sum(SumNumbers);
                    case JsonValueKind.Object:
                        return json.EnumerateObject().Sum(property => SumNumbers(property.Value));
                    default:
                        return 0;
                }
            }

            var input = string.Join("", Lines);
            var json = JsonDocument.Parse(input).RootElement;

            return SumNumbers(json);
        }

        public int Part2()
        {
            int SumNumbersIgnoreRed(JsonElement json)
            {
                switch (json.ValueKind)
                {
                    case JsonValueKind.Number:
                        return json.GetInt32();
                    case JsonValueKind.Array:
                        return json.EnumerateArray().Sum(SumNumbersIgnoreRed);
                    case JsonValueKind.Object:
                        if (json.EnumerateObject().Any(property => property.Value.ValueKind == JsonValueKind.String && property.Value.GetString() == "red"))
                        {
                            return 0;
                        }
                        return json.EnumerateObject().Sum(property => SumNumbersIgnoreRed(property.Value));
                    default:
                        return 0;
                }
            }

            var input = string.Join("", Lines);
            var json = JsonDocument.Parse(input).RootElement;

            return SumNumbersIgnoreRed(json);
        }

    }
}
