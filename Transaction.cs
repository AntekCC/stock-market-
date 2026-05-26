using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace przypomnienie
{
    public  class Transaction
    {
        TradeRecord trade {  get; set; }
     
        public Transaction(TradeRecord _trade)
        {
         trade = _trade;  
        }
        public int  start()
        {
           
            using(var connection = new MySqlConnection("server=localhost;database=crypto;user=root;password=;"))
            {
                connection.Open();
                MySqlTransaction transaction ;
                transaction = connection.BeginTransaction();
                var Askresult = 0;
                var bidResult=0;

                string askQuery = "Update users set btc=btc-@btcAfter,balance=balance+@balanceAfter WHERE id=@id";
                try
                {
                    using (var Askcomm = new MySqlCommand(askQuery, connection, transaction))
                    {
                        Askcomm.Parameters.AddWithValue("@id", trade.askerID);
                        Askcomm.Parameters.AddWithValue("@btcAfter", trade.btcAmount);
                        Askcomm.Parameters.AddWithValue("@balanceAfter", trade.price);
                        Askresult = Askcomm.ExecuteNonQuery(); 

                    }
                    string bidQuery = "Update users set btc=btc+@btcAfter,balance=balance-@balanceAfter WHERE id=@id";

                    using (var Bidcomm = new MySqlCommand(bidQuery, connection, transaction))
                    {
                        Bidcomm.Parameters.AddWithValue("@id", trade.biderID);
                        Bidcomm.Parameters.AddWithValue("@btcAfter", trade.btcAmount);
                        Bidcomm.Parameters.AddWithValue("@balanceAfter", trade.price);
                        bidResult = Bidcomm.ExecuteNonQuery();
                    }
                    if(Askresult>0 && bidResult > 0)
                    {
                        transaction.Commit();
                        return 1;
                    }
                }
                catch (Exception ex) 
                { 
                transaction.Rollback();
                    return -1;
                }
                return -1;
            }
           
        }
    }
}
