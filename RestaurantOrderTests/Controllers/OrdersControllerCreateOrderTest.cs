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
  public class OrdersController_PostOrderShould
  {
    private OrdersController _ordersController;

    public OrdersController_PostOrderShould()
    { }

    [Fact]
    public async Task ReturnOrder()
    {
      var model = new PostOrderViewModel() { Input = "input1" };
      var order = new OrderViewModel() { Input = "input1", Output = "output1" };
      SetupTest(model, order);
      var result = await _ordersController.PostOrder(model);

      var createdAtResult = Assert.IsType<CreatedAtActionResult>(result);
      var returnValue = Assert.IsType<OrderViewModel>(createdAtResult.Value);
      Assert.Equal(order, returnValue);
    }

    private void SetupTest(PostOrderViewModel model, OrderViewModel order)
    {
      var orderService = new Mock<IOrderService>();
      orderService.Setup(os => os.Create(model)).ReturnsAsync(order);
      _ordersController = new OrdersController(orderService.Object);
    }
  }
}
