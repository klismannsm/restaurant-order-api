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
  public class OrderService_FindAllAsyncShould
  {
    private IOrderService _orderService;

    public OrderService_FindAllAsyncShould()
    { }

    [Test]
    public async Task ReturnOrders()
    {
      SetupTest();
      var result = await _orderService.FindAllAsync();

      Assert.AreEqual(2, result.Count);
      Assert.AreEqual("morning,1,2,3", result[0].Input);
      Assert.AreEqual("morning, eggs, toast, coffee", result[0].Output);
      Assert.AreEqual("night,1,2,3,4", result[1].Input);
      Assert.AreEqual("night, steak, potato, wine, cake", result[1].Output);
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
