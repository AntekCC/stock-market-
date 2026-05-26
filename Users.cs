
namespace przypomnienie
{
    public class Users
    {
        public double userBalance { get; private set; }
        public double userBtc { get; private set; }
        public Users(double balance, double btc)
        {
            this.userBalance = balance;
            this.userBtc = btc;
        }
    }
}
