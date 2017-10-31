using PowerGrid.Auctioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerGrid.GameState
{
    class GameObject
    {
        public List<Player> Players { get; set; }

        public AuctionHouse AuctionHouse { get; set; }

        public ResourceMarket ResourceMarket { get; set; }

        public Map Map { get; set; }

        public Steps CurrentStep { get; set; }

        public Phase CurrentPhase { get; set; }

        public int Round { get; set; }

        public GameObject(int numberOfPlayers, Location location)
        {
            Round = 2;
            Players = new List<Player>(numberOfPlayers);
            for (int i = 0; i < numberOfPlayers; i++)
            {
                var player = new Player($"Player {i}", 3);
                player.CanBuy = true;
                Players.Add(player);
            }

            AuctionHouse = new AuctionHouse(location, numberOfPlayers);
            ResourceMarket = new ResourceMarket(location, numberOfPlayers);
            CurrentStep = Steps.Step1;
            CurrentPhase = Phase.AuctionPowerPlants;
        }

        public void PerformBureaucracy()
        {
            CalculateIncomes();
            ResupplyResourceMarket();
            UpdatePowerPlantMarket();
        }

        public void UpdatePlayerOrder()
        {
            if (Round != 1)
            {
                Players = Players.OrderBy(p => p.PowerStations.Count).ThenBy(p => p.GetBiggestPowerStation()).ToList();
            }
            else
            {
                Players = Players.OrderBy(p => Guid.NewGuid()).ToList();
            }
        }

        private void UpdatePowerPlantMarket()
        {
            var x = AuctionHouse.FutureMarket.Last();
            AuctionHouse.ThirdStepPowerStations.Enqueue(x);
            AuctionHouse.FutureMarket.Remove(x);
            AuctionHouse.DrawPowerPlant();
        }

        private void ResupplyResourceMarket()
        {
            ResourceMarket.Refill(CurrentStep);
        }

        private void CalculateIncomes()
        {
            foreach (var player in Players)
            {
                int money = player.CalculateIncome();
                player.Money += money;
            }
        }
    }
}
