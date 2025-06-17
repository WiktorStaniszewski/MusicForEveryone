using Cart.Domain.Models;
using Cart.Domain.Repositories;

namespace Cart.Application;

public class CartUsageService : ICartUsageService
{
    private ICartRepository _cartRepository;
    public CartUsageService(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<OrderItem> AddItemAsync(OrderItem item)
    {
        var result = await _cartRepository.AddItemToOrderAsync(item);
        return result;
    }

    public async Task<Order> AddOrderAsync(Order order)
    {
        var result = await _cartRepository.CreateNewOrderAsync(order);
        return result;
    }

    public Task<List<OrderItem>> GetAllItemsAsync(int orderId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Order>> GetAllOrdersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<OrderItem> GetItemAsync(int orderId, int itemId)
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetOrderAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<OrderItem> UpdateItemAsync(OrderItem item)
    {
        throw new NotImplementedException();
    }

    public Task<Order> UpdateOrderAsync(int id)
    {
        throw new NotImplementedException();
    }
}
