using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantOrder.Business;
using RestaurantOrder.Models;
using RestaurantOrder.ViewModels;

namespace RestaurantOrder.Services
{
  public class OrderService : IOrderService
  {
    private readonly RestaurantOrderContext _context;
    private readonly IOrderCreator _orderCreator;

    public OrderService(RestaurantOrderContext context, IOrderCreator orderCreator)
    {
      _context = context;
      _orderCreator = orderCreator;
    }

    public async Task<OrderViewModel> Create(PostOrderViewModel model)
    {
      var dishes = await _context.Dishes.ToListAsync();
      var order = _orderCreator.Create(model, dishes);
      _context.Orders.Add(order);

      await _context.SaveChangesAsync();
      return BuildOrderViewModel(order);
    }

    public async Task<IList<OrderViewModel>> FindAllAsync()
    {
      var orders = await _context.Orders.ToListAsync();
      return orders.Select(order => BuildOrderViewModel(order)).ToList();
    }

    public async Task<OrderViewModel> FindByIdAsync(long id)
    {
      var order = await _context.Orders.SingleOrDefaultAsync(o => o.Id == id);
      return BuildOrderViewModel(order);
    }

    private OrderViewModel BuildOrderViewModel(Order order)
    {
      if (order == null)
      {
        return null;
      }

      return new OrderViewModel()
      {
        Id = order.Id,
        Input = order.Input,
        Output = order.Output
      };
    }

    private DishViewModel BuildDishViewModel(Dish dish) => new DishViewModel()
    {
      Name = dish.Name,
      Type = dish.Type,
      TimeOfDay = dish.TimeOfDay
    };
  }
}
