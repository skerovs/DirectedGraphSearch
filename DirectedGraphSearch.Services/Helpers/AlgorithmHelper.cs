using System.Collections.Generic;
using System.Linq;
using GraphLibrary;

namespace DirectedGraphSearch.Services.Helpers
{
    public class Algorithms
    {
        public List<List<int>> GetPossiblePaths<T, K>(DirectedGraph<T, K> graph)
        {
            var edges = graph.GetEdgeSet().ToList();
            var start = edges[0].GetFirst();

            var visited = new HashSet<T>();
            
            var stack = new Stack<T>();
            stack.Push(start);

            var previousVertexModulo = graph.GetVertexModulo(start);
            var allPathsAsVertexKeys = new List<List<int>>();
            var allPathsAsVertexValues = new List<List<int>>();
            var newPathToAdd = new List<T>();
            while (stack.Count > 0)
            {
                var vertex = stack.Pop();
                
                if (visited.Contains(vertex))
                    continue;
                
                visited.Add(vertex);

                var adjacentVertices = graph.AdjacentVertices(vertex).ToList();
                previousVertexModulo = graph.GetVertexModulo(vertex);
                var deadEndCounter = 0;
                foreach (var neighbor in adjacentVertices)
                {
                    var neighborVertex = graph.GetVertexPair(neighbor);
                    var currentLevel = graph.GetWeight(vertex, neighborVertex.Key).GetHashCode();

                    if(newPathToAdd.Count > currentLevel)
                        for (var i = newPathToAdd.Count + 1 - currentLevel; i > 0; i--)
                                newPathToAdd.RemoveAt(newPathToAdd.Count - i);

                    var neighborModulo = graph.GetVertexModulo(neighbor);

                    if (!visited.Contains(neighbor) && previousVertexModulo != neighborModulo)
                    {
                        stack.Push(neighbor);
                        if(!newPathToAdd.Contains(vertex))
                            newPathToAdd.Add(vertex);
                    }
                    else
                        deadEndCounter++;
                    
                    if (deadEndCounter == 2)
                        visited.Remove(vertex);
                }

                if (adjacentVertices.Count != 0) continue;

                if (allPathsAsVertexKeys.Count != 0 && newPathToAdd.Count == allPathsAsVertexKeys[0].Count)
                    newPathToAdd.RemoveAt(newPathToAdd.Count - 1);

                newPathToAdd.Add(vertex);
                allPathsAsVertexKeys.Add(newPathToAdd.ConvertAll(x => x.GetHashCode()).ToList());
                var newList = new List<int>();
                newPathToAdd.ForEach(x => newList.Add(graph.GetVertexPair(x).Value.GetHashCode()));
                allPathsAsVertexValues.Add(newList);
            }
            
            return allPathsAsVertexValues;
        }
    }
}
