using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPR_2
{
    class Element
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int PlacedConnections { get; set; }

        public int UnPlacedConnections { get; set; }

        public Element(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool CheckPosition(int x, int y)
        {
            return ((X == x) && (Y == y));
        }
    }
}
