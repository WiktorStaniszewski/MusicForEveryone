using Cart.Domain.Models;
using Microsoft.EntityFrameworkCore;
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

    public async Task<List<OrderItem>> GetAllItemsFromOrderAsync(int orderId)
    {
        List<OrderItem> datalist = await _context.OrderItems.ToListAsync();
        List<OrderItem> output = new List<OrderItem>();
        foreach (var element in datalist)
        {
            if (element.OrderId == orderId)
            {
                output.Add(element);
            }
        }
        return output;
    }

    public async Task<List<OrderItem>> GetItemsByItemIdAsync(int itemId)
    {
        List<OrderItem> datalist = await _context.OrderItems.ToListAsync();
        List<OrderItem> output = new List<OrderItem>();
        foreach (var element in datalist)
        {
            if (element.Id == itemId)
            {
                output.Add(element);
            }
        }
        return output;
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<OrderItem> GetItemFromOrderAsync(int itemId, int orderId)
    {
        List<OrderItem> allItems = await _context.OrderItems.ToListAsync();
        List<OrderItem> orderItems = new List<OrderItem>();
        OrderItem item = new OrderItem();
        foreach (var element in allItems)
        {
            if (element.OrderId == orderId)
            {
                orderItems.Add(element);
            }
        }
        foreach(var element in orderItems)
        {
            if(element.Id == itemId) item = element;
        }
        return item;
    }

    public async Task<Order> GetOrderAsync(int id)
    {
        var order = await _context.Orders.Where(x => x.Id == id).FirstOrDefaultAsync();
        if (order == null)
        {
            throw new Exception($"Order with ID {id} not found.");
        }
        return order;
    }

    public async Task<OrderItem> RemoveItemToOrderAsync(OrderItem item)
    {
        throw new NotImplementedException();
    }
}
