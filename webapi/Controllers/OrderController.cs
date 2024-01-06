using Application.DTOs.Tables;
using Application.Handlers.Tables.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [Authorize]
    public class OrderController : BaseApiController
    {
        private readonly IOrderHandler _orderHandler;

        public OrderController(IOrderHandler orderHandler)
        {
            _orderHandler = orderHandler;
        }

        [HttpGet]
        [Route("MyOrders")]
        [Authorize(Policy = "CustomersOnly")]
        public async Task<IActionResult> GetSellersEventList()//[FromQuery] RequestDto request)
        {
            return HandleResult(await _orderHandler.GetCustomersOrderListAsync());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "CustomersOnly")]
        public async Task<IActionResult> GetCustomersOne(Guid id)
        {
            return HandleResult(await _orderHandler.GetCustomersOrderAsync(id));
        }

        [HttpPost]
        [Authorize(Policy = "CustomersOnly")]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)
        {
            return HandleResult(await _orderHandler.CreateCustomersOneAsync(orderDto));
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> EditEvent(Guid id, OrderDto orderDto)
        //{
        //    orderDto.Id = id;
        //    return HandleResult(await _orderHandler.eedit(orderDto));
        //}

        [HttpDelete("{id}")]
        //TODO
        [Authorize(Policy = "ModeratorsOnly")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            return HandleResult(await _orderHandler.DeleteCustomersOneAsync(id));
        }
    }
}
