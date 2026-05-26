

namespace przypomnienie
{
    public class OrderBook  
    {
        private Dictionary<string, Bid> buyOffers = new();
        private Dictionary<string, Ask> sellOffers = new();


        public void addAsk(string id,Ask ask)
        {
           sellOffers.Add(id, ask);
        }
        public void addBid(string id, Bid bid)
        {
            buyOffers.Add(id, bid);
        }

        public void removeAskById(string id)
        {
            sellOffers.Remove(id);
        }
        public void removeBidById(string id)
        {
            buyOffers.Remove(id);
        }

        public void updateAskById(string id,Ask ask)
        {
            if (!string.IsNullOrEmpty(id) && sellOffers.ContainsKey(id))
            {
                sellOffers[id] = ask;
                var siema = getAskById(id);
            }

        }
        public Ask getAskById(string id)
        {
            if (!string.IsNullOrEmpty(id) && sellOffers.ContainsKey(id))
            {
                    return sellOffers[id];
            }
            return null;
        }

        public Bid getBidById(string id)
        {
            if (!string.IsNullOrEmpty(id) && buyOffers.ContainsKey(id))
            {
                return buyOffers[id];
            }
            return null;
        }

        public void updateBidById(string id,Bid _bid)
        {
            if (_bid != null && buyOffers.ContainsKey(id))
            {
                buyOffers[id] = _bid;
            }
            
        }








        public int GetSellOffersCount()
        {
            return sellOffers.Count(); 
        }
        public int GetBuyOffersCount()
        {
            return buyOffers.Count();
        }
        public  IDictionary<string,Ask> sellOffersWrapper()
        {
            if (this.sellOffers.Count != 0)
            {
                IDictionary<string, Ask> askWrapper = this.sellOffers.AsReadOnly();
                return askWrapper;
            }
            return null;

        }
        public IDictionary<string, Bid> buyOffersWrapper()
        {
            if (this.buyOffers.Count != 0)
            {
                IDictionary<string, Bid> bidWrapped = this.buyOffers.AsReadOnly();
                return bidWrapped;
            }
            return null;

        }





    }
}
