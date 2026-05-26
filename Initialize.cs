 using MySql.Data.MySqlClient;
namespace przypomnienie

{
    public static class Initlize
    {

        public static void Start(UserRepository _userRepository, MySqlConnection conn)
        {
            UserRepository userRepository = _userRepository;
            AddUsersToDatabase addTo = new AddUsersToDatabase();

            int numberOfusersDB = addTo.NumberOfUsers(conn);
            int numberOfusersLocal = userRepository.checkCount();

            if (numberOfusersDB == 0 || numberOfusersDB == 0)
            {
                Random rand = new Random();

                for (int i = 0; i < 20; i++)
                {
                    int btc = rand.Next(1, 100);
                    int balance = rand.Next(100, 10000);
                    Users user = new Users(balance, btc);
                    var userId = generateId();
                    userRepository.AddUser(userId, user);
                    addTo.FillDb(conn, userId, balance, btc);

                }
            }
        }
        public static string generateId()
        {
            Random rand = new Random();
            string chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?";
            var generator = chars.ToList().Select(y => chars[rand.Next(0, chars.Count())]).Take(5).ToList();
            var id = String.Join("", generator);
            return id;
        }
    }
}
