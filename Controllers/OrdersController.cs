 using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieBooking.Data.Cart;

//using MovieBooking.Data.Cart;
using MovieBooking.Data.Services;
using MovieBooking.Data.User;
using MovieBooking.Data.ViewModels;
using MovieBooking.Models;

namespace MovieBooking.Controllers
{   [Authorize]
    public class OrdersController :Controller

    {   private readonly IMoviesService _moviesService;

       private readonly IOrderService _orderService;

         private readonly ShoppingCart _shoppingCart;
        public OrdersController(IMoviesService moviesService, ShoppingCart shoppingCart ,IOrderService orderService)
        {
            _moviesService=moviesService;

            _shoppingCart=shoppingCart;

            _orderService=orderService;
            
        }
        public async Task< IActionResult> Index(){
           
           string userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
           string UserRole=User.FindFirstValue(ClaimTypes.Role);
            var orders= await _orderService.GetOrdersByUserIdAndRoleAsync(userId,UserRole);
            return View(orders);
        }
        public IActionResult ShoppingCart()
        {
               var items=_shoppingCart.GetShoppingCarts();
                _shoppingCart.ShoppingCartItems= items;
               var response=new ShoppingCartView(){
                   ShoppingCart=_shoppingCart,
                   ShoppingCartTotal=_shoppingCart.GetShoppingCartTotal()


               };

            return View(response);
        }

        public  async Task<IActionResult >AddToCart(int id){

            var item=await _moviesService.GetMovieByIdAsync(id);

            if(item !=null){
                _shoppingCart.AddItemToCart(item);
            }

            return RedirectToAction(nameof(ShoppingCart));


        }
         public  async Task<IActionResult >RemoveFromCart(int id){

            var item=await _moviesService.GetMovieByIdAsync(id);

            if(item !=null){
                _shoppingCart.RemoveItemFromCart(item);
            }

            return RedirectToAction(nameof(ShoppingCart));


        }

        public  async Task<IActionResult >CompleteOrder(){
            var item=_shoppingCart.GetShoppingCarts();
            string userId=User.FindFirstValue(ClaimTypes.Name);
            string EmailId=User.FindFirstValue(ClaimTypes.Email);
             await _orderService.StoreOrderAsync(item,userId,EmailId);

             await _shoppingCart.ClearShoppingCartAsync();

             return View("OrderCompleted");

        }
    }
}
