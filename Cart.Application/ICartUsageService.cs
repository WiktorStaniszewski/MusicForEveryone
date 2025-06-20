﻿using Cart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application;

public interface ICartUsageService
{
    Task<Order> GetOrderAsync(int id);
    Task<List<Order>> GetAllOrdersAsync();
    Task<Order> AddOrderAsync(Order order);
    Task<Order> RemoveOrderAsync(Order order);


    Task<OrderItem> GetItemAsync(int orderId, int itemId);
    Task<List<OrderItem>> GetAllItemsAsync(int orderId);
    Task<List<OrderItem>> GetItemsByItemId(int itemId);
    Task<OrderItem> AddItemAsync(OrderItem item);
    Task<OrderItem> UpdateItemAsync(OrderItem item);
    Task<OrderItem> DeleteItemAsync(OrderItem item);
}
