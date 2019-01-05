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
  public class OrderService_FindAllAsyncShould
  {
    private IOrderService _orderService;

    public OrderService_FindAllAsyncShould()
    { }

    [Fact]
    public async Task ReturnOrders()
    {
      SetupTest();
      var result = await _orderService.FindAllAsync();

      Assert.Equal(2, result.Count);
      Assert.Equal("morning,1,2,3", result[0].Input);
      Assert.Equal("morning, eggs, toast, coffee", result[0].Output);
      Assert.Equal("night,1,2,3,4", result[1].Input);
      Assert.Equal("night, steak, potato, wine, cake", result[1].Output);
    }

    private void SetupTest()
    {
      var options = new DbContextOptionsBuilder<RestaurantOrderContext>()
        .UseInMemoryDatabase(databaseName: "FindAllOrders")
        .Options;
      var context = new RestaurantOrderContext(options);
      context.Orders.Add(new Order()
      {
        Input = "morning,1,2,3",
        Output = "morning, eggs, toast, coffee"
      });
      context.Orders.Add(new Order()
      {
        Input = "night,1,2,3,4",
        Output = "night, steak, potato, wine, cake"
      });
      context.SaveChanges();
      var orderCreator = new Mock<IOrderCreator>();
      _orderService = new OrderService(context, orderCreator.Object);
    }
  }
}
