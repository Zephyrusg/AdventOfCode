using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2024D24
    {
        public static string[] Lines = File.ReadAllLines(".\\2024\\Input\\inputDay24.txt");
        public List<Wire> WireList = new List<Wire>();
        private Dictionary<string, Wire> ParseWires(string[] lines, Dictionary<string, int> wireValues)
        {
            var wires = new Dictionary<string, Wire>();
            
            foreach (var line in lines)
            {
                if (line.Contains(":"))
                {
                    // Wire initialization
                    var parts = line.Split(':', StringSplitOptions.TrimEntries);
                    wireValues[parts[0]] = int.Parse(parts[1]);
                }
                else if (!string.IsNullOrWhiteSpace(line))
                {
                    // Gate definition
                    var parts = line.Split(new[] { " -> " }, StringSplitOptions.None);
                    var operationParts = parts[0].Split(' ');

                    var wire = new Wire
                    {
                        Name = parts[1],
                        Input1 = operationParts[0],
                        Operation = operationParts[1],
                        Input2 = operationParts[2],
                        IsOutput = false
                    };
                    WireList.Add(wire);
                    wires[wire.Name] = wire;
                }
            }

            return wires;
        }

        public long Part1()
        {
            var wireValues = new Dictionary<string, int>();
            var gates = new List<string>();

            // Parse input
            foreach (var line in Lines)
            {
                if (line.Contains(":"))
                {
                    // Wire initialization (e.g., "x00: 1")
                    var parts = line.Split(':', StringSplitOptions.TrimEntries);
                    wireValues[parts[0]] = int.Parse(parts[1]);
                }
                else if (!string.IsNullOrWhiteSpace(line))
                {
                    // Gate definition (e.g., "x00 AND y00 -> z00")
                    gates.Add(line);
                }
            }

            // Process gates until all values are resolved
            bool updated;
            do
            {
                updated = false;

                foreach (var gate in gates.ToList())
                {
                    // Parse the gate
                    var parts = gate.Split(new[] { " -> " }, StringSplitOptions.None);
                    var operation = parts[0];
                    var outputWire = parts[1];

                    var opParts = operation.Split(' ');

                    // Identify the type of gate and input wires
                    if (opParts.Length == 3)
                    {
                        var input1 = opParts[0];
                        var input2 = opParts[2];
                        var op = opParts[1];

                        // Ensure inputs are ready
                        if (wireValues.ContainsKey(input1) && wireValues.ContainsKey(input2))
                        {
                            int value1 = wireValues[input1];
                            int value2 = wireValues[input2];
                            int output = op switch
                            {
                                "AND" => value1 & value2,
                                "OR" => value1 | value2,
                                "XOR" => value1 ^ value2,
                                _ => throw new InvalidOperationException("Unknown operation: " + op)
                            };

                            wireValues[outputWire] = output;
                            gates.Remove(gate);
                            updated = true;
                        }
                    }
                }
            } while (updated);

            // Combine outputs from wires starting with 'z' into a binary number
            var binaryNumber = string.Join("",
                wireValues
                    .Where(kvp => kvp.Key.StartsWith("z"))
                    .OrderBy(kvp => kvp.Key)
                    .Select(kvp => kvp.Value.ToString())
                    .Reverse());

            // Convert binary to decimal
            return Convert.ToInt64(binaryNumber, 2);
        }

        public class Wire
        {
            public required string Name { get; set; }     
            public required string Input1 { get; set; }      
            public required string Input2 { get; set; }       
            public required string Operation { get; set; }   
            public bool IsOutput { get; set; }              
        }

        public string Part2()
        {
            int answer = 0;
            
            var wireValues = new Dictionary<string, int>();
            var wires = ParseWires(Lines, wireValues);


            HashSet<string> FaultyOutput = new();
            foreach (var wire in WireList)
            {
                if(wire.Name == "z45" || wire.Name == "z00" || wire.Input1 == "x00" || wire.Input2 == "x00")
                {
                    continue;
                }


                switch (wire.Operation)
                {
                    case "AND":
                        {
                            if (wire.Name[0] == 'z')
                            {
                                FaultyOutput.Add(wire.Name);
                            }
                            if (wires.Where(w => (w.Value.Input1 == wire.Name || w.Value.Input2 == wire.Name) && w.Value.Operation == "OR").Count() != 1)
                            {
                                FaultyOutput.Add(wire.Name);
                            }


                            break;
                        }
                    case "OR":
                        {
                            if (wire.Name[0] == 'z')
                            {
                                FaultyOutput.Add(wire.Name);
                            }

                            break;
                        }
                    case "XOR":
                        {
                            if ((wire.Input1[0] == 'x' || wire.Input1[0] == 'y') && (wire.Input2[0] == 'x' || wire.Input2[0] == 'y'))
                            {
                                if (wire.Name[0] == 'z')
                                {
                                    FaultyOutput.Add(wire.Name);

                                }

                                if (wires.Where(w => (w.Value.Input1 == wire.Name || w.Value.Input2 == wire.Name) && w.Value.Operation == "XOR").Count() != 1)
                                {
                                    FaultyOutput.Add(wire.Name);
                                }


                            }
                            else if ((wire.Input1[0] != 'x' || wire.Input1[0] != 'y' && wire.Input2[0] != 'x' || wire.Input2[0] != 'y'))
                            {
                                if (wire.Name[0] != 'z')
                                {
                                    FaultyOutput.Add(wire.Name);
                                }
                            }
                            break;
                        }
                }
            }
            return string.Join(',', FaultyOutput.Order());
        }
    }
}
