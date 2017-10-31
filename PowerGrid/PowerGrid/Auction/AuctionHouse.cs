using PowerGrid.GameState;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerGrid.Auctioning
{
    class AuctionHouse
    {
        private static Random rng = new Random();

        public Queue<PowerStation> PowerStations;

        public Queue<PowerStation> ThirdStepPowerStations;

        public List<PowerStation> CurrentMarket;

        public List<PowerStation> FutureMarket;

        public Auction CurrentAuction;

        int currentMarketSize;

        int futureMarketSize;

        int playerCount;

        public AuctionHouse(Location location, int playerCount)
        {
            this.playerCount = playerCount;
            currentMarketSize = GetCurrentMarketSize(location);
            futureMarketSize = GetFutureMarketSize(location);

            CurrentMarket = new List<PowerStation>(currentMarketSize);
            FutureMarket = new List<PowerStation>(futureMarketSize);

            PrepareDecks();
        }

        public void DrawPowerPlant()
        {
            var newPowerPlant = PowerStations.Dequeue();
            var picked = CurrentMarket.Union(FutureMarket).ToList();
            picked.Add(newPowerPlant);
            picked = picked.OrderBy(x => x.Value).ToList();

            CurrentMarket = picked.GetRange(0, currentMarketSize);
            FutureMarket = picked.GetRange(currentMarketSize, futureMarketSize);
        }

        public void PrepareDecks()
        {
            var startingStations = new List<PowerStation>();
            var normalStations = new List<PowerStation>();

            startingStations.Add(new PowerStation(3, 1, 2, ResourceType.Coal));
            startingStations.Add(new PowerStation(4, 1, 2, ResourceType.Coal));
            startingStations.Add(new PowerStation(5, 1, 2, ResourceType.Gas));
            startingStations.Add(new PowerStation(6, 1, 1, ResourceType.Oil));
            startingStations.Add(new PowerStation(7, 2, 1, ResourceType.Coal));
            startingStations.Add(new PowerStation(8, 2, 3, ResourceType.Mixed));
            startingStations.Add(new PowerStation(9, 2, 3, ResourceType.Coal));
            startingStations.Add(new PowerStation(10, 2, 2, ResourceType.Oil));
            startingStations.Add(new PowerStation(11, 1, 0, ResourceType.Green));
            startingStations.Add(new PowerStation(12, 2, 2, ResourceType.Coal));
            startingStations.Add(new PowerStation(14, 2, 1, ResourceType.Gas));
            startingStations.Add(new PowerStation(15, 2, 1, ResourceType.Coal));

            Shuffle(startingStations);

            normalStations.Add(new PowerStation(20, 4, 3, ResourceType.Coal));
            normalStations.Add(new PowerStation(25, 5, 2, ResourceType.Coal));
            normalStations.Add(new PowerStation(27, 4, 1, ResourceType.Coal));
            normalStations.Add(new PowerStation(29, 5, 2, ResourceType.Coal));
            normalStations.Add(new PowerStation(33, 6, 3, ResourceType.Coal));
            normalStations.Add(new PowerStation(40, 6, 2, ResourceType.Coal));
            normalStations.Add(new PowerStation(16, 3, 2, ResourceType.Gas));
            normalStations.Add(new PowerStation(19, 3, 1, ResourceType.Gas));
            normalStations.Add(new PowerStation(26, 4, 1, ResourceType.Gas));
            normalStations.Add(new PowerStation(34, 6, 3, ResourceType.Gas));
            normalStations.Add(new PowerStation(39, 6, 2, ResourceType.Gas));
            normalStations.Add(new PowerStation(46, 7, 2, ResourceType.Gas));
            normalStations.Add(new PowerStation(18, 3, 2, ResourceType.Oil));
            normalStations.Add(new PowerStation(23, 4, 2, ResourceType.Oil));
            normalStations.Add(new PowerStation(30, 5, 2, ResourceType.Oil));
            normalStations.Add(new PowerStation(38, 6, 3, ResourceType.Oil));
            normalStations.Add(new PowerStation(42, 6, 2, ResourceType.Oil));
            normalStations.Add(new PowerStation(22, 5, 3, ResourceType.Mixed));
            normalStations.Add(new PowerStation(35, 5, 2, ResourceType.Mixed));
            normalStations.Add(new PowerStation(17, 2, 0, ResourceType.Green));
            normalStations.Add(new PowerStation(24, 3, 0, ResourceType.Green));
            normalStations.Add(new PowerStation(28, 3, 0, ResourceType.Green));
            normalStations.Add(new PowerStation(31, 4, 0, ResourceType.Green));
            normalStations.Add(new PowerStation(36, 5, 0, ResourceType.Green));
            normalStations.Add(new PowerStation(44, 6, 0, ResourceType.Green));
            normalStations.Add(new PowerStation(13, 2, 1, ResourceType.Nuclear));
            normalStations.Add(new PowerStation(21, 3, 1, ResourceType.Nuclear));
            normalStations.Add(new PowerStation(32, 5, 2, ResourceType.Nuclear));
            normalStations.Add(new PowerStation(37, 6, 2, ResourceType.Nuclear));
            normalStations.Add(new PowerStation(50, 7, 2, ResourceType.Nuclear));

            Shuffle(normalStations);

            var picked = startingStations.Take(currentMarketSize + futureMarketSize).OrderBy(x => x.Value).ToList();
            startingStations.RemoveRange(0, currentMarketSize + futureMarketSize);
            startingStations.RemoveRange(0, GetRemovedPowerPlants());

            CurrentMarket = picked.GetRange(0, currentMarketSize);
            FutureMarket = picked.GetRange(currentMarketSize, futureMarketSize);

            PowerStations = new Queue<PowerStation>(normalStations);
            ThirdStepPowerStations = new Queue<PowerStation>();
            startingStations.ForEach(p => PowerStations.Enqueue(p));
        }

        int GetRemovedPowerPlants()
        { 
            switch(playerCount)
            {
                case 2:
                    return 1;
                case 3:
                    return 2;
                case 4:
                    return 1;
                case 5:
                case 6:
                default:
                    return 0;
            }
        }

        static int GetFutureMarketSize(Location location)
        {
            if (location == Location.NA)
            {
                return 4;
            }
            if (location == Location.EU)
            {
                return 5;
            }
            return 0;
        }

        static int GetCurrentMarketSize(Location location)
        {
            return 4;
        }
        static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }


    }


}
