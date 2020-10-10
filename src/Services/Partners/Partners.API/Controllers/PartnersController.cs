using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Extensions.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Partners.API.Services.Repositories.Abstractions;
using Partners.API.Model;
using Shared.Services.API.Communication.Models;

namespace Partners.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnersController : ControllerBase
    {
        private IPartnerRepository _partnerRepository;

        public PartnersController(IPartnerRepository partnerRepository)
        {
            _partnerRepository = partnerRepository;
        }

        public async Task<ActionResult<APIResponse<PartnerModel>>> GetByID()
        {
            return default;
        }

        public async Task<ActionResult<APIResponse<List<PartnerModel>>>> GetAll()
        {
            return default;
        }

        public async Task<ActionResult<APIResponse<PaginationViewModel<PartnerModel>>>> GetPaginated(int pageIndex = 0, int pageSize = 30)
        {
            return default;
        }

        public async Task<ActionResult<APIResponseBase>> Add(PartnerModel model)
        {
            return default;
        }

        public async Task<ActionResult<APIResponseBase>> Modify(int id,PartnerModel model)
        {
            return default;
        }
    }
}