using System;
using System.Collections;
using static Lab3_Ukleiko.TasksLab3;

namespace Lab3_Ukleiko
{
    public static class TasksLab3
    {
        static void Main(string[] args)
        {
            bool isInt = false;
            do
            {
                try
                {
                    Console.Title = ("MENU");
                    Console.Write("Lab 3, Ukleiko Ekaterina, 2 group, 3 course\n\n");
                    Console.Write("MENU\n\n");
                    Console.Write("Enter 1 - Task 3.1 \n");
                    Console.Write("Enter 2 - Task 3.2 \n");
                    Console.Write("Enter 3 - Task 3.3 \n");
                    Console.Write("Enter 4 - Task 3.4 \n");
                    Console.Write("Enter 5 - Task 3.5 \n\n");
                    Console.Write("Enter integer number, please: ");
                    int userChoice = Int32.Parse(Console.ReadLine());
                    Console.Clear();
                    switch (userChoice)
                    {
                        case 1:
                            try
                            {
                                Console.Title = ("TASK 3.1");
                                Graph g = new Graph(5);
                                g.AddEdge(1, 0);
                                g.AddEdge(0, 2);
                                g.AddEdge(2, 1);
                                g.AddEdge(0, 3);
                                g.AddEdge(3, 4);

                                Console.WriteLine("Connected components: ");
                                g.ConnectedComponents();

                                Console.WriteLine("Eulerian Cycle: ");
                                g.EulerianCycle(0);

                                Console.WriteLine("Bipartition: ");
                                g.Bipartition();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("ERROR: " + e.Message);
                            }
                            break;
                        case 2:
                            try
                            {
                                Console.Title = ("TASK 3.2");
                                Console.Write("Enter N - number of cross-roads: ");
                                int N = int.Parse(Console.ReadLine());

                                Console.Write("Enter M - number of roads: ");
                                int M = int.Parse(Console.ReadLine());

                                int[,] travelTimes = new int[N, N];

                                FireStationFinder finder = new FireStationFinder();
                                finder.FindStation(N, M, travelTimes);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("ERROR: " + e.Message);
                            }
                            break;
                        case 3:
                            try
                            {
                                Console.Title = ("TASK 3.3");

                                Console.Write("Algorithm 1 (Prim): ");
                                MinimumSpanningTree.primMST(MinimumSpanningTree.graph);

                                Console.Write("Algorithm 2 (Kruskal): ");

                                Console.Write("Enter V - number of verteces: ");
                                int V = int.Parse(Console.ReadLine());

                                Console.Write("Enter E - number of edges: ");
                                int E = int.Parse(Console.ReadLine());
                                Kruskal graph = new Kruskal(V, E);

                                graph.KruskalMST();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("ERROR: " + e.Message);
                            }
                            break;
                        case 4:
                            try
                            {
                                Console.Title = ("TASK 3.4");
                                Console.Write("Enter K - number of employees: ");
                                int K = int.Parse(Console.ReadLine());
                                ArrayList[] adj = new ArrayList[K];
                                for (int i = 0; i < K; i++)
                                {
                                    adj[i] = new ArrayList();
                                    for (int j = 0; j < K; j++)
                                    {
                                        adj[i].Add(BipartiteMatching.skills[i, j]);
                                    }
                                }

                                int maxMatches = BipartiteMatching.maxBPM(adj);
                                if (maxMatches == K)
                                {
                                    Console.WriteLine("You can assign all tasks to employees");
                                }
                                else
                                {
                                    Console.WriteLine("It is not possible to assign all tasks to employees");
                                    for (int i = 0; i < K; i++)
                                    {
                                        for (int j = 0; j < K; j++)
                                        {
                                            if (BipartiteMatching.skills[i, j] == 0 && adj[i][j].Equals(1))
                                            {
                                                Console.WriteLine("Employee " + i + " needs to be trained to complete task " + j);
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("ERROR: " + e.Message);
                            }
                            break;
                        case 5:
                            try
                            {
                                Console.Title = ("TASK 3.5");
                                List<Employee> employees = new List<Employee>
                                {
                                    new Employee("Employee 1", new List<int> {2, 1, 0}, new List<int> {3, 2, 1}),
                                    new Employee("Employee 2", new List<int> {1, 2, 0}, new List<int> {2, 3, 1}),
                                    new Employee("Employee 3", new List<int> {0, 2, 1}, new List<int> {1, 3, 2})
                                };

                                List<List<int>> taskEfficiencies = new List<List<int>>
                                {
                                    new List<int> {2, 1, 0},
                                    new List<int> {1, 2, 0},
                                    new List<int> {0, 2, 1}
                                };

                                TaskDistribution.DistributionA(employees, taskEfficiencies);
                                TaskDistribution.DistributionB(employees, taskEfficiencies);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("ERROR: " + e.Message);
                            }
                            break;
                    }
                    Console.ReadKey();
                    isInt = true;
                }
                catch
                {
                    Console.Write("You've entered not an integer number! Try again!\n");
                }
            } while (!isInt);
        }

        public class Graph
        {
            private int numVertex; 
            private List<int>[] listAdjacent; 

            public Graph(int v)
            {
                numVertex = v;
                listAdjacent = new List<int>[v];
                for (int i = 0; i < v; ++i)
                    listAdjacent[i] = new List<int>();
            }

            public void AddEdge(int v, int w)
            {
                listAdjacent[v].Add(w);
                listAdjacent[w].Add(v); 
            }

            private void DFSearch(int v, bool[] visited)
            {
                visited[v] = true;
                Console.Write(v + " ");
                foreach (int i in listAdjacent[v])
                {
                    if (!visited[i])
                        DFSearch(i, visited);
                }
            }

            public void ConnectedComponents()
            {
                bool[] visited = new bool[numVertex];
                for (int v = 0; v < numVertex; ++v)
                {
                    if (!visited[v])
                    {
                        DFSearch(v, visited);
                        Console.WriteLine();
                    }
                }
            }

            public bool IsEulerian()
            {
                for (int i = 0; i < numVertex; i++)
                {
                    if (listAdjacent[i].Count % 2 != 0)
                        return false;
                }
                return true;
            }

            private void EulerianCycleUtil(int v, bool[] visited)
            {
                visited[v] = true;
                foreach (int i in listAdjacent[v])
                {
                    if (!visited[i])
                    {
                        EulerianCycleUtil(i, visited);
                    }
                }
            }

            public void EulerianCycle(int v)
            {
                if (IsEulerian())
                {
                    bool[] visited = new bool[numVertex];
                    EulerianCycleUtil(v, visited);
                    for (int i = 0; i < numVertex; i++)
                    {
                        if (listAdjacent[i].Count > 0 && !visited[i])
                        {
                            Console.WriteLine("The graph is not connected, it is not Eulerian!");
                            return;
                        }
                    }
                    Console.WriteLine("Eulerian Cycle: ");
                    DFSearch(v, new bool[numVertex]);
                    Console.WriteLine(v);
                }
                else
                {
                    Console.WriteLine("This graph isn't eulerian!");
                }
            }

            public bool IsBipartite()
            {
                int[] color = new int[numVertex];
                for (int i = 0; i < numVertex; i++)
                    color[i] = -1;

                for (int i = 0; i < numVertex; i++)
                {
                    if (color[i] == -1)
                    {
                        if (!IsBipartiteUtil(i, color))
                            return false;
                    }
                }
                return true;
            }

            private bool IsBipartiteUtil(int src, int[] color)
            {
                color[src] = 1;
                Queue<int> q = new Queue<int>();
                q.Enqueue(src);
                while (q.Count != 0)
                {
                    int u = q.Peek();
                    q.Dequeue();
                    if (listAdjacent[u].Contains(u))
                        return false; 
                    foreach (int v in listAdjacent[u])
                    {
                        if (color[v] == -1)
                        {
                            color[v] = 1 - color[u];
                            q.Enqueue(v);
                        }
                        else if (color[v] == color[u])
                            return false;
                    }
                }
                return true;
            }

            public void Bipartition()
            {
                if (IsBipartite())
                {
                    List<int> set1 = new List<int>();
                    List<int> set2 = new List<int>();
                    for (int i = 0; i < numVertex; i++)
                    {
                        if (i % 2 == 0)
                            set1.Add(i);
                        else
                            set2.Add(i);
                    }
                    Console.Write("Right lobe (set): ");
                    foreach (int i in set1)
                        Console.Write(i + " ");
                    Console.WriteLine();
                    Console.Write("Left lobe (set): ");
                    foreach (int i in set2)
                        Console.Write(i + " ");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("The graph is not bipartite!");
                }
            }
        }

        public class FireStationFinder
        {
            public void FindStation(int N, int M, int[,] travelTimes)
            {
                int[] farthestDistances1 = new int[N];
                for (int i = 0; i < N; i++)
                {
                    int farthestDistance = 0;
                    for (int j = 0; j < N; j++)
                    {
                        if (travelTimes[i, j] > farthestDistance)
                        {
                            farthestDistance = travelTimes[i, j];
                        }
                    }
                    farthestDistances1[i] = farthestDistance;
                }

                int maxDistanceIndex1 = Array.IndexOf(farthestDistances1, farthestDistances1.Max());

                Console.WriteLine("Fire station (algorithm 1): " + maxDistanceIndex1);

                int[] farthestDistances2 = DijkstraAlgorithm(N, travelTimes);

                int maxDistanceIndex2 = Array.IndexOf(farthestDistances2, farthestDistances2.Max());

                Console.WriteLine("Fire station (algorithm 2): " + maxDistanceIndex2);
            }

            public static int[] DijkstraAlgorithm(int N, int[,] travelTimes)
            {
                int[] farthestDistances = new int[N];

                for (int i = 0; i < N; i++)
                {
                    bool[] visited = new bool[N];
                    int[] distances = new int[N];
                    Array.Fill(distances, int.MaxValue);
                    distances[i] = 0;

                    for (int count = 0; count < N - 1; count++)
                    {
                        int u = MinDistance(distances, visited, N);
                        visited[u] = true;

                        for (int v = 0; v < N; v++)
                        {
                            if (!visited[v] && travelTimes[u, v] != 0 && distances[u] != int.MaxValue && distances[u] + travelTimes[u, v] < distances[v])
                            {
                                distances[v] = distances[u] + travelTimes[u, v];
                            }
                        }
                    }

                    int maxDistance = distances.Max();
                    farthestDistances[i] = maxDistance;
                }

                return farthestDistances;
            }

            public static int MinDistance(int[] distances, bool[] visited, int N)
            {
                int min = int.MaxValue, minIndex = -1;

                for (int v = 0; v < N; v++)
                {
                    if (!visited[v] && distances[v] <= min)
                    {
                        min = distances[v];
                        minIndex = v;
                    }
                }

                return minIndex;
            }
        }

        public class MinimumSpanningTree
        {
            public static int N = 5;
            public static int[,] graph = new int[,]
            {
                {0, 2, 0, 6, 0},
                {2, 0, 3, 8, 5},
                {0, 3, 0, 0, 7},
                {6, 8, 0, 0, 9},
                {0, 5, 7, 9, 0}
            };

            public static int minKey(int[] key, bool[] mstSet)
            {
                int min = int.MaxValue, min_index = -1;

                for (int v = 0; v < N; v++)
                {
                    if (mstSet[v] == false && key[v] < min)
                    {
                        min = key[v];
                        min_index = v;
                    }
                }

                return min_index;
            }

            public static void printMST(int[] parent, int[,] graph)
            {
                Console.WriteLine("Edge \tWeight");
                for (int i = 1; i < N; i++)
                {
                    Console.WriteLine(parent[i] + " - " + i + "\t" + graph[i, parent[i]]);
                }
            }

            public static void primMST(int[,] graph)
            {
                int[] parent = new int[N];
                int[] key = new int[N];
                bool[] mstSet = new bool[N];

                for (int i = 0; i < N; i++)
                {
                    key[i] = int.MaxValue;
                    mstSet[i] = false;
                }

                key[0] = 0;
                parent[0] = -1;

                for (int count = 0; count < N - 1; count++)
                {
                    int u = minKey(key, mstSet);
                    mstSet[u] = true;

                    for (int v = 0; v < N; v++)
                    {
                        if (graph[u, v] != 0 && mstSet[v] == false && graph[u, v] < key[v])
                        {
                            parent[v] = u;
                            key[v] = graph[u, v];
                        }
                    }
                }

                printMST(parent, graph);
            }
        }

        public class Kruskal
        {
            class Edge : IComparable<Edge>
            {
                public int src, dest, weight;

                public int CompareTo(Edge compareEdge)
                {
                    return this.weight - compareEdge.weight;
                }
            };

            int V, E;
            Edge[] edge;

            public Kruskal(int v, int e)
            {
                V = v;
                E = e;
                edge = new Edge[E];
                for (int i = 0; i < e; i++)
                    edge[i] = new Edge();
            }

            public int find(int[] parent, int i)
            {
                if (parent[i] == -1)
                    return i;
                return find(parent, parent[i]);
            }

            public void Union(int[] parent, int x, int y)
            {
                int xset = find(parent, x);
                int yset = find(parent, y);
                parent[xset] = yset;
            }

            public void KruskalMST()
            {
                Edge[] result = new Edge[V];
                int e = 0;
                int i = 0;
                for (i = 0; i < V; i++)
                    result[i] = new Edge();

                Array.Sort(edge);

                int[] parent = new int[V];
                for (i = 0; i < V; i++)
                    parent[i] = -1;

                i = 0;
                while (e < V - 1)
                {
                    Edge next_edge = edge[i++];
                    int x = find(parent, next_edge.src);
                    int y = find(parent, next_edge.dest);

                    if (x != y)
                    {
                        result[e++] = next_edge;
                        Union(parent, x, y);
                    }
                }

                Console.WriteLine("Following are the edges in the constructed MST");
                for (i = 0; i < e; ++i)
                    Console.WriteLine(result[i].src + " -- " + result[i].dest + " == " + result[i].weight);
            }
        }

        public class BipartiteMatching
        {
            public static int K;
            public static int[,] skills = new int[,]{};

            public static bool bpm(int s, bool[] seen, int[] matchR, ArrayList[] adj)
            {
                for (int v = 0; v < K; v++)
                {
                    if ((int)adj[s][v] == 1 && !seen[v])
                    {
                        seen[v] = true;

                        if (matchR[v] < 0 || bpm(matchR[v], seen, matchR, adj))
                        {
                            matchR[v] = s;
                            return true;
                        }
                    }
                }
                return false;
            }

            public static int maxBPM(ArrayList[] adj)
            {
                int[] matchR = new int[K];
                Array.Fill(matchR, -1);
                int result = 0;

                for (int u = 0; u < K; u++)
                {
                    bool[] seen = new bool[K];
                    if (bpm(u, seen, matchR, adj))
                    {
                        result++;
                    }
                }
                return result;
            }
        }
        public class Employee
        {
            public string Name { get; set; }
            public List<int> interests;
            public List<int> efficiencies;
            public List<int> tasks;

            public Employee(string name, List<int> interests, List<int> efficiencies)
            {
                Name = name;
                this.interests = interests;
                this.efficiencies = efficiencies;
                tasks = new List<int>();
            }
        }

        public class TaskDistribution
        {
            public static void DistributionA(List<Employee> employees, List<List<int>> taskEfficiencies)
            {
                int N = employees.Count;
                int M = taskEfficiencies.Count;

                for (int i = 0; i < M; i++)
                {
                    int task = i;
                    int maxEfficiency = -1;
                    int bestEmployee = -1;

                    for (int j = 0; j < N; j++)
                    {
                        if (employees[j].tasks.Count == 0 || employees[j].tasks[0] < task)
                        {
                            if (employees[j].interests.Contains(task) && employees[j].efficiencies[task] > maxEfficiency)
                            {
                                maxEfficiency = employees[j].efficiencies[task];
                                bestEmployee = j;
                            }
                        }
                    }

                    if (bestEmployee != -1)
                    {
                        employees[bestEmployee].tasks.Add(task);
                    }
                }

                Console.WriteLine("Distribution (a) completed:");
                for (int i = 0; i < N; i++)
                {
                    Console.WriteLine("{0}: {1}", employees[i].Name, string.Join(", ", employees[i].tasks));
                }
            }

            public static void DistributionB(List<Employee> employees, List<List<int>> taskEfficiencies)
            {
                int N = employees.Count;
                int M = taskEfficiencies.Count;

                for (int i = 0; i < N; i++)
                {
                    int employee = i;
                    int maxInterestTask = -1;
                    int bestTask = -1;

                    for (int j = 0; j < M; j++)
                    {
                        if (!employees[employee].tasks.Contains(j) && employees[employee].interests.Contains(j) && employees[employee].efficiencies[j] > maxInterestTask)
                        {
                            bool allMoreEfficientAssigned = true;
                            for (int k = 0; k < N; k++)
                            {
                                if (employees[k].tasks.Contains(j) && employees[k].efficiencies[j] > employees[employee].efficiencies[j])
                                {
                                    allMoreEfficientAssigned = false;
                                    break;
                                }
                            }
                            if (allMoreEfficientAssigned)
                            {
                                maxInterestTask = employees[employee].efficiencies[j];
                                bestTask = j;
                            }
                        }
                    }

                    if (bestTask != -1)
                    {
                        employees[employee].tasks.Add(bestTask);
                    }
                }

                Console.WriteLine("Distribution (b) completed:");
                for (int i = 0; i < N; i++)
                {
                    Console.WriteLine("{0}: {1}", employees[i].Name, string.Join(", ", employees[i].tasks));
                }
            }
        }
    }
}