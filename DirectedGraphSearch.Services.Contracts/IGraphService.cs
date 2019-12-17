using System;
using System.Collections.Generic;
using System.Text;
using GraphLibrary;

namespace DirectedGraphSearch.Services.Contracts
{
    public interface IGraphService
    {
        DirectedGraph<int, int> ReadGraphFromString(string graphString);
        void PrintGraph(DirectedGraph<int, int> graph, int topMargin = 2, int leftMargin = 2);
        void PrintMostExpensivePathInGraph(DirectedGraph<int, int> graph, List<List<int>> allPossiblePaths);
    }
}
