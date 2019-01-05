﻿using System;
using RestaurantOrder.Enumerations;

namespace RestaurantOrder.Models
{
  public class Dish : BaseEntity
  {
    public string Name { get; set; }
    public DishTypes Type { get; set; }
    public string TimeOfDay { get; set; }
  }
}
