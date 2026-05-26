using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace przypomnienie
{
    public class AddUsersFromDatabase
    {
        public void FillFromDb(MySqlConnection conn, UserRepository userRepository)
        {
            int numberOfusersLocal = userRepository.checkCount();
            if (numberOfusersLocal == 0)
            {
                string query = "SELECT id, balance, btc FROM users;";
                using (var comm = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = comm.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Users user = new Users(Convert.ToInt32(reader["balance"]), Convert.ToInt32(reader["btc"]));
                            userRepository.AddUser(reader["id"].ToString(), user);
                        }
                    }
                }
            }
        }
    }
}

