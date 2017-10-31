using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerGrid.Mapping
{
    public class Map
    {
        public List<City> Cities { get; set; }

        public Map()
        {
            Cities = new List<City>();
        }

        public void AddCity(City city)
        {
            Cities.Add(city);
        }

        public Map(int V)
        {
            /*if (V < 0)
                throw new Exception("Number of vertices in a Graph must be nonnegative");

            this._v = V;

            this._e = 0;

            _adj = new LinkedList<Edge>[V];

            for (int v = 0; v < V; v++)
            {
                _adj[v] = new LinkedList<Edge>();
            }*/
            return;
        }
        /*
        public int V()
        {
            return _v;
        }

        public int E()
        {
            return _e;
        }

        public void AddEdge(Edge e)
        {
            int v = e.Source();
            int w = e.Target(v);
            _adj[v].AddFirst(e);
            _adj[w].AddFirst(e);
            _e++;
        }

        public IEnumerable<Edge> Adj(int v)
        {
            return _adj[v];
        }

        public IEnumerable<Edge> Edges()
        {
            LinkedList<Edge> list = new LinkedList<Edge>();

            for (int v = 0; v < _v; v++)
            {
                int selfLoops = 0;

                foreach (Edge e in Adj(v))
                {
                    if (e.Target(v) > v)
                    {
                        list.AddFirst(e);
                    }
                    else if (e.Target(v) == v)
                    {
                        if (selfLoops % 2 == 0)
                            list.AddFirst(e);
                        selfLoops++;
                    }
                }
            }

            return list;
        }
        */
    }
}
