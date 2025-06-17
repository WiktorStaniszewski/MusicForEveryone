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
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CartController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CartController>
        [HttpPost("item_{itemid}")]
        public async Task<ActionResult> AddToCart(int itemId, int orderId)
        {
            try
            {
                var newItem = await _EShopConnectService.GetProductAsync(itemId);
                if (newItem.Deleted == true) throw new InvalidOperationException("product is not present");
                OrderItem item = new OrderItem { 
                    Id = newItem.Id,
                    Name = newItem.Name,
                    Price = newItem.Price,
                    OrderId = orderId,
                    Quantity = 1
                };

                var result = await _cartUsageService.AddItemAsync(item);

                return Ok(result);
            } catch (Exception ex) { throw ex; }
        }

        // POST api/<CartController>/Order_5
        [HttpPost("cart_{id}")]
        public async Task<ActionResult> PostOrder([FromBody] Order order)
        {
            var result = await _cartUsageService.AddOrderAsync(order);
            return Ok(result);
        }

        // PUT api/<CartController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

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
