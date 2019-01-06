using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantOrder.Services;
using RestaurantOrder.ViewModels;
using RestaurantOrder.Models;

namespace RestaurantOrder.Controllers
{
  [Route("api/v1/[controller]")]
  [ApiController]
  [EnableCors("AllowSpecificOrigins")]
  public class OrdersController : ControllerBase
  {
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
      _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
    }

    // GET: api/orders
    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
      return Ok(await _orderService.FindAllAsync());
    }

    // GET: api/orders/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(long id)
    {
      var order = await _orderService.FindByIdAsync(id);

      if (order == null)
      {
        return NotFound();
      }

      return Ok(order);
    }

    // // POST: api/orders
    [HttpPost]
    public async Task<IActionResult> PostOrder(PostOrderViewModel model)
    {
      var order = await _orderService.Create(model);
      return CreatedAtAction("GetOrder", new { id = order.Id }, order);
    }
  }
}
