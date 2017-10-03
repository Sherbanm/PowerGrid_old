using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerGrid.Auction
{
    class Auction
    {
        AuctionState state;
        Bid CurrentBid;
        PowerStation PowerStation;

        List<Bid> Bids;
        
        public Auction(Bid startingBig, PowerStation powerStation)
        {
            state = AuctionState.Started;
            Bids = new List<Bid>();
            PowerStation = powerStation;
            CurrentBid = startingBig;

        }

        void Bid(Bid bid)
        {
            if (bid.Value > CurrentBid.Value)
            {
                Bids.Add(bid);
                CurrentBid = bid;
            }
        }

        void ResolveAuction()
        {
            CurrentBid.Player.ReceiveStation(PowerStation);
        }
    }
}
