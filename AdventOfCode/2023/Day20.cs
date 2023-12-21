using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{

    internal class Y2023D20
    {
        static List<Module> Modules = new();
        static int LowPulses = 0;
        static int HighPulses = 0;

        class PulseMessage
        {
            public string? Startingpoint;
            public string Pulse;
            public List<string> Destinations = new List<string>();

            public PulseMessage(string pulse, List<string> destinations)
            {
                Pulse = pulse;
                Destinations = destinations;
            }

            public PulseMessage(string pulse, List<string> destinations, string StartingPoint)
            {
                Pulse = pulse;
                Destinations = destinations;
                Startingpoint = StartingPoint;
            }


        }

        abstract class Module
        {
            public string name;
            public List<string> Outputs;
            public string type;

            public virtual PulseMessage? SendSignal(PulseMessage Pulse)
            {
                return null;
            }
        }
        class Flip : Module
        {
            bool state;
            
            public Flip(string name, List<string> Outputs)
            {
                this.name = name;
                this.Outputs = Outputs;
                this.type = "Flip";
               
            }

            public override PulseMessage? SendSignal(PulseMessage Pulse) { 

                if(Pulse.Pulse == "high")
                {
                    return null;
                }

                if (state == false)
                {
                    state = true;
                    HighPulses += Outputs.Count;
                    return new("high", Outputs,name);
                }
                else {
                    state = false;
                    LowPulses += Outputs.Count;
                    return new("low", Outputs,name);
                }
            
            }


        }
        class Conjunction : Module
        {

            public Dictionary<string, string> WatchList = new Dictionary<string, string>();
            bool State = false;
            public Conjunction(string name, List<string> Outputs)
            {
                this.name = name;
                this.Outputs = Outputs;
                this.type = "Con";
            }
            public override PulseMessage SendSignal(PulseMessage Pulse)
            {
              
                WatchList[Pulse.Startingpoint] = Pulse.Pulse;
                if (WatchList.Values.All(P => P == "high")){
                    State = true;
                }
                else {
                    State = false;
                }
                if (State)
                {
                    LowPulses += Outputs.Count;
                    return new("low", Outputs,name);
                }
                else {
                    HighPulses += Outputs.Count;
                    return new("high", Outputs,name);
                }

            }
        }

        class Broadcast : Module
        {
            public Broadcast(string name, List<string> Outputs)
            {
                this.name = name;
                this.Outputs = Outputs;
                this.type = "Broad";
            }

            public override PulseMessage SendSignal(PulseMessage Pulse)
            {
               
                LowPulses += Outputs.Count;
                return new("low", Outputs, name);

            }
        }


        public static string[] Lines = File.ReadAllLines(".\\2023\\Input\\inputDay20.txt");

        public long Part1()
        {
            long answer = 0;

            foreach (var line in Lines)
            {
                string name;
                char type;
                var part = line.Split(" -> ");
                if (part[0] == "broadcaster")
                {
                    name = "broadcaster";
                    type = 'B';
                }
                else {
                    name = part[0][1..];
                    type = part[0][0];

                }

                List<string> Outputs = part[1].Split(", ").ToList();

                switch (type) {

                    case 'B':
                        {
                            Modules.Add(new Broadcast(name, Outputs));
                            break;
                        }
                    case '%':
                        Modules.Add(new Flip(name, Outputs));
                        break;
                    case '&':
                        Modules.Add(new Conjunction(name, Outputs));
                        break;
                }
            }

            foreach (Conjunction conj in Modules.Where(t => t.type == "Con")) {
                Modules.Where(m => m.Outputs.Contains(conj.name)).ToList().ForEach(t =>
                {
                    conj.WatchList.Add(t.name, "low");
                });
            }

            
            long times = 0;
            while (times < 1000)
            {
                Queue<PulseMessage> Messages = new();
                Messages.Enqueue(new("Low", ["broadcaster"]));
                LowPulses++;
                while (Messages.Count > 0)
                {
                    PulseMessage message = Messages.Dequeue();
                    foreach (var destination in message.Destinations)
                    {
                        Module module = Modules.Find(m => m.name == destination);
                        if(module == null)
                        {
                            continue;
                        }

                        PulseMessage? NewMessage = module.SendSignal(new(message.Pulse, [destination], message.Startingpoint));


                        if (NewMessage != null)
                        {
                            Messages.Enqueue(NewMessage);
                        }

                    }
                }
                times++;
            }

            answer = HighPulses * LowPulses;
            return answer;
        }

        public long Part2()
        {
            long answer = 1;

            Modules = new();
            foreach(var line in Lines)
            {
                string name;
                char type;
                var part = line.Split(" -> ");
                if (part[0] == "broadcaster")
                {
                    name = "broadcaster";
                    type = 'B';
                }
                else
                {
                    name = part[0][1..];
                    type = part[0][0];

                }

                List<string> Outputs = part[1].Split(", ").ToList();

                switch (type)
                {

                    case 'B':
                        {
                            Modules.Add(new Broadcast(name, Outputs));
                            break;
                        }
                    case '%':
                        Modules.Add(new Flip(name, Outputs));
                        break;
                    case '&':
                        Modules.Add(new Conjunction(name, Outputs));
                        break;
                }
            }

            foreach (Conjunction conj in Modules.Where(t => t.type == "Con"))
            {
                Modules.Where(m => m.Outputs.Contains(conj.name)).ToList().ForEach(t =>
                {
                    conj.WatchList.Add(t.name, "low");
                });
            }

            Module toRX = Modules.Find(m => m.Outputs.Contains("rx"));
            List<Module> WatchModules = Modules.Where(m => m.Outputs.Contains(toRX.name)).ToList();

            Dictionary<string, List<long>> watchlist = new Dictionary<string, List<long>>();
            foreach (Module Watchmodule in WatchModules)
            {
                watchlist.Add(Watchmodule.name, new List<long>());
            }

            long times = 0;
            while (true)
            {
                Queue<PulseMessage> Messages = new();
                Messages.Enqueue(new("Low", ["broadcaster"]));
                LowPulses++;
                times++;
                while (Messages.Count > 0)
                {
                    PulseMessage message = Messages.Dequeue();
                    foreach (var destination in message.Destinations)
                    {
                        Module module = Modules.Find(m => m.name == destination);

                        if (destination == toRX.name && message.Pulse == "high"  )
                        {
                            watchlist.TryGetValue(message.Startingpoint, out List<long>? value);
                            value.Add(times);
                            if(watchlist.Values.All(l=> l.Count > 0)) {
                                foreach(var entry in watchlist.Values)
                                {
                                    answer *= entry[0];
                                }
                                goto done;
                            }
                        }

                        if (module == null)
                        {
                            if (message.Pulse == "low")
                            {
                                goto done;
                            }
                            else {
                                continue;
                            }
                            
                        }

                        PulseMessage? NewMessage = module.SendSignal(new(message.Pulse, [destination], message.Startingpoint));
                        if (NewMessage != null)
                        {
                            Messages.Enqueue(NewMessage);
                        }
                    }
                }
            }
            done:

            return answer;
        }

    }
}
