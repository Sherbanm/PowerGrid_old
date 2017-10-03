using PowerGrid.Auction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerGrid.GameState
{
    class GameState
    {
        public List<Player> Players { get; set; }

        public AuctionHouse AuctionHouse { get; set; }

        public Map Map { get; set; }

        public Steps CurrentStep { get; set; }

        public Phase CurrentPhase { get; set; }

        public GameState()
        {
            CurrentStep = Steps.Step1;
            CurrentPhase = Phase.AuctionPowerPlants;
        }

        public void PerformBureaucracy()
        {
            if (CurrentPhase == Phase.BuildGenerators)
            {
                CalculateIncomes();
                ResupplyResourceMarket();
                UpdatePowerPlantMarket();
            }
            CurrentPhase = Phase.DeterminePlayerOrder;
        }

        private void UpdatePowerPlantMarket()
        {
            throw new NotImplementedException();
        }

        private void ResupplyResourceMarket()
        {
            throw new NotImplementedException();
        }

        private void CalculateIncomes()
        {
            throw new NotImplementedException();
        }
    }
}
