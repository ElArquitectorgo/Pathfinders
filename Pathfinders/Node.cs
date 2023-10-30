using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinders
{
    public class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Walkable { get; set; }
        public Node Parent { get; set; }
        public int gCost { get; set; }
        public int hCost { get; set; }
        public int fCost
        {
            get { return gCost + hCost; }
        }
        public Node(int x, int y, bool walkable)
        {
            X = x;
            Y = y;
            Walkable = walkable;
        }
    }
}
