using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieBooking.Data.Cart;

namespace MovieBooking.Data.ViewModels
{
    public class ShoppingCartView
    {
        public ShoppingCart ShoppingCart{get;set;}

        public double ShoppingCartTotal{get;set;}
    }
}