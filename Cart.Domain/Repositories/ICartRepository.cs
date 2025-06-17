using Cart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Domain.Repositories;

public interface ICartRepository
{
    #region order
    Task<Order> GetOrderAsync(int id);
    Task<List<Order>> GetAllOrdersAsync();
    Task<Order> CreateNewOrderAsync(Order order);
    Task<Order> DeleteOrderAsync(int id);
    #endregion

    #region orderItem
    Task<OrderItem> GetItemFromOrderAsync(int orderId, int itemId);
    Task<List<OrderItem>> GetAllItemsFromOrderAsync(int orderId);
    Task<OrderItem> AddItemToOrderAsync(OrderItem item);
    Task<OrderItem> RemoveItemToOrderAsync(OrderItem item);
    #endregion
}
