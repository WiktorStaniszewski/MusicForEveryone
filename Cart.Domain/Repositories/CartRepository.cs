using Cart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Domain.Repositories;

public class CartRepository : ICartRepository
{
    private readonly DataContext _context;
    public CartRepository (DataContext context)
    {
        _context = context;
    }

    public async Task<OrderItem> AddItemToOrderAsync(OrderItem item)
    {
        _context.Add(item);
        await _context.SaveChangesAsync();
        return item;
        throw new NotImplementedException();
    }

    public async Task<Order> CreateNewOrderAsync(Order order)
    {
        _context.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<Order> DeleteOrderAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<OrderItem>> GetAllItemsFromOrderAsync(int orderId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<OrderItem> GetItemFromOrderAsync(int orderId, int itemId)
    {
        throw new NotImplementedException();
    }

    public async Task<Order> GetOrderAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<OrderItem> RemoveItemToOrderAsync(OrderItem item)
    {
        throw new NotImplementedException();
    }
}
