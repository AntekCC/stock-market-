
namespace przypomnienie
{
    public class TradeRecord
    {
        public string biderID { get; private set; }
        public string askerID { get; private set; }
        public double price { get; private set; }
        public double btcAmount { get; private set; }
        public double currentBtcPrice { get; private set; }
        public TradeRecord(string _bidId, string _askId, double balanceAfter, double btcAfter, double currentBtcPrice)
        {
            this.biderID = _bidId;
            this.askerID = _askId;
            this.price = balanceAfter;
            this.btcAmount = btcAfter;
            this.currentBtcPrice = currentBtcPrice;
        }
    }
}
