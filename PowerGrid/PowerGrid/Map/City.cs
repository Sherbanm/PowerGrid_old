using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerGrid.Mapping
{
    public class City
    {
        public string Name { get; set; }

        public bool IsMegacity { get; set; }

        public List<Connection> Connections { get; set; }

        public List<Player> Generators { get; set; }

        public City(string name, bool isMegacity)
        {
            Name = name;
            IsMegacity = isMegacity;

            Connections = new List<Connection>();
            Generators = new List<Player>();    
        }

        public void AddConnection(Connection connection)
        {
            Connections.Add(connection);
        }
    }
}
