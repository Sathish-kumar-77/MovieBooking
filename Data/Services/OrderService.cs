using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieBooking.Models;

namespace MovieBooking.Data.Services
{
    public class OrderService : IOrderService
    {      
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context=context;
        }
         public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId,string UserRole)
        {
          
            
           var orderss = await _context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Movie).
           Include(n=>n.User).ToListAsync();
           if(UserRole !="Admin"){
            orderss=orderss.Where(n=>n.UserId == userId).ToList();
           }
              return orderss;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string EmailId)
        {
             var Order=new Order(){
                UserId=userId,
                Email =EmailId

             };
             await _context.Orders.AddAsync(Order);
             await _context.SaveChangesAsync();
             foreach (var item in items){
               var orderItem= new OrderItem(){
                Amount=item.Amount,
                MovieId=item.Movie.Id,
                OrderId=Order.Id,
                Price=item.Movie.Price,
               };
               await _context.OrderItems.AddAsync(orderItem);
             }

             await _context.SaveChangesAsync();
        }
    }
}