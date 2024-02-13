using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieBooking.Models;

namespace MovieBooking.Data.Cart
{
    public class ShoppingCart
    {
        public AppDbContext _context { get; set; }

        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public ShoppingCart(AppDbContext context)
        {

            _context = context;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider service){

            ISession session=service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context=service.GetService<AppDbContext>();
            string CartId= session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId",CartId);
            return new ShoppingCart(context){ShoppingCartId=CartId};

        }
        public void AddItemToCart(Movie movie)
        {

            var ShoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);
            if (ShoppingCartItem == null)
            {
                ShoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1
                };

                _context.ShoppingCartItems.Add(ShoppingCartItem);
            }
            else
            {
                ShoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromCart(Movie movie)
        {

            var ShoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);
            if (ShoppingCartItem != null)
            {

                if (ShoppingCartItem.Amount > 1)
                {
                    ShoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(ShoppingCartItem);
                }


            }

            _context.SaveChanges();

        }

        public List<ShoppingCartItem> GetShoppingCarts()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId)
            .Include(n => n.Movie).ToList());
        }
        public double GetShoppingCartTotal() =>

            _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId)
            .Select(n => n.Movie.Price * n.Amount).Sum();

            public async Task ClearShoppingCartAsync(){

                var items=await _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId)
           .ToListAsync();

           _context.ShoppingCartItems.RemoveRange(items);

           await _context.SaveChangesAsync();


            }
    }
}
