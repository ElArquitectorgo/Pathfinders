using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinders
{
    public class Grid
    {
        public Node[,] Nodes { get; set; }
        public Grid(Node[,] nodes) 
        {
            Nodes = nodes;
        }

        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0) continue;
                    int checkX = node.X + x;
                    int checkY = node.Y + y;
                    if (isInsideGrid(checkX, checkY)) neighbours.Add(Nodes[checkX, checkY]);
                }
            }
            return neighbours;
        }

        private bool isInsideGrid(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Nodes.GetLength(0) && y < Nodes.GetLength(1);
        }
    }
}
