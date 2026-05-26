using MySql.Data.MySqlClient;
using System.Collections;
namespace przypomnienie
{
    internal class TransactionHistoryDataBase
    {

        public void save(string id,TradeRecord record)
        {
            using (var conn = new MySqlConnection("server=localhost;database=crypto;user=root;password=;"))
            {
                string query = "INSERT INTO transactionhistory (id,biderId,askerId,amount,price,CurrentSingleBtcPrice) VALUES (@id,@bider,@asker,@amount,@price,@singleBtc)";
                
                using (var comm = new MySqlCommand(query, conn))
                {
                    conn.Open();
                    comm.Parameters.AddWithValue("@id",id);
                    comm.Parameters.AddWithValue("@bider",record.biderID);
                    comm.Parameters.AddWithValue("@asker",record.askerID);
                    comm.Parameters.AddWithValue("@amount",record.btcAmount);
                    comm.Parameters.AddWithValue("@price",record.price);
                    comm.Parameters.AddWithValue("@singleBtc", record.currentBtcPrice);
                    comm.ExecuteNonQuery();
                }
            }
            
        }
        public void load(MySqlConnection connection, UsersTransactionArchive archive)
        {
            string query = "SELECT id,biderId,askerId,amount,price,CurrentSingleBtcPrice FROM transactionhistory";
            using (var loadComm = new MySqlCommand(query, connection))
            {
                using (MySqlDataReader reader = loadComm.ExecuteReader() )
                {
                    while (reader.Read()) 
                    {
                        TradeRecord record = new TradeRecord(reader["biderId"].ToString(), reader["askerId"].ToString(), Convert.ToDouble(reader["price"]), Convert.ToDouble(reader["amount"]), Convert.ToDouble(reader["CurrentSingleBtcPrice"]));
                        archive.add(reader["id"].ToString(), record);
                    }
                }
                
            }

        }
    }
}
