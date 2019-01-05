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
  public class OrdersController_GetOrderShould
  {
    private OrdersController _ordersController;

    public OrdersController_GetOrderShould()
    { }

    [Fact]
    public async Task ReturnNotFoundIfNonExistantOrder()
    {
      SetupTest(null);
      var result = await _ordersController.GetOrder(100);

      Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task ReturnOrderIfFound()
    {
      var model = new OrderViewModel()
      {
        Input = "morning,1,2,3",
        Output = "morning, eggs, toast, coffee"
      };

      SetupTest(model);
      var result = await _ordersController.GetOrder(100);

      var okResult = Assert.IsType<OkObjectResult>(result);
      var returnValue = Assert.IsType<OrderViewModel>(okResult.Value);
      Assert.Equal(model, returnValue);
    }

    private void SetupTest(OrderViewModel model)
    {
      var orderService = new Mock<IOrderService>();
      orderService.Setup(os => os.FindByIdAsync(100)).ReturnsAsync(model);
      _ordersController = new OrdersController(orderService.Object);
    }
  }
}
