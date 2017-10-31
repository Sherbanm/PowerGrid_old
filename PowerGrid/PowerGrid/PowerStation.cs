using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerGrid
{
    public class PowerStation
    {
        public int Value { get; set; }

        public int Power { get; set; }

        public int Resource { get; set; }

        public ResourceType Type { get; set; }

        public PowerStation(int value, int power, int resource, ResourceType type)
        {
            Value = value;
            Power = power;
            Resource = resource;
            Type = type;
        }
    }
}
