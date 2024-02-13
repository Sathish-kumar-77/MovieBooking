using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieBooking.Data.ViewComponents;
using MovieBooking.Models;

namespace MovieBooking.Data.Services
{
    public interface IOrderService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items,string userId,string EmailId);

        Task<List<Order>>GetOrdersByUserIdAndRoleAsync(string userId,string UserRole);
    }
}