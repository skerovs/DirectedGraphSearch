using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLibrary
{
    public class DirectedGraph <T,K> : AbstractGraph<T,K>
    {

        public override bool AddEdge(KeyValuePair<T, K> v1, KeyValuePair<T,K> v2, K weight)
        {
            if(v1.Key == null || v2.Key == null || weight == null)
                throw new ArgumentNullException();
            if (!VertexSet.Contains(v1) || !VertexSet.Contains(v2))
                return false;
            IPairValue<T> pair = new PairValue<T>(v1.Key,v2.Key);

            if (EdgeSet.Contains(pair))
                return false;

            EdgeSet.Add(pair);
            Weights[pair] = weight;
            return true;
        }

        public override K GetWeight(T v1Key, T v2Key)
        {
            if(v1Key == null || v2Key == null)
                throw new ArgumentNullException();
            IPairValue<T> pair = new PairValue<T>(v1Key, v2Key);

            if(!Weights.ContainsKey(pair))
                throw new ArgumentException();

            return Weights[pair];
        }

        public override bool DeleteEdge(T v1Key, T v2Key)
        {
            if (v1Key == null || v2Key == null)
                throw new ArgumentNullException();

            IPairValue<T> pair = new PairValue<T>(v1Key, v2Key);

            if (EdgeSet.Contains(pair))
            {
                EdgeSet.Remove(pair);
                Weights.Remove(pair);
                return true;
            }

            return false;
        }

        public override KeyValuePair<T, K> GetVertexPair(T v1Key)
        {
            if (v1Key == null)
                throw new ArgumentNullException();

            var vertex = VertexSet.FirstOrDefault(x => x.Key.Equals(v1Key));

            if (!VertexSet.Contains(vertex))
                throw new ArgumentException();

            return vertex;
        }

        public override IEnumerable<T> AdjacentVertices(T vertexKey)
        {
            foreach (IPairValue<T> p in EdgeSet)
            {
                if (p.GetFirst().Equals(vertexKey))
                    yield return p.GetSecond();
            }
        }
    }
}
