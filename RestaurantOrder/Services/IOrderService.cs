using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantOrder.ViewModels;

namespace RestaurantOrder.Services
{
  public interface IOrderService
  {
      Task<IList<OrderViewModel>> FindAllAsync();
      Task<OrderViewModel> FindByIdAsync(long id);
      Task<OrderViewModel> Create(PostOrderViewModel model);
  }
}
