using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinders
{
    public interface PathAlgorithm
    {
        public List<Node> findPath(int[] startPosition, int[] endPosition);
    }
}
