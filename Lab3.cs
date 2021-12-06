using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace TestConsole
{
    class Program
    {

        class Graph
        {
            public Dictionary<Tuple<int, int, int>, List<Tuple<int, int, int>>> adj;

            public Graph()
            {
                adj = new Dictionary<Tuple<int, int, int>, List<Tuple<int, int, int>>>();
            }

            public int bfs(int b1, int b2, int b3, int t)
            {
                var queue = new Queue<Tuple<Tuple<int, int, int>, int>>();
                HashSet<Tuple<int, int, int>> visited = new HashSet<Tuple<int, int, int>>();

                queue.Enqueue(new Tuple<Tuple<int, int, int>, int>(new Tuple<int, int, int>(b1, b2, b3), 0));
                while(queue.Count > 0)
                {
                    var pair = queue.Dequeue();
                    var current_vertex = pair.Item1;
                    var depth = pair.Item2;
                    if(current_vertex.Item1 == t)
                    {
                        return depth;
                    }
                    visited.Add(current_vertex);
                    foreach(var v in adj[current_vertex])
                    {
                        if (!visited.Contains(v))
                        {
                            queue.Enqueue(new Tuple<Tuple<int,int,int>, int>(v, depth+1));
                            visited.Add(v);
                        }
                    }
                }
                return -1;
            }

        }



        static Graph build_graph(int b1, int b2, int b3)
        {
            Graph g = new Graph();
            var queue = new Queue<Tuple<int, int, int>>();
            queue.Enqueue(new Tuple<int, int, int>(b1, 0, 0));
            while(queue.Count > 0)
            {
                int dif, out_liters, in_liters;
                Tuple<int, int, int> next_vertex;
                var current_vertex = queue.Dequeue();
                if (g.adj.ContainsKey(current_vertex))
                {
                    continue;
                }
                g.adj.Add(current_vertex, new List<Tuple<int, int, int>>());

                //Спробуємо перелити з першого відра в друге
                out_liters = current_vertex.Item1; // скільки можна вилити з першого відра
                in_liters = b2 - current_vertex.Item2;//скільки можна влити в друге відро
                dif = Math.Min(out_liters, in_liters);
                if(dif > 0)
                {
                    next_vertex = new Tuple<int, int, int>(current_vertex.Item1 - dif, current_vertex.Item2 + dif, current_vertex.Item3);
                    g.adj[current_vertex].Add(next_vertex);
                    if (!g.adj.ContainsKey(next_vertex))
                    {
                        //якщо такої вершини в графі ще немає, додаємо її в чергу
                        queue.Enqueue(next_vertex);
                    }
                }

                //якби ми перелили з першого відра в третє
                dif = Math.Min(current_vertex.Item1, b3 - current_vertex.Item3);
                if (dif > 0)
                {
                    next_vertex = new Tuple<int, int, int>(current_vertex.Item1 - dif, current_vertex.Item2, current_vertex.Item3 + dif);
                    g.adj[current_vertex].Add(next_vertex);
                    if (!g.adj.ContainsKey(next_vertex))
                    {
                        queue.Enqueue(next_vertex);
                    }
                }

                //якби ми перелили з другого відра в третє
                dif = Math.Min(current_vertex.Item2, b3 - current_vertex.Item3);
                if (dif > 0)
                {
                    next_vertex = new Tuple<int, int, int>(current_vertex.Item1, current_vertex.Item2 - dif, current_vertex.Item3 + dif);
                    g.adj[current_vertex].Add(next_vertex);
                    if (!g.adj.ContainsKey(next_vertex))
                    {
                        queue.Enqueue(next_vertex);
                    }
                }

                //якби ми перелили з другого відра в перше
                dif = Math.Min(current_vertex.Item2, b1 - current_vertex.Item1);
                if (dif > 0)
                {
                    next_vertex = new Tuple<int, int, int>(current_vertex.Item1 + dif, current_vertex.Item2 - dif, current_vertex.Item3);
                    g.adj[current_vertex].Add(next_vertex);
                    if (!g.adj.ContainsKey(next_vertex))
                    {
                        queue.Enqueue(next_vertex);
                    }
                }

                //якби ми перелили з третього відра в перше
                dif = Math.Min(current_vertex.Item3, b1 - current_vertex.Item1);
                if (dif > 0)
                {
                    next_vertex = new Tuple<int, int, int>(current_vertex.Item1 + dif, current_vertex.Item2, current_vertex.Item3 - dif);
                    g.adj[current_vertex].Add(next_vertex);
                    if (!g.adj.ContainsKey(next_vertex))
                    {
                        queue.Enqueue(next_vertex);
                    }
                }

                //якби ми перелили з третього відра в друге
                dif = Math.Min(current_vertex.Item3, b2 - current_vertex.Item2);
                if (dif > 0)
                {
                    next_vertex = new Tuple<int, int, int>(current_vertex.Item1, current_vertex.Item2 + dif, current_vertex.Item3 - dif);
                    g.adj[current_vertex].Add(next_vertex);
                    if (!g.adj.ContainsKey(next_vertex))
                    {
                        queue.Enqueue(next_vertex);
                    }
                }

            }

            return g;

        }
        
        static void Main(string[] args)
        {
            string numbers;
            string stringResult = "IMPOSSIBLE";
            using (StreamReader reader = new StreamReader("input.txt"))
            {
                numbers = reader.ReadToEnd();
            }
            var nums = numbers.Split().ToList();
            if(nums.Count != 4)
            {
                using(StreamWriter writer = new StreamWriter("output.txt"))
                {
                    writer.WriteLine(stringResult);
                }
                return;
            }
            var b1 = int.Parse(nums[0]);
            var b2 = int.Parse(nums[1]);
            var b3 = int.Parse(nums[2]);
            var t = int.Parse(nums[3]);

            var graph = build_graph(b1, b2, b3);
            var result = graph.bfs(b1, 0, 0, t);
            if(result >= 0)
            {
                stringResult = result.ToString();
            }
            using(StreamWriter writer = new StreamWriter("output.txt"))
            {
                writer.WriteLine(stringResult);
            }
        }
    }
}
