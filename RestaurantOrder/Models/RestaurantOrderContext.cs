using System;
using Microsoft.EntityFrameworkCore;
using RestaurantOrder.Enumerations;
using RestaurantOrder.Models;

namespace RestaurantOrderApi.Models
{
  public class RestaurantOrderContext : DbContext
  {
    public RestaurantOrderContext(DbContextOptions<RestaurantOrderContext> options)
        : base(options)
    {
    }

    public DbSet<Dish> Dishes { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Dish>().HasData(
        new Dish
        {
          Id = 1,
          CreatedAt = DateTime.Now,
          Type = DishTypes.entree,
          Name = "eggs",
          TimeOfDay = "morning"
        },
        new Dish
        {
          Id = 2,
          CreatedAt = DateTime.Now,
          Type = DishTypes.side,
          Name = "toast",
          TimeOfDay = "morning"
        },
        new Dish
        {
          Id = 3,
          CreatedAt = DateTime.Now,
          Type = DishTypes.drink,
          Name = "coffee",
          TimeOfDay = "morning"
        },
        new Dish
        {
          Id = 4,
          CreatedAt = DateTime.Now,
          Type = DishTypes.entree,
          Name = "steak",
          TimeOfDay = "night"
        },
        new Dish
        {
          Id = 5,
          CreatedAt = DateTime.Now,
          Type = DishTypes.side,
          Name = "potato",
          TimeOfDay = "night"
        },
        new Dish
        {
          Id = 6,
          CreatedAt = DateTime.Now,
          Type = DishTypes.drink,
          Name = "wine",
          TimeOfDay = "night"
        },
        new Dish
        {
          Id = 7,
          CreatedAt = DateTime.Now,
          Type = DishTypes.dessert,
          Name = "cake",
          TimeOfDay = "night"
        }
      );
    }
  }
}
