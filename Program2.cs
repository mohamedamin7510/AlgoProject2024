using System;
using System.Collections.Generic;

public class Graph
{
    public int V, E;
    public List<Edge> edges;

    public class Edge
    {
        public int u, v, w;
    }

    public Graph(int V, int E)
    {
        this.V = V;
        this.E = E;
        edges = new List<Edge>();
    }

    public void AddEdge(int u, int v, int w)
    {
        Edge edge = new Edge { u = u, v = v, w = w };
        edges.Add(edge);
    }

    public int Find(int[] parent, int i)
    {
        if (parent[i] == i)
            return i;
        return Find(parent, parent[i]);
    }

    public void Union(int[] parent, int[] rank, int x, int y)
    {
        int xroot = Find(parent, x);
        int yroot = Find(parent, y);

        if (rank[xroot] < rank[yroot])
            parent[xroot] = yroot;
        else if (rank[xroot] > rank[yroot])
            parent[yroot] = xroot;
        else
        {
            parent[yroot] = xroot;
            rank[xroot]++;
        }
    }

    public void Kruskal()
    {
        edges.Sort((a, b) => a.w.CompareTo(b.w));

        int[] parent = new int[V];
        int[] rank = new int[V];

        for (int i = 0; i < V; i++)
        {
            parent[i] = i;
            rank[i] = 0;
        }

        List<Edge> mst = new List<Edge>();

        foreach (Edge edge in edges)
        {
            int x = Find(parent, edge.u);
            int y = Find(parent, edge.v);

            if (x != y)
            {
                mst.Add(edge);
                Union(parent, rank, x, y);
            }
        }

        Console.WriteLine("Minimum Spanning Tree:");
        foreach (var edge in mst)
        {
            Console.WriteLine($"{edge.u} -- {edge.v} == {edge.w}");
        }
    }
}

class Program
{
    static void Main()
    {
        Graph graph = new Graph(4, 5);
        graph.AddEdge(0, 1, 10);
        graph.AddEdge(0, 2, 6);
        graph.AddEdge(0, 3, 5);
        graph.AddEdge(1, 3, 15);
        graph.AddEdge(2, 3, 4);

        graph.Kruskal();
    }
}

