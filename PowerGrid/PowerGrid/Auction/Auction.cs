using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerGrid.Auctioning
{
    class Auction
    {
        public AuctionState State { get; set; }

        public Bid CurrentBid { get; set; }

        public PowerStation PowerStation { get; set; }

        List<Bid> Bids;
        
        public Auction(Bid startingBig, PowerStation powerStation)
        {
            State = AuctionState.Started;
            Bids = new List<Bid>();
            PowerStation = powerStation;
            CurrentBid = startingBig;

        }

        public void Bid(Bid bid)
        {
            if (bid.Value > CurrentBid.Value)
            {
                Bids.Add(bid);
                CurrentBid = bid;
            }
        }

        public void ResolveAuction()
        {
            CurrentBid.Player.ReceiveStation(PowerStation);
            CurrentBid.Player.Money -= CurrentBid.Value;
            CurrentBid.Player.CanBuy = false;
        }
    }
}
