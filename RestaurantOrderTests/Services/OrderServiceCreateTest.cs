using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using RestaurantOrder.Business;
using RestaurantOrder.Enumerations;
using RestaurantOrder.Models;
using RestaurantOrder.Services;
using RestaurantOrder.ViewModels;
using Xunit;

namespace RestaurantOrderTests.Services
{
  public class OrderService_CreateShould
  {
    private IOrderService _orderService;

    private readonly IEnumerable<Dish> Dishes;

    public OrderService_CreateShould()
    {
      Dishes = new List<Dish>()
      {
        new Dish {
          Id = 1,
          CreatedAt = DateTime.Now,
          Type = DishTypes.entree,
          Name = "eggs",
          TimeOfDay = "morning"
        },
        new Dish {
          Id = 2,
          CreatedAt = DateTime.Now,
          Type = DishTypes.side,
          Name = "toast",
          TimeOfDay = "morning"
        },
        new Dish {
          Id = 3,
          CreatedAt = DateTime.Now,
          Type = DishTypes.drink,
          Name = "coffee",
          TimeOfDay = "morning"
        },
        new Dish {
          Id = 4,
          CreatedAt = DateTime.Now,
          Type = DishTypes.entree,
          Name = "steak",
          TimeOfDay = "night"
        },
        new Dish {
          Id = 5,
          CreatedAt = DateTime.Now,
          Type = DishTypes.side,
          Name = "potato",
          TimeOfDay = "night"
        },
        new Dish {
          Id = 6,
          CreatedAt = DateTime.Now,
          Type = DishTypes.drink,
          Name = "wine",
          TimeOfDay = "night"
        },
        new Dish {
          Id = 7,
          CreatedAt = DateTime.Now,
          Type = DishTypes.dessert,
          Name = "cake",
          TimeOfDay = "night"
        }
      };
    }

    [Fact]
    public async Task ReturnOrder()
    {
      var model = new PostOrderViewModel()
      {
        Input = "morning,1,2,3"
      };
      var order = new Order()
      {
        Input = model.Input,
        Output = "morning, eggs, toast, coffee"
      };
      SetupTest(model, order);
      var result = await _orderService.Create(model);

      Assert.Equal(model.Input, result.Input);
      Assert.Equal(order.Output, result.Output);
    }

    private void SetupTest(PostOrderViewModel model, Order order)
    {
      var options = new DbContextOptionsBuilder<RestaurantOrderContext>()
        .UseInMemoryDatabase(databaseName: "CreateOrders")
        .Options;
      var context = new RestaurantOrderContext(options);
      context.Dishes.AddRange(Dishes);
      context.SaveChanges();
      var orderCreator = new Mock<IOrderCreator>();
      orderCreator.Setup(oc => oc.Create(model, Dishes)).Returns(order);
      _orderService = new OrderService(context, orderCreator.Object);
    }
  }
}
