
namespace przypomnienie
{
    public class BidGenerator
    {
        UserRepository userRepositoryReference {  get;  set; }
        OrderBook OrderBookReference { get; set; }
        public BidGenerator(UserRepository userRepository,OrderBook orderBook) 
        {
            userRepositoryReference = userRepository;
            OrderBookReference = orderBook;

        }
            public void firstBids() //te bidy  ustala cene btc,bo nastapi 1 transakcja
        {
            Random rand = new Random();
            if (OrderBookReference.GetSellOffersCount() > 0) 
            {
                var allids = userRepositoryReference.GetAllids();
                
                for (int i = 0; i < 3; i++)
                {
                    var randUserId = allids[rand.Next(1, userRepositoryReference.GetAllids().Count())];
                    var bidId = Initlize.generateId();
                    var user = userRepositoryReference.getUserById(randUserId);
                    var price = rand.Next(500, 800);
                    while (user.userBalance < price)
                    {
                        price = rand.Next(500, 800);
                    }
                    var ammount = rand.Next(2, 10);
                    Bid bid = new Bid(price, ammount, randUserId);
                    OrderBookReference.addBid(bidId, bid);
                }
               
            }
            
        }

    

    }
}
