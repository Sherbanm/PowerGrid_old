using System;

namespace PowerGrid.Mapping
{
    public class Connection : IComparable<Connection>
    {
        public City To { get; set; }

        public City From { get; set; }

        public int Cost { get; set; }

        public Connection(City to, City from, int cost)
        {
            To = to;
            From = from;
            Cost = cost;
        }

        public int CompareTo(Connection that)
        {
            if (Cost < that.Cost)
                return -1;
            else if (Cost > that.Cost)
                return +1;
            else
                return 0;
        }

        public City OtherEnd(City oneEnd)
        {
            if (oneEnd == To)
            {
                return From;
            }
            else if (oneEnd == From)
            {
                return To;
            }
            else
            {
                throw new Exception("Illegal endpoint");
            }
        }
    }
}