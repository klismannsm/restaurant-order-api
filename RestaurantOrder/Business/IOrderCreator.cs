using System.Collections.Generic;
using RestaurantOrder.Models;
using RestaurantOrder.ViewModels;

namespace RestaurantOrder.Business
{
  public interface IOrderCreator
  {
      Order Create(PostOrderViewModel model, IEnumerable<Dish> dishes);
  }
}
