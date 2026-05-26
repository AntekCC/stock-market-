using System;
using System.Collections.Generic;
using System.Text;

namespace przypomnienie
{
    public class Ask
    {
        public string userId {  get; private set; }
        public double price {  get; private set; }
        public double ammount { get; private set; }

        public Ask(double price, double ammount,string id)
        {
           this.price = price;
           this.ammount = ammount;
            this.userId = id;
        }
       
    }
}
