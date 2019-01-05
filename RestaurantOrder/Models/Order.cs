using System;
using System.Collections.Generic;

namespace RestaurantOrder.Models
{
  public class Order : BaseEntity
  {
    public string Input { get; set; }
    public string Output { get; set; }
  }
}
