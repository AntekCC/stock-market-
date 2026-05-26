using System;
using System.Collections.Generic;
using System.Text;

namespace przypomnienie
{
    public  class UsersTransactionArchive
    {
       private Dictionary<string, TradeRecord> TransactionArchive = new();
       
        public void add(string transactionId,TradeRecord transactionRecord)
        {
            TransactionArchive.Add(transactionId, transactionRecord);
        }
    }
}
