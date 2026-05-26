
using MySql.Data.MySqlClient;
using System.Collections;
using System.Data.Common;

namespace przypomnienie
{
    public class AddUsersToDatabase
    {

        public void FillDb(MySqlConnection conn, string idParameter, int balanceParameter, int btcParameter)
        {
            string query = "INSERT INTO users (id,balance, btc) VALUES (@id,@balance,@btc)";
            using (var comm = new MySqlCommand(query, conn))
            {
                comm.Parameters.AddWithValue("@id", idParameter);
                comm.Parameters.AddWithValue("@balance", balanceParameter);
                comm.Parameters.AddWithValue("@btc", btcParameter);
                comm.ExecuteNonQuery();
            }
        }
        public int NumberOfUsers(MySqlConnection conn)
        {
            int liczba = 0;
            string checkAmmount = "SELECT COUNT(*) AS liczba FROM users";
            using (var comm = new MySqlCommand(checkAmmount,conn))
            {
               liczba = Convert.ToInt32(comm.ExecuteScalar());
            }
            return liczba;
            
        }
    }


}

