using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerGrid
{
    class Player
    {
        public string Name { get; set; }

        public List<PowerStation> PowerStations { get; set; }

        public int MaxPowerStations { get; set; }

        public Player(string name, int maxPowerStations)
        {
            Name = Name;
            MaxPowerStations = maxPowerStations;
        } 

        public void ReceiveStation(PowerStation powerStation)
        {
            if (PowerStations.Count == MaxPowerStations)
            {
                DiscardPowerStation();
            }
            PowerStations.Add(powerStation);
        }

        void DiscardPowerStation()
        {
            throw new NotImplementedException();
        }
    }
}
