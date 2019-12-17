using System;
using System.Collections.Generic;
using System.Linq;
using DirectedGraphSearch.Services.Contracts;
using GraphLibrary;

namespace DirectedGraphSearch.Services
{
    public class GraphService : IGraphService
    {
        public DirectedGraph<int, int> ReadGraphFromString(string graphString)
        {
            var listOfGraphRowsAsStrings = graphString.Split("\r\n");
            var graphLevels = new List<List<KeyValuePair<int, int>>>();

            var listOfVertices = new List<KeyValuePair<int,int>>();
            var vertexNumber = 1;
            foreach (var rowString in listOfGraphRowsAsStrings)
            {
                var listOfRowVertices = new List<KeyValuePair<int, int>>();
                var rowNumbersAsStrings = rowString.Split(" ");
                foreach (var number in rowNumbersAsStrings)
                {
                    var newVertex = new KeyValuePair<int, int>(vertexNumber, int.Parse(number));
                    listOfRowVertices.Add(newVertex);
                    listOfVertices.Add(newVertex);
                    vertexNumber++;
                }
                graphLevels.Add(listOfRowVertices);
            }

            var newDirectedGraph = new DirectedGraph<int, int>();
            newDirectedGraph.AddVertex(listOfVertices);

            var previousRow = new List<KeyValuePair<int, int>>();
            var levelCounter = 0;
            foreach (var graphLevel in graphLevels)
            {
                if(graphLevel.Count > 1)
                {
                    for(var i = 0; i < previousRow.Count; i++)
                    {
                        var v1 = previousRow[i];
                        newDirectedGraph.AddEdge(v1, graphLevel[i], levelCounter);
                        newDirectedGraph.AddEdge(v1,
                            graphLevel[i + 1],levelCounter);
                    }
                }
                previousRow = graphLevel;
                levelCounter++;
            }

            return newDirectedGraph;
        }

        public void PrintGraph(DirectedGraph<int, int> graph, int topMargin = 4, int leftMargin = 2)
        {
            if (graph == null) return;
            var rootTop = Console.CursorTop + topMargin;

            var setOfEdges = graph.GetEdgeSet().ToList();

            var numberOfIntegersInRow = 0;
            var left = true;
            var newRow = true;
            var counter = 0;

            foreach (var edge in setOfEdges)
            {
                var vertex1Key = edge.GetFirst();
                var vertex2Key = edge.GetSecond();
                var v2String = graph.GetVertexPair(vertex2Key).Value.ToString(" 0 ");
                var currentLevel = graph.GetWeight(vertex1Key, vertex2Key);

                if (v2String.Length <= 4)
                    v2String = $" {v2String}";

                var size = v2String.Length;

                if (newRow)
                {
                    numberOfIntegersInRow = currentLevel + 1;
                    counter = 0;
                    newRow = false;
                }

                var startPosition = (setOfEdges.Count / 2 - currentLevel * size) + counter * size * 2;
                var endPosition = currentLevel + 1;
                var top = 2 * currentLevel;

                if (currentLevel == 1 && counter == 0)
                    PrintNumber(graph.GetVertexPair(vertex1Key).Value.ToString(" 0 "), startPosition + size, 0);

                if (left)
                {
                    PrintNumber(v2String, startPosition, top);

                    PrintLink(top-1, "┌", "┘", startPosition + 3, startPosition + 5);
                    left = false;
                    counter++;
                }
                else
                {
                    if(counter == numberOfIntegersInRow - 1 )
                        PrintNumber(v2String, startPosition, top);

                    PrintLink(top - 1, "└", "┐", startPosition - 1, startPosition + 1);
                    left = true;
                    
                    if (counter == numberOfIntegersInRow - 1) newRow = true;
                }
            }
        }

        public void PrintMostExpensivePathInGraph(DirectedGraph<int, int> graph, List<List<int>> allPossiblePaths)
        {
            Console.Write(allPossiblePaths.Count <= 1
                ? $"\n There is {allPossiblePaths.Count} paths.\n"
                : $"\n There are {allPossiblePaths.Count} paths.\n");

            if (allPossiblePaths.Count <= 0) return;
            foreach (var pathValues in allPossiblePaths)
            {
                var pathSum = 0;
                Console.Write("[");
                for (var i = 0; i < pathValues.Count; i++)
                {
                    pathSum += pathValues[i];
                    Console.Write(pathValues.Count - 1 == i ? $" {pathValues[i]}" : $" {pathValues[i]},");
                }

                Console.Write($"] This graph path sums up to {pathSum} \n");
            }
        }
        
        private static void PrintNumber(string text, int startPosition, int top)
        {
            Print(text, top, startPosition);
        }

        private static void PrintLink(int top, string start, string end, int startPos, int endPos)
        {
            Print(start, top, startPos);
            Print("─", top, startPos + 1, endPos);
            Print(end, top, endPos);
        }

        private static void Print(string s, int top, int left, int right = -1)
        {
            if (left + s.Length >= Console.WindowWidth )
                Console.SetWindowSize(left + 6, Console.WindowHeight);
            if (top >= Console.WindowHeight )
                Console.SetWindowSize(Console.WindowWidth, top + 6);

            Console.SetCursorPosition(left, top);
            if (right < 0) 
                right = left + s.Length;
            while (Console.CursorLeft < right) 
                Console.Write(s);
        }
    }
}
