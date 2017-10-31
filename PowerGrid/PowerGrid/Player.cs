using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerGrid
{
    public class Player
    {
        public string Name { get; set; }

        public bool CanBuy { get; set; }

        public List<PowerStation> PowerStations { get; set; }

        public int MaxPowerStations { get; set; }

        public int Money { get; set; }

        public int[] Resources {get;set;}

        public Player(string name, int maxPowerStations)
        {
            Name = name;
            Money = 50;
            PowerStations = new List<PowerStation>();
            MaxPowerStations = maxPowerStations;
            Resources = new int[4];
        } 

        public void ReceiveStation(PowerStation powerStation)
        {
            if (PowerStations.Count == MaxPowerStations)
            {
                DiscardPowerStation();
            }
            PowerStations.Add(powerStation);
        }

        public int GetBiggestPowerStation()
        {
            return PowerStations.OrderByDescending(p => p.Value).FirstOrDefault()?.Value ?? 0;
        }

        public int CalculateIncome()
        {
            var orderedPowerStations = PowerStations.OrderByDescending(p => p.Power);
            List<PowerStation> stationsThatCanBePowered = new List<PowerStation>();
            foreach (var powerStation in PowerStations)
            {
                if (CanPower(powerStation))
                {
                    stationsThatCanBePowered.Add(powerStation);
                }
            }
            return stationsThatCanBePowered.Sum(p => p.Power);
        }

        private bool CanPower(PowerStation powerStation)
        {
            switch(powerStation.Type)
            {
                case ResourceType.Coal:
                    return powerStation.Resource <= Resources[ResourceMarket.COAL];
                case ResourceType.Gas:
                    return powerStation.Resource <= Resources[ResourceMarket.GAS];
                case ResourceType.Oil:
                    return powerStation.Resource <= Resources[ResourceMarket.OIL];
                case ResourceType.Nuclear:
                    return powerStation.Resource <= Resources[ResourceMarket.NUCLEAR];
                default:
                    return true;
            }
        }

        void DiscardPowerStation()
        {
            throw new NotImplementedException();
        }

        
    }
}
