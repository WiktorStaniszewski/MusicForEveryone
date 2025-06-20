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

    public async Task<List<OrderItem>> GetAllItemsAsync(int orderId)
    {
        var result = await _cartRepository.GetAllItemsFromOrderAsync(orderId);
        return result;
    }

    public async Task<List<OrderItem>> GetItemsByItemId(int itemId)
    {
        var result = await _cartRepository.GetItemsByItemIdAsync(itemId);
        return result;
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        var result = await _cartRepository.GetAllOrdersAsync();
        return result;
    }

    public async Task<OrderItem> GetItemAsync(int itemId, int orderId)
    {
        var result = await _cartRepository.GetItemFromOrderAsync(itemId, orderId);
        return result;
    }

    public async Task<Order> GetOrderAsync(int id)
    {
        var result = await _cartRepository.GetOrderAsync(id);
        return result;
    }

    public Task<OrderItem> UpdateItemAsync(OrderItem item)
    {
        throw new NotImplementedException();
    }

    public async Task<Order> RemoveOrderAsync(Order order)
    {
        var result = await _cartRepository.DeleteOrderAsync(order);
        return result;
    }

    public async Task<OrderItem> DeleteItemAsync(OrderItem item)
    {
        var result = await _cartRepository.RemoveItemToOrderAsync(item);
        return result;
    }
}
