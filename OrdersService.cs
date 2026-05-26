using System;
using System.Collections.Generic;
using System.Text;

namespace przypomnienie
{
    public static class OrdersService
    {

        public static Ask updateAsk(TradeRecord tradeData, Ask askStateBeforeTransaction )
        {
            var btcAmmountAfterTransaction = askStateBeforeTransaction.ammount - tradeData.btcAmount;
            var btcPriceAfterTransaction = btcAmmountAfterTransaction * tradeData.currentBtcPrice;
            Ask ask = new Ask(btcPriceAfterTransaction, btcAmmountAfterTransaction,askStateBeforeTransaction.userId);
            return ask;
        }
    }
}
