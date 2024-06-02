using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using OrderLy_API.Models;
using OrderLy_API.Services;

namespace OrderLy_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<List<Order>> Get()
        {
            return await _orderService.GetAsync();
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Order>> Get(string id)
        {
            var order = await _orderService.GetAsync(id);

            if (order is null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Order newOrder)
        {
            //Set CreatedAt to current time
            newOrder.CreatedAt = DateTime.UtcNow;
            //Calculate overall and consumer costs
            List<Consumer> consumers = [.. newOrder.Consumers];
            foreach (Consumer cs in consumers)
            {
                cs.CalcCost();
                newOrder.Cost += cs.MoneyOwed;
            }
            newOrder.Consumers = consumers.ToArray();

            await _orderService.CreateAsync(newOrder);

            return CreatedAtAction(nameof(Get), new { id = newOrder.Id }, newOrder);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Order updatedOrder)
        {
            var order = await _orderService.GetAsync(id);

            if (order is null)
            {
                return NotFound();
            }

            updatedOrder.Id = order.Id;
            //Calculate overall and consumer costs
            List<Consumer> consumers = [.. updatedOrder.Consumers];
            foreach (Consumer cs in consumers)
            {
                cs.CalcCost();
                updatedOrder.Cost += cs.MoneyOwed;
            }
            updatedOrder.Consumers = consumers.ToArray();

            await _orderService.UpdateAsync(id, updatedOrder);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var order = await _orderService.GetAsync(id);
            if (order is null)
            {
                return NotFound();
            }
            await _orderService.RemoveAsync(id);
            return NoContent();
        }

        [HttpGet("/api/Order/Count")]
        public async Task<int> GetOrderCount()
        {
            var count = await _orderService.GetAsync();
            return count.Count;
        }

        [HttpGet("/api/Order/Weekly")]
        public async Task<List<Week>> GetWeeklyOrdersAsync()
        {
            var orders = await _orderService.GetWeeklyAsync();
            return orders;
        }

        [HttpGet("/api/Order/MaxCostOrder")]
        public async Task<ActionResult<Order>> GetMaxCostOrderAsync()
        {
            var order = await _orderService.GetMaxCostOrderAsync();
            if (order is not null)
            {
                return order;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("/api/Order/OverallCost")]
        public async Task<double> GetOverallCostAsync()
        {
            double costs = await _orderService.GetOverallCost();
            return costs;
        }
    }
}
