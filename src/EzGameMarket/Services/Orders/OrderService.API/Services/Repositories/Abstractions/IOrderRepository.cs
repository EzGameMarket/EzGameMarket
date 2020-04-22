using OrderService.API.Models.DbModels;
using System.Threading.Tasks;

namespace OrderService.API.Services.Repositories.Abstractions
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByID(string id);
    }
}