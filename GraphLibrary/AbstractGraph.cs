using System;
using System.Collections.Generic;

namespace GraphLibrary
{
    public abstract class AbstractGraph <T,K> : IGraph<T,K>
    {
        protected readonly List<KeyValuePair<T, K>> VertexSet =  new List<KeyValuePair<T, K>>();
        protected readonly List<IPairValue<T>> EdgeSet = new List<IPairValue<T>>();
        protected readonly Dictionary<IPairValue<T>,K> Weights = new Dictionary<IPairValue<T>,K>();
        public bool AddVertex(KeyValuePair<T, K> vertex)
        {
            if(vertex.Key == null || vertex.Value == null)
                throw new ArgumentException();
            if (VertexSet.Contains(vertex))
                return false;
            VertexSet.Add(vertex);
            return true;
        }

        public void AddVertex(IEnumerable<KeyValuePair<T, K>> vertexSet)
        {
            if (vertexSet == null)
                throw new ArgumentException();
            using (var it = vertexSet.GetEnumerator())
            {
                while (it.MoveNext())
                {
                    if(it.Current.Key != null && !VertexSet.Contains(it.Current))
                        VertexSet.Add(it.Current);
                }
            }
        }

        public bool DeleteVertex(KeyValuePair<T, K> vertex)
        {
            if (vertex.Key == null || vertex.Value == null)
                throw new ArgumentNullException();
            if (!VertexSet.Contains(vertex))
                return false;
            VertexSet.Remove(vertex);
            return true;
        }

        public void DeleteVertex(IEnumerable<KeyValuePair<T, K>> vertexSet)
        {
            if (vertexSet == null)
                throw new ArgumentNullException();
            using(var it = vertexSet.GetEnumerator())
                while (it.MoveNext())
                {
                    if (it.Current.Key != null)
                        VertexSet.Remove(it.Current);
                }
        }

        public abstract K GetWeight(T v1Key, T v2Key);
        public abstract KeyValuePair<T, K> GetVertexPair(T v1Key);
        public abstract bool AddEdge(KeyValuePair<T, K> v1, KeyValuePair<T, K> v2, K weight);

        public abstract bool DeleteEdge(T v1, T v2);
        
        public int VerticesNumber()
        {
            return VertexSet.Count;
        }

        public int EdgesNumber()
        {
            return EdgeSet.Count;
        }

        public int GetVertexModulo(T vertex)
        {
            return GetVertexPair(vertex).Value.GetHashCode() % 2;
        }

        public abstract IEnumerable<T> AdjacentVertices(T vertexKey);

        public IEnumerable<KeyValuePair<T, K>> GetVertexSet()
        {
            return VertexSet;
        }

        public IEnumerable<IPairValue<T>> GetEdgeSet()
        {
            return EdgeSet;
        }
    }
}
