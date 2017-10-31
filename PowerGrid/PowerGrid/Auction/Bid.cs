using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerGrid.Auctioning
{
    class Bid
    {
        public Player Player { get; set; }

        public int Value { get; set; }

        public Bid(Player player, int value)
        {
            Player = player;
            Value = value;
        }
    }
}
