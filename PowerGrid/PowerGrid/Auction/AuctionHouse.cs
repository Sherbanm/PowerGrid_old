using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerGrid.Auction
{
    class AuctionHouse
    {
        public List<PowerStation> PowerStations;

        public List<PowerStation> CurrentMarket;

        public List<PowerStation> FutureMarket;

        public Auction CurrentAuction;

        int currentMarketSize;

        int futureMarketSize;

        public AuctionHouse(int CurrentMarketSize, int FutureMarketSize)
        {
            currentMarketSize = CurrentMarketSize;
            futureMarketSize = FutureMarketSize;
        }


    }


}
