using System;
using RestaurantOrder.Enumerations;

namespace RestaurantOrder.ViewModels
{
  public class DishViewModel
  {
    public string Name { get; set; }
    public DishTypes Type { get; set; }
    public string TimeOfDay { get; set; }
  }
}
