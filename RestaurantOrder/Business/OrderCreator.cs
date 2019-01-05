using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using RestaurantOrder.Enumerations;
using RestaurantOrder.Models;
using RestaurantOrder.ViewModels;

namespace RestaurantOrder.Business
{
  public class OrderCreator : IOrderCreator
  {
    private readonly IList<string> ValidTimesOfDay = new List<string>() { "morning", "night" };

    private IEnumerable<Dish> Dishes { get; set; }

    public Order Create(PostOrderViewModel model, IEnumerable<Dish> dishes)
    {
      Dishes = dishes;
      var order = new Order() { Input = model.Input };
      var values = model.Input.Split(',');
      if (values.Count() < 2 || ValidTimesOfDay.IndexOf(values[0]) < 0)
      {
        order.Output = "error";
      }
      else
      {
        var output = OrderOutput(values);
        order.Output = output.Join(", ");
      }
      order.CreatedAt = DateTime.Now;
      return order;
    }

    public IList<string> OrderOutput(IList<string> values)
    {
      var timeOfDay = values[0];
      var dishes = GetDishes(timeOfDay, values);
      var output = new List<string>() { values[0] };
      foreach (var dish in dishes)
      {
        var dishName = FindDishName(timeOfDay, (DishTypes)dish.Key);
        if (dishName == "error")
        {
          output.Add(dishName);
          return output;
        }
        if (dish.Value > 1)
        {
          if (validDishQuantity(timeOfDay, (DishTypes)dish.Key))
          {
            output.Add(String.Format("{0}(x{1})", dishName, dish.Value));
          }
          else
          {
            output.Add(dishName);
            output.Add("error");
            break;
          }
        }
        else
        {
          output.Add(dishName);
        }
      }
      return output;
    }

    private SortedDictionary<int, int> GetDishes(string timeOfDay, IList<string> values)
    {
      var dishes = new SortedDictionary<int, int>();
      int dish;
      for (var index = 1; index < values.Count(); index++)
      {
        var isInt = Int32.TryParse(values[index], out dish);
        if (!isInt || !Enum.IsDefined(typeof(DishTypes), dish))
        {
          dish = (int)DishTypes.invalid;
        }
        AddDish(dishes, timeOfDay, dish);
      }
      return dishes;
    }

    private void AddDish(SortedDictionary<int, int> dishes, string timeOfDay, int dish)
    {
      if (dishes.ContainsKey(dish))
      {
        dishes[dish] += 1;
      }
      else
      {
        dishes.Add(dish, 1);
      }
    }

    private bool validDishQuantity(string timeOfDay, DishTypes dish)
    {
      return dish == DishTypes.invalid ||
        (timeOfDay == "morning" && dish == DishTypes.drink) ||
        (timeOfDay == "night" && dish == DishTypes.side);
    }

    private string FindDishName(string timeOfDay, DishTypes dishType)
    {
      var chosenDish = Dishes
        .FirstOrDefault(dish => dish.TimeOfDay == timeOfDay && dish.Type == dishType);
      if (chosenDish == null)
      {
        return "error";
      }
      return chosenDish.Name;
    }
  }
}
