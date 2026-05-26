

using System.ComponentModel.DataAnnotations;

namespace przypomnienie
{

    namespace przypomnienie
    {
        public class AskGenerator
        {
            UserRepository userRepositoryReference { get; set; }
            OrderBook OrderBookReference { get; set; }
            Random rand = new Random();
            public AskGenerator(UserRepository userRepository, OrderBook OrderBook)
            {
                userRepositoryReference = userRepository;
                OrderBookReference = OrderBook;

            }
            public void generateAsks()
            {
                var initnialUsers = userRepositoryReference.initialUsers();
                firstAsks(initnialUsers);
                var askOffersCount = OrderBookReference.GetSellOffersCount();
                Thread generateInBackground = new Thread(newAsks);
                generateInBackground.Start();
                
            }

            public void firstAsks(Dictionary<string,Users> users)
            {
                
                while (users.Count>0)
                {
                    var askId = Initlize.generateId();
                    var userId = users.Keys.First();
                    var percent = rand.Next(5, 16);
                    var price = rand.Next(500, 700);
                    var ammountOfBtc = Math.Round(users.Values.First().userBtc * percent/100, 1);
                    Ask ask = new Ask(price, ammountOfBtc, userId);
                    OrderBookReference.addAsk(askId, ask);
                    users.Remove(userId);
                }
                
            }
            public void newAsks()
            {
                
                var usersCount = userRepositoryReference.checkCount();
                var allIds = userRepositoryReference.GetAllids();
                
                
                var randomUser = userRepositoryReference.getUserById(allIds[rand.Next(1, usersCount)]);
                

            }


        }
    }

}
