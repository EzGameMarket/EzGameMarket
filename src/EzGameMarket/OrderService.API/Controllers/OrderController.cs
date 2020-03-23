using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.API.Data;
using System.Threading.Tasks;

namespace OrderService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private OrderDbContext _dbContext;

        public OrderController(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ActionResult> CancelOrder(string orderID)
        {
            return Accepted();
        }
    }
}