using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantOrder.ViewModels
{
  public class OrderViewModel
  {
    public long Id { get; set; }
    public string Input { get; set; }
    public string Output { get; set; }
  }
}
