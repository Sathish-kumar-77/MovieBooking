using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieBooking.Data.Cart;

namespace MovieBooking.Data.ViewComponents
{  

    public class shoppingCart:ViewComponent

    {
        private readonly ShoppingCart _shoppingCart;

        public shoppingCart(ShoppingCart shoppingCart)
        {
            _shoppingCart=shoppingCart;
        }

        public IViewComponentResult Invoke(){

            var item= _shoppingCart.GetShoppingCarts();

            return View(item.Count);
        }
        
        
          
        }
    }
