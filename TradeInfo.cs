using System;
using System.Collections.Generic;
using System.Text;

namespace przypomnienie
{

    public class TradeInfo
    {
        public TradeRecord tradeRecord {  get; private  set; }
        public bool bidLessThanAsk { get; private set; }
        public bool bidEqualToAsk { get; private set; }
        public Ask askStateAfterTransaction { get; private set; }
        public Bid bidtateAfterTransaction { get; private set; }
        public TradeInfo(TradeRecord record,bool _bidLessThanAsk, bool _bidEqualToAsk)
        {
            tradeRecord = record;
            bidLessThanAsk = _bidLessThanAsk;
            bidEqualToAsk = _bidEqualToAsk;
           
        }
        public void setAsk(Ask ask)
        {
            askStateAfterTransaction = ask;
        }
        public void setBid(Bid bid)
        {
            bidtateAfterTransaction = bid;
        }


    }
}
