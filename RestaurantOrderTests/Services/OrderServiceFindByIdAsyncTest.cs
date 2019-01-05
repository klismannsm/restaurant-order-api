using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using RestaurantOrder.Business;
using RestaurantOrder.Enumerations;
using RestaurantOrder.Models;
using RestaurantOrder.Services;
using RestaurantOrder.ViewModels;

namespace Tests.Services
{
  [TestFixture]
  public class OrderService_FindByIdAsyncShould
  {
    private IOrderService _orderService;

    public OrderService_FindByIdAsyncShould()
    { }

    [Test]
    public async Task ReturnOrders()
    {
      SetupTest();
      var result = await _orderService.FindByIdAsync(100);

      Assert.AreEqual("morning,1,2,3", result.Input);
      Assert.AreEqual("morning, eggs, toast, coffee", result.Output);
    }

    private void SetupTest()
    {
      var options = new DbContextOptionsBuilder<RestaurantOrderContext>()
        .UseInMemoryDatabase(databaseName: "FindByIdOrders")
        .Options;
      var context = new RestaurantOrderContext(options);
      context.Orders.Add(new Order()
      {
        Id = 100,
        Input = "morning,1,2,3",
        Output = "morning, eggs, toast, coffee"
      });
      context.SaveChanges();
      var orderCreator = new Mock<IOrderCreator>();
      _orderService = new OrderService(context, orderCreator.Object);
    }
  }
}
