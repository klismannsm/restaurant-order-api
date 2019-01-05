using System;
using System.Collections.Generic;
using NUnit.Framework;
using RestaurantOrder.Business;
using RestaurantOrder.Enumerations;
using RestaurantOrder.Models;
using RestaurantOrder.ViewModels;

namespace RestaurantOrderTests.Business
{
  [TestFixture]
  public class OrderCreator_CreateShould
  {
    private readonly IOrderCreator _orderCreator;
    private readonly IEnumerable<Dish> Dishes;

    public OrderCreator_CreateShould()
    {
      Dishes = new List<Dish>()
      {
        new Dish {
          Id = 1,
          CreatedAt = DateTime.Now,
          Type = DishTypes.entree,
          Name = "eggs",
          TimeOfDay = "morning"
        },
        new Dish {
          Id = 2,
          CreatedAt = DateTime.Now,
          Type = DishTypes.side,
          Name = "toast",
          TimeOfDay = "morning"
        },
        new Dish {
          Id = 3,
          CreatedAt = DateTime.Now,
          Type = DishTypes.drink,
          Name = "coffee",
          TimeOfDay = "morning"
        },
        new Dish {
          Id = 4,
          CreatedAt = DateTime.Now,
          Type = DishTypes.entree,
          Name = "steak",
          TimeOfDay = "night"
        },
        new Dish {
          Id = 5,
          CreatedAt = DateTime.Now,
          Type = DishTypes.side,
          Name = "potato",
          TimeOfDay = "night"
        },
        new Dish {
          Id = 6,
          CreatedAt = DateTime.Now,
          Type = DishTypes.drink,
          Name = "wine",
          TimeOfDay = "night"
        },
        new Dish {
          Id = 7,
          CreatedAt = DateTime.Now,
          Type = DishTypes.dessert,
          Name = "cake",
          TimeOfDay = "night"
        }
      };
      _orderCreator = new OrderCreator();
    }

    [Test]
    public void ReturnDishesIfValidMorningInput()
    {
      var model = new PostOrderViewModel()
      {
        Input = "morning,1,2,3"
      };
      var result = _orderCreator.Create(model, Dishes);

      Assert.AreEqual(model.Input, result.Input);
      Assert.AreEqual("morning, eggs, toast, coffee", result.Output);
    }

    [Test]
    public void ReturnDishesIfValidNightInput()
    {
      var model = new PostOrderViewModel()
      {
        Input = "night,1,2,3,4"
      };
      var result = _orderCreator.Create(model, Dishes);

      Assert.AreEqual(model.Input, result.Input);
      Assert.AreEqual("night, steak, potato, wine, cake", result.Output);
    }

    [Test]
    public void ReturnDishesIfUnorderedInputWasGiven()
    {
      var model = new PostOrderViewModel()
      {
        Input = "night,4,2,1,3"
      };
      var result = _orderCreator.Create(model, Dishes);

      Assert.AreEqual(model.Input, result.Input);
      Assert.AreEqual("night, steak, potato, wine, cake", result.Output);
    }

    [Test]
    public void ReturnErrorIfInvalidTimeOfDay()
    {
      var model = new PostOrderViewModel()
      {
        Input = "day,1,2,3,4"
      };
      var result = _orderCreator.Create(model, Dishes);

      Assert.AreEqual(model.Input, result.Input);
      Assert.AreEqual("error", result.Output);
    }

    [Test]
    public void ReturnErrorIfAskedForMoreEntriesInMorning()
    {
      var model = new PostOrderViewModel()
      {
        Input = "morning,1,1,2,3"
      };
      var result = _orderCreator.Create(model, Dishes);

      Assert.AreEqual(model.Input, result.Input);
      Assert.AreEqual("morning, eggs, error", result.Output);
    }

    [Test]
    public void ReturnErrorIfAskedForMoreSidesInMorning()
    {
      var model = new PostOrderViewModel()
      {
        Input = "morning,1,2,2,3"
      };
      var result = _orderCreator.Create(model, Dishes);

      Assert.AreEqual(model.Input, result.Input);
      Assert.AreEqual("morning, eggs, toast, error", result.Output);
    }

    [Test]
    public void ReturnDishesIfAskedForMoreDrinksInMorning()
    {
      var model = new PostOrderViewModel()
      {
        Input = "morning,1,2,3,3,3"
      };
      var result = _orderCreator.Create(model, Dishes);

      Assert.AreEqual(model.Input, result.Input);
      Assert.AreEqual("morning, eggs, toast, coffee(x3)", result.Output);
    }

    [Test]
    public void ReturnErrorIfAskedForMoreEntriesInNight()
    {
      var model = new PostOrderViewModel()
      {
        Input = "night,1,1,2,3,4"
      };
      var result = _orderCreator.Create(model, Dishes);

      Assert.AreEqual(model.Input, result.Input);
      Assert.AreEqual("night, steak, error", result.Output);
    }

    [Test]
    public void ReturnDishesIfAskedForMoreSidesInNight()
    {
      var model = new PostOrderViewModel()
      {
        Input = "night,1,2,2,3,4"
      };
      var result = _orderCreator.Create(model, Dishes);

      Assert.AreEqual(model.Input, result.Input);
      Assert.AreEqual("night, steak, potato(x2), wine, cake", result.Output);
    }

    [Test]
    public void ReturnErrorIfAskedForMoreDrinksInNight()
    {
      var model = new PostOrderViewModel()
      {
        Input = "night,1,2,3,3,4"
      };
      var result = _orderCreator.Create(model, Dishes);

      Assert.AreEqual(model.Input, result.Input);
      Assert.AreEqual("night, steak, potato, wine, error", result.Output);
    }

    [Test]
    public void ReturnErrorIfAskedForMoreDessertsInNight()
    {
      var model = new PostOrderViewModel()
      {
        Input = "night,1,2,3,4,4"
      };
      var result = _orderCreator.Create(model, Dishes);

      Assert.AreEqual(model.Input, result.Input);
      Assert.AreEqual("night, steak, potato, wine, cake, error", result.Output);
    }
  }
}
