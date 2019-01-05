using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using RestaurantOrder.Controllers;
using RestaurantOrder.Enumerations;
using RestaurantOrder.Models;
using RestaurantOrder.Services;
using RestaurantOrder.ViewModels;
using Xunit;

namespace RestaurantOrderTests.Controllers
{
  public class OrdersController_GetOrdersShould
  {
    private OrdersController _ordersController;

    public OrdersController_GetOrdersShould()
    { }

    [Fact]
    public async Task ReturnOrders()
    {
      var models = new List<OrderViewModel>()
      {
        new OrderViewModel() { Input = "input1", Output = "output1" },
        new OrderViewModel() { Input = "input2", Output = "output2" }
      };
      SetupTest(models);
      var result = await _ordersController.GetOrders();

      var okResult = Assert.IsType<OkObjectResult>(result);
      var returnValue = Assert.IsType<List<OrderViewModel>>(okResult.Value);
      Assert.Equal(models, returnValue);
    }

    private void SetupTest(IList<OrderViewModel> models)
    {
      var orderService = new Mock<IOrderService>();
      orderService.Setup(os => os.FindAllAsync()).ReturnsAsync(models);
      _ordersController = new OrdersController(orderService.Object);
    }
  }
}
