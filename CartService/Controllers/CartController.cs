using Cart.Application;
using Cart.Domain.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CartService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IEShopConnectService _EShopConnectService;
        private readonly ICartUsageService _cartUsageService;

        public CartController(IEShopConnectService eShopConnectService, ICartUsageService cartUsageService)
        {
            _EShopConnectService = eShopConnectService;
            _cartUsageService = cartUsageService;
        }



        // GET: api/<CartController>
        [HttpGet("orders")]
        public async Task<ActionResult> GetAllOrders()
        {
            var result = _cartUsageService.GetAllOrdersAsync();
            return Ok(result);
        }

        // GET api/<CartController>/5
        [HttpGet("order_{id}")]
        public async Task<ActionResult> GetOrder(int id)
        {
            var result = _cartUsageService.GetOrderAsync(id);
            return Ok(result);
        }

        [HttpGet("items_from_order_{id}")]
        public async Task<ActionResult> GetItemsFromOrder(int id)
        {
            var result = _cartUsageService.GetAllItemsAsync(id);
            return Ok(result);
        }

        [HttpGet("items_by_itemId_{id}")]
        public async Task<ActionResult> GetItemsByItemId(int id)
        {
            var result = _cartUsageService.GetItemsByItemId(id);
            return Ok(result);
        }

        [HttpGet("specific_item")]
        public async Task<ActionResult> GetItemByIdAndOrderId(int itemId, int orderId)
        {
            var result = _cartUsageService.GetItemAsync(itemId, orderId);
            return Ok(result);
        }

        // POST api/<CartController>
        [HttpPost("item_{itemId}")]
        public async Task<ActionResult> AddToCart(int itemId, int orderId)
        {
            try
            {
                var order = await _cartUsageService.GetOrderAsync(orderId);
                //if (order == null) throw new InvalidOperationException("no order of such id exists");  niepotrzebne, błąd wyrzuca dzięki temu co wyżej

                var newItem = await _EShopConnectService.GetProductAsync(itemId);
                if (newItem.Deleted == true) throw new InvalidOperationException("product is not present");

                OrderItem item = new OrderItem
                {
                    Id = newItem.Id,
                    Name = newItem.Name,
                    Price = newItem.Price,
                    OrderId = orderId,
                    Quantity = 1
                };

                var result = await _cartUsageService.AddItemAsync(item);

                return Ok(result);
            }
            catch (Exception ex) { throw ex; }
        }

        // POST api/<CartController>/Order_5
        [HttpPost]
        public async Task<ActionResult> PostOrder([FromBody] Order order)
        {
            var result = await _cartUsageService.AddOrderAsync(order);
            return Ok(result);
        }


        /*
        // PUT api/<CartController>/5
        [HttpPut("item_{itemId}")]
        public async Task<ActionResult> Put(int itemId, int orderId)
        {
            try
            {
                var newItem = await _EShopConnectService.GetProductAsync(itemId);
                if (newItem.Deleted == true) throw new InvalidOperationException("product is not present");

                var CurrentItemsInOrders = await _cartUsageService.GetItemsByItemId(itemId);
                if (CurrentItemsInOrders.Count == 0)
                {
                    OrderItem item = new OrderItem
                    {
                        Id = newItem.Id,
                        Name = newItem.Name,
                        Price = newItem.Price,
                        OrderId = orderId,
                        Quantity = 1
                    };
                    var result = await _cartUsageService.AddItemAsync(item);
                    return Ok(result);
                }
                else
                {
                    int CurrentItemsCount = 0;
                    foreach (var element in CurrentItemsInOrders) { CurrentItemsCount += element.Quantity; }
                    if (CurrentItemsCount <= newItem.Stock)
                    {

                    }
                    else
                    {
                        throw new Exception("not enough of the product in stock to increase the order");
                    }
                }
                //wyszukaj czy jest już ten item
                //jeśli nie, dodaj nowy
                //jeśli tak, czy quantity+1 <= stock?
                //jak tak, dodaj quantity+=1
                //jak nie, powiedz że się nie da



            }
            catch (Exception ex) { throw ex; }
        }
        */


        // DELETE api/<CartController>/5
        [HttpDelete("cart_{id}")]
        public void DeleteCart(int id)
        {
        }
        [HttpDelete("order_{order_id}")]
        public void DeleteOrderItem(int cart_id, int order_id)
        {
        }
    }
}
