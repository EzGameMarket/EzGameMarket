using OrderService.API.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.API.Services
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByID(string id);
    }
}
