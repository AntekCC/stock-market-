using MySql.Data.MySqlClient;
using przypomnienie;
using przypomnienie.przypomnienie;

string connString = "server=localhost;database=crypto;user=root;password=;";
UserRepository userRepository = new UserRepository();
TransactionHistoryDataBase transactionHistoryData = new TransactionHistoryDataBase();
OrderBook OrderBook = new OrderBook();
AskGenerator askGenerator = new AskGenerator(userRepository, OrderBook);
BidGenerator bidGenerator = new BidGenerator(userRepository, OrderBook);
UsersTransactionArchive archive = new UsersTransactionArchive();
MatchEngine matchEngine = new MatchEngine(OrderBook);

using (MySqlConnection createUserconn = new MySqlConnection(connString))
{
    createUserconn.Open();
    Initlize.Start(userRepository, createUserconn);

}
using (MySqlConnection loadUserconn = new MySqlConnection(connString))
{
    loadUserconn.Open();
    AddUsersFromDatabase loadUsers = new AddUsersFromDatabase();
    loadUsers.FillFromDb(loadUserconn, userRepository);
}
using (MySqlConnection loadTransactionHistory = new MySqlConnection(connString))
{
    loadTransactionHistory.Open();
    transactionHistoryData.load(loadTransactionHistory, archive);
}

askGenerator.generateAsks();
bidGenerator.firstBids();


var tradeInfo = matchEngine.order();
var orderBookAskAndBidId = matchEngine.getCurrentAskAndBidId();  //item1:AskId item2:BidId


var tradeRecord = tradeInfo.tradeRecord;

if (tradeRecord != null)
{
Transaction transaction = new Transaction(tradeRecord);
    var result = transaction.start();
    if (result == 1)//udana transakcja
    {
        var transactionId = Initlize.generateId();
        archive.add(transactionId, tradeRecord);
        transactionHistoryData.save(transactionId, tradeRecord);
        if (tradeInfo.bidLessThanAsk == true)
        {
            OrderBook.removeBidById(orderBookAskAndBidId.Item2);
            OrderBook.updateAskById(orderBookAskAndBidId.Item1, tradeInfo.askStateAfterTransaction);
            
        }
        else if(tradeInfo.bidLessThanAsk == false)
        {

        }
        else if (tradeInfo.bidEqualToAsk == true)
        {

        }



    }
    else //nieudana 
    {

    }
}



Console.WriteLine("chuj2");







