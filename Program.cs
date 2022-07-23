namespace BFS_Implemenation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("BFS Implementation Example!");

            Graph g = new Graph();
            g.AddEdge("David", "Larisa");
            g.AddEdge("David", "Nastya");
            g.AddEdge("David", "Alisa");

            g.AddEdge("Larisa", "David");
            g.AddEdge("Larisa", "Nastya");
            g.AddEdge("Larisa", "Alisa");
            g.AddEdge("Larisa", "Dafna");
            g.AddEdge("Dafna", "Dani");

            g.AddEdge("Alisa", "David");
            g.AddEdge("Alisa", "Nastya");
            g.AddEdge("Alisa", "Larisa");
            g.AddEdge("Alisa", "Gefen");

            g.AddEdge("Nastya", "David");
            g.AddEdge("Nastya", "Larisa");
            g.AddEdge("Nastya", "Alisa");
            g.AddEdge("Nastya", "Slava");

            string nameToStart = "David";
            string nameToFind = "Dani";
            if(g.IsExist(nameToStart, nameToFind)) {
                Console.WriteLine("{0} found.", nameToFind);
            } else {
                Console.WriteLine("{0} not found.", nameToFind);
            }

            g.printShortestPath(nameToStart, nameToFind);
        }
    }

    class Graph{
        public Dictionary<string, List<string>>? adjMap {get; set;}
        Dictionary<string, string> parentList {get; set;}

        public Graph(){
            adjMap = new Dictionary<string, List<string>>();
        }

        public void AddEdge(string parentNode, string childNode){
            List<string>? list;
            if(adjMap.TryGetValue(parentNode, out list)){
                list.Add(childNode);
            } else {
                list = new List<string>();
                list.Add(childNode);
                if(adjMap == null){
                    adjMap = new Dictionary<string, List<string>>();
                }
                adjMap[parentNode] = list;
            }
        }

        public bool IsExist(string nameToStart, string nameToFind){
            List<string> list;
            if(!adjMap.TryGetValue(nameToStart, out list)){
                Console.WriteLine("{0} not found in graph.", nameToStart);
                return false;
            }
            List<string> visited = new List<string>();
            parentList = new Dictionary<string, string>();

            Queue<string> queue = new Queue<string>(); 

            queue.Enqueue(nameToStart);
            visited.Add(nameToStart);

            while(queue.Count != 0) {
                string currentName = queue.Dequeue();
                List<string> adjList;
                adjMap.TryGetValue(currentName, out adjList);

                if(currentName == nameToFind) {
                    return true;
                }

                if(adjList != null && adjList.Count > 0){
                    foreach (string n in adjList) {
                        if(visited.Contains(n)) {
                            continue;
                        } else {
                            queue.Enqueue(n);
                            visited.Add(n);
                            if(!parentList.TryGetValue(n, out _)){
                                parentList[n] = currentName;
                            }
                        }                
                    }
                }
                Console.WriteLine("{0}", currentName);
            }
            return false;
        }

        public void printShortestPath(string nameToStart, string name) {
            Console.WriteLine();
            Console.WriteLine("Shortest path between {0} to {1}", name, nameToStart);
            string currentName = name;
            Console.Write("{0}", currentName);
            string parent;
            while( currentName != nameToStart && parentList.TryGetValue(currentName, out parent)) {
                Console.Write(" - {0}", parent);
                currentName = parent;
            }
            Console.WriteLine();
        }
    }
}


