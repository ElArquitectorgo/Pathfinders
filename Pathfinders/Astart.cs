using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinders
{
    public class Astart : PathAlgorithm
    {
        private Grid grid;
        public Astart(Grid grid) 
        {
            this.grid = grid;
        }
        public List<Node> ReconstructPath(Node endNode)
        {
            List<Node> path = new List<Node>();
            Node node = endNode.Parent;
            while (node.Parent != null)
            {
                path.Add(node);
                node = node.Parent;
            }
            path.Reverse();

            return path;
        }
        public List<Node> findPath(int[] startPosition, int[] endPosition)
        {
            Node startNode = grid.Nodes[startPosition[0], startPosition[1]];
            Node endNode = grid.Nodes[endPosition[0], endPosition[1]];
            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost)
                    {
                        if (openSet[i].hCost < currentNode.hCost) currentNode = openSet[i];
                    }
                }
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == endNode)
                {
                    return ReconstructPath(endNode);
                }

                foreach (Node neighbour in grid.GetNeighbours(currentNode))
                {
                    if (!neighbour.Walkable || closedSet.Contains(neighbour)) continue;

                    int newPathToNeighbour = currentNode.gCost + GetManhattanDistance(currentNode, neighbour);
                    if (newPathToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newPathToNeighbour;
                        neighbour.hCost = GetManhattanDistance(neighbour, endNode);
                        neighbour.Parent = currentNode;

                        if (!openSet.Contains(neighbour)) openSet.Add(neighbour);
                    }
                }
            }

            return openSet;
        }

        private int GetManhattanDistance(Node a, Node b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }
    }
}
