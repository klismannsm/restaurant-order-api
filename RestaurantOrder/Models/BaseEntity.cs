﻿using System;

namespace RestaurantOrder.Models
{
  public class BaseEntity
  {
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
  }
}
