using System;
using System.Collections.Generic;
using ManageMe.Common.Interfaces;

namespace ManageMe.Common
{
    public class Node<T> where T : IColorable
    {
        public T Data { get; set; }

        public Node(T data)
        {
            Data = data;
        }
    }

    public class Graph<T> where T : IColorable
    {
        private int V; // Number of vertices
        private Dictionary<Node<T>, int> nodeIndex; // Map nodes to their index
        public Node<T>[] Nodes { get; set; }
        private int[,] adj; // Adjacency matrix

        public Graph(int v)
        {
            V = v;
            Nodes = new Node<T>[v];
            adj = new int[v, v];
            nodeIndex = new Dictionary<Node<T>, int>();
        }

        public void AddNode(Node<T> node, int index)
        {
            Nodes[index] = node;
            nodeIndex[node] = index;
        }

        public void AddEdge(Node<T> v, Node<T> w)
        {
            int vIndex = nodeIndex[v];
            int wIndex = nodeIndex[w];
            adj[vIndex, wIndex] = 1;
            adj[wIndex, vIndex] = 1; // Since the graph is undirected
        }

        private void DFSUtil(int v, bool[] visited, List<int> component)
        {
            visited[v] = true;
            component.Add(v);

            for (int i = 0; i < V; i++)
            {
                if (adj[v, i] == 1 && !visited[i])
                {
                    DFSUtil(i, visited, component);
                }
            }
        }

        private List<List<int>> GetConnectedComponents()
        {
            bool[] visited = new bool[V];
            List<List<int>> components = new List<List<int>>();

            for (int v = 0; v < V; v++)
            {
                if (!visited[v])
                {
                    List<int> component = new List<int>();
                    DFSUtil(v, visited, component);
                    components.Add(component);
                }
            }

            return components;
        }

        private bool IsSafe(int v, int[] color, int c)
        {
            for (int i = 0; i < V; i++)
                if (adj[v, i] == 1 && color[i] == c)
                    return false;
            return true;
        }

        private bool GraphColoringUtil(int m, int[] color, int v, List<int> component)
        {
            if (v == component.Count)
                return true;

            for (int c = 1; c <= m; c++)
            {
                if (IsSafe(component[v], color, c))
                {
                    color[component[v]] = c;
                    if (GraphColoringUtil(m, color, v + 1, component))
                        return true;
                    color[component[v]] = 0; // Backtrack
                }
            }
            return false;
        }

        public bool GraphColoring(int m)
        {
            int[] color = new int[V];
            for (int i = 0; i < V; i++)
                color[i] = 0;

            List<List<int>> components = GetConnectedComponents();

            foreach (var component in components)
            {
                if (!GraphColoringUtil(m, color, 0, component))
                {
                    return false;
                }
            }

            AssignColors(color);
            return true;
        }

        private void AssignColors(int[] color)
        {
            for (int i = 0; i < V; i++)
                Nodes[i].Data.ColorCode = color[i];
        }
    }
}