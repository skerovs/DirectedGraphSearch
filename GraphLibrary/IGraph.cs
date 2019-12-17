using System.Collections.Generic;

namespace GraphLibrary
{
    public interface IGraph <T, K>
    {
        bool AddVertex(KeyValuePair<T, K> vertex);
        void AddVertex(IEnumerable<KeyValuePair<T, K>> vertexSet);
        bool DeleteVertex(KeyValuePair<T, K> vertex);
        void DeleteVertex(IEnumerable<KeyValuePair<T, K>> vertexSet);
        bool AddEdge(KeyValuePair<T, K> v1, KeyValuePair<T, K> v2, K weight);
        K GetWeight(T v1Key, T v2Key);
        KeyValuePair<T, K> GetVertexPair(T v1Key);
        bool DeleteEdge(T v1Key, T v2Key);
        int VerticesNumber();
        int EdgesNumber();
        IEnumerable<T> AdjacentVertices(T vertex);
        IEnumerable<KeyValuePair<T, K>> GetVertexSet();
        IEnumerable<IPairValue<T>> GetEdgeSet();
        int GetVertexModulo(T vertex);
    }

    public interface IPairValue<T>
    {
        T GetFirst();
        T GetSecond();
        bool Contains(T value);
    }
}
