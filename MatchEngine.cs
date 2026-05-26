using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text;

namespace przypomnienie
{
    internal class MatchEngine
    {
        OrderBook OrderBookReference { get; set; }

        KeyValuePair<string, Ask> tradeAsker { get; set; } //orderbook sellOffers id , Ask
        KeyValuePair<string, Bid> tradeBider { get; set; } //orderbook buyoffers id , Bid



        public MatchEngine(OrderBook orderBook)
        {
            OrderBookReference = orderBook;
        }
        public TradeRecord handleTask()
        {

            var askerId = tradeAsker.Value.userId;
            var biderId = tradeBider.Value.userId;
            var offerAmmount = tradeBider.Value.ammount;
            var singleBtc = Math.Round(tradeAsker.Value.price / tradeAsker.Value.ammount, 1);
            var balanceAfter = Math.Round(offerAmmount * singleBtc, 2);
            TradeRecord tradeRecord = new TradeRecord(biderId, askerId, balanceAfter, offerAmmount,singleBtc);
            return tradeRecord;
        }


        public TradeInfo order()
        {
            var asksQueue = OrderBookReference.sellOffersWrapper().OrderBy(x => x.Value.price / x.Value.ammount).ToList();
            var bidQueue = OrderBookReference.buyOffersWrapper().OrderBy(x => x.Value.price / x.Value.ammount).ToList();
            bidQueue.Reverse();
            tradeAsker = asksQueue[0];
            tradeBider = bidQueue[0];
            bool bidLessThanAsk = false;
            bool bidEqualToAsk = false;
            if (tradeAsker.Value.ammount < tradeBider.Value.ammount) //ask zostanie zrealizowany w calosci
            {

                var record = handleTask();
                TradeInfo tradeUpdate = new TradeInfo(record, bidLessThanAsk, bidEqualToAsk);
                return tradeUpdate;
            }
            if (tradeAsker.Value.ammount > tradeBider.Value.ammount) //bid zostanie zrealizowany w calosci
            {

                var record = handleTask();
                bidLessThanAsk = true;
                var askStateAfterTransaction = OrdersService.updateAsk(record,tradeAsker.Value);
                TradeInfo tradeUpdate = new TradeInfo(record, bidLessThanAsk, bidEqualToAsk);
                tradeUpdate.setAsk(askStateAfterTransaction);

                return tradeUpdate;
            }
            if (tradeAsker.Value.ammount == tradeBider.Value.ammount) //bid  i ask zostana zrealizowane w calosci
            {

                var record = handleTask();
                bidEqualToAsk = true;
                TradeInfo tradeUpdate = new TradeInfo(record, bidLessThanAsk, bidEqualToAsk);
                return tradeUpdate;
            }

            return null;
        }


        public ValueTuple<string, string> getCurrentAskAndBidId()
        {
            var para = (tradeAsker.Key, tradeBider.Key);
            return para;
        }
    }
}
