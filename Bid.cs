using System;
using System.Collections.Generic;
using System.Text;

namespace przypomnienie
{
    public  class Bid
    {
        public double price {  get; private set; }
        public double ammount { get; private set; }
        public string userId { get; private set; }


        public Bid(double price , double ammount,string id ) 
        { 
        this.price = price;
        this.ammount = ammount;
        this.userId = id;
        }

    }
    
}
